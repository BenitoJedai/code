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
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using HTMLCanvasPlasma.HTML.Pages;
using HTMLCanvasPlasma;
using ScriptCoreLib.Avalon;

namespace HTMLCanvasPlasma
{
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        const int DefaultWidth = 300;
        const int DefaultHeight = 300;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IXDefaultPage page)
        {
            var canvas = new IHTMLCanvas().AttachTo(page.Content);

            Plasma.generatePlasma(DefaultWidth, DefaultHeight);

            var shift = 0;


            canvas.width = DefaultWidth;
            canvas.height = DefaultHeight;

            var context = (CanvasRenderingContext2D)canvas.getContext("2d");

            var t = new ScriptCoreLib.JavaScript.Runtime.Timer();

            //var x = new MyImageData(DefaultWidth, DefaultHeight);
            var x = context.getImageData(0, 0, DefaultWidth, DefaultHeight);

            t.Tick +=
                delegate
                {

                    var buffer = Plasma.shiftPlasma(shift);

                    //var x = context.createImageData(DefaultWidth, DefaultHeight);


                    var k = 0;
                    for (int i = 0; i < DefaultWidth; i++)
                        for (int j = 0; j < DefaultHeight; j++)
                        {
                            var i4 = i * 4;
                            var j4 = j * 4;

                            

                            x.data[(ulong)(i4 + j4 * DefaultWidth + 2)] = (byte)((buffer[k] >> (0 * 8)) & 0xff);
                            x.data[(ulong)(i4 + j4 * DefaultWidth + 1)] = (byte)((buffer[k] >> (1 * 8)) & 0xff);
                            x.data[(ulong)(i4 + j4 * DefaultWidth + 0)] = (byte)((buffer[k] >> (2 * 8)) & 0xff);
                            x.data[(ulong)(i4 + j4 * DefaultWidth + 3)] = 0xff;

                            k++;
                        }

                    context.putImageData(x, 0, 0, 0, 0, DefaultWidth, DefaultHeight);
                    shift++;
                };

            t.StartInterval(50);


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
