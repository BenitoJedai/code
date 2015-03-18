using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    public class FragmentShader : Shader
    {
		// can we have a jsc nuget to analyse and rewrite glsl code?
		// the CLR version could work like IDL parser, assets libray and link to GLSL types.
		// the chrome version would need type info?

		// Expression to glsl?

		// ANGLE_ENABLE_LOOP_FLATTEN
		// https://chromium.googlesource.com/angle/angle/+/master/src/compiler/translator/ForLoopUnroll.cpp
		// https://chromium.googlesource.com/angle/angle/+/master/src/compiler/translator/OutputGLSLBase.cpp
		// https://chromium.googlesource.com/angle/angle/+/master/src/compiler/translator/OutputHLSL.cpp
		// https://chromium.googlesource.com/angle/angle/+/master/src/compiler/translator/BuiltInFunctionEmulatorHLSL.cpp
		// x:\jsc.svn\examples\javascript\chrome\apps\webgl\chromeshadertoyoculimbobydaeken\chromeshadertoyoculimbobydaeken\shaders\program.frag

		// GLSL allows to write things like "float x = x;" where a new variable x is defined
		// and the value of an existing variable x is assigned. HLSL uses C semantics (the
		// new variable is created before the assignment is evaluated), so we need to convert
		// this to "float t = x, x = t;".

		// https://www.khronos.org/opengles/sdk/docs/man31/html/gl_FragCoord.xhtml
		// even GLSL gets translated
		// static float4 gl_FragCoord = float4(0, 0, 0, 0);

		//  'assign' :  l-value required "gl_FragCoord" (can't modify gl_FragCoord)
		// http://stackoverflow.com/questions/13711252/what-does-gl-fragcoord-z-gl-fragcoord-w-represent
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
