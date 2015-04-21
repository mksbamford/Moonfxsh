// ReSharper disable All

using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ShaderPassPostprocessConstantInfoNewBlock : ShaderPassPostprocessConstantInfoNewBlockBase
    {
        public ShaderPassPostprocessConstantInfoNewBlock( BinaryReader binaryReader ) : base( binaryReader )
        {
        }
    };

    [LayoutAttribute( Size = 7, Alignment = 4 )]
    public class ShaderPassPostprocessConstantInfoNewBlockBase : IGuerilla
    {
        internal Moonfish.Tags.StringID parameterName;
        internal byte[] invalidName_;

        internal ShaderPassPostprocessConstantInfoNewBlockBase( BinaryReader binaryReader )
        {
            parameterName = binaryReader.ReadStringID( );
            invalidName_ = binaryReader.ReadBytes( 3 );
        }

        public int Write( System.IO.BinaryWriter binaryWriter, Int32 nextAddress )
        {
            using ( binaryWriter.BaseStream.Pin( ) )
            {
                binaryWriter.Write( parameterName );
                binaryWriter.Write( invalidName_, 0, 3 );
                return nextAddress;
            }
        }
    };
}