using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class VibrationFrequencyDefinitionStructBlock : VibrationFrequencyDefinitionStructBlockBase
    {
        public  VibrationFrequencyDefinitionStructBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 12)]
    public class VibrationFrequencyDefinitionStructBlockBase
    {
        internal float durationSeconds;
        internal MappingFunctionBlock dirtyWhore;
        internal  VibrationFrequencyDefinitionStructBlockBase(BinaryReader binaryReader)
        {
            this.durationSeconds = binaryReader.ReadSingle();
            this.dirtyWhore = new MappingFunctionBlock(binaryReader);
        }
        internal  virtual byte[] ReadData(BinaryReader binaryReader)
        {
            var blamPointer = binaryReader.ReadBlamPointer(1);
            var data = new byte[blamPointer.count];
            if(blamPointer.count > 0)
            {
                using (binaryReader.BaseStream.Pin())
                {
                    binaryReader.BaseStream.Position = blamPointer[0];
                    data = binaryReader.ReadBytes(blamPointer.count);
                }
            }
            return data;
        }
    };
}
