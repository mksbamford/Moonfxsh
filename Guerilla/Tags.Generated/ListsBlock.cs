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
    public partial class ListsBlock : ListsBlockBase
    {
        public ListsBlock() : base()
        {
        }
    };

    [LayoutAttribute(Size = 56, Alignment = 4)]
    public class ListsBlockBase : GuerillaBlock
    {
        internal byte[] invalidName_;
        internal short size;
        internal short count;
        internal byte[] invalidName_0;
        internal byte[] invalidName_1;
        internal int childShapesSize;
        internal int childShapesCapacity;
        internal ChildShapesStorage[] childShapesStorage;

        public override int SerializedSize
        {
            get { return 56; }
        }

        public override int Alignment
        {
            get { return 4; }
        }

        public ListsBlockBase() : base()
        {
        }

        public override Queue<BlamPointer> ReadFields(BinaryReader binaryReader)
        {
            var blamPointers = new Queue<BlamPointer>(base.ReadFields(binaryReader));
            invalidName_ = binaryReader.ReadBytes(4);
            size = binaryReader.ReadInt16();
            count = binaryReader.ReadInt16();
            invalidName_0 = binaryReader.ReadBytes(4);
            invalidName_1 = binaryReader.ReadBytes(4);
            childShapesSize = binaryReader.ReadInt32();
            childShapesCapacity = binaryReader.ReadInt32();
            childShapesStorage = new[]
            {new ChildShapesStorage(), new ChildShapesStorage(), new ChildShapesStorage(), new ChildShapesStorage()};
            blamPointers = new Queue<BlamPointer>(blamPointers.Concat(childShapesStorage[0].ReadFields(binaryReader)));
            blamPointers = new Queue<BlamPointer>(blamPointers.Concat(childShapesStorage[1].ReadFields(binaryReader)));
            blamPointers = new Queue<BlamPointer>(blamPointers.Concat(childShapesStorage[2].ReadFields(binaryReader)));
            blamPointers = new Queue<BlamPointer>(blamPointers.Concat(childShapesStorage[3].ReadFields(binaryReader)));
            return blamPointers;
        }

        public override void ReadPointers(BinaryReader binaryReader, Queue<BlamPointer> blamPointers)
        {
            base.ReadPointers(binaryReader, blamPointers);
            childShapesStorage[0].ReadPointers(binaryReader, blamPointers);
            childShapesStorage[1].ReadPointers(binaryReader, blamPointers);
            childShapesStorage[2].ReadPointers(binaryReader, blamPointers);
            childShapesStorage[3].ReadPointers(binaryReader, blamPointers);
        }

        public override int Write(BinaryWriter binaryWriter, int nextAddress)
        {
            base.Write(binaryWriter, nextAddress);
            using (binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(invalidName_, 0, 4);
                binaryWriter.Write(size);
                binaryWriter.Write(count);
                binaryWriter.Write(invalidName_0, 0, 4);
                binaryWriter.Write(invalidName_1, 0, 4);
                binaryWriter.Write(childShapesSize);
                binaryWriter.Write(childShapesCapacity);
                childShapesStorage[0].Write(binaryWriter);
                childShapesStorage[1].Write(binaryWriter);
                childShapesStorage[2].Write(binaryWriter);
                childShapesStorage[3].Write(binaryWriter);
                return nextAddress;
            }
        }

        [LayoutAttribute(Size = 8, Alignment = 1)]
        public class ChildShapesStorage : GuerillaBlock
        {
            internal ShapeType shapeType;
            internal Moonfish.Tags.ShortBlockIndex2 shape;
            internal int collisionFilter;

            public override int SerializedSize
            {
                get { return 8; }
            }

            public override int Alignment
            {
                get { return 1; }
            }

            public ChildShapesStorage() : base()
            {
            }

            public override Queue<BlamPointer> ReadFields(BinaryReader binaryReader)
            {
                var blamPointers = new Queue<BlamPointer>(base.ReadFields(binaryReader));
                shapeType = (ShapeType) binaryReader.ReadInt16();
                shape = binaryReader.ReadShortBlockIndex2();
                collisionFilter = binaryReader.ReadInt32();
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
                    binaryWriter.Write((Int16) shapeType);
                    binaryWriter.Write(shape);
                    binaryWriter.Write(collisionFilter);
                    return nextAddress;
                }
            }

            internal enum ShapeType : short
            {
                Sphere = 0,
                Pill = 1,
                Box = 2,
                Triangle = 3,
                Polyhedron = 4,
                MultiSphere = 5,
                Unused0 = 6,
                Unused1 = 7,
                Unused2 = 8,
                Unused3 = 9,
                Unused4 = 10,
                Unused5 = 11,
                Unused6 = 12,
                Unused7 = 13,
                List = 14,
                Mopp = 15,
            };
        };
    };
}