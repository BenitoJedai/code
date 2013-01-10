using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestSwitch.Design;
using TestSwitch.HTML.Pages;

namespace TestSwitch
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            page.ColorBySwitchDoneInBrowser.onclick +=
                delegate
                {
                    page.ColorBySwitchDoneInBrowser.style.backgroundColor =
                        X.GetColor(1000);

                };

        }

    }

    static class X
    {
        // this will cause jsc to break!
        public static string GetColor(int u)
        {
            var value = "";

            switch (u)
            {
                case -1: value = "red"; break;
                case 1000: value = "green"; break;
                case 0x1000: value = "blue"; break;
                case 0x1001: value = "yellow"; break;
                case 0x1002: value = "purple"; break;

                case 0x2000: value = ""; break;
                case 0x2001: value = ""; break;
                case 0x2002: value = ""; break;
                case 0x2003: value = ""; break;
                case 0x2004: value = ""; break;

                default: value = "transparent"; break;
            }

            return value;
        }
    }
}
