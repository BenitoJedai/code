using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLDebugShaders.idl
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLDebugRendererInfo.idl

	[Script(HasNoPrototype = true, InternalConstructor = true)]
	public class WebGLDebugShaders : WebGLObject
	{
		public string getTranslatedShaderSource(WebGLShader shader) { return default(string); }


		//gl.getExtension("WEBGL_debug_shaders")


		#region Constructor

		[Obsolete("getExtension(WEBGL_debug_shaders)")]
		public WebGLDebugShaders(WebGLRenderingContext gl)
		{
			// InternalConstructor
		}

		static WebGLDebugShaders InternalConstructor(WebGLRenderingContext gl)
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Library\ShaderToy.cs
			var p = (WebGLDebugShaders)gl.getExtension("WEBGL_debug_shaders");

			return p;
		}

		#endregion
	}
}
