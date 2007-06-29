using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace jsc.CodeModel
{
    public class VariableInfo
    {
        public MethodBase Method;

        public LocalVariableInfo LocalVariable;

        public ILInstruction[] RelatedInstructions;

        public void Attach(ILBlock iLBlock)
        {
            List<ILInstruction> i = new List<ILInstruction>();

            
            foreach (ILInstruction var in iLBlock.Instructrions)
            {
                if (var.TargetVariable != null)
                {
                    if (var.TargetVariable.LocalIndex == LocalVariable.LocalIndex)
                    {
                        var.TargetVariableInfo = this;

                        i.Add(var);
                    }
                }
            }

            RelatedInstructions = i.ToArray();
        }
    }
}
