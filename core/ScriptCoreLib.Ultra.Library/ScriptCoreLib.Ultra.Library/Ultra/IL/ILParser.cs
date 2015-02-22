using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.IDL;

namespace ScriptCoreLib.Ultra.IL
{
    public static class ILParser
    {
        public static ILAssembly ToAssemblyWithMethods(this IDLParserToken t)
        {
            // used by?

            var a = new ILAssembly();

            var q = t;

     
            #region manage those quouted literals
            {
                var p = q;
                while (p != null)
                {
                    Action<string> CombineAsPairs =
                        Qoute =>
                        {
                            var ok = p.Text == Qoute;

                            while (ok)
                            {
                                p.IsCommentDiscoveryEnabled = false;

                                // do we allow escaping? :)
                                if (p.Next.Text == Qoute)
                                    ok = false;

                                new[] { p, p.Next }.Combine();

                                //Console.WriteLine("literal: " + p.Text);
                            }

                            p.IsCommentDiscoveryEnabled = true;
                        };

                    CombineAsPairs("\"");
                    CombineAsPairs("'");

                    Action<string> CombineKeyword =
                        k =>
                        {
                            if (p.Text + p.Next.Text == k)
                            {
                                new[] { p, p.Next }.Combine();
                            }
                        };


                    CombineKeyword(".corflags");
                    CombineKeyword(".class");
                    CombineKeyword(".method");
                    CombineKeyword(".ctor");

                    p = p.SkipTo();
                }
            }
            #endregion



            {


                {
                    var p = q;
                    while (p != null)
                    {
                        if (p.Text == ".method")
                        {
                            var NameToken = p.SkipWhile(k => k.SkipTo().Text != "(").First();
                            var Sig = p.TakeWhile(k => k != NameToken).ToArray();

                            var IsStatic = Sig.Any(k => k.Text == "static");
                            var IsUnmanagedExport = Sig.Any(k => k.Text == "unmanagedexp");

                            a.Methods.Add(
                                new ILAssemblyMethod
                                {
                                    Token = p,
                                    IsStatic = IsStatic,
                                    IsUnmanagedExport = IsUnmanagedExport,
                                    NameToken = NameToken,
                                    ParameterStartToken = p.SkipWhile(k => k.Text != "(").First(),
                                    BodyStartToken = p.SkipWhile(k => k.Text != "{").First(),
                                }
                            );
                        }
                        p = p.SkipTo();
                    }
                }
            }

            return a;
        }

        public static ILAssembly ToAssembly(this IDLParserToken t)
        {
            var a = new ILAssembly();

            var q = t;

            #region AssemblyExternList
            {
                var p = q;

                while (p != null)
                {
                    var n = p.ToAssemblyExtern();

                    if (n == null)
                    {
                        p = null;
                    }
                    else
                    {
                        a.AssemblyExternList.Add(n);
                        p = n.Scope.Item2;
                    }
                }
            }
            #endregion



            return a;
        }

        private static ILAssemblyExtern ToAssemblyExtern(this IDLParserToken p)
        {
            var dot = p.SkipTo().AssertSymbol(".");
            var _assmembly = dot.Next.AssertName("assembly");
            var _extern = _assmembly.SkipTo();
            if (_extern.Text != "extern")
                return null;

            var q = new IDLParserTokenPair();

            q.Item1 = _extern.SkipTo().AssertSymbol("'");
            var _name = q.Item1.TakeWhile(k => k.Text != "'").Combine();
            q.Item2 = _name.Next.AssertSymbol("'");


            var n = new ILAssemblyExtern { Token = dot, Name = _name };

            var scope = new IDLParserTokenPair();
            n.Scope = scope;
            scope.Item1 = q.Item2.SkipTo().AssertSymbol("{");


            Func<IDLParserToken, IDLParserToken> _publickeytoken_value = __publickeytoken =>
                __publickeytoken.AssertName("publickeytoken").SkipTo().AssertSymbol("=").SkipTo().AssertSymbol("(").TakeWhile(
                    k => !((k.IsWhiteSpace && k.Next.Text == ")") || k.Text == ")")
                ).Combine();
            Func<IDLParserToken, IDLParserToken> _ver_value = __ver => __ver.AssertName("ver").SkipWhile(k => k.IsWhiteSpace).TakeWhile(k => !k.IsWhiteSpace).Combine();

            var _publickeytoken = scope.Item1.SkipTo().AssertSymbol(".").Next;

            if (_publickeytoken.Text == "publickeytoken")
            {
                n.PublicKeyToken = _publickeytoken_value(_publickeytoken);

                var _ver = n.PublicKeyToken.SkipTo().AssertSymbol(")").SkipTo().AssertSymbol(".").Next;

                n.Version = _ver_value(_ver);
            }
            else
            {
                n.Version = _ver_value(_publickeytoken);
            }

            scope.Item2 = n.Version.SkipTo().AssertSymbol("}");

            return n;
        }
    }
}
