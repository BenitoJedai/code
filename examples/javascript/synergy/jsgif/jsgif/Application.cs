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
using jsgif.Design.Styles;
using jsgif.HTML.Pages;
using jsgif.Library;

namespace jsgif
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // based on http://antimatter15.com/wp/2010/07/javascript-to-animated-gif/

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            new Design.b64().Content.WhenAvailable(
               delegate
               {

                   new Design.LZWEncoder().Content.WhenAvailable(
                        delegate
                        {

                            new Design.NeuQuant().Content.WhenAvailable(
                               delegate
                               {

                                   new Design.GIFEncoder().Content.WhenAvailable(
                                      delegate
                                      {

                                          InitializeContent(page);
                                      }
                                    );
                               }
                            );
                        }
                     );
               }
            );


            style.Content.AttachToHead();
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        private void InitializeContent(IDefault page)
        {
            var canvas = new IHTMLCanvas().AttachToDocument();

              var context = (CanvasRenderingContext2D) canvas.getContext("2d");



              var my_gradient = context.createLinearGradient(0, 0, 300, 0);

              my_gradient.addColorStop(0, "black");

              my_gradient.addColorStop(1, "white");





              context.fillStyle = my_gradient; //"rgb(255,255,255)";  

              context.fillRect(0,0,canvas.width, canvas.height); //GIF can't do transparent so do white







              var encoder = new GIFEncoder();

              encoder.setRepeat(0); //auto-loop

              encoder.setDelay(500);

              encoder.start();

  

  

              context.fillStyle = "rgb(200,0,0)";  

              context.fillRect (10, 10, 75, 50);  

              encoder.addFrame(context);

              context.fillStyle = "rgb(20,0,200)";  

              context.fillRect (30, 30, 55, 50);  

              encoder.addFrame(context);

              encoder.finish();

            var image = new IHTMLImage();

            
            image.src  = "data:image/gif;base64,"+b64.Window.encode64(encoder.stream().getData());
            image.AttachToDocument();

        }

    }

    [Script(HasNoPrototype = true)]
    public class b64
    {
        [Script(ExternalTarget = "window")]
        public static b64 Window;

        public string encode64(object e)
        {
                throw new NotImplementedException();
        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "GIFEncoder")]
    public class GIFEncoder
    {
        [Script(HasNoPrototype = true, ExternalTarget = "ByteArray")]
        public class ByteArray
        {
            public object getData()
            {
                throw new NotImplementedException();
            }
        }

        public void setRepeat(int p)
        {
            throw new NotImplementedException();
        }

        public void setDelay(int p)
        {
            throw new NotImplementedException();
        }

        public void start()
        {
            throw new NotImplementedException();
        }

        public void addFrame(CanvasRenderingContext2D context)
        {
            throw new NotImplementedException();
        }

        public void finish()
        {
            throw new NotImplementedException();
        }

        public ByteArray stream()
        {
            throw new NotImplementedException();
        }
    }
}
