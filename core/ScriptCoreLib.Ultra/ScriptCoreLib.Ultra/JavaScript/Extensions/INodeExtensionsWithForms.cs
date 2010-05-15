using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Windows.Forms;

namespace ScriptCoreLib.JavaScript.Extensions
{
	public static class INodeExtensionsWithForms
	{
		public static void ReplaceWith(this INode e, UserControl value)
		{
			var c = value.GetHTMLTargetContainer();
			// should we do it here? :)
			c.style.display = IStyle.DisplayEnum.inline_block;
			e.ReplaceWith(c);
		}
	}
}
