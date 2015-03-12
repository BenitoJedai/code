using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLRenderbuffer.idl

	[Script(HasNoPrototype = true, InternalConstructor = true)]
	public class WebGLFramebuffer
	{
		#region Constructor

		[Obsolete("createFramebuffer")]
		public WebGLFramebuffer(WebGLRenderingContext gl)
		{
			// InternalConstructor
		}

		static WebGLFramebuffer InternalConstructor(WebGLRenderingContext gl)
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeWebGLFrameBuffer\ChromeWebGLFrameBuffer\Application.cs
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Library\ShaderToy.cs
			var p = gl.createFramebuffer();

			return p;
		}

		#endregion
	}
}
