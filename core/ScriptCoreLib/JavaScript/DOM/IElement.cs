using ScriptCoreLib.JavaScript;


namespace ScriptCoreLib.JavaScript.DOM
{
    /// <summary>
    /// http://developer.kde.org/documentation/library/3.3-api/khtml/html/classDOM_1_1Element.html
    /// http://developer.mozilla.org/en/docs/DOM:element.setAttribute
    /// http://www.devguru.com/Technologies/xmldom/quickref/obj_node.html
    /// </summary>
    [Script(HasNoPrototype = true)]
    public class IElement : INode
    {
        public string tagName;

        internal void setAttributeNS(string ns, string name, object value)
        {

        }

        public void setAttribute(string name, object value)
        {

        }

        public object getAttribute(string name)
        {
            return default(object);
        }

        public bool hasAttribute(string name)
        {
            return default(bool);
        }

        public void removeAttribute(string name)
        {
        }
    }
}
