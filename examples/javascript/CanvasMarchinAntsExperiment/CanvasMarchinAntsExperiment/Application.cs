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
using CanvasMarchinAntsExperiment;
using CanvasMarchinAntsExperiment.Design;
using CanvasMarchinAntsExperiment.HTML.Pages;

namespace CanvasMarchinAntsExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // http://msdn.microsoft.com/en-us/library/ie/dn265037(v=vs.85).aspx

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // inspired by
            // http://www.amplifon.co.uk/sounds-of-street-view/how-and-create/index.html


            new CanvasRenderingContext2D(500, 400).With(
               async ctx =>
                {
                    ctx.canvas.AttachToDocument();


                    //  Marching Ant code
                    var antOffset = 0;  // Starting offset value
                    var dashList = new[] { 12.0, 3, 3, 3 };  // Create a dot/dash sequence

                    while (true)
                    {
                        ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);
                        //  Assign the dashList for the dash sequence
                        ctx.setLineDash(dashList);
                        //  Get the current offset 
                        ctx.lineDashOffset = antOffset;  // Animate the lines
                        ctx.lineJoin = "round";
                        ctx.lineWidth = 3;
                        ctx.strokeStyle = "blue";
                        ctx.strokeRect(5, 5, 300, 250);
                        ctx.strokeStyle = "red";
                        ctx.strokeRect(150, 200, 300, 150);
                        ctx.lineDashOffset = -antOffset;  // Reverse animation
                        ctx.lineWidth = 7;
                        ctx.strokeStyle = "green";
                        ctx.strokeRect(250, 50, 150, 250);

                        antOffset++;
                        if (antOffset > 20)  // Reset offset after total of dash List values
                        {
                            antOffset = 0;
                        }

                        Native.document.title = new { antOffset }.ToString();

                        // can we see anything?
                        await Task.Delay(30);
                    }
                }
           );




        }

    }
}
