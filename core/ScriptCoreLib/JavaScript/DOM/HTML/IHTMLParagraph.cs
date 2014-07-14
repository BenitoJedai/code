using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLParagraphElement.idl

    [Script(InternalConstructor = true)]
    public class IHTMLParagraph : IHTMLElement
    {


        #region Constructor

        public IHTMLParagraph()
        {
            // InternalConstructor
        }

        static IHTMLParagraph InternalConstructor()
        {
            return (IHTMLParagraph)(object)new IHTMLElement(HTMLElementEnum.p);
        }

        #endregion

    }
}
