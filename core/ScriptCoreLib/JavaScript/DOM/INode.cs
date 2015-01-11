using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Query;
using ScriptCoreLib.JavaScript.Extensions;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using System;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/dom/Node.idl


    // X:\opensource\github\WootzJs\WootzJs.Web\Node.cs

    // SCRIPT5009: 'Node' is undefined 
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/core/nsIDOMElement.idl
    [Script(HasNoPrototype = true)]
    public partial class INode :
        IEventTarget,
        IEnumerable<INode>
    {


        // http://www.w3.org/TR/2000/WD-DOM-Level-1-20000929/idl-definitions.html
        // http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113/idl-definitions.html

        //readonly attribute NamedNodeMap     attributes;

        // add

        [Script(HasNoPrototype = true)]
        class __INode_text : INode
        {
            public string text;
            public string textContent;
        }

        protected INode() { }

        public string nodeValue;
        public string nodeName;

        public string text
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // http://www.webmasterworld.com/forum26/119.htm

                var x = (__INode_text)this;

                if (Expando.InternalIsMember(x, "text"))
                    return x.text;

                if (Expando.InternalIsMember(x, "textContent"))
                    return x.textContent;

                // why not only read this?
                if (Expando.InternalIsMember(x, "nodeValue"))
                    return this.nodeValue;

                throw new System.Exception(".text");
            }


        }

        /// <summary>
        /// http://msdn.microsoft.com/workshop/author/dhtml/reference/properties/nodetype.asp
        /// 
        /// http://www.w3schools.com/dom/dom_nodetype.asp
        /// 
        /// to be replace with  System.Xml.XmlNodeType ?
        /// </summary>
        public enum NodeTypeEnum
        {
            None = 0,
            ElementNode = 1,
            TextNode = 3,
            CommentNode = 8
        }

        public NodeTypeEnum nodeType;

        public INode parentNode;

        public INode firstChild;
        public INode lastChild;

        public INode previousSibling;
        public INode nextSibling;

        public INode[] childNodes;

        public INode cloneNode(bool deep) { return default(INode); }

        public readonly IDocument<IElement> ownerDocument;



        public void appendChild(INode child)
        {

        }



        [Script(DefineAsStatic = true)]
        public void appendChild(params INode[] children)
        {
            foreach (INode x in children)
                appendChild(x);

        }

        [Script(DefineAsStatic = true)]
        public void appendChild(params string[] e)
        {
            foreach (string z in e)
                appendChild(new ITextNode(this.ownerDocument, z));
        }






        public void insertBefore(INode newNode, INode oldNode)
        {

        }



        [Script(DefineAsStatic = true)]
        public void insertPreviousSibling(INode e)
        {
            parentNode.insertBefore(e, this);
        }

        /// <summary>
        /// extension method
        /// </summary>
        /// <param name="e"></param>
        [Script(DefineAsStatic = true)]
        public void insertNextSibling(INode e)
        {
            if (nextSibling == null)
            {
                parentNode.appendChild(e);
            }
            else
            {
                nextSibling.insertPreviousSibling(e);
            }
        }


        public void removeChild(INode e)
        {
        }

        // http://developer.apple.com/internet/webcontent/dom2i.html
        public void replaceChild(INode _new, INode _old)
        {
        }

        #region IEnumerable<INode> Members

        [Script(DefineAsStatic = true)]
        public IEnumerator<INode> GetEnumerator()
        {
            // implementing interfaces on native types
            // requres DefineAsStatic

            // invoking explicit interface methods
            // is not currently supported

            // todo: jsc should create a wrapper to call DefineAsStatic methods via interf

            // does jsc support Array / T[] to IEnumerable<T> yet?

            var a = new List<INode>();

            foreach (var item in this.childNodes)
            {
                a.Add(item);
            }

            return a.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        [Script(DefineAsStatic = true)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            // notice: this method cannot be called by current jsc 
            // implementation.

            return this.GetEnumerator();
        }






        #endregion



        // 
    }
}