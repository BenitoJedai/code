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
            // https://github.com/tumblr/jumblr

            new Abstractatech.ConsoleFormPackage.Library.ConsoleForm()
                .InitializeConsoleFormWriter()
                .PopupInsteadOfClosing()
                .Show();


            // 
            new IHTMLIFrame
            {
                src = "http://www.whatsmyip.us/",
                //src = "http://whatsmyip.net/", 
                frameBorder = "0",
                scrolling = "no",
                width = 400,
                height = 72
            }.AttachToDocument().style.SetSize(
                400, 72);

            new IHTMLBreak().AttachToDocument();

            //new IHTMLButton { innerText = "swapCache" }.AttachToDocument().WhenClicked(
            //    delegate
            //    {
            //        // Uncaught InvalidStateError: Failed to execute 'swapCache' on 'ApplicationCache': there is no newer application cache to swap to.
            //        Native.window.applicationCache.swapCache();
            //    }
            //);


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier

            service.getNumberOfCameras(
                cameras =>
                {

                    Enumerable.Range(0, int.Parse(cameras)).Select(i => new { i }).WithEach(
                        ii =>
                        {
                            var i = ii.i;
                            new IHTMLBreak().AttachToDocument();

                            #region show me stills
                            new IHTMLButton { innerText = "show me stills " + new { i } }.AttachToDocument().onclick +=
                                delegate
                                {
                                    service.WebMethod2(
                                        "" + i,
                                        value =>
                                        {
                                            if (!value.StartsWith("data:"))
                                            {
                                                Console.WriteLine(value);
                                                return;
                                            }

                                            new IHTMLImage { src = value }.AttachToDocument();
                                        }
                                    );
                                };
                            #endregion


                            #region show me animation


                            new IHTMLButton { innerText = "show me animation" }.AttachToDocument().onclick +=
                                delegate
                                {
                                    var img = new IHTMLImage { }.AttachToDocument();

                                    var data = new List<string>();

                                    service.WebMethod2(
                                       "" + i,
                                        value =>
                                        {
                                            if (!value.StartsWith("data:"))
                                            {
                                                Console.WriteLine(value);
                                                return;
                                            }

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
                            #endregion


                            #region show me animation async
                            new IHTMLButton { innerText = "show me animation async" }.AttachToDocument().onclick +=
                                 delegate
                                 {



                                     var img = new IHTMLImage { }.AttachToDocument();

                                     var data = new List<string>();



                                     service.async_WebMethod2(
                                        "" + i,
                                         value =>
                                         {
                                             if (!value.StartsWith("data:"))
                                             {
                                                 Console.WriteLine(value);
                                                 return;
                                             }

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
                            #endregion

                            #region show me animation async and gif it
                            new IHTMLButton { innerText = "gif it " + new { i } }.AttachToDocument().onclick +=
                                 delegate
                                 {
                                     Console.WriteLine("working...");


                                     new IHTMLBreak().AttachToDocument();

                                     var img = new IHTMLImage { }.AttachToDocument();

                                     var data = new List<string>();

                                     var gifmaxframes = 60;

                                     #region Timer
                                     new Timer(
                                         t =>
                                         {
                                             if (data.Count == 0)
                                                 return;

                                             if (data.Count < gifmaxframes)
                                             {
                                                 img.src = data.Last();
                                                 return;
                                             }

                                             img.src = data[t.Counter % data.Count];
                                         }
                                     ).StartInterval(1000 / 15);
                                     #endregion


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


                                     // http://stackoverflow.com/questions/1864756/web-workers-and-canvas

                                     service.async_WebMethod2(
                                         "" + i,
                                         value =>
                                         {
                                             if (!value.StartsWith("data:"))
                                             {
                                                 Console.WriteLine(value);
                                                 return;
                                             }

                                             data.Add(value);



                                             if (data.Count == gifmaxframes)
                                             {
                                                 // need webworker!

                                                 #region gif
                                                 var st = new Stopwatch();
                                                 st.Start();

                                                 var encoder1 = new GIFEncoderAsync(
                                                     320, 240
                                                 );

                                                 //encoder1.setRepeat(0); //auto-loop
                                                 //encoder1.setDelay(1000 / 15);
                                                 //encoder1.start();

                                                 var encoder2 = new GIFEncoderAsync(
                                                     320, 240
                                                 );

                                                 //encoder2.setRepeat(0); //auto-loop
                                                 //encoder2.setDelay(1000 / 15);
                                                 //encoder2.start();

                                                 #region frames
                                                 Console.WriteLine("gif " + new { st.Elapsed });

                                                 for (int datai = 0; datai < data.Count; datai++)
                                                 {
                                                     Console.WriteLine("gif " + new { datai, st.Elapsed });

                                                     c1.drawImage(new IHTMLImage { src = data[datai] }, 0, 0, 320, 240);
                                                     encoder2.addFrame(c1);


                                                     c0.drawImage(new IHTMLImage { src = data[datai] }, 0, 0, 320, 240);

                                                     #region grayscale
                                                     var imageData = c0.getImageData(0, 0, 320, 240);

                                                     // This loop gets every pixels on the image and
                                                     for (var j = 0; j < imageData.width; j++)
                                                     {
                                                         for (var ji = 0; ji < imageData.height; ji++)
                                                         {
                                                             var index = (uint)((ji * 4) * imageData.width + (j * 4));
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
                                                     #endregion


                                                     encoder1.addFrame(c0);

                                                     Console.WriteLine("gif done " + new { datai, st.Elapsed });

                                                 }


                                                 encoder1.finish(
                                                 src =>
                                                 {
                                                     var image = new IHTMLImage();
                                                     image.src = src;
                                                     image.style.SetSize(640, 480);

                                                     Console.WriteLine("gif got src" + new { st.Elapsed });

                                                     image.AttachToDocument();
                                                 }
                                                 );
                                                 encoder2.finish(
                                                 src =>
                                                 {
                                                     var image = new IHTMLImage();

                                                     image.src = src;
                                                     image.style.SetSize(640, 480);

                                                     Console.WriteLine("gif got src" + new { st.Elapsed });

                                                     image.AttachToDocument();
                                                 }
                                                 );
                                                 #endregion


                                                 #endregion



                                             }
                                         }

                                     );

                                 };
                            #endregion





                        }
                    );




                }
            );


            new IHTMLBreak().AttachToDocument();
        }

    }
}
