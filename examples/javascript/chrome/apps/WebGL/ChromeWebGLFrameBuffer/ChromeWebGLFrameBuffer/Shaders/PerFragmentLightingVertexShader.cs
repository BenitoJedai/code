using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace ChromeWebGLFrameBuffer.Shaders
{
	[Description("Future versions of JSC will allow shaders to be written in a .NET language")]
	class __PerFragmentLightingVertexShader : ScriptCoreLib.GLSL.VertexShader
	{
		[attribute]
		vec3 aVertexPosition;
		[attribute]
		vec3 aVertexNormal;
		[attribute]
		vec2 aTextureCoord;

		[uniform]
		mat4 uMVMatrix;
		[uniform]
		mat4 uPMatrix;
		[uniform]
		mat3 uNMatrix;

		[varying]
		vec2 vTextureCoord;
		[varying]
		vec3 vTransformedNormal;
		[varying]
		vec4 vPosition;


		void main()
		{
			vPosition = uMVMatrix * vec4(aVertexPosition, 1.0f);
			gl_Position = uPMatrix * vPosition;
			vTextureCoord = aTextureCoord;
			vTransformedNormal = uNMatrix * aVertexNormal;
		}
	}
}
