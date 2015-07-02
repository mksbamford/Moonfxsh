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
    
    public partial class GlobalStructurePhysicsStructBlock : GuerillaBlock, IWriteQueueable
    {
        public byte[] moppCode;
        private byte[] fieldpad = new byte[4];
        public OpenTK.Vector3 moppBoundsMin;
        public OpenTK.Vector3 moppBoundsMax;
        public byte[] BreakableSurfacesMoppCode;
        public BreakableSurfaceKeyTableBlock[] BreakableSurfaceKeyTable = new BreakableSurfaceKeyTableBlock[0];
        public override int SerializedSize
        {
            get
            {
                return 52;
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
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(1));
            this.fieldpad = binaryReader.ReadBytes(4);
            this.moppBoundsMin = binaryReader.ReadVector3();
            this.moppBoundsMax = binaryReader.ReadVector3();
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(1));
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(32));
            return pointerQueue;
        }
        public override void ReadInstances(System.IO.BinaryReader binaryReader, System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue)
        {
            base.ReadInstances(binaryReader, pointerQueue);
            this.moppCode = base.ReadDataByteArray(binaryReader, pointerQueue.Dequeue());
            this.BreakableSurfacesMoppCode = base.ReadDataByteArray(binaryReader, pointerQueue.Dequeue());
            this.BreakableSurfaceKeyTable = base.ReadBlockArrayData<BreakableSurfaceKeyTableBlock>(binaryReader, pointerQueue.Dequeue());
        }
        public override void QueueWrites(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.QueueWrites(queueableBinaryWriter);
            queueableBinaryWriter.QueueWrite(this.moppCode, 16);
            queueableBinaryWriter.QueueWrite(this.BreakableSurfacesMoppCode);
            queueableBinaryWriter.QueueWrite(this.BreakableSurfaceKeyTable);
        }
        public override void Write_(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.Write_(queueableBinaryWriter);
            queueableBinaryWriter.WritePointer(this.moppCode);
            queueableBinaryWriter.Write(this.fieldpad);
            queueableBinaryWriter.Write(this.moppBoundsMin);
            queueableBinaryWriter.Write(this.moppBoundsMax);
            queueableBinaryWriter.WritePointer(this.BreakableSurfacesMoppCode);
            queueableBinaryWriter.WritePointer(this.BreakableSurfaceKeyTable);
        }
    }
}
