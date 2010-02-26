using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using java.applet;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.JavaScript.Extensions
{
	public static class AppletExtensions
	{
		

		public static IHTMLElement AttachAppletToDocument(this Applet e)
		{
			return e.AttachAppletTo(Native.Document.body);
		}

		public static IHTMLElement AttachAppletTo(this Applet e, INode parent)
		{
			// at the moment the .castclass opcode will be translated only within
			//  rewriteable assemblies

			var i = (IHTMLElement)(object)e;

			parent.appendChild(i);

			return i;
		}
	}
}
