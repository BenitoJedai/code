using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib;
using System;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://developer.mozilla.org/en-US/docs/Web/API/ErrorEvent
    [Script(HasNoPrototype = true)]
    public class IErrorEvent : IEvent
    {
        // http://www.w3.org/TR/html5/webappapis.html#the-errorevent-interface
        // X:\jsc.svn\examples\javascript\async\AsyncWindowUncaughtError\AsyncWindowUncaughtError\Application.cs

        public readonly string message;
        public readonly string filename;
        public readonly int lineno;
        public readonly int column;
        public readonly IError error;
    }




}
