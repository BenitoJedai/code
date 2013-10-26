using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ColorDisco;
using ColorDisco.Design;
using ColorDisco.HTML.Pages;

namespace ColorDisco
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

            go();
        }

        public async void go()
        {
            //transition: background-color 100ms linear;
            Native.document.body.style.transition = "background-color 100ms linear";

            while (true)
            {
                await yield();

                Native.document.title = backgroundColor;

                Native.document.body.style.backgroundColor = backgroundColor;

                //IStyleSheet.Default["body"].style.backgroundColor = backgroundColor;

                // tail?

                await Task.Delay(333);
            }

        }
    }
}
