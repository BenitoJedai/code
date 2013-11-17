using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace CSS3DMeetsFormsWithWebGL
{



    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript. JSC supports string data type for all platforms.</param>
        /// <param name="y">A callback to javascript. In the future all platforms will allow Action&lt;XElementConvertable&gt; delegates.</param>
        public void WebMethod2(string e, StringAction y)
        {
            // Send it back to the caller.
            y(e);
        }

        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            if (h.Context.Request.Path == "/jsc")
            {
                h.Diagnostics();
                return;
            }


            var HostUri = new
            {
                Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                Port = h.Context.Request.Headers["Host"].SkipUntilIfAny(":")
            };

            var apps = new[]
            {
                new { path = "/WebGLLesson10", client =  h.Applications.FirstOrDefault(k => k.TypeName == "__WebGLLesson10Application") },
                new { path = "/WebGLSpiral", client =  h.Applications.FirstOrDefault(k => k.TypeName == "__WebGLSpiral") },
                new { path = "/ImpAdventures", client =  h.Applications.FirstOrDefault(k => k.TypeName == "__ImpAdventures") },
                new { path = "/IsometricTycoonViewWithToolbar", client =  h.Applications.FirstOrDefault(k => k.TypeName == "__IsometricTycoonViewWithToolbar") },
                new { path = "/McKrackenFirstRoom", client =  h.Applications.FirstOrDefault(k => k.TypeName == "__McKrackenFirstRoom") },
                //new { path = "/AvalonUgh", client =  h.Applications.FirstOrDefault(k => k.TypeName == "__AvalonUgh") },
                //new { path = "/JavaDosBoxQuakeBeta", client =  h.Applications.FirstOrDefault(k => k.TypeName == "__JavaDosBoxQuakeBeta") },
                //new { path = "/Boing4KTemplate", client =  h.Applications.FirstOrDefault(k => k.TypeName == "__Boing4KTemplate") },
                //new { path = "/RayCasterApplet", client =  h.Applications.FirstOrDefault(k => k.TypeName == "__RayCasterApplet") },
                //new { path = "/FlashCamera", client =  h.Applications.FirstOrDefault(k => k.TypeName == "__FlashCamera") },

                new { path = "", client = h.Applications.FirstOrDefault(k => k.TypeName =="Application") }
            };

            #region app
            var app = apps.FirstOrDefault(
              k =>
              {
                  if (k.path == h.Context.Request.Path)
                      return true;

                  if (h.Context.Request.Path == k.path + "/view-source")
                      return true;

                  if (h.Context.Request.Path.StartsWith(k.path))
                      return true;

                  // default
                  if (k.path == "")
                      return true;

                  return false;
              }
            );
            #endregion


            #region /view-source
            if (h.Context.Request.Path == app.path + "/view-source")
            {
                h.Context.Response.ContentType = "text/javascript";

                //// Accept-Encoding: gzip,deflate,sdch
                //foreach (var item in app.client.References)
                //{
                //    h.Context.Response.Write("/* " + new { item.AssemblyFile, bytes = 1 } + " */\r\n");
                //}

                //foreach (var item in app.client.References)
                //{
                //    // asp.net needs absolute paths
                //    //h.Context.Response.WriteFile("/" + item.AssemblyFile + ".js");

                //    // what if it does not exist?
                //    h.Context.Response.WriteFile(item.AssemblyFile + ".js");
                //}

                h.WriteSource(app.client);
                h.CompleteRequest();
                return;
            }
            #endregion

            if (app.client != null)
            {

                h.Context.Response.ContentType = "text/html";

                var xml = XElement.Parse(app.client.PageSource);

                xml.Add(
                     new XElement("script",
                         new XAttribute("src", app.path + "/view-source"),

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
