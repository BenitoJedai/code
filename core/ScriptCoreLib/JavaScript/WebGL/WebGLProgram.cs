using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLProgram.idl

	[Script(HasNoPrototype = true, InternalConstructor = true)]
	public class WebGLProgram
	{
		// https://www.opengl.org/sdk/docs/man/html/glCreateProgram.xhtml

		// could we add a ref to gl?
		// what if its a shared object?

		// https://www.khronos.org/registry/webgl/extensions/WEBGL_shared_resources/
		// Note that implementing this extension changes the base class of the sharable resources. Specifically: WebGLBuffer, WebGLProgram, WebGLRenderbuffer, WebGLShader, and WebGLTexture change their base class from WebGLObject to WebGLSharedObject.

		#region Constructor

		[Obsolete("createProgram")]
		public WebGLProgram(WebGLRenderingContext gl)
		{
			// InternalConstructor
		}

		static WebGLProgram InternalConstructor(WebGLRenderingContext gl)
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Library\ShaderToy.cs
			var p = gl.createProgram();

			return p;
		}

		#endregion
	}

}
