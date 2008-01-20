using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ScriptCoreLib;
using System.Reflection.Emit;
using jsc.Script;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler : Script.CompilerCLike
    {
        public static string FileExtension = "as";

        public readonly AssamblyTypeInfo MySession;

        public ActionScriptCompiler(TextWriter xw, AssamblyTypeInfo xs)
            : base(xw)
        {
            MySession = xs;

            CreateInstructionHandlers();
        }

        private void GetImportTypesFromMethod(Type t, List<Type> imp, MethodBase v)
        {

            ScriptAttribute vs = ScriptAttribute.OfProvider(v);


            // DebugBreak(vs);

            if (vs != null && vs.DefineAsStatic)
                imp.Add(t);

            DebugBreak(vs);

            //imp.AddRange(GetMethodExceptions(v));

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

            //for (var i = b.First; i != null; i = i.Next)
            foreach (var i in b.Instructrions)
            {

                if (i == OpCodes.Ldftn)
                {
                    imp.Add(typeof(IntPtr));
                }

                if (i == OpCodes.Box)
                {
                    imp.Add(i.TargetType);
                }

                if (i.TargetMethod != null)
                {
                    var attr = i.TargetMethod.ToScriptAttribute();

                    if (attr != null && attr.NotImplementedHere)
                    {
                        var impl = MySession.ResolveImplementation(i.TargetMethod.DeclaringType, i.TargetMethod, AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveNativeImplementationExtension);

                        if (impl != null)
                            imp.Add(impl.DeclaringType);
                    }
                }

                if (i.ReferencedMethod != null)
                {
                    if (!IsTypeOfOperator(i.ReferencedMethod))
                        if (i.ReferencedMethod.DeclaringType != typeof(object))
                        {
                            if (ScriptAttribute.IsCompilerGenerated(i.ReferencedMethod.DeclaringType))
                            {
                                imp.Add(i.ReferencedMethod.DeclaringType);
                                continue;
                            }

                            MethodBase method = GetMethodImplementation(MySession, i);
                            var method_attribute = method.ToScriptAttribute();
                            if (method.IsConstructor || method.IsStatic || (method_attribute != null && method_attribute.DefineAsStatic))
                            {
                                imp.Add(method.DeclaringType);
                                continue;
                            }
                        }
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


        private List<Type> GetImportTypes(Type t)
        {



            var imp = new List<Type>();

            Type[] tinterfaces = t.GetInterfaces();

            foreach (Type tinterface in tinterfaces)
                imp.Add(tinterface);

            if (t.BaseType == typeof(MulticastDelegate))
                return new List<Type> { 
                    
                    MySession.ResolveImplementation(typeof(IntPtr)) };

            /*
            Type bp = t.BaseType;

            while (bp != typeof(object) &&
                    bp != null)
            {
                imp.Add(bp);
                bp = bp.BaseType;
            }
            */
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

            var imp_types = new List<Type>();

            imp.RemoveAll(i => i.IsGenericParameter);


            while (imp.Count > 0)
            {
                Type p = imp[0];

                // remove duplicates
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

                // todo fix additional types handling

                while (p.IsArray)
                {
                    p = p.GetElementType();

                }

                if (p == typeof(object)) continue;
                if (p == typeof(void)) continue;
                if (p == typeof(string)) continue;
                if (p == typeof(int)) continue;
                if (p == typeof(uint)) continue;
                if (p == typeof(short)) continue;
                if (p == typeof(long)) continue;
                if (p == typeof(float)) continue;
                if (p == typeof(double)) continue;
                if (p == typeof(byte)) continue;
                if (p == typeof(sbyte)) continue;
                if (p == typeof(bool)) continue;
                if (p == typeof(char)) continue;

                // is a BCL type
                var a = p.ToScriptAttribute();

                if (a == null)
                {
                    if (ScriptAttribute.IsCompilerGenerated(p))
                    {
                        imp_types.Add(p);

                        continue;
                    }

                    // and has an implementation type
                    var p_impl = MySession.ResolveImplementation(p);

                    if (p_impl == null)
                    {

                        Break("class import: no implementation for " + p.FullName + " at " + t.FullName);
                    }

                    p = p_impl;
                    // a = ScriptAttribute.Of(p, true);
                }


                imp_types.Add(p);


            }


            return imp_types;
        }

        public void WriteImportTypes(Type z)
        {
            // all field types, return types, parameter types, variable types, statics

            List<Type> t = GetImportTypes(z);
            List<string> imports = new List<string>();

            /*
            t.RemoveAll(delegate(Type x)
            {
                return IsEmptyImplementationType(x);
            });
            */

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

                        imports.Add(n + GetDecoratedTypeName(p, false));
                    }

                    imports.Add(GetDecoratedTypeName(p, true));


                }
                else
                {

                    imports.Add(n + GetDecoratedTypeName(p, true));



                }



            }


            imports.Sort(
               delegate(string x, string y)
               {
                   return x.CompareTo(y);
               });

            foreach (string var in imports)
            {

                WriteIdent();

                Write("import ");
                Write(NamespaceFixup(var));
                WriteLine(";");

            }


        }



        private void CreateInstructionHandlers()
        {
            CIW[OpCodes.Callvirt] =
                e =>
                {
                    WriteMethodCall(e.p, e.i, e.i.TargetMethod);
                };

            #region call
            CIW[OpCodes.Call] =
                e =>
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

            #region Newobj
            CIW[OpCodes.Newobj] =
                e =>
                {
                    WriteTypeConstruction(e);
                };
            #endregion

            #region fld
            CIW[OpCodes.Ldfld] =
                e =>
                {

                    Emit(e.p, e.FirstOnStack);
                    Write(".");
                    WriteSafeLiteral(e.i.TargetField.Name);

                };

            CIW[OpCodes.Stfld] =
                e =>
                {


                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    Emit(e.p, s[0]);
                    Write(".");
                    WriteSafeLiteral(e.i.TargetField.Name);
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

            CIW[OpCodes.Pop] = CodeEmitArgs.DelegateEmitFirstOnStack;


            CIW[OpCodes.Ldstr] =
                e =>
                {
                    WriteQuotedLiteral(e.i.TargetLiteral);
                };


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

            #region Ret
            CIW[OpCodes.Ret] =
                e =>
                {
                    WriteReturn(e.p, e.i);
                };
            #endregion


            #region ldc
            CIW[OpCodes.Ldc_R4] =
                e =>
                {
                    WriteNumeric(e.i.OpParamAsFloat);
                };

            CIW[OpCodes.Ldc_R8] =
                e =>
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
               e =>
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

            #region operators
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

            #region conv

            // not supported
            // CIW[OpCodes.Conv_I1] = e => ConvertTypeAndEmit(e, "byte");
            // CIW[OpCodes.Conv_U2] = e => ConvertTypeAndEmit(e, "char");
            CIW[OpCodes.Conv_I4] = e => ConvertTypeAndEmit(e, "int");

            // CIW[OpCodes.Conv_I8] = e => ConvertTypeAndEmit(e, "long");
            // CIW[OpCodes.Conv_U8] = e => ConvertTypeAndEmit(e, "long");

            CIW[OpCodes.Conv_R4] = e => ConvertTypeAndEmit(e, "float");
            CIW[OpCodes.Conv_R8] = e => ConvertTypeAndEmit(e, "Number");

            // CIW[OpCodes.Conv_U1] = e => ConvertTypeAndEmit(e, "byte");
            CIW[OpCodes.Conv_Ovf_I] = e => ConvertTypeAndEmit(e, "int");
            #endregion

            CIW[OpCodes.Ldnull] = e => Write("null");


            #region Newarr
            CIW[OpCodes.Newarr] =
                e =>
                {

                    #region inline newarr
                    if (e.p.IsValidInlineArrayInit)
                    {
                        WriteLine("[");
                        Ident++;

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

                        Ident--;
                        WriteIdent();
                        Write("]");
                    }
                    #endregion
                    else
                    {
                        Write("[]");
                        /*
                    
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
                         * */
                    }
                };
            #endregion


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

            CIW[OpCodes.Castclass] = e => ConvertTypeAndEmit(e, GetDecoratedTypeName(e.i.TargetType, true));


            #region Stsfld
            CIW[OpCodes.Stsfld] =
               e =>
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
                           Write(".");
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
                e =>
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
                    Write(".");
                    Write(e.i.TargetField.Name);
                };


            CIW[OpCodes.Dup] = e => EmitFirstOnStack(e);

            CIW[OpCodes.Box] =
                e =>
                {
                    Write("new ");
                    Write(GetDecoratedTypeName(e.i.TargetType, true));
                    Write("(");

                    EmitFirstOnStack(e);

                    Write(")");
                };

            #region Ldftn
            CIW[OpCodes.Ldftn] =
                delegate(CodeEmitArgs e)
                {
                    // we must load it as IntPtr
                    var _IntPtr = MySession.ResolveImplementation(typeof(IntPtr));
                    var _Operators = _IntPtr.GetExplicitOperators(null, _IntPtr);

                    var _IntPtr_string = _Operators.Single(i => i.GetParameters().Single().ParameterType == typeof(string));
                    var _IntPtr_Function = _Operators.Single(i => i.GetParameters().Single().ParameterType != typeof(string));

                    var _Method = e.i.TargetMethod;
                    if (_Method.IsStatic)
                    {
                        WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false);
                        Write(".");
                        WriteDecoratedMethodName(_IntPtr_Function, false);
                        Write("(");
                        WriteDecoratedTypeNameOrImplementationTypeName(_Method.DeclaringType, false, false);
                        Write(".");
                        WriteDecoratedMethodName(e.i.TargetMethod, false);
                        Write(")");
                    }
                    else
                    {
                        if (_Method.DeclaringType == e.Method.DeclaringType)
                        {
                            WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false);
                            Write(".");
                            WriteDecoratedMethodName(_IntPtr_Function, false);
                            Write("(");
                            /*
                            WriteDecoratedTypeNameOrImplementationTypeName(_Method.DeclaringType, false, false);
                            Write(".");*/
                            WriteDecoratedMethodName(e.i.TargetMethod, false);
                            Write(")");
                        }
                        else
                        {
                            WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false);
                            Write(".");
                            WriteDecoratedMethodName(_IntPtr_string, false);
                            Write("(");
                            WriteDecoratedMethodName(_Method, true);
                            Write(")");
                        }
                    }



                };
            #endregion
        }



        public override void WriteMethodParameterList(MethodBase m)
        {
            ParameterInfo[] mp = m.GetParameters();

            var ma = m.ToScriptAttribute();


            bool bStatic = (ma != null && ma.DefineAsStatic);

            if (bStatic)
            {
                if (m.IsStatic)
                {
                    Break("method is already static, but is marked to be declared out of band : " + m.DeclaringType.FullName + "." + m.Name);
                }


                DebugBreak(ma);

                // cannot use 'this' on arguments as it is a keyword
                WriteSelf();
                Write(":");
                WriteDecoratedTypeNameOrImplementationTypeName(m.DeclaringType, true, true);

                //var sa = ScriptAttribute.Of(m.DeclaringType, false);

                //if (sa.Implements == null)
                //{
                //    WriteDecoratedTypeName(m.DeclaringType);

                //}
                //else
                //{
                //    WriteDecoratedTypeName(sa.Implements);
                //}
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

                Write(p.Name);
                Write(":");
                WriteDecoratedTypeNameOrImplementationTypeName(p.ParameterType, true, true);
                /*
                if (za.Implements == null || m.DeclaringType.GUID != p.ParameterType.GUID)
                    WriteDecoratedTypeName(p.ParameterType);
                else
                    WriteDecoratedTypeName(za.Implements);
                */

            }
        }
    }
}
