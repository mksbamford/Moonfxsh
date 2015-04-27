// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class GDefaultVariantsBlock : GDefaultVariantsBlockBase
    {
        public  GDefaultVariantsBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  GDefaultVariantsBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 20, Alignment = 4)]
    public class GDefaultVariantsBlockBase : GuerillaBlock
    {
        internal Moonfish.Tags.StringID variantName;
        internal VariantType variantType;
        internal GDefaultVariantSettingsBlock[] settings;
        internal byte descriptionIndex;
        internal byte[] invalidName_;
        
        public override int SerializedSize{get { return 20; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  GDefaultVariantsBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            variantName = binaryReader.ReadStringID();
            variantType = (VariantType)binaryReader.ReadInt32();
            settings = Guerilla.ReadBlockArray<GDefaultVariantSettingsBlock>(binaryReader);
            descriptionIndex = binaryReader.ReadByte();
            invalidName_ = binaryReader.ReadBytes(3);
        }
        public  GDefaultVariantsBlockBase(): base()
        {
            
        }
        public override void Read(BinaryReader binaryReader)
        {
            variantName = binaryReader.ReadStringID();
            variantType = (VariantType)binaryReader.ReadInt32();
            settings = Guerilla.ReadBlockArray<GDefaultVariantSettingsBlock>(binaryReader);
            descriptionIndex = binaryReader.ReadByte();
            invalidName_ = binaryReader.ReadBytes(3);
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(variantName);
                binaryWriter.Write((Int32)variantType);
                nextAddress = Guerilla.WriteBlockArray<GDefaultVariantSettingsBlock>(binaryWriter, settings, nextAddress);
                binaryWriter.Write(descriptionIndex);
                binaryWriter.Write(invalidName_, 0, 3);
                return nextAddress;
            }
        }
        internal enum VariantType : int
        {
            Slayer = 0,
            Oddball = 1,
            Juggernaut = 2,
            King = 3,
            Ctf = 4,
            Invasion = 5,
            Territories = 6,
        };
    };
}
