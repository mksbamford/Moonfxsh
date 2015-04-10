using Moonfish.Tags.BlamExtension;
using System.IO;
using Moonfish.Tags;
using OpenTK;

namespace Moonfish.Guerilla.Tags
{
    public partial class VerticesBlock : VerticesBlockBase
    {
        public VerticesBlock( BinaryReader binaryReader )
            : base( binaryReader )
        {

        }
    };
    [Layout( Size = 16 )]
    public class VerticesBlockBase
    {
        internal Vector3 point;
        internal short firstEdge;
        internal byte[] invalidName_;
        internal VerticesBlockBase( BinaryReader binaryReader )
        {
            this.point = binaryReader.ReadVector3();
            this.firstEdge = binaryReader.ReadInt16();
            this.invalidName_ = binaryReader.ReadBytes( 2 );
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
