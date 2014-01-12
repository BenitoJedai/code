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

            this.title += "client";

            this.counter = 333;

            //Error	2	The call is ambiguous between the following methods or properties: 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButtonExtensions.WhenClicked(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton, System.Action<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton>)' and 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButtonAsyncExtensions.WhenClicked(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton, System.Func<System.Threading.Tasks.Task>)'	X:\jsc.svn\examples\javascript\appengine\RemainingMillisExperiment\RemainingMillisExperiment\Application.cs	41	13	RemainingMillisExperiment


            new IHTMLButton { innerText = "RemainingMillis" }.AttachToDocument().WhenClicked(
                async button =>
                {
                    page.Content = "?";
                    //page.ContentContainer.AsXElement().Element("span").Add(new XAttribute("id", "Content"));

                    await Task.Delay(333);

                    var RemainingMillis = await this.RemainingMillis;

                    Native.document.title = this.title;

                    page.Content = new { RemainingMillis }.ToString();
                    //page.ContentContainer.AsXElement().Element("span").Add(new XAttribute("id", "Content"));
                }
            );

            new IHTMLButton { innerText = "RemainingMillis lazy optimistic" }.AttachToDocument().WhenClicked(
                 delegate
                 {
                     page.Content = "?";
                     //page.ContentContainer.AsXElement().Element("span").Add(new XAttribute("id", "Content"));

                     page.Content = this.RemainingMillisString;
                     //page.ContentContainer.AsXElement().Element("span").Add(new XAttribute("id", "Content"));


                 }
            );

        }

    }
}
