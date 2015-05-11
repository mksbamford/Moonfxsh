// ReSharper disable All

using Moonfish.Model;

using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Moonfish.Guerilla.Tags
{
    public partial class RasterizerScreenEffectConvolutionBlock : RasterizerScreenEffectConvolutionBlockBase
    {
        public RasterizerScreenEffectConvolutionBlock() : base()
        {
        }
    };

    [LayoutAttribute(Size = 92, Alignment = 4)]
    public class RasterizerScreenEffectConvolutionBlockBase : GuerillaBlock
    {
        internal Flags flags;
        internal byte[] invalidName_;
        internal byte[] invalidName_0;
        internal float convolutionAmount0Inf;
        internal float filterScale;
        internal float filterBoxFactor01NotUsedForZoom;
        internal float zoomFalloffRadius;
        internal float zoomCutoffRadius;
        internal float resolutionScale01;

        public override int SerializedSize
        {
            get { return 92; }
        }

        public override int Alignment
        {
            get { return 4; }
        }

        public RasterizerScreenEffectConvolutionBlockBase() : base()
        {
        }

        public override Queue<BlamPointer> ReadFields(BinaryReader binaryReader)
        {
            var blamPointers = new Queue<BlamPointer>(base.ReadFields(binaryReader));
            flags = (Flags) binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            invalidName_0 = binaryReader.ReadBytes(64);
            convolutionAmount0Inf = binaryReader.ReadSingle();
            filterScale = binaryReader.ReadSingle();
            filterBoxFactor01NotUsedForZoom = binaryReader.ReadSingle();
            zoomFalloffRadius = binaryReader.ReadSingle();
            zoomCutoffRadius = binaryReader.ReadSingle();
            resolutionScale01 = binaryReader.ReadSingle();
            return blamPointers;
        }

        public override void ReadPointers(BinaryReader binaryReader, Queue<BlamPointer> blamPointers)
        {
            base.ReadPointers(binaryReader, blamPointers);
        }

        public override int Write(BinaryWriter binaryWriter, int nextAddress)
        {
            base.Write(binaryWriter, nextAddress);
            using (binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int16) flags);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write(invalidName_0, 0, 64);
                binaryWriter.Write(convolutionAmount0Inf);
                binaryWriter.Write(filterScale);
                binaryWriter.Write(filterBoxFactor01NotUsedForZoom);
                binaryWriter.Write(zoomFalloffRadius);
                binaryWriter.Write(zoomCutoffRadius);
                binaryWriter.Write(resolutionScale01);
                return nextAddress;
            }
        }

        [FlagsAttribute]
        internal enum Flags : short
        {
            OnlyWhenPrimaryIsActive = 1,
            OnlyWhenSecondaryIsActive = 2,
            PredatorZoom = 4,
        };
    };
}