using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.NotImplementedException))]
    internal class __NotImplementedException : __Exception
    {
        public __NotImplementedException()
        {
            throw new Exception("multiconstructor for PHP broken. needs a fix.");
        }

        public __NotImplementedException(string message)
            : base(message)
        {
            throw new Exception("multiconstructor for PHP broken. needs a fix.");
        }


    }
}
