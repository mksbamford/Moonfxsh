// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class SkyCubemapBlock : SkyCubemapBlockBase
    {
        public  SkyCubemapBlock(System.IO.BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 12)]
    public class SkyCubemapBlockBase
    {
        [TagReference("bitm")]
        internal Moonfish.Tags.TagReference cubeMapReference;
        /// <summary>
        /// 0 Defaults to 1.
        /// </summary>
        internal float powerScale;
        internal  SkyCubemapBlockBase(System.IO.BinaryReader binaryReader)
        {
            cubeMapReference = binaryReader.ReadTagReference();
            powerScale = binaryReader.ReadSingle();
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
                binaryWriter.Write(cubeMapReference);
                binaryWriter.Write(powerScale);
            }
        }
    };
}
