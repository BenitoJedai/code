using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{

   
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/dom/MutationRecord.idl
    [Script(HasNoPrototype = true, ExternalTarget = "MutationObserver")]
    public class MutationRecord
    {
        public readonly string type;
        public readonly INode target;

        // MutationCallback: {{ type = childList }}
        // x:\jsc.svn\examples\javascript\chrome\extensions\chromeextensionpreshadow\chromeextensionpreshadow\application.cs
        public readonly INode[] addedNodes;

        

        public readonly string attributeName;
    }


}
