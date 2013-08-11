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
using MandelbrotCanvas.Design;
using MandelbrotCanvas.HTML.Pages;
using Mandelbrot;
using ScriptCoreLib.JavaScript.Runtime;

namespace MandelbrotCanvas
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
        public Application(IDefault page)
        {
            var shift = 0;
            //Debugger.Break();
            var buffer = MandelbrotProvider.DrawMandelbrotSet(shift);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = 0;
            }




            var context = new CanvasRenderingContext2D();

            var canvas = context.canvas.AttachToDocument();
            canvas.width = MandelbrotProvider.DefaultWidth;
            canvas.height = MandelbrotProvider.DefaultHeight;

            //var t = new Timer();

            //var x = new MyImageData(DefaultWidth, DefaultHeight);
            var x = context.getImageData(0, 0, MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight);

            Action Refresh =
                delegate
                {
                    buffer = MandelbrotProvider.DrawMandelbrotSet(shift);

                    //var x = context.createImageData(DefaultWidth, DefaultHeight);


                    var k = 0;
                    for (int i = 0; i < MandelbrotProvider.DefaultWidth; i++)
                        for (int j = 0; j < MandelbrotProvider.DefaultHeight; j++)
                        {
                            var i4 = i * 4;
                            var j4 = j * 4;

                            var offset = (uint)(i4 + j4 * MandelbrotProvider.DefaultWidth);

                            x.data[offset + 2] = (byte)((buffer[k] >> (0 * 8)) & 0xff);
                            x.data[offset + 1] = (byte)((buffer[k] >> (1 * 8)) & 0xff);
                            x.data[offset + 0] = (byte)((buffer[k] >> (2 * 8)) & 0xff);
                            x.data[offset + 3] = 0xff;

                            //x.data[offset + 2] = 0x0f;
                            //x.data[offset + 1] = 0xff;
                            //x.data[offset + 0] = 0x0f;
                            //x.data[offset + 3] = 0xff;

                            k++;
                        }

                    context.putImageData(x, 0, 0, 0, 0, MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight);

                };



            Native.window.onframe +=
                delegate
                {

                    Refresh();

                    shift++;
                };


            canvas.ondblclick +=
                delegate
                {
                    canvas.requestFullscreen();

                };
            //t.StartInterval(50);
            //Refresh();

            //canvas.AttachToDocument();


            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
