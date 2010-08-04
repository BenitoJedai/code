
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {

        private void WriteTypeStaticConstructor(Type z, ScriptAttribute za)
        {
            ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);

            if (ci.Length > 0)
            {
                if (ci.Length > 1)
                    Break("more than  static ctor?");

                this.WriteIndent();
                this.WriteKeyword(Keywords._static);
                this.WriteLine();


                foreach (ConstructorInfo m in ci)
                {
                    ILBlock cctor = new ILBlock(m);

                    WriteMethodBody(m,
                        delegate(ILBlock.Prestatement p)
                        {
                            // final fields must be final

                            if (p.Instruction != null)
                            {
                                FieldInfo f = p.Instruction.TargetField;

                                if (f != null && f.IsStatic && f.IsInitOnly)
                                {
                                    ILBlock.Prestatement xp = cctor.GetStaticFieldFinalAssignment(p.Instruction.TargetField);

                                    if (xp != null && xp.Instruction.Index == p.Instruction.Index)
                                        return true;
                                }
                            }

                            return p.Instruction == OpCodes.Ret;
                        }
                    );
                }
            }
            else
            {

                if (ScriptLibraryImport(z) != null)
                {
                    this.WriteIndent();
                    this.WriteKeyword(Keywords._static);
                    this.WriteLine();

                    using (this.CreateScope())
                    {
                        WriteIndent();

                        WriteLine("java.lang.System.loadLibrary(\"" + ScriptLibraryImport(z) + "\");");
                    }
                }
            }
        }




    }
}
