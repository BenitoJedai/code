using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

using ScriptCoreLib;
using System.Collections;


namespace jsc.Script
{
    /// <summary>
    /// c like language compiler
    /// </summary>
    public abstract class CompilerCLike : CompilerBase
    {
        #region scope
        public class ScopeHelper : IDisposable
        {
            public bool UseNewLine = true;

            CompilerCLike _c;

            public ScopeHelper(CompilerCLike e)
            {
                _c = e;

                e.WriteScopeBegin();
            }

            #region IDisposable Members

            public void Dispose()
            {
                _c.WriteScopeEnd(UseNewLine);
            }

            #endregion
        }


        public override IDisposable CreateScope()
        {
            return new ScopeHelper(this);
        }


        public IDisposable CreateScope(bool _insert_newline)
        {
            ScopeHelper h = new ScopeHelper(this);

            h.UseNewLine = _insert_newline;

            return h;
        }

        public CompilerCLike(TextWriter xw)
            : base(xw)
        {
        }

        #endregion


        public override void EmitLoopBlock(ILBlock.Prestatement p, ILLoopConstruct loop)
        {
            if (loop.IsBreak(p.Instruction))
            {
                WriteIdent();
                Write("break");
                Write(";");
                WriteLine();

            }
            else if (loop.IsContinue(p.Instruction))
            {
                WriteIdent();
                Write("continue");
                Write(";");
                WriteLine();

            }
            else
            {
                WriteIdent();
                Write("while");
                WriteSpace();
                Write("(");


                ILBlock.PrestatementBlock block = p.Owner.ExtractBlock(loop.CFirst, loop.CLast);

                block.IsCompound = true;

                if (block.LastPrestatement.Instruction.OpCode.FlowControl == FlowControl.Branch)
                {
                    Write("true");
                }
                else
                {
                    Emit(block.LastPrestatement, block.LastPrestatement.Instruction.StackBeforeStrict[0]);
                }

                Write(")");
                WriteLine();

                EmitScope(p.Owner.ExtractBlock(loop.BodyFirst, loop.BodyLast));
            }
        }

        public override void WriteTypeConstructionVerified(CodeEmitArgs e, Type mtype, MethodBase m, ScriptAttribute mza)
        {
            string alias = mza == null ? null : mza.GetConstructorAlias();

            if (alias != null)
            {
                MethodBase m_impl = ResolveImplementationMethod(m.DeclaringType, m, alias);

                if (m_impl == null)
                    Break("Constructor '" + alias + "' missing: " + e.i.TargetConstructor + " for " + e.i.TargetConstructor.DeclaringType);

                WriteMethodCall(e.p, e.i, m_impl);

            }
            else
            {

                Write("new");
                WriteSpace();

                if (mza != null && mza.ImplementationType != null)
                {
                    WriteDecoratedTypeName(e.Method.DeclaringType,  mza.ImplementationType);
                }
                else
                    WriteDecoratedTypeName(e.Method.DeclaringType, m.DeclaringType);

                WriteParameterInfoFromStack(m, e.p, e.i.StackBeforeStrict, 0);
            }
        }

        public virtual void MethodCallParameterTypeCast(ParameterInfo p)
        {
        }

        public virtual bool AlwaysDefineAsStatic
        {
            get
            {
                return false;
            }
        }

        public virtual bool AlwaysDoTypeCastOnParameters
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsBooleanSupported
        {
            get
            {
                return true;
            }
        }

        protected virtual bool IsTypeCastRequired(Type e, ILFlow.StackItem s)
        {
            return AlwaysDoTypeCastOnParameters;
        }

        public void WriteParameterInfoFromStack(MethodBase m, ILBlock.Prestatement p, ILFlow.StackItem[] s, int offset)
        {
            ParameterInfo[] pi = m == null ? null : m.GetParameters();

            Write("(");

            var a = m.ToScriptAttribute();

            

            bool pWritten = false;


            if (!m.IsStatic)
            {
                if (AlwaysDefineAsStatic || (a != null && a.DefineAsStatic))
                {
                    pWritten = true;

                    if (AlwaysDoTypeCastOnParameters)
                        WriteTypeCast(m.DeclaringType);

                    Emit(p, s[0]);
                }
            }

            WriteParameters(p, m, s, offset, pi, pWritten, ",");

            Write(")");
        }

        public virtual void WriteArrayToCustomArrayEnumeratorCast(Type Enumerable, Type ElementType, ILBlock.Prestatement p, ILFlow.StackItem s)
        {
            Write("/* autocast " + Enumerable.Name + " */");
            Emit(p, s);
        }

        public override void WriteParameters(ILBlock.Prestatement p, MethodBase _method, ILFlow.StackItem[] s, int offset, ParameterInfo[] pi, bool pWritten, string op)
        {


            if (s != null)
            {
                for (int si = offset; si < s.Length; si++)
                {
                    if (si > offset || pWritten)
                    {
                        Write(op);
                        WriteSpace();
                    }


                    ParameterInfo parameter = null;

                    if (pi.Length > si - offset)
                        parameter = pi[si - offset];

                    if (parameter == null)
                    {
                        if (AlwaysDoTypeCastOnParameters)
                            WriteTypeCast(_method.DeclaringType);

                        Emit(p, s[si]);
                    }
                    else
                    {

                        if (pi == null || !EmitEnumAsString(s[si].SingleStackInstruction, parameter.ParameterType))
                        {
                            #region [Hex] parameter
                            object[] HexA = parameter.GetCustomAttributes(typeof(HexAttribute), false);

                            if (HexA.Length == 1)
                            {
                                int? HexV = s[si].SingleStackInstruction.TargetInteger;

                                if (HexV != null)
                                {
                                    Write(string.Format("0x{0:x8}", HexV.Value));

                                    continue;
                                }
                            }
                            #endregion


                            #region boolean
                            if (IsBooleanSupported)
                            {
                                if (s[si].SingleStackInstruction.TargetInteger != null)
                                {
                                    if (parameter.ParameterType == typeof(bool))
                                    {

                                        if (s[si].SingleStackInstruction.TargetInteger == 0)
                                            WriteKeywordFalse();
                                        else
                                            WriteKeywordTrue();

                                        continue;
                                    }
                                    else
                                    {
                                        MethodCallParameterTypeCast(parameter);
                                    }
                                }
                            }
                            #endregion

                            #region SupportsCustomArrayEnumerator
                            if (SupportsCustomArrayEnumerator)
                            {

                                var SingleStackInstruction = s[si].SingleStackInstruction;
                                if (SingleStackInstruction != null)
                                {
                                    var ReferencedType = SingleStackInstruction.ReferencedType;
                                    if (ReferencedType != null && ReferencedType.IsArray)
                                    {
                                        var ElementType = ReferencedType.GetElementType();
                                        var Enumerable = typeof(IEnumerable<>).MakeGenericType(ElementType);

                                        if (Enumerable.GUID == parameter.ParameterType.GUID)
                                        {
                                            WriteArrayToCustomArrayEnumeratorCast(Enumerable, ElementType, p, s[si]);
                                            continue;
                                        }
                                    }
                                }
                            }
                            #endregion


                            // todo: only if types donot comply

                            if (IsTypeCastRequired(parameter.ParameterType, s[si]))
                                //AlwaysDoTypeCastOnParameters)
                                MethodCallParameterTypeCast(parameter);




                            Emit(p, s[si]);
                        }
                    }
                }
            }
        }

        protected virtual void WriteTypeCast(Type type)
        {

        }

        public void WriteKeywordTrue()
        {
            Write("true");
        }

        public void WriteKeywordFalse()
        {
            Write("false");
        }

        public void WriteKeywordNull()
        {
            Write("null");
        }

        public bool EmitEnumAsStringSafe(CodeEmitArgs e)
        {
            bool bEnum = false;

            if (e.FirstOnStack.StackInstructions.Length == 1 && e.i.TargetVariable != null)
                if (EmitEnumAsString(e.FirstOnStack.SingleStackInstruction, e.i.TargetVariable.LocalType))
                    bEnum = true;

            return bEnum;
        }

        public bool EmitEnumAsString(ILInstruction i, Type type)
        {


            if (type != null && type.IsEnum)
            {
                ScriptAttribute a = ScriptAttribute.Of(type, false);

                if (a != null && a.IsStringEnum)
                {
                    int? v = i.TargetInteger;

                    if (v != null)
                    {
                        string name = Enum.GetName(type, v.Value);

                        ScriptAttribute ma = ScriptAttribute.OfTypeMember(type, name);

                        if (ma != null)
                        {
                            if (ma.ExternalTarget != null)
                            {
                                this.WriteQuotedLiteral(ma.ExternalTarget);

                                return true;

                            }
                        }

                        this.WriteQuotedLiteral(Enum.GetName(type, v.Value));


                        return true;
                    }
                }
            }



            return false;
        }

        public override void EmitLogic(ILBlock.Prestatement p, ILBlock.InlineLogic logic)
        {
            if (logic.hint == ILBlock.InlineLogic.SpecialType.AndOperator)
            {
                if (logic.IsNegative)
                    Write("!");

                Write("(");


                EmitLogic(p, logic.lhs);

                WriteSpace();
                Write("&&");
                WriteSpace();

                EmitLogic(p, logic.rhs);

                Write(")");

                return;
            }

            if (logic.hint == ILBlock.InlineLogic.SpecialType.OrOperator)
            {
                if (logic.IsNegative)
                    Write("!");

                Write("(");
                EmitLogic(p, logic.lhs);

                WriteSpace();
                Write("||");
                WriteSpace();

                EmitLogic(p, logic.rhs);

                Write(")");

                return;
            }

            if (logic.hint == ILBlock.InlineLogic.SpecialType.Value)
            {
                if (logic.IsNegative)
                    Write("!");

                Emit(p, logic.value);


                return;
            }

            if (logic.hint == ILBlock.InlineLogic.SpecialType.IfClause)
            {
                Write("(");

                if (logic.IsNegative)
                {
                    Write("!");

                }

                Write("(");

                if (logic.IfClause.Branch == OpCodes.Brtrue_S
                    || logic.IfClause.Branch == OpCodes.Brfalse_S)
                    Emit(p, logic.IfClause.Branch.StackBeforeStrict[0]);
                else
                    EmitInstruction(p, logic.IfClause.Branch);

                Write(")");

                WriteSpace();
                Write("?");
                WriteSpace();

                ILBlock.PrestatementBlock block;

                block = p.Owner.ExtractBlock(
                    /*logic.IsNegative ? logic.IfClause.FFirst :*/ logic.IfClause.BodyFalseFirst,
                    /*logic.IsNegative ? logic.IfClause.FLast :*/ logic.IfClause.BodyFalseLast
                );

                EmitInstruction(
                  block.PrestatementCommands[block.PrestatementCommands.Count - 1],
                  block.PrestatementCommands[block.PrestatementCommands.Count - 1].Instruction
                  );



                WriteSpace();
                Write(":");
                WriteSpace();

                block = p.Owner.ExtractBlock(
                    /*!logic.IsNegative ?*/ logic.IfClause.BodyTrueFirst /*: logic.IfClause.TFirst*/,
                    /*!logic.IsNegative ?*/ logic.IfClause.BodyTrueLast /*: logic.IfClause.TLast*/
                );


                EmitInstruction(
                    block.PrestatementCommands[block.PrestatementCommands.Count - 1],
                    block.PrestatementCommands[block.PrestatementCommands.Count - 1].Instruction
                    );


                Write(")");

                return;
            }

            Break("EmitAsArgument");
        }

        public override void EmitIfBlock(ILBlock.Prestatement p, ILIfElseConstruct iif)
        {
            WriteLine();
            WriteIdent();
            WriteKeywordIf();

            Write("(");

            if (iif.Branch.IsAnyOpCodeOf(OpCodes.Brfalse_S, OpCodes.Brfalse))
            {
                Emit(p, iif.Branch.StackBeforeStrict[0]);
            }
            else
            {
                if (iif.Branch.IsAnyOpCodeOf(OpCodes.Brtrue_S, OpCodes.Brtrue))
                {
                    ILFlow.StackItem expression = iif.Branch.StackBeforeStrict[0];

                    bool compact = false;

                    if (expression.StackInstructions.Length == 1)
                    {
                        if (expression.SingleStackInstruction.TargetVariable != null)
                            compact = true;
                    }

                    if (compact)
                    {
                        // get if inline value
                        //ILFlow.StackItem if_Value = expression.SingleStackInstruction.InlineAssigmentValue.Instruction.StackBeforeStrict[0];


                        //if (p.Instruction.IsDebugCode)
                        //    Debugger.Break();

                        if (SupportsInlineAssigments && expression.StackInstructions.Length == 1 &&
                            expression.SingleStackInstruction.InlineAssigmentValue != null)
                        {
                            #region redundant !! removal
                            expression = expression.SingleStackInstruction.InlineAssigmentValue.Instruction.StackBeforeStrict[0];

                            if (expression.IsSingle)
                            {
                                if (expression.SingleStackInstruction.IsNegativeOperator)
                                {
                                    Emit(p, expression.SingleStackInstruction.StackBeforeStrict[0]);

                                    goto skipx;
                                }
                            }
                            #endregion


                            Write("!");
                            Emit(p, expression);

                        skipx:
                            ;
                        }
                        else
                        {
                            Write("!");
                            Emit(p, expression);
                        }
                    }
                    else
                    {
                        Write("!");
                        Write("(");
                        Emit(p, expression);

                        Write(")");
                    }

                }
                else Break("invalid if block");
            }

            Write(")");
            WriteLine();


            EmitScope(p.Owner.ExtractBlock(iif.BodyTrueFirst, iif.BodyTrueLast));

            if (iif.HasElseClause)
            {
                WriteIdent();
                WriteKeywordElse();

                EmitScope(p.Owner.ExtractBlock(iif.BodyFalseFirst, iif.BodyFalseLast));


            }

            WriteLine();
        }

        private void WriteKeywordElse()
        {
            Write("else");
            WriteLine();
        }

        private void WriteKeywordIf()
        {
            Write("if ");
        }

        public void WriteScopeEnd()
        {
            WriteScopeEnd(true);
        }

        public void WriteScopeEnd(bool usenewline)
        {
            Ident--;

            WriteIdent();
            Write("}");

            if (usenewline)
                WriteLine();
        }

        public void WriteScopeBegin()
        {
            WriteIdent();
            Write("{");
            WriteLine();

            Ident++;
        }

        public virtual void WriteInstanceOfOperator(ILInstruction value, Type type)
        {
            throw new NotImplementedException("instanceof operator not supported");
        }


        public void WriteInlineOperator(ILBlock.Prestatement p, ILInstruction i, string op)
        {
            ILFlow.StackItem[] s = i.StackBeforeStrict;

            #region is operator support
            if (s[0].SingleStackInstruction == OpCodes.Isinst)
            {
                if (s[1].SingleStackInstruction == OpCodes.Ldnull)
                {
                    // write the instanceof opcode

                    ILInstruction _instanceof =
                        s[0].SingleStackInstruction;

                    WriteInstanceOfOperator(
                        _instanceof.StackBeforeStrict[0].SingleStackInstruction,
                        _instanceof.TargetType
                    );


                    //Emit(p, s[0]);

                    return;
                }
            }
            #endregion

            Write("(");
            Emit(p, s[0]);
            WriteSpace();
            Write(op);
            WriteSpace();
            Emit(p, s[1]);
            Write(")");
        }

        public void WriteQuotedLiteral(string e)
        {

            WriteQuote();
            this.WriteDecoratedLiteralString(e);
            WriteQuote();

        }

        public override bool IsVisibleCharacter(char c)
        {
            return (c >= 'a' && c <= 'z')
                || (c >= 'A' && c <= 'Z')
                || (c >= '0' && c <= '9')
                || " +-/*:_#%.,=;!&<>|()[]{}?".IndexOf(c) > -1;

        }

        public override void WriteMethodHint(MethodBase m)
        {
            WriteIdent();
            WriteCommentLine((m.IsStatic ? "static " : "instance ") + m.DeclaringType.FullName + "." + m.Name);
        }

        public void WriteCommentLine(string p)
        {
            WriteLine("// " + p);
        }

        public void WriteBoxedCommentLine(string e)
        {
            WriteLine("/* " + e + " */");
        }

        public void WriteMachineGeneratedWarning()
        {
            WriteBoxedCommentLine("prevalidated at " + DateTime.Now.ToUniversalTime());
        }

        public void WriteKeywordReturn()
        {
            Write("return");
        }

        public override void WriteBoxedComment(string p)
        {
            Write("/* " + p + " */");
        }

        public virtual bool WillReturnPointerToThisOnConstructorReturn
        {
            get
            {
                return false;
            }
        }



        public void WriteReturn(ILBlock.Prestatement p, ILInstruction i)
        {
            DebugBreak(i.OwnerMethod.ToScriptAttribute());

            ILFlow.StackItem[] s = i.StackBeforeStrict;

            //WriteBoxedComment("return");

            WriteKeywordReturn();



            if (i.OwnerMethod is ConstructorInfo)
            {
                if (WillReturnPointerToThisOnConstructorReturn)
                {
                    WriteSpace();
                    WriteSelf();
                }

                return;
            }
            
            if (((MethodInfo)i.OwnerMethod).ReturnType == typeof(void))
                return;

            Action<ILInstruction> WriteReturnValue =
                left =>
                {
                    if (SupportsInlineAssigments)
                    {
                        if (left.InlineAssigmentValue != null)
                        {
                            //WriteBoxedComment("inline");

                            WriteSpace();


                            ILBlock.Prestatement _p = left.InlineAssigmentValue;
                            ILInstruction _i = _p.Instruction;

                            if (_i.IsStoreInstruction)
                                WriteReturnParameter(_p, _i.StackBeforeStrict.Single().SingleStackInstruction);
                            else
                                WriteReturnParameter(_p, _i);

                            return;
                        }
                    }

                    //WriteBoxedComment(" br ");

                    WriteSpace();
                    //Emit(p, s[0]);

                    WriteReturnParameter(p, left);
                };

            if (s.Length == 1)
            {
                WriteReturnValue(s[0].SingleStackInstruction);

            }
            else
            {
                if (i.TargetFlow.Branch == OpCodes.Ret)
                {

                    // this is a dirty fix for return branch with a value
                    s = i.Prev.StackBeforeStrict;

                    if (s.Length == 1)
                    {
                        WriteReturnValue(s[0].SingleStackInstruction);
                    }
                }
            }

        }

        public virtual void WriteReturnParameter(ILBlock.Prestatement _p, ILInstruction _i)
        {
            var m = _i.OwnerMethod as MethodInfo;

            if (m != null && m.ReturnType == typeof(bool))
            {
                if (_i.OpCode == OpCodes.Ldc_I4_0)
                {
                    WriteKeywordFalse();
                    return;
                }

                if (_i.OpCode == OpCodes.Ldc_I4_1)
                {
                    WriteKeywordTrue();
                    return;
                }
            }

            EmitInstruction(_p, _i);
        }

        public virtual void WriteExceptionVar()
        {
            Write("__exc");
        }


        public virtual void ConvertTypeAndEmit(CodeEmitArgs e, string x)
        {
            Write("((" + x + ")(");
            EmitFirstOnStack(e);
            Write("))");
        }

        public void WriteBlockComment(string Summary)
        {
            string x = Summary.Trim();

            WriteIdent();
            WriteLine("/**");

            foreach (string var in x.Split('\n'))
            {
                WriteIdent();
                WriteLine(" * " + var.Trim());

            }

            WriteIdent();
            WriteLine(" */");
        }


        public static MethodBase GetMethodImplementation(AssamblyTypeInfo MySession, ILInstruction i)
        {
            MethodBase method = i.ReferencedMethod;


            ScriptAttribute method_type_attribute = ScriptAttribute.OfProvider(method.DeclaringType);

            if (method_type_attribute == null)
            {
                MethodBase impl = MySession.ResolveImplementation(method.DeclaringType, method);

                if (impl == null)
                    throw new NotSupportedException("implementation not found for type import : " +
                        (method.DeclaringType.FullName
                        ?? method.DeclaringType.Name) + " :: " + method);

                method = impl;

            }
            return method;
        }

        public bool IsEmptyImplementationType(Type e)
        {
            ScriptAttribute a = ScriptAttribute.Of(e);

            if (a == null)
                return false;

            if (a.Implements == null)
                return false;
            MethodInfo[] u = GetAllMethods(e);

            foreach (MethodInfo var in u)
            {
                ScriptAttribute s = ScriptAttribute.Of(var);

                if (s != null)
                {
                    if (s.ExternalTarget != null)
                        continue;

                    if (s.StringConcatOperator != null)
                        continue;
                }

                return false;
            }

            return true;


        }


        protected virtual void WriteTypeInstanceConstructors(Type z)
        {
            ConstructorInfo[] zci = GetAllInstanceConstructors(z);


            foreach (ConstructorInfo zc in zci)
            {
                WriteMethodSignature(zc, false);
                WriteMethodBody(zc);

            }

            WriteLine();
        }

        public override void WriteTypeInstanceMethods(Type z, ScriptAttribute za)
        {
            MethodInfo[] mx = GetAllInstanceMethods(z);
            MethodInfo[] mxb = GetAllInstanceMethods(z.BaseType);

            int idx = 0;

            foreach (MethodInfo m in mx)
            {


                ScriptAttribute ma = ScriptAttribute.Of(m);

                bool dStatic = ma != null && ma.DefineAsStatic;

                if (dStatic)
                {
                    continue;
                }

                if (ma != null && (ma.IsNative || ma.ExternalTarget != null))
                    continue;

                if (ma == null && !m.IsStatic && (za.HasNoPrototype))
                    continue;


                // if overmaps another method in base class and it isnt virtual
                // issue warning

                if (mxb != null)
                {
                    ParameterInfo[] m_params = m.GetParameters();

                    foreach (MethodBase var in mxb)
                    {
                        if (var.Name == m.Name)
                        {
                            // signatures must match

                            ParameterInfo[] var_params = var.GetParameters();


                            if (!var.IsVirtual && !var.IsAbstract && ParameterInfoArrayEquals(m_params, var_params))
                            {
                                Break("method overlapps " + m.DeclaringType.FullName + " - " + m.ToString() + " :: " + var.DeclaringType.FullName + " - " + var.ToString());
                            }

                        }
                    }
                }



                if (idx++ > 0)
                    WriteLine();


                if (m.ToScriptAttributeOrDefault().IsDebugCode)
                {
                    WriteIdent();
                    WriteCommentLine("[Script(IsDebugCode = true)]");
                }
                 
                WriteXmlDoc(m);
                WriteMethodSignature(m, dStatic);

                if (ScriptIsPInvoke(m))
                {
                }
                else if (!m.IsAbstract)
                    WriteMethodBody(m);


            }
        }

        public override void EmitPrestatement(ILBlock.Prestatement p)
        {
            if (p.Instruction.IsLoadInstruction)
                BreakToDebugger("statement cannot be a load instruction (compiler fault?): " + p.Instruction.Location);


            WriteIdent();

            try
            {
                EmitInstruction(p, p.Instruction);
                WriteLine(";");
            }
            catch (SkipThisPrestatementException exc)
            {
                WriteLine();
            }
            catch
            {
                throw;
            }
        }



    }
}
