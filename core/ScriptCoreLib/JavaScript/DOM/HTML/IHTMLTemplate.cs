using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://www.w3.org/TR/html5/scripting-1.html#the-template-element
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/HTMLTemplateElement.webidl

    [Script(InternalConstructor = true)]
    public class IHTMLTemplate : IHTMLElement<IHTMLTemplate>
    {
        // once template becomes popular AssetsLibrary needs to learn it too

        public readonly IDocumentFragment content;

        // tested by ?
        // X:\jsc.svn\examples\javascript\Test\TestTemplateElement\TestTemplateElement\Application.cs



        public IHTMLTemplate() { }

        internal static IHTMLTemplate InternalConstructor()
        {
            return (IHTMLTemplate)((object)new IHTMLElement(HTMLElementEnum.template));
        }


    }
}
