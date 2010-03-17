using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;

namespace ScriptCoreLib.JavaScript.Remoting.Extensions
{
	public static class PNodeExtensions
	{
		public static void AttachTo(this PNode e, PNode parent)
		{
			parent.appendChild(e);
		}
	}
}
