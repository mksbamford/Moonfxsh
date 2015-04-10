using Moonfish.Tags.BlamExtension;
using System.IO;
using Moonfish.Tags;

namespace Moonfish.Guerilla.Tags
{
    public partial class TextureBlock : TextureBlockBase
    {
        public TextureBlock( BinaryReader binaryReader )
            : base( binaryReader )
        {

        }
    };
    [Layout( Size = 4 )]
    public class TextureBlockBase
    {
        internal byte stageIndex;
        internal byte parameterIndex;
        internal byte globalTextureIndex;
        internal byte flags;
        internal TextureBlockBase( BinaryReader binaryReader )
        {
            this.stageIndex = binaryReader.ReadByte();
            this.parameterIndex = binaryReader.ReadByte();
            this.globalTextureIndex = binaryReader.ReadByte();
            this.flags = binaryReader.ReadByte();
        }
        internal virtual byte[] ReadData( BinaryReader binaryReader )
        {
            var blamPointer = binaryReader.ReadBlamPointer( 1 );
            var data = new byte[ blamPointer.Count ];
            if ( blamPointer.Count > 0 )
            {
                using ( binaryReader.BaseStream.Pin() )
                {
                    binaryReader.BaseStream.Position = blamPointer[ 0 ];
                    data = binaryReader.ReadBytes( blamPointer.Count );
                }
            }
            return data;
        }
    };
}
