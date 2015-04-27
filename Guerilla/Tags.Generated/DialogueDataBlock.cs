// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class DialogueDataBlock : DialogueDataBlockBase
    {
        public  DialogueDataBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  DialogueDataBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 4, Alignment = 4)]
    public class DialogueDataBlockBase : GuerillaBlock
    {
        internal short startIndexPostprocess;
        internal short lengthPostprocess;
        
        public override int SerializedSize{get { return 4; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  DialogueDataBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            startIndexPostprocess = binaryReader.ReadInt16();
            lengthPostprocess = binaryReader.ReadInt16();
        }
        public  DialogueDataBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(startIndexPostprocess);
                binaryWriter.Write(lengthPostprocess);
                return nextAddress;
            }
        }
    };
}
