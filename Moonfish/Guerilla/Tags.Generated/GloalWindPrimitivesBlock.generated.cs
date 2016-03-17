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
    
    public partial class GloalWindPrimitivesBlock : GuerillaBlock, IWriteQueueable
    {
        public OpenTK.Vector3 Position;
        public float Radius;
        public float Strength;
        public WindPrimitiveTypeEnum WindPrimitiveType;
        private byte[] fieldpad = new byte[2];
        public override int SerializedSize
        {
            get
            {
                return 24;
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
            this.Radius = binaryReader.ReadSingle();
            this.Strength = binaryReader.ReadSingle();
            this.WindPrimitiveType = ((WindPrimitiveTypeEnum)(binaryReader.ReadInt16()));
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
            queueableBinaryWriter.Write(this.Position);
            queueableBinaryWriter.Write(this.Radius);
            queueableBinaryWriter.Write(this.Strength);
            queueableBinaryWriter.Write(((short)(this.WindPrimitiveType)));
            queueableBinaryWriter.Write(this.fieldpad);
        }
        public enum WindPrimitiveTypeEnum : short
        {
            Vortex = 0,
            Gust = 1,
            Implosion = 2,
            Explosion = 3,
        }
    }
}