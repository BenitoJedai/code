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
using ScriptCoreLib.Avalon;

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
        public Application(IDefault page)
        {

            //AddTransform(canvas);

            var context = new CanvasRenderingContext2D();

            var canvas = context.canvas.AttachTo(page.Content);
            canvas.style.SetSize(400, 400);


            Action DrawLogo = delegate { };

            new white_jsc().InvokeOnComplete(
                img =>
                {
                    var data = img.toDataURL();

                    DrawLogo = delegate
                    {

                        context.drawImage(
                            new IHTMLImage { src = data },
                            0, 0, 96, 48 // ??
                        );
                    };
                }
            );

            var r = new Random();
            Func<float> random = () => (float)r.NextDouble();

            var lastX = context.canvas.width * random();
            var lastY = context.canvas.height * random();
            var hue = 0;
            Action line = delegate
            {
                context.save();
                context.translate(context.canvas.width / 2, context.canvas.height / 2);
                context.scale(0.9f, 0.9f);
                context.translate(-context.canvas.width / 2, -context.canvas.height / 2);
                context.beginPath();
                context.lineWidth = 5f + random() * 10f;
                context.moveTo(lastX, lastY);
                lastX = context.canvas.width * random();
                lastY = context.canvas.height * random();
                context.bezierCurveTo(context.canvas.width * random(),
                                      context.canvas.height * random(),
                                      context.canvas.width * random(),
                                      context.canvas.height * random(),
                                      lastX, lastY);

                hue = hue + Convert.ToInt32((double)(10 * random()));
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

            blank();

            blank.AtInterval(40);
        }

        private static void AddTransform(IHTMLCanvas canvas)
        {
            canvas.style.SetMatrixTransform(
                new AffineTransformBase
                {
                    Left = 0,
                    Top = 0,
                    Width = 400,
                    Height = 400,

                    X1 = 400,
                    Y1 = 100,

                    X2 = 100,
                    Y2 = 400,

                    X3 = 0,
                    Y3 = 0
                }
            );
        }

    }
}
