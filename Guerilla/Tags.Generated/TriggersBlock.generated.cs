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
    
    public partial class TriggersBlock : GuerillaBlock, IWriteQueueable
    {
        public Moonfish.Tags.String32 Name;
        public TriggerFlags TriggersTriggerFlags;
        public CombinationRuleEnum CombinationRule;
        private byte[] fieldpad = new byte[2];
        public OrderCompletionCondition[] Conditions = new OrderCompletionCondition[0];
        public override int SerializedSize
        {
            get
            {
                return 48;
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
            this.Name = binaryReader.ReadString32();
            this.TriggersTriggerFlags = ((TriggerFlags)(binaryReader.ReadInt32()));
            this.CombinationRule = ((CombinationRuleEnum)(binaryReader.ReadInt16()));
            this.fieldpad = binaryReader.ReadBytes(2);
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(56));
            return pointerQueue;
        }
        public override void ReadInstances(System.IO.BinaryReader binaryReader, System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue)
        {
            base.ReadInstances(binaryReader, pointerQueue);
            this.Conditions = base.ReadBlockArrayData<OrderCompletionCondition>(binaryReader, pointerQueue.Dequeue());
        }
        public override void QueueWrites(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.QueueWrites(queueableBinaryWriter);
            queueableBinaryWriter.QueueWrite(this.Conditions);
        }
        public override void Write_(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.Write_(queueableBinaryWriter);
            queueableBinaryWriter.Write(this.Name);
            queueableBinaryWriter.Write(((int)(this.TriggersTriggerFlags)));
            queueableBinaryWriter.Write(((short)(this.CombinationRule)));
            queueableBinaryWriter.Write(this.fieldpad);
            queueableBinaryWriter.WritePointer(this.Conditions);
        }
        [System.FlagsAttribute()]
        public enum TriggerFlags : int
        {
            None = 0,
            LatchONWhenTriggered = 1,
        }
        public enum CombinationRuleEnum : short
        {
            OR = 0,
            AND = 1,
        }
    }
}