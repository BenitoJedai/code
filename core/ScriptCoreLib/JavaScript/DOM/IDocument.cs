using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System;

// http://www.w3.org/TR/DOM-Level-2-HTML/html.html#ID-5353782642

namespace ScriptCoreLib.JavaScript.DOM
{
	// http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Document.webidl
	// https://code.google.com/p/dart/source/browse/third_party/WebCore/core/dom/Document.idl?r=26952
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/dom/Document.idl

	// http://www.w3schools.com/dom/dom_document.asp
	// https://github.com/bridgedotnet/Bridge/blob/master/Html5/Document.cs

	[Script(HasNoPrototype = true)]
    public partial class IDocument : INode
    {
		// https://developer.mozilla.org/en-US/docs/Web/API/Document/registerElement


		// or is it to be moved to IHTMLDocument?
		[Script(DefineAsStatic = true)]
        public IFunction registerElement<TElement>(
            string name,
            Action<TElement> createdCallback,
            Action<string, TElement> attributeChangedCallback = null
            ) where TElement : IHTMLElement
        {
            // !! Elements cannot be registered from extensions.
            // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionPreShadow\ChromeExtensionPreShadow\Application.cs
            // https://code.google.com/p/chromium/issues/detail?id=390807


            // X:\jsc.svn\examples\javascript\WebGL\WebGLSpiral\WebGLSpiral\Application.cs

            // what if we used it for image assets, audio assets?

            // Custom element names must always contain a dash (-). ?
            // http://src.chromium.org/viewvc/chrome/trunk/src/chrome/renderer/resources/extensions/web_view.js

            // lets hope this wont turn out like XAML is. complicated as hell.


            // http://w3c.github.io/webcomponents/spec/custom/

            // https://code.google.com/p/chromium/issues/detail?id=180965

            // X:\jsc.svn\examples\javascript\Test\TestShadowIFrame\TestShadowIFrame\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestShadowDOM\TestShadowDOM\Application.cs

            // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
            dynamic prototype = new IFunction("return Object.create(HTMLElement.prototype)").apply(null);


            // first attempt to allow <element> inheritace ctor?
            prototype.createdCallback =
                 new IFunction("y", "return function () { y(this); };").apply(null,
                     (IFunction)createdCallback
                 );

            // https://github.com/Polymer/CustomElements

            if (attributeChangedCallback != null)
            {
                prototype.attributeChangedCallback =
                      new IFunction("y", "return function (attributeName) { y(attributeName, this); };").apply(null,
                          (IFunction)attributeChangedCallback
                      );

            }


            // http://html5-demos.appspot.com/shadowdom-visualizer
            // http://component.kitchen/components/CustomElements
            var __foo = this.registerElement(name,
                new { prototype = (object)prototype }
            );

            return __foo;
        }
                

        //object registerElement(DOMString name, optional ElementRegistrationOptions options);
        public IFunction registerElement(string name, object options)
        {
            // X:\jsc.svn\examples\javascript\Test\TestShadowDOM\TestShadowDOM\Application.cs

            return null;
        }

        public IFunction registerElement(string name)
        {
            // X:\jsc.svn\examples\javascript\Test\TestShadowDOM\TestShadowDOM\Application.cs

            return null;
        }


        readonly internal IDOMImplementation implementation;


        // http://www.w3schools.com/jsref/prop_doc_baseuri.asp
        public string baseURI;



        [Script(DefineAsStatic = true)]
        public new void appendChild<TChild>(TChild e)
        {
            // what if we redirected this to root element instead? :)

            throw new global::System.Exception("IDocument.appendChild is forbidden");
        }

        /// <summary>
        /// The createComment() method creates a comment node.
        /// This method returns a Comment object.
        /// </summary>
        /// <param name="data">A string that specifies the data for the node</param>
        /// <returns></returns>
        public ICommentNode createComment(string data)
        {
            // http://msdn.microsoft.com/en-us/library/ms536383(VS.85).aspx

            return default(ICommentNode);
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
        [Obsolete]
        public INode importNode(INode externalNode, bool deep)
        {
            // http://msdn.microsoft.com/en-us/library/ie/ff975209(v=vs.85).aspx

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
        public DOMElement[] querySelectorAll(string selectors)
        {
            // http://www.w3.org/TR/selectors-api/
            return null;
        }

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

        //internal static void __dummy_IHTMLElement<TDummy>(TDummy e) where TDummy : IHTMLElement
        //{

        //}

        // Could not load type 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement' from assembly 
        // 'ScriptCoreLib, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.

        // at System.Reflection.Emit.TypeBuilder._TermCreateClass(Int32 handle, Module module)
        // at System.Reflection.Emit.TypeBuilder.CreateTypeNoLock()
        // at System.Reflection.Emit.TypeBuilder.CreateType()

        // JSC is still rewriting IHTMLElement but IDocument`1 needs it...
        internal static IHTMLElement __InternalTypeReferenceHint__1;
    }
}
