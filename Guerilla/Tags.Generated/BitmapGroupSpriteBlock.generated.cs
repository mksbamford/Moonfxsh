//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Moonfish.Guerilla.Tags
{
    using Moonfish.Tags;
    using Moonfish.Model;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    
    public partial class BitmapGroupSpriteBlock : GuerillaBlock, IWriteQueueable
    {
        public short BitmapIndex;
        private byte[] fieldpad = new byte[2];
        private byte[] fieldpad0 = new byte[4];
        public float Left;
        public float Right;
        public float Top;
        public float Bottom;
        public OpenTK.Vector2 RegistrationPoint;
        public override int SerializedSize
        {
            get
            {
                return 32;
            }
        }
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        public override System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> ReadFields(System.IO.BinaryReader binaryReader)
        {
            System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue = new System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer>(base.ReadFields(binaryReader));
            this.BitmapIndex = binaryReader.ReadInt16();
            this.fieldpad = binaryReader.ReadBytes(2);
            this.fieldpad0 = binaryReader.ReadBytes(4);
            this.Left = binaryReader.ReadSingle();
            this.Right = binaryReader.ReadSingle();
            this.Top = binaryReader.ReadSingle();
            this.Bottom = binaryReader.ReadSingle();
            this.RegistrationPoint = binaryReader.ReadVector2();
            return pointerQueue;
        }
        public override void ReadInstances(System.IO.BinaryReader binaryReader, System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue)
        {
            base.ReadInstances(binaryReader, pointerQueue);
        }
        public override void QueueWrites(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.QueueWrites(queueableBinaryWriter);
        }
        public override void Write_(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.Write_(queueableBinaryWriter);
            queueableBinaryWriter.Write(this.BitmapIndex);
            queueableBinaryWriter.Write(this.fieldpad);
            queueableBinaryWriter.Write(this.fieldpad0);
            queueableBinaryWriter.Write(this.Left);
            queueableBinaryWriter.Write(this.Right);
            queueableBinaryWriter.Write(this.Top);
            queueableBinaryWriter.Write(this.Bottom);
            queueableBinaryWriter.Write(this.RegistrationPoint);
        }
    }
}