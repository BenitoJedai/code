using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;

using ScriptCoreLib;

using jsc.CodeModel;
using jsc.Script;


namespace jsc
{
    /// <summary>
    /// denotes to a method, or a methods protected region
    /// </summary>
    public partial class ILBlock
    {
        public readonly ILInstruction[] Instructrions;

        public readonly MethodBase OwnerMethod;

        public readonly MethodBody Body;

        /// <summary>
        /// returns null, if dealing with a raw block
        /// </summary>
        public ILBlock[] Children;

        public ILBlock Next;
        public ILBlock Prev;

        public readonly ILBlock Parent;
        public readonly ExceptionHandlingClause Clause;



        public ILFlow Flow;

        /// <summary>
        /// should return false if reference not required, also should be defiend as static for optimization
        /// </summary>
        public bool MethodMakesUseOfThisReference
        {
            get
            {
                int x = 0;

                foreach (ILInstruction i in this.Root.Instructrions)
                {
                    if (i.TargetIsThis)
                        x++;
                }

                if (this.OwnerMethod.IsInstanceConstructor())
                {
                    if (this.OwnerMethod.DeclaringType.BaseType == typeof(object))
                    {
                        return x > 1;

                    }
                }

                return x > 0;
            }
        }

        public ILBlock NextNonClauseBlock
        {
            get
            {
                ILBlock z = this.Next;

                while (z != null && z.Clause != null)
                {
                    z = z.Next;
                }

                return z;
            }
        }

        public ILBlock Root
        {
            get
            {
                ILBlock p = this;

                while (p.Parent != null)
                    p = p.Parent;

                return p;
            }
        }

        public bool IsTryBlock
        {
            get
            {

                if (Clause == null)
                    return false;

                if (First == null)
                    return false;

                return Clause.TryOffset == First.Offset;

            }
        }

        public Prestatement ByInstruction(ILInstruction i)
        {
            if (this.Children == null)
            {
                foreach (Prestatement p in this.Prestatements.PrestatementCommands)
                {
                    if (p.Instruction.UsedStackInfo.Contains(i))
                        return p;
                }
            }
            else
            {
                foreach (ILBlock b in this.Children)
                {
                    Prestatement p = b.ByInstruction(i);

                    if (p == null)
                        continue;

                    return p;
                }
            }

            return null;
        }

        /// <summary>
        /// returns true if .catch block
        /// </summary>
        public bool IsHandlerBlock
        {
            get
            {

                if (Clause == null)
                    return false;

                if (First == null)
                    return false;

                return Clause.HandlerOffset == First.Offset;


            }
        }



        public ILInstruction[] ExtractInstructions(int offset, int length)
        {
            if (length < 0)
                throw new NotSupportedException();

            ILInstruction x = First;

            if (x.Offset > offset)
                throw new NotSupportedException();

            while (x.Offset < offset)
                x = x.Next;

            int count = 1;

            while (x.Offset < (offset + length))
            {
                x = x.Next;
                count++;
            }

            while (x.Offset > (offset + length))
            {
                x = x.Prev;
                count--;
            }

            ILInstruction[] n = new ILInstruction[count];

            while (count-- > 0)
            {

                n[count] = x;
                x = x.Prev;
            }

            if (n.Length == 0)
                throw new ArgumentException();

            return n;
        }

        public ILBlock(ILBlock p, ILBlock prev, ExceptionHandlingClause c, ILInstruction[] i)
        {
            Instructrions = i;

            Parent = p;

            using (new Task("ilblock child"))
            {
                Task.WriteLine("root: " + Root);

                if (i.Length == 0)
                    throw new ArgumentException();

                OwnerMethod = p.OwnerMethod;
                Body = p.Body;

                Prev = prev;
                if (Prev != null)
                    Prev.Next = this;



                Clause = c;

                if (c != null)
                {
                    PopulateChildren();
                }
            }
        }

        void CreateFlow()
        {
            if (!this.IsRoot)
                Debugger.Break();

            using (new Task("binding flow", "method entry"))
            {
                new ILFlow(ResolveFlowBlock, First, new ILFlow.EvaluationStack());

                CreateExceptionEntry();
            }
        }

        void CreateExceptionEntry()
        {
            if (this.IsHandlerBlock)
            {
                using (new Task("binding flow", "catch block"))
                {
                    ILFlow.EvaluationStack s = new ILFlow.EvaluationStack();

                    if (Clause.Flags == ExceptionHandlingClauseOptions.Clause)
                        s.Push(new ILFlow.StackItem(this));


                    new ILFlow(Root.ResolveFlowBlock, First, s);

                }
            }

            if (Children == null)
                return;

            foreach (ILBlock c in Children)
                c.CreateExceptionEntry();
        }

        bool ContainsInstruction(ILInstruction i)
        {
            if (Children != null)
                Debugger.Break();

            return i.IsBetween(this.First, this.Last);
        }

        ILBlock ResolveFlowBlock(ILInstruction i)
        {
            if (Children == null)
            {
                return this.ContainsInstruction(i) ? this : null;
            }
            else
                foreach (ILBlock b in Children)
                {
                    ILBlock x = b.ResolveFlowBlock(i);

                    if (x != null)
                        return x;
                }

            return null;
        }

        static bool DoDiagonstics = false;



        public ILBlock(MethodBase c)
        {

            ScriptAttribute sa = ScriptAttribute.Of(c);

            OwnerMethod = c;
            Body = c.GetMethodBody();

            //
            Instructrions = ILInstruction.GetILAsInstructionArray(c);


            if (sa != null)
            {
                if (DoDiagonstics)
                {
                    Type[] ga = c.DeclaringType.GetGenericArguments();

                    byte[] b = c.GetMethodBody().GetILAsByteArray();
                    using (new Task("IL", b.Length + " bytes"))
                    {
                        using (new Task("generic arguments"))
                        {
                            for (int i = 0; i < ga.Length; i++)
                                Task.WriteLine("0x{0:x8}: {1}", ga[i].MetadataToken, ga[i].Name);
                        }



                        for (int i = 0; i < b.Length; i++)
                            Task.WriteLine("0x{1:x4}: {0:x2}", b[i], i);

                    }
                }
            }

            if (Instructrions == null)
                return;

            PopulateChildren();

            CreateFlow();

            if (DoDiagonstics)
            {
                BlockDiagnostics();
            }

            if (sa != null)
            {
                if (sa.IsDebugCode)
                    this.ToConsole();
            }

        }

        VariableInfo[] _VariableInfoArray;

        public VariableInfo[] VariableInfoArray
        {
            get
            {
                if (IsRoot)
                {
                    if (_VariableInfoArray == null)
                    {
                        List<VariableInfo> i = new List<VariableInfo>();

                        foreach (LocalVariableInfo var in this.OwnerMethod.GetMethodBody().LocalVariables)
                        {
                            VariableInfo u = new VariableInfo();

                            u.LocalVariable = var;
                            u.Method = this.OwnerMethod;

                            u.Attach(this);

                            i.Add(u);
                        }

                        _VariableInfoArray = i.ToArray();
                    }

                    return _VariableInfoArray;
                }
                else
                    return Root.VariableInfoArray;
            }
        }

        public bool IsRoot { get { return this.Parent == null; } }

        void BlockDiagnostics()
        {
            using (new Task(IsRoot ? "root" : "child", this.ToString()))
            {
                if (Children == null)
                {
                    Task.WriteLine(this.Flow == null ? "noflow" : this.Flow.ToString());
                    Task.WriteLine("root: " + this.Root.ToString());

                    using (new Task("pre"))
                        try
                        {
                            foreach (Prestatement x in this.Prestatements.PrestatementCommands)
                            {
                                Task.WriteLine(x.ToString());
                            }
                        }
                        catch
                        {
                            Task.WriteLine("failed");
                        }
                }
                else
                    foreach (ILBlock x in Children)
                    {
                        x.BlockDiagnostics();
                    }
            }
        }


        public override string ToString()
        {
            return "block :: " + this.First + " - " + this.Last;
        }

        public class InlineLogic
        {
            // responsible for ternary and short circut schemas

            public InlineLogic()
            {

            }

            public InlineLogic(SpecialType _hint, ILFlow.StackItem _value)
            {
                hint = _hint;
                value = _value;
            }

            public enum SpecialType
            {
                AndOperator,
                OrOperator,
                Value,
                IfClause
            }

            public bool IsNegative;


            public InlineLogic lhs;
            public InlineLogic rhs;

            public SpecialType hint;

            public ILFlow.StackItem value;

            public ILIfElseConstruct IfClause;

            public void Resolve(ILFlow.StackItem s, ILIfElseConstruct iif)
            {


                if (resolve_iif(iif, OpCodes.Brfalse_S, false, true, false, true)) return;
                if (resolve_iif(iif, OpCodes.Brtrue_S, false, true, true, false)) return;

                this.IfClause = iif;
                this.hint = SpecialType.IfClause;

            }

            void resolve_iif_sub(ILIfElseConstruct iif)
            {
                if (iif.BodyTrueFirst == iif.BodyTrueLast)
                {
                    this.rhs = new InlineLogic(SpecialType.Value, iif.BodyTrueLast.StackAfterStrict[0]);
                }
                else
                {
                    if (iif.FCondition == null)
                    {
                        this.rhs = new InlineLogic(SpecialType.Value, iif.BodyTrueLast.StackAfterStrict[0]);
                    }
                    else
                    {
                        InlineLogic x = new InlineLogic();

                        x.Resolve(null, iif.FCondition);

                        rhs = x;
                    }
                }

                foreach (ILIfElseConstruct i in iif.ExternalConditions)
                {
                    if (resolve_iif_sub_ext(iif, i, OpCodes.Brfalse_S, false, true, false, true)) continue;
                    if (resolve_iif_sub_ext(iif, i, OpCodes.Brtrue_S, false, true, true, false)) continue;

                    // we need additional testing 

                    Task.Error("bad iif in external conditions");

                    Debugger.Break();
                }
            }

            bool resolve_iif_sub_ext(ILIfElseConstruct owner,
                ILIfElseConstruct iif,
                OpCode op,
                bool or_value,
                bool and_value,
                bool neg_lhs,
                bool neg_rhs)
            {
                if (iif.Branch == op)
                {
                    InlineLogic x = new InlineLogic();

                    x.lhs = new InlineLogic(SpecialType.Value, iif.Branch.StackBeforeStrict[0]);
                    x.rhs = lhs;



                    if (owner.IsTResult(or_value))
                    {
                        x.hint = SpecialType.OrOperator;
                        x.lhs.IsNegative = neg_lhs && (owner.Branch == iif.Branch.OpCode);
                        x.rhs.IsNegative = neg_rhs && (owner.Branch == iif.Branch.OpCode);

                    }
                    if (owner.IsTResult(and_value))
                    {
                        x.hint = SpecialType.AndOperator;
                        x.lhs.IsNegative = neg_lhs && (owner.Branch == iif.Branch.OpCode);
                        x.rhs.IsNegative = neg_rhs && (owner.Branch == iif.Branch.OpCode);
                    }


                    if ((owner.IsTResult(false) && iif.Branch == OpCodes.Brfalse_S && owner.Branch == OpCodes.Brtrue_S)
                       || (owner.IsTResult(false) && iif.Branch == OpCodes.Brtrue_S && owner.Branch == OpCodes.Brtrue_S)
                       || (owner.IsTResult(false) && iif.Branch == OpCodes.Brfalse_S && owner.Branch == OpCodes.Brfalse_S)
                       || (owner.IsTResult(true) && iif.Branch == OpCodes.Brtrue_S && owner.Branch == OpCodes.Brfalse_S))
                    {
                        x.lhs.IsNegative = !x.lhs.IsNegative;
                        x.rhs.IsNegative = !x.rhs.IsNegative;
                    }

                    this.lhs = x;

                    return true;
                }

                return false;
            }

            bool resolve_iif(ILIfElseConstruct iif, OpCode o, bool and_value, bool or_value, bool lhs_and_neg, bool lhs_or_neg)
            {
                if (iif.Branch == o)
                {
                    InlineLogic x = new InlineLogic();

                    x.hint = SpecialType.Value;
                    x.value = iif.Branch.StackBeforeStrict[0];

                    if (iif.IsTResult(and_value))
                    {
                        lhs = x;
                        lhs.IsNegative = lhs_and_neg;

                        hint = SpecialType.AndOperator;

                        resolve_iif_sub(iif);

                        return true;
                    }

                    if (iif.IsTResult(or_value))
                    {
                        lhs = x;
                        lhs.IsNegative = lhs_or_neg;
                        hint = SpecialType.OrOperator;

                        resolve_iif_sub(iif);

                        return true;
                    }


                    this.IfClause = iif;
                    this.hint = SpecialType.IfClause;
                    this.IsNegative = iif.Branch != OpCodes.Brtrue_S;

                    resolve_iif_sub(iif);

                    return true;
                }

                return false;
            }
        }

        public class Prestatement
        {
            public MethodBase DeclaringMethod
            {
                get
                {
                    return Owner.OwnerBlock.OwnerMethod;
                }
            }


            public ILInstruction Instruction;
            public ILBlock Block;
            public PrestatementBlock Owner;

            public ILFlow Flow
            {
                get
                {
                    return Instruction.Flow;
                }
            }

            public bool IsInlineAssigment
            {
                get { return Instruction.IsInlineAssigmentInstruction; }
                set { Instruction.IsInlineAssigmentInstruction = value; }
            }

            public static void ValidateInlineAssigment(Prestatement p)
            {
                p.ValidateInlineAssigment();
            }

            public void ValidateInlineAssigment()
            {
                #region try catch

                if (Block != null)
                {
                    if (Block.IsTryBlock)
                    {
                        ILBlock.PrestatementBlock b = Block.Prestatements;

                        bool _pop = false;
                        bool _leave = b.Last == OpCodes.Leave_S && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First;

                        b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last);

                        return;
                    }

                    if (Block.IsHandlerBlock)
                    {
                        ILBlock.PrestatementBlock b = Block.Prestatements;

                        bool _pop = b.First == OpCodes.Pop && (Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause);
                        bool _leave =
                            b.Last == OpCodes.Endfinally
                        ||
                            (b.Last == OpCodes.Leave_S && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First);

                        b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last);

                        return;
                    }
                }


                #endregion

                if (Instruction == null)
                    return;

                #region iif

                ILIfElseConstruct iif = Instruction.InlineIfElseConstruct;

                if (iif != null)
                {
                    // calls populate which in turn calls validate

                    Owner.ExtractBlock(iif.BodyTrueFirst, iif.BodyTrueLast);


                    if (iif.HasElseClause)
                        Owner.ExtractBlock(iif.BodyFalseFirst, iif.BodyFalseLast);

                    return;
                }

                #endregion

                #region loop
                ILLoopConstruct loop = Instruction.InlineLoopConstruct;

                if (loop != null)
                {
                    if (loop.IsBreak(Instruction))
                        return;
                    if (loop.IsContinue(Instruction))
                        return;


                    //if (Instruction.IsDebugCode)
                    //    Debugger.Break();


                    if (loop.BodyFirst == null && loop.BodyLast == null)
                    {
                        // this represents: while (x);
                    }
                    else
                    {
                        Owner.ExtractBlock(loop.BodyFirst, loop.BodyLast);

                        //ILBlock.Prestatement
                        ILBlock.Prestatement[] p = Owner.ExtractBlock(loop.CFirst, loop.CLast).PrestatementCommands.ToArray();

                        if (p.Length == 2)
                        {
                            if (p[0].Instruction.IsStoreLocal)
                            {

                                ILInstruction[] a_ = p[1].GetLoadInstructions(p[0]);

                                if (a_.Length == 1)
                                {
                                    p[0].IsInlineAssigment = true;
                                    a_[0].InlineAssigmentValue = p[0];

                                }
                            }
                        }
                        else
                        {
                            CompilerBase.BreakToDebugger(
                                "unknown while condition at " + this.Instruction.OwnerMethod.ToString()
                                + ". Maybe you did not turn off c# compiler 'optimize code' feature?");
                        }
                    }

                    return;
                }
                #endregion



                if (Next == null)
                    return;

                #region initobj
                if (Instruction == OpCodes.Initobj)
                {
                    // T t = default(T) inline support

                    ILInstruction left = Instruction.StackBeforeStrict[0].SingleStackInstruction;

                    if (Next.Instruction.IsStoreLocal)
                    {
                        ILInstruction right = Next.Instruction.StackBeforeStrict[0].SingleStackInstruction;

                        if (right.IsEqualVariable(left.TargetVariable))
                        {
                            IsInlineAssigment = true;

                            right.InlineAssigmentValue = this;
                        }
                    }

                    return;
                }
                #endregion



                if (!Instruction.IsStoreInstruction)
                    return;

                //if (Instruction.IsDebugCode)
                //    Debugger.Break();

                if (Next.Instruction == null)
                    return;

                // indirect return (special)
                if (this.Instruction.IsStoreLocal &&
                    Next.Instruction.IsAnyOpCodeOf(OpCodes.Br, OpCodes.Br_S) &&
                    Next.Instruction.TargetFlow.Branch == OpCodes.Ret)
                    goto skip;



                // only support if and return statements
                if (
                    !(Next.Instruction.InlineIfElseConstruct != null ||
                    Next.Instruction == OpCodes.Ret))
                    return;


                ILInstruction[] a = Next.GetLoadInstructions(this);

                if (a.Length != 1)
                    return;

                if (a[0].InlineAssigmentValue != null)
                {
                    IsInlineAssigment = a[0].InlineAssigmentValue.Instruction == this.Instruction;

                    return;
                }

                // if there is another load after this point this cannot be inlined

                // how do we detect that?
                // lets check the next statement (skip inline array, follow flow until store command is found)

                if (Next.Next != null)
                    if (Next.Next.Instruction != null)
                        if (Next.Next.GetLoadInstructions(this).Length > 0)
                            return;



                a[0].InlineAssigmentValue = this;

            skip:

                IsInlineAssigment = true;
            }

            private ILInstruction[] GetLoadInstructions(ILBlock.Prestatement e)
            {
                List<ILInstruction> a = new List<ILInstruction>();

                if (e.Instruction.IsStoreInstruction)
                {



                    Instruction.VisitStackInstructions(
                        delegate(ILInstruction i)
                        {
                            if (i.IsLoadInstruction)
                            {
                                if (i.IsEqualStoreLocation(e.Instruction))
                                {
                                    a.Add(i);
                                }
                            }

                            return false;
                        });
                }

                return a.ToArray();
            }

            public bool IsValidForStatement
            {
                get
                {
                    if (!this.Instruction.IsStoreInstruction)
                        return false;

                    if (Next == null)
                        return false;

                    if (Next.Instruction == null)
                        return false;

                    ILLoopConstruct c = Next.Instruction.InlineLoopConstruct;

                    if (c == null)
                        return false;

                    if (c.IsBreak(Next.Instruction))
                        return false;

                    if (c.IsContinue(Next.Instruction))
                        return false;


                    ILBlock.PrestatementBlock cblock = this.Owner.ExtractBlock(c.CFirst, c.CLast);

                    //if (!cblock.LastPrestatement.Instruction.IsStoreLocal)
                    //    return false;

                    ILBlock.PrestatementBlock wblock = this.Owner.ExtractBlock(c.BodyFirst, c.BodyLast);

                    //wblock.RemoveNopOpcodes();

                    if (!wblock.LastPrestatement.Instruction.IsStoreInstruction)
                        return false;

                    // allow empty for statements?

                    //if (wblock.PrestatementCommands.Count <= 1)
                    //    return false;

                    return true;
                }
            }

            public int InlineArrayInitLength
            {
                get
                {
                    if (!IsInlineArrayInit)
                        return -1;

                    ILFlow.StackItem _stack = this.Instruction.StackBeforeStrict[0];
                    ILInstruction _newarr = _stack.SingleStackInstruction;

                    _stack = _newarr.StackBeforeStrict[0];

                    if (_stack.StackInstructions.Length != 1)
                        return -1;

                    if (_stack.SingleStackInstruction.TargetInteger == null)
                        return -1;

                    return _stack.SingleStackInstruction.TargetInteger.Value;
                }
            }

            public bool IsInlineArrayInit
            {
                get
                {
                    if (!this.Instruction.IsStoreInstruction)
                        return false;

                    ILFlow.StackItem _stack = this.Instruction.StackBeforeStrict[0];

                    if (_stack.StackInstructions.Length != 1)
                        return false;

                    ILInstruction _newarr = _stack.SingleStackInstruction;

                    if (_newarr != OpCodes.Newarr)
                        return false;

                    // we need to verify array length, and elements too;

                    return true;
                }
            }

            public bool IsValidInlineArrayInit
            {
                get
                {
                    if (!IsInlineArrayInit)
                        return false;

                    if (InlineArrayInitLength == -1)
                        return false;

                    if (InlineArrayInitElements == null)
                        return false;

                    if (InlineArrayInitElements.All(k => k == null))
                        return false;

                    return true;
                }
            }

            public int InlineArrayInitElementsFound
            {
                get
                {
                    int u = 0;

                    ILFlow.StackItem[] s = InlineArrayInitElements;

                    if (s == null)
                        return -1;

                    foreach (ILFlow.StackItem var in s)
                    {
                        if (var != null)
                            u++;
                    }

                    return u;
                }
            }

            public ILFlow.StackItem[] InlineArrayInitElements
            {
                get
                {
                    int len = InlineArrayInitLength;

                    if (len == -1)
                        return null;

                    ILFlow.StackItem[] u = new ILFlow.StackItem[len];

                    Prestatement p = this;

                    int i = 0;

                    int xlen = len;

                    while (i < xlen)
                    {
                        p = p.Next;

                        // we mus store to this pointer

                        // we found an opcode which is not part of
                        // array initialization, so we assume we are done here
                        if (!p.Instruction.IsStoreInstruction)
                            return u;

                        if (!p.Instruction.StackBeforeStrict[0].SingleStackInstruction.IsEqualStoreLocation(this.Instruction))
                            return u;

                        if (p.Instruction.StackBeforeStrict.Length < 2)
                            return u;

                        int? _offset = p.Instruction.StackBeforeStrict[1].SingleStackInstruction.TargetInteger;

                        if (_offset == null)
                            return null;

                        // the offset must match
                        // 2009.06.16 why?

                        //if (_offset == i)

                        u[_offset.Value] = p.Instruction.StackBeforeStrict[2];

                        //else
                        //{
                        //if (_offset < xlen && _offset > i)
                        //{
                        //    // fill spaces with zeros

                        //    for (int j = i; j < _offset; j++)
                        //    {
                        //        xlen--;

                        //        u[j] = null;
                        //    }

                        //    i = _offset.Value;

                        //    u[i] = p.Instruction.StackBeforeStrict[2];

                        //    continue;
                        //}
                        //else
                        //    return null;
                        //}


                        i++;
                    }

                    return u;
                }
            }

            public bool IsConstructorCall()
            {
                if (Instruction == null)
                    return false;

                return Instruction.IsConstructorCall();
            }

            public bool IsBaseConstructorCall()
            {
                if (Instruction == null)
                    return false;

                return Instruction.IsBaseConstructorCall();
            }
            /// <summary>
            /// returns true, if between "if block"
            /// </summary>
            /// <param name="i"></param>
            /// <returns></returns>
            public bool Contains(ILInstruction i)
            {
                if (Instruction == null)
                    throw new NotSupportedException();

                if (Instruction.InlineIfElseConstruct == null)
                    throw new NotSupportedException();

                if (i.IsBetween(Instruction.InlineIfElseConstruct.Branch, Instruction.InlineIfElseConstruct.Join))
                    return true;

                return false;
            }

            public ILFlow.StackItem FirstOnStack
            {
                get
                {
                    if (this.Instruction.StackBeforeStrict.Length != 1)
                        throw new ArgumentException();

                    return this.Instruction.StackBeforeStrict[0];
                }
            }

            public Prestatement Prev;
            public Prestatement Next;

            public Prestatement RefToNonNop
            {
                get
                {
                    Prestatement p = this;

                    while (p != null && p.Instruction == OpCodes.Nop)
                        p = p.Next;


                    return p;
                }
            }

            public override string ToString()
            {
                try
                {

                    if (this.Instruction != null)
                        return this.Instruction.ToString();
                    if (this.Block != null)
                        return this.Block.ToString();

                    return "unknown";


                }
                catch (Exception exc)
                {
                    return exc.Message;
                }
            }



            public bool IsVerifiedTempVariable(ILInstruction i)
            {
                LocalVariableInfo v = i.TargetVariable;



                if (v != null)
                {
                    int st = i.Flow.OwnerBlock.GetVariableStoreCount(v);
                    int ld = i.Flow.OwnerBlock.GetVariableLoadCount(v);

                    // only if 1 set and 1 get

                    if (st == 1 && ld == 1)
                    {
                        // are we in the same block?



                        return true;

                    }
                }

                return false;
            }

            public bool IsTempVariable()
            {
                // we got first assignment, but are we only set once to be reused?

                try
                {
                    LocalVariableInfo v = this.Instruction.TargetVariable;



                    if (v != null)
                    {
                        return IsVerifiedTempVariable(Instruction);
                    }

                    v = this.FirstOnStack.SingleStackInstruction.TargetVariable;

                    if (v != null)
                    {
                        return IsVerifiedTempVariable(this.FirstOnStack.SingleStackInstruction);
                    }

                    return false;
                }
                catch
                {
                    return false;
                }

            }
        }

        public class PrestatementBlock
        {
            public List<Prestatement> PrestatementMemory = new List<Prestatement>();
            public List<Prestatement> PrestatementCommands = new List<Prestatement>();



            public void RemoveNopOpcodes()
            {
                PrestatementCommands.RemoveAll(
                        delegate(Prestatement v)
                        {
                            return v.Instruction == OpCodes.Nop;
                        }
                    );
            }
            public ILBlock OwnerBlock;

            public ILInstruction First;
            public ILInstruction Last;

            /// <summary>
            /// redundant to inlineassigmentvalue. when true, all ld opcodes should be resolved within block
            /// </summary>
            public bool IsCompound;

            // TODO: rewrite to use next/prev
            public Prestatement SourcePrestatement(Prestatement p, ILInstruction i)
            {
                int z = PrestatementCommands.IndexOf(p);

                while (z-- > 0)
                {
                    ILInstruction ix = PrestatementCommands[z].Instruction;


                    if (ix.TargetVariable.LocalIndex == i.TargetVariable.LocalIndex)
                    {
                        return PrestatementCommands[z];
                    }
                }

                return null;
            }


            public bool DemandsScope
            {
                get
                {
                    int x = 0;

                    foreach (Prestatement p in PrestatementCommands)
                    {
                        if (p.Instruction != OpCodes.Nop)
                            x++;
                    }

                    return x != 1;
                }
            }

            public Prestatement LastPrestatement
            {
                get
                {
                    return PrestatementCommands[PrestatementCommands.Count - 1];
                }
            }

            public PrestatementBlock(ILBlock o, ILInstruction lo, ILInstruction hi)
            {
                this.OwnerBlock = o;
                this.First = lo;
                this.Last = hi;
            }

            public PrestatementBlock ExtractBlock(ILInstruction lo, ILInstruction hi)
            {
                PrestatementBlock n = new PrestatementBlock(this.OwnerBlock, lo, hi);

                if (lo != null && hi != null)
                    n.Populate();

                return n;
            }

            ///// <summary>
            ///// finds the prestatement attached to given instruction, nop opcodes will be excluded
            ///// </summary>
            ///// <param name="x"></param>
            ///// <returns></returns>
            //public Prestatement CommandBy(ILInstruction x)
            //{
            //    for (int i = 0; i < PrestatementCommands.Count; i++)
            //    {
            //        Prestatement p = PrestatementCommands[i];

            //        if (p.Instruction == null)
            //        {
            //            Prestatement z = p.Block.Prestatements.CommandBy(x);

            //            if (z != null)
            //                return z;
            //        }
            //        else
            //        {
            //            if (p.Instruction.UsedStackInfo.Contains(x))
            //            {
            //                return p.RefToNonNop;
            //            }


            //        }
            //    }

            //    return null;
            //}

            public Prestatement MemoryBy(ILFlow.StackItem s)
            {
                ILInstruction t = s.StackInstructions[0];
                ILInstruction f = s.StackInstructions[1];

                for (int i = 0; i < PrestatementMemory.Count; i++)
                {
                    if (PrestatementMemory[i].Contains(t)
                        && PrestatementMemory[i].Contains(f))
                        return PrestatementMemory[i];
                }

                return null;
            }


            public void Populate()
            {
                Populate(this.First, this.Last);

                // renew linkedlist

                Prestatement p = null;

                for (int x = 0; x < this.PrestatementCommands.Count; x++)
                {

                    this.PrestatementCommands[x].Prev = p;


                    if (p != null)
                        p.Next = this.PrestatementCommands[x];

                    p = this.PrestatementCommands[x];
                }

                //CompilerBase.DebugBreak(ScriptAttribute.Of(this.OwnerBlock.OwnerMethod));

                // we need to validate the inner blocks also


                foreach (var pp in PrestatementCommands)
                {
                    Prestatement.ValidateInlineAssigment(pp);
                }
            }

            private void Populate(ILInstruction First, ILInstruction Last)
            {
                if (First.Offset > Last.Offset)
                    return;

                // read out statements, memory blocks for iif, if and while statements and blocks

                if (OwnerBlock == null)
                    throw new Exception();

                ILInstruction i = First;

                //Console.WriteLine(".finding prestatements between [0x{0:x4} to 0x{1:x4}]"
                //    , First.Offset
                //    , Last.Offset);

                string last_jump = "not set";

            next:

                Prestatement p = new Prestatement();

                //Console.WriteLine(".[0x{0:x4}]"
                //    , i.Offset
                //    );

                p.Owner = this;
                p.Instruction = i;
                p.Block = OwnerBlock.By(i);

                if (p.Block != null && p.Block.Clause != null)
                {
                    p.Instruction = null;

                    //Console.WriteLine(".found block between [0x{0:x4} to 0x{1:x4}], skipping",
                    //    p.Block.First.Offset,
                    //    p.Block.Last.Offset);

                    PrestatementCommands.Add(p);

                    last_jump = "block skipped";

                    i = p.Block.Last.Next;
                    goto next;
                }

                // prestatement
                if (i.InlineIfElseConstruct != null)
                {
                    if (i.InlineIfElseConstruct.IsExternalCoCondition)
                    {
                        //Console.WriteLine(".found external iif, added to mem");

                        PrestatementMemory.Add(p);

                        goto advance;
                    }
                    else
                    {
                        if (i.InlineIfElseConstruct.Join.StackBefore.Count == Last.StackAfter.Count)
                        {
                            //Console.WriteLine(".found if clause, added to cmd");

                            PrestatementCommands.Add(p);
                        }
                        else
                        {
                            //Console.WriteLine(".found iif, added to mem");

                            PrestatementMemory.Add(p);
                        }

                        if (i.InlineIfElseConstruct.Join.Offset > Last.Offset)
                        {
                            return;
                        }

                        last_jump = "if skipped";

                        i = i.InlineIfElseConstruct.Join;

                        goto next;
                    }

                }

                if (i.InlineLoopConstruct != null)
                {
                    //Console.WriteLine(".found loop, added to cmd");

                    PrestatementCommands.Add(p);

                    if (i.InlineLoopConstruct.IsBreak(i) || i.InlineLoopConstruct.IsContinue(i))
                        goto skip;

                    if (i.InlineLoopConstruct.Join == null)
                    {
                        return;
                    }

                    if (i.InlineLoopConstruct.Join.Offset > Last.Offset)
                    {
                        return;


                    }

                    last_jump = "loop skipped";

                    i = i.InlineLoopConstruct.Join;

                    goto next;
                }


                // whats this if doing? :)
                // fixme: here is the bug!
                // why is StackAfter null?
                // 2010.01.32: it seems OpCodes.Leave has StackAfter null.. why?
                if (
                    i.StackAfter == null ||

                    i.StackAfter.Count > (Last.StackAfter == null ? 0 : Last.StackAfter.Count)
                    )
                    goto skip;
                else
                {
                    //if (i.OpCode == OpCodes.Nop)
                    //    goto skip;

                    if (i.OpCode == OpCodes.Br_S && i.TargetInstruction == i.Next)
                        goto skip;

                    // investigate unreferenced opcodes


                    AddPrestatement(p);
                }

            skip:

                if (i == Last)
                    return;

                if (i.Offset > Last.Offset)
                {
                    Task.Error("invalid branch in block build - " + last_jump);

                    Console.Error.WriteLine("current {0:x4}, first {1:x4}, last {2:x4}",
                        i.Offset, First.Offset, Last.Offset);

                    //this.Owner.ToConsole();
                    this.OwnerBlock.Parent.ToConsole();

                    Debugger.Break();
                    throw new IndexOutOfRangeException();
                }

            advance:

                last_jump = "next command";


                i = i.Next;

                goto next;


            }

            public override string ToString()
            {
                return "prestatements :: " + this.First + " -> " + this.Last;
            }

            void AddPrestatement(Prestatement p)
            {
                if (p.Instruction.OpCode.FlowControl == FlowControl.Cond_Branch)
                {
                    if (p.Instruction.TargetInstruction.Prev.InlineLoopConstruct == null)
                    {
                        Script.CompilerBase.BreakToDebugger(
                            "unsupported flow detected, try to simplify '"
                            + p.Instruction.OwnerMethod.DeclaringType.FullName + "."
                            + p.Instruction.OwnerMethod.Name
                            + "'. Try ommiting the return, break or continue instruction.");

                    }
                }

                ILInstruction.StackSourceInfo s = p.Instruction.UsedStackInfo;


                ILInstructionList ss = s.SubInstructions;

                if (ss != null)
                {
                    // detect iif
                    if (ss.High.OpCode.FlowControl == FlowControl.Branch)
                    {
                        if (ss.Count > 2)
                        {
                            ILIfElseConstruct iif = ss[ss.Count - 2].InlineIfElseConstruct;

                            if (iif != null)
                            {
                                // what about the other ss items?
                                // verify arguments

                                goto skip;
                            }
                        }
                    }

                    if (ss.Count == 1)
                    {
                        if (ss[0] == OpCodes.Br_S)
                            goto skip;

                        this.Populate(ss.Low, ss.High);

                        goto skip;
                    }
                    else // if (s.SubInstructions[0].Low.StackPopCount == 0)
                    {
                        if (ss.Count == 3)
                        {
                            if (
                                    ss[0].IsAnyOpCodeOf(OpCodes.Ldc_I4_1)
                                && ss[1].IsAnyOpCodeOf(OpCodes.Sub, OpCodes.Add)
                                && ss[2].IsStoreLocal)
                                goto skip;



                        }

                        this.Populate(ss.Low, ss.High);

                        goto skip;
                    }


                }

            skip:

                PrestatementCommands.Add(p);
            }





            //public ILBlock.Prestatement GetFirstAssigmentInstruction(LocalVariableInfo v)
            //{
            //    foreach (ILBlock.Prestatement p in PrestatementCommands)
            //    {
            //        LocalVariableInfo t = p.Instruction.TargetVariable;

            //        if (p.Instruction.IsEqualVariable(v) && p.Instruction.StackBeforeStrict.Length > 0)
            //            return p;


            //    }

            //    return null;
            //}


        }

        PrestatementBlock _Prestatements_cached;


        public PrestatementBlock Prestatements
        {
            [DebuggerNonUserCode]
            get
            {
                if (_Prestatements_cached == null)
                {
                    PrestatementBlock b = new PrestatementBlock(this, First, Last);

                    b.Populate();


                    _Prestatements_cached = b;
                }


                return _Prestatements_cached;
            }
        }



        public ILBlock By(ILInstruction e)
        {
            if (e == null)
            {
                Task.Error("block by zero");
                Debugger.Break();
                return null;
            }

            if (Children == null)
                return null;

            for (int i = 0; i < Children.Length; i++)
            {
                if (e.IsBetween(Children[i].First, Children[i].Last))
                    return Children[i];
            }

            return null;
        }


        public ILInstruction First
        {
            get
            {
                return Instructrions[0];
            }
        }

        public ILInstruction Last
        {
            get
            {
                return Instructrions[Instructrions.Length - 1];
            }
        }


        ExceptionHandlingClause FindNextEHC(ILInstruction f)
        {

            //Console.WriteLine("find try for [0x{0:x4}] in [0x{1:x4} to 0x{2:x4}]", f.Offset, First.Offset, Last.Offset);

            ExceptionHandlingClause fx = null;

            foreach (ExceptionHandlingClause x in Body.ExceptionHandlingClauses)
            {
                if ((x.TryOffset < f.Offset) || (x.TryOffset + x.TryLength + x.HandlerLength) > Last.Offset)
                {
                    //Console.WriteLine("out of bounds:try [0x{0:x4} to 0x{1:x4}] catch [0x{2:x4} to 0x{3:x4}]"
                    //    , x.TryOffset
                    //    , x.TryOffset + x.TryLength

                    //    , x.HandlerOffset
                    //    , x.HandlerOffset + x.HandlerLength
                    //    );

                    continue;
                }

                if (x == Clause)
                {
                    //Console.WriteLine("current");
                    continue;
                }

                if (
                    // first item
                        fx == null
                    // lower offset
                        || x.TryOffset < fx.TryOffset
                    // multiple catches
                        || (x.TryOffset == fx.TryOffset && (x.HandlerOffset + x.HandlerLength > fx.HandlerOffset + fx.HandlerLength))
                    )
                {
                    fx = x;


                    //Console.WriteLine("matched:try [0x{0:x4} to 0x{1:x4}] catch [0x{2:x4} to 0x{3:x4}]"
                    //    , fx.TryOffset
                    //    , fx.TryOffset + fx.TryLength

                    //    , fx.HandlerOffset
                    //    , fx.HandlerOffset + fx.HandlerLength
                    //    );

                    continue;
                }

                //Console.WriteLine("no match");
            }


            //Console.WriteLine();

            return fx;
        }

        void PopulateChildren()
        {
            ExceptionHandlingClause c = FindNextEHC(First);

            if (c != null)
            {
                using (new Task("populating", "try/catch blocks exist"))
                {
                    ILBlock b = null;
                    int count = 0;

                    if ((c.TryOffset - 1) - First.Offset >= 0)
                    {

                        b = new ILBlock(this, null, null, ExtractInstructions(First.Offset, (c.TryOffset - 1) - First.Offset));

                        Task.WriteLine("first try/catch starts block at " + b.Last);


                        count += 1;
                    }

                    while (c != null)
                    {
                        b = new ILBlock(this, b, c, ExtractInstructions(c.TryOffset, c.TryLength - 1));
                        b = new ILBlock(this, b, c, ExtractInstructions(c.HandlerOffset, c.HandlerLength - 1));

                        Task.WriteLine("try/catch block ends at " + b.Last);

                        c = FindNextEHC(b.Last.Next);

                        if (c == null)
                        {
                            b = new ILBlock(this, b, null, ExtractInstructions(b.Last.Next.Offset, (Last.Offset) - b.Last.Next.Offset));
                        }
                        else
                            b = new ILBlock(this, b, null, ExtractInstructions(b.Last.Next.Offset, (c.TryOffset - 1) - b.Last.Next.Offset));

                        count += 3;
                    }

                    Children = new ILBlock[count];

                    while (count-- > 0)
                        b = (Children[count] = b).Prev;
                }

            }
        }

        public void ToConsole()
        {
            ToConsole(true, 0);
        }

        public void ToConsole(bool s, int i)
        {
            string ident = "".PadLeft(i);
            string ident2 = "".PadLeft(i + 4);

            if (Parent == null)
            {
                Console.WriteLine(OwnerMethod.Name);
            }

            if (IsTryBlock)
                Console.WriteLine(ident + ".try [0x{0:x4} to 0x{1:x4}]", First.Offset, Last.Offset);
            else if (IsHandlerBlock)
                Console.WriteLine(ident + ".handler {2} [0x{0:x4} to 0x{1:x4}]", First.Offset, Last.Offset, Clause.Flags);

            if (s || IsTryBlock || IsHandlerBlock)
                Console.WriteLine(ident + "{");

            if (Parent == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(ident2 + ".locals {");

                Console.ForegroundColor = ConsoleColor.Red;
                foreach (LocalVariableInfo loc in this.Body.LocalVariables)
                {
                    Console.WriteLine(ident2 + "  [{0}] {1}", loc.LocalIndex, loc.LocalType);
                }
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(ident2 + "}");

                Console.ForegroundColor = ConsoleColor.Blue;

                foreach (ExceptionHandlingClause exc in this.Body.ExceptionHandlingClauses)
                {
                    Console.WriteLine(ident2 + ".try [0x{0:x4} to 0x{1:x4}] catch [0x{2:x4} to 0x{3:x4}]"
                        , exc.TryOffset
                        , exc.TryOffset + exc.TryLength

                        , exc.HandlerOffset
                        , exc.HandlerOffset + exc.HandlerLength
                        );
                }

                Console.ForegroundColor = ConsoleColor.Gray;
            }

            if (Children == null)
            {

                foreach (ILInstruction x in Instructrions)
                {
                    x.ToConsole(i + 4);
                }


            }
            else
            {
                foreach (ILBlock x in Children)
                    x.ToConsole(false, (x.IsTryBlock || x.IsHandlerBlock ? 4 : 0) + i);
            }

            if (s || IsTryBlock || IsHandlerBlock)
                Console.WriteLine("".PadLeft(i) + "}");
        }



        internal int GetVariableStoreCount(LocalVariableInfo v)
        {
            if (v == null)
                return 0;

            int x = 0;

            foreach (ILInstruction i in this.Instructrions)
            {
                if (i.IsEqualVariable(v) && i.StackPopCount == 1)
                    x++;
            }

            return x;
        }


        internal int GetVariableLoadCount(LocalVariableInfo v)
        {
            int x = 0;

            foreach (ILInstruction i in this.Instructrions)
            {
                if (i.IsEqualVariable(v) && i.StackPopCount == 0)
                    x++;
            }

            return x;
        }

        internal Prestatement GetStaticFieldFinalAssignment(FieldInfo zfn)
        {
            if (zfn.IsStatic && zfn.IsInitOnly)
            {
                foreach (ILBlock.Prestatement p in this.Prestatements.PrestatementCommands)
                {

                    if (p.Instruction != null)
                    {
                        if (p.Instruction.ReferencedMethod != null)
                            return null;

                        if (p.Instruction.TargetField == zfn)
                        {
                            return p;
                        }
                    }
                }
            }

            return null;
        }
    }
}