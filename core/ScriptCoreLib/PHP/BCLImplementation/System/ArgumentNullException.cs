using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.ArgumentNullException))]
    internal class __ArgumentNullException : __Exception
    {
        public __ArgumentNullException(string message) : base(message) { }
    }
}
