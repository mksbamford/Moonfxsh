﻿using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Moonfish.Guerilla.Reflection;
using Moonfish.Tags;
using OpenTK;

namespace Moonfish.Guerilla.CodeDom
{
    internal class GuerillaBlockClass
    {
        private static readonly Dictionary<MoonfishFieldType, Type> ValueTypeDictionary;
        private static GuerillaCommentCollection _comments = new GuerillaCommentCollection();
        private readonly string _outputFileName;
        private readonly CodeTypeDeclaration _targetClass;
        private readonly CodeCompileUnit _targetUnit;
        private readonly TokenDictionary _tokenDictionary;

        static GuerillaBlockClass()
        {
            BinaryIOReflection.CacheMethods();
            var assembly = typeof (StringIdent).Assembly;
            var query = from type in assembly.GetTypes()
                where type.GetCustomAttributes(typeof (GuerillaTypeAttribute), false).Any()
                select type;
            var valueTypes = query.ToArray();
            ValueTypeDictionary = new Dictionary<MoonfishFieldType, Type>(valueTypes.Count());
            foreach (var type in valueTypes)
            {
                var guerillaTypeAttributes =
                    (GuerillaTypeAttribute[]) type.GetCustomAttributes(typeof (GuerillaTypeAttribute), false);
                foreach (var guerillaType in guerillaTypeAttributes)
                {
                    ValueTypeDictionary.Add(guerillaType.FieldType, type);
                }
            }
            ValueTypeDictionary.Add(MoonfishFieldType.FieldAngle, typeof (float));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealEulerAngles_3D, typeof (Vector3));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldCharInteger, typeof (byte));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldShortInteger, typeof (short));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldShortBounds, typeof (int));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldLongInteger, typeof (int));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldReal, typeof (float));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealFraction, typeof (float));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealFractionBounds, typeof (Vector2));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealPoint_2D, typeof (Vector2));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealVector_3D, typeof (Vector3));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealVector_2D, typeof (Vector2));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealPoint_3D, typeof (Vector3));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealEulerAngles_2D, typeof (Vector2));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealPlane_2D, typeof (Vector3));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealPlane_3D, typeof (Vector4));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealQuaternion, typeof (Quaternion));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRealArgbColor, typeof (Vector4));
            ValueTypeDictionary.Add(MoonfishFieldType.FieldRectangle_2D, typeof (Vector2));
        }

        public GuerillaBlockClass(MoonfishTagGroup tag, IList<MoonfishTagGroup> tagGroups = null)
            : this(tag.Definition.Name.ToPascalCase().ToValidToken())
        {
            var size = 0;
            var hasParent = tagGroups != null && tagGroups.Any(x => x.Class == tag.ParentClass);
            if (hasParent)
            {
                var parentTag = tagGroups.First(x => x.Class == tag.ParentClass);
                _targetClass.BaseTypes.Clear();
                _targetClass.BaseTypes.Add(
                    new CodeTypeReference(parentTag.Definition.Name.ToPascalCase().ToValidToken()));

                // loop through all the parents summing up thier sizes
                while (hasParent)
                {
                    size += parentTag.Definition.CalculateSizeOfFieldSet();
                    hasParent = tagGroups.Any(x => x.Class == parentTag.ParentClass);
                    if (hasParent)
                        parentTag = tagGroups.First(x => x.Class == parentTag.ParentClass);
                }
            }
            size = tag.Definition.CalculateSizeOfFieldSet();

            Initialize(tag.Definition, size);
        }

        public GuerillaBlockClass(MoonfishTagDefinition definition)
            : this(definition.Name.ToPascalCase().ToValidToken())
        {
            var size = definition.CalculateSizeOfFieldSet();
            Initialize(definition, size);
        }

        private GuerillaBlockClass(string className)
        {
            _outputFileName = string.Format("{0}.generated.cs", className);

            _tokenDictionary = new TokenDictionary();
            _targetUnit = new CodeCompileUnit();

            var tagsCodeNamespace = new CodeNamespace("Moonfish.Guerilla.Tags");
            tagsCodeNamespace.Imports.Add(new CodeNamespaceImport("Moonfish.Tags"));
            tagsCodeNamespace.Imports.Add(new CodeNamespaceImport("Moonfish.Model"));
            tagsCodeNamespace.Imports.Add(new CodeNamespaceImport("System.IO"));
            tagsCodeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            tagsCodeNamespace.Imports.Add(new CodeNamespaceImport("System.Linq"));
            _targetClass = new CodeTypeDeclaration(className)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public,
                BaseTypes = {new CodeTypeReference(typeof (GuerillaBlock))}
            };
            tagsCodeNamespace.Types.Add(_targetClass);
            _targetUnit.Namespaces.Add(tagsCodeNamespace);
        }

        private void Initialize(MoonfishTagDefinition definition, int size)
        {
            AddReadOnlyIntProperty(StaticReflection.GetMemberName((GuerillaBlock block) => block.SerializedSize), size);
            AddReadOnlyIntProperty(StaticReflection.GetMemberName((GuerillaBlock block) => block.Alignment),
                definition.Alignment);
            GenerateFields(definition.Fields);
            GenerateReadFieldsMethod();
            GenerateCSharpCode();
        }

        private void GenerateReadFieldsMethod()
        {
            CodeMemberMethod method = new CodeMemberMethod
            {
                Name = "ReadFields",
                Attributes = MemberAttributes.Override | MemberAttributes.Public,
                ReturnType = new CodeTypeReference(typeof (BlamPointer[]))
            };

            //  BinaryReader binaryReader = new BinaryReader();
            const string binaryReader = "binaryReader";
            var binaryReaderReference = new CodeTypeReference(typeof(BinaryReader));
            var binaryReaderVariableDeclaration =
                new CodeVariableDeclarationStatement(binaryReaderReference,
                   binaryReader, new CodeObjectCreateExpression(binaryReaderReference));
            method.Statements.Add(binaryReaderVariableDeclaration);

            foreach (CodeObject codeObject in _targetClass.Members)
            {
                if (!(codeObject is CodeMemberField)) continue;

                // 
                var field = (CodeMemberField)codeObject;

               
                var systemType = Type.GetType(field.Type.BaseType);
                if(systemType != null)
                {
                    var methodName = BinaryIOReflection.GetBinaryReaderMethodName(systemType);
                    var binaryReaderVariable = new CodeVariableReferenceExpression(binaryReader);

                    //this.Name
                    var fieldReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
                    var fieldAssignment = new CodeAssignStatement(fieldReference,
                        new CodeMethodInvokeExpression(binaryReaderVariable, methodName, new CodeArgumentReferenceExpression()));
                    method.Statements.Add(fieldAssignment);

                }
            }
            _targetClass.Members.Add(method);
        }

        private void GenerateFields(IList<MoonfishTagField> fields)
        {
            foreach (var field in fields)
            {
                switch (field.Type)
                {
                    case MoonfishFieldType.FieldTagReference:
                    {
                        var member = new CodeMemberField(typeof (TagReference),
                            _tokenDictionary.GenerateValidToken(GenerateFieldName(field)));
                        member.CustomAttributes.Add(
                            new CodeAttributeDeclaration(new CodeTypeReference(typeof (TagReferenceAttribute)),
                                new CodeAttributeArgument(new CodePrimitiveExpression(field.Definition.Class))));
                        GenerateSummary(member);
                        member.Attributes = MemberAttributes.Public;
                        _targetClass.Members.Add(member);
                        break;
                    }
                    case MoonfishFieldType.FieldBlock:
                    {
                        var fieldBlockClass = new GuerillaBlockClass(field.Definition);
                        var typeName = fieldBlockClass._targetClass.Name;
                        var member = new CodeMemberField(typeName + "[]",
                            _tokenDictionary.GenerateValidToken(GenerateFieldName(field)));
                        GenerateSummary(member);
                        member.Attributes = MemberAttributes.Public;
                        _targetClass.Members.Add(member);
                        break;
                    }
                    case MoonfishFieldType.FieldStruct:
                    {
                        var member =
                            new CodeMemberField(
                                ((GuerillaName) field.Definition.Name).Name.ToPascalCase().ToValidToken(),
                                _tokenDictionary.GenerateValidToken(GenerateFieldName(field)));
                        GenerateSummary(member);
                        member.Attributes = MemberAttributes.Public;
                        _targetClass.Members.Add(member);
                        break;
                    }
                    case MoonfishFieldType.FieldData:
                    {
                        var type = ((MoonfishTagDataDefinition) field.Definition).DataElementSize == 1
                            ? typeof (byte[])
                            : typeof (short[]);
                        var member = new CodeMemberField(type,
                            _tokenDictionary.GenerateValidToken(GenerateFieldName(field)));
                        GenerateSummary(member);
                        member.Attributes = MemberAttributes.Public;
                        _targetClass.Members.Add(member);
                        break;
                    }
                    case MoonfishFieldType.FieldExplanation:
                    {
                        var value = field.Definition as string;
                        if (string.IsNullOrWhiteSpace(value)) continue;
                        PushComments(value);
                        break;
                    }
                    case MoonfishFieldType.FieldByteFlags:
                    case MoonfishFieldType.FieldLongFlags:
                    case MoonfishFieldType.FieldWordFlags:
                    case MoonfishFieldType.FieldCharEnum:
                    case MoonfishFieldType.FieldEnum:
                    case MoonfishFieldType.FieldLongEnum:
                    {
                        var fieldTypeName = field.Strings.Name.ToPascalCase().ToValidToken();
                        CodeTypeDeclaration typeDeclaration;
                        switch (field.Type)
                        {
                            case MoonfishFieldType.FieldByteFlags:
                                typeDeclaration = new CodeTypeDeclaration(fieldTypeName)
                                {
                                    IsEnum = true,
                                    BaseTypes = {new CodeTypeReference(typeof (byte))}
                                };
                                typeDeclaration.CustomAttributes.Add(
                                    new CodeAttributeDeclaration(new CodeTypeReference(typeof (FlagsAttribute))));
                                break;
                            case MoonfishFieldType.FieldWordFlags:
                                typeDeclaration = new CodeTypeDeclaration(fieldTypeName)
                                {
                                    IsEnum = true,
                                    BaseTypes = {new CodeTypeReference(typeof (short))}
                                };
                                typeDeclaration.CustomAttributes.Add(
                                    new CodeAttributeDeclaration(new CodeTypeReference(typeof (FlagsAttribute))));
                                break;
                            case MoonfishFieldType.FieldLongFlags:
                                typeDeclaration = new CodeTypeDeclaration(fieldTypeName)
                                {
                                    IsEnum = true,
                                    BaseTypes = {new CodeTypeReference(typeof (int))}
                                };
                                typeDeclaration.CustomAttributes.Add(
                                    new CodeAttributeDeclaration(new CodeTypeReference(typeof (FlagsAttribute))));
                                break;
                            case MoonfishFieldType.FieldCharEnum:
                                typeDeclaration = new CodeTypeDeclaration(fieldTypeName)
                                {
                                    IsEnum = true,
                                    BaseTypes = {new CodeTypeReference(typeof (byte))}
                                };
                                break;
                            case MoonfishFieldType.FieldEnum:
                                typeDeclaration = new CodeTypeDeclaration(fieldTypeName)
                                {
                                    IsEnum = true,
                                    BaseTypes = {new CodeTypeReference(typeof (short))}
                                };
                                break;
                            case MoonfishFieldType.FieldLongEnum:
                                typeDeclaration = new CodeTypeDeclaration(fieldTypeName)
                                {
                                    IsEnum = true,
                                    BaseTypes = {new CodeTypeReference(typeof (int))}
                                };
                                break;
                            default:
                                continue;
                        }
                        var comments = PullComments();
                        var memberComments = comments.Descriptions.ToList();
                        var enumDefintion = (MoonfishTagEnumDefinition) field.Definition;
                        for (var index = 0; index < enumDefintion.Names.Count; index++)
                        {
                            var value = enumDefintion.Names[index];
                            var comment = index < memberComments.Count ? memberComments[index] : null;
                            var member = new CodeMemberField
                            {
                                Name = value.ToPascalCase().ToValidToken()
                            };
                            if (comment != null)
                                member.Comments.AddRange(
                                    new[]
                                    {
                                        new CodeCommentStatement("<summary>", true),
                                        new CodeCommentStatement(comment.Trim(), true),
                                        new CodeCommentStatement("</summary>", true)
                                    });
                            typeDeclaration.Members.Add(member);
                        }
                        var fieldMember = new CodeMemberField(new CodeTypeReference(typeDeclaration.Name),
                            _tokenDictionary.GenerateValidToken(GenerateFieldName(field)));
                        if (comments.HasSummary)
                            typeDeclaration.Comments.AddRange(new[]
                            {
                                new CodeCommentStatement("<summary>", true),
                                new CodeCommentStatement(comments.Summary.Trim(), true),
                                new CodeCommentStatement("</summary>", true)
                            });
                        fieldMember.Attributes = MemberAttributes.Public;
                        _targetClass.Members.Add(typeDeclaration);
                        _targetClass.Members.Add(fieldMember);
                        break;
                    }
                    case MoonfishFieldType.FieldByteBlockFlags:
                    {
                        var member = GenerateCodeMemberField<BlockFlags8>(field);
                        _targetClass.Members.Add(member);
                    }
                        break;
                    case MoonfishFieldType.FieldWordBlockFlags:
                    {
                        var member = GenerateCodeMemberField<BlockFlags16>(field);
                        _targetClass.Members.Add(member);
                    }
                        break;
                    case MoonfishFieldType.FieldLongBlockFlags:
                    {
                        var member = GenerateCodeMemberField<BlockFlags32>(field);
                        _targetClass.Members.Add(member);
                    }
                        break;
                    case MoonfishFieldType.FieldCharBlockIndex1:
                    {
                        var member = GenerateCodeMemberField<ByteBlockIndex1>(field);
                        _targetClass.Members.Add(member);
                    }
                        break;
                    case MoonfishFieldType.FieldShortBlockIndex1:
                    {
                        var member = GenerateCodeMemberField<ShortBlockIndex1>(field);
                        _targetClass.Members.Add(member);
                    }
                        break;
                    case MoonfishFieldType.FieldLongBlockIndex1:
                    {
                        var member = GenerateCodeMemberField<LongBlockIndex1>(field);
                        _targetClass.Members.Add(member);
                    }
                        break;
                    case MoonfishFieldType.FieldCharBlockIndex2:
                    {
                        var member = GenerateCodeMemberField<ByteBlockIndex2>(field);
                        _targetClass.Members.Add(member);
                    }
                        break;
                    case MoonfishFieldType.FieldShortBlockIndex2:
                    {
                        var member = GenerateCodeMemberField<ShortBlockIndex2>(field);
                        _targetClass.Members.Add(member);
                    }
                        break;
                    case MoonfishFieldType.FieldLongBlockIndex2:
                    {
                        var member = GenerateCodeMemberField<LongBlockIndex2>(field);
                        _targetClass.Members.Add(member);
                    }
                        break;
                    case MoonfishFieldType.FieldArrayStart:
                    {
                        break;
                    }
                    case MoonfishFieldType.FieldArrayEnd:
                    {
                        return;
                    }
                    case MoonfishFieldType.FieldSkip:
                    case MoonfishFieldType.FieldPad:
                    {
                        var member = GenerateCodeMemberField<byte>(field, MemberAttributes.Private);
                        _targetClass.Members.Add(member);
                    }
                        break;
                    case MoonfishFieldType.FieldUselessPad:
                    case MoonfishFieldType.FieldTerminator:
                    case MoonfishFieldType.FieldCustom:
                    {
                        break;
                    }
                    default:
                    {
                        var member = new CodeMemberField(ValueTypeDictionary[field.Type],
                            _tokenDictionary.GenerateValidToken(GenerateFieldName(field)));
                        member.Attributes = MemberAttributes.Public;
                        GenerateSummary(member);

                        _targetClass.Members.Add(member);
                        break;
                    }
                }
            }
        }

        private CodeMemberField GenerateCodeMemberField<T>(MoonfishTagField field,
            MemberAttributes attributes = MemberAttributes.Public)
        {
            var member = new CodeMemberField(typeof (T),
                _tokenDictionary.GenerateValidToken(GenerateFieldName(field, attributes)))
            {
                Attributes = attributes
            };
            GenerateSummary(member);
            return member;
        }

        private static string GenerateFieldName(MoonfishTagField field,
            MemberAttributes attributes = MemberAttributes.Public)
        {
            string token;
            try
            {
                token = field.Strings.Name.IsValidIdentifier()
                    ? field.Strings.Name
                    : field.Definition != null ? field.Definition.Name : "_invalid Name";
            }
            catch (Exception e)
            {
                token = "_invalid Name";
            }
            return attributes.HasFlag(MemberAttributes.Public)
                ? token.ToPascalCase().ToValidToken()
                : token.ToCamelCase().ToValidToken();
        }

        private static void GenerateSummary(CodeTypeMember member)
        {
            var comment = PullComments();
            if (comment.HasSummary)
                member.Comments.AddRange(
                    new[]
                    {
                        new CodeCommentStatement("<summary>", true),
                        new CodeCommentStatement(comment.Summary, true),
                        new CodeCommentStatement("</summary>", true)
                    });
        }

        private static void PushComments(string value)
        {
            _comments = new GuerillaCommentCollection(value);
        }

        private static GuerillaCommentCollection PullComments()
        {
            var copy = _comments.CreateCopy();
            _comments = new GuerillaCommentCollection();
            return copy;
        }

        public void AddReadOnlyIntProperty(string name, int value)
        {
            var serializedSizeProperty = new CodeMemberProperty
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                Name = name.ToPascalCase(),
                HasGet = true,
                Type = new CodeTypeReference(typeof (int))
            };
            serializedSizeProperty.GetStatements.Add(
                new CodeMethodReturnStatement(new CodePrimitiveExpression(value)));

            _targetClass.Members.Add(serializedSizeProperty);
        }

        public void GenerateCSharpCode()
        {
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var options = new CodeGeneratorOptions
            {
                BracingStyle = "C",
                BlankLinesBetweenMembers = false,
                VerbatimOrder = false
            };
            var filename = Path.Combine(Local.ProjectDirectory, Path.Combine("Guerilla\\Debug\\", _outputFileName));
            using (var streamWriter = new StreamWriter(File.Create(filename)))
            {
                provider.GenerateCodeFromCompileUnit(_targetUnit, streamWriter, options);
            }
        }
    };
}