using Moonfish.Tags.BlamExtension;
using System.IO;
using Moonfish.Tags;
using OpenTK;

namespace Moonfish.Guerilla.Tags
{
    public partial class UnitPosturesBlock : UnitPosturesBlockBase
    {
        public UnitPosturesBlock( BinaryReader binaryReader )
            : base( binaryReader )
        {

        }
    };
    [Layout( Size = 16 )]
    public class UnitPosturesBlockBase
    {
        internal StringID name;
        internal Vector3 pillOffset;
        internal UnitPosturesBlockBase( BinaryReader binaryReader )
        {
            this.name = binaryReader.ReadStringID();
            this.pillOffset = binaryReader.ReadVector3();
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
