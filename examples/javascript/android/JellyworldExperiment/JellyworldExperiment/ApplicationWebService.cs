using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace JellyworldExperiment
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


        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            if (h.Context.Request.Path == "/jsc")
            {
                h.Diagnostics();
                h.CompleteRequest();

                return;
            }

            var apps = new Dictionary<string, string> 
            {
                {"", "Application"},
                {"HardwareDetection", "Application_HardwareDetection"}
            };

            var appname = apps[""];

            var kk = h.Context.Request.Path.SkipUntilIfAny("/").TakeUntilIfAny("/view-source");

            if (apps.ContainsKey(kk))
                appname = apps[kk];

            var app = h.Applications.Single(k => k.TypeName == appname);

            #region /view-source

            var IsViewSource = h.Context.Request.Path.EndsWith("/view-source");
            if (IsViewSource)
            {
                h.Context.Response.ContentType = "text/javascript";

                // Accept-Encoding: gzip,deflate,sdch
                foreach (var item in app.References)
                {
                    h.Context.Response.Write("/* " + new { item.AssemblyFile, bytes = 1 } + " */\r\n");
                }

                foreach (var item in app.References)
                {
                    // asp.net needs absolute paths
                    h.Context.Response.WriteFile("/" + item.AssemblyFile + ".js");
                }


                h.CompleteRequest();
                return;
            }
            #endregion

            #region text/html
            h.Context.Response.ContentType = "text/html";

            var xml = XElement.Parse(app.PageSource);

            var src = h.Context.Request.Path + "/view-source";

            xml.Add(
                new XElement("script",
                    new XAttribute("src", src),

                    // android otherwise closes the tag?
                    " "
                )
            );

            h.Context.Response.Write(xml.ToString());

            h.CompleteRequest();
            #endregion

        }
    }
}
