using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using java.applet;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.Runtime;

namespace Ultra2
{
	public static class UltraApplicationExtensions
	{
		public static IHTMLDiv ToWarningColor(this IHTMLDiv e)
		{
			e.style.color = JSColor.Red;

			return e;
		}

		public static void AttachToDocument(this Sprite e)
		{
			e.AttachTo(Native.Document.body);
		}

		public static void AttachTo(this Sprite e, INode parent)
		{
			// at the moment the .castclass opcode will be translated only within
			// this assembly

			var i = (IHTMLElement)(object)e;

			parent.appendChild(i);

		}


		public static void AttachToDocument(this Applet e)
		{
			e.AttachTo(Native.Document.body);
		}

		public static void AttachTo(this Applet e, INode parent)
		{
			// at the moment the .castclass opcode will be translated only within
			// this assembly

			var i = (IHTMLElement)(object)e;

			parent.appendChild(i);

		}
	}

}
