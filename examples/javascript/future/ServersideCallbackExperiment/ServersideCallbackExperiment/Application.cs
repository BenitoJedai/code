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
using ServersideCallbackExperiment.Design;
using ServersideCallbackExperiment.HTML.Pages;

namespace ServersideCallbackExperiment
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
            @"Hello world".ToDocumentTitle();
            

            // this wont compile just yet
            service.WebMethod2(
                @"A string from JavaScript.",
                (value, secret) =>
                {
                    // we are also talking to the serverside class
                    // this means this part does not have to be in javascript
                    secret.x++;

                    // we are using javascript api.
                    value.ToDocumentTitle();
                }
            );
        }

    }
}
