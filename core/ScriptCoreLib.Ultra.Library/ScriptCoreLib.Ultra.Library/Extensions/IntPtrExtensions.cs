using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class IntPtrExtensions
    {
        public static bool IsZero(this IntPtr e)
        {
            return IntPtr.Zero == e;
        }
    }
}
