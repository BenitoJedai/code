using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ScriptCoreLib;
using System.Reflection.Emit;
using jsc.Script;
using System.Runtime.InteropServices;



namespace jsc.Languages.CSharp2
{
    partial class CSharp2Compiler
    {


        public override bool CompileType(Type z)
        {
            WriteLine("// cs2");

            WriteImportTypes(z);

            WriteNamespace(NamespaceFixup(z.Namespace),
                delegate
                {
                    // using

                    WriteIdent();
                    WriteKeywordSpace(Keywords._class);
                    WriteDecoratedTypeName(z);
                    WriteLine();

                    using (CreateScope())
                    {

                    }
                }
            );
            
            return true;
        }



        void WriteNamespace(string ns, Action e)
        {
            if (string.IsNullOrEmpty(ns))
            {
                e();
                return;
            }

            WriteIdent();
            WriteKeywordSpace(Keywords._namespace);
            Write(ns);
            WriteLine();

            using (CreateScope())
                e();
        }
    }


}
