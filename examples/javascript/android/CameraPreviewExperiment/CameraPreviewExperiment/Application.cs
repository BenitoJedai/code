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
using CameraPreviewExperiment;
using CameraPreviewExperiment.Design;
using CameraPreviewExperiment.HTML.Pages;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;
using System.Diagnostics;

namespace CameraPreviewExperiment
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
            new IHTMLButton { innerText = "swapCache" }.AttachToDocument().WhenClicked(
                delegate
                {
                    // Uncaught InvalidStateError: Failed to execute 'swapCache' on 'ApplicationCache': there is no newer application cache to swap to.
                    Native.window.applicationCache.swapCache();
                }
            );


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier

            new IHTMLButton { innerText = "show me stills" }.AttachToDocument().onclick +=
                delegate
                {
                    service.WebMethod2(
                        @"A string from JavaScript.",
                        value => new IHTMLImage { src = value }.AttachToDocument()
                    );
                };

            new IHTMLButton { innerText = "show me animation" }.AttachToDocument().onclick +=
                delegate
                {
                    var img = new IHTMLImage { }.AttachToDocument();

                    var data = new List<string>();

                    service.WebMethod2(
                        @"A string from JavaScript.",
                        value => data.Add(value)
                    );

                    new Timer(
                        t =>
                        {
                            if (data.Count == 0)
                                return;

                            img.src = data[t.Counter % data.Count];
                        }
                    ).StartInterval(1000 / 15);
                };


            new IHTMLButton { innerText = "show me animation async" }.AttachToDocument().onclick +=
                 delegate
                 {



                     var img = new IHTMLImage { }.AttachToDocument();

                     var data = new List<string>();



                     service.async_WebMethod2(
                         @"A string from JavaScript.",
                         value =>
                         {
                             data.Add(value);


                         }

                     );

                     new Timer(
                         t =>
                         {
                             if (data.Count == 0)
                                 return;

                             img.src = data[t.Counter % data.Count];
                         }
                     ).StartInterval(1000 / 15);
                 };



            new IHTMLButton { innerText = "show me animation async and gif it" }.AttachToDocument().onclick +=
                 delegate
                 {
                     var encoder1 = new GIFEncoder();

                     encoder1.setRepeat(0); //auto-loop
                     encoder1.setDelay(1000 / 15);
                     encoder1.start();

                     var encoder2 = new GIFEncoder();

                     encoder2.setRepeat(0); //auto-loop
                     encoder2.setDelay(1000 / 15);
                     encoder2.start();

                     var img = new IHTMLImage { }.AttachToDocument();

                     var data = new List<string>();


                     new Timer(
                         t =>
                         {
                             if (data.Count == 0)
                                 return;

                             img.src = data[t.Counter % data.Count];
                         }
                     ).StartInterval(1000 / 15);


                     var c0 = new CanvasRenderingContext2D();

                     // no scale!
                     c0.canvas.width = 320;
                     c0.canvas.height = 240;
                     c0.canvas.style.SetSize(320, 240);



                     var c1 = new CanvasRenderingContext2D();

                     // no scale!
                     c1.canvas.width = 320;
                     c1.canvas.height = 240;
                     c1.canvas.style.SetSize(320, 240);

                     //c0.canvas.AttachToDocument();

                     var maxframes = 15;

                     service.async_WebMethod2(
                         @"A string from JavaScript.",
                         value =>
                         {
                             data.Add(value);



                             if (data.Count < maxframes)
                             {



                             }
                             else if (data.Count == maxframes)
                             {
                                 var st = new Stopwatch();
                                 st.Start();

                                 Console.WriteLine("gif " + new { st.Elapsed });

                                 for (int datai = 0; datai < data.Count; datai++)
                                 {
                                     Console.WriteLine("gif " + new { datai, st.Elapsed });

                                     c1.drawImage(new IHTMLImage { src = data[datai] }, 0, 0, 320, 240);
                                     encoder2.addFrame(c1);


                                     c0.drawImage(new IHTMLImage { src = data[datai] }, 0, 0, 320, 240);


                                     var imageData = c0.getImageData(0, 0, 320, 240);

                                     // This loop gets every pixels on the image and
                                     for (var j = 0; j < imageData.width; j++)
                                     {
                                         for (var i = 0; i < imageData.height; i++)
                                         {
                                             var index = (uint)((i * 4) * imageData.width + (j * 4));
                                             var red = imageData.data[index];
                                             var green = imageData.data[index + 1];
                                             var blue = imageData.data[index + 2];
                                             var alpha = imageData.data[index + 3];
                                             var average = (byte)((red + green + blue) / 3);

                                             imageData.data[index] = average;
                                             imageData.data[index + 1] = average;
                                             imageData.data[index + 2] = average;

                                             //imageData.data[index + 1] = average;
                                             //imageData.data[index + 2] = average;

                                             imageData.data[index + 3] = alpha;
                                         }
                                     }

                                     // overwrite original image
                                     c0.putImageData(imageData, 0, 0, 0, 0, 320, 240);

                                     encoder1.addFrame(c0);

                                     Console.WriteLine("gif done " + new { datai, st.Elapsed });

                                 }


                                 encoder1.finish();
                                 encoder2.finish();


                                 {
                                     var image = new IHTMLImage();
                                     var xdata = encoder1.stream().getData();
                                     var bytes = Encoding.ASCII.GetBytes(xdata);


                                     image.src = "data:image/gif;base64," + System.Convert.ToBase64String(bytes);
                                     image.style.SetSize(640, 480);

                                     Console.WriteLine("gif got src" + new { st.Elapsed, bytes.Length });

                                     image.AttachToDocument();
                                 }


                                 {
                                     var image = new IHTMLImage();
                                     var xdata = encoder2.stream().getData();
                                     var bytes = Encoding.ASCII.GetBytes(xdata);


                                     image.src = "data:image/gif;base64," + System.Convert.ToBase64String(bytes);
                                     image.style.SetSize(640, 480);

                                     Console.WriteLine("gif got src" + new { st.Elapsed, bytes.Length });

                                     image.AttachToDocument();
                                 }


                             }
                         }

                     );

                 };
        }

    }
}
