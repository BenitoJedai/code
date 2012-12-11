﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Diagnostics;

namespace ScriptCoreLib.Ultra.IDL
{
    public static class IDLParser
    {

        public static IDLTypeReference ToTypeReference(this IDLParserToken source)
        {
            var Namespace = source;
            var Operator = Namespace.Next.CombineToSymbol("::");
            var Name = default(IDLParserToken);

            if (Operator.Text == "::")
            {
                Name = Operator.Next;
            }
            else
            {
                if (Namespace.Text == "unsigned")
                    Name = Operator.Next;
                else
                {
                    Name = Namespace;
                    Namespace = null;
                }

            }

            if (Name.Text == "long")
            {
                var k = Name.SkipTo();

                if (k.Text == "long")
                {
                    Name = new[] { Name, Name.Next, k }.Combine();
                }
            }

            var r = new IDLTypeReference
            {
                Name = Name.AssertName(),
                Operator = Operator,
                Namespace = Namespace
            };

            if (Name.Next.Text == "<")
            {
                r.GenericParameterSymbols.Item1 = Name.Next;
                r.GenericParameterSymbols.Item2 = r.GenericParameterSymbols.Item1.UntilSelected(
                    pp =>
                    {
                        if (pp.Text != ">")
                        {
                            if (r.GenericParameters.Count > 0)
                            {
                                pp.AssertSymbol(",");
                            }

                            var rr = ToTypeReference(pp.SkipTo());

                            r.GenericParameters.Add(rr);

                            return rr.Terminator.SkipTo();
                        }

                        return pp;
                    }
                ).AssertSymbol(">");
            }

            if (Name.Next.Text == "[")
            {
                r.ArraySymbols.Item1 = Name.Next;
                r.ArraySymbols.Item2 = r.ArraySymbols.Item1.SkipTo().AssertSymbol("]");
            }


            if (r.Terminator.Next.Text == "?")
            {
                r.NullableSymbol = r.Terminator.Next;

            }

            return r;
        }


        public static IDLModule ToModule(IDLParserToken source)
        {
            // what are we looking at?
            // at first we are looking at zero length empty token

            var GlobalModule = new IDLModule();


            var ScanModuleMember = default(Func<IDLParserToken, IDLModule, IDLParserToken>);

            ScanModuleMember =
                (p, module) =>
                {
                    if (p == null)
                        return p;

                    #region module
                    if (p.Text == "module")
                    {
                        // nested modules?
                        var NestedModule = new IDLModule();

                        module.NestedModules.Add(NestedModule);

                        NestedModule.Keyword = p;
                        NestedModule.Name = p.SkipTo().AssertName();

                        NestedModule.ModuleBody.Item1 = NestedModule.Name.SkipTo().AssertSymbol("{");
                        NestedModule.ModuleBody.Item2 = NestedModule.ModuleBody.Item1.SkipTo().UntilSelected(
                            pp =>
                            {
                                if (pp.Text == "}")
                                    return pp;

                                return ScanModuleMember(pp, NestedModule);
                            }
                        ).AssertSymbol("}");

                        return NestedModule.ModuleBody.Item2.SkipTo();
                    }
                    #endregion

                    #region typedef
                    if (p.Text == "typedef")
                    {
                        var typedef = new IDLTypeDefinition
                        {
                            DeclaringModule = module,
                            Keyword = p,
                            Type = p.SkipTo().ToTypeReference()
                        };

                        typedef.Name = typedef.Type.Terminator.SkipTo().AssertName();
                        typedef.Terminator = typedef.Name.SkipTo().AssertSymbol(";");

                        module.TypeDefinitions.Add(typedef);

                        p = typedef.Terminator.SkipTo();
                        return p;
                    }
                    #endregion

                    #region Constructors
                    var AnnotationArray = new IDLMemberAnnotationArray();

                    if (p.Text == "[")
                    {
                        AnnotationArray = p.ToAnnotationArray();

                        p = AnnotationArray.Symbols.Item2.SkipTo();
                    }
                    #endregion

                    #region interface
                    if (p.Text == "interface")
                    {
                        var i = ToInterface(p);

                        i.Members.AddRange(AnnotationArray.Items);

                        module.Interfaces.Add(i);

                        p = i.Terminator.SkipTo();
                        return p;
                    }
                    #endregion

                    return p;
                };

            // we expect module or later an interface...

            var EOF = source.SkipTo().UntilSelected(
                p =>
                {
                    return ScanModuleMember(p, GlobalModule);
                }
            );




            // retry?

            //Debugger.Break();
            // typedef?
            // interface?
            // end of body?

            return GlobalModule;
        }

        private static IDLInterface ToInterface(IDLParserToken p)
        {
            var i = new IDLInterface
            {
                Keyword = p,
                Name = p.SkipTo().AssertName()
            };

            i.InterfaceBody.Item1 = i.Name.SkipTo().UntilSelected(
                pp =>
                {
                    // what if javascript library wants to use nested types?
                    // tested by: X:\jsc.svn\examples\javascript\WebGLCannonPhysicsEngine\WebGLCannonPhysicsEngine\Application.cs
                    if (pp.Text == ".")
                    {
                        var NameFragment = pp.SkipTo().AssertName();

                        i.Name = new[] { 
                            i.Name,
                            pp,
                            NameFragment
                        }.Combine();

                        return NameFragment.SkipTo();
                    }

                    if (pp.Text == ":")
                    {
                        i.BaseType = pp.SkipTo().AssertName();

                        return i.BaseType.SkipTo();
                    }

                    return pp;
                }
            ).AssertSymbol("{");

            i.InterfaceBody.Item2 = i.InterfaceBody.Item1.SkipTo().UntilSelected(
                 pp =>
                 {
                     if (pp.Text == "}")
                         return pp;

                     #region const
                     if (pp.Text == "const")
                     {
                         var Constant = new IDLMemberConstant
                         {
                             Keyword = pp,
                             Type = pp.SkipTo().ToTypeReference(),
                         };

                         Constant.Name = Constant.Type.Terminator.SkipTo().AssertName();
                         Constant.KeywordAssignment = Constant.Name.SkipTo().AssertSymbol("=");
                         Constant.Value = Constant.KeywordAssignment.SkipTo().ToNumericLiteral();
                         Constant.Terminator = Constant.Value.Terminator.SkipTo().AssertSymbol(";");

                         // 0x00 ?

                         i.Members.Add(Constant);

                         return Constant.Terminator.SkipTo();
                     }
                     #endregion


                     var KeywordReadOnly = default(IDLParserToken);
                     var KeywordDeleter = default(IDLParserToken);
                     var Keyword_stringifier = default(IDLParserToken);

                     #region readonly
                     if (pp.Text == "readonly")
                     {
                         KeywordReadOnly = pp;
                         pp = pp.SkipTo();
                     }
                     #endregion


                     #region deleter
                     if (pp.Text == "deleter")
                     {
                         KeywordDeleter = pp;
                         pp = pp.SkipTo();
                     }
                     #endregion

                     #region stringifier
                     if (pp.Text == "stringifier")
                     {
                         Keyword_stringifier = pp;
                         pp = pp.SkipTo();
                     }
                     #endregion

                     var AnnotationArray = default(IDLMemberAnnotationArray);

                     if (pp.Text == "[")
                     {
                         AnnotationArray = pp.ToAnnotationArray();
                         pp = AnnotationArray.Symbols.Item2.SkipTo();
                     }

                     #region attribute
                     if (pp.Text == "attribute")
                     {
                         var Keyword = pp;


                         if (pp.SkipTo().Text == "[")
                         {
                             AnnotationArray = pp.SkipTo().ToAnnotationArray();
                             pp = AnnotationArray.Symbols.Item2;
                         }

                         var a = new IDLMemberAttribute
                         {
                             KeywordReadOnly = KeywordReadOnly,
                             Keyword = Keyword,
                             Annotations = AnnotationArray,
                             Type = ToTypeReference(pp.SkipTo()),
                         };

                         a.Name = a.Type.Terminator.SkipTo();
                         a.Terminator = a.Name.SkipTo().AssertSymbol(";");

                         i.Members.Add(a);
                         return a.Terminator.SkipTo();
                     }
                     #endregion

                     #region omittable
                     var __omittable = default(IDLParserToken);
                     if (pp.Text == "omittable")
                     {
                         __omittable = pp;
                         pp = pp.SkipTo();
                     }
                     #endregion


                     #region getter
                     var KeywordGetter = default(IDLParserToken);
                     if (pp.Text == "getter")
                     {
                         KeywordGetter = pp;
                         pp = pp.SkipTo();
                     }
                     #endregion

                     #region setter
                     var KeywordSetter = default(IDLParserToken);
                     if (pp.Text == "setter")
                     {
                         KeywordSetter = pp;
                         pp = pp.SkipTo();
                     }
                     #endregion

                     #region setter
                     var KeywordCreator = default(IDLParserToken);
                     if (pp.Text == "creator")
                     {
                         KeywordCreator = pp;
                         pp = pp.SkipTo();
                     }
                     #endregion


                     // method!!
                     var Method = ToMemberMethod(pp, KeywordGetter, KeywordSetter, KeywordDeleter);

                     i.Members.Add(Method);

                     return Method.Terminator.SkipTo();
                 }
            ).AssertSymbol("}");

            i.Terminator = i.InterfaceBody.Item2.SkipTo();

            if (i.Terminator == null)
            {
                i.Terminator = (IDLParserToken.Literal)";";
                i.Terminator.IsSymbol = true;
            }

            i.Terminator.AssertSymbol(";");

            return i;
        }

        private static IDLMemberMethod ToMemberMethod(IDLParserToken pp, IDLParserToken KeywordGetter, IDLParserToken KeywordSetter, IDLParserToken KeywordDeleter)
        {
            var Type = default(IDLTypeReference);
            var Name = default(IDLParserToken.Literal);

            Func<IDLParserToken> GetParameterSymbols = () => Name.Value.SkipTo();

            var NextToken = pp.SkipTo();

            // are we typeless? IDL is partial...
            if (NextToken.Text != "(")
            {
                Type = ToTypeReference(pp);
                Name = Type.Terminator.SkipTo().AssertName();
            }
            else
            {
                if (KeywordDeleter != null)
                {
                    Type = ToTypeReference(pp);
                    Name = "deleter";
                    GetParameterSymbols = () => pp.SkipTo();
                }
                else
                {
                    // or are we nameless? is this method an indexer?

                    // or any other
                    var NameLookup = new Dictionary<string, string>
                    {
                        {"void", "set"},
                        {"octet", "get"}
                    };

                    if (NameLookup.ContainsKey(pp.Text))
                    {
                        Type = ToTypeReference(pp);
                        Name = NameLookup[pp.Text];
                        GetParameterSymbols = () => pp.SkipTo();
                    }
                    else
                    {
                        if (KeywordGetter == null)
                        {
                            // are we typeless? IDL is partial...
                            Name = pp.AssertName();
                        }
                        else
                        {
                            Type = ToTypeReference(pp);
                            Name = "item";
                            GetParameterSymbols = () => pp.SkipTo();
                        }
                    }
                }
            }

            var Method = new IDLMemberMethod
            {
                ReturnType = Type,
                KeywordGetter = KeywordGetter,
                KeywordSetter = KeywordSetter,
                Name = Name
            };

            var Parameters = Method.Parameters;
            var ParameterSymbols = Method.ParameterSymbols;

            ParameterSymbols.Item1 = GetParameterSymbols();

            ToParameters(Parameters, ParameterSymbols);

            Method.KeywordRaises = Method.ParameterSymbols.Item2.SkipTo();

            if (Method.KeywordRaises.Text == "raises")
            {
                Method.RaisesSymbols.Item1 = Method.KeywordRaises.SkipTo().AssertSymbol("(");
                Method.RaisesType = Method.RaisesSymbols.Item1.SkipTo().ToTypeReference();
                Method.RaisesSymbols.Item2 = Method.RaisesType.Terminator.SkipTo().AssertSymbol(")");

                Method.Terminator = Method.RaisesSymbols.Item2.SkipTo();

            }
            else
            {
                Method.Terminator = Method.KeywordRaises;
                Method.KeywordRaises = null;
            }

            Method.Terminator.AssertSymbol(";");

            return Method;
        }

        private static void ToParameters(List<IDLParameter> Parameters, IDLParserTokenPair ParameterSymbols)
        {
            ParameterSymbols.Item1 = ParameterSymbols.Item1.AssertSymbol("(");
            ParameterSymbols.Item2 = ParameterSymbols.Item1.SkipTo().UntilSelected(
                p =>
                {
                    if (p.Text != ")")
                    {
                        if (Parameters.Count > 0)
                        {
                            p.AssertSymbol(",");
                            p = p.SkipTo();
                        }

                        var ParameterTypeToken = p;
                        var KeywordIn = default(IDLParserToken);
                        var KeywordOptional = default(IDLParserToken);


                        if (ParameterTypeToken.Text == "in")
                        {
                            KeywordIn = ParameterTypeToken;
                            ParameterTypeToken = ParameterTypeToken.SkipTo();
                        }

                        if (ParameterTypeToken.Text == "optional")
                        {
                            KeywordOptional = ParameterTypeToken;
                            ParameterTypeToken = ParameterTypeToken.SkipTo();
                        }

                        var ParameterType = ToTypeReference(ParameterTypeToken);

                        var param = new IDLParameter
                        {
                            KeywordIn = KeywordIn,
                            KeywordOptional = KeywordOptional,
                            ParameterType = ParameterType,
                            Name = ParameterType.Terminator.SkipTo().AssertName()

                        };

                        Parameters.Add(param);

                        return param.Name.SkipTo();
                    }

                    return p;
                }
            ).AssertSymbol(")");
        }

        public static IDLNumericLiteral ToNumericLiteral(this IDLParserToken Marker)
        {
            var n = new IDLNumericLiteral { Marker = Marker };

            Marker.AssertDigit();

            var w = new StringBuilder();
            var i = n.Marker;

            n.Terminator = i;

            if (n.Marker.Text == "0")
            {
                if (n.Marker.Next.Text.StartsWith("x"))
                {
                    n.Style = System.Globalization.NumberStyles.HexNumber;

                    w.Append(n.Marker.Next.Text.Substring(1));

                    n.Terminator = i.Next;

                    i = i.Next.Next;
                }
            }




            i.UntilSelected(
                p =>
                {
                    if (p.Text.IsDigit())
                    {
                        n.Terminator = p;
                        w.Append(p.Text);
                    }

                    if (p.Next.Text.IsDigit())
                        return p.Next;

                    return p;
                }
            );

            n.Int32Value = int.Parse(w.ToString(), n.Style);


            return n;
        }


        public static IDLMemberAnnotationArray ToAnnotationArray(this IDLParserToken p)
        {
            var a = new IDLMemberAnnotationArray();

            a.Symbols.Item1 = p.AssertSymbol("[");


            // multiple constructors
            a.Symbols.Item2 = p.UntilSelected(
                pp =>
                {
                    if (pp.Text == "]")
                        return pp;

                    if (a.Items.Count > 0)
                    {
                        pp = pp.AssertSymbol(",");
                    }

                    var Constructor = new IDLMemberAnnotation
                    {
                        Keyword = pp.SkipTo(),
                        //.AssertName("Constructor"),
                    };

                    Constructor.Keyword.SkipTo().With(
                        Item1 =>
                        {
                            if (Item1.Text != "(")
                                return;

                            Constructor.ParameterSymbols.Item1 = Item1;
                            ToParameters(Constructor.Parameters, Constructor.ParameterSymbols);
                        }
                    );

                    a.Items.Add(Constructor);

                    return Constructor.Terminator.SkipTo();
                }
            ).AssertSymbol("]");

            return a;
        }
    }
}
