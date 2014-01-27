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
using TestImplicitTimelineRecordingEvents;
using TestImplicitTimelineRecordingEvents.Design;
using TestImplicitTimelineRecordingEvents.HTML.Pages;

namespace TestImplicitTimelineRecordingEvents
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
            new IHTMLButton { 
                "Note: The console.timeStamp() method only functions while a Timeline recording is in progress."
            }.AttachToDocument().WhenClicked(
                async button =>
                {
                    button.innerText = "event: click!";

                    Console.WriteLine("event: click!");

                    // wont work if button is disabled
                    button.disabled = false;
                    await button.async.onmouseout;

                    button.innerText = "event: onmouseout!";

                    Console.WriteLine("event: onmouseout!");
                }
            );

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            this.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
