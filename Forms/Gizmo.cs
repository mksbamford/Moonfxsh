﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Moonfish.Guerilla.Tags;
using Moonfish.Tags;
using Point = System.Drawing.Point;

namespace Moonfish.Graphics
{
    public partial class Gizmo : Form
    {
        private DynamicScene Scene { get; set; }
        private CacheStream Map { get; set; }
        private TagIdent SelectedTag { get; set; }

        #region Peek Message Native

        [StructLayout( LayoutKind.Sequential )]
        public struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }

        [DllImport( "user32.dll" )]
        public static extern int PeekMessage( out NativeMessage message, IntPtr window, uint filterMin, uint filterMax,
            uint remove );

        #endregion

        private static bool IsApplicationIdle( )
        {
            NativeMessage result;
            return PeekMessage( out result, IntPtr.Zero, 0, 0, 0 ) == 0;
        }

        public Gizmo( )
        {
            InitializeComponent( );
            glControl1.Load += glControl1_Load;
        }

        private void glControl1_Load( object sender, EventArgs e )
        {
            Scene = new DynamicScene( );
            Application.Idle += HandleApplicationIdle;
            Scene.OnFrameReady += Scene_OnFrameReady;

            glControl1.Resize += glControl1_Resize;
            glControl1.MouseDown += Scene.Camera.OnMouseDown;
            glControl1.MouseMove += Scene.Camera.OnMouseMove;
            glControl1.MouseUp += Scene.Camera.OnMouseUp;
            glControl1.MouseCaptureChanged += Scene.Camera.OnMouseCaptureChanged;

            glControl1.MouseDown += Scene.OnMouseDown;
            glControl1.MouseMove += Scene.OnMouseMove;
            glControl1.MouseUp += Scene.OnMouseUp;
            glControl1.MouseClick += Scene.OnMouseClick;

            Open( Path.Combine(Local.MapsDirectory, "headlong.map") );

            var identifier = Map.Index.Select( TagClass.Hlmt, "banshee" ).First( ).Identifier;

            var @object = (ModelBlock)Map.Deserialize(identifier);
            foreach (IResourceBlock section in  @object.RenderModel.sections)
                section.LoadRawResources();
            var scenarioObject = new ScenarioObject( @object );
            Scene.ObjectManager.Add(identifier, scenarioObject);
            Scene.ProgramManager.LoadMaterials( @object.RenderModel.materials.Select( x => x.shader.Ident ), Map );
            Scene.ProgramManager.Materials.First().Value.UsePass(0);
            Scene.CollisionManager.LoadScenarioObjectCollision(
                Scene.ObjectManager[identifier].First());

            propertyGrid1.SelectedObject = scenarioObject.Nodes[ 0 ];

            //  firing this method is meant to load the view-projection matrix values into 
            //  the shader uniforms, and initalizes the camera
            glControl1_Resize( this, new EventArgs( ) );
        }

        private void Open( string fileName )
        {
            var directory = Path.GetDirectoryName( fileName );

            if ( directory != null )
                LoadResourceMaps( directory );

            Map = new CacheStream( fileName );
        }

        private static void LoadResourceMaps( string directory )
        {
            var maps = Directory.GetFiles( directory, "*.map", SearchOption.TopDirectoryOnly );
            var resourceMaps = maps.GroupBy(
                Halo2.CheckMapType
                ).Where( x => x.Key == MapType.MainMenu
                              || x.Key == MapType.Shared
                              || x.Key == MapType.SinglePlayerShared )
                .Select( g => g.First( ) ).ToList( );
            resourceMaps.ForEach( x => Halo2.LoadResource( new CacheStream( x ) ) );
        }

        private void glControl1_Resize( object sender, EventArgs e )
        {
            ChangeViewport( glControl1.Width, glControl1.Height );
            glControl1.Invalidate( );
        }

        private void ChangeViewport( int width, int height )
        {
            Scene.Camera.Viewport.Size = new Size( width, height );
        }

        private void Scene_OnFrameReady( object sender, EventArgs e )
        {
            glControl1.SwapBuffers( );
        }

        private void HandleApplicationIdle( object sender, EventArgs e )
        {
            while ( IsApplicationIdle( ) )
            {
                UpdateState( );
                Scene.Update( );
                Scene.RenderFrame( );
                propertyGrid1.Refresh( );
            }
        }

        private void UpdateState( )
        {
            lblRenderTime.Text = string.Format( lblRenderTime.Tag.ToString( ),
                TimeSpan.FromTicks( ( long ) Scene.Performance.FrameTime ).TotalMilliseconds );
        }
    }
}