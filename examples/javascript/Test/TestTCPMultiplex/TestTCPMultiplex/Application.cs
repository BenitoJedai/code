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
using TestTCPMultiplex;
using TestTCPMultiplex.Design;
using TestTCPMultiplex.HTML.Pages;

namespace TestTCPMultiplex
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
            // X:\jsc.svn\examples\java\hybrid\JVMCLRTCPMultiplex\JVMCLRTCPMultiplex\Program.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141018-ssl
            // http://blog.engelke.com/

            new IHTMLPre {
                new
                {
                    Native.document.location.protocol,
                    Native.document.location.host
                }
            }.AttachToDocument();

            new IHTMLAnchor { href = "http://" + Native.document.location.host, innerText = "http" }.AttachToDocument();
            new IHTMLAnchor { href = "https://" + Native.document.location.host, innerText = "https" }.AttachToDocument();

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            this.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
