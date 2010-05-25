using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor=true)]
    public class IHTMLOrderedList : IHTMLElement
    {
        public IHTMLOrderedList()
        {
        }

        internal static IHTMLOrderedList InternalConstructor()
        {
            return (IHTMLOrderedList)IHTMLElement.InternalConstructor(HTMLElementEnum.ol);
        }
    }
}
