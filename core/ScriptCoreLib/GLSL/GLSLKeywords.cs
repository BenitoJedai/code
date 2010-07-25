using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    // http://www.khronos.org/registry/gles/specs/2.0/GLSL_ES_Specification_1.0.17.pdf

    /// <summary>
    /// A uniform is a global GLSL variable declared with the "uniform" storage qualifier. These act as parameters that the user of a shader program can pass to that program. They are stored in a program object.
    /// http://www.opengl.org/wiki/GLSL_Uniforms
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class uniform : Attribute
    {

    }



    /// <summary>
    /// Varying variables provide the interface between the vertex shader, the fragment shader, and the fixed
    /// functionality between them.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class varying : Attribute
    {

    }

    /// <summary>
    /// Satisfies the minimum requirements above for the fragment language. Its
    /// range and precision has to be greater than or the same as provided by lowp
    /// and less than or the same as provided by highp.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class mediump : Attribute
    {

    }
}
