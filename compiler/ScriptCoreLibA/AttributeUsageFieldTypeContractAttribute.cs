using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AttributeUsageFieldTypeContractAttribute : Attribute
    {
        public readonly Type[] Types;
        public AttributeUsageFieldTypeContractAttribute(params Type[] Types)
        {
            this.Types = Types;
        }

    }
}
