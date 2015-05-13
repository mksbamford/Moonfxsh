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
    
    public partial class GlobalHudMultitextureOverlayDefinition : GuerillaBlock, IWriteQueueable
    {
        private byte[] fieldpad = new byte[2];
        public short Type;
        public FramebufferBlendFuncEnum FramebufferBlendFunc;
        private byte[] fieldpad0 = new byte[2];
        private byte[] fieldpad1 = new byte[32];
        public PrimaryAnchorEnum PrimaryAnchor;
        public SecondaryAnchorEnum SecondaryAnchor;
        public TertiaryAnchorEnum TertiaryAnchor;
        public _0To1BlendFuncEnum _0To1BlendFunc;
        public _1To2BlendFuncEnum _1To2BlendFunc;
        private byte[] fieldpad2 = new byte[2];
        /// <summary>
        /// how much to scale the textures
        /// </summary>
        public OpenTK.Vector2 PrimaryScale;
        public OpenTK.Vector2 SecondaryScale;
        public OpenTK.Vector2 TertiaryScale;
        /// <summary>
        /// how much to offset the origin of the texture
        /// </summary>
        public OpenTK.Vector2 PrimaryOffset;
        public OpenTK.Vector2 SecondaryOffset;
        public OpenTK.Vector2 TertiaryOffset;
        /// <summary>
        /// which maps to use
        /// </summary>
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference Primary;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference Secondary;
        [Moonfish.Tags.TagReferenceAttribute("bitm")]
        public Moonfish.Tags.TagReference Tertiary;
        public PrimaryWrapModeEnum PrimaryWrapMode;
        public SecondaryWrapModeEnum SecondaryWrapMode;
        public TertiaryWrapModeEnum TertiaryWrapMode;
        private byte[] fieldpad3 = new byte[2];
        private byte[] fieldpad4 = new byte[184];
        public GlobalHudMultitextureOverlayEffectorDefinition[] Effectors = new GlobalHudMultitextureOverlayEffectorDefinition[0];
        private byte[] fieldpad5 = new byte[128];
        public override int SerializedSize
        {
            get
            {
                return 452;
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
            this.fieldpad = binaryReader.ReadBytes(2);
            this.Type = binaryReader.ReadInt16();
            this.FramebufferBlendFunc = ((FramebufferBlendFuncEnum)(binaryReader.ReadInt16()));
            this.fieldpad0 = binaryReader.ReadBytes(2);
            this.fieldpad1 = binaryReader.ReadBytes(32);
            this.PrimaryAnchor = ((PrimaryAnchorEnum)(binaryReader.ReadInt16()));
            this.SecondaryAnchor = ((SecondaryAnchorEnum)(binaryReader.ReadInt16()));
            this.TertiaryAnchor = ((TertiaryAnchorEnum)(binaryReader.ReadInt16()));
            this._0To1BlendFunc = ((_0To1BlendFuncEnum)(binaryReader.ReadInt16()));
            this._1To2BlendFunc = ((_1To2BlendFuncEnum)(binaryReader.ReadInt16()));
            this.fieldpad2 = binaryReader.ReadBytes(2);
            this.PrimaryScale = binaryReader.ReadVector2();
            this.SecondaryScale = binaryReader.ReadVector2();
            this.TertiaryScale = binaryReader.ReadVector2();
            this.PrimaryOffset = binaryReader.ReadVector2();
            this.SecondaryOffset = binaryReader.ReadVector2();
            this.TertiaryOffset = binaryReader.ReadVector2();
            this.Primary = binaryReader.ReadTagReference();
            this.Secondary = binaryReader.ReadTagReference();
            this.Tertiary = binaryReader.ReadTagReference();
            this.PrimaryWrapMode = ((PrimaryWrapModeEnum)(binaryReader.ReadInt16()));
            this.SecondaryWrapMode = ((SecondaryWrapModeEnum)(binaryReader.ReadInt16()));
            this.TertiaryWrapMode = ((TertiaryWrapModeEnum)(binaryReader.ReadInt16()));
            this.fieldpad3 = binaryReader.ReadBytes(2);
            this.fieldpad4 = binaryReader.ReadBytes(184);
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(220));
            this.fieldpad5 = binaryReader.ReadBytes(128);
            return pointerQueue;
        }
        public override void ReadInstances(System.IO.BinaryReader binaryReader, System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue)
        {
            base.ReadInstances(binaryReader, pointerQueue);
            this.Effectors = base.ReadBlockArrayData<GlobalHudMultitextureOverlayEffectorDefinition>(binaryReader, pointerQueue.Dequeue());
        }
        public override void QueueWrites(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.QueueWrites(queueableBinaryWriter);
            queueableBinaryWriter.QueueWrite(this.Effectors);
        }
        public override void Write_(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.Write_(queueableBinaryWriter);
            queueableBinaryWriter.Write(this.fieldpad);
            queueableBinaryWriter.Write(this.Type);
            queueableBinaryWriter.Write(((short)(this.FramebufferBlendFunc)));
            queueableBinaryWriter.Write(this.fieldpad0);
            queueableBinaryWriter.Write(this.fieldpad1);
            queueableBinaryWriter.Write(((short)(this.PrimaryAnchor)));
            queueableBinaryWriter.Write(((short)(this.SecondaryAnchor)));
            queueableBinaryWriter.Write(((short)(this.TertiaryAnchor)));
            queueableBinaryWriter.Write(((short)(this._0To1BlendFunc)));
            queueableBinaryWriter.Write(((short)(this._1To2BlendFunc)));
            queueableBinaryWriter.Write(this.fieldpad2);
            queueableBinaryWriter.Write(this.PrimaryScale);
            queueableBinaryWriter.Write(this.SecondaryScale);
            queueableBinaryWriter.Write(this.TertiaryScale);
            queueableBinaryWriter.Write(this.PrimaryOffset);
            queueableBinaryWriter.Write(this.SecondaryOffset);
            queueableBinaryWriter.Write(this.TertiaryOffset);
            queueableBinaryWriter.Write(this.Primary);
            queueableBinaryWriter.Write(this.Secondary);
            queueableBinaryWriter.Write(this.Tertiary);
            queueableBinaryWriter.Write(((short)(this.PrimaryWrapMode)));
            queueableBinaryWriter.Write(((short)(this.SecondaryWrapMode)));
            queueableBinaryWriter.Write(((short)(this.TertiaryWrapMode)));
            queueableBinaryWriter.Write(this.fieldpad3);
            queueableBinaryWriter.Write(this.fieldpad4);
            queueableBinaryWriter.WritePointer(this.Effectors);
            queueableBinaryWriter.Write(this.fieldpad5);
        }
        public enum FramebufferBlendFuncEnum : short
        {
            AlphaBlend = 0,
            Multiply = 1,
            DoubleMultiply = 2,
            Add = 3,
            Subtract = 4,
            ComponentMin = 5,
            ComponentMax = 6,
            AlphamultiplyAdd = 7,
            ConstantColorBlend = 8,
            InverseConstantColorBlend = 9,
            None = 10,
        }
        /// <summary>
        /// where you want the origin of the texture.
        /// </summary>
        public enum PrimaryAnchorEnum : short
        {
            /// <summary>
            /// "texture" uses the texture coordinates supplied
            /// 
            /// </summary>
            Texture = 0,
            /// <summary>
            /// "screen" uses the origin of the screen as the origin of the texture
            /// </summary>
            Screen = 1,
        }
        public enum SecondaryAnchorEnum : short
        {
            Texture = 0,
            Screen = 1,
        }
        public enum TertiaryAnchorEnum : short
        {
            Texture = 0,
            Screen = 1,
        }
        /// <summary>
        /// how to blend the textures together
        /// </summary>
        public enum _0To1BlendFuncEnum : short
        {
            Add = 0,
            Subtract = 1,
            Multiply = 2,
            Multiply2x = 3,
            Dot = 4,
        }
        public enum _1To2BlendFuncEnum : short
        {
            Add = 0,
            Subtract = 1,
            Multiply = 2,
            Multiply2x = 3,
            Dot = 4,
        }
        public enum PrimaryWrapModeEnum : short
        {
            Clamp = 0,
            Wrap = 1,
        }
        public enum SecondaryWrapModeEnum : short
        {
            Clamp = 0,
            Wrap = 1,
        }
        public enum TertiaryWrapModeEnum : short
        {
            Clamp = 0,
            Wrap = 1,
        }
    }
}