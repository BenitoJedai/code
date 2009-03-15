using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public class IHTMLObject : IHTMLElement
	{


		public int width
		{
			set
			{
				if (context != null)
				{
					context.SetElementPropertyString(id, "width", "" + value);
				}
			}
		}

		public int height
		{
			set
			{
				if (context != null)
				{
					context.SetElementPropertyString(id, "height", "" + value);
				}
			}
		}

	}
}
