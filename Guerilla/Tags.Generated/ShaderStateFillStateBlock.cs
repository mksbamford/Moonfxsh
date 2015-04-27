// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ShaderStateFillStateBlock : ShaderStateFillStateBlockBase
    {
        public  ShaderStateFillStateBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  ShaderStateFillStateBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 12, Alignment = 4)]
    public class ShaderStateFillStateBlockBase : GuerillaBlock
    {
        internal Flags flags;
        internal FillMode fillMode;
        internal BackFillMode backFillMode;
        internal byte[] invalidName_;
        internal float lineWidth;
        
        public override int SerializedSize{get { return 12; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  ShaderStateFillStateBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            flags = (Flags)binaryReader.ReadInt16();
            fillMode = (FillMode)binaryReader.ReadInt16();
            backFillMode = (BackFillMode)binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            lineWidth = binaryReader.ReadSingle();
        }
        public  ShaderStateFillStateBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int16)flags);
                binaryWriter.Write((Int16)fillMode);
                binaryWriter.Write((Int16)backFillMode);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write(lineWidth);
                return nextAddress;
            }
        }
        [FlagsAttribute]
        internal enum Flags : short
        {
            FlatShading = 1,
            EdgeAntialiasing = 2,
        };
        internal enum FillMode : short
        {
            Solid = 0,
            Wireframe = 1,
            Points = 2,
        };
        internal enum BackFillMode : short
        {
            Solid = 0,
            Wireframe = 1,
            Points = 2,
        };
    };
}
