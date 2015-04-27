// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class TableViewListRowReferenceBlock : TableViewListRowReferenceBlockBase
    {
        public  TableViewListRowReferenceBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  TableViewListRowReferenceBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 16, Alignment = 4)]
    public class TableViewListRowReferenceBlockBase : GuerillaBlock
    {
        internal Flags flags;
        internal short rowHeight;
        internal byte[] invalidName_;
        internal TableViewListItemReferenceBlock[] rowCells;
        
        public override int SerializedSize{get { return 16; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  TableViewListRowReferenceBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            flags = (Flags)binaryReader.ReadInt32();
            rowHeight = binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            rowCells = Guerilla.ReadBlockArray<TableViewListItemReferenceBlock>(binaryReader);
        }
        public  TableViewListRowReferenceBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int32)flags);
                binaryWriter.Write(rowHeight);
                binaryWriter.Write(invalidName_, 0, 2);
                nextAddress = Guerilla.WriteBlockArray<TableViewListItemReferenceBlock>(binaryWriter, rowCells, nextAddress);
                return nextAddress;
            }
        }
        [FlagsAttribute]
        internal enum Flags : int
        {
            Unused = 1,
        };
    };
}
