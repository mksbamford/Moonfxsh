// ReSharper disable All
using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public  partial class UiErrorCategoryBlock : UiErrorCategoryBlockBase
    {
        public  UiErrorCategoryBlock(BinaryReader binaryReader): base(binaryReader)
        {
            
        }
    };
    [LayoutAttribute(Size = 40, Alignment = 4)]
    public class UiErrorCategoryBlockBase  : IGuerilla
    {
        internal Moonfish.Tags.StringID categoryName;
        internal Flags flags;
        internal DefaultButton defaultButton;
        internal byte[] invalidName_;
        [TagReference("unic")]
        internal Moonfish.Tags.TagReference stringTag;
        internal Moonfish.Tags.StringID defaultTitle;
        internal Moonfish.Tags.StringID defaultMessage;
        internal Moonfish.Tags.StringID defaultOk;
        internal Moonfish.Tags.StringID defaultCancel;
        internal UiErrorBlock[] errorBlock;
        internal  UiErrorCategoryBlockBase(BinaryReader binaryReader)
        {
            categoryName = binaryReader.ReadStringID();
            flags = (Flags)binaryReader.ReadInt16();
            defaultButton = (DefaultButton)binaryReader.ReadByte();
            invalidName_ = binaryReader.ReadBytes(1);
            stringTag = binaryReader.ReadTagReference();
            defaultTitle = binaryReader.ReadStringID();
            defaultMessage = binaryReader.ReadStringID();
            defaultOk = binaryReader.ReadStringID();
            defaultCancel = binaryReader.ReadStringID();
            errorBlock = Guerilla.ReadBlockArray<UiErrorBlock>(binaryReader);
        }
        public int Write(System.IO.BinaryWriter binaryWriter, Int32 nextAddress)
        {
            using(binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(categoryName);
                binaryWriter.Write((Int16)flags);
                binaryWriter.Write((Byte)defaultButton);
                binaryWriter.Write(invalidName_, 0, 1);
                binaryWriter.Write(stringTag);
                binaryWriter.Write(defaultTitle);
                binaryWriter.Write(defaultMessage);
                binaryWriter.Write(defaultOk);
                binaryWriter.Write(defaultCancel);
                Guerilla.WriteBlockArray<UiErrorBlock>(binaryWriter, errorBlock, nextAddress);
                return nextAddress = (int)binaryWriter.BaseStream.Position;
            }
        }
        [FlagsAttribute]
        internal enum Flags : short
        {
            UseLargeDialog = 1,
        };
        internal enum DefaultButton : byte
        {
            NoDefault = 0,
            DefaultOk = 1,
            DefaultCancel = 2,
        };
    };
}
