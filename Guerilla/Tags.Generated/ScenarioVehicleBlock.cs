// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ScenarioVehicleBlock : ScenarioVehicleBlockBase
    {
        public  ScenarioVehicleBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  ScenarioVehicleBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 84, Alignment = 4)]
    public class ScenarioVehicleBlockBase : GuerillaBlock
    {
        internal Moonfish.Tags.ShortBlockIndex1 type;
        internal Moonfish.Tags.ShortBlockIndex1 name;
        internal ScenarioObjectDatumStructBlock objectData;
        internal byte[] indexer;
        internal ScenarioObjectPermutationStructBlock permutationData;
        internal ScenarioUnitStructBlock unitData;
        
        public override int SerializedSize{get { return 84; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  ScenarioVehicleBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            type = binaryReader.ReadShortBlockIndex1();
            name = binaryReader.ReadShortBlockIndex1();
            objectData = new ScenarioObjectDatumStructBlock(binaryReader);
            indexer = binaryReader.ReadBytes(4);
            permutationData = new ScenarioObjectPermutationStructBlock(binaryReader);
            unitData = new ScenarioUnitStructBlock(binaryReader);
        }
        public  ScenarioVehicleBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(type);
                binaryWriter.Write(name);
                objectData.Write(binaryWriter);
                binaryWriter.Write(indexer, 0, 4);
                permutationData.Write(binaryWriter);
                unitData.Write(binaryWriter);
                return nextAddress;
            }
        }
    };
}
