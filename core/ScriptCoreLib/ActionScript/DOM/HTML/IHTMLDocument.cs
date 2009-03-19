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

			createElement(n);

			return n;
		}

		internal void createElement(IHTMLElement n)
		{
			if (n == null)
				throw new Exception("n");

			if (this.context == null)
				throw new Exception("context");

			n.context = this.context;
			n.token = this.context.CreateToken();

			context.ExternalContext_IHTMLDocument_createElement(n.tag, n.token);

			// update properties
			if (n._innerHTML != null)
				n.innerHTML = n._innerHTML;

			if (!string.IsNullOrEmpty(n.id))
				context.SetGlobalPropertyString(n.token, "id", n.id);
		}

		IHTMLElement _body;

		public IHTMLElement body
		{
			get
			{
				if (_body == null)
				{
					_body = new IHTMLElement();

					if (this.context != null)
					{
						_body.context = context;
						_body.token = context.CreateToken();

						context.ExternalContext_IHTMLDocument_get_body(_body.token);
					}
				}

				return _body;
			}
		}
	}
}
