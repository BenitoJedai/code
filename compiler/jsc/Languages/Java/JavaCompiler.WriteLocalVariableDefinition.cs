
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
		public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
		{
            var LocalType = v.LocalType;
            Action WriteName = () => WriteVariableName(u.DeclaringType, u, v);

            WriteLocalVariableDefinition(LocalType, WriteName);
		}

        public void WriteLocalVariableDefinition(Type LocalType, Action WriteName, bool Initialize = false)
        {
            WriteIndent();

            WriteDecoratedTypeNameOrImplementationTypeName(LocalType, true, true);
            WriteSpace();

            //WriteVariableType(v.LocalType, true);

            WriteName();

            if (LocalType == typeof(IntPtr) || (LocalType.IsValueType && !LocalType.IsPrimitive && !LocalType.IsEnum))
            {
                var z = MySession.ResolveImplementation(LocalType) ?? LocalType;

                // define default ctor
                if (z.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new Type[0], null) == null)
                    Break("valuetype " + z.ToString() + " - " + z.Namespace + "." + z.Name + " must define a default .ctor");


                WriteAssignment();
                WriteKeywordSpace(Keywords._new);
                WriteDecoratedTypeNameOrImplementationTypeName(z, true, true);
                Write("()");
            }
            else
            {
                if (Initialize)
                {
                    // TODO: more defaults. Maybe even implement something like Write(default(T));
                    if (LocalType == typeof(bool))
                    {
                        this.WriteAssignment();
                        this.WriteKeyword(JavaCompiler.Keywords._false);
                    }
                    else if (LocalType == typeof(int))
                    {
                        this.WriteAssignment();
                        this.Write("0");
                    }
                    else if (LocalType.IsClass | LocalType.IsInterface)
                    {
                        this.WriteAssignment();
                        this.WriteKeyword(JavaCompiler.Keywords._null);
                    }
                }
            }

            WriteLine(";");
        }

    }
}
