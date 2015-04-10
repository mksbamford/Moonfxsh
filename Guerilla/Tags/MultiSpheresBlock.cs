using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class MultiSpheresBlock : MultiSpheresBlockBase
    {
        public  MultiSpheresBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 176)]
    public class MultiSpheresBlockBase
    {
        internal Moonfish.Tags.StringID name;
        internal Moonfish.Tags.ShortBlockIndex1 material;
        internal Flags flags;
        internal float relativeMassScale;
        internal float friction;
        internal float restitution;
        internal float volume;
        internal float mass;
        internal byte[] invalidName_;
        internal Moonfish.Tags.ShortBlockIndex1 phantom;
        internal byte[] invalidName_0;
        internal short size;
        internal short count;
        internal byte[] invalidName_1;
        internal int numSpheres;
        internal FourVectorsStorage[] fourVectorsStorage;
        internal  MultiSpheresBlockBase(BinaryReader binaryReader)
        {
            this.name = binaryReader.ReadStringID();
            this.material = binaryReader.ReadShortBlockIndex1();
            this.flags = (Flags)binaryReader.ReadInt16();
            this.relativeMassScale = binaryReader.ReadSingle();
            this.friction = binaryReader.ReadSingle();
            this.restitution = binaryReader.ReadSingle();
            this.volume = binaryReader.ReadSingle();
            this.mass = binaryReader.ReadSingle();
            this.invalidName_ = binaryReader.ReadBytes(2);
            this.phantom = binaryReader.ReadShortBlockIndex1();
            this.invalidName_0 = binaryReader.ReadBytes(4);
            this.size = binaryReader.ReadInt16();
            this.count = binaryReader.ReadInt16();
            this.invalidName_1 = binaryReader.ReadBytes(4);
            this.numSpheres = binaryReader.ReadInt32();
            this.fourVectorsStorage = new []{ new FourVectorsStorage(binaryReader), new FourVectorsStorage(binaryReader), new FourVectorsStorage(binaryReader), new FourVectorsStorage(binaryReader), new FourVectorsStorage(binaryReader), new FourVectorsStorage(binaryReader), new FourVectorsStorage(binaryReader), new FourVectorsStorage(binaryReader),  };
        }
        internal  virtual byte[] ReadData(BinaryReader binaryReader)
        {
            var blamPointer = binaryReader.ReadBlamPointer(1);
            var data = new byte[blamPointer.count];
            if(blamPointer.count > 0)
            {
                using (binaryReader.BaseStream.Pin())
                {
                    binaryReader.BaseStream.Position = blamPointer[0];
                    data = binaryReader.ReadBytes(blamPointer.count);
                }
            }
            return data;
        }
        [FlagsAttribute]
        internal enum Flags : short
        
        {
            Unused = 1,
        };
        public class FourVectorsStorage
        {
            internal OpenTK.Vector3 sphere;
            internal byte[] invalidName_;
            internal  FourVectorsStorage(BinaryReader binaryReader)
            {
                this.sphere = binaryReader.ReadVector3();
                this.invalidName_ = binaryReader.ReadBytes(4);
            }
            internal  virtual byte[] ReadData(BinaryReader binaryReader)
            {
                var blamPointer = binaryReader.ReadBlamPointer(1);
                var data = new byte[blamPointer.count];
                if(blamPointer.count > 0)
                {
                    using (binaryReader.BaseStream.Pin())
                    {
                        binaryReader.BaseStream.Position = blamPointer[0];
                        data = binaryReader.ReadBytes(blamPointer.count);
                    }
                }
                return data;
            }
        };
    };
}
