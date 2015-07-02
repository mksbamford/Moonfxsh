﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Moonfish.Graphics.Input;
using Moonfish.Graphics.Primitives;
using OpenTK.Graphics.ES10;

namespace Moonfish.Graphics
{
    public partial class DynamicScene : Scene
    {
        public ConvexHullCaster caster;

        public DynamicScene( )
        {
            caster = new ConvexHullCaster( );
            SceneUpdate += caster.OnUpdate;
            DrawDebugCollision = false;
            CollisionManager = new CollisionManager( ProgramManager.SystemProgram );
            MousePole = new TranslationGizmo( );
            Camera.CameraUpdated += MousePole.OnCameraUpdate;
            SelectedObjectChanged += OnSelectedObjectChanged;
            SelectedObjectChanged += caster.OnSelectedObjectChanged;
            MouseMove += caster.OnMouseMove;
            MouseUp += caster.OnMouseUp;
            foreach ( var item in MousePole.CollisionObjects )
                CollisionManager.World.AddCollisionObject( item );

#if DEBUG
            GLDebug.DebugProgram = ProgramManager.SystemProgram;
            GLDebug.ScreenspaceProgram = ProgramManager.ScreenProgram;
#endif
        }

        public CollisionManager CollisionManager { get; set; }
        public bool DrawDebugCollision { get; set; }
        public TranslationGizmo MousePole { get; set; }

        public override void Draw( float delta )
        {
            base.Draw( delta );
            ObjectManager.Draw( ProgramManager, MousePole.Model );

#if DEBUG
            if ( DrawDebugCollision || true )
                CollisionManager.World.DebugDrawWorld( );

            GLDebug.DrawPoints( Color.Red, 5.0f );
            GLDebug.DrawLines( Color.Yellow, 1.0f );
#endif
            //GLDebug.DrawPoint(caster.debugPoint2, Color.Gold, 5);
            //GLDebug.DrawPoint(caster.debugPoint3, Color.DodgerBlue, 5);
        }

        private event EventHandler SceneUpdate;

        public override void Update( )
        {
            if (SceneUpdate!=null)SceneUpdate(this, new EventArgs(  ));

            ObjectManager.Update( );
            CollisionManager.Update();
            base.Update( );
        }

        private void OnSelectedObjectChanged( object seneder, SelectEventArgs e )
        {
            //if (e.SelectedObject == null)
            //{
            //    MousePole.DropHandlers();
            //    MousePole.Show(false);
            //    return;
            //}
            //var item = e.SelectedObject as CollisionObject;
            //if (item != null)
            //{
            //    var scenarioObject = item.UserObject as ScenarioObject;
            //    if ( scenarioObject == null ) return;

            //    MousePole.Show(true);
            //    MousePole.DropHandlers();
            //    MousePole.WorldMatrix = item.WorldTransform;
            //    MousePole.WorldMatrixChanged +=
            //        delegate(object sender, MatrixChangedEventArgs args)
            //        {
            //            scenarioObject.WorldMatrix = args.Matrix.ClearScale(  );
            //        };
            //}
        }
    }
}