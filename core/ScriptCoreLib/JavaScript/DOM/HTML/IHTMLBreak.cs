using ScriptCoreLib;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLBRElement.idl

    [Script(InternalConstructor = true)]
    public class IHTMLBreak : IHTMLElement
    {


        #region Constructor

        public IHTMLBreak()
        {
            // InternalConstructor
        }

        static IHTMLBreak InternalConstructor()
        {
            return (IHTMLBreak)((object)new IHTMLElement(HTMLElementEnum.br));
        }

        #endregion
    }
}
