// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class DecoratorProjectedDecalBlock : DecoratorProjectedDecalBlockBase
    {
        public  DecoratorProjectedDecalBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  DecoratorProjectedDecalBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 64, Alignment = 4)]
    public class DecoratorProjectedDecalBlockBase : GuerillaBlock
    {
        internal Moonfish.Tags.ByteBlockIndex1 decoratorSet;
        internal byte decoratorClass;
        internal byte decoratorPermutation;
        internal byte spriteIndex;
        internal OpenTK.Vector3 position;
        internal OpenTK.Vector3 left;
        internal OpenTK.Vector3 up;
        internal OpenTK.Vector3 extents;
        internal OpenTK.Vector3 previousPosition;
        
        public override int SerializedSize{get { return 64; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  DecoratorProjectedDecalBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            decoratorSet = binaryReader.ReadByteBlockIndex1();
            decoratorClass = binaryReader.ReadByte();
            decoratorPermutation = binaryReader.ReadByte();
            spriteIndex = binaryReader.ReadByte();
            position = binaryReader.ReadVector3();
            left = binaryReader.ReadVector3();
            up = binaryReader.ReadVector3();
            extents = binaryReader.ReadVector3();
            previousPosition = binaryReader.ReadVector3();
        }
        public  DecoratorProjectedDecalBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            decoratorSet = binaryReader.ReadByteBlockIndex1();
            decoratorClass = binaryReader.ReadByte();
            decoratorPermutation = binaryReader.ReadByte();
            spriteIndex = binaryReader.ReadByte();
            position = binaryReader.ReadVector3();
            left = binaryReader.ReadVector3();
            up = binaryReader.ReadVector3();
            extents = binaryReader.ReadVector3();
            previousPosition = binaryReader.ReadVector3();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(decoratorSet);
                binaryWriter.Write(decoratorClass);
                binaryWriter.Write(decoratorPermutation);
                binaryWriter.Write(spriteIndex);
                binaryWriter.Write(position);
                binaryWriter.Write(left);
                binaryWriter.Write(up);
                binaryWriter.Write(extents);
                binaryWriter.Write(previousPosition);
                return nextAddress;
            }
        }
    };
}
