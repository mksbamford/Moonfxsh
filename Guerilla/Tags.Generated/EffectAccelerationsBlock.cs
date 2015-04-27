// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class EffectAccelerationsBlock : EffectAccelerationsBlockBase
    {
        public  EffectAccelerationsBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  EffectAccelerationsBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 20, Alignment = 4)]
    public class EffectAccelerationsBlockBase : GuerillaBlock
    {
        internal CreateIn createIn;
        internal CreateIn createIn0;
        internal Moonfish.Tags.ShortBlockIndex1 location;
        internal byte[] invalidName_;
        internal float acceleration;
        internal float innerConeAngleDegrees;
        internal float outerConeAngleDegrees;
        
        public override int SerializedSize{get { return 20; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  EffectAccelerationsBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            createIn = (CreateIn)binaryReader.ReadInt16();
            createIn0 = (CreateIn)binaryReader.ReadInt16();
            location = binaryReader.ReadShortBlockIndex1();
            invalidName_ = binaryReader.ReadBytes(2);
            acceleration = binaryReader.ReadSingle();
            innerConeAngleDegrees = binaryReader.ReadSingle();
            outerConeAngleDegrees = binaryReader.ReadSingle();
        }
        public  EffectAccelerationsBlockBase(): base()
        {
            
        }
        public override void Read(BinaryReader binaryReader)
        {
            createIn = (CreateIn)binaryReader.ReadInt16();
            createIn0 = (CreateIn)binaryReader.ReadInt16();
            location = binaryReader.ReadShortBlockIndex1();
            invalidName_ = binaryReader.ReadBytes(2);
            acceleration = binaryReader.ReadSingle();
            innerConeAngleDegrees = binaryReader.ReadSingle();
            outerConeAngleDegrees = binaryReader.ReadSingle();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int16)createIn);
                binaryWriter.Write((Int16)createIn0);
                binaryWriter.Write(location);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write(acceleration);
                binaryWriter.Write(innerConeAngleDegrees);
                binaryWriter.Write(outerConeAngleDegrees);
                return nextAddress;
            }
        }
        internal enum CreateIn : short
        {
            AnyEnvironment = 0,
            AirOnly = 1,
            WaterOnly = 2,
            SpaceOnly = 3,
        };
        internal enum CreateIn0 : short
        {
            EitherMode = 0,
            ViolentModeOnly = 1,
            NonviolentModeOnly = 2,
        };
    };
}
