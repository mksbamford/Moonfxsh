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
    
    public partial class StructureBspSoundEnvironmentPaletteBlock : GuerillaBlock, IWriteQueueable
    {
        public Moonfish.Tags.String32 Name;
        [Moonfish.Tags.TagReferenceAttribute("snde")]
        public Moonfish.Tags.TagReference SoundEnvironment;
        public float CutoffDistance;
        public float InterpolationSpeed;
        private byte[] fieldpad = new byte[24];
        public override int SerializedSize
        {
            get
            {
                return 72;
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
            this.Name = binaryReader.ReadString32();
            this.SoundEnvironment = binaryReader.ReadTagReference();
            this.CutoffDistance = binaryReader.ReadSingle();
            this.InterpolationSpeed = binaryReader.ReadSingle();
            this.fieldpad = binaryReader.ReadBytes(24);
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
            queueableBinaryWriter.Write(this.Name);
            queueableBinaryWriter.Write(this.SoundEnvironment);
            queueableBinaryWriter.Write(this.CutoffDistance);
            queueableBinaryWriter.Write(this.InterpolationSpeed);
            queueableBinaryWriter.Write(this.fieldpad);
        }
    }
}