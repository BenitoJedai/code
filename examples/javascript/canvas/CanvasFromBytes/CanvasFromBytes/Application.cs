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
using CanvasFromBytes;
using CanvasFromBytes.Design;
using CanvasFromBytes.HTML.Pages;

namespace CanvasFromBytes
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
            Console.WriteLine("before");
            {
                var c = new CanvasRenderingContext2D(256, 256);


                var bytes = new byte[256 * 256 * 4];

                for (int x = 0; x < 256; x++)
                {
                    for (int y = 0; y < 256; y++)
                    {
                        bytes[x * 4 + 0 + 256 * 4 * y] = (byte)x;
                        bytes[x * 4 + 1 + 256 * 4 * y] = (byte)y;
                        bytes[x * 4 + 2 + 256 * 4 * y] = 0xff;
                        bytes[x * 4 + 3 + 256 * 4 * y] = 0xcf;
                    }


                }

                var i = c.getImageData();

                i.data.set(bytes, 0);

                c.putImageData(i, 0, 0, 0, 0, 256, 256);

                c.canvas.AttachToDocument();

            }


            {
                var bytes = new byte[256 * 256 * 4];

                for (int x = 0; x < 256; x++)
                {
                    for (int y = 0; y < 256; y++)
                    {
                        bytes[x * 4 + 0 + 256 * 4 * y] = (byte)x;
                        bytes[x * 4 + 1 + 256 * 4 * y] = 0xff;
                        bytes[x * 4 + 2 + 256 * 4 * y] = (byte)y;
                        bytes[x * 4 + 3 + 256 * 4 * y] = 0xcf;
                    }


                }

                new CanvasRenderingContext2D(256, 256) { bytes = bytes }.canvas.AttachToDocument();

            }
            Console.WriteLine("after");


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
