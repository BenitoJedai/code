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
using CameraPreviewExperiment;
using CameraPreviewExperiment.Design;
using CameraPreviewExperiment.HTML.Pages;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;

namespace CameraPreviewExperiment
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
            // Send data from JavaScript to the server tier

            new IHTMLButton { innerText = "show me stills" }.AttachToDocument().onclick +=
                delegate
                {
                    service.WebMethod2(
                        @"A string from JavaScript.",
                        value => new IHTMLImage { src = value }.AttachToDocument()
                    );
                };

            new IHTMLButton { innerText = "show me animation" }.AttachToDocument().onclick +=
                delegate
                {
                    var img = new IHTMLImage { }.AttachToDocument();

                    var data = new List<string>();

                    service.WebMethod2(
                        @"A string from JavaScript.",
                        value => data.Add(value)
                    );

                    new Timer(
                        t =>
                        {
                            if (data.Count == 0)
                                return;

                            img.src = data[t.Counter % data.Count];
                        }
                    ).StartInterval(1000 / 15);
                };
        }

    }
}
