// ReSharper disable All

using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ScenarioAtmosphericFogMixerBlock : ScenarioAtmosphericFogMixerBlockBase
    {
        public ScenarioAtmosphericFogMixerBlock( BinaryReader binaryReader ) : base( binaryReader )
        {
        }
    };

    [LayoutAttribute( Size = 16, Alignment = 4 )]
    public class ScenarioAtmosphericFogMixerBlockBase : IGuerilla
    {
        internal byte[] invalidName_;
        internal Moonfish.Tags.StringID atmosphericFogSourceFromScenarioAtmosphericFogPalette;
        internal Moonfish.Tags.StringID interpolatorFromScenarioInterpolators;
        internal byte[] invalidName_0;
        internal byte[] invalidName_1;

        internal ScenarioAtmosphericFogMixerBlockBase( BinaryReader binaryReader )
        {
            invalidName_ = binaryReader.ReadBytes( 4 );
            atmosphericFogSourceFromScenarioAtmosphericFogPalette = binaryReader.ReadStringID( );
            interpolatorFromScenarioInterpolators = binaryReader.ReadStringID( );
            invalidName_0 = binaryReader.ReadBytes( 2 );
            invalidName_1 = binaryReader.ReadBytes( 2 );
        }

        public int Write( System.IO.BinaryWriter binaryWriter, Int32 nextAddress )
        {
            using ( binaryWriter.BaseStream.Pin( ) )
            {
                binaryWriter.Write( invalidName_, 0, 4 );
                binaryWriter.Write( atmosphericFogSourceFromScenarioAtmosphericFogPalette );
                binaryWriter.Write( interpolatorFromScenarioInterpolators );
                binaryWriter.Write( invalidName_0, 0, 2 );
                binaryWriter.Write( invalidName_1, 0, 2 );
                return nextAddress;
            }
        }
    };
}