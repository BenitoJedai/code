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
            if (z.IsAnonymousType())
                return false;

            WriteLine("// cs2 " + DateTime.Now);

            WriteImportTypes(z);

            WriteNamespaceAndDeclaringTypes(NamespaceFixup(z.Namespace), z,
                delegate
                {
                    // using

                    WriteIdent();



                    if (!z.IsEnum && !z.IsDelegate())
                        if (z.IsSealed)
                            WriteKeywordSpace(Keywords._sealed);


                    var ImplementsOrDefault = z.ToScriptAttributeOrDefault().Implements ?? z;


                    if (ImplementsOrDefault.IsPublic || ImplementsOrDefault.IsNestedPublic)
                        WriteKeywordSpace(Keywords._public);

                    if (!z.IsInterface)
                        if (z.IsAbstract)
                            WriteKeywordSpace(Keywords._abstract);

                    if (z.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).Any())
                        WriteKeywordSpace(Keywords._partial);

                    if (z.IsDelegate())
                        WriteKeywordSpace(Keywords._delegate);
                    else if (z.IsEnum)
                        WriteKeywordSpace(Keywords._enum);
                    else if (z.IsInterface)
                        WriteKeywordSpace(Keywords._interface);
                    else
                        WriteKeywordSpace(Keywords._class);



                    if (z.IsDelegate())
                    {
                        var Invoke = z.GetMethod("Invoke");

                        if (Invoke == null)
                            throw new NotSupportedException(z.Name);

                        WriteGenericTypeName(z, Invoke.ReturnType);
                        WriteSpace();

                        Write(GetShortName(z));
                        WriteGenericTypeParameters(z, z);

                        Write("(");

                        WriteMethodParameterList(Invoke);

                        Write(")");

                        WriteLine(";");
                    }
                    else
                    {
                        Write(GetShortName(z));
                        WriteGenericTypeParameters(z, z);

                        if (z.BaseType != null && z.BaseType != typeof(object))
                        {
                            if (z.IsEnum)
                            {
                                //Error	7	Type byte, sbyte, short, ushort, int, uint, long, or ulong expected	X:\jsc.svn\actionscript\Games\LightsOut\LightsOut.Client\bin\Debug\web\LightsOut\ActionScript\Shared\SharedClass1.Messages.cs	7	39	gp

                            }
                            else
                            {

                                WriteSpace();
                                Write(":");
                                WriteSpace();
                                WriteGenericTypeName(z, z.BaseType);
                            }
                        }

                        WriteLine();

                        Ident++;
                        foreach (var v in z.GetGenericArguments())
                        {
                            var ParameterConstraints = v.GetGenericParameterConstraints();

                            if (ParameterConstraints.Length > 0)
                            {
                                WriteIdent();

                                WriteKeywordSpace(Keywords._where);

                                WriteGenericTypeName(z, v);

                                Write(":");

                                for (int i = 0; i < ParameterConstraints.Length; i++)
                                {
                                    if (i > 0)
                                        Write(", ");

                                    WriteGenericTypeName(z, ParameterConstraints[i]);
                                }

                                WriteLine();
                            }
                        }
                        Ident--;

                        using (CreateScope())
                        {
                            if (z.IsEnum)
                            {
                                WriteEnumFields(z, z.ToScriptAttributeOrDefault());
                            }
                            else
                            {
                                WriteTypeEvents(z);

                                WriteTypeInstanceMethods(z, z.ToScriptAttributeOrDefault());
                                WriteLine();

                                WriteTypeProperties(z);
                                WriteLine();


                                WriteTypeFields(z, z.ToScriptAttributeOrDefault());
                            }
                        }
                    }
                }
            );

            return true;
        }

        private void WriteTypeEvents(Type z)
        {
            foreach (var p in z.GetEvents(
                BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public
                ))
            {
                MethodBase AddMethod = p.GetAddMethod(true);
                MethodBase RemoveMethod =  p.GetRemoveMethod(true) ;

                WriteIdent();

                if (AddMethod != null && AddMethod.IsStatic || RemoveMethod != null && RemoveMethod.IsStatic)
                    WriteKeywordSpace(Keywords._static);

                if (!z.IsInterface)
                    if (AddMethod != null && AddMethod.IsPublic || RemoveMethod != null && RemoveMethod.IsPublic)
                        WriteKeywordSpace(Keywords._public);


                WriteKeywordSpace(Keywords._event);

                WriteGenericTypeName(z, p.EventHandlerType);
                WriteSpace();

                WriteSafeLiteral(p.Name);
                WriteLine(";");
            }
        }

        private void WriteTypeProperties(Type z)
        {
            foreach (var p in z.GetProperties(
                BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public
                ))
            {
                WriteIdent();

                MethodBase Get = p.CanRead ? p.GetGetMethod(true) : null;
                MethodBase Set = p.CanWrite ? p.GetSetMethod(true) : null;

                if (Get != null && Get.IsStatic || Set != null && Set.IsStatic)
                    WriteKeywordSpace(Keywords._static);

                if (!z.IsInterface)
                    if (Get != null && Get.IsPublic || Set != null && Set.IsPublic)
                        WriteKeywordSpace(Keywords._public);


                WriteGenericTypeName(z, p.PropertyType);
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


        void WriteDeclaringTypes(Stack<Type> s, Action e)
        {
            var p = s.PopOrDefault();

            if (p == null)
            {
                e();
                return;
            }

            WriteIdent();
            WriteKeywordSpace(Keywords._partial);

            if (p.IsInterface)
                WriteKeywordSpace(Keywords._interface);
            else
                WriteKeywordSpace(Keywords._class);

            Write(GetShortName(p));
            WriteGenericTypeParameters(p, p);

            WriteLine();

            using (CreateScope())
            {
                WriteDeclaringTypes(s, e);
            }
        }

        void WriteNamespaceAndDeclaringTypes(string ns, Type z, Action e)
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
            {
                WriteDeclaringTypes(z.DeclaringTypesToStack(), e);
            }
        }
    }


}
