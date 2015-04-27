// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class EnvironmentObjectNodes : EnvironmentObjectNodesBase
    {
        public  EnvironmentObjectNodes(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  EnvironmentObjectNodes(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 4, Alignment = 4)]
    public class EnvironmentObjectNodesBase : GuerillaBlock
    {
        internal short referenceFrameIndex;
        internal byte projectionAxis;
        internal ProjectionSign projectionSign;
        
        public override int SerializedSize{get { return 4; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  EnvironmentObjectNodesBase(BinaryReader binaryReader): base(binaryReader)
        {
            referenceFrameIndex = binaryReader.ReadInt16();
            projectionAxis = binaryReader.ReadByte();
            projectionSign = (ProjectionSign)binaryReader.ReadByte();
        }
        public  EnvironmentObjectNodesBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(referenceFrameIndex);
                binaryWriter.Write(projectionAxis);
                binaryWriter.Write((Byte)projectionSign);
                return nextAddress;
            }
        }
        [FlagsAttribute]
        internal enum ProjectionSign : byte
        {
            ProjectionSign = 1,
        };
    };
}
