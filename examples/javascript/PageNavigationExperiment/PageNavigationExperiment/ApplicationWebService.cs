using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PageNavigationExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
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
            // http://www.google.com/webmasters/tools/richsnippets?q=http%3A%2F%2Feventful.com%2Fsanfrancisco%2Fevents

            Console.WriteLine(
                new
                {
                    h.Context.Request.Path
                }
            );


            // building google sitemap
            if (h.Context.Request.Path == "/ThirdPage.htm"
                || h.Context.Request.Path == "/third-page")
            {

                // 302 Found
                // h.Context.Response.RedirectPermanent("/#/ThirdPage.htm", true);

                var app = h.Applications[0];

                var PageSource = XElement.Parse(app.PageSource);
                var PageSource_body = PageSource.Element("body");

                // chrome will skip body. have to repair on the client
                PageSource_body.Name = "ydob";

                // pre saved prevous state
                var hidden = new XElement("hidden-body", PageSource_body);

                // Duplicate attribute.

                //PageSource_body.Attribute("style").Value += "display: none;";

                hidden.Add(
                    new XAttribute("style", "display: none;")
                );


                // http://stackoverflow.com/questions/17607823/exclude-div-or-string-of-text-from-search-engines
                // https://support.google.com/customsearch/answer/2364585?hl=en
                hidden.Add(
                    new XAttribute("class", "nocontent")
                );

                var ThirdPageSource = XElement.Parse(PageNavigationExperiment.HTML.Pages.ThirdPageSource.Text);
                var ThirdPageSource_body = ThirdPageSource.Element("body");
                //manifest="cache-manifest"

                ThirdPageSource.Add(
                   new XAttribute("manifest", "cache-manifest")
               );


                ThirdPageSource.Add(
                    hidden
                );

                ThirdPageSource.Add(
                 new XElement("script",
                        new XAttribute("src", "view-source"),
                        " "
                    )
                );

                h.Context.Response.Write(
                  "<!DOCTYPE html>"
                );


                h.Context.Response.Write(
                 ThirdPageSource.ToString()
                );

                h.CompleteRequest();
                return;
            }
        }
    }
}
