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

namespace Ultra3
{
	public static class UltraApplicationExtensions
	{
		public static IHTMLDiv ToWarningColor(this IHTMLDiv e)
		{
			e.style.color = JSColor.Red;

			return e;
		}

		public static void AttachSpriteToDocument(this Sprite e)
		{
			e.AttachSpriteTo(Native.Document.body);
		}

		public static void AttachSpriteTo(this Sprite e, INode parent)
		{
			// at the moment the .castclass opcode will be translated only within
			// this assembly

			var i = (IHTMLElement)(object)e;

			parent.appendChild(i);

		}


		public static void AttachAppletToDocument(this Applet e)
		{
			e.AttachAppletTo(Native.Document.body);
		}

		public static void AttachAppletTo(this Applet e, INode parent)
		{
			// at the moment the .castclass opcode will be translated only within
			// this assembly

			var i = (IHTMLElement)(object)e;

			parent.appendChild(i);

		}
	}

}
