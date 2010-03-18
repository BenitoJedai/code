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

	

		public static implicit operator PIHTMLElement(IHTMLElement i)
		{
			var v = new PIHTMLElement
			{
				InternalElement = i,
				InternalNode = i
			};

			return v;
		}
	}
}
