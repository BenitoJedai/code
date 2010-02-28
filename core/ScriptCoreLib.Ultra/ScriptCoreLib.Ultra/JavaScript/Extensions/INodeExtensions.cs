using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Extensions
{
	public static class INodeExtensions
	{
		public static void ReplaceWith(this INode e, INode value)
		{
			// http://msdn.microsoft.com/en-us/library/system.xml.linq.xnode.replacewith.aspx

			if (e.parentNode == null)
				return;

			e.parentNode.replaceChild(value, e);
		}
	}
}
