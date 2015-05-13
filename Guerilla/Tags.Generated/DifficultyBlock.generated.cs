//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Moonfish.Guerilla.Tags
{
    using Moonfish.Tags;
    using Moonfish.Model;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    
    public partial class DifficultyBlock : GuerillaBlock, IWriteQueueable
    {
        /// <summary>
        /// scale values for enemy health and damage settings
        /// </summary>
        public float EasyEnemyDamage;
        public float NormalEnemyDamage;
        public float HardEnemyDamage;
        public float ImpossEnemyDamage;
        public float EasyEnemyVitality;
        public float NormalEnemyVitality;
        public float HardEnemyVitality;
        public float ImpossEnemyVitality;
        public float EasyEnemyShield;
        public float NormalEnemyShield;
        public float HardEnemyShield;
        public float ImpossEnemyShield;
        public float EasyEnemyRecharge;
        public float NormalEnemyRecharge;
        public float HardEnemyRecharge;
        public float ImpossEnemyRecharge;
        public float EasyFriendDamage;
        public float NormalFriendDamage;
        public float HardFriendDamage;
        public float ImpossFriendDamage;
        public float EasyFriendVitality;
        public float NormalFriendVitality;
        public float HardFriendVitality;
        public float ImpossFriendVitality;
        public float EasyFriendShield;
        public float NormalFriendShield;
        public float HardFriendShield;
        public float ImpossFriendShield;
        public float EasyFriendRecharge;
        public float NormalFriendRecharge;
        public float HardFriendRecharge;
        public float ImpossFriendRecharge;
        public float EasyInfectionForms;
        public float NormalInfectionForms;
        public float HardInfectionForms;
        public float ImpossInfectionForms;
        private byte[] fieldpad = new byte[16];
        /// <summary>
        /// difficulty-affecting values for enemy ranged combat settings
        /// </summary>
        public float EasyRateOfFire;
        public float NormalRateOfFire;
        public float HardRateOfFire;
        public float ImpossRateOfFire;
        public float EasyProjectileError;
        public float NormalProjectileError;
        public float HardProjectileError;
        public float ImpossProjectileError;
        public float EasyBurstError;
        public float NormalBurstError;
        public float HardBurstError;
        public float ImpossBurstError;
        public float EasyNewTargetDelay;
        public float NormalNewTargetDelay;
        public float HardNewTargetDelay;
        public float ImpossNewTargetDelay;
        public float EasyBurstSeparation;
        public float NormalBurstSeparation;
        public float HardBurstSeparation;
        public float ImpossBurstSeparation;
        public float EasyTargetTracking;
        public float NormalTargetTracking;
        public float HardTargetTracking;
        public float ImpossTargetTracking;
        public float EasyTargetLeading;
        public float NormalTargetLeading;
        public float HardTargetLeading;
        public float ImpossTargetLeading;
        public float EasyOverchargeChance;
        public float NormalOverchargeChance;
        public float HardOverchargeChance;
        public float ImpossOverchargeChance;
        public float EasySpecialFireDelay;
        public float NormalSpecialFireDelay;
        public float HardSpecialFireDelay;
        public float ImpossSpecialFireDelay;
        public float EasyGuidanceVsPlayer;
        public float NormalGuidanceVsPlayer;
        public float HardGuidanceVsPlayer;
        public float ImpossGuidanceVsPlayer;
        public float EasyMeleeDelayBase;
        public float NormalMeleeDelayBase;
        public float HardMeleeDelayBase;
        public float ImpossMeleeDelayBase;
        public float EasyMeleeDelayScale;
        public float NormalMeleeDelayScale;
        public float HardMeleeDelayScale;
        public float ImpossMeleeDelayScale;
        private byte[] fieldpad0 = new byte[16];
        /// <summary>
        /// difficulty-affecting values for enemy grenade behavior
        /// </summary>
        public float EasyGrenadeChanceScale;
        public float NormalGrenadeChanceScale;
        public float HardGrenadeChanceScale;
        public float ImpossGrenadeChanceScale;
        public float EasyGrenadeTimerScale;
        public float NormalGrenadeTimerScale;
        public float HardGrenadeTimerScale;
        public float ImpossGrenadeTimerScale;
        private byte[] fieldpad1 = new byte[16];
        private byte[] fieldpad2 = new byte[16];
        private byte[] fieldpad3 = new byte[16];
        /// <summary>
        /// difficulty-affecting values for enemy placement
        /// </summary>
        public float EasyMajorUpgrade;
        public float NormalMajorUpgrade;
        public float HardMajorUpgrade;
        public float ImpossMajorUpgrade;
        public float EasyMajorUpgrade0;
        public float NormalMajorUpgrade0;
        public float HardMajorUpgrade0;
        public float ImpossMajorUpgrade0;
        public float EasyMajorUpgrade1;
        public float NormalMajorUpgrade1;
        public float HardMajorUpgrade1;
        public float ImpossMajorUpgrade1;
        /// <summary>
        /// difficulty-affecting values for vehicle driving/combat
        /// </summary>
        public float EasyPlayerVehicleRamChance;
        public float NormalPlayerVehicleRamChance;
        public float HardPlayerVehicleRamChance;
        public float ImpossPlayerVehicleRamChance;
        private byte[] fieldpad4 = new byte[16];
        private byte[] fieldpad5 = new byte[16];
        private byte[] fieldpad6 = new byte[16];
        private byte[] fieldpad7 = new byte[84];
        public override int SerializedSize
        {
            get
            {
                return 644;
            }
        }
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        public override System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> ReadFields(System.IO.BinaryReader binaryReader)
        {
            System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue = new System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer>(base.ReadFields(binaryReader));
            this.EasyEnemyDamage = binaryReader.ReadSingle();
            this.NormalEnemyDamage = binaryReader.ReadSingle();
            this.HardEnemyDamage = binaryReader.ReadSingle();
            this.ImpossEnemyDamage = binaryReader.ReadSingle();
            this.EasyEnemyVitality = binaryReader.ReadSingle();
            this.NormalEnemyVitality = binaryReader.ReadSingle();
            this.HardEnemyVitality = binaryReader.ReadSingle();
            this.ImpossEnemyVitality = binaryReader.ReadSingle();
            this.EasyEnemyShield = binaryReader.ReadSingle();
            this.NormalEnemyShield = binaryReader.ReadSingle();
            this.HardEnemyShield = binaryReader.ReadSingle();
            this.ImpossEnemyShield = binaryReader.ReadSingle();
            this.EasyEnemyRecharge = binaryReader.ReadSingle();
            this.NormalEnemyRecharge = binaryReader.ReadSingle();
            this.HardEnemyRecharge = binaryReader.ReadSingle();
            this.ImpossEnemyRecharge = binaryReader.ReadSingle();
            this.EasyFriendDamage = binaryReader.ReadSingle();
            this.NormalFriendDamage = binaryReader.ReadSingle();
            this.HardFriendDamage = binaryReader.ReadSingle();
            this.ImpossFriendDamage = binaryReader.ReadSingle();
            this.EasyFriendVitality = binaryReader.ReadSingle();
            this.NormalFriendVitality = binaryReader.ReadSingle();
            this.HardFriendVitality = binaryReader.ReadSingle();
            this.ImpossFriendVitality = binaryReader.ReadSingle();
            this.EasyFriendShield = binaryReader.ReadSingle();
            this.NormalFriendShield = binaryReader.ReadSingle();
            this.HardFriendShield = binaryReader.ReadSingle();
            this.ImpossFriendShield = binaryReader.ReadSingle();
            this.EasyFriendRecharge = binaryReader.ReadSingle();
            this.NormalFriendRecharge = binaryReader.ReadSingle();
            this.HardFriendRecharge = binaryReader.ReadSingle();
            this.ImpossFriendRecharge = binaryReader.ReadSingle();
            this.EasyInfectionForms = binaryReader.ReadSingle();
            this.NormalInfectionForms = binaryReader.ReadSingle();
            this.HardInfectionForms = binaryReader.ReadSingle();
            this.ImpossInfectionForms = binaryReader.ReadSingle();
            this.fieldpad = binaryReader.ReadBytes(16);
            this.EasyRateOfFire = binaryReader.ReadSingle();
            this.NormalRateOfFire = binaryReader.ReadSingle();
            this.HardRateOfFire = binaryReader.ReadSingle();
            this.ImpossRateOfFire = binaryReader.ReadSingle();
            this.EasyProjectileError = binaryReader.ReadSingle();
            this.NormalProjectileError = binaryReader.ReadSingle();
            this.HardProjectileError = binaryReader.ReadSingle();
            this.ImpossProjectileError = binaryReader.ReadSingle();
            this.EasyBurstError = binaryReader.ReadSingle();
            this.NormalBurstError = binaryReader.ReadSingle();
            this.HardBurstError = binaryReader.ReadSingle();
            this.ImpossBurstError = binaryReader.ReadSingle();
            this.EasyNewTargetDelay = binaryReader.ReadSingle();
            this.NormalNewTargetDelay = binaryReader.ReadSingle();
            this.HardNewTargetDelay = binaryReader.ReadSingle();
            this.ImpossNewTargetDelay = binaryReader.ReadSingle();
            this.EasyBurstSeparation = binaryReader.ReadSingle();
            this.NormalBurstSeparation = binaryReader.ReadSingle();
            this.HardBurstSeparation = binaryReader.ReadSingle();
            this.ImpossBurstSeparation = binaryReader.ReadSingle();
            this.EasyTargetTracking = binaryReader.ReadSingle();
            this.NormalTargetTracking = binaryReader.ReadSingle();
            this.HardTargetTracking = binaryReader.ReadSingle();
            this.ImpossTargetTracking = binaryReader.ReadSingle();
            this.EasyTargetLeading = binaryReader.ReadSingle();
            this.NormalTargetLeading = binaryReader.ReadSingle();
            this.HardTargetLeading = binaryReader.ReadSingle();
            this.ImpossTargetLeading = binaryReader.ReadSingle();
            this.EasyOverchargeChance = binaryReader.ReadSingle();
            this.NormalOverchargeChance = binaryReader.ReadSingle();
            this.HardOverchargeChance = binaryReader.ReadSingle();
            this.ImpossOverchargeChance = binaryReader.ReadSingle();
            this.EasySpecialFireDelay = binaryReader.ReadSingle();
            this.NormalSpecialFireDelay = binaryReader.ReadSingle();
            this.HardSpecialFireDelay = binaryReader.ReadSingle();
            this.ImpossSpecialFireDelay = binaryReader.ReadSingle();
            this.EasyGuidanceVsPlayer = binaryReader.ReadSingle();
            this.NormalGuidanceVsPlayer = binaryReader.ReadSingle();
            this.HardGuidanceVsPlayer = binaryReader.ReadSingle();
            this.ImpossGuidanceVsPlayer = binaryReader.ReadSingle();
            this.EasyMeleeDelayBase = binaryReader.ReadSingle();
            this.NormalMeleeDelayBase = binaryReader.ReadSingle();
            this.HardMeleeDelayBase = binaryReader.ReadSingle();
            this.ImpossMeleeDelayBase = binaryReader.ReadSingle();
            this.EasyMeleeDelayScale = binaryReader.ReadSingle();
            this.NormalMeleeDelayScale = binaryReader.ReadSingle();
            this.HardMeleeDelayScale = binaryReader.ReadSingle();
            this.ImpossMeleeDelayScale = binaryReader.ReadSingle();
            this.fieldpad0 = binaryReader.ReadBytes(16);
            this.EasyGrenadeChanceScale = binaryReader.ReadSingle();
            this.NormalGrenadeChanceScale = binaryReader.ReadSingle();
            this.HardGrenadeChanceScale = binaryReader.ReadSingle();
            this.ImpossGrenadeChanceScale = binaryReader.ReadSingle();
            this.EasyGrenadeTimerScale = binaryReader.ReadSingle();
            this.NormalGrenadeTimerScale = binaryReader.ReadSingle();
            this.HardGrenadeTimerScale = binaryReader.ReadSingle();
            this.ImpossGrenadeTimerScale = binaryReader.ReadSingle();
            this.fieldpad1 = binaryReader.ReadBytes(16);
            this.fieldpad2 = binaryReader.ReadBytes(16);
            this.fieldpad3 = binaryReader.ReadBytes(16);
            this.EasyMajorUpgrade = binaryReader.ReadSingle();
            this.NormalMajorUpgrade = binaryReader.ReadSingle();
            this.HardMajorUpgrade = binaryReader.ReadSingle();
            this.ImpossMajorUpgrade = binaryReader.ReadSingle();
            this.EasyMajorUpgrade0 = binaryReader.ReadSingle();
            this.NormalMajorUpgrade0 = binaryReader.ReadSingle();
            this.HardMajorUpgrade0 = binaryReader.ReadSingle();
            this.ImpossMajorUpgrade0 = binaryReader.ReadSingle();
            this.EasyMajorUpgrade1 = binaryReader.ReadSingle();
            this.NormalMajorUpgrade1 = binaryReader.ReadSingle();
            this.HardMajorUpgrade1 = binaryReader.ReadSingle();
            this.ImpossMajorUpgrade1 = binaryReader.ReadSingle();
            this.EasyPlayerVehicleRamChance = binaryReader.ReadSingle();
            this.NormalPlayerVehicleRamChance = binaryReader.ReadSingle();
            this.HardPlayerVehicleRamChance = binaryReader.ReadSingle();
            this.ImpossPlayerVehicleRamChance = binaryReader.ReadSingle();
            this.fieldpad4 = binaryReader.ReadBytes(16);
            this.fieldpad5 = binaryReader.ReadBytes(16);
            this.fieldpad6 = binaryReader.ReadBytes(16);
            this.fieldpad7 = binaryReader.ReadBytes(84);
            return pointerQueue;
        }
        public override void ReadInstances(System.IO.BinaryReader binaryReader, System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue)
        {
            base.ReadInstances(binaryReader, pointerQueue);
        }
        public override void QueueWrites(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.QueueWrites(queueableBinaryWriter);
        }
        public override void Write_(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.Write_(queueableBinaryWriter);
            queueableBinaryWriter.Write(this.EasyEnemyDamage);
            queueableBinaryWriter.Write(this.NormalEnemyDamage);
            queueableBinaryWriter.Write(this.HardEnemyDamage);
            queueableBinaryWriter.Write(this.ImpossEnemyDamage);
            queueableBinaryWriter.Write(this.EasyEnemyVitality);
            queueableBinaryWriter.Write(this.NormalEnemyVitality);
            queueableBinaryWriter.Write(this.HardEnemyVitality);
            queueableBinaryWriter.Write(this.ImpossEnemyVitality);
            queueableBinaryWriter.Write(this.EasyEnemyShield);
            queueableBinaryWriter.Write(this.NormalEnemyShield);
            queueableBinaryWriter.Write(this.HardEnemyShield);
            queueableBinaryWriter.Write(this.ImpossEnemyShield);
            queueableBinaryWriter.Write(this.EasyEnemyRecharge);
            queueableBinaryWriter.Write(this.NormalEnemyRecharge);
            queueableBinaryWriter.Write(this.HardEnemyRecharge);
            queueableBinaryWriter.Write(this.ImpossEnemyRecharge);
            queueableBinaryWriter.Write(this.EasyFriendDamage);
            queueableBinaryWriter.Write(this.NormalFriendDamage);
            queueableBinaryWriter.Write(this.HardFriendDamage);
            queueableBinaryWriter.Write(this.ImpossFriendDamage);
            queueableBinaryWriter.Write(this.EasyFriendVitality);
            queueableBinaryWriter.Write(this.NormalFriendVitality);
            queueableBinaryWriter.Write(this.HardFriendVitality);
            queueableBinaryWriter.Write(this.ImpossFriendVitality);
            queueableBinaryWriter.Write(this.EasyFriendShield);
            queueableBinaryWriter.Write(this.NormalFriendShield);
            queueableBinaryWriter.Write(this.HardFriendShield);
            queueableBinaryWriter.Write(this.ImpossFriendShield);
            queueableBinaryWriter.Write(this.EasyFriendRecharge);
            queueableBinaryWriter.Write(this.NormalFriendRecharge);
            queueableBinaryWriter.Write(this.HardFriendRecharge);
            queueableBinaryWriter.Write(this.ImpossFriendRecharge);
            queueableBinaryWriter.Write(this.EasyInfectionForms);
            queueableBinaryWriter.Write(this.NormalInfectionForms);
            queueableBinaryWriter.Write(this.HardInfectionForms);
            queueableBinaryWriter.Write(this.ImpossInfectionForms);
            queueableBinaryWriter.Write(this.fieldpad);
            queueableBinaryWriter.Write(this.EasyRateOfFire);
            queueableBinaryWriter.Write(this.NormalRateOfFire);
            queueableBinaryWriter.Write(this.HardRateOfFire);
            queueableBinaryWriter.Write(this.ImpossRateOfFire);
            queueableBinaryWriter.Write(this.EasyProjectileError);
            queueableBinaryWriter.Write(this.NormalProjectileError);
            queueableBinaryWriter.Write(this.HardProjectileError);
            queueableBinaryWriter.Write(this.ImpossProjectileError);
            queueableBinaryWriter.Write(this.EasyBurstError);
            queueableBinaryWriter.Write(this.NormalBurstError);
            queueableBinaryWriter.Write(this.HardBurstError);
            queueableBinaryWriter.Write(this.ImpossBurstError);
            queueableBinaryWriter.Write(this.EasyNewTargetDelay);
            queueableBinaryWriter.Write(this.NormalNewTargetDelay);
            queueableBinaryWriter.Write(this.HardNewTargetDelay);
            queueableBinaryWriter.Write(this.ImpossNewTargetDelay);
            queueableBinaryWriter.Write(this.EasyBurstSeparation);
            queueableBinaryWriter.Write(this.NormalBurstSeparation);
            queueableBinaryWriter.Write(this.HardBurstSeparation);
            queueableBinaryWriter.Write(this.ImpossBurstSeparation);
            queueableBinaryWriter.Write(this.EasyTargetTracking);
            queueableBinaryWriter.Write(this.NormalTargetTracking);
            queueableBinaryWriter.Write(this.HardTargetTracking);
            queueableBinaryWriter.Write(this.ImpossTargetTracking);
            queueableBinaryWriter.Write(this.EasyTargetLeading);
            queueableBinaryWriter.Write(this.NormalTargetLeading);
            queueableBinaryWriter.Write(this.HardTargetLeading);
            queueableBinaryWriter.Write(this.ImpossTargetLeading);
            queueableBinaryWriter.Write(this.EasyOverchargeChance);
            queueableBinaryWriter.Write(this.NormalOverchargeChance);
            queueableBinaryWriter.Write(this.HardOverchargeChance);
            queueableBinaryWriter.Write(this.ImpossOverchargeChance);
            queueableBinaryWriter.Write(this.EasySpecialFireDelay);
            queueableBinaryWriter.Write(this.NormalSpecialFireDelay);
            queueableBinaryWriter.Write(this.HardSpecialFireDelay);
            queueableBinaryWriter.Write(this.ImpossSpecialFireDelay);
            queueableBinaryWriter.Write(this.EasyGuidanceVsPlayer);
            queueableBinaryWriter.Write(this.NormalGuidanceVsPlayer);
            queueableBinaryWriter.Write(this.HardGuidanceVsPlayer);
            queueableBinaryWriter.Write(this.ImpossGuidanceVsPlayer);
            queueableBinaryWriter.Write(this.EasyMeleeDelayBase);
            queueableBinaryWriter.Write(this.NormalMeleeDelayBase);
            queueableBinaryWriter.Write(this.HardMeleeDelayBase);
            queueableBinaryWriter.Write(this.ImpossMeleeDelayBase);
            queueableBinaryWriter.Write(this.EasyMeleeDelayScale);
            queueableBinaryWriter.Write(this.NormalMeleeDelayScale);
            queueableBinaryWriter.Write(this.HardMeleeDelayScale);
            queueableBinaryWriter.Write(this.ImpossMeleeDelayScale);
            queueableBinaryWriter.Write(this.fieldpad0);
            queueableBinaryWriter.Write(this.EasyGrenadeChanceScale);
            queueableBinaryWriter.Write(this.NormalGrenadeChanceScale);
            queueableBinaryWriter.Write(this.HardGrenadeChanceScale);
            queueableBinaryWriter.Write(this.ImpossGrenadeChanceScale);
            queueableBinaryWriter.Write(this.EasyGrenadeTimerScale);
            queueableBinaryWriter.Write(this.NormalGrenadeTimerScale);
            queueableBinaryWriter.Write(this.HardGrenadeTimerScale);
            queueableBinaryWriter.Write(this.ImpossGrenadeTimerScale);
            queueableBinaryWriter.Write(this.fieldpad1);
            queueableBinaryWriter.Write(this.fieldpad2);
            queueableBinaryWriter.Write(this.fieldpad3);
            queueableBinaryWriter.Write(this.EasyMajorUpgrade);
            queueableBinaryWriter.Write(this.NormalMajorUpgrade);
            queueableBinaryWriter.Write(this.HardMajorUpgrade);
            queueableBinaryWriter.Write(this.ImpossMajorUpgrade);
            queueableBinaryWriter.Write(this.EasyMajorUpgrade0);
            queueableBinaryWriter.Write(this.NormalMajorUpgrade0);
            queueableBinaryWriter.Write(this.HardMajorUpgrade0);
            queueableBinaryWriter.Write(this.ImpossMajorUpgrade0);
            queueableBinaryWriter.Write(this.EasyMajorUpgrade1);
            queueableBinaryWriter.Write(this.NormalMajorUpgrade1);
            queueableBinaryWriter.Write(this.HardMajorUpgrade1);
            queueableBinaryWriter.Write(this.ImpossMajorUpgrade1);
            queueableBinaryWriter.Write(this.EasyPlayerVehicleRamChance);
            queueableBinaryWriter.Write(this.NormalPlayerVehicleRamChance);
            queueableBinaryWriter.Write(this.HardPlayerVehicleRamChance);
            queueableBinaryWriter.Write(this.ImpossPlayerVehicleRamChance);
            queueableBinaryWriter.Write(this.fieldpad4);
            queueableBinaryWriter.Write(this.fieldpad5);
            queueableBinaryWriter.Write(this.fieldpad6);
            queueableBinaryWriter.Write(this.fieldpad7);
        }
    }
}