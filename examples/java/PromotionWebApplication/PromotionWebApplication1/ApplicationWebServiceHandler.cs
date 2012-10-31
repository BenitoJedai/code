using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Xml.Linq;

namespace PromotionWebApplication1
{
    //class ApplicationWebServiceHandler
    public sealed partial class ApplicationWebService
    {
        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            if (h.Context.Request.Path == "/jsc")
            {
                h.Diagnostics();
                return;
            }

            var Referer = h.Context.Request.Headers["Referer"];
            if (Referer == null)
                Referer = "any";

            var HostUri = new
            {
                Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                Port = h.Context.Request.Headers["Host"].SkipUntilIfAny(":")
            };




            var app = new { domain = "www.jsc-solutions.net", local = "127.0.0.1", referer = "", client = h.Applications.FirstOrDefault(k => k.TypeName == "Application") };


            //var app = apps.FirstOrDefault(
            //    k =>
            //    {
            //        //http://idea-remixer.tumblr.com/

            //        if (k.referer == Referer)
            //            return true;


            //        // GAE has a different value for referer and port
            //        var r = ("http://" + k.referer + "/");
            //        if (r == Referer)
            //            return true;


            //        if (k.domain == HostUri.Host)
            //            return true;

            //        if (k.local == HostUri.Host)
            //            return true;

            //        if (h.Context.Request.Path == "/" + k.domain)
            //            return true;

            //        if (Referer.EndsWith("/" + k.domain))
            //            return true;

            //        // default
            //        if (k.local == "127.0.0.1")
            //            return true;

            //        return false;
            //    }
            //);

            #region /view-source
            var IsViewSource = h.Context.Request.Path == "/view-source";

            var __explicit = "/" + app.domain + "/view-source";

            if (h.Context.Request.Path == __explicit)
                IsViewSource = true;

            if (IsViewSource)
            {
                h.Context.Response.ContentType = "text/javascript";


                // http://www.webscalingblog.com/performance/caching-http-headers-cache-control-max-age.html
                // this will break if decision was based on referal. should use redirect instead?
                h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");

                h.Context.Response.AddHeader("X-Trace", new { Referer, HostUri, app.domain } + "");

                // Accept-Encoding: gzip,deflate,sdch
                foreach (var item in app.client.References)
                {
                    h.Context.Response.WriteFile("" + item.AssemblyFile + ".js");
                }

                h.CompleteRequest();
                return;
            }
            #endregion

            if (h.IsDefaultPath)
            {
                h.Context.Response.ContentType = "text/html";

                var xml = XElement.Parse(app.client.PageSource);

                var src = __explicit;

                if (HostUri.Host == app.domain)
                    src = "/view-source";



                xml.Add(
                    new XElement("script",
                        new XAttribute("src", src),

                        // android otherwise closes the tag?
                        " "
                    )
                );



                h.Context.Response.Write(xml.ToString());

                h.CompleteRequest();
            }

        }
    }
}
