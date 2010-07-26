using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    public class FragmentShader : Shader
    {
        [mediump]
        protected vec4 gl_FragCoord;
        protected bool gl_FrontFacing;
        [mediump]
        protected vec4 gl_FragColor;
        [mediump]
        protected vec4[] gl_FragData = new vec4[gl_MaxDrawBuffers];
        [mediump]
        protected vec2 gl_PointCoord;
    }
}
