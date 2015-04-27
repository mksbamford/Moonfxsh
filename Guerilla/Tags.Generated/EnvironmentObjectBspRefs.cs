// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class EnvironmentObjectBspRefs : EnvironmentObjectBspRefsBase
    {
        public  EnvironmentObjectBspRefs(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  EnvironmentObjectBspRefs(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 16, Alignment = 4)]
    public class EnvironmentObjectBspRefsBase : GuerillaBlock
    {
        internal int bspReference;
        internal int firstSector;
        internal int lastSector;
        internal short nodeIndex;
        internal byte[] invalidName_;
        
        public override int SerializedSize{get { return 16; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  EnvironmentObjectBspRefsBase(BinaryReader binaryReader): base(binaryReader)
        {
            bspReference = binaryReader.ReadInt32();
            firstSector = binaryReader.ReadInt32();
            lastSector = binaryReader.ReadInt32();
            nodeIndex = binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
        }
        public  EnvironmentObjectBspRefsBase(): base()
        {
            
        }
        public override void Read(BinaryReader binaryReader)
        {
            bspReference = binaryReader.ReadInt32();
            firstSector = binaryReader.ReadInt32();
            lastSector = binaryReader.ReadInt32();
            nodeIndex = binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(bspReference);
                binaryWriter.Write(firstSector);
                binaryWriter.Write(lastSector);
                binaryWriter.Write(nodeIndex);
                binaryWriter.Write(invalidName_, 0, 2);
                return nextAddress;
            }
        }
    };
}
