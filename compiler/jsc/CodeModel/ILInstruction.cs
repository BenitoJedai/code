using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

using jsc.CodeModel;

using ScriptCoreLib;

namespace jsc
{
    public class ILInstructionList : List<ILInstruction>
    {
        /// <summary>
        /// returns instruction with highest offset
        /// </summary>
        public ILInstruction High
        {
            get
            {
                ILInstruction z = null;

                foreach (ILInstruction i in this)
                {
                    if (z == null || z.Offset < i.Offset)
                        z = i;

                }

                return z;
            }
        }

        /// <summary>
        /// returns instruction with lowest offset
        /// </summary>
        public ILInstruction Low
        {
            get
            {
                ILInstruction z = null;

                foreach (ILInstruction i in this)
                {
                    if (z == null || z.Offset > i.Offset)
                        z = i;

                }

                return z;
            }
        }
    }

    public class ILLoopConstruct
    {
        // while
        //      (
        public ILInstruction CFirst;
        /// <summary>
        /// condition
        /// </summary>
        public ILInstruction CLast;
        //      )
        public ILInstruction Branch;
        // [optional]
        // {
        public ILInstruction BodyFirst;
        public ILInstruction BodyLast;
        // }
        // [optional]
        public ILInstruction Join;



        public bool IsConditionOnly
        {
            get { return BodyFirst == null && BodyLast == null; }
        }

        public bool IsContinue(ILInstruction e)
        {
            if (e != Branch && e != null)
            {
                if (e.TargetInstruction == CFirst)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsBreak(ILInstruction e)
        {
            if (e != Branch && e != null)
            {
                if (e.TargetInstruction == Join)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsBody(ILInstruction i)
        {
            if (IsContinue(i))
                return false;

            if (IsBreak(i))
                return false;


            return true;
        }
        public string ToString(ILInstruction e)
        {
            try
            {
                StringWriter w = new StringWriter();


                if (IsContinue(e))
                {
                    w.Write("continue");
                    return w.ToString();
                }

                if (IsBreak(e))
                {
                    w.Write("break");
                    return w.ToString();
                }


                w.Write("while ( [0x{0:x4} to 0x{1:x4}] ) "
                      , CFirst.Offset
                      , CLast.Offset);

                if (!IsConditionOnly)
                {
                    w.Write("[0x{0:x4} to 0x{1:x4}] "
                          , BodyFirst.Offset
                          , BodyLast.Offset
                          );
                }

                if (Join == null)
                {
                    w.Write("nojoin");
                }
                else
                {
                    w.Write("join [0x{0:x4}]"
                        , Join.Offset);
                }

                return w.ToString();
            }
            catch
            {
                return "error";
            }
        }

        public override string ToString()
        {
            return ToString(null);


        }
    }

    public class ILIfElseConstruct
    {
        // if (...)
        public ILInstruction Branch;
        // {
        /// <summary>
        /// first instruction in true block
        /// </summary>
        public ILInstruction BodyTrueFirst;
        public ILInstruction BodyTrueLast;
        // }
        // else
        // {
        /// <summary>
        /// first instruction in false block
        /// </summary>
        public ILInstruction BodyFalseFirst;
        public ILInstruction BodyFalseLast;
        // }
        public ILInstruction Join;

        public bool HasElseClause
        {
            get
            {
                return !(BodyFalseFirst == null && BodyFalseLast == null);
            }
        }

        /// <summary>
        /// gets short circut iif
        /// </summary>
        public ILIfElseConstruct FCondition
        {
            get
            {
                ILInstruction i = BodyTrueFirst;

                goto skip;
            next:

                i = i.Next;

                if (i == null || i.Offset > BodyTrueLast.Offset)
                    return null;

            skip:



                if (i.InlineIfElseConstruct == null)
                    goto next;

                if (i.InlineIfElseConstruct.IsExternalCoCondition)
                    goto next;


                if (i.InlineIfElseConstruct.BodyTrueLast == BodyTrueLast
                    || i.InlineIfElseConstruct.BodyFalseLast == BodyTrueLast)
                    return i.InlineIfElseConstruct;

                return null;
            }
        }

        public bool IsJumpAhead
        {
            get
            {
                if (this.HasElseClause)
                    throw new NotSupportedException();

                return false;
            }
        }

        public bool IsResultOf(ILInstruction i, bool v)
        {
            return (
                i.IsAnyOpCodeOf(OpCodes.Ldc_I4_0) && !v) || (i.IsAnyOpCodeOf(OpCodes.Ldc_I4_1) && v
                );
        }
        public bool IsFResult(bool v)
        {
            return BodyTrueFirst == BodyTrueLast && IsResultOf(BodyTrueFirst, v);
        }

        public bool IsTResult(bool v)
        {
            return BodyFalseFirst == BodyFalseLast && IsResultOf(BodyFalseFirst, v);
        }

        public string ToString(ILInstruction e)
        {
            if (e == Join)
                return "(join)";

            return ToString();
        }

        public ILIfElseConstruct[] ExternalConditions
        {
            get
            {
                int x = 0;

                foreach (ILInstruction i in this.BodyTrueFirst.BranchSources)
                    if (i.InlineIfElseConstruct != null && i.InlineIfElseConstruct.IsExternalCoCondition)
                        x++;

                ILIfElseConstruct[] z = new ILIfElseConstruct[x];

                x = 0;

                foreach (ILInstruction i in this.BodyTrueFirst.BranchSources)
                    if (i.InlineIfElseConstruct != null && i.InlineIfElseConstruct.IsExternalCoCondition)
                        z[x++] = i.InlineIfElseConstruct;


                return z;
            }
        }
        public bool IsExternalCoCondition
        {
            get
            {
                if (HasElseClause)
                    return false;

                ILInstruction i = BodyTrueFirst;

            next:
                if (this.Join == i)
                    return false;

                if (i.StackAfter.Count == 0)
                {
                    if (i.InlineIfElseConstruct != null)
                    {
                        if (i.InlineIfElseConstruct.Join == this.Join)
                            goto go_next;

                        if (i.InlineIfElseConstruct.Join.Offset > this.Join.Offset)
                            return true;
                    }
                }


            go_next:
                i = i.Next;
                goto next;


            }
        }



        public bool IsPlainIfClause
        {
            get
            {
                return Join.StackBefore.Count == 0;
            }
        }

        public string ToString(ILInstruction low, ILInstruction high)
        {
            using (StringWriter w = new StringWriter())
            {
                if (low == high)
                {
                    if (low.IsAnyOpCodeOf(OpCodes.Ldc_I4_1))
                        w.Write("1");
                    else if (low.IsAnyOpCodeOf(OpCodes.Ldc_I4_0))
                        w.Write("0");
                    else
                        w.Write("[0x{0:x4}]", low.Offset);
                }
                else
                    w.Write("[0x{0:x4} to 0x{1:x4}]", low.Offset, high.Offset);

                return w.ToString();
            }
        }

        public override string ToString()
        {
            using (StringWriter w = new StringWriter())
            {

                if (HasElseClause)
                {
                    if (IsPlainIfClause)
                    {

                        w.Write("if ");
                        w.Write(Branch.StackBeforeStrict[0]);

                        if (this.FCondition != null)
                            w.Write(" C ");

                        w.Write("[0x{0:x4} to 0x{1:x4}]", BodyTrueFirst.Offset, BodyTrueLast.Offset);
                        w.Write(" else ");

                        w.Write("[0x{0:x4} to 0x{1:x4}]", BodyFalseFirst.Offset, BodyFalseLast.Offset);

                        w.Write(" join ");
                        w.Write("[0x{0:x4}]", Join.Offset);

                    }
                    else
                    {
                        ILIfElseConstruct c = this;

                        w.Write(c.Branch);

                        w.Write(" ? {0} : {1}"
                            , c.ToString(c.BodyTrueFirst, c.BodyTrueLast)
                            , c.ToString(c.BodyFalseFirst, c.BodyFalseLast));

                    }

                }
                else
                {
                    if (IsExternalCoCondition)
                    {
                        w.Write("external cocondition");
                    }
                    else
                    {
                        w.Write("if (0x{0:x4}) [0x{1:x4} to 0x{2:x4}] join 0x{3:x4}", Branch.Offset, BodyTrueFirst.Offset, BodyTrueLast.Offset, Join.Offset);
                    }
                }

                return w.ToString();
            }
        }
    }

    /// <summary>
    /// provides additional information
    /// </summary>
    public class ILInstructionInfo
    {
        public readonly ILInstruction Instruction;

        public ILInstructionInfo(ILInstruction i)
        {
            Instruction = i;
        }

        public ILBlock.Prestatement Prestatement
        {
            get
            {
                return PrestatementByInstruction(Instruction);
            }
        }

        public ILBlock.Prestatement TargetPrestatement
        {
            get
            {
                return PrestatementByInstruction(Instruction.TargetInstruction);
            }
        }

        public ILBlock.Prestatement PrestatementByInstruction(ILInstruction i)
        {
            ILBlock b = i.Flow.OwnerBlock;

            return b.Root.ByInstruction(i);
        }
    }

    public class ILInstruction
    {
        /// <summary>
        /// cached value
        /// </summary>
        public ILBlock.Prestatement InlineAssigmentValue;

        /// <summary>
        /// if set to true, this should never be rendered, nor its local variable declared
        /// </summary>
        public bool IsInlineAssigmentInstruction;

        /// <summary>
        /// denotes to previous instruction in array
        /// </summary>
        public ILInstruction Prev;

        /// <summary>
        /// denotes to next instruction in array
        /// </summary>
        public ILInstruction Next;

        public ILFlow Flow;

        /// <summary>
        /// this is the index of an instrucction, not to be confused with offset
        /// </summary>
        public readonly int Index;
        public readonly int Offset;

        public readonly MethodBase OwnerMethod;

        public readonly OpCode OpCode;
        public readonly byte[] OpParam;
        public readonly byte[] OpParamData;

        public ILFlow.EvaluationStack StackBefore;
        public ILFlow.EvaluationStack StackAfter;

        public readonly ILInstructionInfo Info;

        public class StackSourceInfo
        {
            public ILInstruction Entry;

            public int[] Index;

            public int Ripple;


            public bool Contains(ILInstruction i)
            {
                for (int idx = 0; idx < Index.Length; idx++)
                {
                    if (Index[idx] > 0)
                        if (RootBlock.Instructrions[idx] == i)
                            return true;
                }

                return false;
            }

            public ILBlock RootBlock
            {
                get
                {
                    return Entry.Flow.OwnerBlock.Root;
                }
            }

            public StackSourceInfo Collect(ILInstruction e)
            {
                return Collect(e, null);
            }

            public StackSourceInfo Collect(ILInstruction e, Predicate<ILInstruction> p)
            {
                Entry = e;

                Index = new int[RootBlock.Instructrions.Length];

                VisitInstructions(e, p);

                bool b = false;

                Ripple = -3;

                for (int i = 0; i < Index.Length; i++)
                {
                    if ((Index[i] > 0 && b) || (Index[i] == 0 && !b))
                    {
                        Ripple++;
                        b = !b;
                    }
                }

                return this;
            }

            /// <summary>
            /// should return unreferenced instructions
            /// </summary>
            public ILInstructionList SubInstructions
            {
                get
                {
                    if (Ripple > 0)
                    {

                        int gen = Ripple;
                        int idx = 0;

                        ILInstructionList n = new ILInstructionList();

                        // skip zero
                        while (Index[idx] == 0) idx++;

                    loop:
                        // skio non-zero
                        while (Index[idx] > 0) idx++;
                        while (Index[idx] == 0)
                        {
                            n.Add(RootBlock.Instructrions[idx]);
                            idx++;
                        }

                        if (gen > 2)
                        {


                            gen -= 2;

                            goto loop;
                        }


                        if (n.Count == 0)
                            Debugger.Break();

                        return n;

                    }

                    return null;
                }
            }


            void VisitInstructions(ILInstruction i)
            {
                VisitInstructions(i, null);
            }

            void VisitInstructions(ILInstruction i, Predicate<ILInstruction> p)
            {
                Helper.Invoke(p, i);


                Index[i.Index]++;

                ILFlow.StackItem[] s = i.StackBeforeStrict;

                foreach (ILFlow.StackItem x in s)
                {
                    string si = x.ToString();

                    foreach (ILInstruction z in x.StackInstructions)
                    {
                        if (z != null)
                            VisitInstructions(z, p);
                    }


                }
            }
        }


        public bool IsNegativeOperator
        {
            get
            {
                if (this != OpCodes.Ceq)
                    return false;


                ILFlow.StackItem[] s = StackBeforeStrict;

                if (s.Length != 2)
                    return false;

                bool b = false;

                if (s[1].SingleStackInstruction == OpCodes.Ldc_I4_0
                    && s[0].SingleStackInstruction.ReferencedType == typeof(bool))
                    b = true;

                if (s[1].SingleStackInstruction == OpCodes.Ldc_I4_0
                    && s[0].SingleStackInstruction.IsBooleanOpcode)
                    b = true;

                return b;
            }
        }

        public bool IsBooleanOpcode
        {
            get
            {
                return
                    this == OpCodes.Ceq |
                    this == OpCodes.Clt |
                    this == OpCodes.Cgt;
            }
        }

        public StackSourceInfo VisitStackInstructions(Predicate<ILInstruction> p)
        {
            return new StackSourceInfo().Collect(this, p);
        }

        public StackSourceInfo UsedStackInfo
        {
            get
            {
                return VisitStackInstructions(null);
            }
        }

        [DebuggerNonUserCode]
        public static implicit operator OpCode(ILInstruction e)
        {
            if (e == null)
                return new OpCode();

            return e.OpCode;
        }

        public bool IsInlinePrefixOperatorStatement(OpCode e)
        {

            ILFlow.StackItem[] s = this.StackBeforeStrict;

            if (s.Length == 1)
            {
                if (s[0].StackInstructions.Length > 1)
                    return false;

                if (s[0].SingleStackInstruction == e)
                {
                    s = s[0].SingleStackInstruction.StackBeforeStrict;

                    if (s.Length == 2)
                    {
                        ILInstruction _left = s[1].SingleStackInstruction;
                        ILInstruction _right = s[0].SingleStackInstruction;

                        if (_left == OpCodes.Ldc_I4_1
                            && _right.TargetVariable != null
                            && _right.TargetVariable.LocalIndex ==
                            this.TargetVariable.LocalIndex)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;

        }

        public bool IsInlinePrefixOperator(OpCode e)
        {
            ILFlow.StackItem[] s = this.StackBeforeStrict;

            if ((this == e) && this.Next == OpCodes.Dup)
            {
                if (s[1].SingleStackInstruction == OpCodes.Ldc_I4_1)
                {
                    ILInstruction ins = s[0].SingleStackInstruction;

                    if (ins.TargetField != null)
                    {
                        if (ins.TargetField == this.Next.Next.TargetField)
                        {

                            return true;
                        }
                    }
                    else if (ins.TargetVariable != null)
                    {
                        if (ins.TargetVariable.LocalIndex == this.Next.Next.TargetVariable.LocalIndex)
                        {

                            return true;
                        }
                    }
                    else
                        Debugger.Break();


                }
            }

            return false;
        }

        public bool IsInlinePostSub
        {
            get
            {
                if (this.Next == this.OpCode || this.Next == OpCodes.Dup)
                {
                    if (this.Next.Next == OpCodes.Ldc_I4_1)
                    {
                        if (this.Next.Next.Next == OpCodes.Sub)
                        {
                            if (this.Next.Next.Next.Next.TargetVariable == null)
                                return false;

                            if (this.Next.Next.Next.Next.TargetVariable.LocalIndex == this.TargetVariable.LocalIndex)
                                return true;

                        }
                    }
                }

                return false;
            }
        }

        public bool IsInlinePostAdd
        {
            get
            {
                if (this.Next == this.OpCode || this.Next == OpCodes.Dup)
                {
                    if (this.Next.Next == OpCodes.Ldc_I4_1)
                    {
                        if (this.Next.Next.Next == OpCodes.Add)
                        {
                            if (this.Next.Next.Next.Next.TargetVariable == null)
                                return false;

                            if (this.Next.Next.Next.Next.TargetVariable.LocalIndex == this.TargetVariable.LocalIndex)
                                return true;

                        }
                    }
                }

                return false;
            }
        }

        //public bool IsPartOfInlineCode
        //{
        //    get
        //    {
        //        if (this.Method.DeclaringType.GetCustomAttributes(typeof(ScriptAttribute), true).Length == 1)
        //        {
        //            if (this.Method.GetCustomAttributes(typeof(ScriptAttribute), true).Length > 0)
        //            {
        //                ScriptAttribute sa = this.Method.GetCustomAttributes(typeof(ScriptAttribute), true)[0] as ScriptAttribute;

        //                if (sa.InlineCode)
        //                    return true;
        //            }
        //        }

        //        return false;
        //    }
        //}

        /// <summary>
        /// returns a stackarray on which stack analysis can be maded without chaning this instructuion
        /// </summary>
        public ILFlow.StackItem[] StackBeforeStrict
        {
            get
            {
                return StackBefore.Clone().Pop(StackPopCount);
            }
        }

        public ILFlow.StackItem[] StackAfterStrict
        {
            get
            {
                return StackAfter.Clone().Pop(StackPushCount);
            }
        }

        #region loop
        Variant<ILLoopConstruct> _cached_loop;

        /// <summary>
        /// shall detect assiocated loop construct
        /// </summary>
        public ILLoopConstruct InlineLoopConstruct
        {
            get
            {
                if (!_cached_loop.Dirty)
                {
                    _cached_loop.Dirty = true;

                    // loop start
                    // loop continue
                    // loop break



                    if (this.OpCode.FlowControl == FlowControl.Branch)
                    {
                        ILInstruction x = this.TargetInstruction;

                        // continue [x=CFirst]
                        {
                            ILInstruction branch = x.BranchSources.Low;

                            if (branch != this)
                            {
                                //Console.WriteLine("lookup 0x{0:x4} from 0x{1:x4}", branch.Offset, this.Offset);
                                ILLoopConstruct loop = branch.InlineLoopConstruct;

                                if (loop != null && loop.CFirst == x)
                                    return _cached_loop.Value = loop;
                            }
                        }

                        // break
                        if (x.Prev.OpCode.FlowControl == FlowControl.Cond_Branch)
                        {
                            ILInstruction branch = x.Prev.TargetInstruction.Prev;
                            ILLoopConstruct loop = branch.InlineLoopConstruct;

                            if (loop != null && loop.Join == x)
                            {
                                if (this.Offset < loop.Branch.Offset || this.Offset > loop.Join.Offset)
                                    return null;

                                return _cached_loop.Value = loop;
                            }
                        }
                    }

                    ILLoopConstruct z = new ILLoopConstruct();


                    #region detect while(...) {...}

                    if (this.OpCode.FlowControl == FlowControl.Branch)
                    {
                        z.Branch = this;

                        if (z.Branch.Next == z.Branch.TargetInstruction)
                        {
                            z.CFirst = z.Branch.TargetInstruction;
                            z.CLast = z.CFirst.BranchSources.High;
                            z.Join = z.CLast.Next;

                            if (z.CLast.Offset < z.CFirst.Offset)
                                return null;

                            return _cached_loop.Value = z;
                        }

                        z.BodyFirst = z.Branch.Next;

                        if (z.BodyFirst == null)
                            return null;

                        if (z.BodyFirst.BranchSources.Count == 0)
                            return null;

                        z.CLast = z.BodyFirst.BranchSources.High;
                        z.Join = z.CLast.Next;
                        z.CFirst = z.Branch.TargetInstruction;
                        z.BodyLast = z.CFirst.Prev;

                        if (z.CLast.Offset < z.CFirst.Offset)
                            return null;

                        if (z.BodyLast.Offset < z.BodyFirst.Offset)
                            return null;

                        // are we dealing with for statement ?

                        return _cached_loop.Value = z;

                    }
                    #endregion


                    return null;
                }

                return _cached_loop.Value;
            }
        }

        #endregion

        public bool IsBetween(ILInstruction low, ILInstruction high)
        {
            return Offset >= low.Offset && Offset <= high.Offset;
        }

        public static ILIfElseConstruct ResolveIfClause(ILInstruction t, ILInstruction f)
        {
            if (t.Offset > f.Offset)
                throw new NotSupportedException();

            ILInstruction z = t;
            ILIfElseConstruct iif;

            while (true)
            {
                iif = z.InlineIfElseConstruct;

                if (iif != null)
                {
                    if (!t.IsBetween(iif.BodyTrueFirst, iif.BodyTrueLast))
                        return null;

                    if (!f.IsBetween(iif.BodyFalseFirst, iif.BodyFalseLast))
                        return null;

                    return iif;
                }

                z = z.Prev;
            }

        }




        RecursionGuard _InlineIfElseConstruct_guard = new RecursionGuard();

        Variant<ILIfElseConstruct> _cached_iif;

        /// <summary>
        /// shall detect assiocated if block
        /// </summary>
        public ILIfElseConstruct InlineIfElseConstruct
        {
            get
            {
                if (_cached_iif.Dirty)
                    return _cached_iif.Value;

                _cached_iif.Dirty = true;

                using (_InlineIfElseConstruct_guard.Lock)
                {

                    if (this == OpCodes.Br_S)
                        return null;

                    // fixme: while continue/break detected as elseif
                    ILIfElseConstruct z = new ILIfElseConstruct();

                    // case 1: this is a cond_branch instruction

                    if (this.OpCode.FlowControl == FlowControl.Cond_Branch
                        && this.BranchTargets.Length == 1)
                    {
                        ILInstruction t = this.TargetInstruction;

                        bool ret_alias = false;

                        if (
                            t.Prev.OpCode.FlowControl == FlowControl.Branch
                            &&
                            t.Prev.TargetFlow.Branch == OpCodes.Ret)
                        {
                            if (t.Prev.TargetFlow.Branch.StackPopCount == 1)
                            {
                                if (t.Prev.TargetFlow.Branch.StackBeforeStrict[0].SingleStackInstruction == t.Prev.TargetInstruction)
                                    ret_alias = true;

                            }
                            if (t.Prev.TargetFlow.Branch.StackPopCount == 0)
                            {
                                if (t.Prev.TargetFlow.Branch == t.Prev.TargetInstruction)
                                    ret_alias = true;
                            }
                        }



                        if (t.Prev.OpCode.FlowControl != FlowControl.Branch
                            || (t.Prev.InlineLoopConstruct != null)
                            || ret_alias
                            || t.Prev.TargetInstruction == t)
                        {
                            z.Branch = this;
                            z.Join = t;

                            z.BodyTrueFirst = z.Branch.Next;
                            z.BodyTrueLast = z.Join.Prev;

                            if (z.BodyTrueLast.Offset < z.BodyTrueFirst.Offset)
                                return null;

                            return _cached_iif.Value = z;
                        }

                        bool is_if_else = t.Prev.IsAnyOpCodeOf(OpCodes.Br, OpCodes.Br_S);




                        if (t.Prev.Flow != Flow)
                        {
                            if (t.Prev.Flow.OwnerBlock.IsHandlerBlock)
                            {
                                Script.CompilerBase.BreakToDebugger("unable to recover from if/else detection at " + OwnerMethod.DeclaringType.FullName + "." + OwnerMethod.Name);

                            }
                        }


                        if (is_if_else)
                        {
                            z.Branch = this;
                            z.Join = t.Prev.BranchTargets[0];

                            if (z.Branch.Offset > z.Join.Offset)
                                return null;

                            z.BodyFalseFirst = t;
                            z.BodyFalseLast = z.Join.Prev;

                            if (z.BodyFalseLast.Offset < z.BodyFalseFirst.Offset)
                                return null;

                            z.BodyTrueFirst = z.Branch.Next;
                            z.BodyTrueLast = z.BodyFalseFirst.Prev.Prev;

                            if (z.BodyTrueLast.Offset < z.BodyTrueFirst.Offset)
                                return null;


                            return _cached_iif.Value = z;
                        }
                    }

                    return null;
                }
            }
        }

        RecursionGuard _IndirectReturnPrestatement_guard = new RecursionGuard();

        /// <summary>
        /// returns true if the call is made to return
        /// </summary>
        public ILBlock.Prestatement IndirectReturnPrestatement
        {
            get
            {
                if (TargetInstruction == null)
                    return null;

                using (_IndirectReturnPrestatement_guard.Lock)
                {
                    ILBlock.Prestatement tx = this.Info.TargetPrestatement;

                    if (tx == null)
                        Debugger.Break();

                    tx = tx.RefToNonNop;

                    if (tx != null && tx.Instruction != null && tx.Instruction.OpCode.FlowControl == FlowControl.Return)
                    {
                        return tx;
                    }

                    return null;
                }
            }
        }

        public bool IsTryBlockEntry
        {
            get
            {
                MethodBody b = this.OwnerMethod.GetMethodBody();

                foreach (ExceptionHandlingClause x in b.ExceptionHandlingClauses)
                {
                    if (x.TryOffset == this.Offset)
                        return true;
                }

                return false;
            }
        }

        public bool IsFlowBreak
        {
            get
            {
                if (OpCode.FlowControl == FlowControl.Cond_Branch)
                    return true;

                if (OpCode.FlowControl == FlowControl.Throw)
                    return true;

                if (NextInstruction != null && NextInstruction.BranchSources.Count > 0)
                    return true;




                return false;
            }
        }



        /// <summary>
        /// returns next instruction or a branch target if unconditional branch
        /// </summary>
        public ILInstruction NextInstruction
        {
            get
            {
                if (OpCode.FlowControl == FlowControl.Branch)
                    return BranchTargets[0];

                if (OpCode.FlowControl == FlowControl.Next)
                    return Next;

                if (OpCode.FlowControl == FlowControl.Call)
                    return Next;

                if (OpCode.FlowControl == FlowControl.Return)
                    return null;

                if (OpCode.FlowControl == FlowControl.Throw)
                    return null;

                if (OpCode.FlowControl == FlowControl.Meta)
                {
                    if (Next == OpCodes.Callvirt)
                        return Next;

                }

                System.Diagnostics.Debug.WriteLine("unknown flowcontrol value in opcode: " + OpCode.FlowControl);

                //Script.CompilerBase.BreakToDebugger("unknown flowcontrol value in opcode: ");

                return null;
            }
        }


        public ILInstruction First
        {
            get
            {
                ILInstruction i = this;

                while (i.Prev != null)
                    i = i.Prev;

                return i;
            }
        }

        public ILInstruction Last
        {
            get
            {
                ILInstruction i = this;

                while (i.Next != null)
                    i = i.Next;

                return i;
            }
        }



        public ILInstruction(MethodBase m, ref byte[] x, int o, ILInstruction p, int i)
        {
            Info = new ILInstructionInfo(this);
            OwnerMethod = m;
            Prev = p;
            Index = i;
            Offset = o;
            OpCode = OpCodeLookup(ref x);
            OpParam = OpParamLookup(ref x);
            OpParamData = OpParamDataLookup(ref x);
        }

        public byte[] OpParamDataLookup(ref byte[] x)
        {
            byte[] z = new byte[OpParamDataSize];

            Array.Copy(x, Offset + OpCode.Size + OpParamSize, z, 0, z.Length);

            return z;
        }

        public int OpParamDataSize
        {
            get
            {
                if (OpCode.OperandType == OperandType.InlineSwitch)
                    return OpParamAsInt32 * 4;
                else
                    return 0;
            }
        }

        public byte OpParamAsInt8
        {
            get
            {
                return OpParam[0];
            }
        }


        public int OpParamDataAsInt32(int i)
        {
            int x = i * 4;

            return OpParamData[x + 0] + (OpParamData[x + 1] << 8) + (OpParamData[x + 2] << 16) + (OpParamData[x + 3] << 24);
        }

        public int OpParamAsInt32
        {
            get
            {
                if (OpParam.Length == 4)
                    return OpParam[0] + (OpParam[1] << 8) + (OpParam[2] << 16) + (OpParam[3] << 24);
                else
                    throw new Exception();
            }
        }

        // http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfSystemReflectionEmitOpCodesClassLdc_R8Topic.asp
        public unsafe double OpParamAsDouble
        {
            get
            {
                int n = 8;

                if (OpParam.Length == n)
                {

                    /*
                    byte* b = (byte*)&x;

                    while (n-- > 0)
                        b[n] = OpParam[n];
                    */

                    double x = 0;

                    OpParamAsBytes((byte*)&x, n);

                    return x;
                }
                else
                    throw new Exception();
            }
        }

        public unsafe long OpParamAsLong
        {
            get
            {
                int n = sizeof(long);

                if (OpParam.Length == n)
                {

                    /*
                    byte* b = (byte*)&x;

                    while (n-- > 0)
                        b[n] = OpParam[n];
                    */

                    long x = 0;

                    OpParamAsBytes((byte*)&x, n);

                    return x;
                }
                else
                    throw new Exception();
            }
        }

        public unsafe void OpParamAsBytes(byte* b, int n)
        {
            while (n-- > 0)
                b[n] = OpParam[n];
        }

        public unsafe float OpParamAsFloat
        {
            get
            {
                int n = 4;

                if (OpParam.Length == n)
                {
                    float x = 0;

                    byte* b = (byte*)&x;

                    while (n-- > 0)
                        b[n] = OpParam[n];

                    return x;
                }
                else
                    throw new Exception();
            }
        }

        /*
        public unsafe long OpParamAsLong
        {
            get
            {
                int n = 8;

                if (OpParam.Length == n)
                {
                    long x = 0;

                    byte* b = (byte*)&x;

                    while (n-- > 0)
                        b[n] = OpParam[n];

                    return x;
                }
                else
                    throw new Exception();
            }
        }
        */
        public byte[] OpParamLookup(ref byte[] x)
        {
            byte[] z = new byte[OpParamSize];

            Array.Copy(x, Offset + OpCode.Size, z, 0, z.Length);

            return z;
        }

        public ILInstruction ByOffset(int i)
        {
            ILInstruction z = this;


            int o = z.Offset + z.Size + i;

            while (z != null)
            {
                if (z.Offset == o)
                    return z;

                z = i < 0 ? z.Prev : z.Next;
            }

            throw new ArgumentException();
        }


        public ILInstructionList BranchSources
        {
            get
            {
                ILInstruction z = First;

                ILInstructionList list = new ILInstructionList();

                while (z != null)
                {
                    if (z != this)
                    {
                        foreach (ILInstruction i in z.BranchTargets)
                            if (i == this)
                            {
                                list.Add(z);
                            }
                    }

                    z = z.Next;
                }

                return list;
            }
        }

        public ILInstruction[] BranchTargets
        {
            get
            {
                if (OpCode.OperandType == OperandType.ShortInlineBrTarget)
                    return new ILInstruction[] { ByOffset((sbyte)OpParam[0]) };

                if (OpCode.OperandType == OperandType.InlineBrTarget)
                    return new ILInstruction[] { ByOffset(OpParamAsInt32) };

                if (OpCode.OperandType == OperandType.InlineSwitch)
                {
                    ILInstruction[] z = new ILInstruction[OpParamAsInt32];

                    for (int zi = 0; zi < z.Length; zi++)
                        z[zi] = ByOffset(OpParamDataAsInt32(zi));

                    return z;
                }

                return new ILInstruction[0];
            }
        }

        /// <summary>
        /// returns the size of an instruction including operand size
        /// </summary>
        public int Size
        {
            get
            {
                return OpCode.Size + OpParamSize + OpParamDataSize;
            }
        }

        public int StackPopCount
        {
            get
            {
                if (OpCode.StackBehaviourPop == StackBehaviour.Pop0) return 0;
                if (OpCode.StackBehaviourPop == StackBehaviour.Pop1) return 1;
                if (OpCode.StackBehaviourPop == StackBehaviour.Pop1_pop1) return 2;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popi) return 1;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popi_pop1) return 2;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popi_popi) return 2;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popi_popi_popi) return 3;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popi_popi8) return 2;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popi_popr4) return 2;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popi_popr8) return 2;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popref) return 1;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popref_pop1) return 2;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popref_popi) return 2;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popref_popi_pop1) return 3;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popref_popi_popi) return 3;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popref_popi_popi8) return 3;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popref_popi_popr4) return 3;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popref_popi_popr8) return 3;
                if (OpCode.StackBehaviourPop == StackBehaviour.Popref_popi_popref) return 3;
                if (OpCode.StackBehaviourPop == StackBehaviour.Varpop)
                {
                    if (this == OpCodes.Newobj)
                    {
                        if (TargetConstructor == null)
                        {
                            Debugger.Break();

                            return 0;
                        }

                        return TargetConstructor.GetParameters().Length;
                    }

                    if (OpCode.FlowControl == FlowControl.Return)
                    {
                        if (OwnerMethod is MethodInfo)
                            return ((MethodInfo)OwnerMethod).ReturnType == typeof(void) ? 0 : 1;
                        else
                            return 0;
                    }

                    if (OpCode.FlowControl == FlowControl.Call)
                    {


                        if (TargetMethod != null)
                        {
                            //Script.CompilerBase.DebugBreak(ScriptAttribute.Of(TargetMethod));

                            if (TargetMethod.CallingConvention == CallingConventions.VarArgs)
                            {
                                return this.StackBefore.Count;
                            }

                            if (this == OpCodes.Callvirt)
                            {
                                return (TargetMethod.IsStatic ? 0 : 1) + TargetMethod.GetParameters().Length;

                            }

                            return (TargetMethod.IsStatic ? 0 : 1) + TargetMethod.GetParameters().Length;
                        }

                        if (TargetConstructor != null)
                        {
                            return (TargetConstructor.IsStatic ? 0 : 1) + TargetConstructor.GetParameters().Length;
                        }

                        return 0;
                    }
                }

                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// returns the instruction control is being branched from
        /// </summary>
        public ILInstruction SourceInstruction
        {
            get
            {
                if (BranchSources.Count == 1)
                    return BranchSources[0];
                else throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// returns the instruction control is being branched to
        /// </summary>
        public ILInstruction TargetInstruction
        {
            get
            {
                if (BranchTargets.Length == 1)
                    return BranchTargets[0];
                else throw new InvalidOperationException();
            }
        }

        public ILFlow TargetFlow
        {
            get
            {

                return TargetInstruction.Flow;
            }
        }

        public bool TargetIsThis
        {
            get
            {
                return !OwnerMethod.IsStatic && this == OpCodes.Ldarg_0;
            }

        }

        public ParameterInfo TargetParameter
        {
            get
            {
                ParameterInfo[] p = OwnerMethod.GetParameters();

                if (OwnerMethod.IsStatic)
                {

                    if (this == OpCodes.Ldarg_0) return p[0];
                    if (this == OpCodes.Ldarg_1) return p[1];
                    if (this == OpCodes.Ldarg_2) return p[2];
                    if (this == OpCodes.Ldarg_3) return p[3];
                    if (this.IsAnyOpCodeOf(OpCodes.Ldarga, OpCodes.Ldarga_S, OpCodes.Ldarg, OpCodes.Ldarg_S)) return p[OpParamAsInt8];
                    if (this == OpCodes.Starg_S) return p[OpParamAsInt8];




                }
                else
                {
                    if (this == OpCodes.Ldarg_0) return null;
                        // Debugger.Break();


                    if (this == OpCodes.Ldarg_1) return p[0];
                    if (this == OpCodes.Ldarg_2) return p[1];
                    if (this == OpCodes.Ldarg_3) return p[2];
                    if (this.IsAnyOpCodeOf(OpCodes.Ldarga, OpCodes.Ldarga_S, OpCodes.Ldarg_S)) return p[OpParamAsInt8 - 1];
                    if (this == OpCodes.Starg_S) return p[OpParamAsInt8 - 1];
                }



                return null;
            }
        }


        static OpCode[] TargetMethodFilter =
            new OpCode[] 
            {

            };

        public MethodInfo TargetMethod
        {
            get
            {
                if (this.OpParam.Length != 4)
                    return null;

                if (TargetMethodFilter.Contains(this.OpCode))
                    return null;

                Type[] ma = (OwnerMethod.IsGenericMethod) ? OwnerMethod.GetGenericArguments() : null;



                try
                {

                    // OwnerMethod.Module.Assembly.ModuleResolve += new ModuleResolveEventHandler(Assembly_ModuleResolve);

                    MethodBase x = OwnerMethod.Module.ResolveMethod(OpParamAsInt32,
                        OwnerMethod.DeclaringType.GetGenericArguments(),
                        ma);

                    if (x is MethodInfo)
                        return (MethodInfo)x;
                    else
                        return null;
                }

                catch (ArgumentException e)
                {

                    // not a method nor a ctor
                    return null;
                }
                catch (Exception __exc)
                {
                    throw;
                }


            }
        }

        Module Assembly_ModuleResolve(object sender, ResolveEventArgs e)
        {

            return null;
        }

        public ConstructorInfo TargetConstructor
        {
            get
            {

                if (this.OpParam.Length != 4)
                    return null;

                if (TargetMethodFilter.Contains(this.OpCode))
                    return null;

                Type[] ma = (OwnerMethod.IsGenericMethod) ? OwnerMethod.GetGenericArguments() : null;

                try
                {
                    MethodBase x = OwnerMethod.Module.ResolveMethod(OpParamAsInt32, OwnerMethod.DeclaringType.GetGenericArguments(), ma);

                    if (x is ConstructorInfo)
                        return (ConstructorInfo)x;
                    else
                        return null;
                }

                catch (ArgumentException e)
                {
                    // not a method nor a ctor
                    return null;
                }
                catch { throw; }



            }
        }

        public int StackPushCount
        {
            get
            {
                if (OpCode.StackBehaviourPush == StackBehaviour.Push0) return 0;
                if (OpCode.StackBehaviourPush == StackBehaviour.Push1) return 1;
                if (OpCode.StackBehaviourPush == StackBehaviour.Push1_push1) return 2;
                if (OpCode.StackBehaviourPush == StackBehaviour.Pushi) return 1;
                if (OpCode.StackBehaviourPush == StackBehaviour.Pushi8) return 1;
                if (OpCode.StackBehaviourPush == StackBehaviour.Pushr4) return 1;
                if (OpCode.StackBehaviourPush == StackBehaviour.Pushr8) return 1;
                if (OpCode.StackBehaviourPush == StackBehaviour.Pushref) return 1;
                if (OpCode.StackBehaviourPush == StackBehaviour.Varpush)
                {
                    if (OpCode.FlowControl == FlowControl.Call)
                    {


                        if (TargetMethod != null)
                        {
                            return TargetMethod.ReturnType == typeof(void) ? 0 : 1;
                        }

                        if (TargetConstructor != null)
                        {
                            return 0;
                        }
                    }
                }

                throw new Exception("unable to extract stackpushcount");



            }
        }

        public bool IsFirstInFlow
        {
            get
            {
                return Flow == null ? false : Flow.Entry == this;
            }
        }

        public bool IsLastInFlow
        {
            get
            {
                return Flow == null ? false : Flow.Branch == this;
            }
        }

        public Type TargetType
        {
            get
            {
                try
                {
                    return OwnerMethod.Module.ResolveType(OpParamAsInt32, this.OwnerMethod.DeclaringType.GetGenericArguments(), this.OwnerMethod.IsGenericMethod ? this.OwnerMethod.GetGenericArguments() : null);
                }
                catch
                {
                    try
                    {
                        return OwnerMethod.Module.ResolveType(OpParamAsInt32, this.OwnerMethod.DeclaringType.GetGenericArguments(), this.OwnerMethod.GetGenericArguments());
                    }
                    catch
                    {
                        Debugger.Break();

                        return null;
                    }
                }

            }
        }

        public bool IsTargetMethod(Type t, string n)
        {
            if (this.TargetMethod != null)
                return this.TargetMethod.DeclaringType == t && this.TargetMethod.Name == n;

            if (this.TargetConstructor != null)
                return this.TargetConstructor.DeclaringType == t && this.TargetConstructor.Name == n;

            return false;
        }

  
        public bool IsLoadLocal
        {
            get
            {
                return IsAnyOpCodeOf(OpCodes.Ldloc, OpCodes.Ldloca, OpCodes.Ldloca_S, OpCodes.Ldloc_S,
                                OpCodes.Ldloc_3, OpCodes.Ldloc_2, OpCodes.Ldloc_1, OpCodes.Ldloc_0);
            }
        }

        public bool IsStoreLocal
        {
            get
            {
                return IsAnyOpCodeOf(OpCodes.Stloc, OpCodes.Stloc_S,
                                OpCodes.Stloc_3, OpCodes.Stloc_2, OpCodes.Stloc_1, OpCodes.Stloc_0);
            }
        }



        // redundant IsOpCodeOf
        public bool IsAnyOpCodeOf(params OpCode[] e)
        {
            foreach (OpCode z in e)
                if (z == this)
                    return true;


            return false;
        }

        public LocalVariableInfo TargetVariable
        {
            get
            {
                if (this == OpCodes.Ldloc_0 || this == OpCodes.Stloc_0) return this.OwnerMethod.GetMethodBody().LocalVariables[0];
                if (this == OpCodes.Ldloc_1 || this == OpCodes.Stloc_1) return this.OwnerMethod.GetMethodBody().LocalVariables[1];
                if (this == OpCodes.Ldloc_2 || this == OpCodes.Stloc_2) return this.OwnerMethod.GetMethodBody().LocalVariables[2];
                if (this == OpCodes.Ldloc_3 || this == OpCodes.Stloc_3) return this.OwnerMethod.GetMethodBody().LocalVariables[3];
                if (this == OpCodes.Ldloc_S || this == OpCodes.Stloc_S || this == OpCodes.Ldloca_S)
                    return this.OwnerMethod.GetMethodBody().LocalVariables[this.OpParamAsInt8];
                if (this == OpCodes.Ldloc || this == OpCodes.Stloc || this == OpCodes.Ldloca)
                    return this.OwnerMethod.GetMethodBody().LocalVariables[this.OpParamAsInt32];



                return null;
            }
        }

        VariableInfo _TargetVariableInfo;

        public VariableInfo TargetVariableInfo
        {
            get
            {
                if (TargetVariable == null)
                    return null;

                if (_TargetVariableInfo == null)
                {
                    VariableInfo u = new VariableInfo();

                    u.LocalVariable = TargetVariable;
                    u.Method = this.OwnerMethod;

                    u.Attach(this.Flow.OwnerBlock.Root);
                }

                return _TargetVariableInfo;
            }

            set
            {
                _TargetVariableInfo = value;
            }
        }

        /// <summary>
        /// returns method or constructor
        /// </summary>
        public MethodBase ReferencedMethod
        {
            get
            {
                if (this.TargetMethod != null)
                    return this.TargetMethod;

                if (this.TargetConstructor != null)
                    return this.TargetConstructor;

                return null;

            }
        }

        /// <summary>
        /// returns ctor, parameter,  var, field, method type
        /// </summary>
        public Type ReferencedType
        {
            get
            {

                Type cexc = null;

                if (this.TargetConstructor != null)
                {
                    cexc = this.TargetConstructor.DeclaringType;
                }
                else if (this.TargetParameter != null)
                {
                    cexc = this.TargetParameter.ParameterType;
                }
                else if (this.TargetVariable != null)
                {
                    cexc = this.TargetVariable.LocalType;
                }
                else if (this.TargetField != null)
                {
                    cexc = this.TargetField.FieldType;

                }
                else if (this.TargetMethod != null)
                {
                    cexc = this.TargetMethod.ReturnParameter.ParameterType;

                }
                return cexc;
            }
        }

        public string TargetLiteral
        {
            get
            {
                return OwnerMethod.Module.ResolveString(OpParamAsInt32);
            }
        }

        public long? TargetLong
        {
            get
            {
                if (this == OpCodes.Ldc_I8) return this.OpParamAsLong;


                return null;
            }
        }


        public int? TargetInteger
        {
            get
            {

                if (this == OpCodes.Ldc_I4_0) return 0;
                if (this == OpCodes.Ldc_I4_1) return 1;
                if (this == OpCodes.Ldc_I4_2) return 2;
                if (this == OpCodes.Ldc_I4_3) return 3;
                if (this == OpCodes.Ldc_I4_4) return 4;
                if (this == OpCodes.Ldc_I4_5) return 5;
                if (this == OpCodes.Ldc_I4_6) return 6;
                if (this == OpCodes.Ldc_I4_7) return 7;
                if (this == OpCodes.Ldc_I4_8) return 8;
                if (this == OpCodes.Ldc_I4_M1) return -1;
                if (this == OpCodes.Ldc_I4) return this.OpParamAsInt32;
                if (this == OpCodes.Ldc_I4_S) return (sbyte)this.OpParamAsInt8;


                return null;
            }
        }

        public FieldInfo TargetField
        {
            get
            {


                if (this.OpParam.Length != 4)
                    return null;

                Type[] ma = (OwnerMethod.IsGenericMethod) ? OwnerMethod.GetGenericArguments() : null;



                try
                {
                    FieldInfo x = OwnerMethod.Module.ResolveField(OpParamAsInt32, OwnerMethod.DeclaringType.GetGenericArguments(), ma);

                    return x as FieldInfo;
                }
                catch { return null; }
            }
        }

        public override string ToString()
        {
            // int ip = 0;

            try
            {

                StringWriter w = new StringWriter();

                w.Write("[0x{0:x4}] {1} ", this.Offset, this.OpCode.Name.PadRight(10));

                if (this.OpCode == OpCodes.Ldstr)
                    w.Write("[" + this.TargetLiteral + "]");

                w.Write("+{0} -{1}", this.StackPushCount, this.StackPopCount);

                if (this.StackBefore != null)
                {
                    ILFlow.StackItem[] s = this.StackBefore.Clone().Pop(this.StackPopCount);

                    for (int i = 0; i < s.Length; i++)
                        w.Write("{0} ", s[i]);
                }

                //if (OpCode.FlowControl == FlowControl.Branch)
                //{
                //    w.Write("[branch: {0}]", TargetInstruction);
                //}

                return w.ToString();
            }
            catch (Exception exc)
            {
                return "error: " + exc.Message + " : " + exc.StackTrace;
            }
        }

        public void ToConsole(int ident)
        {
            // stack analysis

            string pad = "".PadLeft(ident);

            Console.ForegroundColor = BranchSources.Count == 0 ? ConsoleColor.Cyan : ConsoleColor.Green;
            Console.Write(pad + "0x{5:x8}. [->{4}] [{3}] -{2} +{1} IL_{0:x4}:"
                , Offset
                , StackPushCount
                , StackPopCount
                , StackAfter == null ? -1 : StackAfter.Count
                , BranchSources.Count
                , Flow == null ? 0 : Flow.GetHashCode());

            Console.ForegroundColor =
                (StackAfter == null ? -1 : StackAfter.Count) == 0
                ? ConsoleColor.Blue
                :
                (
                    OpCode.FlowControl == FlowControl.Next
                    ? ConsoleColor.Gray
                    :
                    (
                        (OpCode.FlowControl == FlowControl.Branch
                        || OpCode.FlowControl == FlowControl.Cond_Branch)
                        ? ConsoleColor.Red
                        : ConsoleColor.Yellow
                    )
                );
            Console.Write(" {0} [{1}] ", OpCode.Name.PadRight(12),
                InlineIfElseConstruct != null
                ? InlineIfElseConstruct.ToString(this)
                :
                    InlineLoopConstruct != null
                    ? InlineLoopConstruct.ToString(this)
                    : "_");

            if (OpCode.FlowControl == FlowControl.Branch
             || OpCode.FlowControl == FlowControl.Cond_Branch)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                if (OpCode.OperandType == OperandType.InlineSwitch)
                {
                    Console.Write("(");

                    for (int xi = 0; xi < BranchTargets.Length; xi++)
                    {
                        if (xi > 0)
                            Console.Write(", ");

                        Console.Write("IL_{0:x4}", BranchTargets[xi].Offset);
                    }

                    Console.Write(")");
                }
                else
                {

                    Console.Write(" IL_{0:x4}", BranchTargets[0].Offset);
                }



            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Green;



                if (this.TargetInteger != null)
                {
                    Console.Write("int32 {0}", this.TargetInteger);
                }
                else if (OpCode.OperandType == OperandType.InlineMethod)
                {
                    MethodBase x = TargetMethod;

                    if (x == null) x = TargetConstructor;

                    Console.Write(x.DeclaringType.FullName + "::" + x.Name);
                    Console.Write("(");

                    ParameterInfo[] pi = x.GetParameters();

                    for (int xxi = 0; xxi < pi.Length; xxi++)
                    {
                        if (xxi > 0)
                            Console.Write(", ");

                        Console.Write(pi[xxi].ParameterType.FullName);
                    }

                    Console.Write(")");


                }
                else if (OpCode.OperandType == OperandType.InlineString)
                {
                    Console.Write("'{0}'", TargetLiteral);
                }
                else if (OpCode.OperandType == OperandType.InlineType)
                {
                    Console.Write("{0}", TargetType.FullName);
                }
                else if (OpCode.OperandType == OperandType.InlineField)
                {
                    Console.Write("{1}::{0}", TargetField.Name, TargetField.DeclaringType.FullName);
                }
                else
                {
                    if (OpParamSize == 4)
                        Console.Write("{0:x8}", OpParamAsInt32);
                }
            }




            Console.WriteLine();



            if (IsLastInFlow || (StackAfter != null && StackAfter.Count == 0))
            {
                Console.ForegroundColor = IsLastInFlow ? ConsoleColor.Red : ConsoleColor.Yellow;


                Console.Write("".PadLeft(ident - 2) + "| ");

                Console.WriteLine();

            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public int OpParamSize
        {
            get
            {

                //InlinePhi 	This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.
                //InlineTok 	The operand is a FieldRef, MethodRef, or TypeRef token.

                if (OpCode.OperandType == OperandType.InlineTok)
                {

                    return 4; // 	No operand.

                }

                if (OpCode.OperandType == OperandType.InlineNone) return 0; // 	No operand.

                if (OpCode.OperandType == OperandType.ShortInlineBrTarget) return 1; // The operand is an 8-bit integer branch target.
                if (OpCode.OperandType == OperandType.ShortInlineVar) return 1; // The operand is an 8-bit integer containing the ordinal of a local variable or an argumenta .
                if (OpCode.OperandType == OperandType.ShortInlineI) return 1; // The operand is a 8-bit integer.

                if (OpCode.OperandType == OperandType.InlineVar) return 2; // The operand is 16-bit integer containing the ordinal of a local variable or an argument.


                if (OpCode.OperandType == OperandType.InlineBrTarget) return 4; // The operand is a 32-bit integer branch target.
                if (OpCode.OperandType == OperandType.InlineField) return 4; // The operand is a 32-bit metadata token.
                if (OpCode.OperandType == OperandType.InlineI) return 4; // The operand is a 32-bit integer.
                if (OpCode.OperandType == OperandType.InlineMethod) return 4; // The operand is a 32-bit metadata token.
                if (OpCode.OperandType == OperandType.ShortInlineR) return 4; // The operand is a 32-bit IEEE floating point number.
                if (OpCode.OperandType == OperandType.InlineSig) return 4; // The operand is a 32-bit metadata signature token.
                if (OpCode.OperandType == OperandType.InlineString) return 4; // The operand is a 32-bit metadata string token.
                if (OpCode.OperandType == OperandType.InlineSwitch) return 4; // The operand is the 32-bit integer argument to a switch instruction.
                if (OpCode.OperandType == OperandType.InlineType) return 4; // The operand is a 32-bit metadata token.

                if (OpCode.OperandType == OperandType.InlineR) return 8; // The operand is a 64-bit IEEE floating point number.
                if (OpCode.OperandType == OperandType.InlineI8) return 8; // The operand is a 64-bit integer.


                Task.Error("OpCode.OperandType not supported: " + Enum.GetName(typeof(OperandType), OpCode.OperandType) + " at " + OwnerMethod.DeclaringType.FullName + "." + OwnerMethod.Name);
                Task.Fail(this);

                return default(int);
            }
        }

        public OpCode OpCodeLookup(ref byte[] x)
        {
            int id = x[Offset] == 0xFE ? (x[Offset] << 8) + x[Offset + 1] : x[Offset];

            FieldInfo[] f = typeof(OpCodes).GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (FieldInfo i in f)
            {
                OpCode c = (OpCode)i.GetValue(null);

                if ((c.Value & 0x0000FFFF) == id)
                    return c;
            }

            throw new Exception();
        }


        public ScriptAttribute MemberScriptAttribute
        {
            get
            {
                if (TargetMethod != null)
                    return GetScriptAttribute(TargetMethod);

                if (TargetField != null)
                    return GetScriptAttribute(TargetField);

                if (TargetConstructor != null)
                    return GetScriptAttribute(TargetConstructor);

                Debugger.Break();

                return null;
            }
        }

        public ScriptAttribute DeclaringTypeScriptAttribute
        {
            get
            {
                if (TargetMethod != null)
                    return GetScriptAttribute(TargetMethod.DeclaringType);

                try
                {
                    if (TargetField != null)
                        return GetScriptAttribute(TargetField.DeclaringType);
                }
                catch
                {

                }

                if (TargetConstructor != null)
                    return GetScriptAttribute(TargetConstructor.DeclaringType);

                Debugger.Break();

                return null;
            }
        }

        ScriptAttribute GetScriptAttribute(MemberInfo t)
        {
            object[] o = t.GetCustomAttributes(typeof(ScriptAttribute), true);

            if (o.Length > 0)
            {
                if (o.Length == 1)
                    return o[0] as ScriptAttribute;
                else
                    Debugger.Break();
            }

            return null;
        }


        public static ILInstruction[] GetILAsInstructionArray(MethodBase b)
        {
            MethodBody mbody = b.GetMethodBody();

            if (mbody == null)
            {
                return null;
            }

            byte[] x = mbody.GetILAsByteArray();

            int offset = 0;

            ILInstruction i = null;

            int count = 0;

            while (offset < x.Length)
            {
                i = new ILInstruction(b, ref x, offset, i, count++);

                offset = offset + i.Size;
            }

            ILInstruction[] n = new ILInstruction[count];

            while (i != null)
            {
                n[--count] = i;

                if (i.Prev != null)
                    i.Prev.Next = i;

                i = i.Prev;
            }

            return n;
        }

        internal bool IsEqualVariable(LocalVariableInfo v)
        {
            if (v == null)
                return false;

            LocalVariableInfo t = TargetVariable;

            return t != null
                && t.LocalIndex == v.LocalIndex
                && t.LocalType == v.LocalType;


        }

        public bool IsBaseConstructorCall()
        {
            if (TargetConstructor == null)
                return false;

            Type Self = OwnerMethod.DeclaringType;
            Type Base = TargetConstructor.DeclaringType;

            return Self.BaseType == Base;
        }

        public bool IsBaseConstructorCall(MethodBase m)
        {
            if (TargetConstructor == null)
                return false;

            return m.DeclaringType == this.TargetConstructor.DeclaringType;
        }

        public bool IsOpCodeOf(params OpCode[] e)
        {
            return Helper.InArray(this.OpCode, e);
        }

        public bool IsLoadInstruction
        {
            get
            {
                return IsLoadLocal ||
                    IsOpCodeOf(

                    OpCodes.Ldfld,
                    OpCodes.Ldarg,
                    OpCodes.Ldarg_S,
                    OpCodes.Ldelem,
                    OpCodes.Ldelem_I,
                    OpCodes.Ldelem_I1,
                    OpCodes.Ldelem_I2,
                    OpCodes.Ldelem_I4,
                    OpCodes.Ldelem_I8,
                    OpCodes.Ldelem_R4,
                    OpCodes.Ldelem_R8,
                    OpCodes.Ldelem_Ref,
                    OpCodes.Ldsfld,
                    OpCodes.Ldobj);
            }
        }

        public bool IsStoreInstruction
        {
            get
            {
                return IsStoreLocal ||
                    IsOpCodeOf(

                    OpCodes.Stfld,
                    OpCodes.Starg,
                    OpCodes.Starg_S,
                    OpCodes.Stelem,
                    OpCodes.Stelem_I,
                    OpCodes.Stelem_I1,
                    OpCodes.Stelem_I2,
                    OpCodes.Stelem_I4,
                    OpCodes.Stelem_I8,
                    OpCodes.Stelem_R4,
                    OpCodes.Stelem_R8,
                    OpCodes.Stelem_Ref,
                    OpCodes.Stsfld,
                    OpCodes.Stobj);
            }
        }

        public bool IsDebugCode
        {
            get
            {
                ScriptAttribute a = ScriptAttribute.Of(OwnerMethod);

                return a != null && a.IsDebugCode;
            }
        }

        public bool IsEqualStoreLocation(ILInstruction u)
        {
            if (this.TargetVariable != null && u.TargetVariable != null)
            {
                return this.TargetVariable.LocalIndex == u.TargetVariable.LocalIndex;
            }

            // xxx fields


            // params

            // element

            return false;
        }

        public string Location
        {
            get
            {
                return "type: " + OwnerMethod.DeclaringType.FullName + " offset: " + string.Format("0x{0:x4}", this.Offset) + "  method:" + OwnerMethod.ToString();
            }
        }

        public bool IsLiteral
        {
            get
            {
                return false;
            }
        }
    }

}