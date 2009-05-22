using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibAppJet.JavaScript.BCLImplementation.System.Reflection
{
    [Script]
    internal sealed class __AssemblyNameValue
    {
        public string Name;
        public string FullName;
    }

    [Script(Implements = typeof(global::System.Reflection.AssemblyName))]
    internal sealed class __AssemblyName
    {
        public __AssemblyValue __Value;
        public __AssemblyNameValue __NameValue;

        public string Name { get { return __NameValue.Name; } }
        public string FullName { get { return __NameValue.FullName; } }
    }
}
