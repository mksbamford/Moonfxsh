﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moonfish.Tags;
using Moonfish.Tags.BlamExtension;

namespace Moonfish.Guerilla
{
    public class Validator
    {
        private delegate void Log( string message );

        private static int startOffset;
        private Func<BlamPointer, bool> IsPointerOwnedByTagDelegate;
        private Func<BlamPointer, bool> isValidDelegate;
        private List<Tuple<BlamPointer, ElementArray>> PointersList;
        private Log WriteMessage;

        private void OnWriteMessage( string message )
        {
            if ( WriteMessage != null ) WriteMessage( message );
        }

        private bool IsOwnedByTag( BlamPointer pointer )
        {
            return IsPointerOwnedByTagDelegate != null && IsPointerOwnedByTagDelegate( pointer );
        }

        public void Validate( MoonfishTagGroup validateTag, IEnumerable<MoonfishTagGroup> tagPool, string[] filenames )
        {
            PointersList = new List<Tuple<BlamPointer, ElementArray>>( );
            var stringWriter =
                File.CreateText( string.Format( @"{1}\analysis\{0}.txt",
                    validateTag.Class.ToTokenString( ), Local.MapsDirectory ) );

            WriteMessage = ( stringWriter.WriteLine );

            var offset = 0;
            var elementArray = CompileElementArray( validateTag, tagPool, offset );

            elementArray.Count = 1;

            foreach ( var file in filenames )
            {
                using ( var map = new MapStream( file ) )
                {
                    var binaryReader = new BinaryReader( map );

                    OnWriteMessage( string.Format( "Begin ({0})", map.MapName ) );

                    foreach ( var tag in map.Tags )
                    {
                        if ( !( tag.Class == validateTag.Class ) ) continue;

                        var metaTableMemory = new VirtualMappedAddress
                        {
                            Address = map.Tags[ 0 ].VirtualAddress,
                            Length = map.TagCacheLength
                        };
                        isValidDelegate = metaTableMemory.Contains;
                        var virtualTagMemory = new VirtualMappedAddress
                        {
                            Address = tag.VirtualAddress,
                            Length = tag.Length
                        };
                        IsPointerOwnedByTagDelegate = virtualTagMemory.Contains;
                        OnWriteMessage( string.Format( "Tag ({0})", tag.Path ) );

                        offset = ( int ) map.Seek( tag );
                        elementArray.VirtualAddress = map.GetTag( tag.Identifier ).VirtualAddress;
                        PointersList = new List<Tuple<BlamPointer, ElementArray>>( );
                        ValidateTagBlock( elementArray, elementArray.ToFixedArrayPointer( ), binaryReader, ref offset );
                        AnalyzePointers( PointersList );
                        stringWriter.Flush( );
                    }

                    Console.WriteLine( @"Parsed ({0})", map.MapName );
                }
            }
            stringWriter.Close( );
        }

        private void AnalyzePointers( List<Tuple<BlamPointer, ElementArray>> arrayPointerList )
        {
            var size = arrayPointerList.First( ).Item1.PointedSize;
            var nextAddress = arrayPointerList.First( ).Item1.startAddress;

            var arraySize = default( int );
            var arrayStartAddress = default( int );
            var arrayEndAddress = nextAddress;

            foreach ( var arrayPointer in arrayPointerList )
            {
                //if (arrayPointer.Item1.Address != arrayEndAddress)
                //OnWriteMessage(string.Format("{1} Hole {0}", arrayPointer.Item1.Address - arrayEndAddress, arrayPointer.Item2.ToHierarchyString()));
                arraySize = 0;
                arrayStartAddress = arrayPointer.Item1.startAddress;
                foreach ( var pointer in arrayPointer.Item1 )
                {
                    arraySize += arrayPointer.Item1.elementSize;
                }
                arrayEndAddress = arrayStartAddress + arraySize;
            }
        }

        private ElementArray CompileElementArray( MoonfishTagGroup tag, IEnumerable<MoonfishTagGroup> tags, int offset )
        {
            ElementArray elementArray;
            if ( tag.ParentClass != TagClass.Null )
            {
                var guerillaTagGroups = tags as IList<MoonfishTagGroup> ?? tags.ToList( );
                var parentClass = guerillaTagGroups.Single( x => x.Class == tag.ParentClass );
                if ( parentClass.ParentClass != TagClass.Null )
                {
                    var baseClass = guerillaTagGroups.Single( x => x.Class == parentClass.ParentClass );
                    elementArray = ProcessTagBlockDefinition( baseClass.Definition, ref offset, true );
                    elementArray.Append( ProcessTagBlockDefinition( parentClass.Definition, ref offset, true ) );
                    elementArray.Append( ProcessTagBlockDefinition( tag.Definition, ref offset, true ) );
                }
                else
                {
                    elementArray = ProcessTagBlockDefinition( parentClass.Definition, ref offset, true );
                    elementArray.Append( ProcessTagBlockDefinition( tag.Definition, ref offset, true ) );
                }
            }
            else
            {
                elementArray = ProcessTagBlockDefinition( tag.Definition, ref offset, true );
            }
            return elementArray;
        }

        private bool IsValid( BlamPointer pointer )
        {
            if ( isValidDelegate != null ) return isValidDelegate( pointer );
            return false;
        }

        private void ProcessArrayFields( IList<MoonfishTagField> fields, ElementArray elementArray,
            MoonfishTagField field, ref int i, ref int offset )
        {
            var name = field.Name;
            ++i; //move past field_type._field_array_start
            for ( var index = 0; index < field.Count; ++index )
            {
                var startindex = i;
                ProcessFields( fields, elementArray, ref startindex, ref offset );
            }
            ++i; // move past field_type._field_array_end
        }

        private void ProcessFields( IList<MoonfishTagField> fields, ElementArray elementArray, ref int i, ref int offset )
        {
            for ( ; i < fields.Count; ++i )
            {
                var field = fields[ i ];
                // Check the field type.
                switch ( field.Type )
                {
                    case MoonfishFieldType.FieldBlock:
                    {
                        var childElementArray = ProcessTagBlockDefinition( elementArray, field.Definition, ref offset );
                        elementArray.Children.Add( childElementArray );
                        break;
                    }
                    case MoonfishFieldType.FieldStruct:
                    {
                        var struct_definition = ( MoonfishTagStruct ) field.Definition;
                        var structOffset = offset;
                        var childElementArray = ProcessTagStructDefinition( elementArray, struct_definition.Definition,
                            ref structOffset );
                        elementArray.Children.AddRange( childElementArray );

                        break;
                    }
                    case MoonfishFieldType.FieldData:
                    {
                        var data_definition = ( MoonfishTagDataDefinition ) field.Definition;
                        var childElementArray = new ElementArray
                        {
                            ElementSize = 1,
                            Name = data_definition.Name,
                            Address = offset,
                            Parent = elementArray,
                            Alignment = data_definition.Alignment
                        };
                        elementArray.Children.Add( childElementArray );
                        break;
                    }
                    case MoonfishFieldType.FieldArrayStart:
                    {
                        ProcessArrayFields( fields, elementArray, field, ref i, ref offset );
                        break;
                    }
                    case MoonfishFieldType.FieldArrayEnd:
                    {
                        return;
                    }
                }
                offset += MoonfishTagDefinition.CalculateSizeOfField( field );
            }
        }

        private ElementArray ProcessTagBlockDefinition( MoonfishTagDefinition tagBlock, ref int offset,
            bool inline = false )
        {
            return ProcessTagBlockDefinition( null, tagBlock, ref offset, inline );
        }

        private ElementArray ProcessTagBlockDefinition( ElementArray parent, MoonfishTagDefinition tagBlock,
            ref int offset, bool inline = false )
        {
            var size = tagBlock.CalculateSizeOfFieldSet( );

            var blockElementArray = new ElementArray
            {
                Name = tagBlock.Name,
                ElementSize = size,
                Address = offset,
                Parent = parent,
                MaxElementCount = tagBlock.MaximumElementCount,
                Alignment = tagBlock.Alignment
            };

            var i = 0;
            var blockOffset = inline ? offset : 0;
            ProcessFields( tagBlock.Fields, blockElementArray, ref i, ref blockOffset );
            if ( inline ) offset = blockOffset;
            return blockElementArray;
        }

        private IEnumerable<ElementArray> ProcessTagStructDefinition( ElementArray parent,
            MoonfishTagDefinition definition, ref int offset )
        {
            var size = definition.CalculateSizeOfFieldSet( );

            var blockElementArray = new ElementArray
            {
                Name = definition.Name,
                ElementSize = size,
                Address = offset,
                Parent = parent,
                MaxElementCount = definition.MaximumElementCount,
                Alignment = definition.Alignment
            };

            var i = 0;
            ProcessFields( definition.Fields, blockElementArray, ref i, ref offset );
            return blockElementArray.Children;
        }

        private bool ValidateBlamPointer( BlamPointer blamPointer, ElementArray info, MapStream stream )
        {
            var stringWriter = new StringWriter( );
            if ( blamPointer.elementCount == 0 && blamPointer.startAddress == 0 ) return true;
            if ( blamPointer.elementCount == 0 ^ blamPointer.startAddress == 0 )
                stringWriter.WriteLine( "-> null-value count({0}) address({1}) is invalid", blamPointer.elementCount,
                    blamPointer.startAddress );
            if ( blamPointer.elementCount < 0 )
                stringWriter.WriteLine( "-> count({0}) is invalid", blamPointer.elementCount );
            if ( blamPointer.elementCount > info.MaxElementCount && info.MaxElementCount > 0 )
                stringWriter.WriteLine( "-> count({0}) > max-count({1})", blamPointer.elementCount, info.MaxElementCount );
            if ( !stream.ContainsPointer( blamPointer ) )
                stringWriter.WriteLine( "-> address({0}) not contained in stream({1})", blamPointer.startAddress,
                    stream.Name );

            var errors = stringWriter.ToString( );
            if ( !string.IsNullOrWhiteSpace( errors ) )
            {
                OnWriteMessage( string.Format( "Pointer ({0})\n{1}", info.Name, errors ) );
                return false;
            }
            return true;
        }

        private void ValidateChildren( ElementArray elementArray, BinaryReader binaryReader, ref int nextAddress )
        {
            var childrenArrayPointers = ( from child in elementArray.Children
                select new
                {
                    ElementArray = child,
                    ArrayPointer = new Func<BlamPointer>( ( ) =>
                    {
                        using ( binaryReader.BaseStream.Pin( ) )
                        {
                            binaryReader.BaseStream.Seek( child.Address, SeekOrigin.Current );
                            var arrayPointer = binaryReader.ReadBlamPointer( child.ElementSize );
                            child.VirtualAddress = arrayPointer.startAddress;
                            child.Count = arrayPointer.elementCount;
                            return arrayPointer;
                        }
                    } )( )
                } ).ToList( );
            foreach ( var child in childrenArrayPointers )
            {
                if (
                    !ValidateBlamPointer( child.ArrayPointer, child.ElementArray, binaryReader.BaseStream as MapStream ) )
                    continue;
                if ( !( child.ArrayPointer.elementCount == 0 && child.ArrayPointer.startAddress == 0 ) )
                {
                    ValidateTagBlock( child.ElementArray, child.ArrayPointer, binaryReader, ref nextAddress );
                }
            }
        }

        private void ValidateTagBlock( ElementArray info, BlamPointer pointer, BinaryReader reader, ref int address )
        {
            using ( reader.BaseStream.Pin( ) )
            {
                // If owned by tag and memory has not been allocated yet*
                var allocated = from item in PointersList
                    where item.Item1.Equals( pointer )
                    select item;
                var partiallyAllocated = from item in PointersList
                    where item.Item1.Intersects( pointer )
                    select item;
                if ( IsOwnedByTag( pointer ) )
                {
                    var enumerable = allocated as IList<Tuple<BlamPointer, ElementArray>> ?? allocated.ToList( );
                    if ( !enumerable.Any( ) )
                    {
                        var alignedAddress = ( address - startOffset ) +
                                             Padding.GetCount( address - startOffset, info.Alignment );
                        if ( pointer.startAddress - startOffset != alignedAddress )
                        {
                            var mapStream = reader.BaseStream as MapStream;
                            if ( mapStream != null )
                            {
                                OnWriteMessage( string.Format( "{2}: Expected address \"{0}\"  - actually was \"{1}\"",
                                    address - startOffset, pointer.startAddress - startOffset, info.Name ) );
                            }
                        }
                        address = pointer.startAddress + pointer.PointedSize;
                    }
                    if ( enumerable.Any( ) )
                    {
                    }
                    else
                    {
                        var overlappingItems = partiallyAllocated as IList<Tuple<BlamPointer, ElementArray>> ??
                                               partiallyAllocated.ToList( );
                        if ( overlappingItems.Any( ) )
                        {
                            foreach ( var overlappingItem in overlappingItems )
                            {
                                var overlap = pointer.startAddress - overlappingItem.Item1.startAddress -
                                              overlappingItem.Item1.PointedSize;
                                OnWriteMessage( string.Format( "Overlap of ({0})[{3}] with ({1}) by ({2}) bytes",
                                    overlappingItem.Item2.ToHierarchyString( ), info.ToHierarchyString( ), overlap,
                                    overlappingItem.Item1.elementCount ) );
                            }
                        }
                    }
                }
                else if ( !IsValid( pointer ) )
                {
                    OnWriteMessage( string.Format( "INVALID POINTER" ) );
                    return;
                }
                else
                    OnWriteMessage( string.Format( "WILLLLLSOOON SHARE" ) );

                PointersList.Add( new Tuple<BlamPointer, ElementArray>( pointer, info ) );

                foreach ( var elementAddress in pointer )
                {
                    reader.BaseStream.Position = elementAddress;
                    ValidateChildren( info, reader, ref address );
                }
            }
        }
    }

    public class ElementArray
    {
        public int Address;
        public int Alignment;
        public List<ElementArray> Children;
        public int Count;
        public int ElementSize;
        public int MaxElementCount;
        public string Name;
        public ElementArray Parent;
        public int VirtualAddress;

        public ElementArray( )
        {
            Name = default( string );
            ElementSize = default( int );
            MaxElementCount = default( int );
            Count = default( int );
            Address = -1;
            Alignment = 4;
            Children = new List<ElementArray>( );
            Parent = null;
        }

        public bool HasChildren
        {
            get { return Children.Count > 0 ? true : false; }
        }

        public void Append( ElementArray array )
        {
            Name = string.Format( "{0}:{1}", Name, array.Name );
            ElementSize = ElementSize + array.ElementSize;
            Alignment = array.Alignment > Alignment ? array.Alignment : Alignment;
            Children.AddRange( array.Children );
        }

        public BlamPointer ToFixedArrayPointer( )
        {
            return new BlamPointer( Count, VirtualAddress, ElementSize );
        }

        public string ToHierarchyString( )
        {
            if ( Parent == null )
                return Name;
            return Parent.ToHierarchyString( ) + " -> " + Name;
        }

        public override string ToString( )
        {
            return Name;
        }
    }
}