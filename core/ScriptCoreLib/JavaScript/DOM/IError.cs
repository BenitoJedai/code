using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/Microsoft/LiveLabs/JavaScript/JSError.cs
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/dom/DOMError.idl

    // http://www.w3.org/TR/domcore/#domexception
    // http://www.w3.org/TR/domcore/#interface-domerror
	[Script(HasNoPrototype = true, ExternalTarget = "Error")]
	public class IError 
	{
        // https://code.google.com/p/v8/wiki/JavaScriptStackTraceApi
        // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Error/Stack
        // http://msdn.microsoft.com/en-us/library/ie/hh699850(v=vs.94).aspx

        public string name;

        // ??
        // X:\jsc.svn\examples\javascript\async\AsyncWindowUncaughtError\AsyncWindowUncaughtError\Application.cs
        public object stack;
	}
}
