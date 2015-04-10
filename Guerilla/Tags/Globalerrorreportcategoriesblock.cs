using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class GlobalErrorReportCategoriesBlock : GlobalErrorReportCategoriesBlockBase
    {
        public  GlobalErrorReportCategoriesBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 676)]
    public class GlobalErrorReportCategoriesBlockBase
    {
        internal Moonfish.Tags.String256 name;
        internal ReportType reportType;
        internal Flags flags;
        internal byte[] invalidName_;
        internal byte[] invalidName_0;
        internal byte[] invalidName_1;
        internal ErrorReportsBlock[] reports;
        internal  GlobalErrorReportCategoriesBlockBase(BinaryReader binaryReader)
        {
            this.name = binaryReader.ReadString256();
            this.reportType = (ReportType)binaryReader.ReadInt16();
            this.flags = (Flags)binaryReader.ReadInt16();
            this.invalidName_ = binaryReader.ReadBytes(2);
            this.invalidName_0 = binaryReader.ReadBytes(2);
            this.invalidName_1 = binaryReader.ReadBytes(404);
            this.reports = ReadErrorReportsBlockArray(binaryReader);
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
        internal  virtual ErrorReportsBlock[] ReadErrorReportsBlockArray(BinaryReader binaryReader)
        {
            var elementSize = Deserializer.SizeOf(typeof(ErrorReportsBlock));
            var blamPointer = binaryReader.ReadBlamPointer(elementSize);
            var array = new ErrorReportsBlock[blamPointer.count];
            using (binaryReader.BaseStream.Pin())
            {
                for (int i = 0; i < blamPointer.count; ++i)
                {
                    binaryReader.BaseStream.Position = blamPointer[i];
                    array[i] = new ErrorReportsBlock(binaryReader);
                }
            }
            return array;
        }
        internal enum ReportType : short
        
        {
            Silent = 0,
            Comment = 1,
            Warning = 2,
            Error = 3,
        };
        [FlagsAttribute]
        internal enum Flags : short
        
        {
            Rendered = 1,
            TangentSpace = 2,
            Noncritical = 4,
            LightmapLight = 8,
            ReportKeyIsValid = 16,
        };
    };
}
