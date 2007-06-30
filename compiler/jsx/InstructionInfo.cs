using System;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace jsx
{
    using BranchOutWithCost = Tuple<Instruction, int>;

    public static class InstructionInfo
    {
        public class SwitchCase
        {
            public int Index;
            public Instruction Target;
            public bool IsReturnStatementArgument;
        }

        public static IEnumerable<SwitchCase> GetSwitchCases(Instruction i)
        {
            if (i.Value != OpCodes.Switch)
                throw new NotSupportedException();

            for (int x = 0;  x < i.DirectBranchOut.Length;  x++)
            {
                var Target = i.DirectBranchOut[x];

                yield return new SwitchCase {
                    Index = x - 1,
                    Target = Target,
                    IsReturnStatementArgument = IsReturnStatementArgument(Target)
                };
            }
        }

        public static bool IsReturnStatementArgument(Instruction i)
        {
            var x = i.Analysis;
            var brx = InstructionInfo.IsDirectBranch(i) ? i.DirectBranchOut.Single() : i;


            var iu = x.StackUsage.ByProvider[brx.Offset];

            if (iu.Length == 0)
                return false;

            Instruction ic = iu.Single().ClientList.Single();

            if (ic.Value == OpCodes.Ret)
                return true;

            if (ic.Value == OpCodes.Throw)
                return true;

            if (ic.TargetVariableStoreIndex != null)
            {
                var iloadinfo = x.Locals.Values[(int)ic.TargetVariableStoreIndex].ByOffset[ic.Offset];
                var iload = iloadinfo.Info.Clients.Single();
                var iloadu = x.StackUsage.ByProvider[iload.TargetInstruction.Offset];

                Instruction iloadc = iloadu.Single().ClientList.Single();

                if (iloadc.Value == OpCodes.Ret)
                {
                    return true;
                }
            }

            return false;
        }

        public static Instruction FindJoinPoint(Instruction a, Instruction b)
        {
            var an = a.Analysis.BranchOutByOffset[a.Offset];
            var ab = b.Analysis.BranchOutByOffset[b.Offset];

            if (an.Length == 1 && ab.Length == 1)
            {
                if (an[0] == ab[0])
                {
                    var z = a.Analysis.BranchInByOffset[an[0].Offset];

                    if (z.Length == 1)
                        return z[0];
                }

            }



            return null;
        }

        public static IEnumerable<Instruction> GetBranchInInstruction(this IEnumerable<Instruction> i)
        {
            foreach (var v in i)
            {
                foreach (var q in v.Analysis.BranchInByOffset[v.Offset])
                {
                    yield return q;
                }
            }
        }

        public static IEnumerable<Instruction> FindJoinPoints(Instruction a, Instruction b)
        {
            var x = a.Analysis;
            var o = new OnlyOnce[x.InstructionsByOffset.Length];

            foreach (var v in FindBranchOutInstructions(a))
            {
                foreach (var z in FindBranchOutInstructions(b))
                {
                    if (v == z)
                    {

                        foreach (var q in a.Analysis.BranchInByOffset[v.Offset])
                        {
                            if (o[q.Offset]++) continue;

                            yield return q;
                            
                        }

                    }
                }
            }
        }

        public static IEnumerable<Instruction> FindBranchOutInstructions(Instruction e)
        {
            var x = e.Analysis;

            var o = new OnlyOnce[x.InstructionsByOffset.Length];
            var u = new Queue<Instruction>();

            u.Enqueue(e);

            while (u.Count > 0)
            {
                var z = u.Dequeue();
                var c = x.BranchOutByOffset[z.Offset];

                if (c != null)
                foreach (var v in c)
                {
                    if (o[v.Offset]++) continue;

                    yield return v;

                    u.Enqueue(v);
                }
            }
        }

        public static bool IsDirectBranch(Instruction x)
        {
            return x.Value.FlowControl == FlowControl.Branch || x.Value == OpCodes.Nop;
        }

        public static void GetExpression()
        {
        }
        
    }





}
