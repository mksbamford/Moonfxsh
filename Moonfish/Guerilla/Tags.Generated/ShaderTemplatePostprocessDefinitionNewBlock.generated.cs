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
    
    public partial class ShaderTemplatePostprocessDefinitionNewBlock : GuerillaBlock, IWriteQueueable
    {
        public ShaderTemplatePostprocessLevelOfDetailNewBlock[] LevelsOfDetail = new ShaderTemplatePostprocessLevelOfDetailNewBlock[0];
        public TagBlockIndexBlock[] Layers = new TagBlockIndexBlock[0];
        public ShaderTemplatePostprocessPassNewBlock[] Passes = new ShaderTemplatePostprocessPassNewBlock[0];
        public ShaderTemplatePostprocessImplementationNewBlock[] Implementations = new ShaderTemplatePostprocessImplementationNewBlock[0];
        public ShaderTemplatePostprocessRemappingNewBlock[] Remappings = new ShaderTemplatePostprocessRemappingNewBlock[0];
        public override int SerializedSize
        {
            get
            {
                return 40;
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
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(10));
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(2));
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(10));
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(6));
            pointerQueue.Enqueue(binaryReader.ReadBlamPointer(4));
            return pointerQueue;
        }
        public override void ReadInstances(System.IO.BinaryReader binaryReader, System.Collections.Generic.Queue<Moonfish.Tags.BlamPointer> pointerQueue)
        {
            base.ReadInstances(binaryReader, pointerQueue);
            this.LevelsOfDetail = base.ReadBlockArrayData<ShaderTemplatePostprocessLevelOfDetailNewBlock>(binaryReader, pointerQueue.Dequeue());
            this.Layers = base.ReadBlockArrayData<TagBlockIndexBlock>(binaryReader, pointerQueue.Dequeue());
            this.Passes = base.ReadBlockArrayData<ShaderTemplatePostprocessPassNewBlock>(binaryReader, pointerQueue.Dequeue());
            this.Implementations = base.ReadBlockArrayData<ShaderTemplatePostprocessImplementationNewBlock>(binaryReader, pointerQueue.Dequeue());
            this.Remappings = base.ReadBlockArrayData<ShaderTemplatePostprocessRemappingNewBlock>(binaryReader, pointerQueue.Dequeue());
        }
        public override void QueueWrites(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.QueueWrites(queueableBinaryWriter);
            queueableBinaryWriter.QueueWrite(this.LevelsOfDetail);
            queueableBinaryWriter.QueueWrite(this.Layers);
            queueableBinaryWriter.QueueWrite(this.Passes);
            queueableBinaryWriter.QueueWrite(this.Implementations);
            queueableBinaryWriter.QueueWrite(this.Remappings);
        }
        public override void Write_(Moonfish.Guerilla.QueueableBinaryWriter queueableBinaryWriter)
        {
            base.Write_(queueableBinaryWriter);
            queueableBinaryWriter.WritePointer(this.LevelsOfDetail);
            queueableBinaryWriter.WritePointer(this.Layers);
            queueableBinaryWriter.WritePointer(this.Passes);
            queueableBinaryWriter.WritePointer(this.Implementations);
            queueableBinaryWriter.WritePointer(this.Remappings);
        }
    }
}