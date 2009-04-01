using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public sealed class IHTMLImage : IHTMLElement
	{
		// http://www.w3schools.com/tags/tag_img.asp

		public IHTMLImage()
		{
			this.tag = "img";

			this.__src = new ExternalContext.Token.Property(this.Token, "src");
			this.__alt = new ExternalContext.Token.Property(this.Token, "alt");
		}


		internal readonly ExternalContext.Token.Property __src;
		public string src
		{
			set
			{
				this.__src.PropertyValue = value;
			}
		}


		internal readonly ExternalContext.Token.Property __alt;
		public string alt
		{
			set
			{
				this.__alt.PropertyValue = value;
			}
		}
		
	}
}
