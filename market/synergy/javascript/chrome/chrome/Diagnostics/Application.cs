extern alias xglobal;

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
using xglobal::chromelabs.HTML.Pages;

namespace chromelabs
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
            // Error	1	Ambiguity between 'chrome.webstore' and 'chrome.webstore'	X:\jsc.svn\market\synergy\javascript\chrome\chrome\Application.cs	30	28	chrome
            //chrome.webstore.install(
            //    "",
            //    successCallback: null,
            //    failureCallback: null
            //);

            //Console.WriteLine(new { chrome.app.isInstalled });




            //w.i

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
