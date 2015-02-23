using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "DragEvent")]
    public class DragEvent : IEvent
    {
        // "X:\jsc.svn\examples\javascript\Test\TestPackageAsApplication\TestPackageAsApplication.sln"
        // downloadURL no longer works?

        public readonly DataTransfer dataTransfer;

        public DragEvent(string type) { }
    }
}
