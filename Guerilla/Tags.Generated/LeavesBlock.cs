// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class LeavesBlock : LeavesBlockBase
    {
        public  LeavesBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  LeavesBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 4, Alignment = 4)]
    public class LeavesBlockBase : GuerillaBlock
    {
        internal Flags flags;
        internal byte bSP2DReferenceCount;
        internal short firstBSP2DReference;
        
        public override int SerializedSize{get { return 4; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  LeavesBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            flags = (Flags)binaryReader.ReadByte();
            bSP2DReferenceCount = binaryReader.ReadByte();
            firstBSP2DReference = binaryReader.ReadInt16();
        }
        public  LeavesBlockBase(): base()
        {
            
        }
        public override void Read(BinaryReader binaryReader)
        {
            flags = (Flags)binaryReader.ReadByte();
            bSP2DReferenceCount = binaryReader.ReadByte();
            firstBSP2DReference = binaryReader.ReadInt16();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Byte)flags);
                binaryWriter.Write(bSP2DReferenceCount);
                binaryWriter.Write(firstBSP2DReference);
                return nextAddress;
            }
        }
        [FlagsAttribute]
        internal enum Flags : byte
        {
            ContainsDoubleSidedSurfaces = 1,
        };
    };
}
