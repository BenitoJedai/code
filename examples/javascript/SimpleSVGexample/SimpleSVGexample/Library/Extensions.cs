﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;

namespace SimpleSVGexample
{
    public static class Extensions
    {
		public static Action AtInterval(this int ms, Action e)
		{
			var t = new Timer(
				tt =>
				{
					e();
				}, 0, ms);

			return t.Stop;
		}

        public static IHTMLElement AsElement(this string tag)
        {
            return new IHTMLElement(tag);
        }

        public static Action<Action, T> AsDefaultDelegate<T>(this Action<Action, T> e)
        {
            return (h, x) => h();
        }

        public static IHTMLElement AttachTo(this IHTMLElement e, IHTMLElement c)
        {
            c.appendChild(e);

            return e;
        }

        public static IHTMLElement AttachToDocument(this string tag)
        {
            var e = tag.AsElement();

            Native.Document.body.appendChild(e);

            return e;
        }


    }
}
