// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class PathfindingHintsBlock : PathfindingHintsBlockBase
    {
        public  PathfindingHintsBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  PathfindingHintsBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 20, Alignment = 4)]
    public class PathfindingHintsBlockBase : GuerillaBlock
    {
        internal HintType hintType;
        internal short nextHintIndex;
        internal short hintData0;
        internal short hintData1;
        internal short hintData2;
        internal short hintData3;
        internal short hintData4;
        internal short hintData5;
        internal short hintData6;
        internal short hintData7;
        
        public override int SerializedSize{get { return 20; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  PathfindingHintsBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            hintType = (HintType)binaryReader.ReadInt16();
            nextHintIndex = binaryReader.ReadInt16();
            hintData0 = binaryReader.ReadInt16();
            hintData1 = binaryReader.ReadInt16();
            hintData2 = binaryReader.ReadInt16();
            hintData3 = binaryReader.ReadInt16();
            hintData4 = binaryReader.ReadInt16();
            hintData5 = binaryReader.ReadInt16();
            hintData6 = binaryReader.ReadInt16();
            hintData7 = binaryReader.ReadInt16();
        }
        public  PathfindingHintsBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            hintType = (HintType)binaryReader.ReadInt16();
            nextHintIndex = binaryReader.ReadInt16();
            hintData0 = binaryReader.ReadInt16();
            hintData1 = binaryReader.ReadInt16();
            hintData2 = binaryReader.ReadInt16();
            hintData3 = binaryReader.ReadInt16();
            hintData4 = binaryReader.ReadInt16();
            hintData5 = binaryReader.ReadInt16();
            hintData6 = binaryReader.ReadInt16();
            hintData7 = binaryReader.ReadInt16();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int16)hintType);
                binaryWriter.Write(nextHintIndex);
                binaryWriter.Write(hintData0);
                binaryWriter.Write(hintData1);
                binaryWriter.Write(hintData2);
                binaryWriter.Write(hintData3);
                binaryWriter.Write(hintData4);
                binaryWriter.Write(hintData5);
                binaryWriter.Write(hintData6);
                binaryWriter.Write(hintData7);
                return nextAddress;
            }
        }
        internal enum HintType : short
        {
            IntersectionLink = 0,
            JumpLink = 1,
            ClimbLink = 2,
            VaultLink = 3,
            MountLink = 4,
            HoistLink = 5,
            WallJumpLink = 6,
            BreakableFloor = 7,
        };
    };
}
