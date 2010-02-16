using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor=true)]
    public class IHTMLAnchor : IHTMLElement
    {
        #region constructors

        public IHTMLAnchor(string href, string text)
        {
        }

        public IHTMLAnchor(string href, params INode[] children)
        {
        }

        public IHTMLAnchor(string text)
        {
        }

		public IHTMLAnchor()
		{
		}


		private static IHTMLAnchor InternalConstructor()
		{
			IHTMLAnchor a = (IHTMLAnchor)new IHTMLElement(HTMLElementEnum.a);


			return a;
		}

        private static IHTMLAnchor InternalConstructor(string text)
        {
            IHTMLAnchor a = InternalConstructor("about:blank", text);


            return a;
        }

        private static IHTMLAnchor InternalConstructor(string href, params INode[] children)
        {
            IHTMLAnchor a = (IHTMLAnchor)new IHTMLElement(HTMLElementEnum.a);

            a.href = href;
            a.target = "_blank";
            a.appendChild(children);

            return a;
        }

        private static IHTMLAnchor InternalConstructor(string href, string text)
        {
            IHTMLAnchor a = (IHTMLAnchor)new IHTMLElement(HTMLElementEnum.a);

            a.href = href;
            a.target = "_blank";

            if (text != null)
            {
                a.appendChild(text);
            }

            return a;
        }

        #endregion

        public string href;
        public string target;


    }
}
