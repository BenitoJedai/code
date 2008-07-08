using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.ArgumentException)
        )
    ]
    internal class __ArgumentException : __Exception
    {
        public __ArgumentException() : this("")
        {
        }

        public __ArgumentException(string message)
            : base(message)
        {
            Name = "ArgumentException";
        }

       
    }
}
