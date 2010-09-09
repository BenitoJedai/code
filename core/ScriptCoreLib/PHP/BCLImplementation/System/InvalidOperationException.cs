using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.InvalidOperationException))]
    internal class __InvalidOperationException : __Exception
    {
        public __InvalidOperationException(string message) : base(message) { }
    }
}
