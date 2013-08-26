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
using jsgif.HTML.Pages;
//using jsgif.opensource;

namespace jsgif
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // based on http://antimatter15.com/wp/2010/07/javascript-to-animated-gif/

        public readonly ApplicationWebService service = new ApplicationWebService();


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            // do we need automatic decomposer?

            var context = new CanvasRenderingContext2D();

            // needs to be in dom? no
            var canvas = context.canvas;

            var my_gradient = context.createLinearGradient(0, 0, 300, 0);
            my_gradient.addColorStop(0, "black");
            my_gradient.addColorStop(1, "white");


            context.fillStyle = my_gradient; //"rgb(255,255,255)";  
            context.fillRect(0, 0, canvas.width, canvas.height); //GIF can't do transparent so do white


            var encoder = new GIFEncoder();

            encoder.setRepeat(0); //auto-loop
            encoder.setDelay(500);
            encoder.start();
            
            context.fillStyle = "rgb(200,0,0)";
            context.fillRect(10, 10, 75, 50);

            encoder.addFrame(context);

            context.fillStyle = "rgb(20,0,200)";
            context.fillRect(30, 30, 55, 50);

            encoder.addFrame(
                context.getImageData(0, 0, context.canvas.width, context.canvas.height).data,
                true
            );

            encoder.finish();

            {
                var image = new IHTMLImage();
                var data = encoder.stream().getData();

                image.src = "data:image/gif;base64," + Convert.ToBase64String(Encoding.ASCII.GetBytes(data));
                image.AttachToDocument();
            }

        }

    }


}
