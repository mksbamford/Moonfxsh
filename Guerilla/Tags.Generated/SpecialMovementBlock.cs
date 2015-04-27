// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class SpecialMovementBlock : SpecialMovementBlockBase
    {
        public  SpecialMovementBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  SpecialMovementBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 4, Alignment = 4)]
    public class SpecialMovementBlockBase : GuerillaBlock
    {
        internal SpecialMovement1 specialMovement1;
        
        public override int SerializedSize{get { return 4; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  SpecialMovementBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            specialMovement1 = (SpecialMovement1)binaryReader.ReadInt32();
        }
        public  SpecialMovementBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int32)specialMovement1);
                return nextAddress;
            }
        }
        [FlagsAttribute]
        internal enum SpecialMovement1 : int
        {
            Jump = 1,
            Climb = 2,
            Vault = 4,
            Mount = 8,
            Hoist = 16,
            WallJump = 32,
            NA = 64,
        };
    };
}
