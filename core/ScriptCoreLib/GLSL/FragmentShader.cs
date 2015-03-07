using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    public class FragmentShader : Shader
    {
		// https://www.khronos.org/opengles/sdk/docs/man31/html/gl_FragCoord.xhtml
		[mediump]
        protected vec4 gl_FragCoord;

		// https://www.khronos.org/opengles/sdk/docs/man31/html/gl_FrontFacing.xhtml
		protected bool gl_FrontFacing;


        [mediump]
        protected vec4 gl_FragColor;
        [mediump]
        protected vec4[] gl_FragData = new vec4[gl_MaxDrawBuffers];

		// https://www.khronos.org/opengles/sdk/docs/man31/html/gl_PointCoord.xhtml
		[mediump]
        protected vec2 gl_PointCoord;
    }
}
