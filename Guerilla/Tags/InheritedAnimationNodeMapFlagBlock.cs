using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class InheritedAnimationNodeMapFlagBlock : InheritedAnimationNodeMapFlagBlockBase
    {
        public  InheritedAnimationNodeMapFlagBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 4)]
    public class InheritedAnimationNodeMapFlagBlockBase
    {
        internal int localNodeFlags;
        internal  InheritedAnimationNodeMapFlagBlockBase(BinaryReader binaryReader)
        {
            this.localNodeFlags = binaryReader.ReadInt32();
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
