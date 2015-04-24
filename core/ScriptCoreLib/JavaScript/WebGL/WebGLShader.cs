using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLShader.cpp
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLShader.h
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLShader.idl

	[Script(HasNoPrototype = true, InternalConstructor = true)]
	public class WebGLShader
	{
		// http://www.saschawillems.de/?page_id=1822
		// http://www.g-truc.net/post-0714.html#menu
		// http://j2glsl.googlecode.com/svn/trunk/Java2GLSL/src/kr/co/iniline/j2glsl/
		//http://www.java-gaming.org/index.php?topic=22493.0
		// https://www.opengl.org/sdk/docs/man/html/glCreateShader.xhtml

		#region Constructor

		[Obsolete("createShader")]
		public WebGLShader(WebGLRenderingContext gl, uint type = WebGLRenderingContext.VERTEX_SHADER)
		{
			// InternalConstructor
		}

		static WebGLShader InternalConstructor(WebGLRenderingContext gl, uint type = WebGLRenderingContext.VERTEX_SHADER)
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Library\ShaderToy.cs
			var p = gl.createShader(type);

			return p;
		}

		#endregion

		// https://www.facebook.com/Shadertoy/posts/190170644463245
		// while()" doesn't exist. Please use only loops of the form "for( int i=0; i<CONSTANT; i++)". remember, 
		// "while" doesn't exist in GLSL. See here: http://www.khronos.org/opengles/sdk/docs/manglsl/

		// http://hothardware.com/news/amd-tackles-virtual-reality-with-liquidvr-sdk-at-gdc

		//  Creating and destroying GPU resources is one of the slowest things you can do on a per frame basis. 
		// https://jeremiahmorrill.wordpress.com/2011/02/14/a-critical-deep-dive-into-the-wpf-rendering-system/

		// http://webglreport.com/

		// WebGL 1.0 supports tokens up to 256 characters in length. WebGL 2.0 follows The OpenGL ES Shading Language,
		// Version 3.00 (OpenGL ES 3.0.3 §3.8) and allows tokens up to 1024 characters in length. 
		// Shaders containing tokens longer than 1024 characters must fail to compile.

		// "X:\opensource\android-ndk-r10c\sources\android\ndk_helper\shader.cpp"

		// There's no way to bail out of the loop early, at least with OpenGL ES 2.0 (WebGL) shaders. We can't break or do any sort of branching on the loop variable
		// http://nullprogram.com/blog/2014/06/01/


	}
}
