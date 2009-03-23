using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public abstract class IHTMLElement : INode
	{
		protected event Action tokenChanged;

		string _token;
		public string token
		{
			get
			{
				return _token;
			}
			set
			{
				_token = value;
				if (tokenChanged != null)
					tokenChanged();
			}
		}

		public string id { get; set; }

		public ExternalContext context { get; set; }

		public IHTMLElement()
		{
			this.tokenChanged +=
				delegate
				{
					// update properties
					if (this._innerHTML != null)
						this.innerHTML = this._innerHTML;
				};
		}

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

				context.ExternalContext_token_set_property(token, "innerHTML", value);
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

		protected override void INode_removeChild(INode child)
		{
			if (token == null)
				throw new Exception("token");

			var childelement = child as IHTMLElement;
			if (childelement != null)
			{
				if (childelement.token == null)
					throw new Exception("childelement.token");

				this.context.ExternalContext_token_call_token(this.token, "removeChild", childelement.token);

				return;
			}

			throw new Exception("INode_removeChild failed");
		}

		public event Action onclick
		{
			add
			{
				if (this.context == null)
					throw new ArgumentNullException("context");

				if (this.token == null)
					throw new ArgumentNullException("token");

				var FlashToken = this.context.ToExternal(
					delegate
					{
						value();
					}
				);

				// now we need to bind elemen.onclick...
				this.context.ExternalContext_token_add_event(this.token, "click", this.context.Element.id, FlashToken);
			}
			remove
			{
				throw new NotSupportedException("cannot remove remote events");
			}
		}
	}
}
