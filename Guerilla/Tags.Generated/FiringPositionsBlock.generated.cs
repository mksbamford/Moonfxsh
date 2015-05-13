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
    
    public partial class FiringPositionsBlock : GuerillaBlock, IWriteQueueable
    {
        /// <summary>
        /// Ctrl-N: Creates a new area and assigns it to the current selection of firing points.
        /// </summary>
        public OpenTK.Vector3 Position;
        public short ReferenceFrame;
        public Flags FiringPositionsFlags;
        public Moonfish.Tags.ShortBlockIndex1 Area;
        public short ClusterIndex;
        private byte[] fieldskip = new byte[4];
        public OpenTK.Vector2 Normal;
        public override int SerializedSize
        {
            get
            {
                return 32;
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
            this.Position = binaryReader.ReadVector3();
            this.ReferenceFrame = binaryReader.ReadInt16();
            this.FiringPositionsFlags = ((Flags)(binaryReader.ReadInt16()));
            this.Area = binaryReader.ReadShortBlockIndex1();
            this.ClusterIndex = binaryReader.ReadInt16();
            this.fieldskip = binaryReader.ReadBytes(4);
            this.Normal = binaryReader.ReadVector2();
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
            queueableBinaryWriter.Write(this.Position);
            queueableBinaryWriter.Write(this.ReferenceFrame);
            queueableBinaryWriter.Write(((short)(this.FiringPositionsFlags)));
            queueableBinaryWriter.Write(this.Area);
            queueableBinaryWriter.Write(this.ClusterIndex);
            queueableBinaryWriter.Write(this.fieldskip);
            queueableBinaryWriter.Write(this.Normal);
        }
        [System.FlagsAttribute()]
        public enum Flags : short
        {
            None = 0,
            Open = 1,
            Partial = 2,
            Closed = 4,
            Mobile = 8,
            WallLean = 16,
            Perch = 32,
            GroundPoint = 64,
            DynamicCoverPoint = 128,
        }
    }
}