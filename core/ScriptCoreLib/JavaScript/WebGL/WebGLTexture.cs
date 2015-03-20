using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLTexture.idl

	[Script(HasNoPrototype = true, InternalConstructor = true)]
    public class WebGLTexture
    {
		// https://www.opengl.org/sdk/docs/man/html/glCreateTextures.xhtml

		#region Constructor

		[Obsolete("createTexture")]
		public WebGLTexture(WebGLRenderingContext gl)
		{
			// InternalConstructor
		}

		static WebGLTexture InternalConstructor(WebGLRenderingContext gl)
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeWebGLFrameBuffer\ChromeWebGLFrameBuffer\Application.cs
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Library\ShaderToy.cs
			var p = gl.createTexture();

			return p;
		}

		#endregion
	}
}
