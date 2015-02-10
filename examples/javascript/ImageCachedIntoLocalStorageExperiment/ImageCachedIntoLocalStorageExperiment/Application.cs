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
using ImageCachedIntoLocalStorageExperiment.Design;
using ImageCachedIntoLocalStorageExperiment.HTML.Pages;
using ImageCachedIntoLocalStorageExperiment.HTML.Images.FromAssets;

namespace ImageCachedIntoLocalStorageExperiment
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
            // what about service worker?

            Native.window.localStorage[new { index = 1 }].With(
                dataURL =>
                {
                    var i = new IHTMLImage { src = dataURL }.AttachToDocument();


                    new IHTMLBreak().AttachToDocument();

                    new IHTMLButton("clear localStorage").AttachToDocument().WhenClicked(
                        clear =>
                        {
                            Native.window.localStorage.removeItem(new { index = 1 });

                            i.Orphanize();

                            clear.Orphanize();
                        }
                    );

                }
            );


            new Preview().AttachToDocument().InvokeOnComplete(
                img =>
                {
                    // https://www.ibm.com/developerworks/community/blogs/bobleah/entry/html5_code_example_store_images_using_localstorage57?lang=en
                    // http://stackoverflow.com/questions/934012/get-image-data-in-javascript

                    //var canvas = new IHTMLCanvas
                    //{
                    //    width = img.width,
                    //    height = img.height
                    //};

                    //var context = (CanvasRenderingContext2D)canvas.getContext("2d");

                    var context = new CanvasRenderingContext2D();

                    context.canvas.width = img.width;
                    context.canvas.height = img.height;

                    context.drawImage(img, 0, 0, img.width, img.height);

                    // http://www.w3schools.com/tags/canvas_filltext.asp
                    context.fillStyle = "yellow";
                    context.font = "20px Verdana";
                    context.moveTo(0, 0);

                    // y means bottom
                    context.fillText("cache", 0, 20, img.width);


                    context.canvas.AttachToDocument();

                    var dataURL = context.canvas.toDataURL();

                    // data. ?
                    Native.window.localStorage[new { index = 1 }] = dataURL;


                    new IHTMLPre { innerText = new { dataURL }.ToString() }.AttachToDocument();

                }
            );
        }

    }
}
