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

    [DebuggerDisplay("{Offset} {Value.Name}")]
    public sealed class Instruction
    {
        public InstructionAnalysis Analysis;

        public OpCode Value;

        public uint Operand;
        public uint[] OperandArguments;

        public int Offset;
        public int NextOffset;

        public MethodBase TargetMethod;
        public FieldInfo TargetField;
        public string TargetString;
        public int? TargetInteger;

        public Instruction[] BranchIn;
        public Instruction[] BranchOut;

        /// <summary>
        /// jumps over FlowControl.Branch instructions
        /// </summary>
        public Instruction[] DirectBranchOut;



        public int? TargetVariableIndex
        {
            get { return TargetVariableStoreIndex ?? TargetVariableLoadIndex; }
        }

        public int? TargetParameterIndex
        {
            get { return TargetParameterStoreIndex ?? TargetParameterLoadIndex; }
        }

        public int? TargetVariableStoreIndex;
        public int? TargetVariableLoadIndex;

        public int? TargetParameterStoreIndex;
        public int? TargetParameterLoadIndex;

        public int? StackSizeIn;
        public int? StackSizeOut;





        public override string ToString()
        {
            return "0x" + Convert.ToString(this.Offset, 16).PadLeft(4, '0');
        }

        public static int GetStackChange(StackBehaviour e)
        {
            switch (e)
            {

                case StackBehaviour.Pop0: return 0;
                case StackBehaviour.Pop1: return -1;
                case StackBehaviour.Pop1_pop1: return -2;
                case StackBehaviour.Popi: return -1;
                case StackBehaviour.Popi_pop1: return -2;
                case StackBehaviour.Popi_popi: return -2;
                case StackBehaviour.Popi_popi8: return -2;
                case StackBehaviour.Popi_popi_popi: return -3;
                case StackBehaviour.Popi_popr4: return -2;
                case StackBehaviour.Popi_popr8: return -2;
                case StackBehaviour.Popref: return -1;
                case StackBehaviour.Popref_pop1: return -2;
                case StackBehaviour.Popref_popi: return -2;
                case StackBehaviour.Popref_popi_popi: return -3;
                case StackBehaviour.Popref_popi_popi8: return -3;
                case StackBehaviour.Popref_popi_popr4: return -3;
                case StackBehaviour.Popref_popi_popr8: return -3;
                case StackBehaviour.Popref_popi_popref: return -3;
                case StackBehaviour.Popref_popi_pop1: return -3;


                case StackBehaviour.Push0: return 0;
                case StackBehaviour.Push1: return 1;
                case StackBehaviour.Push1_push1: return 2;
                case StackBehaviour.Pushi: return 1;
                case StackBehaviour.Pushi8: return 1;
                case StackBehaviour.Pushr4: return 1;
                case StackBehaviour.Pushr8: return 1;
                case StackBehaviour.Pushref: return 1;



            }

            throw new NotSupportedException();

        }

        public int? StackPopCount;
        




        public int? StackPushCount;





    }




}
