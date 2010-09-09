using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(InternalConstructor = true, Implements = typeof(global::System.InvalidOperationException))]
    internal class __InvalidOperationException : __Exception
    {
        public __InvalidOperationException() { }
        public __InvalidOperationException(string message) { }

        static __InvalidOperationException InternalConstructor()
        {
            return (__InvalidOperationException)__Exception.InternalConstructor("InvalidOperationException");
        }

        static __InvalidOperationException InternalConstructor(string e)
        {
            return (__InvalidOperationException)__Exception.InternalConstructor("InvalidOperationException: " + e);
        }
    }
}
