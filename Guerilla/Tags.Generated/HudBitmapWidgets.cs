// ReSharper disable All

using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class HudBitmapWidgets : HudBitmapWidgetsBase
    {
        public HudBitmapWidgets( BinaryReader binaryReader ) : base( binaryReader )
        {
        }
    };

    [LayoutAttribute( Size = 100, Alignment = 4 )]
    public class HudBitmapWidgetsBase : IGuerilla
    {
        internal Moonfish.Tags.StringID name;
        internal HudWidgetInputsStructBlock hudWidgetInputsStruct;
        internal HudWidgetStateDefinitionStructBlock hudWidgetStateDefinitionStruct;
        internal Anchor anchor;
        internal Flags flags;
        [TagReference( "bitm" )] internal Moonfish.Tags.TagReference bitmap;
        [TagReference( "shad" )] internal Moonfish.Tags.TagReference shader;
        internal byte fullscreenSequenceIndex;
        internal byte halfscreenSequenceIndex;
        internal byte quarterscreenSequenceIndex;
        internal byte[] invalidName_;
        internal Moonfish.Tags.Point fullscreenOffset;
        internal Moonfish.Tags.Point halfscreenOffset;
        internal Moonfish.Tags.Point quarterscreenOffset;
        internal OpenTK.Vector2 fullscreenRegistrationPoint;
        internal OpenTK.Vector2 halfscreenRegistrationPoint;
        internal OpenTK.Vector2 quarterscreenRegistrationPoint;
        internal HudWidgetEffectBlock[] effect;
        internal SpecialHudType specialHudType;
        internal byte[] invalidName_0;

        internal HudBitmapWidgetsBase( BinaryReader binaryReader )
        {
            name = binaryReader.ReadStringID( );
            hudWidgetInputsStruct = new HudWidgetInputsStructBlock( binaryReader );
            hudWidgetStateDefinitionStruct = new HudWidgetStateDefinitionStructBlock( binaryReader );
            anchor = ( Anchor ) binaryReader.ReadInt16( );
            flags = ( Flags ) binaryReader.ReadInt16( );
            bitmap = binaryReader.ReadTagReference( );
            shader = binaryReader.ReadTagReference( );
            fullscreenSequenceIndex = binaryReader.ReadByte( );
            halfscreenSequenceIndex = binaryReader.ReadByte( );
            quarterscreenSequenceIndex = binaryReader.ReadByte( );
            invalidName_ = binaryReader.ReadBytes( 1 );
            fullscreenOffset = binaryReader.ReadPoint( );
            halfscreenOffset = binaryReader.ReadPoint( );
            quarterscreenOffset = binaryReader.ReadPoint( );
            fullscreenRegistrationPoint = binaryReader.ReadVector2( );
            halfscreenRegistrationPoint = binaryReader.ReadVector2( );
            quarterscreenRegistrationPoint = binaryReader.ReadVector2( );
            effect = Guerilla.ReadBlockArray<HudWidgetEffectBlock>( binaryReader );
            specialHudType = ( SpecialHudType ) binaryReader.ReadInt16( );
            invalidName_0 = binaryReader.ReadBytes( 2 );
        }

        public int Write( System.IO.BinaryWriter binaryWriter, Int32 nextAddress )
        {
            using ( binaryWriter.BaseStream.Pin( ) )
            {
                binaryWriter.Write( name );
                hudWidgetInputsStruct.Write( binaryWriter );
                hudWidgetStateDefinitionStruct.Write( binaryWriter );
                binaryWriter.Write( ( Int16 ) anchor );
                binaryWriter.Write( ( Int16 ) flags );
                binaryWriter.Write( bitmap );
                binaryWriter.Write( shader );
                binaryWriter.Write( fullscreenSequenceIndex );
                binaryWriter.Write( halfscreenSequenceIndex );
                binaryWriter.Write( quarterscreenSequenceIndex );
                binaryWriter.Write( invalidName_, 0, 1 );
                binaryWriter.Write( fullscreenOffset );
                binaryWriter.Write( halfscreenOffset );
                binaryWriter.Write( quarterscreenOffset );
                binaryWriter.Write( fullscreenRegistrationPoint );
                binaryWriter.Write( halfscreenRegistrationPoint );
                binaryWriter.Write( quarterscreenRegistrationPoint );
                nextAddress = Guerilla.WriteBlockArray<HudWidgetEffectBlock>( binaryWriter, effect, nextAddress );
                binaryWriter.Write( ( Int16 ) specialHudType );
                binaryWriter.Write( invalidName_0, 0, 2 );
                return nextAddress;
            }
        }

        internal enum Anchor : short
        {
            HealthAndShield = 0,
            WeaponHud = 1,
            MotionSensor = 2,
            Scoreboard = 3,
            Crosshair = 4,
            LockOnTarget = 5,
        };

        [FlagsAttribute]
        internal enum Flags : short
        {
            FlipHorizontally = 1,
            FlipVertically = 2,
            ScopeMirrorHorizontally = 4,
            ScopeMirrorVertically = 8,
            ScopeStretch = 16,
        };

        internal enum SpecialHudType : short
        {
            Unspecial = 0,
            SBPlayerEmblem = 1,
            SBOtherPlayerEmblem = 2,
            SBPlayerScoreMeter = 3,
            SBOtherPlayerScoreMeter = 4,
            UnitShieldMeter = 5,
            MotionSensor = 6,
            TerritoryMeter = 7,
        };
    };
}