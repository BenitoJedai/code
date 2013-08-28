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
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;
using System.Collections.Generic;

namespace MandelbrotCanvas
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();


        class MandelbrotAsync
        {

            public byte[] frame { private set; get; }


            public event Action onframe;


            public MandelbrotAsync()
            {
                var w = new Worker(
                     scope =>
                     {
                         Console.WriteLine("waiting for scope data");

                         int shift = 0;


                         var once = false;
                         scope.onmessage +=
                              ze =>
                              {
                                  #region waiting for scope data
                                  if (!once)
                                  {
                                      once = true;
                                      return;
                                  }
                                  #endregion



                                  var frame = (byte[])ze.data;



                                  var zDefaultWidth = MandelbrotProvider.DefaultWidth;
                                  var zDefaultHeight = MandelbrotProvider.DefaultHeight;


                                  var e = new Stopwatch();
                                  e.Start();

                                  var buffer = MandelbrotProvider.DrawMandelbrotSet(shift);

                                  var k = 0;
                                  for (int i = 0; i < zDefaultWidth; i++)
                                      for (int j = 0; j < zDefaultHeight; j++)
                                      {
                                          var i4 = i * 4;
                                          var j4 = j * 4;


                                          frame[(i4 + j4 * zDefaultWidth + 2)] = (byte)((buffer[k] >> (0 * 8)) & 0xff);
                                          frame[(i4 + j4 * zDefaultWidth + 1)] = (byte)((buffer[k] >> (1 * 8)) & 0xff);
                                          frame[(i4 + j4 * zDefaultWidth + 0)] = (byte)((buffer[k] >> (2 * 8)) & 0xff);
                                          frame[(i4 + j4 * zDefaultWidth + 3)] = 0xff;

                                          k++;
                                      }

                                  ze.ports.WithEach(port => port.postMessage(frame));

                                  //Console.WriteLine("worker: " + new { shift, e.ElapsedMilliseconds });

                                  shift++;
                              };

                     }
                );


                w.postMessage(new { });



                var memory = new Queue<byte[]>();

                for (int i = 0; i < 3; i++)
                    memory.Enqueue(new byte[MandelbrotProvider.DefaultWidth * MandelbrotProvider.DefaultHeight * 4]);

                Native.window.onframe +=
                    delegate
                    {
                        // need a few next frames

                        if (memory.Count > 0)
                        {
                            var x = memory.Dequeue();

                            Action<MessageEvent> yield =
                                e =>
                                {
                                    var xe = new Stopwatch();
                                    xe.Start();


                                    if (frame != null)
                                        memory.Enqueue(frame);

                                    frame = (byte[])e.data;

                                    if (onframe != null)
                                        onframe();

                                    //Console.WriteLine("yield: " + new { xe.ElapsedMilliseconds });
                                };

                            w.postMessage(x, yield);
                        }
                    };

            }
        }

        public Application(IDefault page)
        {
            var MandelbrotAsync = new MandelbrotAsync();


            var context = new CanvasRenderingContext2D(MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight);
            var canvas = context.canvas.AttachToDocument();

            var fpsc = new Stopwatch();
            fpsc.Start();

            var fps = 0;
            Native.window.onframe +=
                delegate
                {
                    if (fpsc.ElapsedMilliseconds < 1000)
                    {
                        fps++;
                    }
                    else
                    {
                        Native.document.title = new { fps }.ToString();
                        fps = 0;
                        fpsc.Restart();
                    }

                    if (MandelbrotAsync.frame == null)
                        return;

                    context.bytes = MandelbrotAsync.frame;
                };



        }

    }
}
