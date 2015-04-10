using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class StructureBspClusterPortalIndexBlock : StructureBspClusterPortalIndexBlockBase
    {
        public  StructureBspClusterPortalIndexBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 2)]
    public class StructureBspClusterPortalIndexBlockBase
    {
        internal short portalIndex;
        internal  StructureBspClusterPortalIndexBlockBase(BinaryReader binaryReader)
        {
            this.portalIndex = binaryReader.ReadInt16();
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
