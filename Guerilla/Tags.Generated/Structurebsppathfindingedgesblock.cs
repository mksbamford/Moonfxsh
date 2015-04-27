// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class StructureBspPathfindingEdgesBlock : StructureBspPathfindingEdgesBlockBase
    {
        public  StructureBspPathfindingEdgesBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  StructureBspPathfindingEdgesBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 1, Alignment = 4)]
    public class StructureBspPathfindingEdgesBlockBase : GuerillaBlock
    {
        internal byte midpoint;
        
        public override int SerializedSize{get { return 1; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  StructureBspPathfindingEdgesBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            midpoint = binaryReader.ReadByte();
        }
        public  StructureBspPathfindingEdgesBlockBase(): base()
        {
            
        }
        public override void Read(BinaryReader binaryReader)
        {
            midpoint = binaryReader.ReadByte();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(midpoint);
                return nextAddress;
            }
        }
    };
}
