using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace jsc.Languages.JavaScript
{
    static class MethodBodyOptimizer
    {

        static Dictionary<OpCode, string> OptimizeAssignment_Operators;

        // http://msdn2.microsoft.com/en-us/library/6a71f45d(VS.80).aspx
        public static bool OptimizeAssignment(this IdentWriter w, ILBlock.Prestatement p, ILInstruction i)
        {
            // optimize: g = g + 1 to g += 1

            if (i.StackBeforeStrict.Length == 1)
            {
                var store_value = i.StackBeforeStrict[0];
                var dict = OptimizeAssignment_Operators ?? (OptimizeAssignment_Operators = new Dictionary<OpCode, string>
                {
                    {OpCodes.Add, "+="},
                    {OpCodes.Sub, "-="},
                    {OpCodes.Mul, "*="},
                    {OpCodes.Div, "/="},
                });

                if (store_value.StackInstructions.Length == 1)
                {
                    var store_op = store_value.StackInstructions[0];

                    if (store_op != null)
                        if (dict.ContainsKey(store_op.OpCode))
                        {
                            var source = store_op.StackBeforeStrict[0].SingleStackInstruction;

                            if (source.IsEqualVariable(i.TargetVariable))
                            {
                                w.Helper.WriteOptionalSpace();
                                w.Write(dict[store_op.OpCode]);
                                w.Helper.WriteOptionalSpace();
                                IL2ScriptGenerator.OpCodeHandler(w, p, i, store_op.StackBeforeStrict[1]);
                                return true;
                            }
                        }
                }
            }

            return false;
        }

        public static bool TryOptimize(IdentWriter w, ILBlock xb)
        {
            /* Try to optimize this:
              // <>f__AnonymousType0`3.get_b
              type$_1w3gkpRFAjefjmufrOWDFQ.get_b = function ()
              {
                var a = this, b;

                b = a._b_i__Field;
                return b;
              }; 
              
             To:
              // <>f__AnonymousType0`3.get_g
              type$_1w3gkpRFAjefjmufrOWDFQ.get_g = function ()
              {
                return this._g_i__Field;
              }
             
             File size for ScriptCoreLib.dll.js
             
             Before: 365 670 bytes
             * 365 670 bytes
             
             */

            var p = xb.Prestatements.LastPrestatement;

            if (p.Instruction == OpCodes.Ret)
                if (p.Instruction.StackBeforeStrict.Length == 1)
                {
                    var p_load_statement = p.Instruction.StackBeforeStrict.Single();
                    if (p_load_statement.StackInstructions.Length == 1)
                    {
                        var p_load = p_load_statement.SingleStackInstruction;

                        if (p_load.IsLoadLocal)
                        {
                            var p_store = p.Prev.Instruction;

                            if (p_store.IsStoreLocal)
                                if (p_store.TargetVariable.LocalIndex == p_load.TargetVariable.LocalIndex)
                                    if (p.Prev.Prev == null)
                                    {
                                        var p_value_statement = p_store.StackBeforeStrict.Single();
                                        if (p_value_statement.StackInstructions.Length == 1)
                                        {
                                            w.WriteIdent();
                                            w.Write("return ");

                                            w.Override_WriteSelf = () => { w.Write("this"); return true; };




                                            IL2ScriptGenerator.OpCodeHandler(w, p, p_value_statement.SingleStackInstruction, null);

                                            w.Override_WriteSelf = null;

                                            w.WriteLine(";");
                                            return true;
                                        }

                                    }
                        }
                    }
                }

            return false;
        }
    }
}
