// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ShaderTextureStateKillStateBlock : ShaderTextureStateKillStateBlockBase
    {
        public  ShaderTextureStateKillStateBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  ShaderTextureStateKillStateBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 11, Alignment = 4)]
    public class ShaderTextureStateKillStateBlockBase : GuerillaBlock
    {
        internal Flags flags;
        internal byte[] invalidName_;
        internal ColorkeyMode colorkeyMode;
        internal byte[] invalidName_0;
        internal Moonfish.Tags.RGBColor colorkeyColor;
        
        public override int SerializedSize{get { return 11; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  ShaderTextureStateKillStateBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            flags = (Flags)binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            colorkeyMode = (ColorkeyMode)binaryReader.ReadInt16();
            invalidName_0 = binaryReader.ReadBytes(2);
            colorkeyColor = binaryReader.ReadRGBColor();
        }
        public  ShaderTextureStateKillStateBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            flags = (Flags)binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
            colorkeyMode = (ColorkeyMode)binaryReader.ReadInt16();
            invalidName_0 = binaryReader.ReadBytes(2);
            colorkeyColor = binaryReader.ReadRGBColor();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int16)flags);
                binaryWriter.Write(invalidName_, 0, 2);
                binaryWriter.Write((Int16)colorkeyMode);
                binaryWriter.Write(invalidName_0, 0, 2);
                binaryWriter.Write(colorkeyColor);
                return nextAddress;
            }
        }
        [FlagsAttribute]
        internal enum Flags : short
        {
            AlphaKill = 1,
        };
        internal enum ColorkeyMode : short
        {
            Disabled = 0,
            ZeroAlpha = 1,
            ZeroARGB = 2,
            Kill = 3,
        };
    };
}
