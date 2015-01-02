using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script]
    public sealed class __AssemblyNameValue
    {
        public string Name;
        public string FullName;
    }

    // .Shared?
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Reflection/AssemblyName.cs

    [Script(Implements = typeof(global::System.Reflection.AssemblyName))]
    public sealed class __AssemblyName
    {
        public __AssemblyValue __Value;
        public __AssemblyNameValue __NameValue;

        public string Name { get { return __NameValue.Name; } }
        public string FullName { get { return __NameValue.FullName; } }
    }
}
