// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class PathfindingObjectIndexListBlock : PathfindingObjectIndexListBlockBase
    {
        public  PathfindingObjectIndexListBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  PathfindingObjectIndexListBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 4, Alignment = 4)]
    public class PathfindingObjectIndexListBlockBase : GuerillaBlock
    {
        internal short bSPIndex;
        internal short pathfindingObjectIndex;
        
        public override int SerializedSize{get { return 4; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  PathfindingObjectIndexListBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            bSPIndex = binaryReader.ReadInt16();
            pathfindingObjectIndex = binaryReader.ReadInt16();
        }
        public  PathfindingObjectIndexListBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            bSPIndex = binaryReader.ReadInt16();
            pathfindingObjectIndex = binaryReader.ReadInt16();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(bSPIndex);
                binaryWriter.Write(pathfindingObjectIndex);
                return nextAddress;
            }
        }
    };
}
