using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript
{
	public abstract class UltraComponent 
	{
		// Should we bet on the ComponentModel.Component?
		// http://www.google.com/search?q=UltraComponent



		public object Tag { get; set; }

		public abstract IHTMLElement GetContainer();
	}

	public static class UltraComponentExtensions
	{
		public static void AttachToDocument(UltraComponent c)
		{
			c.GetContainer().AttachToDocument();
		}

		public static void AttachTo(UltraComponent c, INode p)
		{
			c.GetContainer().AttachTo(p);
		}
	}
}
