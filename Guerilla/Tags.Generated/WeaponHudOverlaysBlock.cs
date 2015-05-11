// ReSharper disable All

using Moonfish.Model;

using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Moonfish.Guerilla.Tags
{
    public partial class WeaponHudOverlaysBlock : WeaponHudOverlaysBlockBase
    {
        public WeaponHudOverlaysBlock() : base()
        {
        }
    };

    [LayoutAttribute(Size = 92, Alignment = 4)]
    public class WeaponHudOverlaysBlockBase : GuerillaBlock
    {
        internal StateAttachedTo stateAttachedTo;
        internal byte[] invalidName_;
        internal CanUseOnMapType canUseOnMapType;
        internal byte[] invalidName_0;
        internal byte[] invalidName_1;
        [TagReference("bitm")] internal Moonfish.Tags.TagReference overlayBitmap;
        internal WeaponHudOverlayBlock[] overlays;
        internal byte[] invalidName_2;

        public override int SerializedSize
        {
            get { return 92; }
        }

        public override int Alignment
        {
            get { return 4; }
        }

        public WeaponHudOverlaysBlockBase() : base()
        {
        }

        public override Queue<BlamPointer> ReadFields(BinaryReader binaryReader)
        {
            var blamPointers = new Queue<BlamPointer>(base.ReadFields(binaryReader));
            stateAttachedTo = (StateAttachedTo) binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            canUseOnMapType = (CanUseOnMapType) binaryReader.ReadInt16();
            invalidName_0 = binaryReader.ReadBytes(2);
            invalidName_1 = binaryReader.ReadBytes(28);
            overlayBitmap = binaryReader.ReadTagReference();
            blamPointers.Enqueue(ReadBlockArrayPointer<WeaponHudOverlayBlock>(binaryReader));
            invalidName_2 = binaryReader.ReadBytes(40);
            return blamPointers;
        }

        public override void ReadPointers(BinaryReader binaryReader, Queue<BlamPointer> blamPointers)
        {
            base.ReadPointers(binaryReader, blamPointers);
            overlays = ReadBlockArrayData<WeaponHudOverlayBlock>(binaryReader, blamPointers.Dequeue());
        }

        public override int Write(BinaryWriter binaryWriter, int nextAddress)
        {
            base.Write(binaryWriter, nextAddress);
            using (binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int16) stateAttachedTo);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write((Int16) canUseOnMapType);
                binaryWriter.Write(invalidName_0, 0, 2);
                binaryWriter.Write(invalidName_1, 0, 28);
                binaryWriter.Write(overlayBitmap);
                nextAddress = Guerilla.WriteBlockArray<WeaponHudOverlayBlock>(binaryWriter, overlays, nextAddress);
                binaryWriter.Write(invalidName_2, 0, 40);
                return nextAddress;
            }
        }

        internal enum StateAttachedTo : short
        {
            InventoryAmmo = 0,
            LoadedAmmo = 1,
            Heat = 2,
            Age = 3,
            SecondaryWeaponInventoryAmmo = 4,
            SecondaryWeaponLoadedAmmo = 5,
            DistanceToTarget = 6,
            ElevationToTarget = 7,
        };

        internal enum CanUseOnMapType : short
        {
            Any = 0,
            Solo = 1,
            Multiplayer = 2,
        };
    };
}