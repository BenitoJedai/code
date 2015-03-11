using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLProgram.idl

    [Script(HasNoPrototype = true)]
    public class WebGLProgram
    {
		// https://www.khronos.org/registry/webgl/extensions/WEBGL_shared_resources/
		// Note that implementing this extension changes the base class of the sharable resources. Specifically: WebGLBuffer, WebGLProgram, WebGLRenderbuffer, WebGLShader, and WebGLTexture change their base class from WebGLObject to WebGLSharedObject.

	}

}
