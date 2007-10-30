using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(HasNoPrototype = true)]
    internal class IHTMLDocumentNS : IHTMLDocument
    {

        public IHTMLElement createElementNS(string ns, string tag)
        {
            return default(IHTMLElement);
        }
    }
}
