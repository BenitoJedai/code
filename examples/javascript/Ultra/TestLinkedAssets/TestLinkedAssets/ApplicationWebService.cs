// For more information visit:
// http://studio.jsc-solutions.net/

// View as Visual Basic project
// http://do.jsc-solutions.net/View-as-Visual-Basic-project

// View as Visual FSharp project
// http://do.jsc-solutions.net/View-as-Visual-FSharp-project

using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using TestLinkedAssets.Assets;
using System.IO;

namespace TestLinkedAssets
{
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript</param>
        /// <param name="y">A callback to javascript</param>
        public void WebMethod2(XElement e, Action<XElement> y)
        {
            // Send something back from WebMethod2
            // http://do.jsc-solutions.net/Send-something-back-from-WebMethod2

            e.Element(@"Data").ReplaceAll(@"Data from the web server");
            // Send it to the caller.
            y(e);
        }


        public void Publish(WebServiceHandler h)
        {
            var publish = h.Context.Request.Path.SkipUntilOrEmpty("/publish/");
            var p = new Publish();

            if (p.ContainsKey(publish))
            {
                var f = p[publish];

                var bytes = File.ReadAllBytes(f);

                h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                h.CompleteRequest();
            }
        }
    }
}
