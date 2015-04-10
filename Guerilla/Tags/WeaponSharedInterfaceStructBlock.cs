using Moonfish.Tags.BlamExtension;
using System.IO;
using Moonfish.Tags;

namespace Moonfish.Guerilla.Tags
{
    public partial class WeaponSharedInterfaceStructBlock : WeaponSharedInterfaceStructBlockBase
    {
        public WeaponSharedInterfaceStructBlock( BinaryReader binaryReader )
            : base( binaryReader )
        {

        }
    };
    [Layout( Size = 16 )]
    public class WeaponSharedInterfaceStructBlockBase
    {
        internal byte[] invalidName_;
        internal WeaponSharedInterfaceStructBlockBase( BinaryReader binaryReader )
        {
            this.invalidName_ = binaryReader.ReadBytes( 16 );
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
