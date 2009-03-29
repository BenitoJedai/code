using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public abstract class IHTMLElement : INode
	{
		public readonly ExternalContext.Token Token;



		public string id { get; set; }


		public IHTMLElement()
		{
			this.Token = new ExternalContext.Token();
			this.Token.RequestToken =
				delegate
				{
					if (string.IsNullOrEmpty(this.id))
						throw new Exception("id is missing");

					this.Token.TokenValue = this.Token.Context.CreateToken();
					this.Token.Context.ExternalContext_token_set_getElementById(this.Token.TokenValue, this.id);
				};

			this.__innerHTML = new ExternalContext.Token.Property(this.Token, "innerHTML");
			this.__title = new ExternalContext.Token.Property(this.Token, "title");
		}

		IHTMLDocument _ownerDocument;
		public IHTMLDocument ownerDocument
		{
			get
			{
				if (_ownerDocument == null)
				{
					_ownerDocument = new IHTMLDocument { };
					_ownerDocument.context = this.Token.Context;
				}

				return _ownerDocument;
			}
		}

		public string innerHTML
		{
			set
			{
				this.__innerHTML.PropertyValue = value;
			}
		}
		internal readonly ExternalContext.Token.Property __innerHTML;

		/// <summary>
		/// Used to give specific elements a title which may appear as a tooltip in some browsers when the mouse is held at or near the element. 
		/// </summary>
		public string title
		{
			set
			{
				this.__title.PropertyValue = value;
			}
		}
		internal readonly ExternalContext.Token.Property __title;


		protected override void INode_appendChild(INode child)
		{
			var childelement = child as IHTMLElement;
			if (childelement != null)
			{
				if (this.Token.TokenValue != null)
				{
					if (this.Token.Context != null)
					{
						if (childelement.Token.Context == null)
							this.ownerDocument.createElement(childelement);

						this.Token.Context.ExternalContext_IHTMLElement_appendChild(this.Token.TokenValue, childelement.Token.TokenValue);
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
			if (this.Token.TokenValue == null)
				throw new Exception("token");

			var childelement = child as IHTMLElement;
			if (childelement != null)
			{
				if (childelement.Token.TokenValue == null)
					throw new Exception("childelement.token");

				this.Token.Context.ExternalContext_token_call_token(this.Token.TokenValue, "removeChild", childelement.Token.TokenValue);

				return;
			}

			throw new Exception("INode_removeChild failed");
		}

		public event Action onclick
		{
			add
			{
				if (this.Token.Context == null)
					throw new ArgumentNullException("context");

				if (this.Token.TokenValue == null)
					throw new ArgumentNullException("token");

				var FlashToken = this.Token.Context.ToExternal(
					delegate
					{
						value();
					}
				);

				// now we need to bind elemen.onclick...
				this.Token.Context.ExternalContext_token_add_event(this.Token.TokenValue, "click", this.Token.Context.Element.id, FlashToken);
			}
			remove
			{
				throw new NotSupportedException("cannot remove remote events");
			}
		}

		IHTMLStyle _style;
		public IHTMLStyle style
		{
			get
			{
				if (this._style == null)
				{
					this._style = new IHTMLStyle();
					this._style.Token.Context = this.Token.Context;
					this._style.Token.TokenValue = this.Token.Context.CreateToken();

					this._style.Token.Context.ExternalContext_let_token_get_property(this._style.Token.TokenValue, this.Token.TokenValue, "style");
				}

				return this._style;
			}
		}
	}
}
