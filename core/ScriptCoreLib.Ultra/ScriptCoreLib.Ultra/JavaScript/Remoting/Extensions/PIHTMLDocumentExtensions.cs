using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.Remoting.Extensions
{
	public static class PIHTMLDocumentExtensions
	{
		public static PHTMLDocument ToProxy(this IHTMLDocument e)
		{
			return (PIHTMLDocument)e;
		}

		public static PHTMLElement ToProxy(this IHTMLElement e)
		{
			return (PIHTMLElement)e;
		}
	}
}
