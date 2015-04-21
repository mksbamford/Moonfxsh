// ReSharper disable All

using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class MissionDialogueLinesBlock : MissionDialogueLinesBlockBase
    {
        public MissionDialogueLinesBlock( BinaryReader binaryReader ) : base( binaryReader )
        {
        }
    };

    [LayoutAttribute( Size = 16, Alignment = 4 )]
    public class MissionDialogueLinesBlockBase : IGuerilla
    {
        internal Moonfish.Tags.StringID name;
        internal MissionDialogueVariantsBlock[] variants;
        internal Moonfish.Tags.StringID defaultSoundEffect;

        internal MissionDialogueLinesBlockBase( BinaryReader binaryReader )
        {
            name = binaryReader.ReadStringID( );
            variants = Guerilla.ReadBlockArray<MissionDialogueVariantsBlock>( binaryReader );
            defaultSoundEffect = binaryReader.ReadStringID( );
        }

        public int Write( System.IO.BinaryWriter binaryWriter, Int32 nextAddress )
        {
            using ( binaryWriter.BaseStream.Pin( ) )
            {
                binaryWriter.Write( name );
                nextAddress = Guerilla.WriteBlockArray<MissionDialogueVariantsBlock>( binaryWriter, variants,
                    nextAddress );
                binaryWriter.Write( defaultSoundEffect );
                return nextAddress;
            }
        }
    };
}