using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.DOM;

namespace ScriptCoreLib.ActionScript.Extensions
{
	[Script]
	public static class DOMExtensions
	{
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
