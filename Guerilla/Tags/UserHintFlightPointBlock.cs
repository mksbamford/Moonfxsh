using Moonfish.Tags.BlamExtension;
using System.IO;
using Moonfish.Tags;
using OpenTK;

namespace Moonfish.Guerilla.Tags
{
    public partial class UserHintFlightPointBlock : UserHintFlightPointBlockBase
    {
        public UserHintFlightPointBlock( BinaryReader binaryReader )
            : base( binaryReader )
        {

        }
    };
    [Layout( Size = 12 )]
    public class UserHintFlightPointBlockBase
    {
        internal Vector3 point;
        internal UserHintFlightPointBlockBase( BinaryReader binaryReader )
        {
            this.point = binaryReader.ReadVector3();
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
