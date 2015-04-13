using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class PersistentBackgroundAnimationBlock : PersistentBackgroundAnimationBlockBase
    {
        public  PersistentBackgroundAnimationBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 16)]
    public class PersistentBackgroundAnimationBlockBase
    {
        internal byte[] invalidName_;
        internal int animationPeriodMilliseconds;
        internal BackgroundAnimationKeyframeReferenceBlock[] interpolatedKeyframes;
        internal  PersistentBackgroundAnimationBlockBase(BinaryReader binaryReader)
        {
            this.invalidName_ = binaryReader.ReadBytes(4);
            this.animationPeriodMilliseconds = binaryReader.ReadInt32();
            this.interpolatedKeyframes = ReadBackgroundAnimationKeyframeReferenceBlockArray(binaryReader);
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
        internal  virtual BackgroundAnimationKeyframeReferenceBlock[] ReadBackgroundAnimationKeyframeReferenceBlockArray(BinaryReader binaryReader)
        {
            var elementSize = Deserializer.SizeOf(typeof(BackgroundAnimationKeyframeReferenceBlock));
            var blamPointer = binaryReader.ReadBlamPointer(elementSize);
            var array = new BackgroundAnimationKeyframeReferenceBlock[blamPointer.elementCount];
            using (binaryReader.BaseStream.Pin())
            {
                for (int i = 0; i < blamPointer.elementCount; ++i)
                {
                    binaryReader.BaseStream.Position = blamPointer[i];
                    array[i] = new BackgroundAnimationKeyframeReferenceBlock(binaryReader);
                }
            }
            return array;
        }
    };
}