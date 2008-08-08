using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime;
using System.Reflection.Emit;
using System.Reflection;
using System.Xml;

using ScriptCoreLib;

namespace jsc.Script
{
    using Languages;

    public delegate void CodeInstructionHandler(CodeEmitArgs e);


    public class CodeInstructionWrapper<THandler> where THandler : class
    {
        public THandler this[params OpCode[] i]
        {
            set
            {
                foreach (OpCode c in i)
                {
                    if (this[c] != null)
                        throw new Exception("cannot overwrite opcode handler: " + c.Name);

                    this[c] = value;
                }
            }
        }


        public THandler this[OpCode i]
        {
            [DebuggerNonUserCode]
            get
            {
                return this[i.Value];
            }
            set
            {
                this[i.Value] = value;
            }
        }

        THandler[] List = new THandler[0xFFFF + 1];


        public THandler this[short i]
        {
            [DebuggerNonUserCode]
            get
            {
                return List[i & 0x0000ffff];
            }
            set
            {
                List[i & 0x0000ffff] = value;
            }
        }
    }

    /*
     * Todo:
     * - map static calls directly to native apis
     */

    public class CodeEmitArgs
    {
        public Type TypeExpectedOrDefault;

        public ILBlock.Prestatement p;
        public ILInstruction i;
        public CompilerBase c;

        public OpCode OpCode
        {
            get { return i.OpCode; }
        }
        public MethodBase Method
        {
            get
            {
                return i.OwnerMethod;
            }
        }

        public static CodeInstructionHandler DelegateEmitFirstOnStack =
            delegate(CodeEmitArgs e)
            {
                e.c.Emit(e.p, e.FirstOnStack);
            };

        public CodeEmitArgs(CompilerBase c)
        {
            this.c = c;


        }

        public ILFlow.StackItem FirstOnStack
        {
            [DebuggerNonUserCode]
            get
            {
                //if (i.StackPopCount != 1)
                //    return null;

                return i.StackBeforeStrict[0];
            }
        }

        public void Emit(ILInstruction i)
        {
            // c.Emit(p, i);
        }




    }

    public abstract partial class CompilerBase
    {
        public CompilerJob CurrentJob;

        public XmlDocument XmlDoc;

        public readonly CodeInstructionWrapper<CodeInstructionHandler> CIW = new CodeInstructionWrapper<CodeInstructionHandler>();


        public TextWriter MyWriter;

        protected int Ident;


        public enum MessageType
        {
            warning,
            error,
            message
        }

        public virtual bool CompileToSingleFile
        {
            get
            {
                return true;
            }
        }

        public static string ScriptGetExternalTarget(Type m)
        {
            string x = null;

            ScriptAttribute ma = ScriptAttribute.OfProvider(m);

            if (ma != null && ma.ExternalTarget != null)
                x = (ma.ExternalTarget);
            return x;
        }

        public bool IsNativeType(Type e)
        {
            ScriptAttribute a = ScriptAttribute.Of(e, false);

            return a != null && a.IsNative;
        }

        public CompilerBase(TextWriter xw)
        {
            MyWriter = xw;
        }

        public string ScriptLibraryImport(Type e)
        {
            ScriptAttribute x = ScriptAttribute.Of(e);

            if (x == null)
                return null;

            return x.LibraryImport;
        }

        public bool ScriptIsPInvoke(MethodBase m)
        {

            ScriptAttribute sm = ScriptAttribute.Of(m);

            return (sm != null && sm.IsPInvoke) || (ScriptLibraryImport(m.DeclaringType) != null);
        }

        public abstract ScriptType GetScriptType();


        public static void WriteVisualStudioMessage(MessageType type, int code, string msg)
        {

            // TODO make it work as message

            Console.Error.WriteLine(@"script: {0} JSC{1}: {2}", Enum.GetName(typeof(MessageType), type), code, msg);

        }

        public abstract bool CompileType(Type e);

        [DebuggerNonUserCode]
        public void Break(string e)
        {
            CompilerBase.BreakToDebugger(Enum.GetName(typeof(ScriptType), this.GetScriptType()) + " : " + e);
        }


        [DebuggerNonUserCode]
        public static void DebugBreak(ICustomAttributeProvider e)
        {
            DebugBreak(ScriptAttribute.Of(e));
        }

        [DebuggerNonUserCode]
        public static void DebugBreak(ScriptAttribute vs)
        {
            if (vs != null && vs.IsDebugCode && Debugger.IsAttached)
                Debugger.Break();
        }

        [DebuggerNonUserCode]
        public static void DebugBreak()
        {
            if (Debugger.IsAttached)
                Debugger.Break();
        }

        [DebuggerNonUserCode]
        public static void BreakToDebugger(string e)
        {

            WriteVisualStudioMessage(MessageType.error, 1000, e);

            Console.ReadLine();

            if (Debugger.IsAttached)
            {

                Debugger.Break();
            }
            else
            {

                Environment.Exit(-1);
            }

        }



        public virtual char GetSpecialChar()
        {
            return '_';
        }


        RecursionGuard EmitGuard = new RecursionGuard(32);

        public void Emit(ILBlock.Prestatement p, ILFlow.StackItem s)
        {
            Emit(p, s, null);
        }

        public void Emit(ILBlock.Prestatement p, ILFlow.StackItem s, Type TypeExpectedOrDefault)
        {
            using (EmitGuard.Lock)
            {
                if (s.StackInstructions.Length == 1)
                {
                    EmitInstruction(p, s.SingleStackInstruction, TypeExpectedOrDefault);
                }
                else
                {
                    EmitLogic(p, s.InlineLogic(p.Owner.MemoryBy(s)));


                }
            }
        }

        #region write


        public virtual void WriteDecoratedTypeName(Type context, Type subject)
        {
            MyWriter.Write(GetDecoratedTypeName(subject, true));
        }


        public virtual void WriteDecoratedTypeName(Type z)
        {
            MyWriter.Write(GetDecoratedTypeName(z, true));
        }

        public void WriteDecoratedFieldVerified(FieldInfo z)
        {
            Write(GetDecoratedGUID(z.DeclaringType.GUID));
            Write(GetSpecialChar());
            MyWriter.Write("{0:x4}", z.MetadataToken);
        }


        public virtual void WriteNativeNoExceptionMethodName(MethodBase m)
        {
            Write(m.Name);
        }

        public virtual void WriteDecoratedMethodName(MethodBase z, bool q)
        {
            if (q)
                WriteQuote();

            ScriptAttribute x = ScriptAttribute.Of(z.DeclaringType, true);
            ScriptAttribute a = ScriptAttribute.Of(z);

            bool isExternalObject = (x != null && x.HasNoPrototype && z.IsPublic);
            bool isNoDecoration = (a != null && (a.NoDecoration || a.IsNative)) || (x != null && x.NoDecoration);
            bool isAdditionalMethod = a != null && (a.DefineAsStatic || a.OptimizedCode != null);
            bool isCompilerGenerated = ScriptAttribute.IsCompilerGenerated(z);
            bool isDecoratedStatic = z.IsStatic && !isNoDecoration;

            if (a != null && a.IsNative && a.NoExeptions && !q)
            {
                WriteNativeNoExceptionMethodName(z);
            }
            else
            {
                if (!isDecoratedStatic
                    && (!isCompilerGenerated && (isNoDecoration || isExternalObject) && !isAdditionalMethod))
                {

                    Write(z.Name);
                }
                else
                    MyWriter.Write("{0}_{1:x4}", GetDecoratedGUID(z.DeclaringType.GUID), z.MetadataToken);
            }

            if (q)
                WriteQuote();
        }

        public bool WriteIdent_Enabled = true;

        public void WriteIdent()
        {
            if (WriteIdent_Enabled)
                Write(new string(' ', Ident * 4));
            else
                Write(" ");
        }

        public void WriteNumeric(double i)
        {
            NumberFormatInfo provider = new NumberFormatInfo();

            this.Write(Convert.ToString(i, provider));


        }

        public void Write(string e)
        {
            MyWriter.Write(e);
        }

        public void Write(char e)
        {
            MyWriter.Write(e);
        }


        public bool WriteLine_NewLineEnabled = true;

        public void WriteLine()
        {
            if (WriteLine_NewLineEnabled)

                MyWriter.WriteLine();
            else
                WriteSpace();

        }

        public void WriteSpace()
        {
            Write(" ");

        }

        public void WriteLine(string p)
        {
            if (WriteLine_NewLineEnabled)
                MyWriter.WriteLine(p);
            else
            {
                Write(p);
                WriteSpace();
            }
        }


        public void WriteQuote()
        {
            this.Write("\"");
        }


        public virtual bool IsUTF8SupportedInLiterals()
        {
            return false;

        }

        /// <summary>
        /// if returned true, the actual char is put on literal, otherwise either its hex form or escaped form
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public virtual bool IsVisibleCharacter(char c)
        {
            return true;
        }

        /// <summary>
        /// returns escaped char
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public virtual string GetEscapedCharacter(char c)
        {
            if (c == '\\') return "\\\\";
            if (c == '\"') return "\\\"";
            if (c == '\'') return "\\\'";
            if (c == '\r') return "\\r";
            if (c == '\n') return "\\n";
            if (c == '\t') return "\\t";

            return null;
        }

        public void WriteDecoratedLiteralString(string z)
        {
            if (IsUTF8SupportedInLiterals())
            {
                foreach (char x in z)
                    if (IsVisibleCharacter(x))
                        MyWriter.Write(x);
                    else
                    {
                        string esc = GetEscapedCharacter(x);

                        if (esc != null)
                            MyWriter.Write(esc);
                        else
                            MyWriter.Write(@"\u{0:x4}", (int)x);
                    }

                return;
            }

            char[] b = z.ToCharArray();

            foreach (char x in b)
                if (IsVisibleCharacter(x))
                    MyWriter.Write(x);
                else
                    MyWriter.Write(@"\x{0:x2}", (byte)x);

        }

        #region abstract
        public abstract void WriteTypeSignature(Type z, ScriptAttribute za);

        public abstract void WriteTypeFields(Type z, ScriptAttribute za);

        public abstract void WriteTypeFieldModifier(FieldInfo zfn);

        public abstract void WriteTypeInstanceMethods(Type z, ScriptAttribute za);

        public virtual void WriteMethodHint(MethodBase m)
        {
        }

        public virtual void WriteXmlDoc(MethodBase e)
        {
        }

        public virtual bool WillEmitMethodBody()
        {

            return true;
        }

        public virtual bool DoWriteStaticMethodHint
        {
            get
            {
                return false;
            }
        }

        public void WriteTypeStaticMethods(Type z, ScriptAttribute za)
        {
            MethodInfo[] mx = z.GetMethods(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (MethodInfo m in mx)
            {
                var ma = m.ToScriptAttribute();

                DebugBreak(ma);




                //DebugBreak(ma);


                bool dStatic = IsDefineAsStatic(m);

                if (!dStatic && !m.IsStatic)
                {
                    continue;
                }


                if (ma != null && ma.IsNative)
                    continue;


                if (ma != null && ma.ExternalTarget != null)
                    continue;

                if (ma != null && ma.StringConcatOperator != null)
                    continue;

                //WriteHint(m.DeclaringType.FullName + "." + m.Name);

                if (DoWriteStaticMethodHint)
                    WriteMethodHint(m);

                WriteXmlDoc(m);
                WriteMethodSignature(z, m, dStatic);

                if (WillEmitMethodBody())
                    if (!ScriptIsPInvoke(m))
                        WriteMethodBody(m, MethodBodyFilter);

                WriteLine();
            }


        }

        public virtual void WriteMethodSignature(Type compiland, MethodBase m, bool dStatic)
        {
            WriteMethodSignature(m, dStatic);
        }

        public abstract void WriteMethodSignature(MethodBase m, bool dStatic);

        public abstract void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m);


        public abstract void WriteMethodParameterList(MethodBase m);

        public abstract void WriteSelf();

        public abstract void WriteTypeConstructionVerified(CodeEmitArgs e, Type mtype, MethodBase m, ScriptAttribute mza);


        public abstract void EmitPrestatement(ILBlock.Prestatement p);

        public abstract void EmitIfBlock(ILBlock.Prestatement p, ILIfElseConstruct iif);

        public abstract void EmitLoopBlock(ILBlock.Prestatement p, ILLoopConstruct loop);

        public abstract void EmitLogic(ILBlock.Prestatement p, ILBlock.InlineLogic logic);


        #endregion

        public abstract Type[] GetActiveTypes();


        public void CompileAllTypes()
        {
            foreach (Type z in GetActiveTypes())
            {
                CompileType(z);
            }
        }

        public virtual bool SupportsCustomArrayEnumerator
        {
            get
            {
                return false;
            }
        }

        public virtual bool SupportsForStatements
        {
            get
            {
                return false;
            }
        }

        public virtual bool SupportsInlineArrayInit
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// if set to true, the intermediate that variable will not be used at instance methods
        /// </summary>
        public virtual bool SupportsInlineThisReference
        {
            get
            {
                return false;
            }
        }

        public virtual bool SupportsInlineAssigments
        {
            get
            {
                return false;
            }
        }

        public virtual bool SupportsInlineExceptionVariable
        {
            get
            {
                return false;
            }
        }

        public void EmitPrestatementBlock(ILBlock.PrestatementBlock xbp)
        {
            EmitPrestatementBlock(xbp, null);
        }

        static RecursionGuard _for_statement = new RecursionGuard(8);


        public void EmitPrestatementBlock(ILBlock.PrestatementBlock xbp, Predicate<ILBlock.Prestatement> predicate)
        {
            ILBlock.Prestatement[] c = xbp.PrestatementCommands.ToArray();

            // xxx - block.getactiveprestatements

            for (int xbi = 0; xbi < c.Length; xbi++)
            {
                ILBlock.Prestatement p = c[xbi];

                if (predicate != null)
                {
                    if (predicate(p))
                        continue;
                }

                #region try
                if (p.Block != null)
                {
                    //Debugger.Break();

                    if (EmitTryBlock(p))
                        continue;

                }
                #endregion

                if (p.Instruction == OpCodes.Nop)
                    continue;

                if (p.Instruction.TargetConstructor != null)
                {
                    if (p.Instruction.TargetConstructor.DeclaringType == typeof(object))
                        continue;

                    // todo: php need base:ctor calls, while java does not ( super() calls? )
                    //if (p.Instruction.Method.DeclaringType.BaseType == p.Instruction.TargetConstructor.DeclaringType)
                    //    continue;
                }

                if (SupportsForStatements)
                {
                    if (p.IsValidForStatement)
                    {
                        using (_for_statement.Lock)
                        {
                            WriteForStatement(p);
                        }

                        xbi += 1;
                        continue;
                    }
                }

                #region loop
                ILLoopConstruct loop = p.Instruction.InlineLoopConstruct;

                if (loop != null)
                {
                    EmitLoopBlock(p, loop);

                    continue;
                }
                #endregion

                #region if
                ILIfElseConstruct iif = p.Instruction.InlineIfElseConstruct;

                if (iif != null)
                {
                    EmitIfBlock(p, iif);

                    continue;
                }
                #endregion

                if (SupportsInlineAssigments)
                {
                    //if (p.IsInlineAssigment)
                    //    continue;
                }

                #region newarr
                if (SupportsInlineArrayInit)
                {
                    if (p.IsValidInlineArrayInit)
                    {
                        xbi += p.InlineArrayInitElementsFound;
                    }
                }
                #endregion

                if (SupportsInlineAssigments)
                {
                    if (p.IsInlineAssigment)
                        continue;
                }


                EmitPrestatement(p);
            }
        }

        public abstract void WriteBoxedComment(string e);


        public abstract IDisposable CreateScope();

        [DebuggerNonUserCode]
        public void WriteMethodBody(MethodBase m)
        {
            WriteMethodBody(m, this.MethodBodyFilter);

        }

        public void WriteMethodBody(MethodBase m, Predicate<ILBlock.Prestatement> predicate)
        {
            WriteMethodBody(m, predicate, null);
        }



        public void WriteMethodBody(MethodBase m, Predicate<ILBlock.Prestatement> predicate, Action CustomVariableInitialization)
        {
            using (CreateScope())
            {

                try
                {
                    var a = m.ToScriptAttribute();


                    if (a != null && a.OptimizedCode != null)
                    {
                        // we are lucky, as the inline code was provided;

                        WriteIdent();

                        string code = a.OptimizedCode;

                        if (a.UseCompilerConstants)
                        {
                            code = ReplaceWithCompilerConstants(code, m);
                        }

                        Write(code);
                        WriteLine();
                    }
                    else
                    {
                        // we now want to do the code



                        ILBlock xb = new ILBlock(m);

                        #region scan for inline assigments

                        ScanInlineAssigments(xb);

                        #endregion

                        // base construcor call must be first...


                        EmitPrestatementBlock(xb.Prestatements,
                            delegate(ILBlock.Prestatement p)
                            {
                                if (!p.IsConstructorCall())
                                    return true;

                                return predicate != null && predicate(p);
                            }
                        );


                        WriteMethodLocalVariables(xb);

                        if (CustomVariableInitialization != null)
                            CustomVariableInitialization();

                        DebugBreak(a);


                        EmitPrestatementBlock(xb.Prestatements,
                            delegate(ILBlock.Prestatement p)
                            {


                                if (p.IsConstructorCall())
                                    return true;

                                return predicate != null && predicate(p);
                            }
                        );
                    }

                }
                catch (Exception exc)
                {
                    Break("internal compiler error at method " + m.DeclaringType.FullName + "." + m.Name + " : " + exc.Message + "\n" + exc.StackTrace);
                }

            }
        }

        private static void ScanInlineAssigments(ILBlock xb)
        {
            //Action<ILBlock.Prestatement> __inline_assigment = null;

            //__inline_assigment =
            //    delegate(ILBlock.Prestatement ppp)
            //    {
            //        // xxx: try catch bracnch?

            //        ILIfElseConstruct iif = ppp.Instruction.InlineIfElseConstruct;

            //        if (iif != null)
            //        {

            //            ppp.Owner.Extract(iif.FFirst, iif.FLast).PrestatementCommands.ForEach(__inline_assigment);

            //            if (iif.HasElseClause)
            //                ppp.Owner.Extract(iif.TFirst, iif.TLast).PrestatementCommands.ForEach(__inline_assigment);


            //            return;
            //        }

            //        ppp.ValidateInlineAssigment();
            //    };

            //xb.Prestatements.PrestatementCommands.ForEach(__inline_assigment);

            //IList<LocalVariableInfo> u = xb.OwnerMethod.GetMethodBody().LocalVariables;


        }



        public virtual Predicate<ILBlock.Prestatement> MethodBodyFilter
        {
            get
            {
                return null;
            }
        }

        private void WriteComment(string p)
        {
            WriteIdent();
            WriteLine("// " + p);
        }


        public virtual void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
        {
            WriteComment("undefined variable: " + v.LocalIndex);
        }


        #region IsInlineExceptonVariable
        // in large methods the recursion is higher
        RecursionGuard _IsInlineExceptonVariable = new RecursionGuard(32);
        bool _IsInlineExceptonVariable_Verbose = false;


        public bool IsInlineExceptonVariable(LocalVariableInfo i, ILBlock.PrestatementBlock xb)
        {
            using (_IsInlineExceptonVariable.Lock)
            {
                #region _IsInlineExceptonVariable_Verbose
                if (_IsInlineExceptonVariable_Verbose)
                {
                    Console.WriteLine(".enter: " + xb.First.ToString());
                }
                #endregion

                foreach (ILBlock.Prestatement var in xb.PrestatementCommands)
                {
                    if (var.Instruction != null)
                    {
                        #region if
                        ILIfElseConstruct iif = var.Instruction.InlineIfElseConstruct;

                        if (iif != null)
                        {
                            if (IsInlineExceptonVariable(i,
                                xb.ExtractBlock(iif.BodyTrueFirst, iif.BodyTrueLast)
                                ))
                            {
                                return true;
                            }

                            if (iif.HasElseClause)
                            {
                                if (IsInlineExceptonVariable(i,
                                    xb.ExtractBlock(iif.BodyFalseFirst, iif.BodyFalseLast)
                                    ))
                                {
                                    return true;
                                }
                            }
                            //method.Extract(iif.FFirst, iif.FLast);
                        }
                        #endregion

                        #region loop

                        ILLoopConstruct loop = var.Instruction.InlineLoopConstruct;

                        if (loop != null)
                        {
                            if (loop.IsBody(var.Instruction))
                            {
                                #region _IsInlineExceptonVariable_Verbose
                                if (_IsInlineExceptonVariable_Verbose)
                                {
                                    Console.WriteLine(". inside loop");

                                    loop.BodyFirst.ToConsole(4);
                                    loop.BodyLast.ToConsole(4);
                                }
                                #endregion


                                if (IsInlineExceptonVariable(i,
                                    xb.ExtractBlock(loop.BodyFirst, loop.BodyLast)
                                    ))
                                {
                                    return true;
                                }
                            }
                        }

                        #endregion

                        continue;
                    }

                    if (var.Block.IsHandlerBlock)
                    {
                        ILBlock.Prestatement set_exc = var.Block.Prestatements.PrestatementCommands[0];


                        if (set_exc.Instruction != null && set_exc.Instruction.IsStoreLocal)
                        {
                            if (set_exc.Instruction.IsEqualVariable(i))
                                return true;
                            // maybe ok
                        }
                    }

                    // a block case

                    if (IsInlineExceptonVariable(i, var.Block.Prestatements))
                        return true;
                }

            }

            return false;
        }

        #endregion


        public virtual void WriteMethodLocalVariables(ILBlock xb)
        {
            int i = 0;

            //DebugBreak(ScriptAttribute.Of(xb.OwnerMethod));

            foreach (LocalVariableInfo v in xb.Body.LocalVariables)
            {

                #region do not define variables that were marked as inline assigments as they are not used
#if true
                if (SupportsInlineAssigments)
                {
                    if (v.LocalType.IsValueType && !v.LocalType.IsPrimitive)
                    {
                        goto define;
                    }

                    foreach (ILInstruction var in xb.Instructrions)
                    {
                        if (var.IsStoreLocal)
                            if (var.IsEqualVariable(v))
                            {

                                if (!var.IsInlineAssigmentInstruction)
                                    goto define;
                            }
                    }

                    
                    continue;
                }

#endif
                #endregion

            define:

                if (SupportsInlineExceptionVariable)
                {
                    if (IsInlineExceptonVariable(v, xb.Prestatements))
                        continue;

                }



                i++;

                WriteLocalVariableDefinition(v, xb.OwnerMethod);
            }

            if (i > 0)
                WriteLine();
        }

        #endregion


        public string GetDecoratedGUID(Guid g)
        {
            using (StringWriter w = new StringWriter())
            {
                w.Write(GetSpecialChar());

                byte[] b = g.ToByteArray();

                for (int i = 0; i < b.Length; i++)
                    w.Write("{0:x2}", b[i]);

                return w.ToString();
            }
        }

        public abstract void WriteParameters(ILBlock.Prestatement p, MethodBase _method, ILFlow.StackItem[] s, int offset, ParameterInfo[] pi, bool pWritten, string op);

        public bool IsTypeOfOperator(MethodBase m)
        {
            if (m.DeclaringType == typeof(Type))
                if (m.IsStatic)
                    if (m.Name == "GetTypeFromHandle")
                        if (m.GetParameters().Length == 1)
                            if (m.GetParameters()[0].ParameterType == typeof(RuntimeTypeHandle))
                                return true;

            return false;
        }

        protected virtual void WriteTypeOf(ILBlock.Prestatement p, ILInstruction i)
        {
            throw new NotImplementedException();
        }

        public void WriteMethodCall(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
        {
            try
            {
                Type t = m.DeclaringType;
                MethodBase method = m;

                if (IsTypeOfOperator(m))
                {
                    WriteTypeOf(p, i);

                    return;
                }

                if (!m.DeclaringType.IsInterface)
                {
                    if (m.DeclaringType == typeof(object))
                    {
                        // 
                    }

                    if (!ScriptAttribute.IsAnonymousType(t) && ScriptAttribute.Of(m.DeclaringType, true) == null)
                    {
                        method = ResolveImplementationMethod(t, m);

                        if (method == null)
                        {
                            if (SupportsBCLTypesAreNative)
                            {
                                method = m;
                            }
                            else
                            {
                                Break(
                                    @"BCL needs another method, please define it. Cannot call type without script attribute : " + m.DeclaringType + " for " + m + " used at " + i.OwnerMethod.DeclaringType.FullName + "." + i.OwnerMethod.Name + ". If the use of this method is intended, an implementation should be provided with the attribute [Script(Implements=typeof(...)] set.");
                            }
                        }


                    }

                }

                ScriptAttribute ma = ScriptAttribute.Of(method);

                if (ma != null && ma.StringConcatOperator != null)
                {
                    //Write("(");

                    WriteParameters(p, m, i.StackBeforeStrict, 0, m.GetParameters(), false, ma.StringConcatOperator);

                    //Write(")");
                }
                else
                {

                    WriteMethodCallVerified(p, i, method);
                }


            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());

                Break("failure at " + m.DeclaringType.FullName + "." + m.Name + " : " + exc.Message);
            }
        }



        public abstract MethodBase ResolveImplementationMethod(Type t, MethodBase m);
        public abstract MethodBase ResolveImplementationMethod(Type t, MethodBase m, string alias);

        public abstract void WriteTypeConstructionVerified();


        public void WriteTypeConstruction(CodeEmitArgs e)
        {
            MethodBase m = null;
            Type t = null;

            if (e.OpCode == OpCodes.Newarr)
            {
                t = e.i.TargetType;
            }
            else
            {
                m = e.i.TargetConstructor;
                t = m.DeclaringType;
            }

            if (t == typeof(object))
            {
                this.WriteTypeConstructionVerified();

                return;
            }

            // now why did we want to create implementation objects instead of the real things??

            var mza = t.ToScriptAttribute();

            DebugBreak(e.Method.ToScriptAttribute());

            if (!ScriptAttribute.IsCompilerGenerated(t) && mza == null && m != null)
            {
                // .net object called -> there is an implementation type. there can also be a native type 
                // for direct mapping

                MethodBase m_impl = ResolveImplementationMethod(t, m);

                if (m_impl == null)
                {
                    if (SupportsBCLTypesAreNative)
                    {
                        WriteTypeConstructionVerified(e, t, m, mza);
                    }
                    else
                    {
                        Break("(corelib referenced?) implementation for " + t.FullName + " not found - " + m.ToString());
                    }


                }
                else
                {
                    WriteTypeConstructionVerified(e, m_impl.DeclaringType, m_impl, ScriptAttribute.Of(m_impl.DeclaringType, true));
                }
            }
            else
            {
                WriteTypeConstructionVerified(e, t, m, mza);
            }
        }

        public abstract bool EmitTryBlock(ILBlock.Prestatement p);


        public void EmitInstruction(ILBlock.Prestatement p, ILInstruction i)
        {
            EmitInstruction(p, i, null);
        }

        public void EmitInstruction(ILBlock.Prestatement p, ILInstruction i, Type TypeExpectedOrDefault)
        {
            if (CIW[i] == null)
            {
                Break("Opcode not implemented: " + i.OpCode.Name + " at " + i.OwnerMethod.DeclaringType.FullName + "." + i.OwnerMethod.Name);
            }

            var a = new CodeEmitArgs(this)
            {
                i = i,
                p = p,
                TypeExpectedOrDefault = TypeExpectedOrDefault
            };


            try
            {
                CIW[i](a);
            }
            catch (SkipThisPrestatementException)
            {
                throw;
            }
            catch (Exception exc)
            {
                Break("unable to emit " + i.OpCode + " at '" + i.OwnerMethod.DeclaringType.FullName + "." + i.OwnerMethod.Name + "'#" + string.Format("{0:x4}", i.Offset) + ": " + exc.Message + "\n" + exc.StackTrace);
            }

        }

        public void EmitFirstOnStack(ILBlock.Prestatement e)
        {
            Emit(e, e.FirstOnStack);
        }

        public void EmitFirstOnStack(CodeEmitArgs e)
        {
            Emit(e.p, e.FirstOnStack);
        }

        public virtual void WriteAssignment()
        {
            WriteSpace();
            Write("=");
            WriteSpace();
        }

        public void EmitScope(ILBlock.PrestatementBlock bTrue)
        {

            using (CreateScope())
            {
                if (bTrue.PrestatementCommands.Count > 0)
                    EmitPrestatementBlock(bTrue);

            }

        }

        public virtual string GetDecoratedMethodParameter(ParameterInfo p)
        {
            return p.Name;
        }

        public virtual void WriteDecoratedMethodParameter(ParameterInfo p)
        {
            Write(GetDecoratedMethodParameter(p));
        }

        public bool IsDefineAsStatic(MethodBase m)
        {
            ScriptAttribute a = ScriptAttribute.Of(m);

            return a != null && a.DefineAsStatic;
        }

        public void WriteMethodParameterOrSelf(ILInstruction i)
        {
            if (i.TargetIsThis)
            {
                // this or that?

                if (SupportsInlineThisReference && !IsDefineAsStatic(i.OwnerMethod))
                {
                    WriteThisReference();
                }
                else
                {
                    WriteSelf();
                }
            }
            else
                WriteDecoratedMethodParameter(i.TargetParameter);
        }

        protected void WriteThisReference()
        {
            Write("this");
        }

        public string TokenToString(int e)
        {
            StringWriter w = new StringWriter();


            w.Write("{0:x4}", e);

            return w.ToString();
        }

        public virtual string GetTypeNameForFilename(Type z)
        {
            return GetDecoratedTypeNameWithinNestedName(z);
        }

        public virtual string GetDecoratedTypeNameWithinNestedName(Type z)
        {
            return z.Name;
        }



        public virtual string GetDecoratedTypeName(Type z, bool bExternalAllowed)
        {
            ScriptAttribute a = ScriptAttribute.Of(z, false);

            bool bND = false;

            if (a == null)
            {
                bND = ScriptAttribute.Of(z, true) == null;
            }
            else
            {
                bND = a.NoDecoration;

            }

            if (bND)
                return z.Name;
            else
                return GetDecoratedGUID(z.GUID);
        }

        public FieldInfo[] GetAllFields(Type z)
        {
            return z.GetFields(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        }

        public virtual MethodInfo[] GetAllInstanceMethods(Type z)
        {
            if (z == null)
                return null;

            return z.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        }

        public MethodInfo[] GetAllMethods(Type z)
        {
            return z.GetMethods(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        }


        public static bool ParameterInfoArrayEquals(ParameterInfo[] a, ParameterInfo[] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].ParameterType != b[i].ParameterType)
                    return false;
            }

            return true;
        }


        public static bool IsToStringMethod(MethodBase m)
        {
            if (m.Name == "ToString")
            {
                if (!m.IsStatic)
                {
                    if (m.GetParameters().Length == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public bool IsOverloadedMethod(MethodBase m)
        {
            if (m.IsConstructor)
                return true;

            MethodInfo[] x = m.DeclaringType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            int u = 0;

            foreach (MethodInfo var in x)
            {
                if (var.Name == m.Name)
                    u++;
            }

            return u > 1;
        }

        public string GetSafeTypeFullname(Type t)
        {
            string u = "";

            if (t.Namespace != null)
                u += t.Namespace + "_";

            string n = t.Name;

            Type p = t.DeclaringType;

            while (p != null)
            {
                n = p.Name + "_" + n;

                p = p.DeclaringType;
            }

            u += n;
            u = u.Replace('`', '_');
            u = u.Replace('.', '_');

            return u;
        }

        public static ConstructorInfo[] GetAllInstanceConstructors(Type z)
        {
            return z.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        }

        public virtual void WriteForStatement(ILBlock.Prestatement p)
        {
            if (!p.IsValidForStatement)
                BreakToDebugger("not a valid for statement");

            WriteLine();

            //p.Instruction.ToConsole(4);

            WriteIdent();
            Write("for (");

            EmitInstruction(p, p.Instruction);

            Write("; ");

            ILLoopConstruct loop = p.Next.Instruction.InlineLoopConstruct;

            ILBlock.PrestatementBlock block = p.Owner.ExtractBlock(loop.CFirst, loop.CLast);

            block.IsCompound = true;

            if (block.LastPrestatement.Instruction.OpCode.FlowControl == FlowControl.Branch)
            {
                Write("true");
            }
            else
            {
                ILFlow.StackItem ux = block.LastPrestatement.Instruction.StackBeforeStrict[0];

                // xxx: for redundantsy

                //if (ux.SingleStackInstruction.IsStoreLocal)
                //    ux = ux.SingleStackInstruction.StackBeforeStrict[0];

                Emit(block.LastPrestatement, ux);
            }

            Write("; ");

            ILBlock.PrestatementBlock wblock = p.Owner.ExtractBlock(loop.BodyFirst, loop.BodyLast);

            // while differs from for in continue statement

            //wblock.RemoveNopOpcodes();


            EmitInstruction(wblock.LastPrestatement, wblock.LastPrestatement.Instruction);

            Predicate<ILBlock.Prestatement> filter =
                delegate(ILBlock.Prestatement xxp)
                {
                    return xxp == wblock.LastPrestatement;
                };

            WriteLine(")");

            using (CreateScope())
            {
                EmitPrestatementBlock(wblock, filter);
            }

            WriteLine();


        }

        public virtual bool IsSafeLiteralChar(char x)
        {
            return char.IsLetter(x) || char.IsNumber(x);
        }

        public string GetSafeLiteral(string z)
        {
            if (z == null)
                return null;

            var w = new StringWriter();

            foreach (char x in z)
            {

                if (IsSafeLiteralChar(x))
                {
                    w.Write(x);
                }
                else
                {
                    w.Write("_");
                }
            }

            return w.ToString();
        }

        public void WriteSafeLiteral(string z)
        {
            this.Write(GetSafeLiteral(z));
        }

        protected XmlNode GetXMLNode(Type e)
        {
            if (this.XmlDoc == null)
                return null;

            string MethodSig = "T:" + e.FullName;

            XmlNode n = this.XmlDoc.SelectSingleNode(@"//members/member[@name='" + MethodSig + "']");
            return n;
        }

        protected XmlNode GetXMLNodeForMethod(MethodBase m)
        {
            string xtype = "M";

            string mname = m.Name;


            if (mname.StartsWith("get_")) { mname = mname.Substring("get_".Length); xtype = "P"; }
            else if (mname.StartsWith("set_")) { mname = mname.Substring("set_".Length); ; xtype = "P"; }

            string MethodSig = xtype + ":" + m.DeclaringType.Namespace + "." + m.DeclaringType.Name + "." + mname;


            ParameterInfo[] _params = m.GetParameters();

            if (_params.Length > 0)
            {
                MethodSig += "(";

                int i = 0;

                foreach (ParameterInfo MethodParam in _params)
                {
                    if (i++ > 0)
                        MethodSig += ",";

                    MethodSig += MethodParam.ParameterType.FullName;
                }

                MethodSig += ")";
            }

            XmlNode n = this.XmlDoc.SelectSingleNode(@"//members/member[@name='" + MethodSig + "']");
            return n;
        }




        /// <summary>
        /// if there is no implementation class, just use the one referenced if set to true
        /// </summary>
        public virtual bool SupportsBCLTypesAreNative
        {
            get
            {
                return false;
            }
        }

        public virtual bool SupportsAbstractMethods
        {
            get
            {
                return true;
            }
        }

        public virtual void WriteMethodBodyBefore(MethodBase m)
        {

        }

        public virtual void WriteMethodBodyAfter(MethodBase m)
        {

        }
    }
}
