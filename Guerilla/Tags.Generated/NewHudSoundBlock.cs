// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class NewHudSoundBlock : NewHudSoundBlockBase
    {
        public  NewHudSoundBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  NewHudSoundBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 24, Alignment = 4)]
    public class NewHudSoundBlockBase : GuerillaBlock
    {
        [TagReference("null")]
        internal Moonfish.Tags.TagReference chiefSound;
        internal LatchedTo latchedTo;
        internal float scale;
        [TagReference("null")]
        internal Moonfish.Tags.TagReference dervishSound;
        
        public override int SerializedSize{get { return 24; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  NewHudSoundBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            chiefSound = binaryReader.ReadTagReference();
            latchedTo = (LatchedTo)binaryReader.ReadInt32();
            scale = binaryReader.ReadSingle();
            dervishSound = binaryReader.ReadTagReference();
        }
        public  NewHudSoundBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            chiefSound = binaryReader.ReadTagReference();
            latchedTo = (LatchedTo)binaryReader.ReadInt32();
            scale = binaryReader.ReadSingle();
            dervishSound = binaryReader.ReadTagReference();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(chiefSound);
                binaryWriter.Write((Int32)latchedTo);
                binaryWriter.Write(scale);
                binaryWriter.Write(dervishSound);
                return nextAddress;
            }
        }
        [FlagsAttribute]
        internal enum LatchedTo : int
        {
            ShieldRecharging = 1,
            ShieldDamaged = 2,
            ShieldLow = 4,
            ShieldEmpty = 8,
            HealthLow = 16,
            HealthEmpty = 32,
            HealthMinorDamage = 64,
            HealthMajorDamage = 128,
            RocketLocking = 256,
            RocketLocked = 512,
        };
    };
}
