// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Tags
{
    public partial struct TagClass
    {
        public static readonly TagClass Mply = (TagClass)"mply";
    };
};

namespace Moonfish.Guerilla.Tags
{
    [TagClassAttribute("mply")]
    public partial class MultiplayerScenarioDescriptionBlock : MultiplayerScenarioDescriptionBlockBase
    {
        public  MultiplayerScenarioDescriptionBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  MultiplayerScenarioDescriptionBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 8, Alignment = 4)]
    public class MultiplayerScenarioDescriptionBlockBase : GuerillaBlock
    {
        internal ScenarioDescriptionBlock[] multiplayerScenarios;
        
        public override int SerializedSize{get { return 8; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  MultiplayerScenarioDescriptionBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            multiplayerScenarios = Guerilla.ReadBlockArray<ScenarioDescriptionBlock>(binaryReader);
        }
        public  MultiplayerScenarioDescriptionBlockBase(): base()
        {
            
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                nextAddress = Guerilla.WriteBlockArray<ScenarioDescriptionBlock>(binaryWriter, multiplayerScenarios, nextAddress);
                return nextAddress;
            }
        }
    };
}
