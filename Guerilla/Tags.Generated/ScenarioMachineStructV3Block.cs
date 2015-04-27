// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ScenarioMachineStructV3Block : ScenarioMachineStructV3BlockBase
    {
        public  ScenarioMachineStructV3Block(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  ScenarioMachineStructV3Block(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 12, Alignment = 4)]
    public class ScenarioMachineStructV3BlockBase : GuerillaBlock
    {
        internal Flags flags;
        internal PathfindingObjectIndexListBlock[] pathfindingReferences;
        
        public override int SerializedSize{get { return 12; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  ScenarioMachineStructV3BlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            flags = (Flags)binaryReader.ReadInt32();
            pathfindingReferences = Guerilla.ReadBlockArray<PathfindingObjectIndexListBlock>(binaryReader);
        }
        public  ScenarioMachineStructV3BlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int32)flags);
                nextAddress = Guerilla.WriteBlockArray<PathfindingObjectIndexListBlock>(binaryWriter, pathfindingReferences, nextAddress);
                return nextAddress;
            }
        }
        [FlagsAttribute]
        internal enum Flags : int
        {
            DoesNotOperateAutomatically = 1,
            OneSided = 2,
            NeverAppearsLocked = 4,
            OpenedByMeleeAttack = 8,
            OneSidedForPlayer = 16,
            DoesNotCloseAutomatically = 32,
        };
    };
}
