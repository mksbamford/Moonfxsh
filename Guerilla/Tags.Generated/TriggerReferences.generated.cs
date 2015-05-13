//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Moonfish.Guerilla.Tags
{
    using Moonfish.Tags;
    using Moonfish.Model;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    
    public partial class TriggerReferences : GuerillaBlock, IWriteQueueable
    {
        public TriggerFlags TriggerReferencesTriggerFlags;
        public Moonfish.Tags.ShortBlockIndex1 Trigger;
        private byte[] fieldpad = new byte[2];
        public override int SerializedSize
        {
            get
            {
                return 8;
            }
        }
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        public override System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> ReadFields(System.IO.BinaryReader binaryReader)
        {
            System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue = new System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer>(base.ReadFields(binaryReader));
            this.TriggerReferencesTriggerFlags = ((TriggerFlags)(binaryReader.ReadInt32()));
            this.Trigger = binaryReader.ReadShortBlockIndex1();
            this.fieldpad = binaryReader.ReadBytes(2);
            return pointerQueue;
        }
        public override void ReadInstances(System.IO.BinaryReader binaryReader, System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue)
        {
            base.ReadInstances(binaryReader, pointerQueue);
        }
        public override void QueueWrites(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.QueueWrites(queueableBinaryWriter);
        }
        public override void Write_(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.Write_(queueableBinaryWriter);
            queueableBinaryWriter.Write(((int)(this.TriggerReferencesTriggerFlags)));
            queueableBinaryWriter.Write(this.Trigger);
            queueableBinaryWriter.Write(this.fieldpad);
        }
        [System.FlagsAttribute()]
        public enum TriggerFlags : int
        {
            None = 0,
            Not = 1,
        }
    }
}