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
    public partial class ScenarioNetgameEquipmentOrientationStructBlock :
        ScenarioNetgameEquipmentOrientationStructBlockBase
    {
        public ScenarioNetgameEquipmentOrientationStructBlock() : base()
        {
        }
    };

    [LayoutAttribute(Size = 12, Alignment = 4)]
    public class ScenarioNetgameEquipmentOrientationStructBlockBase : GuerillaBlock
    {
        internal OpenTK.Vector3 orientation;

        public override int SerializedSize
        {
            get { return 12; }
        }

        public override int Alignment
        {
            get { return 4; }
        }

        public ScenarioNetgameEquipmentOrientationStructBlockBase() : base()
        {
        }

        public override Queue<BlamPointer> ReadFields(BinaryReader binaryReader)
        {
            var blamPointers = new Queue<BlamPointer>(base.ReadFields(binaryReader));
            orientation = binaryReader.ReadVector3();
            return blamPointers;
        }

        public override void ReadPointers(BinaryReader binaryReader, Queue<BlamPointer> blamPointers)
        {
            base.ReadPointers(binaryReader, blamPointers);
        }

        public override int Write(BinaryWriter binaryWriter, int nextAddress)
        {
            base.Write(binaryWriter, nextAddress);
            using (binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(orientation);
                return nextAddress;
            }
        }
    };
}