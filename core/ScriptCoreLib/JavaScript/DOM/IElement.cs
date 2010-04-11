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
		// http://www.w3.org/TR/2000/WD-DOM-Level-1-20000929/idl-definitions.html

        public string tagName;

        internal void setAttributeNS(string ns, string name, object value)
        {

        }

        public void setAttribute(string name, object value)
        {

        }

		// http://msdn.microsoft.com/en-us/library/ms536429(VS.85).aspx

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

		// https://developer.mozilla.org/En/DOM/Node.attributes
		public IAttr[] attributes;
    }
}
