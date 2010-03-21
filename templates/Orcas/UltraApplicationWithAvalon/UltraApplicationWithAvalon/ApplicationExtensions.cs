using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace UltraApplicationWithAvalon
{
	public static class ApplicationExtensions
	{
		// XElement API style...

		public static IHTMLButton Button(this IHTMLElement e, string text)
		{
			var x = new IHTMLButton { innerText = text };

			x.AttachTo(e);
			
			return x;
		}
	}
}
