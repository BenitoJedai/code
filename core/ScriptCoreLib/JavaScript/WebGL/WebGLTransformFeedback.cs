using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    // https://www.khronos.org/registry/webgl/specs/latest/2.0/
    // http://www.opengl.org/wiki/Query_Object

    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/WebGL2RenderingContext.webidl
    [Script(HasNoPrototype = true, InternalConstructor = true)]
    public class WebGLTransformFeedback : WebGLObject
    {
		// https://www.opengl.org/sdk/docs/man/html/glCreateTransformFeedbacks.xhtml
		// mentioned by Brandon Jones
		// https://www.youtube.com/watch?v=zPNM3yOsP0I

		// tested by ?
	}
}
