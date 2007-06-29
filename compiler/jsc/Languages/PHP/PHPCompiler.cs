using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

using jsc.CodeModel;

using ScriptCoreLib;

namespace jsc.Script.PHP
{

    class PHPCompiler : jsc.Script.CompilerCLike
    {
        public static string FileExtension = "php";

        public override ScriptType GetScriptType()
        {
            return ScriptType.PHP;
        }




        public readonly AssamblyTypeInfo MySession;

        public PHPCompiler(TextWriter xw, AssamblyTypeInfo xs)
            : base(xw)
        {

            MySession = xs;
            CreateInstructionHandlers();

        }

        private void CreateInstructionHandlers()
        {
            #region starg
            CIW[OpCodes.Starg_S,
                OpCodes.Starg] =
                delegate(CodeEmitArgs e)
                {
                    WriteMethodParameterOrSelf(e.i);
                    WriteAssignment();
                    if (EmitEnumAsStringSafe(e))
                        return;

                    Emit(e.p, e.FirstOnStack);
                };
            #endregion

            #region passthru

            //CIW[OpCodes.Constrained]=
            //    delegate(CodeEmitArgs e)
            //    {
            //    };

            CIW[OpCodes.Castclass,
                OpCodes.Box,
                OpCodes.Unbox_Any,
                OpCodes.Pop,
                OpCodes.Conv_I4,
                OpCodes.Conv_U2,
                OpCodes.Conv_R8] = CodeEmitArgs.DelegateEmitFirstOnStack;

            CIW[OpCodes.Isinst] = delegate(CodeEmitArgs e)
            {
                throw new NotSupportedException("emitting this opcode isn't directly supported");

                Write("TryCast(variable, type)");
            };


            #endregion

            #region Ret
            CIW[OpCodes.Ret] =
                delegate(CodeEmitArgs e)
                {
                    WriteReturn(e.p, e.i);
                };
            #endregion

            #region Ldlen
            CIW[OpCodes.Ldlen] =
                delegate(CodeEmitArgs e)
                {
                    Write("count(");

                    EmitFirstOnStack(e);

                    Write(")");
                };
            #endregion

            #region Ldftn
            CIW[OpCodes.Ldftn] =
                delegate(CodeEmitArgs e)
                {
                    WriteDecoratedMethodName(e.i.TargetMethod, true);
                };
            #endregion

            #region Ldnull
            CIW[OpCodes.Initobj] =
                delegate(CodeEmitArgs e)
                {
                    Write("$_" + e.i.Prev.TargetVariable.LocalIndex);
                    WriteAssignment();
                    Write("NULL");

                };

            CIW[OpCodes.Ldnull] =
                delegate(CodeEmitArgs e)
                {
                    Write("NULL");
                };
            #endregion

            #region Throw
            CIW[OpCodes.Throw] =
                delegate(CodeEmitArgs e)
                {
                    Write("throw");
                    WriteSpace();

                    Emit(e.p, e.FirstOnStack);
                };
            #endregion


            CIW[OpCodes.Br_S,
                OpCodes.Br] =
                delegate(CodeEmitArgs e)
                {
                    if (e.i.TargetFlow.Branch == OpCodes.Ret)
                    {
                        WriteReturn(e.p, e.i.TargetFlow.Branch);
                    }
                    else Break("invalid br opcode");
                };

            #region fld
            CIW[OpCodes.Ldfld,
                OpCodes.Ldflda] =
                delegate(CodeEmitArgs e)
                {

                    Emit(e.p, e.FirstOnStack);
                    Write("->");
                    WriteDecoratedField(e.i.TargetField, false);
                };

            CIW[OpCodes.Stfld] =
                delegate(CodeEmitArgs e)
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    Emit(e.p, s[0]);
                    Write("->");
                    WriteDecoratedField(e.i.TargetField, false);
                    WriteSpace();
                    Write("=");
                    WriteSpace();
                    Emit(e.p, s[1]);
                };
            #endregion


            #region sfld
            CIW[OpCodes.Ldsfld] =
                delegate(CodeEmitArgs e)
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
                    WriteTypeStaticAccessor();
                    WriteDecoratedField(e.i.TargetField, true);
                };

            CIW[OpCodes.Stsfld] =
                delegate(CodeEmitArgs e)
                {
                    try
                    {
                        WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
                        WriteTypeStaticAccessor();
                        WriteDecoratedField(e.i.TargetField, true);
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


            #region  operands
            CIW[OpCodes.Xor] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "^"); };
            CIW[OpCodes.Clt] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<"); };
            CIW[OpCodes.Cgt,
                OpCodes.Cgt_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">"); };
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
                        Emit(e.p, e.FirstOnStack);
                        return;
                    }

                    WriteInlineOperator(e.p, e.i, "-");
                };

            CIW[OpCodes.Dup] = delegate(CodeEmitArgs e) { EmitFirstOnStack(e); };

            CIW[OpCodes.Shl] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<<"); };
            CIW[OpCodes.Shr] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">>"); };
            CIW[OpCodes.And] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "&"); };
            CIW[OpCodes.Or] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "|"); };
            CIW[OpCodes.Rem] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "%"); };
            CIW[OpCodes.Mul] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "*"); };
            CIW[OpCodes.Div] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "/"); };
            CIW[OpCodes.Bge_S,
                OpCodes.Bge] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">="); };
            CIW[OpCodes.Ble_S,
                OpCodes.Ble] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<="); };
            CIW[OpCodes.Bne_Un_S,
                OpCodes.Bne_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "!="); };
            CIW[OpCodes.Neg] =
                delegate(CodeEmitArgs e)
                {
                    Write("-");
                    Emit(e.p, e.FirstOnStack);
                };

            CIW[OpCodes.Not] =
                delegate(CodeEmitArgs e)
                {
                    Write("~");
                    Emit(e.p, e.FirstOnStack);
                };

            CIW[OpCodes.Ceq] =
                delegate(CodeEmitArgs e)
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    bool b = false;

                    if (s[1].SingleStackInstruction == OpCodes.Ldc_I4_0
                        && s[0].SingleStackInstruction.ReferencedType == typeof(bool))
                        b = true;



                    if (s[1].SingleStackInstruction == OpCodes.Ldc_I4_0
                        && s[0].SingleStackInstruction.IsBooleanOpcode)
                        b = true;

                    if (b)
                    {
                        Write("!");
                        Emit(e.p, e.i.StackBeforeStrict[0]);
                    }
                    else
                        WriteInlineOperator(e.p, e.i, "==");
                };
            #endregion


            #region Ldarg
            CIW[OpCodes.Ldarg_0,
                OpCodes.Ldarg_1,
                OpCodes.Ldarg_2,
                OpCodes.Ldarg_3,
                OpCodes.Ldarg_S,
                OpCodes.Ldarg,
                OpCodes.Ldarga,
                OpCodes.Ldarga_S
                ] =
                delegate(CodeEmitArgs e)
                {

                    WriteMethodParameterOrSelf(e.i);
                };
            #endregion

            #region Ldind_Ref
            CIW[OpCodes.Ldind_Ref] =
                delegate(CodeEmitArgs e)
                {
                    WriteMethodParameterOrSelf(e.FirstOnStack.SingleStackInstruction);
                };
            #endregion



            #region ldc
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
                       Break("ldc unresolved");

                   MyWriter.Write(n.Value);
               };
            #endregion


            #region stloc
            CIW[OpCodes.Stloc_0,
                OpCodes.Stloc_1,
                OpCodes.Stloc_2,
                OpCodes.Stloc_3,
                OpCodes.Stloc_S,
                OpCodes.Stloc] =
                delegate(CodeEmitArgs e)
                {

                    Write("$_" + e.i.TargetVariable.LocalIndex);
                    WriteAssignment();

                    if (e.i.IsFirstInFlow && e.i.Flow.OwnerBlock.IsHandlerBlock)
                    {
                        WriteExceptionVar();
                        return;
                    }

                    if (EmitEnumAsStringSafe(e))
                        return;


                    Emit(e.p, e.FirstOnStack);
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
               delegate(CodeEmitArgs e)
               {
                   if (e.p.Owner.IsCompound)
                   {
                       ILBlock.Prestatement sp = e.p.Owner.SourcePrestatement(e.p, e.i);

                       if (sp != null)
                       {
                           EmitInstruction(sp, sp.Instruction);



                           return;
                       }
                   }

                   Write("$_" + e.i.TargetVariable.LocalIndex);

                   if (e.i.IsInlinePostSub) Write("--");
                   if (e.i.IsInlinePostAdd) Write("++");
               };
            #endregion


            CIW[OpCodes.Ldstr] =
                delegate(CodeEmitArgs e)
                {
                    WriteLiteral(e.i.TargetLiteral);
                };

            CIW[OpCodes.Newarr] =
                delegate(CodeEmitArgs e)
                {
                    #region inline newarr
                    if (e.p.IsValidInlineArrayInit)
                    {
                        WriteLine("array (");
                        Ident++;

                        //using (CreateScope(false))
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
                                        Write("NULL");
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

                           
                        };


                        WriteLine();

                        Ident--;

                        WriteIdent();
                        Write(")");

                    }
                    #endregion
                    else
                        Write("array()");
                };

            #region elem_ref
            CIW[OpCodes.Ldelem_Ref,
                OpCodes.Ldelem_U1,
                OpCodes.Ldelem_U2,
                OpCodes.Ldelem_I1,
                OpCodes.Ldelem_I4,
                OpCodes.Ldelem
                ] =
                delegate(CodeEmitArgs e)
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    Emit(e.p, s[0]);
                    Write("[");
                    Emit(e.p, s[1]);
                    Write("]");
                };

            CIW[OpCodes.Stelem,
                OpCodes.Stelem_Ref,
                OpCodes.Stelem_I1,
                OpCodes.Stelem_I2,
                OpCodes.Stelem_I4
                ] =
                delegate(CodeEmitArgs e)
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    Emit(e.p, s[0]);
                    Write("[");
                    Emit(e.p, s[1]);
                    Write("]");
                    WriteSpace();
                    Write("=");
                    WriteSpace();
                    Emit(e.p, s[2]);
                };
            #endregion


            #region Newobj
            CIW[OpCodes.Newobj] =
                delegate(CodeEmitArgs e)
                {
                    WriteTypeConstruction(e);

                };
            #endregion

            #region call
            CIW[OpCodes.Callvirt,
                OpCodes.Call] =
                delegate(CodeEmitArgs e)
                {

                    MethodBase m = e.i.ReferencedMethod;

                    if (Script.CompilerBase.IsToStringMethod(m))
                    {
                        Write("");
                        EmitFirstOnStack(e);
                        Write("->__toString()");

                        return;
                    }

                    MethodBase mi = MySession.ResolveImplementation(m.DeclaringType, m);

                    if (mi != null)
                    {
                        WriteMethodCall(e.p, e.i, mi);

                        return;
                    }

                    WriteMethodCall(e.p, e.i, m);
                };
            #endregion
        }



        private void WriteTypeStaticAccessor()
        {
            Write("::");
        }







        public override MethodBase ResolveImplementationMethod(Type t, MethodBase m)
        {
            return MySession.ResolveImplementation(t, m); ;
        }

        public override MethodBase ResolveImplementationMethod(Type t, MethodBase m, string alias)
        {
            return MySession.ResolveMethod(m, t, alias); ;

        }


        public override void WriteExceptionVar()
        {
            Write("$__exc");
        }



        public void WriteDecoratedField(FieldInfo z, bool p)
        {
            if (p)
                Write("$");

            bool noDec = false;

            if (z.DeclaringType.IsSerializable)
                noDec = true;

            ScriptAttribute sa = ScriptAttribute.Of(z.DeclaringType, false);

            if (sa != null && sa.HasNoPrototype)
                noDec = true;

            // instance members do not clash, nor should they be redefined

            if (!z.IsStatic)
                noDec = true;

            if (noDec)
            {
                
                WriteSafeLiteral(z.Name);
            }
            else
            {
                WriteDecoratedFieldVerified(z);
            }

        }



        







        public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
        {
            try
            {
                bool bBase = false;

                if (m.IsConstructor)
                {
                    if (m.DeclaringType == i.OwnerMethod.DeclaringType.BaseType)
                    {

                        bBase = true;
                    }
                    else
                        Break("If it was a native constructor, it should be remapped via InternalConstructor attribute.Cannot call constructor : " + m + " used at " + i.OwnerMethod.DeclaringType.FullName + "." + i.OwnerMethod.Name + ".");
                }

                ScriptAttribute ma = ScriptAttribute.OfProvider(m);
                bool dStatic = ma != null && ma.DefineAsStatic;



                ILFlow.StackItem[] s = i == null ? null : i.StackBeforeStrict;

                int offset = 1;



                if (m.IsStatic || dStatic || bBase)
                {
                    if (bBase)
                    {
                        WriteTypeBaseType();
                        Write("::");

                    }



                    offset = !m.IsStatic && (dStatic || bBase) ? 1 : 0;

                }
                else
                {


                    Emit(p, s[0]);

                    Write("->");
                }


                WriteMethodName(m);
                WriteParameterInfoFromStack(m, p, s, offset);
            }
            catch
            {
                Break("cannot write method call");
            }
        }

        private void WriteTypeBaseType()
        {
            Write("parent");
        }

        public override void WriteNativeNoExceptionMethodName(MethodBase m)
        {
            Write("@");

            base.WriteNativeNoExceptionMethodName(m);
        }

        private void WriteMethodName(MethodBase m)
        {


            if (m.IsConstructor)
                Write("__construct");
            else
            {
                if (IsToStringMethod(m))
                {
                    Write("__toString");
                }
                else
                    WriteDecoratedMethodName(m, false);
            }

        }












        public override Type[] GetActiveTypes()
        {
            return MySession.Types;
        }

        TypeInfo[] ActiveTypes
        {
            get
            {
                List<TypeInfo> u = new List<TypeInfo>();

                foreach (Type z in MySession.Types)
                {
                    TypeInfo v = TypeInfoOf(z);

                    u.Add(v);
                }

                return u.ToArray();
            }
        }

        public static TypeInfo TypeInfoOf(Type z)
        {
            TypeInfo v = new TypeInfo(z);

            v.TargetFileNameHandler = delegate(TypeInfo a)
            {
                return "inc/" + a.AssamblyFileName + "/class." + a.Win32TypeName + ".php";
            };

            return v;
        }




        /// <summary>
        /// compiles the main file for the assambly, also compile web/inc/*.dll/class.*.php multithreaded
        /// </summary>
        public void Compile(CompileSessionInfo sinfo)
        {

            DirectoryInfo web = new DirectoryInfo("web");

            DirectoryInfo u = web.CreateSubdirectory("inc");


            WriteLine("<?");

            Helper.WorkPool n = new Helper.WorkPool();

            n.IsThreaded = !Debugger.IsAttached;

            List<TypeInfo> req = new List<TypeInfo>();

            using (new Helper.ConsoleStopper("php type compiler"))
            {
                 n.ForEach(ActiveTypes,
                    delegate(TypeInfo z)
                    {
                        CompilerBase c = new Script.PHP.PHPCompiler(new StringWriter(), this.MySession);

                        c.CurrentJob = null;
             
                        Program.AttachXMLDoc(new FileInfo(z.Value.Assembly.ManifestModule.FullyQualifiedName), c);

                        if (c.CompileType(z.Value))
                        {
                            c.ToConsole(z.Value, sinfo);

                            DirectoryInfo x = u.CreateSubdirectory(z.AssamblyFileName);

                            string content = c.MyWriter.ToString();


                            StreamWriter sw = new StreamWriter(new FileStream(web.FullName + "/" + z.TargetFileName, FileMode.Create));

                            sw.WriteLine("<?");
                            sw.Write(content);
                            sw.WriteLine("?>");
                            sw.Flush();

                            sw.Close();

                            req.Add(z);
                        }
                    }
                );

             }

             foreach (TypeInfo z in req)
             {
                 WriteImport(z);
             }

            WriteLine();

            foreach (Type z in MySession.Types)
            {
                WriteTypeStaticConstructor(z, false);
            }

            WriteLine("?>");
        }

        public void WriteImport(TypeInfo z)
        {
            if (z.IsScript || z.IsCompilerGenerated)
            {
                WriteLine("require_once '" + z.TargetFileName + "';");
            }
        }

        private void WriteTypeStaticConstructor(Type z, bool defMode)
        {
            ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (ConstructorInfo m in ci)
            {
                if (defMode)
                {
                    WriteMethodSignature(z, m, false);
                    WriteMethodBody(m);
                    WriteLine();

                }
                else
                {
                    WriteIdent();
                    
                    WriteHint(m);

                    WriteMethodCallVerified(null, null, m);
                    WriteLine(";");
                    WriteLine();

                }

            }


        }

        private void WriteHint(ConstructorInfo m)
        {
            WriteLine("/* " + m.DeclaringType.FullName + " :: " + m.Name + " */");
        }

        public override bool CompileType(Type z)
        {
            try
            {
            

                if (z.IsEnum)
                    return false;

                if (z.IsValueType)
                    Break("ValueType not supported : " + z.FullName);

                ScriptAttribute za = ScriptAttribute.Of(z, true);

                if (z.BaseType == typeof(global::System.MulticastDelegate))
                {
                    jsc.Languages.PHP.DelegateImplementationProvider.Write(this, z);

                    return true;
                }


                if (z.BaseType != typeof(object) && z.BaseType != null)
                {
                    WriteImport(TypeInfoOf(z.BaseType));
                }


                //Console.WriteLine(z.FullName);



                if (!za.InternalConstructor)
                {
                    WriteTypeSignature(z, za);

                    using (CreateScope())
                    {

                        WriteTypeFields(z, za);
                        WriteTypeInstanceConstructors(z);
                        WriteTypeInstanceMethods(z, za);

                        WriteTypeVirtualMethods(z, za);

                    }
                }

                WriteLine();


                WriteTypeStaticMethods(z, za);

                WriteLine();


                WriteTypeStaticConstructor(z, true);

                WriteLine();
            }
            catch
            {
                Break("internal error while compiling type " + z.FullName);
            }


            return true;
        }

        public override void WriteTypeSignature(Type z, ScriptAttribute za)
        {
            WriteLine("// " + z.FullName);

            WriteIdent();

            if (z.IsInterface)
                Write("interface");
            else
                Write("class");

            WriteSpace();
            WriteDecoratedTypeName(z);

            if (!za.InternalConstructor)
            {
                if (!z.IsInterface)
                {
                    if (z.BaseType != typeof(object))
                    {
                        ScriptAttribute ba = ScriptAttribute.Of(z.BaseType, false);

                        if (ba == null)
                            Break("base class should be for scripting : " + z.BaseType.FullName);

                        WriteSpace();
                        Write("extends");
                        WriteSpace();
                        WriteDecoratedTypeName(z.BaseType);

                    }
                }
            }

            WriteLine();
        }

        private void WriteTypeVirtualMethods(Type owner, ScriptAttribute za)
        {
            // find the virtual name or names

            if (za.HasNoPrototype)
                return;

            List<Type> t =  new List<Type>(owner.GetInterfaces());


            bool b = false;

            foreach (Type x in t)
            {

                InterfaceMapping z = owner.GetInterfaceMap(x);

                int ix = 0;

                foreach (MethodInfo zm in z.TargetMethods)
                {

                    MethodBase a = z.InterfaceMethods[ix];

                    MethodBase u = z.TargetMethods[ix];

                    // since this is php, we cannot share method bodies, we must clone them
                    // wrapper? forwarder?

                    WriteMethodSignature(owner, a, false);
                    WriteMethodBody(u);

                    b = true;

                    ix++;
                }




            }

            if (b)
                WriteLine();

        }

        private void WriteTypeInstanceConstructors(Type z)
        {
            ConstructorInfo[] zci = z.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            if (zci.Length > 1)
            {
                Break("PHP does not support multiple constructors, type: " + z.FullName);
            }

            foreach (ConstructorInfo zc in zci)
            {
                WriteIdent();
                WriteCommentLine(zc.DeclaringType.FullName + ".ctor");
                WriteMethodSignature(z, zc, false);
                WriteMethodBody(zc);

            }
            WriteLine();
        }

        public override void WriteTypeFields(Type z, ScriptAttribute za)
        {
            FieldInfo[] zf = z.GetFields(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (FieldInfo zfn in zf)
            {
                // external class cannot have static variables inside a type
                // should be defined outside as global static instead
                if (za.HasNoPrototype && !zfn.IsStatic)
                    continue;


                WriteIdent();
                WriteTypeFieldModifier(zfn);



                WriteDecoratedField(zfn, true);
                Write(";");
                WriteLine();
            }

            WriteLine();
        }


        public override void WriteTypeFieldModifier(FieldInfo zfn)
        {


            if (zfn.IsStatic)
            {
                Write("static");
                WriteSpace();
            }
            else if (zfn.IsPublic)
            {
                Write("public");
            }
            else
            {
                Write("var");
            }

            WriteSpace();
        }

        public override void WriteTypeInstanceMethods(Type z, ScriptAttribute za)
        {
            MethodInfo[] mx = base.GetAllInstanceMethods(z);

            foreach (MethodInfo m in mx)
            {
                ScriptAttribute ma = ScriptAttribute.Of(m);

                bool dStatic = ma != null && ma.DefineAsStatic;

                if (dStatic)
                {
                    continue;
                }

                if (ma != null && ma.IsNative)
                    continue;


                WriteMethodHint(m);


                WriteMethodSignature(z, m, dStatic);

                if (!z.IsInterface)
                    WriteMethodBody(m);

                WriteLine();
            }
        }











        public override void WriteMethodSignature(Type compiland, MethodBase m, bool dStatic)
        {

            WriteIdent();

            if (compiland.IsInterface)
            {
                if (m.IsPublic)
                    Write("public ");
            }

            Write("function ");
            WriteMethodName(m);

            Write("(");
            WriteMethodParameterList(m);
            Write(")");

            if (compiland.IsInterface)
                WriteLine(";");
            else
                WriteLine();
        }

        public override void WriteMethodSignature(MethodBase m, bool dStatic)
        {
            CompilerBase.BreakToDebugger("obsolete");

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


                if (ScriptParameterByValAttribute.IsDefined(m, typeof(ScriptParameterByValAttribute))
                    || ScriptParameterByValAttribute.IsDefined(m.DeclaringType, typeof(ScriptParameterByValAttribute)))
                {

                }
                else
                {
                    if (ScriptParameterByRefAttribute.IsDefined(m, typeof(ScriptParameterByRefAttribute)))
                    {
                        WriteByRef();
                    }
                    else
                    {
                        if (Debugger.IsAttached)
                            CompilerBase.WriteVisualStudioMessage(MessageType.warning, 1002, m.DeclaringType.FullName + "." + m.Name + " : consider ScriptParameterByRefAttribute");

                        WriteByRef();

                    }
                }

                WriteSelf();
            }

            for (int mpi = 0; mpi < mp.Length; mpi++)
            {
                if (mpi > 0 || bStatic)
                {
                    Write(",");
                    WriteSpace();
                }

                bool bByRef = false;

                if (mp[mpi].ParameterType.IsByRef ||
                    mp[mpi].ParameterType.IsArray)
                {
                    //if (ma.Implements != null)
                    //{
                    //    if (ma.Implements.IsByRef)
                    //    {
                    bByRef = true;
                    //    }
                    //}
                }

                if (ScriptParameterByRefAttribute.IsDefined(mp[mpi], typeof(ScriptParameterByRefAttribute))) bByRef = true;

                if (bByRef)
                    WriteByRef();

                if (ma != null && ma.OptimizedCode != null && !ma.UseCompilerConstants)
                    Write("$" + mp[mpi].Name);
                else
                    WriteDecoratedMethodParameter(mp[mpi]);

            }
        }

        public override string GetDecoratedMethodParameter(ParameterInfo p)
        {
            return "$p" + p.Position;
        }

        private void WriteByRef()
        {
            Write("&");
        }

        public override void WriteSelf()
        {
            Write("$this");
        }





        public override void EmitPrestatement(ILBlock.Prestatement p)
        {


            #region opt-out


            if (IsOptOut(p))
                return;


            if (p.Instruction == OpCodes.Ret)
            {
                if (p.Next == null)
                {
                    if (p.Instruction.StackBeforeStrict.Length == 0)
                    {
                        return;
                    }
                }
            }
            #endregion



            #region if
            ILIfElseConstruct iif = p.Instruction.InlineIfElseConstruct;

            if (iif != null)
            {
                EmitIfBlock(p, iif);

                return;
            }
            #endregion



            WriteIdent();
            EmitInstruction(p, p.Instruction);
            WriteLine(";");
        }

        private static bool IsOptOut(ILBlock.Prestatement p)
        {
            bool bOptOut = false;

            // nop call wont do any good
            if (p.Instruction == OpCodes.Nop)
                bOptOut = true;


            if (p.Instruction.OpCode == OpCodes.Call)
            {
                if (p.Instruction.TargetConstructor != null)
                {
                    // we wont call object() constructor excplicitly
                    if (p.Instruction.TargetConstructor.DeclaringType == typeof(object))
                        bOptOut = true;


                    if (p.Instruction.TargetConstructor.DeclaringType == p.Instruction.OwnerMethod.DeclaringType.BaseType)
                    {
                        ScriptAttribute a = ScriptAttribute.Of(p.Instruction.OwnerMethod.DeclaringType, true);

                        // if construct type is equal to current base type and
                        // we do have script attribute and we are external object
                        // we skip?
                        if (a != null && a.InternalConstructor)
                            bOptOut = true;

                    }
                }
            }
            return bOptOut;
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
                    Write("catch (Exception ");
                    WriteExceptionVar();
                    WriteLine(")");

                    EmitScope(b);
                }
                else
                {
                    Write("catch (Exception ");
                    WriteExceptionVar();
                    WriteLine(")");
                    WriteScopeBegin();

                    WriteIdent();
                    Write("$__" + p.Block.Clause.TryOffset);
                    WriteSpace();
                    Write("=");
                    WriteSpace();
                    WriteExceptionVar();
                    WriteLine(";");

                    WriteScopeEnd();
                    WriteLine();

                    EmitPrestatementBlock(b);

                    WriteIdent();
                    Write("if (isset(");
                    Write("$__" + p.Block.Clause.TryOffset);
                    WriteLine("))");

                    WriteScopeBegin();

                    WriteIdent();
                    Write("throw ");
                    Write("$__" + p.Block.Clause.TryOffset);
                    WriteLine(";");

                    WriteScopeEnd();



                }

            }
            else
            {
                return false;
            }

            return true;
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


        public override bool DoWriteStaticMethodHint
        {
            get
            {
                return true;
            }
        }


        public override void WriteTypeConstructionVerified()
        {
            Write("new stdClass()");
        }

        public override void WriteInstanceOfOperator(ILInstruction value, Type type)
        {
            EmitInstruction(null, value);

            Write(" instanceof ");

            WriteDecoratedTypeName(type);            
        }
    }
}
