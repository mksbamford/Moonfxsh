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
    
    public partial class GlobalParticleSystemLiteBlock : GuerillaBlock, IWriteQueueable
    {
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference Sprites;
        public float ViewBoxWidth;
        public float ViewBoxHeight;
        public float ViewBoxDepth;
        public float ExclusionRadius;
        public float MaxVelocity;
        public float MinMass;
        public float MaxMass;
        public float MinSize;
        public float MaxSize;
        public int MaximumNumberOfParticles;
        public OpenTK.Vector3 InitialVelocity;
        public float BitmapAnimationSpeed;
        public GlobalGeometryBlockInfoStructBlock GeometryBlockInfo = new GlobalGeometryBlockInfoStructBlock();
        public ParticleSystemLiteDataBlock[] ParticleSystemData = new ParticleSystemLiteDataBlock[0];
        public TypeEnum Type;
        private byte[] fieldpad = new byte[2];
        public float MininumOpacity;
        public float MaxinumOpacity;
        public float RainStreakScale;
        public float RainLineWidth;
        private byte[] fieldpad0 = new byte[4];
        private byte[] fieldpad1 = new byte[4];
        private byte[] fieldpad2 = new byte[4];
        public override int SerializedSize
        {
            get
            {
                return 140;
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
            this.Sprites = binaryReader.ReadTagReference();
            this.ViewBoxWidth = binaryReader.ReadSingle();
            this.ViewBoxHeight = binaryReader.ReadSingle();
            this.ViewBoxDepth = binaryReader.ReadSingle();
            this.ExclusionRadius = binaryReader.ReadSingle();
            this.MaxVelocity = binaryReader.ReadSingle();
            this.MinMass = binaryReader.ReadSingle();
            this.MaxMass = binaryReader.ReadSingle();
            this.MinSize = binaryReader.ReadSingle();
            this.MaxSize = binaryReader.ReadSingle();
            this.MaximumNumberOfParticles = binaryReader.ReadInt32();
            this.InitialVelocity = binaryReader.ReadVector3();
            this.BitmapAnimationSpeed = binaryReader.ReadSingle();
            pointerQueue = new System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer>(pointerQueue.Concat(this.GeometryBlockInfo.ReadFields(binaryReader)));
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(48));
            this.Type = ((TypeEnum)(binaryReader.ReadInt16()));
            this.fieldpad = binaryReader.ReadBytes(2);
            this.MininumOpacity = binaryReader.ReadSingle();
            this.MaxinumOpacity = binaryReader.ReadSingle();
            this.RainStreakScale = binaryReader.ReadSingle();
            this.RainLineWidth = binaryReader.ReadSingle();
            this.fieldpad0 = binaryReader.ReadBytes(4);
            this.fieldpad1 = binaryReader.ReadBytes(4);
            this.fieldpad2 = binaryReader.ReadBytes(4);
            return pointerQueue;
        }
        public override void ReadInstances(System.IO.BinaryReader binaryReader, System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue)
        {
            base.ReadInstances(binaryReader, pointerQueue);
            this.GeometryBlockInfo.ReadInstances(binaryReader, pointerQueue);
            this.ParticleSystemData = base.ReadBlockArrayData<ParticleSystemLiteDataBlock>(binaryReader, pointerQueue.Dequeue());
        }
        public override void QueueWrites(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.QueueWrites(queueableBinaryWriter);
            this.GeometryBlockInfo.QueueWrites(queueableBinaryWriter);
            queueableBinaryWriter.QueueWrite(this.ParticleSystemData);
        }
        public override void Write_(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.Write_(queueableBinaryWriter);
            queueableBinaryWriter.Write(this.Sprites);
            queueableBinaryWriter.Write(this.ViewBoxWidth);
            queueableBinaryWriter.Write(this.ViewBoxHeight);
            queueableBinaryWriter.Write(this.ViewBoxDepth);
            queueableBinaryWriter.Write(this.ExclusionRadius);
            queueableBinaryWriter.Write(this.MaxVelocity);
            queueableBinaryWriter.Write(this.MinMass);
            queueableBinaryWriter.Write(this.MaxMass);
            queueableBinaryWriter.Write(this.MinSize);
            queueableBinaryWriter.Write(this.MaxSize);
            queueableBinaryWriter.Write(this.MaximumNumberOfParticles);
            queueableBinaryWriter.Write(this.InitialVelocity);
            queueableBinaryWriter.Write(this.BitmapAnimationSpeed);
            this.GeometryBlockInfo.Write_(queueableBinaryWriter);
            queueableBinaryWriter.WritePointer(this.ParticleSystemData);
            queueableBinaryWriter.Write(((short)(this.Type)));
            queueableBinaryWriter.Write(this.fieldpad);
            queueableBinaryWriter.Write(this.MininumOpacity);
            queueableBinaryWriter.Write(this.MaxinumOpacity);
            queueableBinaryWriter.Write(this.RainStreakScale);
            queueableBinaryWriter.Write(this.RainLineWidth);
            queueableBinaryWriter.Write(this.fieldpad0);
            queueableBinaryWriter.Write(this.fieldpad1);
            queueableBinaryWriter.Write(this.fieldpad2);
        }
        public enum TypeEnum : short
        {
            Generic = 0,
            Snow = 1,
            Rain = 2,
            RainSplash = 3,
            Bugs = 4,
            SandStorm = 5,
            Debris = 6,
            Bubbles = 7,
        }
    }
}