using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib
{
    [global::System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ScriptDelegateDataHintAttribute : Attribute
    {

        // this class shall be omitted from the future versions of jsc
        // jsc needs to infer this information by itself

        public enum FieldType
        {
            List,
            Target,
            Method,

            // special
            // X:\jsc.svn\examples\javascript\test\TestIDLDelegateToFunction\TestIDLDelegateToFunction\Class1.cs
            AsFunction
        }

        public readonly FieldType Value;

        public ScriptDelegateDataHintAttribute(FieldType Value)
        {
            this.Value = Value;
        }
    }

}
