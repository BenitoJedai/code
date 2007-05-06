using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor=true)]
    public class IHTMLSpan : IHTMLElement
    {


        #region ctor
        public IHTMLSpan()
        {
        }

        public IHTMLSpan(string html)
        {
        }

        public IHTMLSpan(params INode[] e)
        {
        }

        static IHTMLSpan InternalConstructor()
        {
            return (IHTMLSpan)Native.Document.createElement(IHTMLElement.HTMLElementEnum.span);
        }

        static IHTMLSpan InternalConstructor(string e)
        {
            IHTMLSpan n = new IHTMLSpan();

            n.innerHTML = e;

            return n;
        }

       static IHTMLSpan InternalConstructor(params INode[] e)
        {
            IHTMLSpan n = new IHTMLSpan();

            n.appendChild( e );

            return n;
        }


        #endregion

    }
}
