using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLContextEvent.idl
	[Script(HasNoPrototype = true)]
    public class WebGLContextEvent : IEvent
    {
		public string statusMessage;
    }
}
