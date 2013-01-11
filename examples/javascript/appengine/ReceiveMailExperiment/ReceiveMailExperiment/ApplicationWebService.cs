using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace ReceiveMailExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        public void Handler(WebServiceHandler h)
        {
            // https://developers.google.com/appengine/docs/java/mail/receiving
            // http://localhost:22209/_ah/admin/inboundmail
            // /_ah/mail/

            var prefix = "/_ah/mail/";

            if (h.Context.Request.Path.StartsWith(prefix))
            {
                var to = h.Context.Request.Path.SkipUntilOrEmpty(prefix);

                Console.WriteLine(new { to });

                //h.Context.Request.InputStream

                h.CompleteRequest();
            }
        }
    }
}
