// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class GrenadeAndPowerupStructBlock : GrenadeAndPowerupStructBlockBase
    {
        public  GrenadeAndPowerupStructBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  GrenadeAndPowerupStructBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 16, Alignment = 4)]
    public class GrenadeAndPowerupStructBlockBase : GuerillaBlock
    {
        internal GrenadeBlock[] grenades;
        internal PowerupBlock[] powerups;
        
        public override int SerializedSize{get { return 16; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  GrenadeAndPowerupStructBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            grenades = Guerilla.ReadBlockArray<GrenadeBlock>(binaryReader);
            powerups = Guerilla.ReadBlockArray<PowerupBlock>(binaryReader);
        }
        public  GrenadeAndPowerupStructBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            grenades = Guerilla.ReadBlockArray<GrenadeBlock>(binaryReader);
            powerups = Guerilla.ReadBlockArray<PowerupBlock>(binaryReader);
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                nextAddress = Guerilla.WriteBlockArray<GrenadeBlock>(binaryWriter, grenades, nextAddress);
                nextAddress = Guerilla.WriteBlockArray<PowerupBlock>(binaryWriter, powerups, nextAddress);
                return nextAddress;
            }
        }
    };
}
