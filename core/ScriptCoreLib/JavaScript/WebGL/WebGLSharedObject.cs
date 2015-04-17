using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
	// https://www.khronos.org/webgl/wiki/SharedResouces
	// https://code.google.com/p/chromium/issues/detail?id=245894
	// https://chromium.googlesource.com/external/Webkit/+/master/Source/WebCore/html/canvas/WebGLRenderingContext.h

	[Script(HasNoPrototype = true)]
	public class WebGLSharedObject : WebGLObject
	{
		// https://www.khronos.org/registry/webgl/extensions/WEBGL_shared_resources/
	}
}
