// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class DialogueVariantBlock : DialogueVariantBlockBase
    {
        public  DialogueVariantBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  DialogueVariantBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 12, Alignment = 4)]
    public class DialogueVariantBlockBase : GuerillaBlock
    {
        /// <summary>
        /// variantNumber to use this dialogue with (must match the suffix in the permutations on the unit's model)
        /// </summary>
        internal short variantNumber;
        internal byte[] invalidName_;
        [TagReference("udlg")]
        internal Moonfish.Tags.TagReference dialogue;
        
        public override int SerializedSize{get { return 12; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  DialogueVariantBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            variantNumber = binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            dialogue = binaryReader.ReadTagReference();
        }
        public  DialogueVariantBlockBase(): base()
        {
            
        }
        public override void Read(BinaryReader binaryReader)
        {
            variantNumber = binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            dialogue = binaryReader.ReadTagReference();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(variantNumber);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write(dialogue);
                return nextAddress;
            }
        }
    };
}
