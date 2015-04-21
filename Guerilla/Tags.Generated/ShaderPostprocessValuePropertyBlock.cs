// ReSharper disable All

using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ShaderPostprocessValuePropertyBlock : ShaderPostprocessValuePropertyBlockBase
    {
        public ShaderPostprocessValuePropertyBlock( BinaryReader binaryReader ) : base( binaryReader )
        {
        }
    };

    [LayoutAttribute( Size = 4, Alignment = 4 )]
    public class ShaderPostprocessValuePropertyBlockBase : IGuerilla
    {
        internal float value;

        internal ShaderPostprocessValuePropertyBlockBase( BinaryReader binaryReader )
        {
            value = binaryReader.ReadSingle( );
        }

        public int Write( System.IO.BinaryWriter binaryWriter, Int32 nextAddress )
        {
            using ( binaryWriter.BaseStream.Pin( ) )
            {
                binaryWriter.Write( value );
                return nextAddress;
            }
        }
    };
}