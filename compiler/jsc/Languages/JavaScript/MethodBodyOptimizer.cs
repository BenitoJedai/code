using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace jsc.Languages.JavaScript
{
    class MethodBodyOptimizer
    {
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
                    var p_load = p.Instruction.StackBeforeStrict.Single().SingleStackInstruction;

                    if (p_load.IsLoadLocal)
                    {
                        var p_store = p.Prev.Instruction;

                        if (p_store.IsStoreLocal)
                            if (p_store.TargetVariable.LocalIndex == p_load.TargetVariable.LocalIndex)
                                if (p.Prev.Prev == null)
                                {
                                    w.WriteIdent();
                                    w.Write("return ");

                                    w.Override_WriteSelf = () => { w.Write("this"); return true; };

                                    IL2ScriptGenerator.OpCodeHandler(w, p, p_store.StackBeforeStrict.Single().SingleStackInstruction, null);

                                    w.Override_WriteSelf = null;

                                    w.WriteLine(";");
                                    return true;

                                }
                    }
                }

            return false;
        }
    }
}
