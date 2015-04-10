// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class WaterGeometrySectionBlock : WaterGeometrySectionBlockBase
    {
        public  WaterGeometrySectionBlock(System.IO.BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 68)]
    public class WaterGeometrySectionBlockBase
    {
        internal GlobalGeometrySectionStructBlock section;
        internal  WaterGeometrySectionBlockBase(System.IO.BinaryReader binaryReader)
        {
            section = new GlobalGeometrySectionStructBlock(binaryReader);
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
                section.Write(binaryWriter);
            }
        }
    };
}
