using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.DateTime)
        )
    ]
    internal class __DateTime
    {
        public static __DateTime Now
        {
            get
            {
                return new __DateTime {  };
            }
        }

        public override string ToString()
        {
            return "[DateTime]";
        }
    }
}
