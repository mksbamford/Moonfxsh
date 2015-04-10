using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class UiLevelsDefinitionBlock : UiLevelsDefinitionBlockBase
    {
        public  UiLevelsDefinitionBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 24)]
    public class UiLevelsDefinitionBlockBase
    {
        internal UiCampaignBlock[] campaigns;
        internal GlobalUiCampaignLevelBlock[] campaignLevels;
        internal GlobalUiMultiplayerLevelBlock[] multiplayerLevels;
        internal  UiLevelsDefinitionBlockBase(BinaryReader binaryReader)
        {
            this.campaigns = ReadUiCampaignBlockArray(binaryReader);
            this.campaignLevels = ReadGlobalUiCampaignLevelBlockArray(binaryReader);
            this.multiplayerLevels = ReadGlobalUiMultiplayerLevelBlockArray(binaryReader);
        }
        internal  virtual byte[] ReadData(BinaryReader binaryReader)
        {
            var blamPointer = binaryReader.ReadBlamPointer(1);
            var data = new byte[blamPointer.count];
            if(blamPointer.count > 0)
            {
                using (binaryReader.BaseStream.Pin())
                {
                    binaryReader.BaseStream.Position = blamPointer[0];
                    data = binaryReader.ReadBytes(blamPointer.count);
                }
            }
            return data;
        }
        internal  virtual UiCampaignBlock[] ReadUiCampaignBlockArray(BinaryReader binaryReader)
        {
            var elementSize = Deserializer.SizeOf(typeof(UiCampaignBlock));
            var blamPointer = binaryReader.ReadBlamPointer(elementSize);
            var array = new UiCampaignBlock[blamPointer.count];
            using (binaryReader.BaseStream.Pin())
            {
                for (int i = 0; i < blamPointer.count; ++i)
                {
                    binaryReader.BaseStream.Position = blamPointer[i];
                    array[i] = new UiCampaignBlock(binaryReader);
                }
            }
            return array;
        }
        internal  virtual GlobalUiCampaignLevelBlock[] ReadGlobalUiCampaignLevelBlockArray(BinaryReader binaryReader)
        {
            var elementSize = Deserializer.SizeOf(typeof(GlobalUiCampaignLevelBlock));
            var blamPointer = binaryReader.ReadBlamPointer(elementSize);
            var array = new GlobalUiCampaignLevelBlock[blamPointer.count];
            using (binaryReader.BaseStream.Pin())
            {
                for (int i = 0; i < blamPointer.count; ++i)
                {
                    binaryReader.BaseStream.Position = blamPointer[i];
                    array[i] = new GlobalUiCampaignLevelBlock(binaryReader);
                }
            }
            return array;
        }
        internal  virtual GlobalUiMultiplayerLevelBlock[] ReadGlobalUiMultiplayerLevelBlockArray(BinaryReader binaryReader)
        {
            var elementSize = Deserializer.SizeOf(typeof(GlobalUiMultiplayerLevelBlock));
            var blamPointer = binaryReader.ReadBlamPointer(elementSize);
            var array = new GlobalUiMultiplayerLevelBlock[blamPointer.count];
            using (binaryReader.BaseStream.Pin())
            {
                for (int i = 0; i < blamPointer.count; ++i)
                {
                    binaryReader.BaseStream.Position = blamPointer[i];
                    array[i] = new GlobalUiMultiplayerLevelBlock(binaryReader);
                }
            }
            return array;
        }
    };
}
