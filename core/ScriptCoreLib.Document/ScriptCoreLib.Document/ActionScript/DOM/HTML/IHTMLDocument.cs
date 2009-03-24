using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public class IHTMLDocument
	{
		public ExternalContext context { get; set; }

		public string title
		{
			set
			{
				if (context != null)
				{
					context.SetGlobalPropertyString("document", "title", value);
				}
			}
		}

		public IHTMLElement createElement(string tag)
		{
			var n = default(IHTMLElement);

			if (tag == "div")
				n = new IHTMLDiv();

			if (tag == "span")
				n = new IHTMLSpan();

			if (tag == "button")
				n = new IHTMLButton();

			if (n == null)
				throw new NotImplementedException(tag);

			createElement(n);

			return n;
		}

		internal void createElement(IHTMLElement n)
		{
			if (n == null)
				throw new Exception("n");

			if (this.context == null)
				throw new Exception("context");

			n.Token.Context = this.context;
			var token = this.context.CreateToken();

			context.ExternalContext_IHTMLDocument_createElement(n.tag, token);

			n.Token.TokenValue = token;



			if (!string.IsNullOrEmpty(n.id))
				context.SetGlobalPropertyString(n.Token.TokenValue, "id", n.id);
		}

		IHTMLElement _body;

		public IHTMLElement body
		{
			get
			{
				if (_body == null)
				{
					_body = new IHTMLBody();

					if (this.context != null)
					{
						_body.Token.Context = context;
						_body.Token.TokenValue = context.CreateToken();

						context.ExternalContext_IHTMLDocument_get_body(_body.Token.TokenValue);
					}
				}

				return _body;
			}
		}
	}
}
