using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.IntPtr))]
    internal class __IntPtr
    {
        public string Token;

        public static explicit operator __IntPtr(string _Token)
        {
            return new __IntPtr { Token = _Token };
        }
    }
}
