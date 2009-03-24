using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public class IHTMLStyle
	{
		internal readonly ExternalContext.Token Token = new ExternalContext.Token();

		public IHTMLStyle()
		{
			this.__color = new ExternalContext.Token.Property(this.Token, "color");
			this.__backgroundColor = new ExternalContext.Token.Property(this.Token, "backgroundColor");

		}

		public string color
		{
			set
			{
				this.__color.PropertyValue = value;
			}
		}
		internal readonly ExternalContext.Token.Property __color;

		public string backgroundColor
		{
			set
			{
				this.__backgroundColor.PropertyValue = value;
			}
		}
		internal readonly ExternalContext.Token.Property __backgroundColor;
	}
}
