// ReSharper disable All

using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Moonfish.Guerilla.Tags
{
    public partial class UserHintLineSegmentBlock : UserHintLineSegmentBlockBase
    {
        public UserHintLineSegmentBlock() : base()
        {
        }
    };

    [LayoutAttribute(Size = 36, Alignment = 4)]
    public class UserHintLineSegmentBlockBase : GuerillaBlock
    {
        internal Flags flags;
        internal OpenTK.Vector3 point0;
        internal short referenceFrame;
        internal byte[] invalidName_;
        internal OpenTK.Vector3 point1;
        internal short referenceFrame0;
        internal byte[] invalidName_0;

        public override int SerializedSize
        {
            get { return 36; }
        }

        public override int Alignment
        {
            get { return 4; }
        }

        public UserHintLineSegmentBlockBase() : base()
        {
        }

        public override Queue<BlamPointer> ReadFields(BinaryReader binaryReader)
        {
            var blamPointers = new Queue<BlamPointer>(base.ReadFields(binaryReader));
            flags = (Flags) binaryReader.ReadInt32();
            point0 = binaryReader.ReadVector3();
            referenceFrame = binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            point1 = binaryReader.ReadVector3();
            referenceFrame0 = binaryReader.ReadInt16();
            invalidName_0 = binaryReader.ReadBytes(2);
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
                binaryWriter.Write((Int32) flags);
                binaryWriter.Write(point0);
                binaryWriter.Write(referenceFrame);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write(point1);
                binaryWriter.Write(referenceFrame0);
                binaryWriter.Write(invalidName_0, 0, 2);
                return nextAddress;
            }
        }

        [FlagsAttribute]
        internal enum Flags : int
        {
            Bidirectional = 1,
            Closed = 2,
        };
    };
}