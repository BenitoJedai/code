using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.IDL;

namespace ScriptCoreLib.Ultra.IL
{
    public static class ILParser
    {
        public static ILAssembly ToAssembly(this IDLParserToken t)
        {
            var a = new ILAssembly();

            var p = t;

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
