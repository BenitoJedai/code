using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://www.w3.org/TR/html5/scripting-1.html#the-template-element
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/HTMLTemplateElement.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLContentElement.idl
    // https://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLContentElement.cpp

    [Script(InternalConstructor = true)]
    public class IHTMLContent : IHTMLElement<IHTMLContent>
    {

        // X:\jsc.svn\examples\javascript\Test\TestInsertionPoints\TestInsertionPoints\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\SplitContainer\SplitContainer.cs

        // X:\jsc.svn\examples\javascript\Test\TestShadowSelectByClass\TestShadowSelectByClass\Application.cs
        // http://www.webcomponentsshift.com/#40
        public string select;

        // once template becomes popular AssetsLibrary needs to learn it too
        // 
        public readonly IDocumentFragment content;

        // tested by ?
        // X:\jsc.svn\examples\javascript\test\TestShadowDOM\TestShadowDOM\Application.cs



        public IHTMLContent() { }

        internal static IHTMLContent InternalConstructor()
        {
            return (IHTMLContent)((object)new IHTMLElement(HTMLElementEnum.content));
        }


    }
}
