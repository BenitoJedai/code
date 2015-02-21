using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLAnchorElement.idl
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/html/nsIDOMHTMLAnchorElement.idl

    [Script(InternalConstructor = true)]
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


        // http://www.chromestatus.com/features/6473924464345088
        // http://stackoverflow.com/questions/19327749/javascript-blob-filename-without-link
        public string download;
        // tested by?


        // http://curia.europa.eu/juris/document/document.jsf?text=&docid=147847&pageIndex=0&doclang=EN&mode=req&dir=&occ=first&part=1&cid=60388
        public string href;
        public string target;

        
    }
}
