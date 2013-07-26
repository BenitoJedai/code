using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FindWebApplicationIcon
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
            y(Preview);
            y(PreviewBase64String);
        }

        static string Preview;
        static string PreviewBase64String;

        public void Handler(WebServiceHandler h)
        {
            // { TypeFullName = FindWebApplicationIcon.Application }

            if (ApplicationWebService.Preview == null)
            {
                ApplicationWebService.Preview = "";

                var app = h.Applications.First();

                var a = new { app.TypeFullName, app.TypeName, Namespace = app.TypeFullName.TakeUntilLastOrEmpty("." + app.TypeName) };

                // file: assets/FindWebApplicationIcon/Preview.png size: 7806

                var Preview = "assets/" + a.Namespace + "/Preview.png";

                if (File.Exists(Preview))
                {
                    ApplicationWebService.Preview = Preview;
                    ApplicationWebService.PreviewBase64String =
                        "data:image/png;base64," +

                        Convert.ToBase64String(
                            File.ReadAllBytes(Preview)
                        );
                }

                Console.WriteLine(new { a, Preview });
            }

        }
    }
}
