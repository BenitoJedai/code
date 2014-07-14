using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLUListElement.idl

    [Script(InternalConstructor=true)]
    public class IHTMLUnorderedList : IHTMLElement
    {
        public IHTMLUnorderedList()
        {
        }

        internal static IHTMLUnorderedList InternalConstructor()
        {
            return (IHTMLUnorderedList)IHTMLElement.InternalConstructor(HTMLElementEnum.ul);
        }
    }
}
