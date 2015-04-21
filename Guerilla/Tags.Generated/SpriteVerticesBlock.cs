// ReSharper disable All

using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class SpriteVerticesBlock : SpriteVerticesBlockBase
    {
        public SpriteVerticesBlock( BinaryReader binaryReader ) : base( binaryReader )
        {
        }
    };

    [LayoutAttribute( Size = 47, Alignment = 4 )]
    public class SpriteVerticesBlockBase : IGuerilla
    {
        internal OpenTK.Vector3 position;
        internal OpenTK.Vector3 offset;
        internal OpenTK.Vector3 axis;
        internal OpenTK.Vector2 texcoord;
        internal Moonfish.Tags.RGBColor color;

        internal SpriteVerticesBlockBase( BinaryReader binaryReader )
        {
            position = binaryReader.ReadVector3( );
            offset = binaryReader.ReadVector3( );
            axis = binaryReader.ReadVector3( );
            texcoord = binaryReader.ReadVector2( );
            color = binaryReader.ReadRGBColor( );
        }

        public int Write( System.IO.BinaryWriter binaryWriter, Int32 nextAddress )
        {
            using ( binaryWriter.BaseStream.Pin( ) )
            {
                binaryWriter.Write( position );
                binaryWriter.Write( offset );
                binaryWriter.Write( axis );
                binaryWriter.Write( texcoord );
                binaryWriter.Write( color );
                return nextAddress;
            }
        }
    };
}