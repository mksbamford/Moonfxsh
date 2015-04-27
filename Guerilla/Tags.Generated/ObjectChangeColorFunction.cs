// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ObjectChangeColorFunction : ObjectChangeColorFunctionBase
    {
        public  ObjectChangeColorFunction(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  ObjectChangeColorFunction(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 40, Alignment = 4)]
    public class ObjectChangeColorFunctionBase : GuerillaBlock
    {
        internal byte[] invalidName_;
        internal ScaleFlags scaleFlags;
        internal Moonfish.Tags.ColorR8G8B8 colorLowerBound;
        internal Moonfish.Tags.ColorR8G8B8 colorUpperBound;
        internal Moonfish.Tags.StringID darkenBy;
        internal Moonfish.Tags.StringID scaleBy;
        
        public override int SerializedSize{get { return 40; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  ObjectChangeColorFunctionBase(BinaryReader binaryReader): base(binaryReader)
        {
            invalidName_ = binaryReader.ReadBytes(4);
            scaleFlags = (ScaleFlags)binaryReader.ReadInt32();
            colorLowerBound = binaryReader.ReadColorR8G8B8();
            colorUpperBound = binaryReader.ReadColorR8G8B8();
            darkenBy = binaryReader.ReadStringID();
            scaleBy = binaryReader.ReadStringID();
        }
        public  ObjectChangeColorFunctionBase(): base()
        {
            
        }
        public override void Read(BinaryReader binaryReader)
        {
            invalidName_ = binaryReader.ReadBytes(4);
            scaleFlags = (ScaleFlags)binaryReader.ReadInt32();
            colorLowerBound = binaryReader.ReadColorR8G8B8();
            colorUpperBound = binaryReader.ReadColorR8G8B8();
            darkenBy = binaryReader.ReadStringID();
            scaleBy = binaryReader.ReadStringID();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(invalidName_, 0, 4);
                binaryWriter.Write((Int32)scaleFlags);
                binaryWriter.Write(colorLowerBound);
                binaryWriter.Write(colorUpperBound);
                binaryWriter.Write(darkenBy);
                binaryWriter.Write(scaleBy);
                return nextAddress;
            }
        }
        [FlagsAttribute]
        internal enum ScaleFlags : int
        {
            BlendInHsvBlendsColorsInHsvRatherThanRgbSpace = 1,
            MoreColorsBlendsColorsThroughMoreHuesGoesTheLongWayAroundTheColorWheel = 2,
        };
    };
}
