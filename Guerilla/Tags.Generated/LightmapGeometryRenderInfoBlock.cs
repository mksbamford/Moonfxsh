// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class LightmapGeometryRenderInfoBlock : LightmapGeometryRenderInfoBlockBase
    {
        public  LightmapGeometryRenderInfoBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  LightmapGeometryRenderInfoBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 4, Alignment = 4)]
    public class LightmapGeometryRenderInfoBlockBase : GuerillaBlock
    {
        internal short bitmapIndex;
        internal byte paletteIndex;
        internal byte[] invalidName_;
        
        public override int SerializedSize{get { return 4; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  LightmapGeometryRenderInfoBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            bitmapIndex = binaryReader.ReadInt16();
            paletteIndex = binaryReader.ReadByte();
            invalidName_ = binaryReader.ReadBytes(1);
        }
        public  LightmapGeometryRenderInfoBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            bitmapIndex = binaryReader.ReadInt16();
            paletteIndex = binaryReader.ReadByte();
            invalidName_ = binaryReader.ReadBytes(1);
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(bitmapIndex);
                binaryWriter.Write(paletteIndex);
                binaryWriter.Write(invalidName_, 0, 1);
                return nextAddress;
            }
        }
    };
}
