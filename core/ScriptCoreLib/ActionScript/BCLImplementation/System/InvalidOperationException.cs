using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
		Implements = typeof(global::System.InvalidOperationException)
        )
    ]
    internal class __InvalidOperationException : __Exception
    {
        public __InvalidOperationException() : this("")
        {
        }

		public __InvalidOperationException(string message)
            : base(message)
        {
			Name = "InvalidOperationException";
        }

       
    }
}
