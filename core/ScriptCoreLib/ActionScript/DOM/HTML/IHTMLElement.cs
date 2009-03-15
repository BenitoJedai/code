using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public class IHTMLElement
	{
		public string id { get; set; }

		public ExternalContext context { get; set; }

		public IHTMLDocument ownerDocument
		{
			get
			{
				return new IHTMLDocument { context = context };
			}
		}
	}
}
