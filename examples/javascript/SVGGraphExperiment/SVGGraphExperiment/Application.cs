using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.SVG;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SVGGraphExperiment.Design;
using SVGGraphExperiment.HTML.Pages;
using ScriptCoreLib.Shared.Drawing;
using System.Collections.Generic;

namespace SVGGraphExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var y = page.ThePath_y;
            var x = page.ThePath_x;

            page.TheText.textContent = "pointer lock movement";

            // script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.CSharp.RuntimeBinder.Binder.IsEvent(Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, System.String, System.Type)]

            //pp.setAttribute("d", pp.getAttribute("d") + " l100,-10         ");


            page.Data.onmousedown +=
                e =>
                {
                    page.Data.requestPointerLock();
                };

            var history = new List<Point>();



            page.Data.onmousemove +=
                e =>
                {
                    if (page.Data == Native.Document.pointerLockElement)
                    {
                        history.Add(
                            // vec2?
                            new Point(e.movementX, e.movementY)
                        );

                        if (history.Count > 500)
                            history.RemoveAt(0);

                        var xw = new StringBuilder().Append("M10,200 ");
                        var yw = new StringBuilder().Append("M10,200 ");


                        history.WithEach(
                            p =>
                            {
                                xw.Append(" l2," + p.X);
                                yw.Append(" l2," + p.Y);

                            }
                        );

                        Console.WriteLine(new { xw, yw });

                        y.d = yw.ToString();
                        x.d = xw.ToString();
                    }
                };

            // An attempt was made to access a socket in a way forbidden by its access permissions

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }


}
