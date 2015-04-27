// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class InvoluntaryDataBlock : InvoluntaryDataBlockBase
    {
        public  InvoluntaryDataBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  InvoluntaryDataBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 4, Alignment = 4)]
    public class InvoluntaryDataBlockBase : GuerillaBlock
    {
        internal short involuntaryVocalizationIndex;
        internal byte[] invalidName_;
        
        public override int SerializedSize{get { return 4; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  InvoluntaryDataBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            involuntaryVocalizationIndex = binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
        }
        public  InvoluntaryDataBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            involuntaryVocalizationIndex = binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(involuntaryVocalizationIndex);
                binaryWriter.Write(invalidName_, 0, 2);
                return nextAddress;
            }
        }
    };
}
