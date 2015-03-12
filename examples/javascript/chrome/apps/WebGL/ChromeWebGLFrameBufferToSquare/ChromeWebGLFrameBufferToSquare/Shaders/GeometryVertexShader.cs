using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace ChromeWebGLFrameBufferToSquare.Shaders
{
	[Description("Future versions of JSC will allow shaders to be written in a .NET language")]
	class __GeometryVertexShader : ScriptCoreLib.GLSL.VertexShader
	{
		[attribute]
		vec3 aVertexPosition;

		[uniform]
		mat4 uMVMatrix;

		[uniform]
		mat4 uPMatrix;

		void main()
		{
			gl_Position = uPMatrix * uMVMatrix * vec4(aVertexPosition, 1.0f);
		}
	}
}
