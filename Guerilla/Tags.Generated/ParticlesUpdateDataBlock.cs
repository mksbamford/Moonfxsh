// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ParticlesUpdateDataBlock : ParticlesUpdateDataBlockBase
    {
        public  ParticlesUpdateDataBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  ParticlesUpdateDataBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 32, Alignment = 4)]
    public class ParticlesUpdateDataBlockBase : GuerillaBlock
    {
        internal float velocityX;
        internal float velocityY;
        internal float velocityZ;
        internal byte[] invalidName_;
        internal float mass;
        internal float creationTimeStamp;
        
        public override int SerializedSize{get { return 32; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  ParticlesUpdateDataBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            velocityX = binaryReader.ReadSingle();
            velocityY = binaryReader.ReadSingle();
            velocityZ = binaryReader.ReadSingle();
            invalidName_ = binaryReader.ReadBytes(12);
            mass = binaryReader.ReadSingle();
            creationTimeStamp = binaryReader.ReadSingle();
        }
        public  ParticlesUpdateDataBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(velocityX);
                binaryWriter.Write(velocityY);
                binaryWriter.Write(velocityZ);
                binaryWriter.Write(invalidName_, 0, 12);
                binaryWriter.Write(mass);
                binaryWriter.Write(creationTimeStamp);
                return nextAddress;
            }
        }
    };
}
