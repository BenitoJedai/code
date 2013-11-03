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
    public sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var y = page.ThePath_y;
            var x = page.ThePath_x;
            var z = page.ThePath_z;

            page.TheText.textContent = "pointer lock movement";

            // script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.CSharp.RuntimeBinder.Binder.IsEvent(Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, System.String, System.Type)]

            //pp.setAttribute("d", pp.getAttribute("d") + " l100,-10         ");



            var history =
                new { x = 0.0, y = 0.0, z = 0.0 }.ToEmptyList();


            50.Times(
                delegate
                {
                    history.Add(
                        // vec2?
                       new { x = 0.0, y = 0.0, z = 0.0 }
                   );

                }
            );

            var movementX = 0.0;
            var movementY = 0.0;
            var movementZ = 0.0;

            Native.Document.body.onmousedown +=
                  e =>
                  {
                      Native.Document.body.requestPointerLock();
                  };


            Native.Document.body.onmousemove +=
                e =>
                {
                    if (Native.Document.body == Native.Document.pointerLockElement)
                    {
                        movementX += e.movementX;
                        movementY += e.movementY;
                    }
                };

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    history.Add(
                        // vec2?
                        new { x = movementX, y = movementY, z = movementZ }
                            );

                    movementX = 0;
                    movementY = 0;
                    movementZ = 0;

                    if (history.Count > 500)
                    {
                        history.RemoveAt(0);
                    }

                    // http://www.w3.org/TR/SVG/paths.html#PathDataMovetoCommands
                    var xw = new StringBuilder().Append("M10,200 ");
                    var yw = new StringBuilder().Append("M10,300 ");
                    var zw = new StringBuilder().Append("M10,400 ");


                    history.WithEachIndex(
                        (p, i) =>
                        {
                            xw.Append(" L" + (10 + 2 * i) + "," + (p.x + 200));
                            yw.Append(" L" + (10 + 2 * i) + "," + (p.y + 300));
                            zw.Append(" L" + (10 + 2 * i) + "," + (p.z + 400));

                        }
                    );

                    xw.Append(" L" + (10 + 2 * history.Count) + "," + (201));
                    yw.Append(" L" + (10 + 2 * history.Count) + "," + (301));
                    zw.Append(" L" + (10 + 2 * history.Count) + "," + (401));

                    //Console.WriteLine(new { xw, yw });

                    y.d = yw.ToString();
                    x.d = xw.ToString();
                    z.d = zw.ToString();
                }
            ).StartInterval(1000 / 10);


            Native.Document.onkeydown +=
                delegate
                {
                    movementZ = 100;
                };

            Native.Document.onkeyup +=
            delegate
            {
                movementZ = 0;
            };


            Native.window.ondeviceorientation +=
                eventData =>
                {

                    movementX = eventData.alpha;
                    movementY = eventData.beta;
                    movementZ = eventData.gamma;


                };
    
        }

    }


}
