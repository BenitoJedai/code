using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace ChromeWebGLFrameBufferToSquare.Shaders
{
	[Description("Future versions of JSC will allow shaders to be written in a .NET language")]
	class __GeometryFragmentShader : FragmentShader
	{
		void main()
		{
			gl_FragColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);
		}
	}
}
