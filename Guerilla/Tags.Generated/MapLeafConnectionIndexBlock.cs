// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class MapLeafConnectionIndexBlock : MapLeafConnectionIndexBlockBase
    {
        public  MapLeafConnectionIndexBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  MapLeafConnectionIndexBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 4, Alignment = 4)]
    public class MapLeafConnectionIndexBlockBase : GuerillaBlock
    {
        internal int connectionIndex;
        
        public override int SerializedSize{get { return 4; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  MapLeafConnectionIndexBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            connectionIndex = binaryReader.ReadInt32();
        }
        public  MapLeafConnectionIndexBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(connectionIndex);
                return nextAddress;
            }
        }
    };
}
