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
            WriteLine("// cs2 " + DateTime.Now);

            WriteImportTypes(z);

            WriteNamespace(NamespaceFixup(z.Namespace),
                delegate
                {
                    // using

                    WriteIdent();

                    if (z.IsSealed)
                        WriteKeywordSpace(Keywords._sealed);

                    if (z.IsPublic)
                        WriteKeywordSpace(Keywords._public);

                    if (z.IsAbstract)
                        WriteKeywordSpace(Keywords._abstract);

                    WriteKeywordSpace(Keywords._partial);
                    WriteKeywordSpace(Keywords._class);

                    WriteGenericTypeName(z, z);

                    if (z.BaseType != null && z.BaseType != typeof(object))
                    {
                        WriteSpace();
                        Write(":");
                        WriteSpace();
                        WriteGenericTypeName(z, z.BaseType);
                    }

                    WriteLine();

                    using (CreateScope())
                    {
                        WriteTypeInstanceMethods(z, z.ToScriptAttributeOrDefault());
                        WriteLine();

                        WriteTypeProperties(z);
                    }
                }
            );
            
            return true;
        }

        private void WriteTypeProperties(Type z)
        {
            foreach (var p in z.GetProperties(
                BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public
                ))
            {
                WriteIdent();

                MethodBase Get =  p.CanRead ? p.GetGetMethod(true) : null;
                MethodBase Set =  p.CanWrite ? p.GetSetMethod(true) : null;

                if (Get != null && Get.IsStatic || Set != null && Set.IsStatic)
                    WriteKeywordSpace(Keywords._static);

                if (Get != null && Get.IsPublic || Set != null && Set.IsPublic)
                    WriteKeywordSpace(Keywords._public);

                
                WriteQualifiedTypeName(z, p.PropertyType);
                WriteSpace();

                WriteSafeLiteral(p.Name);
                WriteLine();

                using (CreateScope())
                {
                    if (Get != null)
                    {
                        WriteMethodSignature(Get, false);

                        if (!Get.IsAbstract)
                            WriteMethodBody(Get);
                    }

                    if (Set != null)
                    {
                        WriteMethodSignature(Set, false);

                        if (!Set.IsAbstract)
                            WriteMethodBody(Set);
                    }
                }
            }
        }

        private void WriteGenericTypeName(Type context, Type subject)
        {
            WriteQualifiedTypeName(context, subject);
            WriteGenericTypeParameters(context, subject);
        }


        public override void WriteGenericTypeParameters(Type context, Type subject)
        {
            if (!subject.IsGenericType)
                return;

            var p = subject.GetGenericArguments();

            Write("<");

            for (int i = 0; i < p.Length; i++)
            {
                if (i > 0)
                    Write(", ");

                WriteGenericTypeName(context, p[i]);
            }


            Write(">");
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
