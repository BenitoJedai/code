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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestWebServiceProgress;
using TestWebServiceProgress.Design;
using TestWebServiceProgress.HTML.Pages;

namespace TestWebServiceProgress
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
            //onopen { url = /xml/WebMethod2 }
            //upload onprogress { url = /xml/WebMethod2, loaded = 42747 }
            //onprogress { url = /xml/WebMethod2, loaded = 8760 }
            //onprogress { url = /xml/WebMethod2, loaded = 42707 }

            IXMLHttpRequestActivity.onopen +=
                x =>
                {
                    var s = Stopwatch.StartNew();

                    new IHTMLPre { "onopen " + new { s.ElapsedMilliseconds, x.url } }.AttachToDocument();

                    // http://stackoverflow.com/questions/76976/how-to-get-progress-from-xmlhttprequest

                    x.request.onprogress +=
                        e =>
                        {
                            new IHTMLPre { "onprogress " + new { s.ElapsedMilliseconds, x.url, e.loaded, e.total, e.lengthComputable } }.AttachToDocument();
                        };

                    x.request.upload.onprogress +=
                        e =>
                        {
                            new IHTMLPre { "upload onprogress " + new { s.ElapsedMilliseconds, x.url, e.loaded, e.total, e.lengthComputable } }.AttachToDocument();
                        };

                };

            //onopen { url = /xml/WebMethod2 }
            //upload onprogress { url = /xml/WebMethod2, loaded = 147456, total = 12266747, lengthComputable = true }
            //upload onprogress { url = /xml/WebMethod2, loaded = 163840, total = 12266747, lengthComputable = true }
            //upload onprogress { url = /xml/WebMethod2, loaded = 163840, total = 12266747, lengthComputable = true }
            //onprogress { url = /xml/WebMethod2, loaded = 0, total = 0, lengthComputable = false }

            //onopen { ElapsedMilliseconds = 1, url = /xml/WebMethod2 }
            //upload onprogress { ElapsedMilliseconds = 81, url = /xml/WebMethod2, loaded = 218537, total = 218537, lengthComputable = true }
            //onprogress { ElapsedMilliseconds = 82, url = /xml/WebMethod2, loaded = 8760, total = 218495, lengthComputable = true }
            //onprogress { ElapsedMilliseconds = 101, url = /xml/WebMethod2, loaded = 218495, total = 218495, lengthComputable = true }

            new IHTMLButton { "do" }.AttachToDocument().WhenClicked(
                delegate
                {
                    this.WebMethod2(
                        @"A string from JavaScript.".PadLeft(163840 * 8),
                        value => value.ToDocumentTitle()
                    );
                }
            );

        }

    }
}
