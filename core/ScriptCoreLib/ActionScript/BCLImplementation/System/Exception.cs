using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.Exception),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.Error)
        )
    ]
    public class __Exception
    {
        public __Exception(string message)
        {

        }

        public string Message
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var e = (global::ScriptCoreLib.ActionScript.Error)(object)this;

                return e.message;
            }
        }
    }
}
