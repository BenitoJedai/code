using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.DOM.HTML;

namespace ScriptCoreLib.ActionScript.DOM.Extensions
{
	[Script]
	public static class DOMExtensions
	{
		public static void AttachAsSpan(this string e, ExternalContext c)
		{
			new IHTMLSpan { innerHTML = e }.AttachTo(c);
		}


		public static void AttachAsDiv(this string e, ExternalContext c)
		{
			new IHTMLDiv { innerHTML = e }.AttachTo(c);
		}

		public static T AttachTo<T>(this T e, ExternalContext c)
			where T : INode
		{
			return e.AttachTo(c.Document.body);
		}

		public static T AttachTo<T>(this T e, INode c)
			where T : INode
		{
			c.appendChild(e);

			return e;
		}

	}
}
