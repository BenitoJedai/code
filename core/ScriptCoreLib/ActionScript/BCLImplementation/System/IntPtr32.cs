using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.IntPtr))]
    internal class __IntPtr
    {
        public string StringToken;
        public Function FunctionToken;

        public static explicit operator __IntPtr(string _Token)
        {
            return new __IntPtr { StringToken = _Token };
        }

        public static explicit operator __IntPtr(Function _Token)
        {
            return new __IntPtr { FunctionToken = _Token };
        }

        public static explicit operator string(__IntPtr _ptr)
        {
            return _ptr.StringToken;
        }

        public static explicit operator Function(__IntPtr _ptr)
        {
            return _ptr.FunctionToken;
        }
    }
}
