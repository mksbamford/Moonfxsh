// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class NodeRenderLeavesBlock : NodeRenderLeavesBlockBase
    {
        public  NodeRenderLeavesBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  NodeRenderLeavesBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 16, Alignment = 4)]
    public class NodeRenderLeavesBlockBase : GuerillaBlock
    {
        internal BspLeafBlock[] collisionLeaves;
        internal BspSurfaceReferenceBlock[] surfaceReferences;
        
        public override int SerializedSize{get { return 16; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  NodeRenderLeavesBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            collisionLeaves = Guerilla.ReadBlockArray<BspLeafBlock>(binaryReader);
            surfaceReferences = Guerilla.ReadBlockArray<BspSurfaceReferenceBlock>(binaryReader);
        }
        public  NodeRenderLeavesBlockBase(): base()
        {
            
        }
        public override void Read(BinaryReader binaryReader)
        {
            collisionLeaves = Guerilla.ReadBlockArray<BspLeafBlock>(binaryReader);
            surfaceReferences = Guerilla.ReadBlockArray<BspSurfaceReferenceBlock>(binaryReader);
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                nextAddress = Guerilla.WriteBlockArray<BspLeafBlock>(binaryWriter, collisionLeaves, nextAddress);
                nextAddress = Guerilla.WriteBlockArray<BspSurfaceReferenceBlock>(binaryWriter, surfaceReferences, nextAddress);
                return nextAddress;
            }
        }
    };
}
