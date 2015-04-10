// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class PlatformSoundOverrideMixbinsBlock : PlatformSoundOverrideMixbinsBlockBase
    {
        public  PlatformSoundOverrideMixbinsBlock(System.IO.BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 8)]
    public class PlatformSoundOverrideMixbinsBlockBase
    {
        internal Mixbin mixbin;
        internal float gainDB;
        internal  PlatformSoundOverrideMixbinsBlockBase(System.IO.BinaryReader binaryReader)
        {
            mixbin = (Mixbin)binaryReader.ReadInt32();
            gainDB = binaryReader.ReadSingle();
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
        internal  virtual void WriteData(System.IO.BinaryWriter binaryWriter)
        {
            
        }
        public void Write(System.IO.BinaryWriter binaryWriter)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write((Int32)mixbin);
                binaryWriter.Write(gainDB);
            }
        }
        internal enum Mixbin : int
        
        {
            FrontLeft = 0,
            FrontRight = 1,
            BackLeft = 2,
            BackRight = 3,
            Center = 4,
            LowFrequency = 5,
            Reverb = 6,
            InvalidName3DFrontLeft = 7,
            InvalidName3DFrontRight = 8,
            InvalidName3DBackLeft = 9,
            InvalidName3DBackRight = 10,
            DefaultLeftSpeakers = 11,
            DefaultRightSpeakers = 12,
        };
    };
}
