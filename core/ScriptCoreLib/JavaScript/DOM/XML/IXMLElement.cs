using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.XML
{
    [Script(InternalConstructor=true)]
    public class IXMLElement : IElement
    {
        #region constructors
        public IXMLElement(IXMLDocument doc, string name, string value) { }
        public IXMLElement(IXMLDocument doc, string name, params INode[] value) { }


        static IXMLElement InternalConstructor(IXMLDocument doc, string name, params INode[] children)
        {
            IXMLElement x = doc.createElement(name);

            if (children.Length > 0)
                x.appendChild(children);

            return x;
        }

        static IXMLElement InternalConstructor(IXMLDocument doc, string name, string value) 
        {
            IXMLElement n = doc.createElement(name);


            if (value != null)
                n.appendChild(new ITextNode(doc, value));

            return n;
        }

        #endregion



        public string outerXML
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return IXMLDocument.ToXMLString(this);
            }
        }

        public string innerXML
        {
            [Script(DefineAsStatic = true)]
            get
            {
                IArray<string> z = new IArray<string>();


                foreach (IXMLElement x in this.childNodes)
                    z.push(x.outerXML);

                return z.join();
            }
        }
    }
}
