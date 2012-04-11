using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace jsgif.Library
{
    public static class MyExtensions
    {
        public static void WhenAvailable(this IHTMLScript s, Action h)
        {
            s.onload +=
                delegate
                {
                    h();
                };

            s.AttachToDocument();
        }
    }
}
