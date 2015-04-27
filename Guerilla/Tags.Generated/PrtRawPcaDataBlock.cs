// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class PrtRawPcaDataBlock : PrtRawPcaDataBlockBase
    {
        public  PrtRawPcaDataBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  PrtRawPcaDataBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 4, Alignment = 4)]
    public class PrtRawPcaDataBlockBase : GuerillaBlock
    {
        internal float rawPcaData;
        
        public override int SerializedSize{get { return 4; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  PrtRawPcaDataBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            rawPcaData = binaryReader.ReadSingle();
        }
        public  PrtRawPcaDataBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(rawPcaData);
                return nextAddress;
            }
        }
    };
}
