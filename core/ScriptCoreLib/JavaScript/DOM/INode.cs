using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Query;

namespace ScriptCoreLib.JavaScript.DOM
{

	[Script(HasNoPrototype = true)]
	public class INode : ISink, IEnumerable<INode>
	{
		// http://www.w3.org/TR/2000/WD-DOM-Level-1-20000929/idl-definitions.html


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

				throw new System.Exception(".text");
			}


		}

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

		public INode cloneNode(bool deep) { return default(INode); }

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

		[Script(DefineAsStatic = true)]
		public void Add(INode e)
		{
			// Implementing Collection Initializers
			// http://msdn.microsoft.com/en-us/library/bb384062.aspx

			this.appendChild(e);
		}

		[Script(DefineAsStatic = true)]
		public void Add(string e)
		{
			// Implementing Collection Initializers
			// http://msdn.microsoft.com/en-us/library/bb384062.aspx

			this.appendChild(new ITextNode(e));
		}
	}
}