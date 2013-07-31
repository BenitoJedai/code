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
using Abstractatech.JavaScript.CODE128.Design;
using Abstractatech.JavaScript.CODE128.HTML.Pages;

namespace ScriptCoreLib.Extensions
{
    public static class CODE128Extensions
    {
        // async supported yet?
        public static /* async */ void ToCode128(this string data, int width = 2, int height = 50, Action<IHTMLImage> yield = null)
        {
            // load only once?
            new Abstractatech.JavaScript.CODE128.Design.CODE128().Content.AttachToDocument().onload +=
             delegate
             {

                 var options =
                              new
                              {
                                  width,
                                  height,

                                  quite = 10
                              };


                 // idl?

                 // dynamic?
                 //dynamic xx = new object();
                 //var xxx = new xx();

                 var encoder = new IFunction("e", "return new CODE128(e)").apply(null, data);

                 var binary = (string)new IFunction("e", "return e.encoded()").apply(null, encoder);


                 var ctx = new CanvasRenderingContext2D();
                 var canvas = ctx.canvas;

                 //Set the width and height of the barcode
                 canvas.width = binary.Length * options.width + 2 * options.quite;
                 canvas.height = options.height;

                 //Paint the canvas white
                 ctx.fillStyle = "#fff";
                 ctx.fillRect(0, 0, canvas.width, canvas.height);

                 //Creates the barcode out of the encoded binary
                 for (var i = 0; i < binary.Length; i++)
                 {

                     var x = i * options.width + options.quite;

                     if (binary[i] == '1')
                     {
                         ctx.fillStyle = "#000";
                     }
                     else
                     {
                         ctx.fillStyle = "#fff";
                     }

                     ctx.fillRect(x, 0, options.width, options.height);
                 }

                 //Grab the dataUri from the canvas
                 var uri = canvas.toDataURL("image/png");


                 var c = new IHTMLCenter().AttachToDocument();

                 var img = new IHTMLImage { src = uri };


                 yield(img);

             };

        }
    }
}

namespace Abstractatech.JavaScript.CODE128
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

            var data = "1234";


            data.ToCode128(
                height: 20,
                yield:
                    img =>
                    {
                        var c = new IHTMLCenter().AttachToDocument();

                        img.AttachTo(c);
                        new IHTMLBreak { }.AttachTo(c);
                        new IHTMLCode { innerText = data }.AttachTo(c).style.fontSize = "x-small";
                    }
            );



        }

    }
}
