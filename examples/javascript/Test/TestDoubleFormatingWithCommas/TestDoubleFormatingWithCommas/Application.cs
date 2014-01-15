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
using TestDoubleFormatingWithCommas;
using TestDoubleFormatingWithCommas.Design;
using TestDoubleFormatingWithCommas.HTML.Pages;

namespace TestDoubleFormatingWithCommas
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
            var s = new IHTMLPre {innerText = 1234567L.ToCustomString()};
            s.AttachTo(page.body);
        }

    }
}

public static class ExtensionMethods
{
    public static string ToCustomString(this long value)
    {
        if (value < 0)
        {
            value = Math.Abs(value);
            var cent = value % 100;
            var t = value - cent;
            var to = t / 100;

            var myNumber = to.ToString();
            var arr = myNumber.ToCharArray();
            var myResult = "";
            for (var i = arr.Length - 1; i >= 0; i--)
            {
                myResult = new string(arr[i], 1) + myResult;
                if ((myNumber.Length - i) % 3 == 0 && i > 0)
                {
                    myResult = "," + myResult;
                }
            }

            var total = "-" + myResult + "." + cent.ToString().PadLeft(2, '0');
            return total;
        }
        else
        {
            var cent = value % 100;
            var t = value - cent;
            var to = t / 100;
            
            var myNumber = to.ToString();
            var arr = myNumber.ToCharArray();
            var myResult = "";
            for (var i = arr.Length - 1; i >= 0; i--)
            {
                myResult = new string(arr[i], 1) + myResult;
                if ((myNumber.Length - i) % 3 == 0 && i > 0)
                {
                    myResult = "," + myResult;
                }
            }

            var total = myResult + "." + cent.ToString().PadLeft(2, '0');
            return total;
        }
    }
}
