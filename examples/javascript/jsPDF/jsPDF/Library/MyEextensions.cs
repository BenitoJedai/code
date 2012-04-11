using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;

namespace jsPDF.Library
{
    public class MyEextensions
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
