using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace ScriptCoreLib.Extensions
{
    public static class OpCodeExtensions
    {
        
        public static bool IsOpCodeLeave(this OpCode e)
        {
            // you gotta love the distinction of short forms.

            return e == OpCodes.Leave
                || e == OpCodes.Leave_S;
        }
    }
}
