// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class SoundPromotionRuntimeTimerBlock : SoundPromotionRuntimeTimerBlockBase
    {
        public  SoundPromotionRuntimeTimerBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  SoundPromotionRuntimeTimerBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 4, Alignment = 4)]
    public class SoundPromotionRuntimeTimerBlockBase : GuerillaBlock
    {
        internal int invalidName_;
        
        public override int SerializedSize{get { return 4; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  SoundPromotionRuntimeTimerBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            invalidName_ = binaryReader.ReadInt32();
        }
        public  SoundPromotionRuntimeTimerBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(invalidName_);
                return nextAddress;
            }
        }
    };
}
