using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    /// <summary>
    /// The vertex processor is a programmable unit that operates on incoming vertices and their associated data.
    /// Source code that is compiled and run on this processor forms a vertex shader.
    /// A vertex shader operates on one vertex at a time. The vertex processor does not replace graphics
    /// operations that require knowledge of several vertices at a time.
    /// </summary>
    public class VertexShader : Shader
    {
        [highp]
        vec4 gl_Position; // should be written to
        [mediump]
        float gl_PointSize; // may be written to
    }
}
