using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace TestScriptSourceMerge
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
            //h.Context.Request.Headers.AllKeys.WithEach(
            //    Key =>
            //    {
            //        Console.WriteLine(Key + ": " + h.Context.Request.Headers[Key]);
            //    }
            //);



            //if (h.Context.Request.Path == "/Other.js")
            //{
            //    h.Context.Response.ContentType = "text/javascript";

            //    foreach (var r in Other.References)
            //    {
            //        h.Context.Response.WriteFile(r.AssemblyFile + ".js");
            //    }

            //    h.CompleteRequest();
            //    return;
            //}



            Action<string, string, WebServiceScriptApplication> y =
                (Alias1, Alias2, Other) =>
                {
                    if (h.Context.Request.Path != Alias1)
                        if (h.Context.Request.Path != Alias2)
                            return;

                    var Accept = h.Context.Request.Headers["Accept"];

                    Console.WriteLine(Accept);

                    if (Accept.Contains("text/html"))
                    {
                        h.Context.Response.ContentType = "text/html";

                        var xml = XElement.Parse(Other.PageSource);


                        //xml.Element("h3").Value = h.Context.Request.UserAgent;
                        //xml.Element("h3").Value = h.Context.Request.Headers["Accept"];

                        //h.Context.Response.Write(
                        //          "<script type='text/xml' class='" + Other.TypeName + "'></script>"
                        //      );



                        xml.Add(
                            new XElement("script",
                            //new XAttribute("type", "text/javascript"),
                            //new XAttribute("src", "Other.js"),
                                new XAttribute("src", Alias1),
                                " "
                            )
                        );

                        //h.Context.Response.Write(
                        //    "<script src='" + r.AssemblyFile + ".js'></script>"
                        //);


                        h.Context.Response.Write(xml.ToString());

                        h.CompleteRequest();
                    }
                    else
                    {
                        var Referer = new Uri(h.Context.Request.Headers["Referer"]);

                        if (Accept.Contains("*/*"))
                            if (Referer.PathAndQuery == Alias1)
                            {

                                h.Context.Response.ContentType = "text/javascript";

                                h.Context.Response.Write("/* jit recompile */");

                                foreach (var r in Other.References)
                                {
                                    h.Context.Response.WriteFile(r.AssemblyFile + ".js");
                                }

                                h.CompleteRequest();
                                return;
                            }
                    }
                };

            y("/o", "/o", h.Applications[1]);

            y("/", "/default.htm", h.Applications[0]);
        }
    }
}
