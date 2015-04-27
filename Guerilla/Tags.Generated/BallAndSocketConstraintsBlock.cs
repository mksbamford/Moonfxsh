// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class BallAndSocketConstraintsBlock : BallAndSocketConstraintsBlockBase
    {
        public  BallAndSocketConstraintsBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  BallAndSocketConstraintsBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 120, Alignment = 4)]
    public class BallAndSocketConstraintsBlockBase : GuerillaBlock
    {
        internal ConstraintBodiesStructBlock constraintBodies;
        internal byte[] invalidName_;
        
        public override int SerializedSize{get { return 120; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  BallAndSocketConstraintsBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            constraintBodies = new ConstraintBodiesStructBlock(binaryReader);
            invalidName_ = binaryReader.ReadBytes(4);
        }
        public  BallAndSocketConstraintsBlockBase(): base()
        {
            
        }
        public override void Read(BinaryReader binaryReader)
        {
            constraintBodies = new ConstraintBodiesStructBlock(binaryReader);
            invalidName_ = binaryReader.ReadBytes(4);
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                constraintBodies.Write(binaryWriter);
                binaryWriter.Write(invalidName_, 0, 4);
                return nextAddress;
            }
        }
    };
}
