using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public class IHTMLElement : INode
	{
		public string token { get; set; }

		public string id { get; set; }

		public ExternalContext context { get; set; }


		IHTMLDocument _ownerDocument;
		public IHTMLDocument ownerDocument
		{
			get
			{
				if (_ownerDocument == null)
					_ownerDocument = new IHTMLDocument { context = context };

				return _ownerDocument;
			}
		}

		internal string _innerHTML;
		public string innerHTML
		{
			set
			{
				if (context == null)
				{
					var v = value;
					_innerHTML = v;
					return;
				}

				if (token == null)
					throw new Exception("token");

				context.ExternalContext_IHTMLElement_set_innerHTML(value, token);
			}
		}

		protected override void INode_appendChild(INode child)
		{
			var childelement = child as IHTMLElement;
			if (childelement != null)
			{
				if (this.token != null)
				{
					if (this.context != null)
					{
						if (childelement.context == null)
							this.ownerDocument.createElement(childelement);

						this.context.ExternalContext_IHTMLElement_appendChild(this.token, childelement.token);

						return;
					}
					else
					{
						throw new Exception("parent.context");
					}
				}
				else
				{
					throw new Exception("parent.token");
				}
			}
			else
			{
				throw new Exception("typeof(IHTMLElement)");
			}


			throw new Exception("INode_appendChild failed");
		}
	}
}
