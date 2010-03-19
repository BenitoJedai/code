using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting
{
	public partial interface PHTMLElement : PNode
	{
		// to be used from flash or java applet
		// to be defined as async API


		void setAttribute(string name, string value);


		string innerText { set; }

		void get_style(PStyleAction e);

		void get_ownerDocument(PHTMLDocumentAction doc);
	}

	public delegate void PHTMLElementAction(PHTMLElement e);

	public partial class PIHTMLElement : PINode, PHTMLElement
	{
		internal IHTMLElement InternalElement;

		public void setAttribute(string name, string value)
		{
			this.InternalElement.setAttribute(name, value);
		}



		public string innerText
		{
			set
			{
				this.InternalElement.innerText = value;
			}
		}

		public void get_style(PStyleAction e)
		{
			e(
				new PIStyle
				{
					InternalStyle = this.InternalElement.style
				}
			);
		}

		public void get_ownerDocument(PHTMLDocumentAction y)
		{
			// type safety out the window..
			var doc = (IHTMLDocument)(object)this.InternalElement.ownerDocument;

			y(
				(PIHTMLDocument)doc
			);
		}


		public static implicit operator PIHTMLElement(IHTMLElement i)
		{
			var v = new PIHTMLElement
			{
				InternalElement = i,
				InternalNode = i
			};

			return v;
		}

		public static implicit operator IHTMLElement(PIHTMLElement i)
		{
			return i.InternalElement;
		}
	}
}
