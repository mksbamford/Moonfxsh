using Moonfish.Tags.BlamExtension;
using System;
using System.IO;
using Moonfish.Tags;

namespace Moonfish.Guerilla.Tags
{
    public partial class TriggerReferences : TriggerReferencesBase
    {
        public TriggerReferences( BinaryReader binaryReader )
            : base( binaryReader )
        {

        }
    };
    [Layout( Size = 8 )]
    public class TriggerReferencesBase
    {
        internal TriggerFlags triggerFlags;
        internal ShortBlockIndex1 trigger;
        internal byte[] invalidName_;
        internal TriggerReferencesBase( BinaryReader binaryReader )
        {
            this.triggerFlags = ( TriggerFlags )binaryReader.ReadInt32();
            this.trigger = binaryReader.ReadShortBlockIndex1();
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
        [Flags]
        internal enum TriggerFlags : int
        {
            Not = 1,
        };
    };
}
