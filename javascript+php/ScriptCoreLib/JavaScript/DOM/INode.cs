using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class INode : ISink
    {
        protected INode() {}

        public string nodeValue;
        public string nodeName;

        /// <summary>
        /// http://msdn.microsoft.com/workshop/author/dhtml/reference/properties/nodetype.asp
        /// </summary>
        public enum NodeTypeEnum
        {
            ElementNode = 1,
            TextNode = 3
        }

        public NodeTypeEnum nodeType;

        public INode parentNode;

        public INode firstChild;
        public INode lastChild;

        public INode previousSibling;
        public INode nextSibling;

        public INode[] childNodes;

        public INode cloneNode(bool deep) { return default(INode);  }

        public readonly IDocument<IElement> ownerDocument;



        public void appendChild(INode child)
        {

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

        [Script(DefineAsStatic=true)]
        public void appendChild(params INode[] children)
        {
                foreach (INode x in children)
                    appendChild(x);

        }

        [Script(DefineAsStatic=true)]
        public void appendChild(params string[] e)
        {
            foreach(string z in e)
                appendChild(new ITextNode(this.ownerDocument, z));
        }


        public void removeChild(INode e)
        {
        }
    }
}