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
using System.Diagnostics;

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
            // we are not yet merging correctly.
            // script would be reloaded after await
            //Native.document.body.querySelectorAll("script").WithEach(
            //    x => x.Orphanize()
            //);

            go();
        }

        public async void go()
        {
            Native.document.body.css.style.transition = "background-color 100ms linear";

            this.body = Native.document.body;


            while (true)
            {
                i++;

                await yield();


                await Task.Delay(333);

            }
        }
    }
}
