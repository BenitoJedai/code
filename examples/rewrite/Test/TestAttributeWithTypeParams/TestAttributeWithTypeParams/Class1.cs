using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAttributeWithTypeParams
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

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    [AttributeUsageFieldTypeContractAttribute(
        typeof(float)
        //jsc does not redirect those yet?
        //typeof(vec2),
        //typeof(vec3),
        //typeof(vec4),
        //typeof(mat2),
        //typeof(mat3),
        //typeof(mat4)
        )]
    public sealed class attribute : Attribute
    {

    }
}
