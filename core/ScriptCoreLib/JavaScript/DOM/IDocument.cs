using ScriptCoreLib.JavaScript;

// http://www.w3.org/TR/DOM-Level-2-HTML/html.html#ID-5353782642

namespace ScriptCoreLib.JavaScript.DOM
{

    [Script(HasNoPrototype = true)]
    public class IDocument : INode
    {
        readonly internal IDOMImplementation implementation;

        [Script(DefineAsStatic = true)]
        public new void appendChild<T>(T e)
        {
			// what if we redirected this to root element instead? :)

            throw new global::System.Exception("IDocument.appendChild is forbidden");
        }

        public ITextNode createTextNode(string text)
        {
            return default(ITextNode);
        }

        public bool hasChildNodes()
        {
            return default(bool);
        }

		// https://developer.mozilla.org/En/DOM/document.importNode
		// http://www.alistapart.com/articles/crossbrowserscripting/
		public INode importNode(INode externalNode, bool deep)
		{
			// Internet Explorer does not understand the DOM Level 2 method importNode().
			// joy.

			return default(INode);
		}

		// https://developer.mozilla.org/en/DOM/document.adoptNode
		public INode adoptNode(INode externalNode)
		{
			return default(INode);

		}

		// http://reference.sitepoint.com/javascript/Document/createAttribute
		public object createAttribute(string name)
		{
			return default(object);
		}
    }

	
    [Script(HasNoPrototype = true)]
    public class IDocument<DOMElement> : IDocument
        where DOMElement : IElement
    {
        internal DOMElement createElementNS(string ns, string tag)
        {
            return default(DOMElement);
        }

        public DOMElement createElement(string tagName)
        {
            return default(DOMElement);
        }



        public DOMElement[] getElementsByTagName(string tag)
        {
            return default(DOMElement[]);
        }

        public DOMElement getElementById(string e)
        {
            return default(DOMElement);
        }


        public DOMElement documentElement;

    }
}
