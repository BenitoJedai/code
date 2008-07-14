
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
    // http://www.javacamp.org/javavscsharp/constructor.html
    // http://www4.ncsu.edu/~kaltofen/courses/Languages/JavaExamples/cpp_vs_java/
    public class JavaCompiler : Script.CompilerCLike
    {
        public static string FileExtension = "java";

        public override ScriptType GetScriptType()
        {
            return ScriptType.Java;
        }

        public override Type[] GetActiveTypes()
        {
            return MySession.Types;
        }

        public override bool CompileToSingleFile
        {
            get
            {
                return false;
            }
        }

        public override bool IsUTF8SupportedInLiterals()
        {
            return true;
        }

        public readonly AssamblyTypeInfo MySession;

        public JavaCompiler(TextWriter xw, AssamblyTypeInfo xs)
            : base(xw)
        {

            MySession = xs;

            CreateInstructionHandlers();

        }


        private void CreateInstructionHandlers()
        {


            #region elem_ref
            CIW[OpCodes.Ldelem_Ref,
                OpCodes.Ldelem_U1,
                OpCodes.Ldelem_U2,
                OpCodes.Ldelem_I1,
                OpCodes.Ldelem_I4,
                OpCodes.Ldelem_I8,
                OpCodes.Ldelem
                ] =
                e =>
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    Emit(e.p, s[0]);
                    Write("[");
                    Emit(e.p, s[1]);
                    Write("]");
                };

            CIW[OpCodes.Stelem_Ref,
                OpCodes.Stelem_I1,
                OpCodes.Stelem_I2,
                OpCodes.Stelem_I4
                ] =
                e =>
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    Emit(e.p, s[0]);
                    Write("[");
                    Emit(e.p, s[1]);
                    Write("]");
                    WriteAssignment();

                    Emit(e.p, s[2]);
                };
            #endregion

            CIW[OpCodes.Leave,
                OpCodes.Leave_S] = delegate { BreakToDebugger("return from within try block not yet supported"); };


            CIW[
                OpCodes.Br_S,
                OpCodes.Br] =
                delegate(CodeEmitArgs e)
                {
                    // adjusted for inline assigment

                    if (e.i.TargetFlow.Branch == OpCodes.Ret)
                    {
                        WriteReturn(e.p, e.i);
                    }
                    else Break("invalid br opcode");
                };


            CIW[OpCodes.Neg] =
                delegate(CodeEmitArgs e)
                {
                    Write("(-(");
                    EmitFirstOnStack(e);
                    Write("))");
                };

            #region Unbox_Any
            CIW[OpCodes.Unbox_Any] =
                e =>
                {
                    if (e.i.TargetType == typeof(int))
                    {
                        Write("((Integer)");
                        EmitFirstOnStack(e);
                        Write(").intValue()");

                        return;
                    }

                    if (e.i.TargetType == typeof(long))
                    {
                        Write("((Long)");
                        EmitFirstOnStack(e);
                        Write(").longValue()");

                        return;
                    }

                    if (e.i.TargetType == typeof(double))
                    {
                        Write("((Double)");
                        EmitFirstOnStack(e);
                        Write(").doubleValue()");

                        return;
                    }

                    if (e.i.TargetType == typeof(bool))
                    {
                        Write("((Boolean)");
                        EmitFirstOnStack(e);
                        Write(").booleanValue()");
                        return;
                    }


                    WriteBoxedComment("unbox " + e.i.TargetType.Name);
                    EmitFirstOnStack(e);
                };
            #endregion

            #region passthru

            CIW[
                OpCodes.Pop] = CodeEmitArgs.DelegateEmitFirstOnStack;

            CIW[
                OpCodes.Ldtoken] = delegate(CodeEmitArgs e)
            {
                if (e.i.TargetType == null)
                    throw new NotSupportedException("ldtoken");

                WriteDecoratedTypeName(e.i.TargetType);
            };

            CIW[OpCodes.Isinst] = delegate(CodeEmitArgs e)
            {
                throw new NotSupportedException("a custom TryCast is not yet implemented");
            };

            #endregion

            CIW[OpCodes.Dup] = delegate(CodeEmitArgs e) { EmitFirstOnStack(e); };

            #region fld
            CIW[OpCodes.Ldfld] =
                e =>
                {

                    Emit(e.p, e.FirstOnStack);
                    Write(".");
                    Write(e.i.TargetField.Name);

                };

            CIW[OpCodes.Stfld] =
                e =>
                {


                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    Emit(e.p, s[0]);
                    Write(".");
                    Write(e.i.TargetField.Name);
                    WriteAssignment();

                    #region  assign boolean literal
                    if (e.i.TargetField.FieldType == typeof(bool))
                    {
                        if (e.i.StackBeforeStrict[1].StackInstructions.Length == 1)
                        {
                            if (e.i.StackBeforeStrict[1].SingleStackInstruction.TargetInteger != null)
                            {
                                if (e.i.StackBeforeStrict[1].SingleStackInstruction.TargetInteger == 0)
                                    Write("false");
                                else
                                    Write("true");

                                return;
                            }
                        }
                    }
                    #endregion

                    Emit(e.p, s[1]);
                };
            #endregion

            CIW[OpCodes.Castclass] =
                    delegate(CodeEmitArgs e)
                    {
                        //EmitFirstOnStack(e);
                        ConvertTypeAndEmit(e, GetDecoratedTypeName(e.i.TargetType, true, false));
                        //Write("((");

                        //WriteDecoratedTypeName(e.i.TargetType);
                        //Write(")");
                        //EmitFirstOnStack(e);
                        //Write(")");

                    };

            CIW[OpCodes.Box] =
                delegate(CodeEmitArgs e)
                {
                    Write("new ");
                    Write(GetDecoratedTypeName(e.i.TargetType, true, false));
                    Write("(");

                    EmitFirstOnStack(e);

                    Write(")");
                };

            #region conv
            CIW[OpCodes.Conv_I1] = e => ConvertTypeAndEmit(e, "byte");
            CIW[OpCodes.Conv_U2] = e => ConvertTypeAndEmit(e, "char");
            CIW[OpCodes.Conv_I4] = e => ConvertTypeAndEmit(e, "int");

            CIW[OpCodes.Conv_I8] = e => ConvertTypeAndEmit(e, "long");
            CIW[OpCodes.Conv_U8] = e => ConvertTypeAndEmit(e, "long");

            CIW[OpCodes.Conv_R4] = e => ConvertTypeAndEmit(e, "float");
            CIW[OpCodes.Conv_R8] = e => ConvertTypeAndEmit(e, "double");

            CIW[OpCodes.Conv_U1] = e => ConvertTypeAndEmit(e, "byte");
            CIW[OpCodes.Conv_Ovf_I] = e => ConvertTypeAndEmit(e, "int");
            #endregion

            #region Ldlen
            CIW[OpCodes.Ldlen] =
                delegate(CodeEmitArgs e)
                {
                    EmitFirstOnStack(e);

                    Write(".length");
                };
            #endregion

            #region Newarr
            CIW[OpCodes.Newarr] =
                e =>
                {
                    Write("new ");


                    #region inline newarr
                    if (e.p.IsValidInlineArrayInit)
                    {
                        WriteDecoratedTypeName(e.i.TargetType);
                        WriteLine("[]");
                        Ident++;

                        using (CreateScope(false))
                        {

                            ILFlow.StackItem[] _stack = e.p.InlineArrayInitElements;

                            for (int si = 0; si < _stack.Length; si++)
                            {


                                if (si > 0)
                                {
                                    Write(",");
                                    WriteLine();
                                }

                                WriteIdent();

                                if (_stack[si] == null)
                                {
                                    if (!e.i.TargetType.IsValueType)
                                    {
                                        Write("null");
                                    }
                                    else
                                    {
                                        if (e.i.TargetType == typeof(int))
                                            Write("0");
                                        else if (e.i.TargetType == typeof(sbyte))
                                            Write("0");
                                        else
                                            BreakToDebugger("default for " + e.i.TargetType.FullName + " is unknown");
                                    }
                                }
                                else
                                {
                                    Emit(e.p, _stack[si]);
                                }

                            }

                            WriteLine();
                        };
                        Ident--;
                    }
                    #endregion
                    else
                    {
                        int rank = 0;
                        Type type = e.i.TargetType;

                        while (type.IsArray)
                        {
                            type = type.GetElementType();
                            rank++;
                        }

                        WriteDecoratedTypeName(type);
                        Write("[");
                        EmitFirstOnStack(e);
                        Write("]");

                        while (rank-- > 0)
                        {
                            Write("[");
                            Write("]");
                        }
                    }
                };
            #endregion

            CIW[OpCodes.Ldnull] =
                delegate(CodeEmitArgs e)
                {
                    Write("null");
                };

            #region Throw
            CIW[OpCodes.Throw] =
                delegate(CodeEmitArgs e)
                {
                    Write("throw");
                    WriteSpace();

                    Emit(e.p, e.FirstOnStack);
                };
            #endregion

            #region Rethrow
            CIW[OpCodes.Rethrow] =
                delegate(CodeEmitArgs e)
                {
                    Write("throw");
                    WriteSpace();
                    WriteExceptionVar();
                };
            #endregion

            #region Ldarg
            CIW[OpCodes.Ldarg_0,
                OpCodes.Ldarg_1,
                OpCodes.Ldarg_2,
                OpCodes.Ldarg_3,
                OpCodes.Ldarg_S,
                OpCodes.Ldarg] =
                e =>
                {
                    WriteMethodParameterOrSelf(e.i);
                };
            #endregion

            #region starg
            CIW[OpCodes.Starg_S,
                OpCodes.Starg] =
                e =>
                {
                    WriteMethodParameterOrSelf(e.i);
                    WriteAssignment();
                    if (EmitEnumAsStringSafe(e))
                        return;

                    Emit(e.p, e.FirstOnStack);
                };
            #endregion

            #region Stsfld
            CIW[OpCodes.Stsfld] =
               delegate(CodeEmitArgs e)
               {
                   try
                   {
                       bool _b_skip_classname = false;

                       if (e.Method.IsStatic && e.Method.MemberType == MemberTypes.Constructor)
                       {
                           if (e.i.TargetField.IsInitOnly)
                           {
                               // javac workaround

                               _b_skip_classname = true;
                           }
                       }

                       if (!_b_skip_classname)
                       {
                           WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
                           WriteTypeStaticAccessor();
                       }

                       Write(e.i.TargetField.Name);
                       WriteAssignment();

                       if (EmitEnumAsStringSafe(e))
                           return;

                       Emit(e.p, e.FirstOnStack);
                   }
                   catch (Exception exc)
                   {
                       throw exc;
                   }
               };
            #endregion


            CIW[OpCodes.Ldsfld] =
                delegate(CodeEmitArgs e)
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
                    WriteTypeStaticAccessor();
                    Write(e.i.TargetField.Name);
                };

            CIW[OpCodes.Callvirt] =
                delegate(CodeEmitArgs e)
                {
                    WriteMethodCall(e.p, e.i, e.i.TargetMethod);
                };

            #region call
            CIW[OpCodes.Call] =
                delegate(CodeEmitArgs e)
                {
                    MethodBase m = e.i.ReferencedMethod;

                    MethodBase mi = MySession.ResolveImplementation(m.DeclaringType, m);

                    if (mi != null)
                    {
                        WriteMethodCall(e.p, e.i, mi);

                        return;
                    }

                    if (m.Name == "op_Implicit")
                    {
                        ScriptAttribute sa = ScriptAttribute.Of(m.DeclaringType, false);

                        if (sa != null && sa.IsNative)
                        {
                            // that implicit call is only for to help c# conversions
                            // so we must emit first parameter

                            EmitFirstOnStack(e);
                            return;
                        }
                    }

                    WriteMethodCall(e.p, e.i, m);
                };
            #endregion



            #region Ret
            CIW[OpCodes.Ret] =
                e =>
                {
                    WriteReturn(e.p, e.i);
                };
            #endregion

            #region Newobj
            CIW[OpCodes.Newobj] =
                e =>
                {
                    WriteTypeConstruction(e);
                };
            #endregion

            #region Stloc
            CIW[OpCodes.Stloc_0,
                OpCodes.Stloc_1,
                OpCodes.Stloc_2,
                OpCodes.Stloc_3,
                OpCodes.Stloc_S,
                OpCodes.Stloc] =
                e =>
                {
                    WriteVariableName(e.i.OwnerMethod.DeclaringType, e.i.OwnerMethod, e.i.TargetVariable);

                    #region ++ --
                    if (e.FirstOnStack.StackInstructions.Length == 1)
                    {
                        ILInstruction i = e.FirstOnStack.SingleStackInstruction;

                        if (i == OpCodes.Add)
                        {
                            if (i.StackBeforeStrict[1].SingleStackInstruction.TargetInteger == 1)
                            {
                                if (i.StackBeforeStrict[0].SingleStackInstruction.IsEqualVariable(e.i.TargetVariable))
                                {
                                    Write("++");
                                    return;
                                }
                            }
                        }

                        if (i == OpCodes.Sub)
                        {
                            if (i.StackBeforeStrict[1].SingleStackInstruction.TargetInteger == 1)
                            {
                                if (i.StackBeforeStrict[0].SingleStackInstruction.IsEqualVariable(e.i.TargetVariable))
                                {
                                    Write("--");
                                    return;
                                }
                            }
                        }
                    }
                    #endregion

                    WriteAssignment();

                    if (e.i.IsFirstInFlow && e.i.Flow.OwnerBlock.IsHandlerBlock)
                    {
                        WriteExceptionVar();
                        return;
                    }

                    if (EmitEnumAsStringSafe(e))
                        return;

                    #region  assign boolean literal
                    if (e.i.TargetVariable.LocalType == typeof(bool))
                    {
                        if (e.i.StackBeforeStrict[0].IsSingle)
                        {
                            if (e.i.StackBeforeStrict[0].SingleStackInstruction.TargetInteger != null)
                            {
                                if (e.i.StackBeforeStrict[0].SingleStackInstruction.TargetInteger == 0)
                                    WriteKeywordFalse();
                                else
                                    WriteKeywordTrue();

                                return;
                            }
                        }
                    }
                    #endregion



                    EmitFirstOnStack(e);
                };
            #endregion


            #region Ldloc
            CIW[OpCodes.Ldloc_0,
                OpCodes.Ldloc_1,
                OpCodes.Ldloc_2,
                OpCodes.Ldloc_3,
                OpCodes.Ldloc_S,
                OpCodes.Ldloc,
                OpCodes.Ldloca,
                OpCodes.Ldloca_S] =
               e =>
               {
                   #region inline assigment
                   if (e.i.InlineAssigmentValue != null)
                   {
                       //WriteBoxedComment("inline");

                       Emit(e.i.InlineAssigmentValue,
                           e.i.InlineAssigmentValue.Instruction.StackBeforeStrict[0]);


                       return;
                   }
                   #endregion

                   if (e.p != null)
                       if (e.p.Owner != null)
                           if (e.p.Owner.IsCompound)
                           {
                               ILBlock.Prestatement sp = e.p.Owner.SourcePrestatement(e.p, e.i);

                               if (sp != null)
                               {
                                   EmitInstruction(sp, sp.Instruction);

                                   return;
                               }
                           }

                   WriteVariableName(e.i.OwnerMethod.DeclaringType, e.i.OwnerMethod, e.i.TargetVariable);


                   if (e.i.IsInlinePostSub) Write("--");
                   if (e.i.IsInlinePostAdd) Write("++");
               };
            #endregion


            CIW[OpCodes.Ldstr] =
                e =>
                {
                    WriteQuotedLiteral(e.i.TargetLiteral);
                };


            #region ldc
            CIW[OpCodes.Ldc_R4] =
                delegate(CodeEmitArgs e)
                {
                    WriteNumeric(e.i.OpParamAsFloat);
                };

            CIW[OpCodes.Ldc_R8] =
                delegate(CodeEmitArgs e)
                {
                    WriteNumeric(e.i.OpParamAsDouble);
                };


            CIW[OpCodes.Ldc_I4,
                OpCodes.Ldc_I4_0,
                OpCodes.Ldc_I4_1,
                OpCodes.Ldc_I4_2,
                OpCodes.Ldc_I4_3,
                OpCodes.Ldc_I4_4,
                OpCodes.Ldc_I4_5,
                OpCodes.Ldc_I4_6,
                OpCodes.Ldc_I4_7,
                OpCodes.Ldc_I4_8,
                OpCodes.Ldc_I4_M1,
                OpCodes.Ldc_I8,

                OpCodes.Ldc_I4_S] =
               delegate(CodeEmitArgs e)
               {
                   int? n = e.i.TargetInteger;

                   if (n == null)
                   {
                       // long fix

                       long? x = e.i.TargetLong;

                       if (x == null)
                       {
                           Break("ldc unresolved");
                       }
                       else
                       {
                           MyWriter.Write(x.Value);
                       }
                   }
                   else
                   {
                       MyWriter.Write(n.Value);
                   }
               };
            #endregion


            #region  operands
            CIW[OpCodes.Xor] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "^"); };
            CIW[OpCodes.Shl] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<<"); };
            CIW[OpCodes.Shr] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">>"); };

            CIW[OpCodes.Clt, OpCodes.Clt_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<"); };
            CIW[OpCodes.Cgt, OpCodes.Cgt_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">"); };

            CIW[OpCodes.Add] =
                delegate(CodeEmitArgs e)
                {
                    if (e.i.IsInlinePrefixOperator(OpCodes.Add))
                    {
                        Write("++");
                        Emit(e.p, e.FirstOnStack);
                        return;
                    }

                    WriteInlineOperator(e.p, e.i, "+");
                };

            CIW[OpCodes.Sub] =
                delegate(CodeEmitArgs e)
                {
                    if (e.i.IsInlinePrefixOperator(OpCodes.Sub))
                    {
                        Write("--");
                        EmitFirstOnStack(e);
                        return;
                    }

                    WriteInlineOperator(e.p, e.i, "-");
                };

            CIW[OpCodes.Or] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "|"); };
            CIW[OpCodes.And] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "&"); };
            CIW[OpCodes.Rem] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "%"); };
            CIW[OpCodes.Mul] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "*"); };
            CIW[OpCodes.Div] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "/"); };
            CIW[OpCodes.Bge_S,
                OpCodes.Bge] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">="); };
            CIW[OpCodes.Ble_S,
                OpCodes.Ble] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<="); };
            CIW[OpCodes.Bne_Un_S,
                OpCodes.Bne_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "!="); };
            CIW[OpCodes.Ceq] =
                delegate(CodeEmitArgs e)
                {
                    if (e.i.IsNegativeOperator)
                    {
                        Write("!");
                        Emit(e.p, e.i.StackBeforeStrict[0]);
                    }
                    else
                        WriteInlineOperator(e.p, e.i, "==");
                };
            #endregion

        }







        private void WriteTypeStaticAccessor()
        {
            Write(".");
        }



        public Type ResolveImplementation(Type t)
        {
            return MySession.ResolveImplementation(t); ;
        }

        public override MethodBase ResolveImplementationMethod(Type t, MethodBase m)
        {
            return MySession.ResolveImplementation(t, m);
        }

        public override MethodBase ResolveImplementationMethod(Type t, MethodBase m, string alias)
        {
            return MySession.ResolveMethod(m, t, alias);
        }







        public override bool CompileType(Type z)
        {
            if (IsNativeType(z))
                return false;

            if (IsEmptyImplementationType(z))
                return false;

            if (ScriptAttribute.IsAnonymousType(z))
                return false;

            //WriteMachineGeneratedWarning();

            if (z.Namespace != null)
            {
                WriteIdent();
                Write("package " + NamespaceFixup(z.Namespace) + ";");
                WriteLine();
                WriteLine();
            }

            this.WriteImportTypes(z);

            WriteLine();


            ScriptAttribute za = ScriptAttribute.Of(z, true);



            #region type summary
            XmlNode u = GetXMLNode(z);

            if (u != null)
                WriteBlockComment(u["summary"].InnerText);
            #endregion


            WriteTypeSignature(z, za);

            using (CreateScope())
            {
                WriteTypeFields(z, za);
                WriteLine();
                WriteTypeStaticConstructor(z, za);
                WriteLine();

                if (za.Implements == null)
                {
                    WriteTypeInstanceConstructors(z);
                    WriteLine();
                }

                WriteTypeInstanceMethods(z, za);
                WriteLine();
                WriteTypeStaticMethods(z, za);


            }

            //Thread.Sleep(100);

            return true;
        }



        private void WriteTypeStaticConstructor(Type z, ScriptAttribute za)
        {
            ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);

            if (ci.Length > 0)
            {
                if (ci.Length > 1)
                    Break("more than  static ctor?");

                this.WriteIdent();
                this.WriteKeywordStatic();
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
                    this.WriteIdent();
                    this.WriteKeywordStatic();
                    this.WriteLine();

                    using (this.CreateScope())
                    {
                        WriteIdent();

                        WriteLine("java.lang.System.loadLibrary(\"" + ScriptLibraryImport(z) + "\");");
                    }
                }
            }
        }

        public void WriteImportTypes(Type z)
        {
            // all field types, return types, parameter types, variable types, statics

            List<Type> t = GetImportTypes(z, true);
            List<string> imports = new List<string>();

            t.RemoveAll(delegate(Type x)
            {
                return IsEmptyImplementationType(x);
            });

            while (t.Count > 0)
            {
                Type p = t[0];

                // optimize me

                t.RemoveAll(
                    delegate(Type x)
                    {
                        return x.GUID == z.GUID || x.GUID == p.GUID;
                    }
                );



                ScriptAttribute a = ScriptAttribute.Of(p, false);

                string n = (p.Namespace == null) ? "" : p.Namespace + ".";



                if (a != null && a.Implements != null && a.ExternalTarget != null)
                {
                    if (p != z)
                    {

                        imports.Add(n + GetDecoratedTypeName(p, false, false, false));
                    }

                    imports.Add(GetDecoratedTypeName(p, true, false, false));


                }
                else
                {

                    imports.Add(n + GetDecoratedTypeName(p, true, false, false));



                }



            }

            imports.RemoveAll(
                delegate(string x)
                {
                    return System.Text.RegularExpressions.Regex.IsMatch(x, @"^java.lang.\w*$");
                }
                );

            imports.Sort(
               delegate(string x, string y)
               {
                   return x.CompareTo(y);
               });

            foreach (string var in imports)
            {
                // exclude onl
                //if (var.StartsWith("java.lang"))
                //    continue;



                WriteKeywordImport();
                Write(NamespaceFixup(var));
                WriteLine(";");

            }


        }











        public override void WriteXmlDoc(MethodBase m)
        {
            if (this.XmlDoc != null)
            {
                DebugBreak(m);

                XmlNode n = GetXMLNodeForMethod(m);

                if (n != null)
                {
                    string Summary = n["summary"].InnerText.Trim();

                    WriteBlockComment(Summary);
                }
                else
                {
                    //WriteJavaDoc(MethodSig);
                }
            }
        }





        public override void WriteMethodSignature(MethodBase m, bool dStatic)
        {
            DebugBreak(ScriptAttribute.Of(m));

            WriteIdent();

            if (m.IsAbstract)
                Write("abstract ");

            int flags = (int)m.GetMethodImplementationFlags();

            // http://blogs.msdn.com/ricom/archive/2004/05/05/126542.aspx
            // http://msdn2.microsoft.com/en-us/library/system.reflection.methodimplattributes.aspx
            if ((flags & (int)MethodImplAttributes.Synchronized) == (int)MethodImplAttributes.Synchronized)
                Write("synchronized ");


            if (m.IsPublic)
                WriteKeywordPublic();
            else
            {
                if (m.IsFamily)
                    Write("protected ");
                else
                    WriteKeywordPrivate();
            }

            if (m.IsStatic || dStatic)
                WriteKeywordStatic();
            else
            {
                if (m is MethodInfo)
                {
                    if (m.IsFinal || !m.IsVirtual)
                        Write("final ");
                }
            }

            if (ScriptIsPInvoke(m))
            {
                Write("native ");
            }

            if (m is MethodInfo)
            {
                MethodInfo mi = m as MethodInfo;

                //WriteDecoratedTypeName(mi.ReturnType);
                WriteDecoratedTypeNameOrImplementationTypeName(mi.ReturnType, true, true);

                //Write(GetDecoratedTypeNameWithinNestedName( mi.ReturnType));
                WriteSpace();
            }

            if (m.IsConstructor)
                Write(GetDecoratedTypeName(m.DeclaringType, false));
            else
                WriteDecoratedMethodName(m, false);

            Write("(");
            WriteMethodParameterList(m);

            Write(")");

            WriteMethodSignatureThrows(m);

            if (m.IsAbstract || ScriptIsPInvoke(m))
                WriteLine(";");
            else
                WriteLine();

        }

        public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
        {
            WriteIdent();

            WriteDecoratedTypeNameOrImplementationTypeName(v.LocalType, true, true);
            WriteSpace();

            //WriteVariableType(v.LocalType, true);

            WriteVariableName(u.DeclaringType, u, v);

            WriteLine(";");
        }



        public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
        {
            DebugBreak(ScriptAttribute.Of(i.OwnerMethod));

            ScriptAttribute ma = ScriptAttribute.Of(m);

            bool IsExternalDefined = ma != null && ma.ExternalTarget != null;
            bool IsDefineAsInstance = ma != null && ma.DefineAsInstance;
            bool IsBaseCall = false;
            bool IsDefineAsStatic = ma != null && ma.DefineAsStatic;

            if (m.IsConstructor)
            {
                // fixme: update the BCL resolving issue
                // the super ctor call gets lost otherwise

                if (i.IsBaseConstructorCall(m))
                {

                    IsBaseCall = true;
                }
                else
                    Break("If it was a native constructor, it should be remapped via InternalConstructor attribute.Cannot call constructor : " + m + " used at " + i.OwnerMethod.DeclaringType.FullName + "." + i.OwnerMethod.Name + ".");
            }



            ILFlow.StackItem[] s = i.StackBeforeStrict;

            int offset = 1;

            #region static call defined as instance call


            if (m.IsStatic && IsExternalDefined & IsDefineAsInstance)
            {
                // what?? string?

                Emit(p, s[0]);
                Write(".");
                WriteExternalMethod(ma.ExternalTarget, m);
                WriteParameterInfoFromStack(m, p, s, 1);

                return;
            }
            #endregion



            if ((m.IsStatic || IsDefineAsStatic) || IsBaseCall)
            {
                #region static
                //TODO: ???
                if (IsBaseCall)
                {
                    //WriteTypeBaseType();
                    //Write(".");

                }
                else
                {
                    //ScriptAttribute ta = ScriptAttribute.Of(m.DeclaringType);

                    if (IsExternalDefined)
                    {
                        //WriteBoxedComment("impl");



                        //WriteTypeOrExternalTargetTypeName(ta.Implements);

                        WriteDecoratedTypeName(ScriptAttribute.Of(m.DeclaringType).ImplementationType);
                        Write(".");

                    }
                    else
                    {
                        //WriteBoxedComment("ext");

                        WriteTypeOrExternalTargetTypeName(m.DeclaringType);
                        Write(".");
                    }
                }
                #endregion

                offset = !m.IsStatic && (IsDefineAsStatic || IsBaseCall) ? 1 : 0;
            }
            else
            {

                // WriteBoxedComment("variable.call");

                // base. ?

                if (i.OpCode == OpCodes.Call &&
                    s[0].SingleStackInstruction == OpCodes.Ldarg_0 &&
                    i.OwnerMethod.DeclaringType.BaseType == m.DeclaringType)
                {
                    Write("super");
                }
                else
                {
                    Emit(p, s[0]);
                }

                Write(".");
            }



            if (IsExternalDefined)
            {
                WriteExternalMethod(ma.ExternalTarget, m);
            }
            else
            {
                if (IsBaseCall)
                {
                    Write("super");
                }
                else
                {
                    WriteDecoratedMethodName(m, false);
                }
            }

            WriteParameterInfoFromStack(m, p, s, offset);

        }

        public void WriteExternalMethod(string p, MethodBase m)
        {
            if (p.Contains("*"))
            {
                foreach (PropertyInfo v in m.DeclaringType.GetProperties())
                {
                    if (v.GetGetMethod() == m || v.GetSetMethod() == m)
                    {
                        Write(p.Replace("*", v.Name));

                        return;
                    }
                }

                throw new NotSupportedException("The use of * is only allowed on properties to capture its name.");

            }
            else
            {
                Write(p);
            }
        }

        public override void MethodCallParameterTypeCast(Type context, ParameterInfo p)
        {
            Write("(");
            WriteDecoratedTypeName(p.ParameterType);
            Write(")");
        }

        private void WriteTypeOrExternalTargetTypeName(Type m)
        {


            string x = ScriptGetExternalTarget(m);

            if (x == null)
                Write(GetDecoratedTypeName(m, false));
            else
                Write(x);

        }


        public void WriteTypeNameAsMemberName(Type e)
        {
            if (e.IsArray)
            {
                Write("ArrayOf");

                WriteTypeNameAsMemberName(e.GetElementType());

                return;
            }

            WriteDecoratedTypeName(e);
        }

        public override void WriteDecoratedMethodName(MethodBase z, bool q)
        {
            if (q)
                Write("\"");

            if (z.Name == "ToString" && !z.IsStatic)
                Write("toString");
            else
            {
                if (z.Name == "op_Implicit")
                {


                    Type rt = ((MethodInfo)z).ReturnType;

                    if (rt == z.DeclaringType)
                    {
                        // name clash?

                        Write("Of");

                        //Write("From");
                        //WriteTypeNameAsMemberName(z.GetParameters()[0].ParameterType);
                    }
                    else
                    {
                        Write("To");

                        if (rt.IsPrimitive)
                            Write("_");

                        WriteTypeNameAsMemberName(rt);

                    }

                }
                else
                {
                    Write(z.Name);
                }

            }

            if (q)
                Write("\"");
        }

        private void WriteMethodSignatureThrows(MethodBase m)
        {
            DebugBreak(ScriptAttribute.Of(m));

            List<Type> list = GetMethodExceptions(m);

            if (list.Count > 0)
            {
                WriteSpace();
                WriteKeywordThrows();

                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                        Write(", ");

                    WriteVariableType(list[i], false);

                }
            }
        }

        private List<Type> GetMethodExceptions(MethodBase m)
        {
            DebugBreak(ScriptAttribute.Of(m));

            List<Type> list = new List<Type>();

            GetMethodExceptionsFromAttribute(m, list);

            if (!m.IsAbstract)
            {
                FindThrownExceptions(new ILBlock(m), list);
            }

            return list;
        }

        private static void GetMethodExceptionsFromAttribute(MethodBase m, List<Type> list)
        {
            ScriptMethodThrows[] throws = ScriptMethodThrows.ArrayOfProvider(m);

            if (throws.Length > 0)
            {
                for (int i = 0; i < throws.Length; i++)
                {
                    list.Add(throws[i].ThrowType);
                }
            }
        }

        private void FindThrownExceptions(ILBlock b, List<Type> list)
        {



            foreach (ILBlock.Prestatement x in b.Prestatements.PrestatementCommands)
            {
                if (x.Block != null)
                {
                    FindThrownExceptions(x.Block, list);
                }

                // todo: declare uncatched native method declared throws automatically

                //if (x.Instruction != null)
                //{
                //    MethodBase nmethod = x.Instruction.TargetMethod;

                //    if (nmethod != null)
                //    {
                //        Type ntype = nmethod.DeclaringType;

                //        ScriptAttribute ntypea = ScriptAttribute.Of(ntype);

                //        if (ntype != null && ntypea.IsNative)
                //        {
                //            // add native throws

                //            GetMethodExceptionsFromAttribute(nmethod, list);
                //        }
                //    }
                //}

                if (x.Instruction == OpCodes.Throw)
                {
                    ILInstruction[] s = x.Instruction.StackBeforeStrict[0].StackInstructions;

                    // we mus walk up the stack to find out if anyone is throwing anything

                    foreach (ILInstruction xs in s)
                    {
                        ILBlock o = xs.Flow.OwnerBlock;

                        Type cexc = xs.ReferencedType;



                        if (cexc == null)
                            Break("unable to detect thrown exception");
                        else
                            FindThrownExceptionsInCatchBlock(list, cexc, o);


                    }
                }
            }
        }

        private void FindThrownExceptionsInCatchBlock(List<Type> list, Type cexc, ILBlock o)
        {
            if (o.IsRoot || o.Parent.IsRoot)
            {
                if (!list.Contains(cexc))
                    list.Add(cexc);
            }
            else
            {
                if (!FindThrownExceptionsInTryBlock(list, cexc, o.Parent))
                    Break("unable to detect thrown exception");
            }
        }

        private bool FindThrownExceptionsInTryBlock(List<Type> list, Type cexc, ILBlock o)
        {
            bool bIsTry = false;

            if (o.IsTryBlock)
            {
                // we might have a protecting catch clause

                if (o.Next.IsHandlerBlock)
                {
                    if (o.Next.Clause.Flags == ExceptionHandlingClauseOptions.Clause)
                    {
                        if (cexc.IsSubclassOf(o.Next.Clause.CatchType) || o.Next.Clause.CatchType == cexc)
                        {
                            // safe
                        }
                        else
                        {
                            FindThrownExceptionsInCatchBlock(list, cexc, o.Next);
                        }
                    }
                    else
                        Break("catch block excpected");
                }
                else
                    Break("malformed try/catch block");

                bIsTry = true;
            }
            return bIsTry;
        }



        public override bool EmitTryBlock(ILBlock.Prestatement p)
        {
            if (p.Block.IsTryBlock)
            {

                WriteIdent();
                WriteLine("try");


                ILBlock.PrestatementBlock b = p.Block.Prestatements;

                bool _pop = false;
                bool _leave = b.Last == OpCodes.Leave_S && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First;

                EmitScope(b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last));


            }
            else if (p.Block.IsHandlerBlock)
            {


                WriteIdent();



                ILBlock.PrestatementBlock b = p.Block.Prestatements;

                bool _pop = b.First == OpCodes.Pop && (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause);
                bool _leave =
                    b.Last == OpCodes.Endfinally
                ||
                    (b.Last == OpCodes.Leave_S && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First);

                b = b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last);

                b.RemoveNopOpcodes();

                if (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause)
                {
                    DebugBreak(p.DeclaringMethod.ToScriptAttribute());

                    Write("catch (");

                    if (p.Block.Clause.CatchType == typeof(object))
                    {
                        Write("java.lang.Throwable");
                        WriteSpace();
                        WriteExceptionVar();
                    }
                    else
                    {
                        var ExceptionType = MySession.ResolveImplementation(p.Block.Clause.CatchType) ?? p.Block.Clause.CatchType;
                        var ExceptionTypeAttribute = ExceptionType.ToScriptAttribute();

                        if (ExceptionTypeAttribute != null && ExceptionTypeAttribute.ImplementationType != null)
                            Write(GetDecoratedTypeName(ExceptionTypeAttribute.ImplementationType, true));
                        else
                            Write(GetDecoratedTypeName(ExceptionType, true));

                        WriteSpace();

                        ILBlock.Prestatement set_exc = p.Block.Prestatements.PrestatementCommands[0];
                        WriteVariableName(p.Block.OwnerMethod.DeclaringType, p.Block.OwnerMethod, set_exc.Instruction.TargetVariable);

                        // remove the set command if there is one
                        if (set_exc.Instruction.TargetVariable != null)
                            b.PrestatementCommands.RemoveAt(0);

                    }


                    WriteLine(")");

                    EmitScope(b);
                }
                else
                {
                    WriteLine("finally");
                    EmitScope(b);
                }

                // additional space
                WriteLine();
            }
            else
            {
                return false;
            }

            return true;

        }






        public override void WriteSelf()
        {
            Write("that");
        }



        public override void WriteMethodParameterList(MethodBase m)
        {
            ParameterInfo[] mp = m.GetParameters();

            ScriptAttribute ma = ScriptAttribute.Of(m);

            bool bStatic = (ma != null && ma.DefineAsStatic);

            if (bStatic)
            {
                if (m.IsStatic)
                {
                    Break("method is already static, but is marked to be declared out of band : " + m.DeclaringType.FullName + "." + m.Name);
                }


                DebugBreak(ma);


                ScriptAttribute sa = ScriptAttribute.Of(m.DeclaringType, false);

                if (sa.Implements == null)
                {
                    WriteDecoratedTypeName(m.DeclaringType);

                }
                else
                {
                    WriteDecoratedTypeName(sa.Implements);
                }

                // this parameter is on the argument list

                WriteSpace();
                WriteSelf();
            }

            for (int mpi = 0; mpi < mp.Length; mpi++)
            {
                if (mpi > 0 || bStatic)
                {
                    Write(",");
                    WriteSpace();
                }

                ParameterInfo p = mp[mpi];

                ScriptAttribute za = ScriptAttribute.Of(m.DeclaringType, true);

                if (za.Implements == null || m.DeclaringType.GUID != p.ParameterType.GUID)
                    WriteVariableType(p.ParameterType, true);
                else
                    WriteVariableType(za.Implements, true);

                Write(p.Name);
            }
        }

        public void WriteVariableType(Type t, bool bSpace)
        {

            Write(GetDecoratedTypeName(t, true, true, true));

            if (bSpace)
                WriteSpace();
        }


        public override void WriteTypeFields(Type z, ScriptAttribute za)
        {
            FieldInfo[] zf = GetAllFields(z);

            foreach (FieldInfo zfn in zf)
            {
                // external class cannot have static variables inside a type
                // should be defined outside as global static instead
                if (za.HasNoPrototype && !zfn.IsStatic)
                    continue;

                if (zfn.IsLiteral)
                    continue;

                WriteIdent();
                WriteTypeFieldModifier(zfn);

                WriteDecoratedTypeNameOrImplementationTypeName(zfn.FieldType, true, true);
                WriteSpace();

                //WriteVariableType(zfn.FieldType, true);
                Write(zfn.Name);

                if (zfn.IsStatic && zfn.IsInitOnly)
                {
                    ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);

                    if (ci.Length == 1)
                    {
                        ILBlock cctor = new ILBlock(ci[0]);
                        ILBlock.Prestatement assign = cctor.GetStaticFieldFinalAssignment(zfn);

                        if (assign != null)
                        {
                            WriteAssignment();

                            EmitFirstOnStack(assign);
                        }
                    }
                }

                WriteLine(";");
            }
        }

        public override void WriteTypeFieldModifier(FieldInfo zfn)
        {
            if (zfn.IsPublic)
                WriteKeywordPublic();
            else
            {
                if (zfn.IsFamily)
                    Write("protected ");
                else
                    WriteKeywordPrivate();
            }

            if (zfn.IsInitOnly)
                WriteKeywordFinal();

            if (zfn.IsStatic)
                WriteKeywordStatic();

            if (zfn.IsNotSerialized)
                Write("transient ");
        }

        #region keywords
        private void WriteKeywordStatic()
        {
            Write("static ");
        }

        private void WriteKeywordImport()
        {
            Write("import ");
        }


        private void WriteKeywordFinal()
        {
            Write("final ");
        }

        private void WriteKeywordPrivate()
        {
            Write("private ");
        }

        private void WriteKeywordPublic()
        {
            Write("public ");
        }

        private void WriteKeywordClass()
        {
            Write("class ");
        }

        private void WriteKeywordThrows()
        {
            Write("throws ");
        }

        #endregion

        public override string GetDecoratedTypeNameWithinNestedName(Type z)
        {
            return GetDecoratedTypeName(z, false, false, false);
        }

        public override void WriteTypeSignature(Type z, ScriptAttribute za)
        {
            WriteIdent();



            if (za.Implements != null || z.IsPublic || z.IsNestedPublic)
                WriteKeywordPublic();
            //else
            //    WriteKeywordPrivate();


            if (z.IsAbstract && !z.IsSealed)
                Write("abstract ");

            if (z.IsSealed)
                Write("final ");
            else
            {
                // Shall we seal all nonused objects?
            }

            if (z.IsInterface)
                WriteKeywordInterface();
            else
                WriteKeywordClass();

            if (za.Implements == null)
                Write(GetDecoratedTypeNameWithinNestedName(z));
            else
                Write(GetDecoratedTypeName(z, false));


            #region extends
            if (z.BaseType != typeof(object) && z.BaseType != null)
            {

                Write(" extends ");

                ScriptAttribute ba = ScriptAttribute.Of(z.BaseType, true);

                if (ba == null)
                    Break("extending object has no attribute");


                if (ba.Implements == null)
                    WriteDecoratedTypeName(z.BaseType);
                else
                    Write(GetDecoratedTypeName(z.BaseType, false));

            }
            #endregion

            #region implements
            Type[] timp = z.GetInterfaces();

            if (timp.Length > 0)
            {
                Write(" implements ");

                int i = 0;

                DebugBreak(za);

                foreach (Type timpv in timp)
                {
                    if (i++ > 0)
                        Write(", ");

                    WriteDecoratedTypeNameOrImplementationTypeName(timpv);
                }
            }
            #endregion

            WriteLine();
        }

        private void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv)
        {
            WriteDecoratedTypeNameOrImplementationTypeName(timpv, false, false);
        }

        /// <summary>
        /// tries to use the implementation name
        /// </summary>
        /// <param name="timpv"></param>
        /// <param name="favorTargetType"></param>
        private void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv, bool favorPrimitives, bool favorTargetType)
        {
            //[Script(Implements = typeof(global::System.Boolean),
            //    ImplementationType=typeof(java.lang.Integer))]


            Type iType = ResolveImplementation(timpv);

            if (iType != null)
            {
                if (favorTargetType)
                {
                    if (ScriptAttribute.OfProvider(iType).ImplementationType != null)
                        iType = null;
                }
            }

            if (iType == null)
                Write(GetDecoratedTypeName(timpv, true, favorPrimitives, true));
            else
                Write(GetDecoratedTypeName(iType, true));
        }

        private void WriteKeywordInterface()
        {
            Write("interface ");
        }


        public override void WriteDecoratedMethodParameter(ParameterInfo p)
        {
            Write(p.Name);
        }

        string ToJavaTypeName(string e)
        {
            e = e.Replace("`", "_");
            e = e.Replace("<", "_");
            e = e.Replace(">", "_");

            return e;
        }

        // http://www.idevelopment.info/data/Programming/java/miscellaneous_java/Java_Primitive_Types.html
        public override string GetDecoratedTypeName(Type type, bool bExternalAllowed)
        {
            if (type == null)
                return "null";

            return GetDecoratedTypeName(type, bExternalAllowed, true);

        }

        public string GetDecoratedTypeName(Type type, bool bExternalAllowed, bool bUsePrimitives)
        {
            return GetDecoratedTypeName(type, bExternalAllowed, bUsePrimitives, true);
        }

        public string GetDecoratedTypeName(Type type, bool bExternalAllowed, bool bUsePrimitives, bool bChopNestedParents)
        {
            if (type.IsArray)
            {
                return GetDecoratedTypeName(type.GetElementType(), bExternalAllowed, bUsePrimitives, bChopNestedParents) + "[]";
            }

            ScriptAttribute a = ScriptAttribute.Of(type, true);


            Type __impl = ResolveImplementation(type);

            if (bExternalAllowed && a == null && __impl != null)
            {
                a = ScriptAttribute.Of(__impl);
            }

            if (bExternalAllowed && a != null && a.ExternalTarget != null)
            {
                return a.ExternalTarget;
            }
            else
            {
                if (type.IsNested)
                {
                    List<string> x = new List<string>();

                    Type p = type;

                    if (bChopNestedParents && a != null && a.IsNative)
                        return type.Name;

                    while (p != null)
                    {
                        x.Add(ToJavaTypeName(p.Name));


                        p = p.DeclaringType;
                    }



                    x.Reverse();

                    // if they are native java inner types
                    // we need to use .

                    // custom nested classes are refactored as
                    // separate classes by _

                    if (a == null)
                        BreakToDebugger("typename");

                    return string.Join(
                        a.IsNative
                        ? "."
                        : "_", x.ToArray());
                }
                else
                {
                    if (bUsePrimitives)
                    {
                        if (type == typeof(void)) return "void";
                        else if (type == typeof(int)) return "int";
                        else if (type == typeof(double)) return "double";
                        else if (type == typeof(bool)) return "boolean";
                        else if (type == typeof(long)) return "long";
                        else if (type == typeof(byte)) Break("java does not support unsigned bytes");
                        else if (type == typeof(sbyte)) return "byte";
                        else if (type == typeof(char)) return "char";
                        else if (type == typeof(short)) return "short";
                        else if (type == typeof(float)) return "float";
                        else if (type == typeof(double)) return "double";

                        if (type.IsArray)
                        {
                            if (type.GetElementType() == typeof(sbyte))
                                return "byte[]";
                            else if (type.GetElementType() == typeof(float))
                                return "float[]";
                        }
                    }
                    else
                    {
                        // http://www.dotnetspider.com/tutorials/Datatypes.aspx
                        // box for java

                        if (type == typeof(int))
                            return "Integer";
                        if (type == typeof(long))
                            return "Long";
                        if (type == typeof(sbyte))
                            return "Byte";
                        if (type == typeof(float))
                            return "Float";
                    }

                    return ToJavaTypeName(type.Name);
                }
            }
        }

        private List<Type> GetImportTypes(Type t, bool bExcludeJavaLang)
        {


            List<Type> imp_types = new List<Type>();
            List<Type> imp = new List<Type>();

            Type[] tinterfaces = t.GetInterfaces();

            foreach (Type tinterface in tinterfaces)
                imp.Add(tinterface);

            Type bp = t.BaseType;

            while (bp != typeof(object) &&
                    bp != null)
            {
                imp.Add(bp);
                bp = bp.BaseType;
            }

            foreach (FieldInfo v in this.GetAllFields(t))
            {
                imp.Add(v.FieldType);
            }

            foreach (MethodBase v in GetAllInstanceConstructors(t))
            {


                GetImportTypesFromMethod(t, imp, v);
            }


            foreach (MethodInfo mi in this.GetAllMethods(t))
            {
                imp.Add(mi.ReturnParameter.ParameterType);

                MethodBase v = mi;

                GetImportTypesFromMethod(t, imp, v);
            }

            while (imp.Count > 0)
            {
                Type p = imp[0];

                imp.RemoveAll(
                    delegate(Type w)
                    {


                        if (w.IsArray && p.IsArray)
                        {
                            return w.GetElementType().GUID == p.GetElementType().GUID;
                        }

                        return w.GUID == p.GUID;
                    }
                );

                // exludeonly java lang classname
                //if (p.Namespace != null)
                //    if (bExcludeJavaLang && p.Namespace.StartsWith("java.lang")) 
                //        continue;

                // todo fix additional types handling

                while (p.IsArray)
                {
                    p = p.GetElementType();

                }

                if (p == typeof(object)) continue;
                if (p == typeof(void)) continue;
                if (p == typeof(string)) continue;
                if (p == typeof(int)) continue;
                if (p == typeof(short)) continue;
                if (p == typeof(long)) continue;
                if (p == typeof(float)) continue;
                if (p == typeof(double)) continue;

                if (p == typeof(byte))
                {
                    Break("use SByte instead - java does not support unsigned bytes at " + t.FullName);

                    continue;
                }

                if (p == typeof(sbyte)) continue;
                if (p == typeof(bool)) continue;
                if (p == typeof(char)) continue;

                ScriptAttribute a = ScriptAttribute.Of(p, true);

                if (a == null)
                {
                    Type p_impl = MySession.ResolveImplementation(p);

                    if (p_impl == null)
                    {
                        if (ScriptAttribute.IsCompilerGenerated(p))
                        {
                            // pass thru..

                            continue;
                        }
                        else
                        {
                            Break("class import: no implementation for " + p.FullName + " at " + t.FullName);
                        }
                    }

                    p = p_impl;
                    a = ScriptAttribute.Of(p, true);
                }


                imp_types.Add(p);


            }


            return imp_types;
        }

        private void GetImportTypesFromMethod(Type t, List<Type> imp, MethodBase v)
        {

            ScriptAttribute vs = ScriptAttribute.OfProvider(v);


            // DebugBreak(vs);

            if (vs != null && vs.DefineAsStatic)
                imp.Add(t);

            DebugBreak(vs);

            imp.AddRange(GetMethodExceptions(v));

            foreach (ParameterInfo p in v.GetParameters())
            {
                imp.Add(p.ParameterType);
            }

            if (v.IsAbstract)
                return;

            foreach (LocalVariableInfo l in v.GetMethodBody().LocalVariables)
            {
                imp.Add(l.LocalType);
            }

            ILBlock b = new ILBlock(v);

            foreach (ILInstruction i in b.Instructrions)
            {

                if (i.ReferencedMethod != null)
                {
                    if (!IsTypeOfOperator(i.ReferencedMethod))
                        if (i.ReferencedMethod.DeclaringType != typeof(object))
                        {
                            MethodBase method = GetMethodImplementation(MySession, i);
                            ScriptAttribute method_attribute = ScriptAttribute.OfProvider(method);


                            if (method.IsConstructor || method.IsStatic || (method_attribute != null && method_attribute.DefineAsStatic))
                            {
                                imp.Add(method.DeclaringType);
                                continue;
                            }
                        }
                }

                if (i == OpCodes.Box)
                {
                    imp.Add(i.TargetType);
                    continue;
                }

                if (i.TargetField != null)
                {
                    if (i.TargetField.IsStatic)
                    {
                        imp.Add(i.TargetField.DeclaringType);
                        continue;
                    }
                }
            }
        }



        public override Predicate<ILBlock.Prestatement> MethodBodyFilter
        {
            get
            {
                return
                 delegate(ILBlock.Prestatement p)
                 {
                     #region remove redundant returns
                     if (p.Instruction != null)
                         if (p.Instruction == OpCodes.Ret)
                             if (p.Instruction.Next == null)
                                 if (p.Instruction.StackBeforeStrict.Length == 0)
                                 {
                                     return true;
                                 }
                     #endregion

                     return false;
                 };
            }
        }

        public override bool SupportsInlineArrayInit
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsForStatements
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsInlineThisReference
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsInlineAssigments
        {
            get
            {
                return true;
            }
        }

        public override void WriteReturnParameter(ILBlock.Prestatement _p, ILInstruction _i)
        {
            if (_i.OwnerMethod is MethodInfo)
            {
                if ((_i.OwnerMethod as MethodInfo).ReturnType == typeof(bool))
                {
                    if (_i.InlineAssigmentValue != null)
                    {
                        if (_i.InlineAssigmentValue.Instruction.IsStoreLocal)
                        {

                            WriteReturnParameter(_p, _i.InlineAssigmentValue.Instruction.StackBeforeStrict[0].SingleStackInstruction);

                            return;
                        }
                    }

                    if (_i == OpCodes.Ldc_I4_0)
                    {
                        WriteKeywordFalse();

                        return;
                    }

                    if (_i == OpCodes.Ldc_I4_1)
                    {
                        WriteKeywordTrue();

                        return;
                    }
                }
            }

            base.WriteReturnParameter(_p, _i);
        }

        public override bool SupportsInlineExceptionVariable
        {
            get
            {
                return true;
            }
        }

        public override void WriteTypeConstructionVerified()
        {
            Write("new Object()");
        }

        public override void WriteInstanceOfOperator(ILInstruction value, Type type)
        {
            EmitInstruction(null, value);

            Write(" instanceof ");

            WriteDecoratedTypeName(type);
        }

        //protected override bool IsTypeCastRequired(Type e, ILFlow.StackItem s)
        //{
        //    if (e == typeof(int) && s.SingleStackInstruction.TargetInteger != null)
        //        return false;

        //    return true;
        //}


        protected override void WriteTypeOf(ILBlock.Prestatement p, ILInstruction i)
        {
            Emit(p, i.StackBeforeStrict[0]);

            Write(".class");
        }

    }
}
