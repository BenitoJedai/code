using AvalonTiltTeacher.Design;
using AvalonTiltTeacher.HTML.Pages;
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

namespace AvalonTiltTeacher
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
        public Application(IDefaultPage page)
        {
            content.AttachToContainer(page.Content);
            content.AutoSizeTo(page.ContentSize);

            // originally this was written for flash on android

            //Native.Window.ondeviceorientation +=
            //    e =>
            //    {
            //        // still { alpha = 334.01653539221314, beta = -0.9072213706108246, gamma = 2.2069056243739933 }
            //        // anwser { alpha = 302.3353381347018, beta = -3.5312795178828775, gamma = -36.28494624868044 }
            //        //  left { alpha = 319.83587080725647, beta = -22.594597038576925, gamma = 2.9337113900403393 }
            //        // right { alpha = 349.2730401488114, beta = 17.597914094646153, gamma = 1.7935541017490462 }


            //        //az = (p.Y / this.Height);
            //        //ax = -1 * ((p.X / this.Width) - 0.5);

            //        var az = Math.Min(0, Math.Max(e.alpha, -33)) / 33.0;
            //        var ax = 1 * Math.Min(33, Math.Max(e.beta, -33)) / 66.0;

            //        Console.WriteLine(
            //              new
            //              {
            //                  e.alpha,
            //                  e.beta,
            //                  e.gamma,
                             
            //              }.ToString()
            //          );

            //        //content.Accelerate(
            //        //    e.gamma,
            //        //    ax,
            //        //    az
            //        //);
            //    };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
