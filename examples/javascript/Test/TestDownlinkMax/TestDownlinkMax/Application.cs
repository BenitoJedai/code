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
using TestDownlinkMax;
using TestDownlinkMax.Design;
using TestDownlinkMax.HTML.Pages;

namespace TestDownlinkMax
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
            // http://w3c.github.io/netinfo/
            // http://w3c.github.io/netinfo/

            // {{ type = unknown, downlinkMax = null }}

            new IHTMLPre {
                new {
                        Native.window.navigator.userAgent,

                     Native.window.navigator.connection.type,
                        Native.window.navigator.connection.downlinkMax,

                }
            }.AttachToDocument();


        }

    }
}
