// ReSharper disable All

using Moonfish.Model;

using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Moonfish.Tags
{
    public partial struct TagClass
    {
        public static readonly TagClass Wphi = (TagClass) "wphi";
    };
} ;

namespace Moonfish.Guerilla.Tags
{
    [TagClassAttribute("wphi")]
    public partial class WeaponHudInterfaceBlock : WeaponHudInterfaceBlockBase
    {
        public WeaponHudInterfaceBlock() : base()
        {
        }
    };

    [LayoutAttribute(Size = 344, Alignment = 4)]
    public class WeaponHudInterfaceBlockBase : GuerillaBlock
    {
        [TagReference("wphi")] internal Moonfish.Tags.TagReference childHud;
        internal Flags flags;
        internal byte[] invalidName_;
        internal short inventoryAmmoCutoff;
        internal short loadedAmmoCutoff;
        internal short heatCutoff;
        internal short ageCutoff;
        internal byte[] invalidName_0;
        internal Anchor anchor;
        internal byte[] invalidName_1;
        internal byte[] invalidName_2;
        internal WeaponHudStaticBlock[] staticElements;
        internal WeaponHudMeterBlock[] meterElements;
        internal WeaponHudNumberBlock[] numberElements;
        internal WeaponHudCrosshairBlock[] crosshairs;
        internal WeaponHudOverlaysBlock[] overlayElements;
        internal byte[] invalidName_3;
        internal GNullBlock[] gNullBlock;
        internal GlobalHudScreenEffectDefinition[] screenEffect;
        internal byte[] invalidName_4;

        /// <summary>
        /// sequenceIndex into the global hud icon bitmap
        /// </summary>
        internal short sequenceIndex;

        /// <summary>
        /// extra spacing beyond bitmap width for text alignment
        /// </summary>
        internal short widthOffset;

        internal Moonfish.Tags.Point offsetFromReferenceCorner;
        internal Moonfish.Tags.ColourA1R1G1B1 overrideIconColor;
        internal byte frameRate030;
        internal Flags flags0;
        internal short textIndex;
        internal byte[] invalidName_5;

        public override int SerializedSize
        {
            get { return 344; }
        }

        public override int Alignment
        {
            get { return 4; }
        }

        public WeaponHudInterfaceBlockBase() : base()
        {
        }

        public override Queue<BlamPointer> ReadFields(BinaryReader binaryReader)
        {
            var blamPointers = new Queue<BlamPointer>(base.ReadFields(binaryReader));
            childHud = binaryReader.ReadTagReference();
            flags = (Flags) binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            inventoryAmmoCutoff = binaryReader.ReadInt16();
            loadedAmmoCutoff = binaryReader.ReadInt16();
            heatCutoff = binaryReader.ReadInt16();
            ageCutoff = binaryReader.ReadInt16();
            invalidName_0 = binaryReader.ReadBytes(32);
            anchor = (Anchor) binaryReader.ReadInt16();
            invalidName_1 = binaryReader.ReadBytes(2);
            invalidName_2 = binaryReader.ReadBytes(32);
            blamPointers.Enqueue(ReadBlockArrayPointer<WeaponHudStaticBlock>(binaryReader));
            blamPointers.Enqueue(ReadBlockArrayPointer<WeaponHudMeterBlock>(binaryReader));
            blamPointers.Enqueue(ReadBlockArrayPointer<WeaponHudNumberBlock>(binaryReader));
            blamPointers.Enqueue(ReadBlockArrayPointer<WeaponHudCrosshairBlock>(binaryReader));
            blamPointers.Enqueue(ReadBlockArrayPointer<WeaponHudOverlaysBlock>(binaryReader));
            invalidName_3 = binaryReader.ReadBytes(4);
            blamPointers.Enqueue(ReadBlockArrayPointer<GNullBlock>(binaryReader));
            blamPointers.Enqueue(ReadBlockArrayPointer<GlobalHudScreenEffectDefinition>(binaryReader));
            invalidName_4 = binaryReader.ReadBytes(132);
            sequenceIndex = binaryReader.ReadInt16();
            widthOffset = binaryReader.ReadInt16();
            offsetFromReferenceCorner = binaryReader.ReadPoint();
            overrideIconColor = binaryReader.ReadColourA1R1G1B1();
            frameRate030 = binaryReader.ReadByte();
            flags0 = (Flags) binaryReader.ReadInt16();
            textIndex = binaryReader.ReadInt16();
            invalidName_5 = binaryReader.ReadBytes(48);
            return blamPointers;
        }

        public override void ReadPointers(BinaryReader binaryReader, Queue<BlamPointer> blamPointers)
        {
            base.ReadPointers(binaryReader, blamPointers);
            staticElements = ReadBlockArrayData<WeaponHudStaticBlock>(binaryReader, blamPointers.Dequeue());
            meterElements = ReadBlockArrayData<WeaponHudMeterBlock>(binaryReader, blamPointers.Dequeue());
            numberElements = ReadBlockArrayData<WeaponHudNumberBlock>(binaryReader, blamPointers.Dequeue());
            crosshairs = ReadBlockArrayData<WeaponHudCrosshairBlock>(binaryReader, blamPointers.Dequeue());
            overlayElements = ReadBlockArrayData<WeaponHudOverlaysBlock>(binaryReader, blamPointers.Dequeue());
            gNullBlock = ReadBlockArrayData<GNullBlock>(binaryReader, blamPointers.Dequeue());
            screenEffect = ReadBlockArrayData<GlobalHudScreenEffectDefinition>(binaryReader, blamPointers.Dequeue());
        }

        public override int Write(BinaryWriter binaryWriter, int nextAddress)
        {
            base.Write(binaryWriter, nextAddress);
            using (binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(childHud);
                binaryWriter.Write((Int16) flags);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write(inventoryAmmoCutoff);
                binaryWriter.Write(loadedAmmoCutoff);
                binaryWriter.Write(heatCutoff);
                binaryWriter.Write(ageCutoff);
                binaryWriter.Write(invalidName_0, 0, 32);
                binaryWriter.Write((Int16) anchor);
                binaryWriter.Write(invalidName_1, 0, 2);
                binaryWriter.Write(invalidName_2, 0, 32);
                nextAddress = Guerilla.WriteBlockArray<WeaponHudStaticBlock>(binaryWriter, staticElements, nextAddress);
                nextAddress = Guerilla.WriteBlockArray<WeaponHudMeterBlock>(binaryWriter, meterElements, nextAddress);
                nextAddress = Guerilla.WriteBlockArray<WeaponHudNumberBlock>(binaryWriter, numberElements, nextAddress);
                nextAddress = Guerilla.WriteBlockArray<WeaponHudCrosshairBlock>(binaryWriter, crosshairs, nextAddress);
                nextAddress = Guerilla.WriteBlockArray<WeaponHudOverlaysBlock>(binaryWriter, overlayElements,
                    nextAddress);
                binaryWriter.Write(invalidName_3, 0, 4);
                nextAddress = Guerilla.WriteBlockArray<GNullBlock>(binaryWriter, gNullBlock, nextAddress);
                nextAddress = Guerilla.WriteBlockArray<GlobalHudScreenEffectDefinition>(binaryWriter, screenEffect,
                    nextAddress);
                binaryWriter.Write(invalidName_4, 0, 132);
                binaryWriter.Write(sequenceIndex);
                binaryWriter.Write(widthOffset);
                binaryWriter.Write(offsetFromReferenceCorner);
                binaryWriter.Write(overrideIconColor);
                binaryWriter.Write(frameRate030);
                binaryWriter.Write((Int16) flags0);
                binaryWriter.Write(textIndex);
                binaryWriter.Write(invalidName_5, 0, 48);
                return nextAddress;
            }
        }

        [FlagsAttribute]
        internal enum Flags : short
        {
            UseParentHudFlashingParameters = 1,
        };

        internal enum Anchor : short
        {
            TopLeft = 0,
            TopRight = 1,
            BottomLeft = 2,
            BottomRight = 3,
            Center = 4,
            Crosshair = 5,
        };

        [FlagsAttribute]
        internal enum Flags0 : byte
        {
            UseTextFromStringListInstead = 1,
            OverrideDefaultColor = 2,
            WidthOffsetIsAbsoluteIconWidth = 4,
        };
    };
}