
using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib;

#if BLOAT

namespace ScriptCoreLib.JavaScript.CSharp
{
    [Script]
    public class CodeWriter : StringWriter
    {
        [Script]
        public class RegionClass : global::System.IDisposable
        {
            CodeWriter _w;

            int _i;

            public RegionClass(CodeWriter w, string t)
            {
                _w = w;

                _w.WriteLine("#region " + t);

                _i = _w.Buffer.length;

                _w.WriteLine();


            }

            #region IDisposable Members

            public void Dispose()
            {
                _w.Prefix("  ", _i);
                _w.WriteLine();
                _w.WriteLine("#endregion");
            }

            #endregion
        }

        [Script]
        public class ScopeClass : global::System.IDisposable
        {
            CodeWriter _w;

            int _i;

            public ScopeClass(CodeWriter w)
            {
                _w = w;


                _w.WriteLine("{");

                _i = _w.Buffer.length;



            }

            #region IDisposable Members

            public void Dispose()
            {
                _w.Prefix("\t", _i);
                _w.WriteLine("}");

            }

            #endregion
        }



        public RegionClass CreateRegion(string t)
        {
            return new RegionClass(this, t);

        }

        public ScopeClass CreateScope()
        {
            return new ScopeClass(this);

        }

        public void WriteSummary(string e)
        {
            WriteLine("/// <summary>");
            WriteLine("/// " + e);
            WriteLine("/// </summary>");
        }



        public void WriteParam(string z, string xxx)
        {
            WriteLine("/// <param name=\"" + z + "\">" + xxx + "</param>");
        }


        public void WriteLineComment(string p)
        {
            WriteLine("// " + p);
        }



        public void ToConsole()
        {
            Console.WriteLine(this.GetString());
        }

        public void WriteSpace()
        {
            this.Write(" ");
        }

        public void WriteT()
        {
            this.WriteLine(";");
        }

        public string ConvertToTypeString(Expando p)
        {
            if (p.IsBoolean) return "bool";
            if (p.IsDouble) return "double";
            if (p.IsNumber) return "int";
            if (p.IsString) return "string";
            if (p.IsObject) return "object";

            return "var";
        }

        public enum SymbolModifier
        {
            Unknown,

            Public,
            Private,
            Protected,
            Internal
        }

        public void WriteModifier(SymbolModifier m)
        {
            if (m == SymbolModifier.Public)
                Write("public");

            if (m == SymbolModifier.Private)
                Write("private");

            if (m == SymbolModifier.Internal)
                Write("internal");
        }

        public void WriteT(string p)
        {
            this.Write(p);
            this.WriteT();
        }
    }
}

#endif