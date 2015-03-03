﻿using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Moonfish.Graphics
{
    public class Scene
    {
        public Performance Performance { get; private set; }
        public MeshManager ObjectManager { get; set; }
        public ProgramManager ProgramManager { get; set; }
        Stopwatch Timer { get; set; }
        public Camera Camera { get; set; }

        OpenTK.Vector3 lightPosition = new OpenTK.Vector3( 3.8f, 3.0F, 3.5f );
        float rotation = 0;

        public event EventHandler OnFrameReady;

        CoordinateGrid Grid;

        public Scene( )
        {
            Initialize();
        }

        public virtual void Initialize( )
        {
            Console.WriteLine( GL.GetString( StringName.Version ) );
            Timer = new Stopwatch();
            Camera = new Camera();
            ObjectManager = new MeshManager();
            ProgramManager = new ProgramManager();
            Performance = new Performance();
            Grid = new CoordinateGrid( 2, 2 );

            Camera.ViewProjectionMatrixChanged += Camera_ViewProjectionMatrixChanged;
            Camera.ViewMatrixChanged += Camera_ViewMatrixChanged;
            Camera.Viewport.ViewportChanged += Viewport_ViewportChanged;

            OpenGL.ReportError();
            GL.ClearColor( Color.FromArgb(~Colours.Green.ToArgb()) );
            OpenGL.ReportError();
            GL.FrontFace( FrontFaceDirection.Ccw );
            OpenGL.ReportError();
            GL.Enable( EnableCap.CullFace );
            OpenGL.ReportError();
            GL.Enable( EnableCap.DepthTest );
            OpenGL.ReportError();
        }

        void Viewport_ViewportChanged( object sender, Viewport.ViewportEventArgs e )
        {
            GL.Viewport( 0, 0, e.Viewport.Width, e.Viewport.Height );
        }

        void Camera_ViewMatrixChanged( object sender, MatrixChangedEventArgs e )
        {
            foreach ( var program in ProgramManager )
            {
                program.Use();
                var viewMatrixUniform = program.GetUniformLocation( "ViewMatrixUniform" );
                program.SetUniform( viewMatrixUniform, ref e.Matrix );
            }
        }

        void Camera_ViewProjectionMatrixChanged( object sender, MatrixChangedEventArgs e )
        {
            foreach ( var program in ProgramManager )
            {
                program.Use();
                var viewProjectionMatrixUniform = program.GetUniformLocation( "ViewProjectionMatrixUniform" );
                program.SetUniform( viewProjectionMatrixUniform, ref e.Matrix );
            }
        }

        public virtual void RenderFrame( )
        {
            //Console.WriteLine("RenderFrame()");
            BeginFrame();
            Draw( Performance.Delta );
            EndFrame();
        }

        private void EndFrame( )
        {
            //Console.WriteLine("EndFrame()");
            GL.Finish();
            Performance.EndFrame();
            if ( OnFrameReady != null ) OnFrameReady( this, new EventArgs() );
        }

        private void BeginFrame( )
        {
            //Console.WriteLine("BeginFrame()");
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );
            Performance.BeginFrame();
        }

        public virtual void Draw( float delta )
        {
            //Console.WriteLine("Draw()");

            ObjectManager.Draw( ProgramManager );
            var program = ProgramManager.GetProgram( new ShaderReference( ShaderReference.ReferenceType.System, 0 ) );
            using ( program.Use() )
            {
                var colourUniform = program.GetUniformLocation( "Colour" );
                program.SetUniform( colourUniform, new ColorF( System.Drawing.Color.Black ).RGBA );
                //Grid.Draw();

            }
        }

        public virtual void Update( )
        {
            //Console.WriteLine("Update()");

            //var R = OpenTK.Matrix4.CreateRotationX( rotation );
            //rotation += OpenTK.MathHelper.DegreesToRadians( 0.03f );
            //rotation = rotation > Maths.Pi2 ? 0 : rotation;
            //var l = OpenTK.Vector3.Transform( lightPosition, R ); //Console.WriteLine(rotation);
            foreach ( var program in ProgramManager )
            {
                var lightPositionAttribute = program.GetUniformLocation( "LightPositionUniform" );

                using ( program.Use() )
                    program.SetUniform( lightPositionAttribute, new OpenTK.Vector4( lightPosition ) );

            }
            Camera.Update();
        }
    };
}
