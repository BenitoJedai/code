using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib
{
    /// <summary>
    /// Will generate an entrypoint to this class with the default value of field 'DefaultData'
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ScriptApplicationEntryPointAttribute : Attribute
    {
        public bool IsClickOnce;
    }

}
