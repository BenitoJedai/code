using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestToStringCurrencyFormat;
using TestToStringCurrencyFormat.Design;
using TestToStringCurrencyFormat.HTML.Pages;

namespace TestToStringCurrencyFormat
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IHTMLPre { new { a = tostr(123453, "0.00") } }.AttachToDocument();
            new IHTMLPre { new { a = tostr(12345309, "0.00") } }.AttachToDocument();
            new IHTMLPre { new { a = 123453L.ToString("0.00") } }.AttachToDocument();
            new IHTMLPre { new { a = 12345309L.ToString("0.00") } }.AttachToDocument();
            new IHTMLPre { new { a = 123453.ToString("0.00") } }.AttachToDocument();
            new IHTMLPre { new { a = 12345309.ToString("0.00") } }.AttachToDocument();

        }

        public string tostr(long s, string format)
        {
            if (format == "0.00")
            {
                var cent = s % 100;
                var t = s - cent;
                var to = t / 100;
                var total = to + "." + cent.ToString().PadLeft(2, '0');
                return total;
            }
            return s.ToString();
        }

    }
}
