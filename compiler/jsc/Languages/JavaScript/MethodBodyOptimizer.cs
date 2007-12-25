using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using ScriptCoreLib;

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


        public static bool SemiInlineWrapperMethod(this IdentWriter w, MethodInfo zm, ScriptAttribute zsa)
        {
            if (!zm.IsStatic)
                return false;

            if (zsa != null && zsa.DefineAsInstance)
                return false;

            if (zsa != null && zsa.OptimizedCode != null)
                return false;

            ILBlock xb = new ILBlock(zm);

            if (xb.Prestatements.PrestatementCommands.Count == 3)
            {
                ILBlock.Prestatement _0 = xb.Prestatements.PrestatementCommands[0];
                ILBlock.Prestatement _1 = xb.Prestatements.PrestatementCommands[1];
                ILBlock.Prestatement _2 = xb.Prestatements.PrestatementCommands[2];

                if (_0.Instruction.OpCode != OpCodes.Nop)
                    goto skip;

                if (!_1.Instruction.IsStoreLocal)
                    goto skip;

                if (_2.Instruction.OpCode != OpCodes.Ret)
                    goto skip;

                // validate that the local variable is the same

                ILInstruction _3 = _2.Instruction.StackBeforeStrict[0].SingleStackInstruction;

                if (!_3.IsLoadLocal)
                    goto skip;

                if (_3.TargetVariable.LocalIndex != _1.Instruction.TargetVariable.LocalIndex)
                    goto skip;

                // now we know that this function returns something

                ILInstruction _4 = _1.Instruction.StackBeforeStrict[0].SingleStackInstruction;

                if (_4.OpCode.FlowControl != FlowControl.Call)
                    goto skip;

                MethodBase _4_Method = _4.TargetMethod;

                var _4_MethodImplementation = w.Session.ResolveImplementation(_4_Method.DeclaringType, _4_Method);

                if (_4_MethodImplementation != null)
                    _4_Method = _4_MethodImplementation;

                if (_4_Method == null)
                    goto skip;

                if (!_4_Method.IsStatic)
                    goto skip;



                // now if it has the same argument types and if we pass the same argument values
                // we can semi-inline it.


                ParameterInfo[] tp = _4_Method.GetParameters();
                ParameterInfo[] mp = zm.GetParameters();

                if (tp.Length != mp.Length)
                    goto skip;

                for (int j = 0; j < tp.Length; j++)
                {
                    if (tp[j].ParameterType != mp[j].ParameterType)
                        goto skip;

                }

                for (int i = 0; i < tp.Length; i++)
                {
                    ParameterInfo v = _4.StackBeforeStrict[i].SingleStackInstruction.TargetParameter;

                    if (v == null)
                        goto skip;

                    if (v.Position != i)
                        goto skip;
                }

                w.WriteIdent();
                w.Write("var ");
                w.WriteDecoratedMethodName(zm);
                w.Write(" = ");


                if (_4_Method.DeclaringType.Assembly == zm.DeclaringType.Assembly)
                {
                    w.Write("function () { ");

                    if (zm.ReturnType != typeof(void))
                    {
                        w.Write("return ");
                    }

                    w.WriteDecoratedMethodName(_4_Method);
                    w.Write(".apply(null, arguments);");
                    w.Write(" }");
                }
                else
                {
                    w.WriteDecoratedMethodName(_4_Method);
                }

                w.WriteLine(";");


                return true;
            }

        skip:

            return false;


        }
    
    }
}
