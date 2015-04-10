// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class ScenarioNetpointsBlock : ScenarioNetpointsBlockBase
    {
        public  ScenarioNetpointsBlock(System.IO.BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 32)]
    public class ScenarioNetpointsBlockBase
    {
        internal OpenTK.Vector3 position;
        internal float facingDegrees;
        internal Type type;
        internal TeamDesignator teamDesignator;
        internal short identifier;
        internal Flags flags;
        internal Moonfish.Tags.StringID eMPTYSTRING;
        internal Moonfish.Tags.StringID eMPTYSTRING0;
        internal  ScenarioNetpointsBlockBase(System.IO.BinaryReader binaryReader)
        {
            position = binaryReader.ReadVector3();
            facingDegrees = binaryReader.ReadSingle();
            type = (Type)binaryReader.ReadInt16();
            teamDesignator = (TeamDesignator)binaryReader.ReadInt16();
            identifier = binaryReader.ReadInt16();
            flags = (Flags)binaryReader.ReadInt16();
            eMPTYSTRING = binaryReader.ReadStringID();
            eMPTYSTRING0 = binaryReader.ReadStringID();
        }
        internal  virtual byte[] ReadData(System.IO.BinaryReader binaryReader)
        {
            var blamPointer = binaryReader.ReadBlamPointer(1);
            var data = new byte[blamPointer.Count];
            if(blamPointer.Count > 0)
            {
                using (binaryReader.BaseStream.Pin())
                {
                    binaryReader.BaseStream.Position = blamPointer[0];
                    data = binaryReader.ReadBytes(blamPointer.Count);
                }
            }
            return data;
        }
        internal  virtual void WriteData(System.IO.BinaryWriter binaryWriter)
        {
            
        }
        public void Write(System.IO.BinaryWriter binaryWriter)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(position);
                binaryWriter.Write(facingDegrees);
                binaryWriter.Write((Int16)type);
                binaryWriter.Write((Int16)teamDesignator);
                binaryWriter.Write(identifier);
                binaryWriter.Write((Int16)flags);
                binaryWriter.Write(eMPTYSTRING);
                binaryWriter.Write(eMPTYSTRING0);
            }
        }
        internal enum Type : short
        
        {
            CTFFlagSpawn = 0,
            CTFFlagReturn = 1,
            AssaultBombSpawn = 2,
            AssaultBombReturn = 3,
            OddballSpawn = 4,
            Unused = 5,
            RaceCheckpoint = 6,
            TeleporterSrc = 7,
            TeleporterDest = 8,
            HeadhunterBin = 9,
            TerritoriesFlag = 10,
            KingHill0 = 11,
            KingHill1 = 12,
            KingHill2 = 13,
            KingHill3 = 14,
            KingHill4 = 15,
            KingHill5 = 16,
            KingHill6 = 17,
            KingHill7 = 18,
        };
        internal enum TeamDesignator : short
        
        {
            RedAlpha = 0,
            BlueBravo = 1,
            YellowCharlie = 2,
            GreenDelta = 3,
            PurpleEcho = 4,
            OrangeFoxtrot = 5,
            BrownGolf = 6,
            PinkHotel = 7,
            NEUTRAL = 8,
        };
        [FlagsAttribute]
        internal enum Flags : short
        
        {
            MultipleFlagBomb = 1,
            SingleFlagBomb = 2,
            NeutralFlagBomb = 4,
        };
    };
}
