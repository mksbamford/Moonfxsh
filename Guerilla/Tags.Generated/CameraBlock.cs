// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class CameraBlock : CameraBlockBase
    {
        public  CameraBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  CameraBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 20, Alignment = 4)]
    public class CameraBlockBase : GuerillaBlock
    {
        [TagReference("trak")]
        internal Moonfish.Tags.TagReference defaultUnitCameraTrack;
        internal float defaultChangePause;
        internal float firstPersonChangePause;
        internal float followingCameraChangePause;
        
        public override int SerializedSize{get { return 20; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  CameraBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            defaultUnitCameraTrack = binaryReader.ReadTagReference();
            defaultChangePause = binaryReader.ReadSingle();
            firstPersonChangePause = binaryReader.ReadSingle();
            followingCameraChangePause = binaryReader.ReadSingle();
        }
        public  CameraBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            defaultUnitCameraTrack = binaryReader.ReadTagReference();
            defaultChangePause = binaryReader.ReadSingle();
            firstPersonChangePause = binaryReader.ReadSingle();
            followingCameraChangePause = binaryReader.ReadSingle();
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(defaultUnitCameraTrack);
                binaryWriter.Write(defaultChangePause);
                binaryWriter.Write(firstPersonChangePause);
                binaryWriter.Write(followingCameraChangePause);
                return nextAddress;
            }
        }
    };
}
