// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class RenderLightingStructBlock : RenderLightingStructBlockBase
    {
        public  RenderLightingStructBlock(System.IO.BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 84)]
    public class RenderLightingStructBlockBase
    {
        internal Moonfish.Tags.ColorR8G8B8 ambient;
        internal OpenTK.Vector3 shadowDirection;
        internal float lightingAccuracy;
        internal float shadowOpacity;
        internal Moonfish.Tags.ColorR8G8B8 primaryDirectionColor;
        internal OpenTK.Vector3 primaryDirection;
        internal Moonfish.Tags.ColorR8G8B8 secondaryDirectionColor;
        internal OpenTK.Vector3 secondaryDirection;
        internal short shIndex;
        internal byte[] invalidName_;
        internal  RenderLightingStructBlockBase(System.IO.BinaryReader binaryReader)
        {
            ambient = binaryReader.ReadColorR8G8B8();
            shadowDirection = binaryReader.ReadVector3();
            lightingAccuracy = binaryReader.ReadSingle();
            shadowOpacity = binaryReader.ReadSingle();
            primaryDirectionColor = binaryReader.ReadColorR8G8B8();
            primaryDirection = binaryReader.ReadVector3();
            secondaryDirectionColor = binaryReader.ReadColorR8G8B8();
            secondaryDirection = binaryReader.ReadVector3();
            shIndex = binaryReader.ReadInt16();
            invalidName_ = binaryReader.ReadBytes(2);
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
                binaryWriter.Write(ambient);
                binaryWriter.Write(shadowDirection);
                binaryWriter.Write(lightingAccuracy);
                binaryWriter.Write(shadowOpacity);
                binaryWriter.Write(primaryDirectionColor);
                binaryWriter.Write(primaryDirection);
                binaryWriter.Write(secondaryDirectionColor);
                binaryWriter.Write(secondaryDirection);
                binaryWriter.Write(shIndex);
                binaryWriter.Write(invalidName_, 0, 2);
            }
        }
    };
}
