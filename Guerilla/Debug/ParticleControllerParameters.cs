// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class ParticleControllerParameters : ParticleControllerParametersBase
    {
        public  ParticleControllerParameters(System.IO.BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 20)]
    public class ParticleControllerParametersBase
    {
        internal int parameterId;
        internal ParticlePropertyScalarStructNewBlock property;
        internal  ParticleControllerParametersBase(System.IO.BinaryReader binaryReader)
        {
            parameterId = binaryReader.ReadInt32();
            property = new ParticlePropertyScalarStructNewBlock(binaryReader);
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
                binaryWriter.Write(parameterId);
                property.Write(binaryWriter);
            }
        }
    };
}
