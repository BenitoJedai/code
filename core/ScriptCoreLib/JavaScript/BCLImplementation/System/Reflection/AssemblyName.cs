using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.AssemblyName))]
    internal sealed class __AssemblyName
    {
        public string Name { get; set; }
    }
}
