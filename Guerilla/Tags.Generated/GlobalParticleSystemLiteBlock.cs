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
    public partial class GlobalParticleSystemLiteBlock : GlobalParticleSystemLiteBlockBase
    {
        public GlobalParticleSystemLiteBlock() : base()
        {
        }
    };

    [LayoutAttribute(Size = 140, Alignment = 4)]
    public class GlobalParticleSystemLiteBlockBase : GuerillaBlock
    {
        [TagReference("bitm")] internal Moonfish.Tags.TagReference sprites;
        internal float viewBoxWidth;
        internal float viewBoxHeight;
        internal float viewBoxDepth;
        internal float exclusionRadius;
        internal float maxVelocity;
        internal float minMass;
        internal float maxMass;
        internal float minSize;
        internal float maxSize;
        internal int maximumNumberOfParticles;
        internal OpenTK.Vector3 initialVelocity;
        internal float bitmapAnimationSpeed;
        internal GlobalGeometryBlockInfoStructBlock geometryBlockInfo;
        internal ParticleSystemLiteDataBlock[] particleSystemData;
        internal Type type;
        internal byte[] invalidName_;
        internal float mininumOpacity;
        internal float maxinumOpacity;
        internal float rainStreakScale;
        internal float rainLineWidth;
        internal byte[] invalidName_0;
        internal byte[] invalidName_1;
        internal byte[] invalidName_2;

        public override int SerializedSize
        {
            get { return 140; }
        }

        public override int Alignment
        {
            get { return 4; }
        }

        public GlobalParticleSystemLiteBlockBase() : base()
        {
        }

        public override Queue<BlamPointer> ReadFields(BinaryReader binaryReader)
        {
            var blamPointers = new Queue<BlamPointer>(base.ReadFields(binaryReader));
            sprites = binaryReader.ReadTagReference();
            viewBoxWidth = binaryReader.ReadSingle();
            viewBoxHeight = binaryReader.ReadSingle();
            viewBoxDepth = binaryReader.ReadSingle();
            exclusionRadius = binaryReader.ReadSingle();
            maxVelocity = binaryReader.ReadSingle();
            minMass = binaryReader.ReadSingle();
            maxMass = binaryReader.ReadSingle();
            minSize = binaryReader.ReadSingle();
            maxSize = binaryReader.ReadSingle();
            maximumNumberOfParticles = binaryReader.ReadInt32();
            initialVelocity = binaryReader.ReadVector3();
            bitmapAnimationSpeed = binaryReader.ReadSingle();
            geometryBlockInfo = new GlobalGeometryBlockInfoStructBlock();
            blamPointers = new Queue<BlamPointer>(blamPointers.Concat(geometryBlockInfo.ReadFields(binaryReader)));
            blamPointers.Enqueue(ReadBlockArrayPointer<ParticleSystemLiteDataBlock>(binaryReader));
            type = (Type) binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            mininumOpacity = binaryReader.ReadSingle();
            maxinumOpacity = binaryReader.ReadSingle();
            rainStreakScale = binaryReader.ReadSingle();
            rainLineWidth = binaryReader.ReadSingle();
            invalidName_0 = binaryReader.ReadBytes(4);
            invalidName_1 = binaryReader.ReadBytes(4);
            invalidName_2 = binaryReader.ReadBytes(4);
            return blamPointers;
        }

        public override void ReadPointers(BinaryReader binaryReader, Queue<BlamPointer> blamPointers)
        {
            base.ReadPointers(binaryReader, blamPointers);
            geometryBlockInfo.ReadPointers(binaryReader, blamPointers);
            particleSystemData = ReadBlockArrayData<ParticleSystemLiteDataBlock>(binaryReader, blamPointers.Dequeue());
        }

        public override int Write(BinaryWriter binaryWriter, int nextAddress)
        {
            base.Write(binaryWriter, nextAddress);
            using (binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(sprites);
                binaryWriter.Write(viewBoxWidth);
                binaryWriter.Write(viewBoxHeight);
                binaryWriter.Write(viewBoxDepth);
                binaryWriter.Write(exclusionRadius);
                binaryWriter.Write(maxVelocity);
                binaryWriter.Write(minMass);
                binaryWriter.Write(maxMass);
                binaryWriter.Write(minSize);
                binaryWriter.Write(maxSize);
                binaryWriter.Write(maximumNumberOfParticles);
                binaryWriter.Write(initialVelocity);
                binaryWriter.Write(bitmapAnimationSpeed);
                geometryBlockInfo.Write(binaryWriter);
                nextAddress = Guerilla.WriteBlockArray<ParticleSystemLiteDataBlock>(binaryWriter, particleSystemData,
                    nextAddress);
                binaryWriter.Write((Int16) type);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write(mininumOpacity);
                binaryWriter.Write(maxinumOpacity);
                binaryWriter.Write(rainStreakScale);
                binaryWriter.Write(rainLineWidth);
                binaryWriter.Write(invalidName_0, 0, 4);
                binaryWriter.Write(invalidName_1, 0, 4);
                binaryWriter.Write(invalidName_2, 0, 4);
                return nextAddress;
            }
        }

        internal enum Type : short
        {
            Generic = 0,
            Snow = 1,
            Rain = 2,
            RainSplash = 3,
            Bugs = 4,
            SandStorm = 5,
            Debris = 6,
            Bubbles = 7,
        };
    };
}