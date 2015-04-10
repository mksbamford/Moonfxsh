// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class MultiplayerUiBlock : MultiplayerUiBlockBase
    {
        public  MultiplayerUiBlock(System.IO.BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 32)]
    public class MultiplayerUiBlockBase
    {
        [TagReference("unic")]
        internal Moonfish.Tags.TagReference randomPlayerNames;
        internal MultiplayerColorBlock[] obsoleteProfileColors;
        internal MultiplayerColorBlock[] teamColors;
        [TagReference("unic")]
        internal Moonfish.Tags.TagReference teamNames;
        internal  MultiplayerUiBlockBase(System.IO.BinaryReader binaryReader)
        {
            randomPlayerNames = binaryReader.ReadTagReference();
            ReadMultiplayerColorBlockArray(binaryReader);
            ReadMultiplayerColorBlockArray(binaryReader);
            teamNames = binaryReader.ReadTagReference();
        }
        internal  virtual byte[] ReadData(System.IO.BinaryReader binaryReader)
        {
            var blamPointer = binaryReader.ReadBlamPointer(1);
            var data = new byte[blamPointer.Count];
            if(blamPointer.Count > 0)
            {
                using (binaryReader.BaseStream.Pin())
                {
                    binaryReader.BaseStream.Position = blamPointer[0];
                    data = binaryReader.ReadBytes(blamPointer.Count);
                }
            }
            return data;
        }
        internal  virtual MultiplayerColorBlock[] ReadMultiplayerColorBlockArray(System.IO.BinaryReader binaryReader)
        {
            var elementSize = Deserializer.SizeOf(typeof(MultiplayerColorBlock));
            var blamPointer = binaryReader.ReadBlamPointer(elementSize);
            var array = new MultiplayerColorBlock[blamPointer.Count];
            using (binaryReader.BaseStream.Pin())
            {
                for (int i = 0; i < blamPointer.Count; ++i)
                {
                    binaryReader.BaseStream.Position = blamPointer[i];
                    array[i] = new MultiplayerColorBlock(binaryReader);
                }
            }
            return array;
        }
        internal  virtual void WriteData(System.IO.BinaryWriter binaryWriter)
        {
            
        }
        internal  virtual void WriteMultiplayerColorBlockArray(System.IO.BinaryWriter binaryWriter)
        {
            
        }
        public void Write(System.IO.BinaryWriter binaryWriter)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(randomPlayerNames);
                WriteMultiplayerColorBlockArray(binaryWriter);
                WriteMultiplayerColorBlockArray(binaryWriter);
                binaryWriter.Write(teamNames);
            }
        }
    };
}
