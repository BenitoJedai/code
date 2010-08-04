using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    // http://www.khronos.org/registry/gles/specs/2.0/GLSL_ES_Specification_1.0.17.pdf

   

    /// <summary>
    /// The attribute qualifier is used to declare variables that are passed to a vertex shader from OpenGL ES on
    /// a per-vertex basis.
    /// 
    /// The attribute qualifier can be used only with the data types float, vec2, vec3, vec4, mat2, mat3, and mat4
    /// 
    /// GLSL 4 uses "in" attribute instead.
    /// </summary>
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
