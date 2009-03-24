using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public class IHTMLStyle
	{
		// http://www.w3schools.com/HTMLDOM/prop_style_cursor.asp

		internal readonly ExternalContext.Token Token = new ExternalContext.Token();

		public IHTMLStyle()
		{
			this.__color = new ExternalContext.Token.Property(this.Token, "color");
			this.__cursor = new ExternalContext.Token.Property(this.Token, "cursor");
			this.__backgroundColor = new ExternalContext.Token.Property(this.Token, "backgroundColor");
			this.__textAlign = new ExternalContext.Token.Property(this.Token, "textAlign");

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

		public string cursor
		{
			set { this.__cursor.PropertyValue = value; }
		}
		internal readonly ExternalContext.Token.Property __cursor;


		public string textAlign
		{
			set { this.__textAlign.PropertyValue = value; }
		}
		internal readonly ExternalContext.Token.Property __textAlign;
	}
}
