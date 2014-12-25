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
using TestIScreen;
using TestIScreen.Design;
using TestIScreen.HTML.Pages;

namespace TestIScreen
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
            // can we open two flash windows and have them synced?

            // on screen changed?

            var c = 0;

            new IHTMLPre
            {
                // can we get a service worker
                // to chime in for multiscreen session?
                // can we then test it as a chrome app too?

                // {{ c = 688, width = 1600, height = 900, aspect = 1.7195121951219512, Width = 1128, Height = 656, screenLeft = -1437, screenTop = 286 }}
                // will we know when we are moved onto the other monitor?
                () => new { c = c++,


                              Native.screen.width,
                              Native.screen.height,

                              Native.window.aspect,

                              Native.window.Width,
                              Native.window.Height,

                              // where is this window on current screen?
                              //(Native.window as dynamic).offsetLeft,
                              //(Native.window as dynamic).offsetTop,

                              (Native.window as dynamic).screenLeft,
                              (Native.window as dynamic).screenTop,



                }

            }.AttachToDocument();
        }

    }
}
