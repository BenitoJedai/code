using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace TestMultiApplication
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
            if (h.Context.Request.Path == "/Other")
            {
                var Other = h.Applications[1];

                h.Context.Response.ContentType = "text/html";

                var xml = XElement.Parse(Other.PageSource);


                //xml.Element("h3").Value = h.Context.Request.UserAgent;
                xml.Element("h3").Value = h.Context.Request.Headers["User-Agent"];

                //h.Context.Response.Write(
                //          "<script type='text/xml' class='" + Other.TypeName + "'></script>"
                //      );

             
                foreach (var r in Other.References)
                {
                    xml.Add(
                        new XElement("script",
                            new XAttribute("src", r.AssemblyFile + ".js"),
                            
                            // android otherwise closes the tag?
                            " "
                        )
                    );

                    //h.Context.Response.Write(
                    //    "<script src='" + r.AssemblyFile + ".js'></script>"
                    //);
                }

                h.Context.Response.Write(xml.ToString());

                h.CompleteRequest();


            }
        }
    }
}
