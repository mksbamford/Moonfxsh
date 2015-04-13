using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class AnimationGraphContentsStructBlock : AnimationGraphContentsStructBlockBase
    {
        public  AnimationGraphContentsStructBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 24)]
    public class AnimationGraphContentsStructBlockBase
    {
        internal AnimationModeBlock[] modesAABBCC;
        internal VehicleSuspensionBlock[] vehicleSuspensionCCAABB;
        internal ObjectAnimationBlock[] objectOverlaysCCAABB;
        internal  AnimationGraphContentsStructBlockBase(BinaryReader binaryReader)
        {
            this.modesAABBCC = ReadAnimationModeBlockArray(binaryReader);
            this.vehicleSuspensionCCAABB = ReadVehicleSuspensionBlockArray(binaryReader);
            this.objectOverlaysCCAABB = ReadObjectAnimationBlockArray(binaryReader);
        }
        internal  virtual byte[] ReadData(BinaryReader binaryReader)
        {
            var blamPointer = binaryReader.ReadBlamPointer(1);
            var data = new byte[blamPointer.elementCount];
            if(blamPointer.elementCount > 0)
            {
                using (binaryReader.BaseStream.Pin())
                {
                    binaryReader.BaseStream.Position = blamPointer[0];
                    data = binaryReader.ReadBytes(blamPointer.elementCount);
                }
            }
            return data;
        }
        internal  virtual AnimationModeBlock[] ReadAnimationModeBlockArray(BinaryReader binaryReader)
        {
            var elementSize = Deserializer.SizeOf(typeof(AnimationModeBlock));
            var blamPointer = binaryReader.ReadBlamPointer(elementSize);
            var array = new AnimationModeBlock[blamPointer.elementCount];
            using (binaryReader.BaseStream.Pin())
            {
                for (int i = 0; i < blamPointer.elementCount; ++i)
                {
                    binaryReader.BaseStream.Position = blamPointer[i];
                    array[i] = new AnimationModeBlock(binaryReader);
                }
            }
            return array;
        }
        internal  virtual VehicleSuspensionBlock[] ReadVehicleSuspensionBlockArray(BinaryReader binaryReader)
        {
            var elementSize = Deserializer.SizeOf(typeof(VehicleSuspensionBlock));
            var blamPointer = binaryReader.ReadBlamPointer(elementSize);
            var array = new VehicleSuspensionBlock[blamPointer.elementCount];
            using (binaryReader.BaseStream.Pin())
            {
                for (int i = 0; i < blamPointer.elementCount; ++i)
                {
                    binaryReader.BaseStream.Position = blamPointer[i];
                    array[i] = new VehicleSuspensionBlock(binaryReader);
                }
            }
            return array;
        }
        internal  virtual ObjectAnimationBlock[] ReadObjectAnimationBlockArray(BinaryReader binaryReader)
        {
            var elementSize = Deserializer.SizeOf(typeof(ObjectAnimationBlock));
            var blamPointer = binaryReader.ReadBlamPointer(elementSize);
            var array = new ObjectAnimationBlock[blamPointer.elementCount];
            using (binaryReader.BaseStream.Pin())
            {
                for (int i = 0; i < blamPointer.elementCount; ++i)
                {
                    binaryReader.BaseStream.Position = blamPointer[i];
                    array[i] = new ObjectAnimationBlock(binaryReader);
                }
            }
            return array;
        }
    };
}