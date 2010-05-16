using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using java.applet;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;

namespace ScriptCoreLib.JavaScript.Extensions
{
	public static class SpriteExtensions
	{
		public static IHTMLElement AttachSpriteToDocument(this Sprite e)
		{
			return e.AttachSpriteTo(Native.Document.body);
		}

		public static IHTMLElement ToHTMLElement(this Sprite e)
		{
			// at the moment the .castclass opcode will be translated only within
			//  rewriteable assemblies

			var i = (IHTMLElement)(object)e;

			return i;
		}

	

		public static IHTMLElement AttachSpriteTo(this Sprite e, INode parent)
		{
			var i = e.ToHTMLElement();

			parent.appendChild(i);

			return i;
		}

		public static void ToTransparentSprite(this Sprite s)
		{
			var x = s.ToHTMLElement();

			var p = x.parentNode;
			if (p != null)
			{
				// if we continue, element will be reloaded!
				return;
			}

			x.setAttribute("wmode", "transparent");

			
		}


		public static void ReplaceWith(this INode e, Sprite value)
		{
			var c = value.ToHTMLElement();
			// should we do it here? :)
			c.style.display = IStyle.DisplayEnum.inline_block;
			e.ReplaceWith(c);
		}
	}
}
