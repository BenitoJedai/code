using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace FakeWindowsLoginExperiment
{
    public partial class ApplicationWebService
    {
        // FakeLogin
        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            if (h.Context.Request.Path == "/jsc")
            {
                h.Diagnostics();
                h.CompleteRequest();

                return;
            }

            var apps = new
            {
                Application = h.Applications.FirstOrDefault(k => k.TypeName == "Application"),
                FakeLogin = h.Applications.FirstOrDefault(k => k.TypeName == "FakeLogin"),
                FakeLoginScreen = h.Applications.FirstOrDefault(k => k.TypeName == "FakeLoginScreen"),
            };

            var app = apps.FakeLogin;

            if (h.Context.Request.Path == "/" + apps.FakeLoginScreen.TypeName)
                app = apps.FakeLoginScreen;

            if (h.Context.Request.Path == "/" + apps.FakeLoginScreen.TypeName + "/view-source")
                app = apps.FakeLoginScreen;


            if (h.Context.Request.Path == "/" + apps.Application.TypeName)
                app = apps.Application;

            if (h.Context.Request.Path == "/" + apps.Application.TypeName + "/view-source")
                app = apps.Application;


            var __explicit = "/" + app.TypeName + "/view-source";


            #region IsDefaultPath
            if (h.IsDefaultPath || h.Context.Request.Path == "/" + app.TypeName)
            {
          

                {
                    var c = h.Context.Request.Cookies["FakeLoginScreen.Delay"];

                    if (c != null)
                    {

                        Console.WriteLine("FakeLoginScreen.Delay");
                        c.Expires = DateTime.Now;
                        c.Value = "";

                        h.Context.Response.SetCookie(c);

                        Thread.Sleep(1000);
                    }
                }

                {
                    var c = h.Context.Request.Cookies["FakeLoginScreen.NoContent"];

                    if (c != null)
                    {

                        Console.WriteLine("FakeLoginScreen.NoContent");
                        c.Expires = DateTime.Now;
                        c.Value = "";

                        h.Context.Response.SetCookie(c);

                        // http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html
                        h.Context.Response.StatusCode = 204;
                        h.CompleteRequest();
                        return;
                    }
                }

                h.Context.Response.ContentType = "text/html";

                var xml = XElement.Parse(app.PageSource);

                var src = __explicit;

                //if (HostUri.Host == app.domain)
                //    src = "/view-source";



                xml.Add(
                    new XElement("script",
                        new XAttribute("src", src),

                        // android otherwise closes the tag?
                        " "
                    )
                );

                //h.Context.Response.Write("<!-- " + new { Referer, HostUri, app.domain } + " -->\r\n");


                h.Context.Response.Write(xml.ToString());

                h.CompleteRequest();
                return;
            }
            #endregion


            #region /view-source
            var IsViewSource = h.Context.Request.Path == "/view-source";


            if (h.Context.Request.Path == __explicit)
                IsViewSource = true;

            if (IsViewSource)
            {
                h.Context.Response.ContentType = "text/javascript";


                // http://www.webscalingblog.com/performance/caching-http-headers-cache-control-max-age.html
                // this will break if decision was based on referal. should use redirect instead?
                h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");


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
        }
    }
}
