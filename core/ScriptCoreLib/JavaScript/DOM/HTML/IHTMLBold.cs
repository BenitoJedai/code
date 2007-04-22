using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor=true)]
    public class IHTMLBold : IHTMLElement
    {
        public IHTMLBold()
        {
        }

        public IHTMLBold(string e)
        {
        }

        internal static IHTMLBold InternalConstructor()
        {
            return (IHTMLBold)IHTMLElement.InternalConstructor(HTMLElementEnum.b);
        }

        internal static IHTMLBold InternalConstructor(string e)
        {
            return (IHTMLBold)IHTMLElement.InternalConstructor(HTMLElementEnum.b, e);
        }
    }
}
