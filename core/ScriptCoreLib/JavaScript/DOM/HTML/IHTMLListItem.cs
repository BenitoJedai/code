using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor=true)]
    public class IHTMLListItem : IHTMLElement
    {
        public IHTMLListItem()
        {
        }

        internal static IHTMLListItem InternalConstructor()
        {
            return (IHTMLListItem)IHTMLElement.InternalConstructor(HTMLElementEnum.li);
        }
    }
}
