using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.NullReferenceException)
        )
    ]
    internal class __NullReferenceException : __Exception
    {
        public __NullReferenceException() : this("")
        {
        }

        public __NullReferenceException(string message)
            : base(message)
        {
            Name = "NullReferenceException";
        }

       
    }
}
