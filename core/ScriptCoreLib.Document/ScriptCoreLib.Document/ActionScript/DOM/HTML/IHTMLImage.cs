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

			InitializeLateBinding();
		}

		private void InitializeLateBinding()
		{
			this.tokenChanged +=
				 delegate
				 {
					 // update properties
					 if (this._src != null)
						 this.src = this._src;
				 };
		}

		internal string _src;
		public string src
		{
			set
			{
				if (context == null)
				{
					var v = value;
					_src = v;
					return;
				}

				if (token == null)
					throw new Exception("token");

				context.ExternalContext_token_set_property(token, "src", value);
			}
		}
	}
}
