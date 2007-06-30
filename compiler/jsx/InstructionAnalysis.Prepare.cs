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


    public partial class InstructionAnalysis
    {
        private void PrepareVariableUsage(
            out VariableUsage.LocalVariableList Locals,
            out VariableUsage.ParameterList Parameters)
        {
            using (~MyPerformance)
            {
                Locals = new VariableUsage.LocalVariableList(Body.LocalVariables, this);
                Parameters = new VariableUsage.ParameterList(Value.GetParameters(), this);
            }
        }



        public void PrepareExceptionHandlingClause(
            out ExceptionHandlingClause[][] b1,
            out ExceptionHandlingClause[][] b2)
        {
            using (~MyPerformance)
            {

                var o1 = new List<ExceptionHandlingClause>[this.InstructionsByOffset.Length];
                var o2 = new List<ExceptionHandlingClause>[this.InstructionsByOffset.Length];



                foreach (var v in this.Body.ExceptionHandlingClauses)
                {
                    var x = v.TryOffset;

                    if (o1[x] == null)
                        o1[x] = new List<ExceptionHandlingClause>();

                    o1[x].Add(v);

                    for (int i = 0; i < v.TryLength; i++)
                    {
                        var z = x + i;

                        if (InstructionsByOffset[z] != null)
                        {


                            if (o2[z] == null)
                                o2[z] = new List<ExceptionHandlingClause>();

                            o2[z].Add(v);
                        }
                    }
                }

                b1 = new ExceptionHandlingClause[o1.Length][];
                b2 = new ExceptionHandlingClause[o2.Length][];

                for (int i = 0; i < o1.Length; i++)
                {
                    if (o1[i] != null) b1[i] = o1[i].ToArray();
                    if (o2[i] != null) b2[i] = o2[i].ToArray();
                }

            }
        }


        private void PrepareInstructions(
                out byte[] bytes,
                out MethodBody body,
                out Instruction[] i
            )
        {
            using (~MyPerformance)
            {
                body = Value.GetMethodBody();

                if (body == null)
                    throw new NotSupportedException();

                bytes = body.GetILAsByteArray();

                i = ToInstructions(new Stack<byte>(bytes.Reverse()), this);

                InstructionsByOffset = new Instruction[this.ByteArray.Length];

                foreach (var v in i)
                {
                    InstructionsByOffset[v.Offset] = v;
                }

            }
        }


        static int? GetInlineInteger(Instruction i)
        {
            if (i.Value == OpCodes.Ldc_I4_0) return 0;
            if (i.Value == OpCodes.Ldc_I4_1) return 1;
            if (i.Value == OpCodes.Ldc_I4_2) return 2;
            if (i.Value == OpCodes.Ldc_I4_3) return 3;
            if (i.Value == OpCodes.Ldc_I4_4) return 4;
            if (i.Value == OpCodes.Ldc_I4_5) return 5;
            if (i.Value == OpCodes.Ldc_I4_6) return 6;
            if (i.Value == OpCodes.Ldc_I4_7) return 7;
            if (i.Value == OpCodes.Ldc_I4_8) return 8;
            if (i.Value == OpCodes.Ldc_I4_M1) return -1;
            if (i.Value == OpCodes.Ldc_I4) return (int)i.Operand;
            if (i.Value == OpCodes.Ldc_I4_S) return (sbyte)i.Operand;

            return null;
        }

        private void PrepareMetatokens(
            out MethodBase[] rdm,
            out MethodBase[] rcm,
            out FieldInfo[] rf
            )
        {
            using (~MyPerformance)
            {
                Func<Instruction, ReflectionCache.MetadataKeyArguments> ToKey = i =>
                    new ReflectionCache.MetadataKeyArguments {
                                    Module = this.Value.Module,
                                    Target = (int)i.Operand,
                                    ReferencedMethod = this.Value,
                                    Instruction = i
                                };

                foreach (Instruction i in Instructions)
                    switch (i.Value.OperandType)
                    {
                        case OperandType.InlineMethod: i.TargetMethod = Cache.Methods[ToKey(i)]; continue;
                        case OperandType.InlineField: i.TargetField = Cache.Fields[ToKey(i)]; continue;
                        case OperandType.InlineString: i.TargetString = Cache.Strings[ToKey(i)]; continue;
                        default: i.TargetInteger = GetInlineInteger(i); continue;

                    }

                rdm = (from i in Instructions
                        where i.Value.EqualsAny(OpCodes.Ldftn, OpCodes.Ldvirtftn)
                        select i.TargetMethod
                      ).Distinct().ToArray();


                rcm = (from i in Instructions
                        where i.Value.FlowControl == FlowControl.Call
                        select i.TargetMethod
                      ).Distinct().ToArray();

                rf = (from i in Instructions
                        where i.TargetField != null
                        select i.TargetField
                      ).Distinct().ToArray();
            }
        }





        private void PrepareBranch(

            out Instruction[][] BranchInByOffset,
            out Instruction[][] BranchOutByOffset
                )
        {
            using (~MyPerformance)
            {
                var branchIn = new List<Instruction>[ByteArray.Length];

                foreach (var v in Instructions)
                {
                    branchIn[v.Offset] = new List<Instruction>(4);
                }


                #region BranchOut

                foreach (Instruction v in Instructions)
                {
                    if (v.Value.FlowControl == FlowControl.Next ||
                        v.Value.FlowControl == FlowControl.Meta ||
                        v.Value.FlowControl == FlowControl.Call)
                    {
                        v.BranchOut = Instructions.Where(i => i.Offset == v.NextOffset).ToArray();
                    }
                    else if (v.Value.FlowControl == FlowControl.Branch)
                    {
                        if (v.Value == OpCodes.Br_S)
                        {
                            v.BranchOut = Instructions.Where(i => i.Offset == v.NextOffset + (sbyte)v.Operand).ToArray();
                        }
                        else if (v.Value == OpCodes.Leave_S)
                        {
                            v.BranchOut = Instructions.Where(i => i.Offset == v.NextOffset + (sbyte)v.Operand).ToArray();
                        }
                        else if (v.Value == OpCodes.Br)
                        {
                            v.BranchOut = Instructions.Where(i => i.Offset == v.NextOffset + (int)v.Operand).ToArray();
                        }
                        else if (v.Value == OpCodes.Leave)
                        {
                            v.BranchOut = Instructions.Where(i => i.Offset == v.NextOffset + (int)v.Operand).ToArray();
                        }
                        else
                            throw new NotSupportedException(v.Value.Name);
                    }
                    else if (v.Value.FlowControl == FlowControl.Cond_Branch)
                    {
                        if (v.Value == OpCodes.Switch)
                        {
                            var _default = v.NextOffset;

                            v.BranchOut = new Instruction[ v.Operand + 1 ];
                            v.BranchOut[0] = InstructionsByOffset[_default];

                            for (int i = 0; i < v.Operand; i++)
                            {
                                v.BranchOut[1 + i] = InstructionsByOffset[_default + (int)v.OperandArguments[i]];
                            }

                        }
                        else if (v.Value.EqualsAny(
                                OpCodes.Brfalse,
                                OpCodes.Brtrue,
                                OpCodes.Beq,
                                OpCodes.Blt,
                                OpCodes.Blt_Un,
                                OpCodes.Bgt,
                                OpCodes.Bgt_Un,
                                OpCodes.Ble,
                                OpCodes.Ble_Un,
                                OpCodes.Bge,
                                OpCodes.Bge_Un,
                            //OpCodes.Bne,
                                OpCodes.Bne_Un
                            ))
                        {
                            v.BranchOut = Instructions.Where(
                                    i => i.Offset == v.NextOffset || i.Offset == v.NextOffset + (int)v.Operand
                                ).ToArray();
                        }
                        else if (v.Value.EqualsAny(
                                OpCodes.Brfalse_S,
                                OpCodes.Brtrue_S,
                                OpCodes.Beq_S,
                                OpCodes.Blt_S,
                                OpCodes.Blt_Un_S,
                                OpCodes.Bgt_S,
                                OpCodes.Bgt_Un_S,
                                OpCodes.Ble_S,
                                OpCodes.Ble_Un_S,
                                OpCodes.Bge_S,
                                OpCodes.Bge_Un_S,
                            //OpCodes.Bne_S,
                                OpCodes.Bne_Un_S
                            ))
                        {
                            v.BranchOut = Instructions.Where(
                                    i => i.Offset == v.NextOffset || i.Offset == v.NextOffset + (sbyte)v.Operand
                                ).ToArray();
                        }
                        else
                            throw new NotSupportedException(v.Value.Name);
                    }

                    if (v.BranchOut != null)
                    {
                        foreach (var vx in v.BranchOut)
                        {
                            branchIn[vx.Offset].Add(v);
                        }
                    }


                }
                #endregion

                #region get blocks
                BranchInByOffset = new Instruction[InstructionsByOffset.Length][];
                BranchOutByOffset = new Instruction[InstructionsByOffset.Length][];

                foreach (Instruction v in Instructions)
                {
                    v.BranchIn = branchIn[v.Offset].ToArray();

                    if (v.BranchOut != null)
                    v.DirectBranchOut = v.BranchOut.Select<Instruction, Instruction>(
                        delegate(Instruction i)
                        {
                            var x = i;

                            while (InstructionInfo.IsDirectBranch(x))
                            {
                                x = x.BranchOut.Single();

                                if (x == i)
                                    throw new StackOverflowException();
                            }

                            return x;
                        }
                    ).ToArray();

                    BranchOutByOffset[v.Offset] = GetFinalBranch(v, ( xz) => xz.BranchOut);
                    BranchInByOffset[v.Offset] = GetFinalBranch(v, ( xz) => branchIn[xz.Offset].ToArray());
                }



                #endregion


            }
        }

        private static Instruction[] GetFinalBranch(Instruction x, Func<Instruction, Instruction[]> h)
        {
            var s = h(x);

            if (s == null)
                return null;

            Instruction[] c = null;

            var a =  new Instruction[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                var p = s[i];

                while ((c = h(p)) != null && c.Length == 1)
                {
                    p = c[0];
                }

                a[i] = p;
            }

            return a;
        }






        private void GetVariableIndex(Instruction i, ref int? ldloc, ref int? stloc, ref int? ldarg, ref int? starg)
        {
            if (i.Value.OperandType == OperandType.InlineVar)
            {
                if (i.Value.EqualsAny(
                   OpCodes.Ldloc,
                   OpCodes.Ldloca
                   )) ldloc = (int)i.Operand;
                else if (i.Value.EqualsAny(
                   OpCodes.Ldarg,
                   OpCodes.Ldarga
                   )) ldarg = (int)i.Operand;
                else if (i.Value == OpCodes.Stloc) stloc = (int)i.Operand;
                else if (i.Value == OpCodes.Starg) starg = (int)i.Operand;
                else
                    throw new NotSupportedException();
            }
            else if (i.Value.OperandType == OperandType.ShortInlineVar)
            {
                if (i.Value.EqualsAny(
                   OpCodes.Ldloc_S,
                   OpCodes.Ldloca_S
                   )) ldloc = (int)i.Operand;
                else if (i.Value.EqualsAny(
                   OpCodes.Ldarg_S,
                   OpCodes.Ldarga_S
                   )) ldarg = (int)i.Operand;
                else if (i.Value == OpCodes.Stloc_S) stloc = (int)i.Operand;
                else if (i.Value == OpCodes.Starg_S) starg = (int)i.Operand;
                else
                    throw new NotSupportedException();
            }
            else if (i.Value.OperandType == OperandType.InlineNone)
            {
                #region InlineNone
                if (i.Value == OpCodes.Stloc_0) stloc = 0;
                else if (i.Value == OpCodes.Ldloc_0) ldloc = 0;
                else if (i.Value == OpCodes.Stloc_1) stloc = 1;
                else if (i.Value == OpCodes.Ldloc_1) ldloc = 1;
                else if (i.Value == OpCodes.Stloc_2) stloc = 2;
                else if (i.Value == OpCodes.Ldloc_2) ldloc = 2;
                else if (i.Value == OpCodes.Stloc_3) stloc = 3;
                else if (i.Value == OpCodes.Ldloc_3) ldloc = 3;
                else if (i.Value == OpCodes.Ldarg_0) ldarg = 0;
                else if (i.Value == OpCodes.Ldarg_1) ldarg = 1;
                else if (i.Value == OpCodes.Ldarg_2) ldarg = 2;
                else if (i.Value == OpCodes.Ldarg_3) ldarg = 3;

                #endregion

            }

            if (Value.IsStatic)
                return;

            if (ldarg != null) ldarg--;
            if (starg != null) starg--;
        }

        private void PrepareVariableIndex()
        {

            using (~MyPerformance)
            {
                foreach (Instruction i in Instructions)
                {
                    GetVariableIndex(
                        i,
                        ref i.TargetVariableLoadIndex,
                        ref i.TargetVariableStoreIndex,
                        ref i.TargetParameterLoadIndex,
                        ref i.TargetParameterStoreIndex
                    );

                    {
                        var ldarg = i.TargetParameterLoadIndex;

                        if (ldarg != null && ldarg >= 0)
                        {

                            var p = Value.GetParameters()[(int)ldarg];

                            if (p.ParameterType.IsByRef)
                            {
                                // check for Stind_I4 to be sure
                                i.TargetParameterLoadIndex = null;
                            }
                        }
                    }

                    if (i.Value.OperandType == OperandType.InlineNone)
                    {
                        #region InlineNone


                        if (i.Value == OpCodes.Stind_I4)
                        {
                            int? zero = null;
                            int? ldarg = null;

                            var u = StackUsage;

                            GetVariableIndex(StackUsage.ByClient[i.Offset].Skip(1).Single(),
                                ref zero,
                                ref zero,
                                ref ldarg,
                                ref zero);

                            i.TargetParameterStoreIndex = ldarg;
                        }
                        else if (i.Value == OpCodes.Ldind_I4)
                        {
                            int? zero = null;
                            int? ldarg = null;

                            GetVariableIndex(StackUsage.ByClient[i.Offset].Single(),
                                ref zero,
                                ref zero,
                                ref ldarg,
                                ref zero);

                            i.TargetParameterLoadIndex = ldarg;
                        }
                        else if (i.Value == OpCodes.Ldind_Ref)
                        {
                            int? zero = null;
                            int? ldarg = null;

                            GetVariableIndex(StackUsage.ByClient[i.Offset].Single(),
                                ref zero,
                                ref zero,
                                ref ldarg,
                                ref zero);

                            i.TargetParameterLoadIndex = ldarg;
                        }
                        #endregion

                    }



                }
            }
        }

        private void PrepareBranchUsage(out BranchUsage v)
        {
            using (~MyPerformance)
            {
                v = new BranchUsage(this);
            }
        }

        private void PrepareStackUsage(out StackUsage v)
        {
            using (~MyPerformance)
            {
                v = new StackUsage(this);
            }
        }



        static Instruction[] ToInstructions(Stack<byte> e, InstructionAnalysis Analysis)
        {
            var c = e.Count;

            var a = new List<Instruction>(e.Count);

            while (e.Count > 0)
            {
                var z = new Instruction { Analysis = Analysis };

                z.Offset = (int)(c - e.Count);

                short u = e.Pop();

                if (u == 0xfe)
                    u = (short)((u << 8) + e.Pop());


                z.Value = opcodes[u & 0xffff];
                z.Operand = e.Pop32(z.Value.GetOperandSize() ?? 0);


                if (z.Value.OperandType == OperandType.InlineSwitch)
                {
                    e.Pop32(z.OperandArguments = new uint[z.Operand]);
                }

                z.NextOffset = (int)(c - e.Count);

                a.Add(z);
            }

            return a.ToArray();
        }

        public void PrepareStackChange()
        {
            using (~MyPerformance)
            {
                foreach (var i in Instructions)
                {
                    i.StackPopCount = GetStackPopCount(i);
                    i.StackPushCount = GetStackPushCount(i);
                }
            }
        }

        int GetStackPushCount(Instruction i)
        {
            if (i.Value.StackBehaviourPush == StackBehaviour.Varpush)
            {
                // calling the base/this ctor
                if (i.Value.FlowControl == FlowControl.Call)
                {
                    if (i.TargetMethod is ConstructorInfo)
                    {
                        return 0;
                    }
                }

                #region return
                if (i.Value.FlowControl == FlowControl.Return)
                {
                    return 0;
                }
                #endregion

                #region parameter count + this reference

                var mi = i.TargetMethod as MethodInfo;

                if (mi != null)
                    if (mi.ReturnType != typeof(void))
                        return 1;
                var ci = i.TargetMethod as ConstructorInfo;

                if (ci != null)
                    return 1;

                return 0;
                #endregion

            }

            return Instruction.GetStackChange(i.Value.StackBehaviourPush);
        }

        public bool HasReturnParameter
        {
            get
            {
                if (Value is MethodInfo)
                {
                    if ((Value as MethodInfo).ReturnType != typeof(void))
                        return true;
                }

                return false;
            }
        }

        int GetStackPopCount(Instruction i)
        {
            if (i.Value.StackBehaviourPop == StackBehaviour.Varpop)
            {
                // calling the base/this ctor
                if (i.Value.FlowControl == FlowControl.Call)
                {
                    if (i.TargetMethod is ConstructorInfo)
                    {
                        if (i.Value == OpCodes.Newobj)
                            return -(i.TargetMethod.GetParameters().Length);

                        return -(i.TargetMethod.GetParameters().Length + 1);
                    }
                }

                #region return void | expression
                if (i.Value.FlowControl == FlowControl.Return)
                {
                    return HasReturnParameter ? -1 : 0;
                }
                #endregion

                #region parameter count + this reference
                var e = i.TargetMethod.GetParameters().Length;

                if (!i.TargetMethod.IsStatic)
                    if (i.TargetMethod is MethodInfo)
                        e++;

                return -e;
                #endregion

            }

            return Instruction.GetStackChange(i.Value.StackBehaviourPop);

        }
    }
}
