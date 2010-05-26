using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Reflection.Emit;




namespace jsc
{
    

    public class ILFlow
    {
        public delegate ILBlock ResolveFlowBlock(ILInstruction i);

        public override string ToString()
        {
            using (StringWriter w = new StringWriter())
            {
                w.Write("flow {2}:[0x{0:x4} to 0x{1:x4}]:{3}", this.Entry.Offset, this.Branch.Offset, this.Parents.Count, this.BranchFlow.Count);


                return w.ToString();
            }
        }

        public ILInstruction FindLastAssignment(ILInstruction i)
        {
            ILInstruction p = i;

            while (true)
            {
                p = p.Prev;

                if (p.TargetVariable != null)
                {
                    if (p.TargetVariable.LocalIndex == i.TargetVariable.LocalIndex && p != i.OpCode)
                        return p;
                }

                if (p == this.Entry)
                    break;
            }

            return null;
        }

        /// <summary>
        /// defines the point which can have 1 or several branch sources
        /// -or- null if it is method entry or handler entry
        /// </summary>
        public readonly ILInstruction Entry;
        /// <summary>
        /// defines the point of branch, where flow will be split
        /// </summary>
        public readonly ILInstruction Branch;
        /// <summary>
        /// a list of each flow which was split by branch instruction
        /// </summary>
        public readonly List<ILFlow> BranchFlow;
        /// <summary>
        /// defines calling block, may it be method entry or handler entry
        /// </summary>
        public readonly ILBlock OwnerBlock;

        /// <summary>
        /// represents item in stack
        /// </summary>
        public class StackItem
        {
            /// <summary>
            /// originating instruction
            /// </summary>
            public ILInstruction Entry;
            /// <summary>
            /// index of push
            /// </summary>
            public int Index;
            /// <summary>
            /// unknown source, so info should be obtained from block given
            /// </summary>
            public ILBlock Block;

            public List<StackItem> Alternate = new List<StackItem>();

            public StackItem ClonedFrom;

            //public static StackItem Duplicate(StackItem e)
            //{
            //    return new StackItem(e.Entry, e.Index + 1);
            //}

            public StackItem(ILInstruction p, int index)
            {
                Entry = p;
                Index = index;
                
            }

            public StackItem(ILBlock b)
            {
                Block = b;
            }

            public StackItem Clone()
            {
                StackItem s = new StackItem(Entry, Index);

                s.Block = Block;

                s.ClonedFrom = this;

                return s;
            }

            public ILBlock.InlineLogic InlineLogic(ILBlock.Prestatement p)
            {
                if (p == null)
                    throw new ArgumentNullException();

                ILBlock.InlineLogic x = new ILBlock.InlineLogic();

                x.Resolve(this, p.Instruction.InlineIfElseConstruct);

                return x;
            }



            public override string ToString()
            {
                if (this.Alternate.Count > 0)
                {
                    return "alternatives";
                }
                else
                    using (StringWriter w = new StringWriter())
                    {
                        w.Write("{");

                        for (int i = 0; i < StackInstructions.Length; i++)
                        {
                            if (i > 0)
                                w.Write(", ");

                            if (StackInstructions[i] == null)
                                w.Write("null");
                            else
                                w.Write(StackInstructions[i].ToString());
                        }

                            w.Write("}");

                        return w.ToString();
                    }

                
            }

            public bool IsSingle
            {
                get
                {
                    return StackInstructions.Length == 1;
                }
            }

            public ILInstruction SingleStackInstruction
            {
                get
                {
                    ILInstruction[] s = StackInstructions;

                    if (s.Length == 1)
                        return s[0];
                    else
                        throw new NotSupportedException("multiple stack entries instead of one");
                }
            }

            /// <summary>
            /// returns uset stack item and all the alternates aswell
            /// </summary>
            public ILInstruction[] StackInstructions
            {
                get
                {
                    ILInstruction[] n = new ILInstruction[Alternate.Count + 1];

                    n[0] = Entry;

                    for (int i = 0; i < Alternate.Count; i++)
                    {
                        if (Alternate[i].Alternate.Count > 0)
                            throw new NotImplementedException();

                        n[i + 1] = Alternate[i].Entry;
                    }

                    return n;
                }
            }
        }

        public class EvaluationStack : Stack<StackItem>
        {
            public StackItem[] Pop(int i)
            {
                StackItem[] n = new StackItem[i];

                while (i-- > 0)
                    n[i] = Pop();

                return n;
            }

            public EvaluationStack Clone(bool deep)
            {
                if (deep)
                {
                    ILFlow.StackItem[] z = base.ToArray();

                    int i = z.Length;

                    EvaluationStack n = new EvaluationStack();

                    while (i-- > 0)
                    {
                        n.Push(z[i].Clone());
                    }

                    return n;
                }
                else
                    return Clone();
            }

            public EvaluationStack Clone()
            {
                ILFlow.StackItem[] z = base.ToArray();

                int i = z.Length;

                EvaluationStack n = new EvaluationStack();

                while (i-- > 0)
                {
                    n.Push(z[i]);
                }

                return n;
            }
        }


        /// <summary>
        /// defines the stack state after all items are iterated
        /// </summary>
        public readonly EvaluationStack Stack;

        /// <summary>
        /// a list of flows which branch to this flow
        /// </summary>
        public readonly List<ILFlow> Parents;

        public ILFlow(ILFlow p, ILInstruction i)
        {
            using (new Task("ilflow", i.ToString()))
            {

                Parents = new List<ILFlow>();
                Parents.Add(p);

                _resolve_block = p._resolve_block;

                Entry = i;

                this.OwnerBlock = _resolve_block(Entry);

                if (this.OwnerBlock.First == Entry)
                {
                    if (this.OwnerBlock.Flow != null)
                        Debugger.Break();

                    this.OwnerBlock.Flow = this;
                }

                Task.WriteLine("owner: " + OwnerBlock.ToString());

                Stack = p.Stack.Clone(true);

               
                BranchFlow = new List<ILFlow>();
                Branch = NextInstructionBranch();

                FollowBranch();
            }
        }

        readonly ResolveFlowBlock _resolve_block;

        public ILFlow(ResolveFlowBlock r, ILInstruction i, EvaluationStack s)
        {
            using (new Task("ilflow with stack", i.ToString()))
            {

                if (i.Flow != null)
                    throw new Exception();

                _resolve_block = r;

                Entry = i;

                this.OwnerBlock = r(Entry);

                if (this.OwnerBlock.First == Entry)
                {
                    if (this.OwnerBlock.Flow != null)
                        Debugger.Break();

                    this.OwnerBlock.Flow = this;
                }

                Task.WriteLine("owner: " + OwnerBlock.ToString());

                Parents = new List<ILFlow>();

               
                Stack = s;
                BranchFlow = new List<ILFlow>();

                using (new Task("fetching branch instruction"))
                    Branch = NextInstructionBranch();

                using (new Task("follow", Branch.ToString()))
                    FollowBranch();
                
            }
        }


        void FollowBranch(ILInstruction z)
        {
			if (z == null)
			{
				throw new ArgumentNullException("!!! IL is missing a  OpCodes.ret ? :)");
			}

            if (z.IsFirstInFlow)
            {
                if (z.StackBefore.Count != Branch.StackAfter.Count)
                    throw new ArgumentException();

                StackItem[] alt = Branch.StackAfter.ToArray();
                StackItem[] cur = z.StackBefore.ToArray();

                // Console.WriteLine("join to 0x{0:x4} from 0x{1:x4} [{2}]: ", z.Offset, this.Branch.Offset, alt.Length);





                for (int i = 0; i < alt.Length; i++)
                {
                    if (cur[i].Entry.OpCode == OpCodes.Ldstr)
                    {
                        // Console.WriteLine("alt {0} : cur {1}", alt[i].Entry.ToString().PadRight(40), cur[i].Entry);
                    }

                    if (cur[i].Entry == alt[i].Entry)
                        continue;

                    cur[i].Alternate.Add(alt[i]);
                }

                // merge


                //using (new Task("join", z.ToString()))
                //{
                    z.Flow.Parents.Add(this);
                    BranchFlow.Add(z.Flow);
                //}
            }
            else
            {
                //using (new Task("branch", z.ToString()))
                //{
                    // Console.WriteLine("branch to {0:x4} : {1}", z.Offset, Branch.StackAfter.Count);
                    BranchFlow.Add(new ILFlow(this, z));
                //}
            }

            //Console.ForegroundColor = ConsoleColor.Gray;
        }

        void FollowBranch()
        {
            if (Branch == null)
                return;

            // Console.WriteLine("flow [{0:x4} - {1:x4}]", Entry.Offset, Branch.Offset);

            if (Branch.OpCode.FlowControl == FlowControl.Throw
                || Branch.OpCode.FlowControl == FlowControl.Return)
                return;

            if (Branch.OpCode.FlowControl == FlowControl.Branch)
            {
                FollowBranch(Branch.BranchTargets[0]);
                return;
            }

            if (Branch.OpCode.FlowControl == FlowControl.Cond_Branch)
            {
                FollowBranch(Branch.Next);

                foreach (ILInstruction x in Branch.BranchTargets)
                    FollowBranch(x);

                return;
            }

            if (Branch.OpCode.FlowControl == FlowControl.Next)
            {

                FollowBranch(Branch.Next);
                return;
            }

            if (Branch.OpCode.FlowControl == FlowControl.Call)
            {
                FollowBranch(Branch.Next);
                return;
            }

			if (Branch.OpCode.FlowControl == FlowControl.Meta)
			{
				FollowBranch(Branch.Next);
				return;
			}

			throw new NotSupportedException();
        }


        void StackFixup(ILInstruction p)
        {
            p.StackBefore = Stack.Clone();

            // verify
            if (Stack.Count < p.StackPopCount)
            {
                Task.Error("*** stack is empty, invalid pop?");
                Task.Fail(p);

                return;
            }

			//using (new Task("stack fixup", p.ToString()))
			//{

                



                // hide dup opcode at stack analysis level
                //if (p == OpCodes.Dup)
                //{
                //    Stack.Push(StackItem.Duplicate(Stack.Peek()));
                //}
                //else
                //{
                for (int i = 0; i < p.StackPopCount; i++)
                    Stack.Pop();


                for (int i = 0; i < p.StackPushCount; i++)
                    Stack.Push(new StackItem(p, i));
                //}

                p.StackAfter = Stack.Clone();
			//}
        }

        public ILInstruction NextInstructionBranch()
        {
            if (Entry.Flow != null)
                throw new NotSupportedException();

            ILInstruction p = Entry;

            while (true)
            {
                if (p.Flow != null)
                    throw new NotSupportedException();

                p.Flow = this;

                StackFixup(p);

                if ((p.Next != null && p.Next.IsTryBlockEntry))
                    break;

                if (!p.IsFlowBreak && p.NextInstruction != null)
                    p = p.NextInstruction;
                else
                    break;
            }

            return p;

        }
    }
}