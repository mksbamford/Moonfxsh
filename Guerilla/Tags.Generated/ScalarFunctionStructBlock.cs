// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ScalarFunctionStructBlock : ScalarFunctionStructBlockBase
    {
        public  ScalarFunctionStructBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
        public  ScalarFunctionStructBlock(): base()
        {
            
        }
    };
    [LayoutAttribute(Size = 8, Alignment = 4)]
    public class ScalarFunctionStructBlockBase : GuerillaBlock
    {
        internal MappingFunctionBlock function;
        
        public override int SerializedSize{get { return 8; }}
        
        
        public override int Alignment{get { return 4; }}
        
        public  ScalarFunctionStructBlockBase(BinaryReader binaryReader): base(binaryReader)
        {
            function = new MappingFunctionBlock(binaryReader);
        }
        public  ScalarFunctionStructBlockBase(): base()
        {
            
        }
        public void Read(BinaryReader binaryReader)
        {
            function = new MappingFunctionBlock(binaryReader);
        }
        public override int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                function.Write(binaryWriter);
                return nextAddress;
            }
        }
    };
}
