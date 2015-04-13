using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    [TagClassAttribute("bsdt")]
    public  partial class BreakableSurfaceBlock : BreakableSurfaceBlockBase
    {
        public  BreakableSurfaceBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 32)]
    public class BreakableSurfaceBlockBase
    {
        internal float maximumVitality;
        [TagReference("effe")]
        internal Moonfish.Tags.TagReference effect;
        [TagReference("snd!")]
        internal Moonfish.Tags.TagReference sound;
        internal ParticleSystemDefinitionBlockNew[] particleEffects;
        internal float particleDensity;
        internal  BreakableSurfaceBlockBase(BinaryReader binaryReader)
        {
            this.maximumVitality = binaryReader.ReadSingle();
            this.effect = binaryReader.ReadTagReference();
            this.sound = binaryReader.ReadTagReference();
            this.particleEffects = ReadParticleSystemDefinitionBlockNewArray(binaryReader);
            this.particleDensity = binaryReader.ReadSingle();
        }
        internal  virtual byte[] ReadData(BinaryReader binaryReader)
        {
            var blamPointer = binaryReader.ReadBlamPointer(1);
            var data = new byte[blamPointer.elementCount];
            if(blamPointer.elementCount > 0)
            {
                using (binaryReader.BaseStream.Pin())
                {
                    binaryReader.BaseStream.Position = blamPointer[0];
                    data = binaryReader.ReadBytes(blamPointer.elementCount);
                }
            }
            return data;
        }
        internal  virtual ParticleSystemDefinitionBlockNew[] ReadParticleSystemDefinitionBlockNewArray(BinaryReader binaryReader)
        {
            var elementSize = Deserializer.SizeOf(typeof(ParticleSystemDefinitionBlockNew));
            var blamPointer = binaryReader.ReadBlamPointer(elementSize);
            var array = new ParticleSystemDefinitionBlockNew[blamPointer.elementCount];
            using (binaryReader.BaseStream.Pin())
            {
                for (int i = 0; i < blamPointer.elementCount; ++i)
                {
                    binaryReader.BaseStream.Position = blamPointer[i];
                    array[i] = new ParticleSystemDefinitionBlockNew(binaryReader);
                }
            }
            return array;
        }
    };
}