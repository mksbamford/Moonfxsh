using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class CameraTrackControlPointBlock : CameraTrackControlPointBlockBase
    {
        public  CameraTrackControlPointBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 28)]
    public class CameraTrackControlPointBlockBase
    {
        internal OpenTK.Vector3 position;
        internal OpenTK.Quaternion orientation;
        internal  CameraTrackControlPointBlockBase(BinaryReader binaryReader)
        {
            this.position = binaryReader.ReadVector3();
            this.orientation = binaryReader.ReadQuaternion();
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
    };
}
