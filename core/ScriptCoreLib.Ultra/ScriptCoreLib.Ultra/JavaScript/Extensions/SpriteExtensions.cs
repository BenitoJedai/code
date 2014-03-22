﻿extern alias jvm;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using jvm::java.applet;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;

namespace ScriptCoreLib.JavaScript.Extensions
{
	public static class SpriteExtensions
	{
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

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


        // can we convert this to AttachSpriteTo<T> ? :)
		public static IHTMLElement AttachSpriteTo(this Sprite e, INode parent)
		{
			var i = e.ToHTMLElement();

			parent.appendChild(i);

			return i;
		}

        public static IHTMLElement AutoSizeSpriteTo(this Sprite e, IHTMLElement shadow)
        {
            var i = e.ToHTMLElement();

            Action Update =
                 delegate
                 {
                     var w = shadow.scrollWidth;
                     var h = shadow.scrollHeight;

                     i.style.SetSize(w, h);
                 };


            Native.window.onresize +=
                delegate
                {
                    Update();
                };

            Update();


            return i;
        }

        /// <summary>
        ///  Given the architecture of plugins there is no way for the Flash Player to know if 
        ///  Flash content is on a hidden tab or not and disable rendering properly.
        ///  If you use WMODE the Flash Player will continue to suck up CPU cycles as if the
        ///  tab was visible. In addition WMODE is much slower than the normal mode.
        ///  
        /// See also: http://blog.kaourantin.net/?p=77
        /// </summary>
        /// <param name="s"></param>
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


        public static void ReplaceWith(this IHTMLElement e, Sprite value)
		{
			var c = value.ToHTMLElement();
			// should we do it here? :)
			c.style.display = IStyle.DisplayEnum.inline_block;
			e.ReplaceWith(c);
		}
	}
}
