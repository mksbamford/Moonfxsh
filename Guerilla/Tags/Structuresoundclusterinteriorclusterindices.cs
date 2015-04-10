using Moonfish.Tags.BlamExtension;
using System.IO;
using Moonfish.Tags;

namespace Moonfish.Guerilla.Tags
{
    public partial class StructureSoundClusterInteriorClusterIndices : StructureSoundClusterInteriorClusterIndicesBase
    {
        public StructureSoundClusterInteriorClusterIndices( BinaryReader binaryReader )
            : base( binaryReader )
        {

        }
    };
    [Layout( Size = 2 )]
    public class StructureSoundClusterInteriorClusterIndicesBase
    {
        internal short interiorClusterIndex;
        internal StructureSoundClusterInteriorClusterIndicesBase( BinaryReader binaryReader )
        {
            this.interiorClusterIndex = binaryReader.ReadInt16();
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
