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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestDownloadStringAsync;
using TestDownloadStringAsync.Design;
using TestDownloadStringAsync.HTML.Pages;

namespace TestDownloadStringAsync
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
            new WebClient().With(
                async w =>
                {

                    //w.DownloadStringCompleted +=
                    //    (sender, args) =>
                    //    {

                    //        new IHTMLPre { new { args.Result } }.AttachToDocument();
                    //    };

                    //w.DownloadStringAsync(new Uri("/jsc", UriKind.Relative));

                    var x = await w.DownloadStringTaskAsync("/crossdomain.xml");


                    new IHTMLPre { new { x } }.AttachToDocument();
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
