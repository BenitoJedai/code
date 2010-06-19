// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using PrettyGlowingLines.HTML.Pages;
using PrettyGlowingLines;
using PrettyGlowingLines.HTML.Images.FromAssets;

namespace PrettyGlowingLines
{
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            var canvas = new IHTMLCanvas().AttachTo(page.Content);

            canvas.style.SetSize(400, 400);

            var context = (CanvasRenderingContext2D)canvas.getContext("2d");

            Action DrawLogo = delegate { };

            new white_jsc().InvokeOnComplete(
                img =>
                {
                    DrawLogo = delegate
                    {
                        context.drawImage(
                            img,
                            0, 0, 96, 48 // ??
                        );
                    };
                }
            );

            var r = new Random();
            Func<double> random = r.NextDouble;

            var lastX = context.canvas.width * random();
            var lastY = context.canvas.height * random();
            var hue = 0;
            Action line = delegate
            {
                context.save();
                context.translate(context.canvas.width / 2, context.canvas.height / 2);
                context.scale(0.9, 0.9);
                context.translate(-context.canvas.width / 2, -context.canvas.height / 2);
                context.beginPath();
                context.lineWidth = 5 + random() * 10;
                context.moveTo(lastX, lastY);
                lastX = context.canvas.width * random();
                lastY = context.canvas.height * random();
                context.bezierCurveTo(context.canvas.width * random(),
                                      context.canvas.height * random(),
                                      context.canvas.width * random(),
                                      context.canvas.height * random(),
                                      lastX, lastY);

                hue = hue + Convert.ToInt32(10 * random());
                context.strokeStyle = "hsl(" + hue + ", 50%, 50%)";
                context.shadowColor = "white";
                context.shadowBlur = 10;
                context.stroke();
                context.restore();

            };

            line.AtInterval(50);


            Action blank = delegate
            {
                context.fillStyle = "rgba(0,0,0,0.1)";
                context.fillRect(0, 0, context.canvas.width, context.canvas.height);

                DrawLogo();
               
            };

            blank.AtInterval(40);
        }

    }
}
