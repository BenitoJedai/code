using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public class IHTMLAnchor : IHTMLElement
	{
		// http://www.w3schools.com/tags/tag_textarea.asp

		public IHTMLAnchor()
		{
			this.tag = "a";

			this.__href = new ExternalContext.Token.Property(this.Token, "href");
		}


		internal readonly ExternalContext.Token.Property __href;
		public string href
		{
			set
			{
				this.__href.PropertyValue = value;
			}
		}
	}
}
