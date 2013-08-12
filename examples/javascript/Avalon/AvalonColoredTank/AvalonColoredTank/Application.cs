using AvalonColoredTank.Design;
using AvalonColoredTank.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AvalonColoredTank
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationCanvas content = new ApplicationCanvas();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page)
        {
            content.AttachToContainer(page.Content);
            content.AutoSizeTo(page.ContentSize);


            Native.window.ondeviceorientation +=
                (e) =>
                {
                    // x 0..1
                    // is
                    // gamma -45 .. 45
 
                    // 0..1
                    // beta -45 .. 45

                    content.Update(
                        Math.Max(0, Math.Min(1, (e.gamma + 45) / 90.0)),
                        Math.Max(0, Math.Min(1, (e.beta + 45) / 90.0))
                    );

                    //content.s.Text = new { e.alpha, e.gamma, e.beta }.ToString();
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
