// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class DynamicSpawnZoneOverloadBlock : DynamicSpawnZoneOverloadBlockBase
    {
        public  DynamicSpawnZoneOverloadBlock(System.IO.BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 16)]
    public class DynamicSpawnZoneOverloadBlockBase
    {
        internal OverloadType overloadType;
        internal byte[] invalidName_;
        internal float innerRadius;
        internal float outerRadius;
        internal float weight;
        internal  DynamicSpawnZoneOverloadBlockBase(System.IO.BinaryReader binaryReader)
        {
            overloadType = (OverloadType)binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            innerRadius = binaryReader.ReadSingle();
            outerRadius = binaryReader.ReadSingle();
            weight = binaryReader.ReadSingle();
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
                binaryWriter.Write((Int16)overloadType);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write(innerRadius);
                binaryWriter.Write(outerRadius);
                binaryWriter.Write(weight);
            }
        }
        internal enum OverloadType : short
        
        {
            Enemy = 0,
            Friend = 1,
            EnemyVehicle = 2,
            FriendlyVehicle = 3,
            EmptyVehicle = 4,
            OddballInclusion = 5,
            OddballExclusion = 6,
            HillInclusion = 7,
            HillExclusion = 8,
            LastRaceFlag = 9,
            DeadAlly = 10,
            ControlledTerritory = 11,
        };
    };
}
