using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.GLSL
{
    // http://www.khronos.org/registry/gles/specs/2.0/GLSL_ES_Specification_1.0.17.pdf



    /// <summary>
    /// Uniforms are used to pass information to your shaders other than vertices. 
    /// http://codeflow.org/entries/2012/apr/25/webgl-statistics-and-the-state-of-webgl-html5/
    /// A uniform is a global GLSL variable declared with the "uniform" storage qualifier. These act as parameters that the user of a shader program can pass to that program. They are stored in a program object.
    /// http://www.opengl.org/wiki/GLSL_Uniforms
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    [Description("Vertex Shader max: 254 vectors, 1016 floats")]
    [Description("Fragment Shader max: 29 vectors, 116 floats")]
    public sealed class uniform : Attribute
    {

    }
}
