using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://www.w3.org/TR/html5/scripting-1.html#the-template-element
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/HTMLTemplateElement.webidl

    [Script(InternalConstructor = true)]
    public class HTMLContentElement : IHTMLElement<HTMLContentElement>
    {
        // once template becomes popular AssetsLibrary needs to learn it too
        // 
        public readonly IDocumentFragment content;

        // tested by ?
        // X:\jsc.svn\examples\javascript\test\TestShadowDOM\TestShadowDOM\Application.cs



        public HTMLContentElement() { }

        internal static HTMLContentElement InternalConstructor()
        {
            return (HTMLContentElement)((object)new IHTMLElement(HTMLElementEnum.content));
        }


    }
}
