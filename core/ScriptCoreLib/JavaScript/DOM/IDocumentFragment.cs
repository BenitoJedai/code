using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/DocumentFragment.webidl
    // https://github.com/adobe/webkit/blob/master/Source/WebCore/dom/DocumentFragment.idl

    [Script(HasNoPrototype = true)]
    public class IDocumentFragment : INode
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\IHTMLTemplate.cs

        //public IElement getElementById(string elementId) { return null; }

        public IElement querySelector(string selectors) { return null; }

        public INode[] querySelectorAll(string selectors) { return null; }
    }
}
