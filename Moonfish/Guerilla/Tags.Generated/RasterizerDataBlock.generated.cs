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
    
    public partial class RasterizerDataBlock : GuerillaBlock, IWriteQueueable
    {
        /// <summary>
        /// Used internally by the rasterizer. (Do not change unless you know what you're doing!)
        /// </summary>
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference DistanceAttenuation;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference VectorNormalization;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference Gradients;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED0;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED1;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference Glow;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED2;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED3;
        private byte[] fieldpad = new byte[16];
        public VertexShaderReferenceBlock[] GlobalVertexShaders = new VertexShaderReferenceBlock[0];
        /// <summary>
        /// Used internally by the rasterizer - additive, multiplicative, detail, vector. (Do not change ever, period.)
        /// </summary>
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference Default2D;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference Default3D;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference DefaultCubeMap;
        /// <summary>
        /// Used internally by the rasterizer. (Used by Bernie's experimental shaders.)
        /// </summary>
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED4;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED5;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED6;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED7;
        /// <summary>
        /// Used in cinematics.
        /// </summary>
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED8;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED9;
        private byte[] fieldpad0 = new byte[36];
        /// <summary>
        /// Used for layers that need to do something for other layers to work correctly if the layer is disabled, also used for active-camo, etc.
        /// </summary>
        [Moonfish.Tags.TagReferenceAttribute("shad")]
        public Moonfish.Tags.TagReference GlobalShader;
        public Flags RasterizerDataFlags;
        private byte[] fieldpad1 = new byte[2];
        public float RefractionAmount;
        public float DistanceFalloff;
        public Moonfish.Tags.ColourR8G8B8 TintColor;
        public float HyperstealthRefraction;
        public float HyperstealthDistanceFalloff;
        public Moonfish.Tags.ColourR8G8B8 HyperstealthTintColor;
        /// <summary>
        /// The PC can't use 3D textures, so we use this instead.
        /// </summary>
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference UNUSED10;
        public override int SerializedSize
        {
            get
            {
                return 264;
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
            this.DistanceAttenuation = binaryReader.ReadTagReference();
            this.VectorNormalization = binaryReader.ReadTagReference();
            this.Gradients = binaryReader.ReadTagReference();
            this.UNUSED = binaryReader.ReadTagReference();
            this.UNUSED0 = binaryReader.ReadTagReference();
            this.UNUSED1 = binaryReader.ReadTagReference();
            this.Glow = binaryReader.ReadTagReference();
            this.UNUSED2 = binaryReader.ReadTagReference();
            this.UNUSED3 = binaryReader.ReadTagReference();
            this.fieldpad = binaryReader.ReadBytes(16);
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(8));
            this.Default2D = binaryReader.ReadTagReference();
            this.Default3D = binaryReader.ReadTagReference();
            this.DefaultCubeMap = binaryReader.ReadTagReference();
            this.UNUSED4 = binaryReader.ReadTagReference();
            this.UNUSED5 = binaryReader.ReadTagReference();
            this.UNUSED6 = binaryReader.ReadTagReference();
            this.UNUSED7 = binaryReader.ReadTagReference();
            this.UNUSED8 = binaryReader.ReadTagReference();
            this.UNUSED9 = binaryReader.ReadTagReference();
            this.fieldpad0 = binaryReader.ReadBytes(36);
            this.GlobalShader = binaryReader.ReadTagReference();
            this.RasterizerDataFlags = ((Flags)(binaryReader.ReadInt16()));
            this.fieldpad1 = binaryReader.ReadBytes(2);
            this.RefractionAmount = binaryReader.ReadSingle();
            this.DistanceFalloff = binaryReader.ReadSingle();
            this.TintColor = binaryReader.ReadColorR8G8B8();
            this.HyperstealthRefraction = binaryReader.ReadSingle();
            this.HyperstealthDistanceFalloff = binaryReader.ReadSingle();
            this.HyperstealthTintColor = binaryReader.ReadColorR8G8B8();
            this.UNUSED10 = binaryReader.ReadTagReference();
            return pointerQueue;
        }
        public override void ReadInstances(System.IO.BinaryReader binaryReader, System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue)
        {
            base.ReadInstances(binaryReader, pointerQueue);
            this.GlobalVertexShaders = base.ReadBlockArrayData<VertexShaderReferenceBlock>(binaryReader, pointerQueue.Dequeue());
        }
        public override void QueueWrites(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.QueueWrites(queueableBinaryWriter);
            queueableBinaryWriter.QueueWrite(this.GlobalVertexShaders);
        }
        public override void Write_(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.Write_(queueableBinaryWriter);
            queueableBinaryWriter.Write(this.DistanceAttenuation);
            queueableBinaryWriter.Write(this.VectorNormalization);
            queueableBinaryWriter.Write(this.Gradients);
            queueableBinaryWriter.Write(this.UNUSED);
            queueableBinaryWriter.Write(this.UNUSED0);
            queueableBinaryWriter.Write(this.UNUSED1);
            queueableBinaryWriter.Write(this.Glow);
            queueableBinaryWriter.Write(this.UNUSED2);
            queueableBinaryWriter.Write(this.UNUSED3);
            queueableBinaryWriter.Write(this.fieldpad);
            queueableBinaryWriter.WritePointer(this.GlobalVertexShaders);
            queueableBinaryWriter.Write(this.Default2D);
            queueableBinaryWriter.Write(this.Default3D);
            queueableBinaryWriter.Write(this.DefaultCubeMap);
            queueableBinaryWriter.Write(this.UNUSED4);
            queueableBinaryWriter.Write(this.UNUSED5);
            queueableBinaryWriter.Write(this.UNUSED6);
            queueableBinaryWriter.Write(this.UNUSED7);
            queueableBinaryWriter.Write(this.UNUSED8);
            queueableBinaryWriter.Write(this.UNUSED9);
            queueableBinaryWriter.Write(this.fieldpad0);
            queueableBinaryWriter.Write(this.GlobalShader);
            queueableBinaryWriter.Write(((short)(this.RasterizerDataFlags)));
            queueableBinaryWriter.Write(this.fieldpad1);
            queueableBinaryWriter.Write(this.RefractionAmount);
            queueableBinaryWriter.Write(this.DistanceFalloff);
            queueableBinaryWriter.Write(this.TintColor);
            queueableBinaryWriter.Write(this.HyperstealthRefraction);
            queueableBinaryWriter.Write(this.HyperstealthDistanceFalloff);
            queueableBinaryWriter.Write(this.HyperstealthTintColor);
            queueableBinaryWriter.Write(this.UNUSED10);
        }
        [System.FlagsAttribute()]
        public enum Flags : short
        {
            None = 0,
            TintEdgeDensity = 1,
        }
    }
}