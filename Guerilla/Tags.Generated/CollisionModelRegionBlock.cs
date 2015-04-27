// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class CollisionModelRegionBlock : CollisionModelRegionBlockBase
    {
        public  CollisionModelRegionBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  CollisionModelRegionBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 12, Alignment = 4)]
    public class CollisionModelRegionBlockBase : GuerillaBlock
    {
        internal Moonfish.Tags.StringID name;
        internal CollisionModelPermutationBlock[] permutations;
        
        public override int SerializedSize{get { return 12; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  CollisionModelRegionBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            name = binaryReader.ReadStringID();
            permutations = Guerilla.ReadBlockArray<CollisionModelPermutationBlock>(binaryReader);
        }
        public  CollisionModelRegionBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(name);
                nextAddress = Guerilla.WriteBlockArray<CollisionModelPermutationBlock>(binaryWriter, permutations, nextAddress);
                return nextAddress;
            }
        }
    };
}
