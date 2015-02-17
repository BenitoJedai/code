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
using RoosterAudioExample.Design;
using RoosterAudioExample.HTML.Pages;
using RoosterAudioExample.HTML.Audio.FromAssets;
using ScriptCoreLib.JavaScript.Runtime;

namespace RoosterAudioExample
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
        public Application(IDefault a)
        {

            var rooster = new rooster();
            a.WebService_GetTime.onclick +=
                delegate
                {

                    new ApplicationWebService().GetTime("time: ",
                        result =>
                        {
                            new IHTMLPre { innerText = result }.AttachTo(a.WebServiceContainer);

                            try
                            {
                                // are we running HTML5 browser
                                rooster.play();
                                rooster = new rooster();
                            }
                            catch
                            {
                                // no? :)
                            }
                        }
                    );

                };

            a.Inline1.onclick +=
                delegate
                {
                    try
                    {
                        // are we running HTML5 browser
                        rooster.play();
                        rooster = new rooster();
                    }
                    catch
                    {
                        // no? :)
                    }
                };

            a.Inline1.onclick +=
                delegate
                {
                    new Timer(
                        delegate
                        {
                            a.Inline1.style.color = "";
                        }
                    ).StartTimeout(1000);
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
