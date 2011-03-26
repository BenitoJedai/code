using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(InternalConstructor = true, Implements = typeof(global::System.NullReferenceException))]
    internal class __NullReferenceException : __Exception
    {
        public __NullReferenceException(string message) { }

        static __NullReferenceException InternalConstructor(string e)
        {
            return (__NullReferenceException)__Exception.InternalConstructor("NullReferenceException: " + e);
        }
    
    }
}
