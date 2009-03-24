using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public class IHTMLObject : IHTMLElement
	{
		public IHTMLObject()
		{
			this.tag = "object";

			this.__width = new ExternalContext.Token.Property(this.Token, "width");
			this.__height = new ExternalContext.Token.Property(this.Token, "height");
		
		}

		public int width
		{
			set
			{
				this.__width.PropertyValue = value;

			}
		}
		internal readonly ExternalContext.Token.Property __width;

		public int height
		{
			set
			{
				this.__height.PropertyValue = value;
			}
		}
		internal readonly ExternalContext.Token.Property __height;

	}
}
