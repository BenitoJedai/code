using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler
    {

        public override bool CompileType(Type z)
        {
            if (IsNativeType(z))
                return false;

            WriteIdent();
            Write("package " + NamespaceFixup(z.Namespace));
            WriteLine();

            using (CreateScope())
            {
                WriteImportTypes(z);

                WriteLine();

                var za = ScriptAttribute.Of(z, true);

                if (ScriptAttribute.IsAnonymousType(z))
                    za = new ScriptAttribute();

                #region type summary
                var u = GetXMLNode(z);

                if (u != null)
                    WriteBlockComment(u["summary"].InnerText);
                #endregion

                if (za.IsDebugCode)
                {
                    WriteIdent();
                    WriteCommentLine("[Script(IsDebugCode = true)]");
                }
                WriteCustomAttributes(z);
                WriteTypeSignature(z, za);

                var cctor = default(Action);

                using (CreateScope())
                {
                    if (z.IsDelegate())
                    {
                        this.WriteDelegate(z, za);
                    }
                    else
                    {
                        WriteTypeFields(z, za);
                        WriteLine();

                        if (ScriptAttribute.IsAnonymousType(z))
                        {
                            WriteTypeInstanceConstructors(z);
                            WriteLine();

                            foreach (var p in z.GetProperties())
                            {
                                var GetMethod = p.GetGetMethod();

                                WriteMethodSignature(GetMethod, false);
                                WriteMethodBody(GetMethod);

                            }


                            var ToString = z.GetMethod("ToString");

                            WriteMethodSignature(ToString, false);
                            WriteMethodBody(ToString);
                        }
                        else
                        {
                            if (za.ImplementationType == null)
                            {
                                // there is another type that needs to be created

                                WriteTypeInstanceConstructors(z);
                                WriteLine();

                                WriteTypeInstanceMethods(z, za);
                                WriteLine();
                            }
                            else
                            {
                                WriteIdent();
                                WriteCommentLine("this class is just extending another class via static members");
                            }
                        }

                        WriteTypeStaticMethods(z, za);
                        WriteLine();

                        cctor = WriteTypeStaticConstructor(z, za);
                        WriteLine();

                        if (!z.IsInterface)
                        {
                            WriteInterfaceMappingMethods(z);
                        }

                        WriteVirtualMethodOverrides(z);
                    }

                }

                if (cctor != null)
                    cctor();
            }

            return true;
        }

    }
}
