// ReSharper disable All

using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Tags
{
    public partial struct TagClass
    {
        public static readonly TagClass Sncl = ( TagClass ) "sncl";
    };
} ;

namespace Moonfish.Guerilla.Tags
{
    [TagClassAttribute( "sncl" )]
    public partial class SoundClassesBlock : SoundClassesBlockBase
    {
        public SoundClassesBlock( BinaryReader binaryReader ) : base( binaryReader )
        {
        }
    };

    [LayoutAttribute( Size = 8, Alignment = 4 )]
    public class SoundClassesBlockBase : IGuerilla
    {
        internal SoundClassBlock[] soundClasses;

        internal SoundClassesBlockBase( BinaryReader binaryReader )
        {
            soundClasses = Guerilla.ReadBlockArray<SoundClassBlock>( binaryReader );
        }

        public int Write( System.IO.BinaryWriter binaryWriter, Int32 nextAddress )
        {
            using ( binaryWriter.BaseStream.Pin( ) )
            {
                nextAddress = Guerilla.WriteBlockArray<SoundClassBlock>( binaryWriter, soundClasses, nextAddress );
                return nextAddress;
            }
        }
    };
}