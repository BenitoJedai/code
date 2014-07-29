using ScriptCoreLib.JavaScript;


namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/core/nsIDOMElement.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/dom/Element.idl

    /// <summary>
    /// http://developer.kde.org/documentation/library/3.3-api/khtml/html/classDOM_1_1Element.html
    /// http://developer.mozilla.org/en/docs/DOM:element.setAttribute
    /// http://www.devguru.com/Technologies/xmldom/quickref/obj_node.html
    /// </summary>
    [Script(HasNoPrototype = true)]
    public class IElement : INode
    {

        [System.Obsolete("experimental like .css")]
        public ShadowRoot shadow
        {
            // tested by
            // X:\jsc.svn\examples\javascript\Test\TestShadowSpan\TestShadowSpan\Application.cs
            // X:\jsc.svn\examples\javascript\WebGL\WebGLSpiral\WebGLSpiral\Application.cs

            [method: Script(DefineAsStatic = true)]
            get
            {
                var s = this.shadowRoot;

                if (s != null)
                    return s;


                return this.createShadowRoot();
            }
        }

        //  readonly    attribute ShadowRoot? shadowRoot;
        public readonly ShadowRoot shadowRoot;

        // X:\jsc.svn\examples\javascript\Test\TestShadowDOM\TestShadowDOM\Application.cs
        public ShadowRoot createShadowRoot() { return null; }



        // http://www.w3.org/TR/2000/WD-DOM-Level-1-20000929/idl-definitions.html

        public string tagName;

        internal void setAttributeNS(string ns, string name, object value)
        {

        }

        public void setAttribute(string name, object value)
        {

        }

        // http://msdn.microsoft.com/en-us/library/ms536429(VS.85).aspx
        // https://developer.mozilla.org/en-US/docs/Web/API/element.getAttributeNode
        // http://www.w3.org/TR/DOM-Level-2-Core/core.html#ID-217A91B8
        public IAttr getAttributeNode(string name)
        {
            return default(IAttr);
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

        // https://developer.mozilla.org/En/DOM/Node.attributes
        public IAttr[] attributes;

        // https://developer.mozilla.org/en-US/docs/Web/API/element?redirectlocale=en-US&redirectslug=DOM%2Felement
        //public INode[] querySelectorAll( selectors[, nsresolver] )
        public INode[] querySelectorAll(string selectors)
        {
            return null;
        }


    }
}
