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
        public __Exception() : this("")
        {

        }

        public __Exception(string message)
        {

        }

        public string Message
        {
            [Script(ExternalTarget = "message")]
            get
            {
                return default(string);
            }
        }


        protected string Name { [Script(ExternalTarget = "name")] get; [Script(ExternalTarget = "name")]  set; }
    }
}
