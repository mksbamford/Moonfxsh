using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class ScreenEffectBonusStructBlock : ScreenEffectBonusStructBlockBase
    {
        public  ScreenEffectBonusStructBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 16)]
    public class ScreenEffectBonusStructBlockBase
    {
        [TagReference("egor")]
        internal Moonfish.Tags.TagReference halfscreenScreenEffect;
        [TagReference("egor")]
        internal Moonfish.Tags.TagReference quarterscreenScreenEffect;
        internal  ScreenEffectBonusStructBlockBase(BinaryReader binaryReader)
        {
            this.halfscreenScreenEffect = binaryReader.ReadTagReference();
            this.quarterscreenScreenEffect = binaryReader.ReadTagReference();
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
