using ScriptCoreLib.JavaScript;

// http://www.w3.org/TR/DOM-Level-2-HTML/html.html#ID-5353782642

namespace ScriptCoreLib.JavaScript.DOM
{

    [Script(HasNoPrototype = true)]
    public class IDocument : INode
    {
        [Script(DefineAsStatic = true)]
        public new void appendChild<T>(T e)
        {
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
