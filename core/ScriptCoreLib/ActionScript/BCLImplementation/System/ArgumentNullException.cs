using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.ArgumentNullException)
        )
    ]
    public class __ArgumentNullException : __Exception
    {
        public __ArgumentNullException() : this("")
        {
        }

        public __ArgumentNullException(string message)
            : base(message)
        {
            Name = "ArgumentNullException";
        }

       
    }
}
