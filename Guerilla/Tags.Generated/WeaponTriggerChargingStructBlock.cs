// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class WeaponTriggerChargingStructBlock : WeaponTriggerChargingStructBlockBase
    {
        public  WeaponTriggerChargingStructBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  WeaponTriggerChargingStructBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 36, Alignment = 4)]
    public class WeaponTriggerChargingStructBlockBase : GuerillaBlock
    {
        /// <summary>
        /// the amount of time it takes for this trigger to become fully charged
        /// </summary>
        internal float chargingTimeSeconds;
        /// <summary>
        /// the amount of time this trigger can be charged before becoming overcharged
        /// </summary>
        internal float chargedTimeSeconds;
        internal OverchargedAction overchargedAction;
        internal byte[] invalidName_;
        /// <summary>
        /// the amount of illumination given off when the weapon is fully charged
        /// </summary>
        internal float chargedIllumination01;
        /// <summary>
        /// length of time the weapon will spew (fire continuously) while discharging
        /// </summary>
        internal float spewTimeSeconds;
        /// <summary>
        /// the chargingEffect is created once when the trigger begins to charge
        /// </summary>
        [TagReference("null")]
        internal Moonfish.Tags.TagReference chargingEffect;
        /// <summary>
        /// the charging effect is created once when the trigger begins to charge
        /// </summary>
        [TagReference("jpt!")]
        internal Moonfish.Tags.TagReference chargingDamageEffect;
        
        public override int SerializedSize{get { return 36; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  WeaponTriggerChargingStructBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            chargingTimeSeconds = binaryReader.ReadSingle();
            chargedTimeSeconds = binaryReader.ReadSingle();
            overchargedAction = (OverchargedAction)binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            chargedIllumination01 = binaryReader.ReadSingle();
            spewTimeSeconds = binaryReader.ReadSingle();
            chargingEffect = binaryReader.ReadTagReference();
            chargingDamageEffect = binaryReader.ReadTagReference();
        }
        public  WeaponTriggerChargingStructBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            chargingTimeSeconds = binaryReader.ReadSingle();
            chargedTimeSeconds = binaryReader.ReadSingle();
            overchargedAction = (OverchargedAction)binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            chargedIllumination01 = binaryReader.ReadSingle();
            spewTimeSeconds = binaryReader.ReadSingle();
            chargingEffect = binaryReader.ReadTagReference();
            chargingDamageEffect = binaryReader.ReadTagReference();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(chargingTimeSeconds);
                binaryWriter.Write(chargedTimeSeconds);
                binaryWriter.Write((Int16)overchargedAction);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write(chargedIllumination01);
                binaryWriter.Write(spewTimeSeconds);
                binaryWriter.Write(chargingEffect);
                binaryWriter.Write(chargingDamageEffect);
                return nextAddress;
            }
        }
        internal enum OverchargedAction : short
        {
            None = 0,
            Explode = 1,
            Discharge = 2,
        };
    };
}
