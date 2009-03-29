using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public class IHTMLTextArea : IHTMLElement
	{
		// http://www.w3schools.com/tags/tag_textarea.asp

		public IHTMLTextArea()
		{
			this.tag = "textarea";

			this.__value = new ExternalContext.Token.Property(this.Token, "value");
		}


		internal readonly ExternalContext.Token.Property __value;
		public string value
		{
			set
			{
				this.__value.PropertyValue = value;
			}
		}
	}
}
