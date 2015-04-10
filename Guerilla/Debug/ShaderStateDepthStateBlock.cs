// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class ShaderStateDepthStateBlock : ShaderStateDepthStateBlockBase
    {
        public  ShaderStateDepthStateBlock(System.IO.BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 16)]
    public class ShaderStateDepthStateBlockBase
    {
        internal Mode mode;
        internal DepthCompareFunction depthCompareFunction;
        internal Flags flags;
        internal byte[] invalidName_;
        internal float depthBiasSlopeScale;
        internal float depthBias;
        internal  ShaderStateDepthStateBlockBase(System.IO.BinaryReader binaryReader)
        {
            mode = (Mode)binaryReader.ReadInt16();
            depthCompareFunction = (DepthCompareFunction)binaryReader.ReadInt16();
            flags = (Flags)binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            depthBiasSlopeScale = binaryReader.ReadSingle();
            depthBias = binaryReader.ReadSingle();
        }
        internal  virtual byte[] ReadData(System.IO.BinaryReader binaryReader)
        {
            var blamPointer = binaryReader.ReadBlamPointer(1);
            var data = new byte[blamPointer.Count];
            if(blamPointer.Count > 0)
            {
                using (binaryReader.BaseStream.Pin())
                {
                    binaryReader.BaseStream.Position = blamPointer[0];
                    data = binaryReader.ReadBytes(blamPointer.Count);
                }
            }
            return data;
        }
        internal  virtual void WriteData(System.IO.BinaryWriter binaryWriter)
        {
            
        }
        public void Write(System.IO.BinaryWriter binaryWriter)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int16)mode);
                binaryWriter.Write((Int16)depthCompareFunction);
                binaryWriter.Write((Int16)flags);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write(depthBiasSlopeScale);
                binaryWriter.Write(depthBias);
            }
        }
        internal enum Mode : short
        
        {
            UseZ = 0,
            UseW = 1,
        };
        internal enum DepthCompareFunction : short
        
        {
            Never = 0,
            Less = 1,
            Equal = 2,
            LessOrEqual = 3,
            Greater = 4,
            NotEqual = 5,
            GreaterOrEqual = 6,
            Always = 7,
        };
        [FlagsAttribute]
        internal enum Flags : short
        
        {
            DepthWrite = 1,
            OffsetPoints = 2,
            OffsetLines = 4,
            OffsetTriangles = 8,
            ClipControlDontCullPrimitive = 16,
            ClipControlClamp = 32,
            ClipControlIgnoreWSign = 64,
        };
    };
}
