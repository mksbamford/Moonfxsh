// ReSharper disable All

using Moonfish.Model;
using Moonfish.Tags.BlamExtension;
using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;

namespace Moonfish.Tags
{
    public partial struct TagClass
    {
        public static readonly TagClass Qip = ( TagClass ) "*qip";
    };
} ;

namespace Moonfish.Guerilla.Tags
{
    [TagClassAttribute( "*qip" )]
    public partial class ScenarioEquipmentResourceBlock : ScenarioEquipmentResourceBlockBase
    {
        public ScenarioEquipmentResourceBlock( BinaryReader binaryReader ) : base( binaryReader )
        {
        }
    };

    [LayoutAttribute( Size = 52, Alignment = 4 )]
    public class ScenarioEquipmentResourceBlockBase : IGuerilla
    {
        internal ScenarioObjectNamesBlock[] names;
        internal DontUseMeScenarioEnvironmentObjectBlock[] invalidName_;
        internal ScenarioStructureBspReferenceBlock[] structureReferences;
        internal ScenarioEquipmentPaletteBlock[] palette;
        internal ScenarioEquipmentBlock[] objects;
        internal int nextObjectIDSalt;
        internal GScenarioEditorFolderBlock[] editorFolders;

        internal ScenarioEquipmentResourceBlockBase( BinaryReader binaryReader )
        {
            names = Guerilla.ReadBlockArray<ScenarioObjectNamesBlock>( binaryReader );
            invalidName_ = Guerilla.ReadBlockArray<DontUseMeScenarioEnvironmentObjectBlock>( binaryReader );
            structureReferences = Guerilla.ReadBlockArray<ScenarioStructureBspReferenceBlock>( binaryReader );
            palette = Guerilla.ReadBlockArray<ScenarioEquipmentPaletteBlock>( binaryReader );
            objects = Guerilla.ReadBlockArray<ScenarioEquipmentBlock>( binaryReader );
            nextObjectIDSalt = binaryReader.ReadInt32( );
            editorFolders = Guerilla.ReadBlockArray<GScenarioEditorFolderBlock>( binaryReader );
        }

        public int Write( System.IO.BinaryWriter binaryWriter, Int32 nextAddress )
        {
            using ( binaryWriter.BaseStream.Pin( ) )
            {
                nextAddress = Guerilla.WriteBlockArray<ScenarioObjectNamesBlock>( binaryWriter, names, nextAddress );
                nextAddress = Guerilla.WriteBlockArray<DontUseMeScenarioEnvironmentObjectBlock>( binaryWriter,
                    invalidName_, nextAddress );
                nextAddress = Guerilla.WriteBlockArray<ScenarioStructureBspReferenceBlock>( binaryWriter,
                    structureReferences, nextAddress );
                nextAddress = Guerilla.WriteBlockArray<ScenarioEquipmentPaletteBlock>( binaryWriter, palette,
                    nextAddress );
                nextAddress = Guerilla.WriteBlockArray<ScenarioEquipmentBlock>( binaryWriter, objects, nextAddress );
                binaryWriter.Write( nextObjectIDSalt );
                nextAddress = Guerilla.WriteBlockArray<GScenarioEditorFolderBlock>( binaryWriter, editorFolders,
                    nextAddress );
                return nextAddress;
            }
        }
    };
}