// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ScenarioVehiclePaletteBlock : ScenarioVehiclePaletteBlockBase
    {
        public  ScenarioVehiclePaletteBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  ScenarioVehiclePaletteBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 40, Alignment = 4)]
    public class ScenarioVehiclePaletteBlockBase : GuerillaBlock
    {
        [TagReference("vehi")]
        internal Moonfish.Tags.TagReference name;
        internal byte[] invalidName_;
        
        public override int SerializedSize{get { return 40; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  ScenarioVehiclePaletteBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            name = binaryReader.ReadTagReference();
            invalidName_ = binaryReader.ReadBytes(32);
        }
        public  ScenarioVehiclePaletteBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            name = binaryReader.ReadTagReference();
            invalidName_ = binaryReader.ReadBytes(32);
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(name);
                binaryWriter.Write(invalidName_, 0, 32);
                return nextAddress;
            }
        }
    };
}
