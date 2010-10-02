using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ScriptCoreLib.Extensions
{
    public static class ExceptionExtensions
    {
        [DebuggerNonUserCode]
        public static T TryDebuggerBreak<T>(this T e) where T : Exception
        {
            if (Debugger.IsAttached)
                Debugger.Break();

            return e;
        }
    }
}
