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

	
	}
}
