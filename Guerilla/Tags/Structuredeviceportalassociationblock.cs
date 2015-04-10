using Moonfish.Tags.BlamExtension;
using System.IO;
using Moonfish.Tags;

namespace Moonfish.Guerilla.Tags
{
    public partial class StructureDevicePortalAssociationBlock : StructureDevicePortalAssociationBlockBase
    {
        public StructureDevicePortalAssociationBlock( BinaryReader binaryReader )
            : base( binaryReader )
        {

        }
    };
    [Layout( Size = 12 )]
    public class StructureDevicePortalAssociationBlockBase
    {
        internal ScenarioObjectIdStructBlock deviceId;
        internal short firstGamePortalIndex;
        internal short gamePortalCount;
        internal StructureDevicePortalAssociationBlockBase( BinaryReader binaryReader )
        {
            this.deviceId = new ScenarioObjectIdStructBlock( binaryReader );
            this.firstGamePortalIndex = binaryReader.ReadInt16();
            this.gamePortalCount = binaryReader.ReadInt16();
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
