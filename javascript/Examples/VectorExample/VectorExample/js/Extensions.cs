using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;

namespace VectorExample.js
{
    [Script]
    public static class Extensions
    {
        public static IHTMLElement AsElement(this string tag)
        {
            return new IHTMLElement(tag);
        }

        public static Action<Action, T> AsDefaultDelegate<T>(this Action<Action, T> e)
        {
            return (h, x) => h();
        }

        public static IHTMLElement AttachToDocument(this string tag)
        {
            var e = tag.AsElement();

            Native.Document.body.appendChild(e);

            return e;
        }
    }
}
