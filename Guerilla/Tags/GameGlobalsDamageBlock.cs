using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class GameGlobalsDamageBlock : GameGlobalsDamageBlockBase
    {
        public  GameGlobalsDamageBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 8)]
    public class GameGlobalsDamageBlockBase
    {
        internal DamageGroupBlock[] damageGroups;
        internal  GameGlobalsDamageBlockBase(BinaryReader binaryReader)
        {
            this.damageGroups = ReadDamageGroupBlockArray(binaryReader);
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
        internal  virtual DamageGroupBlock[] ReadDamageGroupBlockArray(BinaryReader binaryReader)
        {
            var elementSize = Deserializer.SizeOf(typeof(DamageGroupBlock));
            var blamPointer = binaryReader.ReadBlamPointer(elementSize);
            var array = new DamageGroupBlock[blamPointer.count];
            using (binaryReader.BaseStream.Pin())
            {
                for (int i = 0; i < blamPointer.count; ++i)
                {
                    binaryReader.BaseStream.Position = blamPointer[i];
                    array[i] = new DamageGroupBlock(binaryReader);
                }
            }
            return array;
        }
    };
}
