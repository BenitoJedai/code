using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(InternalConstructor = true, Implements = typeof(global::System.NotSupportedException))]
    internal class __NotSupportedException : __Exception
    {
        public __NotSupportedException() { }
        public __NotSupportedException(string message) { }

        static __NotSupportedException InternalConstructor()
        {
            return (__NotSupportedException)__Exception.InternalConstructor("NotSupportedException");
        }

        static __NotSupportedException InternalConstructor(string e)
        {
            return (__NotSupportedException)__Exception.InternalConstructor("NotSupportedException: " + e);
        }

    }
}
