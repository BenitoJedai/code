using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
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
