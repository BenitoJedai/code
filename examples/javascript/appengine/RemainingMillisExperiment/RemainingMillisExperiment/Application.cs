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
using RemainingMillisExperiment.Design;
using RemainingMillisExperiment.HTML.Pages;
using System.Threading.Tasks;


namespace RemainingMillisExperiment
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
            Native.document.title = this.title;

            // Send data from JavaScript to the server tier

            this.title += ".";
            this.counter++;

            this.RemainingMillis.ContinueWithResult(
                RemainingMillis =>
                {
                    Native.document.title = this.title;
                    page.Content.innerText = new { RemainingMillis }.ToString();

                }
            );


        }

    }
}
