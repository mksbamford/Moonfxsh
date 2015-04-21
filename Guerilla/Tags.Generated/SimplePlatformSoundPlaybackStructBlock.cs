// ReSharper disable All

using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Guerilla.Tags
{
    public partial class SimplePlatformSoundPlaybackStructBlock : SimplePlatformSoundPlaybackStructBlockBase
    {
        public SimplePlatformSoundPlaybackStructBlock( BinaryReader binaryReader ) : base( binaryReader )
        {
        }
    };

    [LayoutAttribute( Size = 52, Alignment = 4 )]
    public class SimplePlatformSoundPlaybackStructBlockBase : IGuerilla
    {
        internal PlatformSoundOverrideMixbinsBlock[] platformSoundOverrideMixbinsBlock;
        internal Flags flags;
        internal byte[] invalidName_;
        internal PlatformSoundFilterBlock[] filter;
        internal PlatformSoundPitchLfoBlock[] pitchLfo;
        internal PlatformSoundFilterLfoBlock[] filterLfo;
        internal SoundEffectPlaybackBlock[] soundEffect;

        internal SimplePlatformSoundPlaybackStructBlockBase( BinaryReader binaryReader )
        {
            platformSoundOverrideMixbinsBlock = Guerilla.ReadBlockArray<PlatformSoundOverrideMixbinsBlock>( binaryReader );
            flags = ( Flags ) binaryReader.ReadInt32( );
            invalidName_ = binaryReader.ReadBytes( 8 );
            filter = Guerilla.ReadBlockArray<PlatformSoundFilterBlock>( binaryReader );
            pitchLfo = Guerilla.ReadBlockArray<PlatformSoundPitchLfoBlock>( binaryReader );
            filterLfo = Guerilla.ReadBlockArray<PlatformSoundFilterLfoBlock>( binaryReader );
            soundEffect = Guerilla.ReadBlockArray<SoundEffectPlaybackBlock>( binaryReader );
        }

        public int Write( System.IO.BinaryWriter binaryWriter, Int32 nextAddress )
        {
            using ( binaryWriter.BaseStream.Pin( ) )
            {
                nextAddress = Guerilla.WriteBlockArray<PlatformSoundOverrideMixbinsBlock>( binaryWriter,
                    platformSoundOverrideMixbinsBlock, nextAddress );
                binaryWriter.Write( ( Int32 ) flags );
                binaryWriter.Write( invalidName_, 0, 8 );
                nextAddress = Guerilla.WriteBlockArray<PlatformSoundFilterBlock>( binaryWriter, filter, nextAddress );
                nextAddress = Guerilla.WriteBlockArray<PlatformSoundPitchLfoBlock>( binaryWriter, pitchLfo, nextAddress );
                nextAddress = Guerilla.WriteBlockArray<PlatformSoundFilterLfoBlock>( binaryWriter, filterLfo,
                    nextAddress );
                nextAddress = Guerilla.WriteBlockArray<SoundEffectPlaybackBlock>( binaryWriter, soundEffect, nextAddress );
                return nextAddress;
            }
        }

        [FlagsAttribute]
        internal enum Flags : int
        {
            Use3DRadioHack = 1,
        };
    };
}