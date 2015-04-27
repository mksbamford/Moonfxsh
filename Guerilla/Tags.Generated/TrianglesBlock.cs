// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class TrianglesBlock : TrianglesBlockBase
    {
        public  TrianglesBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  TrianglesBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 96, Alignment = 16)]
    public class TrianglesBlockBase : GuerillaBlock
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
        internal float radius;
        internal OpenTK.Vector3 pointA;
        internal byte[] invalidName_2;
        internal OpenTK.Vector3 pointB;
        internal byte[] invalidName_3;
        internal OpenTK.Vector3 pointC;
        internal byte[] invalidName_4;
        
        public override int SerializedSize{get { return 96; }}
        
        
        public override int Alignment{get { return 16; }}
        
        public  TrianglesBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            name = binaryReader.ReadStringID();
            material = binaryReader.ReadShortBlockIndex1();
            flags = (Flags)binaryReader.ReadInt16();
            relativeMassScale = binaryReader.ReadSingle();
            friction = binaryReader.ReadSingle();
            restitution = binaryReader.ReadSingle();
            volume = binaryReader.ReadSingle();
            mass = binaryReader.ReadSingle();
            invalidName_ = binaryReader.ReadBytes(2);
            phantom = binaryReader.ReadShortBlockIndex1();
            invalidName_0 = binaryReader.ReadBytes(4);
            size = binaryReader.ReadInt16();
            count = binaryReader.ReadInt16();
            invalidName_1 = binaryReader.ReadBytes(4);
            radius = binaryReader.ReadSingle();
            pointA = binaryReader.ReadVector3();
            invalidName_2 = binaryReader.ReadBytes(4);
            pointB = binaryReader.ReadVector3();
            invalidName_3 = binaryReader.ReadBytes(4);
            pointC = binaryReader.ReadVector3();
            invalidName_4 = binaryReader.ReadBytes(4);
        }
        public  TrianglesBlockBase(): base()
        {
            
        }
        public override void Read(BinaryReader binaryReader)
        {
            name = binaryReader.ReadStringID();
            material = binaryReader.ReadShortBlockIndex1();
            flags = (Flags)binaryReader.ReadInt16();
            relativeMassScale = binaryReader.ReadSingle();
            friction = binaryReader.ReadSingle();
            restitution = binaryReader.ReadSingle();
            volume = binaryReader.ReadSingle();
            mass = binaryReader.ReadSingle();
            invalidName_ = binaryReader.ReadBytes(2);
            phantom = binaryReader.ReadShortBlockIndex1();
            invalidName_0 = binaryReader.ReadBytes(4);
            size = binaryReader.ReadInt16();
            count = binaryReader.ReadInt16();
            invalidName_1 = binaryReader.ReadBytes(4);
            radius = binaryReader.ReadSingle();
            pointA = binaryReader.ReadVector3();
            invalidName_2 = binaryReader.ReadBytes(4);
            pointB = binaryReader.ReadVector3();
            invalidName_3 = binaryReader.ReadBytes(4);
            pointC = binaryReader.ReadVector3();
            invalidName_4 = binaryReader.ReadBytes(4);
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(name);
                binaryWriter.Write(material);
                binaryWriter.Write((Int16)flags);
                binaryWriter.Write(relativeMassScale);
                binaryWriter.Write(friction);
                binaryWriter.Write(restitution);
                binaryWriter.Write(volume);
                binaryWriter.Write(mass);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write(phantom);
                binaryWriter.Write(invalidName_0, 0, 4);
                binaryWriter.Write(size);
                binaryWriter.Write(count);
                binaryWriter.Write(invalidName_1, 0, 4);
                binaryWriter.Write(radius);
                binaryWriter.Write(pointA);
                binaryWriter.Write(invalidName_2, 0, 4);
                binaryWriter.Write(pointB);
                binaryWriter.Write(invalidName_3, 0, 4);
                binaryWriter.Write(pointC);
                binaryWriter.Write(invalidName_4, 0, 4);
                return nextAddress;
            }
        }
        [FlagsAttribute]
        internal enum Flags : short
        {
            Unused = 1,
        };
    };
}
