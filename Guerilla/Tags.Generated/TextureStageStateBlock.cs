// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class TextureStageStateBlock : TextureStageStateBlockBase
    {
        public  TextureStageStateBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  TextureStageStateBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 6, Alignment = 4)]
    public class TextureStageStateBlockBase : GuerillaBlock
    {
        internal byte stateIndex;
        internal byte stageIndex;
        internal int stateValue;
        
        public override int SerializedSize{get { return 6; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  TextureStageStateBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            stateIndex = binaryReader.ReadByte();
            stageIndex = binaryReader.ReadByte();
            stateValue = binaryReader.ReadInt32();
        }
        public  TextureStageStateBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            stateIndex = binaryReader.ReadByte();
            stageIndex = binaryReader.ReadByte();
            stateValue = binaryReader.ReadInt32();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(stateIndex);
                binaryWriter.Write(stageIndex);
                binaryWriter.Write(stateValue);
                return nextAddress;
            }
        }
    };
}
