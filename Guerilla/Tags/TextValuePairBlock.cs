using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class TextValuePairBlock : TextValuePairBlockBase
    {
        public  TextValuePairBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 8)]
    public class TextValuePairBlockBase
    {
        [TagReference("sily")]
        internal Moonfish.Tags.TagReference valuePairs;
        internal  TextValuePairBlockBase(BinaryReader binaryReader)
        {
            this.valuePairs = binaryReader.ReadTagReference();
        }
        internal  virtual byte[] ReadData(BinaryReader binaryReader)
        {
            var blamPointer = binaryReader.ReadBlamPointer(1);
            var data = new byte[blamPointer.elementCount];
            if(blamPointer.elementCount > 0)
            {
                using (binaryReader.BaseStream.Pin())
                {
                    binaryReader.BaseStream.Position = blamPointer[0];
                    data = binaryReader.ReadBytes(blamPointer.elementCount);
                }
            }
            return data;
        }
    };
}