using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.NotImplementedException)
        )
    ]
    public class __NotImplementedException : __Exception
    {
        /*
        public __NotImplementedException() : this("")
        {

        }
        */
        public __NotImplementedException(string message) : base(message)
        {
            Name = "NotImplementedException";
        }

       
    }
}
