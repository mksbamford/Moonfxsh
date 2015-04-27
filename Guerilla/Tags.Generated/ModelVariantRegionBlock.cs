// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ModelVariantRegionBlock : ModelVariantRegionBlockBase
    {
        public  ModelVariantRegionBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  ModelVariantRegionBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 20, Alignment = 4)]
    public class ModelVariantRegionBlockBase : GuerillaBlock
    {
        internal Moonfish.Tags.StringID regionNameMustMatchRegionNameInRenderModel;
        internal byte[] invalidName_;
        internal byte[] invalidName_0;
        internal Moonfish.Tags.ShortBlockIndex1 parentVariant;
        internal ModelVariantPermutationBlock[] permutations;
        /// <summary>
        /// negative values mean closer to the camera
        /// </summary>
        internal SortOrderNegativeValuesMeanCloserToTheCamera sortOrder;
        internal byte[] invalidName_1;
        
        public override int SerializedSize{get { return 20; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  ModelVariantRegionBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            regionNameMustMatchRegionNameInRenderModel = binaryReader.ReadStringID();
            invalidName_ = binaryReader.ReadBytes(1);
            invalidName_0 = binaryReader.ReadBytes(1);
            parentVariant = binaryReader.ReadShortBlockIndex1();
            permutations = Guerilla.ReadBlockArray<ModelVariantPermutationBlock>(binaryReader);
            sortOrder = (SortOrderNegativeValuesMeanCloserToTheCamera)binaryReader.ReadInt16();
            invalidName_1 = binaryReader.ReadBytes(2);
        }
        public  ModelVariantRegionBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(regionNameMustMatchRegionNameInRenderModel);
                binaryWriter.Write(invalidName_, 0, 1);
                binaryWriter.Write(invalidName_0, 0, 1);
                binaryWriter.Write(parentVariant);
                nextAddress = Guerilla.WriteBlockArray<ModelVariantPermutationBlock>(binaryWriter, permutations, nextAddress);
                binaryWriter.Write((Int16)sortOrder);
                binaryWriter.Write(invalidName_1, 0, 2);
                return nextAddress;
            }
        }
        internal enum SortOrderNegativeValuesMeanCloserToTheCamera : short
        {
            NoSorting = 0,
            Minus5Closest = 1,
            Minus4 = 2,
            Minus3 = 3,
            Minus2 = 4,
            Minus1 = 5,
            NoBiasSameAsModel = 6,
            Plus1 = 7,
            Plus2 = 8,
            Plus3 = 9,
            Plus4 = 10,
            Plus5Farthest = 11,
        };
    };
}
