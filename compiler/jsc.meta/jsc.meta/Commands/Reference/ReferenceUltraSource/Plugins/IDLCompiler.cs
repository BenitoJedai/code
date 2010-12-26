﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using jsc.Library;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Ultra.IDL;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins
{
    public class IDLCompiler
    {
        public string DefaultNamespace;
        public XElement BodyElement;
        public RewriteToAssembly r;
        public Func<string, FileInfo> GetLocalResource;

        public class IDLFileTuple
        {
            public string TargetNamespace;
            public FileInfo File;
        }

        public void Define()
        {
            var Files = from a in BodyElement.XPathSelectElements("//a")
                        let href = a.Attribute("href").Value
                        where href.EndsWith(".idl")
                        let f = GetLocalResource(href)
                        select new IDLFileTuple
                        {
                            TargetNamespace = a.Attribute("data-namespace").Value,
                            File = f
                        };

            Define(Files);



        }

        public void Define(IEnumerable<IDLFileTuple> Files)
        {
            var modules =
                (from a in Files
                 let m = ((IDLModule)File.ReadAllText(a.File.FullName))
                 select new { f = a.File, m, a.TargetNamespace }
            );

            var PrimaryModules = modules.SelectMany(
                k => new[] { k.m }.Concat(k.m.NestedModules).WithEach(
                    m =>
                    {
                        m.Interfaces.WithEach(
                            i =>
                            {
                                i.TargetNamespace = k.TargetNamespace;
                            }
                        );
                    }
                )
            );

            var PrimaryTypes =
                from m in PrimaryModules
                from Interface in m.Interfaces
                select new { Interface, m };


            #region KnownTypeCache
            var KnownTypeCache = new Dictionary<string, Type>();


            KnownTypeCache["html::HTMLCanvasElement"] = typeof(IHTMLCanvas);
            KnownTypeCache["html::HTMLImageElement"] = typeof(IHTMLImage);
            KnownTypeCache["html::HTMLVideoElement"] = typeof(IHTMLVideo);

            KnownTypeCache["Element"] = typeof(IHTMLElement);

            KnownTypeCache["events::Event"] = typeof(IEvent);

            KnownTypeCache["void"] = typeof(void);
            KnownTypeCache["object"] = typeof(object);
            KnownTypeCache["any"] = typeof(object);
            KnownTypeCache["short"] = typeof(short);
            KnownTypeCache["unsigned short"] = typeof(ushort);
            KnownTypeCache["boolean"] = typeof(bool);
            KnownTypeCache["long"] = typeof(long);
            KnownTypeCache["unsigned long"] = typeof(ulong);
            KnownTypeCache["octet"] = typeof(byte);
            KnownTypeCache["byte"] = typeof(sbyte);
            KnownTypeCache["unsigned byte"] = typeof(byte);
            KnownTypeCache["DOMString"] = typeof(string);

            // let's pretend float is a double... :) ?

            KnownTypeCache["float"] = typeof(float);
            KnownTypeCache["double"] = typeof(double);

            #endregion


            #region TypeCache
            var TypeCache = new VirtualDictionary<IDLTypeReference, Type>();

            TypeCache.Resolve +=
                SourceType =>
                {
                    #region T[]
                    // newer WebIDL is using byte[]
                    if (SourceType.ArraySymbols.Item2 != null)
                    {
                        var ElementType = TypeCache[
                            new IDLTypeReference { Name = SourceType.Name, Namespace = SourceType.Namespace }
                        ];

                        TypeCache.BaseDictionary[SourceType] = ElementType.MakeArrayType();

                        return;
                    }

                    // while some instances were previously using sequence<byte>
                    if (SourceType.Name.Text == "sequence")
                    {
                        if (SourceType.GenericParameters.Count == 1)
                        {
                            var ElementType = TypeCache[SourceType.GenericParameters[0]];

                            TypeCache.BaseDictionary[SourceType] = ElementType.MakeArrayType();

                            return;
                        }
                    }
                    #endregion

                    #region TypeDefinitions
                    var NameLiteral = SourceType.ToString();

                    // do we have to resolve it?


                    var TypeDefinition = (from m in PrimaryModules
                                          from def in m.TypeDefinitions
                                          where def.Name.Text == NameLiteral
                                          select def.Type.ToString()).FirstOrDefault();

                    if (TypeDefinition != null)
                        NameLiteral = TypeDefinition;
                    #endregion

                    #region KnownTypeCache
                    if (KnownTypeCache.ContainsKey(NameLiteral))
                    {
                        TypeCache.BaseDictionary[SourceType] = KnownTypeCache[NameLiteral];
                        return;

                    }
                    #endregion


                    var item = PrimaryTypes.FirstOrDefault(k => k.Interface.Name.Text == SourceType.Name.Text);

                    // alias for spec violation ... 
                    if (item == null)
                    {
                        if (SourceType.Name.Text == "FloatArray")
                        {
                            // typedarrays::FloatArray FloatArray; 

                            TypeCache.BaseDictionary[SourceType] = TypeCache[
                                IDLTypeReference.OfName("Float32Array")
                            ];

                            return;
                        }
                    }

                    if (item == null)
                    {
                        throw new NotSupportedException(SourceType.ToString());
                    }

                    var BaseType = typeof(object);

                    if (item.Interface.BaseType != null)
                        BaseType = TypeCache[
                            new IDLTypeReference { Name = item.Interface.BaseType }
                        ];

                    var FullName = item.Interface.TargetNamespace + "." + item.Interface.Name.Text;

                    Console.WriteLine(FullName);



                    // Definitions...
                    var t = this.r.RewriteArguments.Module.DefineType(
                        FullName,
                        System.Reflection.TypeAttributes.Public,
                        BaseType
                    );

                    KnownTypeCache[NameLiteral] = t;
                    TypeCache.BaseDictionary[SourceType] = t;

                    t.SetCustomAttribute(
                        new ScriptAttribute
                        {
                            HasNoPrototype = true,
                            ExternalTarget = item.Interface.GetConstructors().Any() ? item.Interface.Name.Text : null
                        }.ToCustomAttributeBuilder()(this.r.RewriteArguments.context)

                        //new DescriptionAttribute(
                        //    item.Interface.Keyword.Source.Substring(
                        //        item.Interface.Keyword.Position,
                        //        item.Interface.Terminator.Position -
                        //        item.Interface.Keyword.Position
                        //   )
                        //).ToCustomAttributeBuilder()(this.r.RewriteArguments.context)
                    );

                    #region Methods
                    var MethodCache = item.Interface.GetMethods().ToDictionary(
                        k => k,
                        SourceMethod =>
                        {
                            var ReturnType = typeof(void);

                            if (SourceMethod.ReturnType != null)
                                ReturnType = TypeCache[SourceMethod.ReturnType];

                            var Method = t.DefineMethod(SourceMethod.Name.Text, System.Reflection.MethodAttributes.Public,
                                ReturnType,
                                SourceMethod.Parameters.Select(k => TypeCache[k.ParameterType]).ToArray()
                            );

                            for (int i = 0; i < SourceMethod.Parameters.Count; i++)
                            {
                                Method.DefineParameter(i + 1, System.Reflection.ParameterAttributes.None, SourceMethod.Parameters[i].Name.Text);
                            }

                            Method.NotImplemented();

                            return Method;
                        }
                    );
                    #endregion


                    foreach (var SourceConstructor in item.Interface.GetConstructors())
                    {

                        var Constructor = t.DefineConstructor(
                                    System.Reflection.MethodAttributes.Public,
                                    System.Reflection.CallingConventions.Standard,
                                     SourceConstructor.Parameters.Select(k => TypeCache[k.ParameterType]).ToArray()
                                );

                        for (int i = 0; i < SourceConstructor.Parameters.Count; i++)
                        {
                            var p = Constructor.DefineParameter(i + 1, System.Reflection.ParameterAttributes.None, SourceConstructor.Parameters[i].Name.Text);

                            if (i == SourceConstructor.Parameters.Count - 1)
                            {
                                if (TypeCache[SourceConstructor.Parameters[i].ParameterType].IsArray)
                                {
                                    p.SetCustomAttribute(
                                        new ParamArrayAttribute().ToCustomAttributeBuilder()(null)
                                    );
                                }
                            }
                        }

                        Constructor.GetILGenerator().EmitCode(() => { throw new NotImplementedException(); });
                    }

                    foreach (var m in item.Interface.Members)
                    {
                        #region SourceConstant
                        (m as IDLMemberConstant).With(
                            SourceConstant =>
                            {
                                var FieldType = TypeCache[SourceConstant.Type];
                                var Field = t.DefineField(SourceConstant.Name.Text,
                                    FieldType,
                                    System.Reflection.FieldAttributes.Static |
                                    System.Reflection.FieldAttributes.Public |
                                    System.Reflection.FieldAttributes.Literal
                                );



                                Field.SetConstant(
                                     Convert.ChangeType(
                                    SourceConstant.Value.Int32Value
                                    , FieldType
                                    )
                                );
                            }
                        );
                        #endregion


                        #region SourceAttribute
                        (m as IDLMemberAttribute).With(
                            SourceAttribute =>
                            {
                                var Attributes = System.Reflection.FieldAttributes.Public;

                                SourceAttribute.KeywordReadOnly.With(k => Attributes |= System.Reflection.FieldAttributes.InitOnly);

                                var a = t.DefineField(SourceAttribute.Name.Text,
                                    TypeCache[SourceAttribute.Type],
                                    Attributes
                                );
                            }
                        );
                        #endregion

                    }

                    //     getter GLubyte get(in unsigned long index);
                    //    setter void set(in unsigned long index, in GLubyte value);

                    #region indexer
                    var IndexerName = "Item";
                    var IndexerGetter = item.Interface.GetMethods().FirstOrDefault(
                        k => k.KeywordGetter != null && k.Parameters.Count == 1
                    );

                    var IndexerSetter = item.Interface.GetMethods().FirstOrDefault(
                        k => k.KeywordSetter != null && k.Parameters.Count == 2 && k.ReturnType.ToString() == "void"
                    );

                    if (IndexerGetter != null)
                    {
                        // now the types must also match
                        var IndexerKeyType = IndexerGetter.Parameters[0].ParameterType;
                        var IndexerValueType = IndexerGetter.ReturnType;


                        var Indexer = t.DefineProperty(IndexerName, System.Reflection.PropertyAttributes.SpecialName,
                            TypeCache[IndexerValueType], new[] { TypeCache[IndexerKeyType] }
                        );

                        if (IndexerSetter != null)
                            if (IndexerKeyType.ToString() == IndexerSetter.Parameters[0].ParameterType.ToString())
                                if (IndexerValueType.ToString() == IndexerSetter.Parameters[1].ParameterType.ToString())
                                {
                                    Indexer.SetSetMethod(MethodCache[IndexerSetter]);
                                }

                        Indexer.SetGetMethod(MethodCache[IndexerGetter]);

                        t.SetCustomAttribute(
                            new DefaultMemberAttribute(IndexerName).ToCustomAttributeBuilder()(null)
                        );

                        // calling a getter on a native type which is an indexer should reuse given language syntax...
                    }
                    #endregion

                    t.CreateType();

                };
            #endregion



            var PrimaryTypesDefined = PrimaryTypes.Select(item =>
                TypeCache[
                    new IDLTypeReference { Name = item.Interface.Name }
                ]
           ).ToArray();



            // { 
            // a = {<a href="CanvasRenderingContext2D.idl">CanvasRenderingContext2D</a>}, 
            // f = {W:\jsc.svn\core\ScriptCoreLib.Ultra.Components\ScriptCoreLib.Ultra.Components.IDL\Design\IDLFiles\CanvasRenderingContext2D.idl} 
            // }
        }
    }
}
