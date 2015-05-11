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
    public partial class UserHintWellBlock : UserHintWellBlockBase
    {
        public UserHintWellBlock() : base()
        {
        }
    };

    [LayoutAttribute(Size = 12, Alignment = 4)]
    public class UserHintWellBlockBase : GuerillaBlock
    {
        internal Flags flags;
        internal UserHintWellPointBlock[] points;

        public override int SerializedSize
        {
            get { return 12; }
        }

        public override int Alignment
        {
            get { return 4; }
        }

        public UserHintWellBlockBase() : base()
        {
        }

        public override Queue<BlamPointer> ReadFields(BinaryReader binaryReader)
        {
            var blamPointers = new Queue<BlamPointer>(base.ReadFields(binaryReader));
            flags = (Flags) binaryReader.ReadInt32();
            blamPointers.Enqueue(ReadBlockArrayPointer<UserHintWellPointBlock>(binaryReader));
            return blamPointers;
        }

        public override void ReadPointers(BinaryReader binaryReader, Queue<BlamPointer> blamPointers)
        {
            base.ReadPointers(binaryReader, blamPointers);
            points = ReadBlockArrayData<UserHintWellPointBlock>(binaryReader, blamPointers.Dequeue());
        }

        public override int Write(BinaryWriter binaryWriter, int nextAddress)
        {
            base.Write(binaryWriter, nextAddress);
            using (binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int32) flags);
                nextAddress = Guerilla.WriteBlockArray<UserHintWellPointBlock>(binaryWriter, points, nextAddress);
                return nextAddress;
            }
        }

        [FlagsAttribute]
        internal enum Flags : int
        {
            Bidirectional = 1,
        };
    };
}