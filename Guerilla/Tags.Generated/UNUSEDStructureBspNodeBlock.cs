// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class UNUSEDStructureBspNodeBlock : UNUSEDStructureBspNodeBlockBase
    {
        public  UNUSEDStructureBspNodeBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  UNUSEDStructureBspNodeBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 6, Alignment = 4)]
    public class UNUSEDStructureBspNodeBlockBase : GuerillaBlock
    {
        internal byte[] invalidName_;
        
        public override int SerializedSize{get { return 6; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  UNUSEDStructureBspNodeBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            invalidName_ = binaryReader.ReadBytes(6);
        }
        public  UNUSEDStructureBspNodeBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            invalidName_ = binaryReader.ReadBytes(6);
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(invalidName_, 0, 6);
                return nextAddress;
            }
        }
    };
}
