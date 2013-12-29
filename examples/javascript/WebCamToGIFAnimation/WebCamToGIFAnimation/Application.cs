using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebCamToGIFAnimation;
using WebCamToGIFAnimation.Design;
using WebCamToGIFAnimation.HTML.Pages;

namespace WebCamToGIFAnimation
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
            // http://www.brucelawson.co.uk/2012/specifying-which-camera-in-getusermedia/
            // https://groups.google.com/forum/#!topic/discuss-webrtc/xLBdsLz_Mbw
            // http://stackoverflow.com/questions/15322622/using-multiple-usb-cameras-with-web-rtc

            Native.window.navigator.getUserMedia(
                stream =>
                {

                    var v = new IHTMLVideo { src = stream.ToObjectURL() };

                    v.AttachToDocument();

                    v.play();

                    new IHTMLButton { innerText = "record frame" }.AttachToDocument().onclick +=
                        delegate
                        {
                            // Uncaught IndexSizeError: Index or size was negative, or greater than the allowed value. 
                            var c = new CanvasRenderingContext2D(v.clientWidth, v.clientHeight);

                            c.drawImage(
                                v, 0, 0, c.canvas.width, c.canvas.height
                            );

                            var bytes = c.bytes;

                            c.canvas.AttachToDocument();
                        };

                    new IHTMLButton { innerText = "record gif" }.AttachToDocument().WhenClicked(

                       async btn =>
                       {
                           btn.disabled = true;


                           var frames = new List<byte[]>();
                           var x = v.clientWidth;
                           var y = v.clientHeight;

                           //new Timer(
                           //    async tt =>
                           //    {


                           btn.innerText = "rec " + new { frames.Count }.ToString();
                           //while (frames.Count < 60)

                           while (frames.Count < 20)
                           {
                               await Task.Delay(1000 / 5);

                               btn.innerText = new { frames.Count }.ToString();

                               var bytes = v.bytes;

                               Console.WriteLine(new { bytes.Length });

                               // script: error JSC1000: No implementation found for this native method, please implement
                               // [System.Threading.Tasks.TaskFactory`1.
                               // StartNew(, System.Object, System.Threading.CancellationToken, System.Threading.Tasks.TaskCreationOptions, System.Threading.Tasks.TaskScheduler)]

                               frames.Add(bytes);

                           }


                           Console.WriteLine("encoding!");

                           var e = new Stopwatch();
                           e.Start();

                           var src = await new GIFEncoderWorker(
                               x,
                               y,
                                   delay: 1000 / 10,
                               frames: frames,
                               AtFrame:
                                async index =>
                                {
                                    btn.innerText = new { index, frames.Count }.ToString();

                                    if (index == frames.Count - 1)
                                    {
                                        btn.style.color = "blue";
                                        await Task.Delay(800);
                                        btn.style.color = "";

                                        btn.innerText = "record gif";

                                    }
                                }


                           );


                           Console.WriteLine("done! " + new { e.Elapsed });

                           new IHTMLImage { src = src }.AttachToDocument();


                           btn.disabled = false;
                           btn.title = new { e.Elapsed, src.Length }.ToString();

                       }
                       );
                }
            );

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
