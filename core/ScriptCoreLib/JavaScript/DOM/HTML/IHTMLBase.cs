using ScriptCoreLib;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLBaseElement.idl
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/html/nsIDOMHTMLBaseElement.idl

    // http://stackoverflow.com/questions/1889076/is-it-recommended-to-use-the-base-html-tag
    // http://www.whatwg.org/specs/web-apps/current-work/multipage/semantics.html#the-base-element
    [Script(InternalConstructor = true)]
    public class IHTMLBase : IHTMLElement
    {
        public string href;
        public string target;

        #region Constructor

        public IHTMLBase()
        {
            // InternalConstructor
        }

        static IHTMLBreak InternalConstructor()
        {
            return (IHTMLBreak)((object)new IHTMLElement(HTMLElementEnum.@base));
        }

        #endregion
    }
}
