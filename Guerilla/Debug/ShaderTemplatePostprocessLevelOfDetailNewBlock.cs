// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class ShaderTemplatePostprocessLevelOfDetailNewBlock : ShaderTemplatePostprocessLevelOfDetailNewBlockBase
    {
        public  ShaderTemplatePostprocessLevelOfDetailNewBlock(System.IO.BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 10)]
    public class ShaderTemplatePostprocessLevelOfDetailNewBlockBase
    {
        internal TagBlockIndexStructBlock layers;
        internal int availableLayers;
        internal float projectedHeightPercentage;
        internal  ShaderTemplatePostprocessLevelOfDetailNewBlockBase(System.IO.BinaryReader binaryReader)
        {
            layers = new TagBlockIndexStructBlock(binaryReader);
            availableLayers = binaryReader.ReadInt32();
            projectedHeightPercentage = binaryReader.ReadSingle();
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
                layers.Write(binaryWriter);
                binaryWriter.Write(availableLayers);
                binaryWriter.Write(projectedHeightPercentage);
            }
        }
    };
}
