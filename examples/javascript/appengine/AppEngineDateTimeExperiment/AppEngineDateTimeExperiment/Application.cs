using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AppEngineDateTimeExperiment;
using AppEngineDateTimeExperiment.Design;
using AppEngineDateTimeExperiment.HTML.Pages;

namespace AppEngineDateTimeExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            {
                var now = DateTime.Now;
                var utc = now.ToUniversalTime();
                var loc = utc.ToLocalTime();

                var z = TimeZone.CurrentTimeZone.GetUtcOffset(now);

                new IHTMLPre {
                new {
                    z.Ticks, z, utc, loc
                }
            }.AttachToDocument();


                new IHTMLPre {
                new {
                   now, now.Kind, DateTimeKind.Unspecified
                }
            }.AttachToDocument();

                new IHTMLPre {
                new {
                   utc, utc.Kind, DateTimeKind.Utc
                }
            }.AttachToDocument();

                new IHTMLPre {
                new {
                   loc, loc.Kind, DateTimeKind.Local
                }
            }.AttachToDocument();
            }

            this.WebMethod2(
                DateTime.Now
                ,
                value =>
                {
                    new IHTMLPre { new { value } }.AttachToDocument();
                }
            ).ContinueWithResult(
                utc =>
                {
                    new IHTMLPre { new { utc, utc.Kind } }.AttachToDocument();

                    var loc = utc.ToLocalTime();

                    new IHTMLPre { new { loc, loc.Kind } }.AttachToDocument();
                }
            );

        }

    }
}
