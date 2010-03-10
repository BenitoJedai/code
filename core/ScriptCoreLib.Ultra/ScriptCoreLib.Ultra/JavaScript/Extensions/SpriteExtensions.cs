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
	public static class SpriteExtensions
	{
		public static IHTMLElement AttachSpriteToDocument(this Sprite e)
		{
			return e.AttachSpriteTo(Native.Document.body);
		}

		public static IHTMLElement AttachSpriteTo(this Sprite e, INode parent)
		{
			// at the moment the .castclass opcode will be translated only within
			//  rewriteable assemblies

			var i = (IHTMLElement)(object)e;

			parent.appendChild(i);

			return i;
		}

		public static void ToTransparentSprite(this Sprite s)
		{
			var x = (IHTMLEmbed)(object)s;

			x.setAttribute("wmode", "transparent");
		}
	}
}
