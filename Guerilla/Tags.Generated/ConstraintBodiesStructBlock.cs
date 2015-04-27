// ReSharper disable All

using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class ConstraintBodiesStructBlock : ConstraintBodiesStructBlockBase
    {
        public ConstraintBodiesStructBlock( BinaryReader binaryReader ) : base( binaryReader )
        {
        }
    };

    [LayoutAttribute( Size = 116, Alignment = 4 )]
    public class ConstraintBodiesStructBlockBase : IGuerilla
    {
        internal Moonfish.Tags.StringID name;
        internal Moonfish.Tags.ShortBlockIndex1 nodeA;
        internal Moonfish.Tags.ShortBlockIndex1 nodeB;
        internal float aScale;
        internal OpenTK.Vector3 aForward;
        internal OpenTK.Vector3 aLeft;
        internal OpenTK.Vector3 aUp;
        internal OpenTK.Vector3 aPosition;
        internal float bScale;
        internal OpenTK.Vector3 bForward;
        internal OpenTK.Vector3 bLeft;
        internal OpenTK.Vector3 bUp;
        internal OpenTK.Vector3 bPosition;
        internal Moonfish.Tags.ShortBlockIndex1 edgeIndex;
        internal byte[] invalidName_;

        internal ConstraintBodiesStructBlockBase( BinaryReader binaryReader )
        {
            name = binaryReader.ReadStringID( );
            nodeA = binaryReader.ReadShortBlockIndex1( );
            nodeB = binaryReader.ReadShortBlockIndex1( );
            aScale = binaryReader.ReadSingle( );
            aForward = binaryReader.ReadVector3( );
            aLeft = binaryReader.ReadVector3( );
            aUp = binaryReader.ReadVector3( );
            aPosition = binaryReader.ReadVector3( );
            bScale = binaryReader.ReadSingle( );
            bForward = binaryReader.ReadVector3( );
            bLeft = binaryReader.ReadVector3( );
            bUp = binaryReader.ReadVector3( );
            bPosition = binaryReader.ReadVector3( );
            edgeIndex = binaryReader.ReadShortBlockIndex1( );
            invalidName_ = binaryReader.ReadBytes( 2 );
        }

        public int Write( System.IO.BinaryWriter binaryWriter, Int32 nextAddress )
        {
            using ( binaryWriter.BaseStream.Pin( ) )
            {
                binaryWriter.Write( name );
                binaryWriter.Write( nodeA );
                binaryWriter.Write( nodeB );
                binaryWriter.Write( aScale );
                binaryWriter.Write( aForward );
                binaryWriter.Write( aLeft );
                binaryWriter.Write( aUp );
                binaryWriter.Write( aPosition );
                binaryWriter.Write( bScale );
                binaryWriter.Write( bForward );
                binaryWriter.Write( bLeft );
                binaryWriter.Write( bUp );
                binaryWriter.Write( bPosition );
                binaryWriter.Write( edgeIndex );
                binaryWriter.Write( invalidName_, 0, 2 );
                return nextAddress;
            }
        }
    };
}