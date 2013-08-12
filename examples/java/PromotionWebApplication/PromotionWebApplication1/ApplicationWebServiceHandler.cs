using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Xml.Linq;
using System.Web;
using System.Collections.Specialized;

namespace PromotionWebApplication1
{
    static class X
    {
        public static string GetHeader(this HttpRequest r, string k)
        {
            return r.GetHeader0(k);
        }

        public static string GetHeader0(this HttpRequest r, string k)
        {
            //Caused by: java.lang.NoSuchMethodError: ScriptCoreLibJava.BCLImplementation.System.Web.__HttpRequest.get_Headers()LScriptCoreLib/Shared/BCLImplementation/System/Collections/Specialized/__NameValueCollection;
            //    at PromotionWebApplication1.X.GetHeader0(X.java:24)
            Console.WriteLine("before the problem...");
            Console.WriteLine("r: " + r);

            var t = r.GetType();
            Console.WriteLine("t: " + t);

            var m = t.GetMethods();


            foreach (var item in m)
            {
                Console.WriteLine("m: " + item);
            }

            //before the problem...
            //r: ScriptCoreLibJava.BCLImplementation.System.Web.__HttpRequest@578dfb
            //t: ScriptCoreLibJava.BCLImplementation.System.__Type@12d8ecd
            //m: java.lang.String get_Path()
            //m: java.lang.String get_HttpMethod()
            //m: ScriptCoreLibJava.BCLImplementation.System.Collections.Specialized.__NameValueCollection get_Form()
            //m: void InitializeForm()
            //m: ScriptCoreLibJava.BCLImplementation.System.Collections.Specialized.__NameValueCollection get_QueryString()
            //m: ScriptCoreLibJava.BCLImplementation.System.Collections.Specialized.__NameValueCollection get_Headers()

            // what the fck? is there a OS issue? java version issue? long name issue?
            var h = r.Headers;

            return h.GetHeader1(k);
        }

        public static string GetHeader1(this NameValueCollection r, string k)
        {
            return r[k];
        }
    }

    //class ApplicationWebServiceHandler
    public sealed partial class ApplicationWebService
    {
#if DEBUG
        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            //            Content-Length:0
            //Content-Type:text/html
            //Date:Sat, 29 Dec 2012 12:42:05 GMT
            //Server:Google Frontend

            // http://blog.restphone.com/2011/04/app-engine-debug-project-gets.html
            //Caused by: java.lang.NoSuchMethodError: ScriptCoreLibJava.BCLImplementation.System.Web.__HttpRequest.get_Headers()LScriptCoreLib/Shared/BCLImplementation/System/Collections/Specialized/__NameValueCollection;
            //    at PromotionWebApplication1.ApplicationWebService___c__DisplayClass3._Handler_b__0(ApplicationWebService___c__DisplayClass3.java:46)
            //    ... 37 more
            // }

            if (h.Context.Request.Path == "/jsc")
            {
                h.Diagnostics();
                return;
            }

            if (h.Context.Request.Path == "/xxx")
            {
                h.Context.Response.Write("go away!");
                h.CompleteRequest();
                return;
            }

            try
            {
                Action foo = delegate
                {
                    var Referer = h.Context.Request.GetHeader("Referer");
                    if (Referer == null)
                        Referer = "any";

                    var HostUri = new
                    {
                        Host = h.Context.Request.GetHeader("Host").TakeUntilIfAny(":"),
                        Port = h.Context.Request.GetHeader("Host").SkipUntilIfAny(":")
                    };

                    var app = new { domain = "www.jsc-solutions.net", local = "127.0.0.1", referer = "", client = h.Applications.FirstOrDefault(k => k.TypeName == "Application") };

                    h.Context.Response.AddHeader("X-Trace", new { Referer, HostUri, app.domain } + "");

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

                    //#region /view-source
                    //            var IsViewSource = h.Context.Request.Path == "/view-source";

                    //            var __explicit = "/" + app.domain + "/view-source";

                    //            if (h.Context.Request.Path == __explicit)
                    //                IsViewSource = true;

                    //            if (IsViewSource)
                    //            {
                    //                h.Context.Response.ContentType = "text/javascript";


                    //                // http://www.webscalingblog.com/performance/caching-http-headers-cache-control-max-age.html
                    //                // this will break if decision was based on referal. should use redirect instead?
                    //                h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");


                    //                // Accept-Encoding: gzip,deflate,sdch
                    //                foreach (var item in app.client.References)
                    //                {
                    //                    h.Context.Response.WriteFile("" + item.AssemblyFile + ".js");
                    //                }

                    //                h.CompleteRequest();
                    //                return;
                    //            }
                    //            #endregion

                    //            if (h.IsDefaultPath)
                    //            {
                    //                h.Context.Response.ContentType = "text/html";

                    //                var xml = XElement.Parse(app.client.PageSource);

                    //                var src = __explicit;

                    //                if (HostUri.Host == app.domain)
                    //                    src = "/view-source";



                    //                xml.Add(
                    //                    new XElement("script",
                    //                        new XAttribute("src", src),

                    //                        // android otherwise closes the tag?
                    //                        " "
                    //                    )
                    //                );



                    //                h.Context.Response.Write(xml.ToString());

                    //                h.CompleteRequest();
                    //            }

                };


                // woraround return support inside try block
                foo();
            }
            catch (Exception ex)
            {
                h.Context.Response.Write("yikes! i did something stupid. " + new { ex.Message, ex.StackTrace });
                h.CompleteRequest();
            }
        }
#endif
    }
}
