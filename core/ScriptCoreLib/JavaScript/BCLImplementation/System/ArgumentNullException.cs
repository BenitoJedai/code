using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(InternalConstructor = true, Implements = typeof(global::System.ArgumentNullException))]
    internal class __ArgumentNullException : __Exception
    {
        public __ArgumentNullException(string message) { }

        static __ArgumentNullException InternalConstructor(string e)
        {
            return (__ArgumentNullException)__Exception.InternalConstructor("ArgumentNullException: " + e);
        }
    
    }
}
