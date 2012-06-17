using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestInlineIfElseConstruct
{
    public class Class1
    {
        
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
                            // in F# the compiler will add FailInit conditional check to every public method
                            // it is somewhat different from the if else construct C# compiler is emitting

                            var __true = this.BranchTargets[0];
                            var __false = this.Next;

                            if (__true.OpCode.FlowControl == FlowControl.Branch)
                                if (__false.OpCode.FlowControl == FlowControl.Branch)
                                {
                                    //Debugger.Break();

                                    var __false__ = __false.BranchTargets[0];
                                    var __false_branch = __false__.Flow.Branch;

                                    var __true__ = __true.BranchTargets[0];
                                    var __true_branch = __true__.Flow.Branch;

                                    // we are assuming true clause is empty.
                                    if (__true__.Flow.Entry == __true_branch)
                                        if (__true_branch.OpCode == OpCodes.Nop)
                                        {
                                            var __true_join = __true_branch.Next;

                                            if (__false_branch.OpCode.FlowControl == FlowControl.Branch)
                                            {
                                                var __false_join = __false_branch.BranchTargets[0];

                                                if (__true_join == __false_join)
                                                {
                                                    z.Branch = this;
                                                    z.Join = __true_join;

                                                    z.BodyFalseFirst = __false__;
                                                    z.BodyFalseLast = __false_branch;

                                                    return _cached_iif.Value = z;
                                                }
                                            }
                                        }
                                }


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
}
