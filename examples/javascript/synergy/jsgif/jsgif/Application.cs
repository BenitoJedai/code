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
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;
using System.Collections.Generic;
//using jsgif.opensource;

namespace jsgif
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Tuple.cs

        //no implementation for System.Tuple b6efdcc2-6386-375e-84aa-6732b6518b3f
        //script: error JSC1000: No implementation found for this native method, please implement [static System.Tuple.Create(System.IProgress`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=ne
        //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
        //script: error JSC1000: error at GIFEncoderWorker..ctor,
        // assembly: U:\jsgif.Application.exe
        // type: GIFEncoderWorker, jsgif.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x0049
        //  method:Void .ctor(Int32, Int32, Int32, Int32, System.Collections.Generic.IEnumerable`1[System.Byte[]], System.Action`1[System.Int32])
        //*** Compiler cannot continue... press enter to quit.


        // based on http://antimatter15.com/wp/2010/07/javascript-to-animated-gif/

        //cript: error JSC1000: No implementation found for this native method, please implement [static System.Tuple.Create(System.IProgress`1[[System.Int32,
        //cript: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
        //cript: error JSC1000: error at GIFEncoderWorker..ctor,
        //assembly: V:\jsgif.Application.exe
        //type: GIFEncoderWorker, jsgif.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        //offset: 0x0049
        // method:Void .ctor(Int32, Int32, Int32, Int32, System.Collections.Generic.IEnumerable`1[System.Byte[]], System.Action`1[System.Int32])
        //** Compiler cannot continue... press enter to quit.



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            // https://bugzilla.mozilla.org/show_bug.cgi?id=709490
            // http://stackoverflow.com/questions/7844886/using-webgl-from-inside-a-web-worker-is-it-possible-how

            const int x = 640;
            const int y = 480;
            // do we need automatic decomposer?

            var context = new CanvasRenderingContext2D(x, y);

            // needs to be in dom? no
            var canvas = context.canvas;


            context.fillStyle = "rgb(255,255,255)";
            context.fillRect(0, 0, canvas.width, canvas.height);

			// rebuild redux?
            var my_gradient = context.createLinearGradient(0, 0, 16, 0);
            my_gradient.addColorStop(0, "blue");
            my_gradient.addColorStop(1, "white");
            //context.fillStyle = my_gradient; //"rgb(255,255,255)";  
            context.fillStyle = "rgb(255,255,255)";  
            context.fillRect(0, 0, 16, canvas.height); //GIF can't do transparent so do white

            Action yield = delegate { };

            var r = new Random();

            Func<byte> random = r.NextByte;


            //encoder.addFrame(context);


            // 4 : 3sec
            // 8 : 0.00:00:06 
            // 16 : 0.00:00:18 

            var frames = new List<byte[]>();

            // whats the difference? should jsc switch to tyhe typed array yet?
            //var frames = new List<byte[]>();

            for (int i = 0; i < 16; i++)
            {
                context.fillStyle = "rgb(" + random() + "," + random() + "," + random() + ")";
                context.fillRect(
                    random() / 4,
                    random() / 4,
                    32 + random(),
                    32 + random()
                );

                var data = context.getImageData().data;

                //Uint8Array
                // { data = [object Uint8ClampedArray] } 
                Console.WriteLine(new { data });

                frames.Add(data);


            }

            yield += delegate
            {

            };

            new IHTMLButton { innerText = "encode!" }.AttachToDocument().WhenClicked(
                delegate
                {
                    var e = new Stopwatch();
                    e.Start();


                    Console.WriteLine("new encoder");

                    var encoder = new GIFEncoder();
                    encoder.setSize(x, y);
                    encoder.setRepeat(0); //auto-loop
                    encoder.setDelay(1000 / 15);
                    encoder.start();

                    foreach (var data in frames)
                    {
                        Console.WriteLine("addFrame");
                        encoder.addFrame((Uint8ClampedArray)(object)data, true);
                    }

                    Console.WriteLine("finish");

                    encoder.finish();

                    var bytes = Encoding.ASCII.GetBytes(encoder.stream().getData());
                    var src = "data:image/gif;base64," + Convert.ToBase64String(bytes);

                    Console.WriteLine(e.Elapsed);

                    new IHTMLImage { src = src }.AttachToDocument();
                }
            );

            new IHTMLButton { innerText = "encode via worker" }.AttachToDocument().WhenClicked(
                 delegate
                 {
                     var e = new Stopwatch();
                     e.Start();

                     var w = new Worker(
                         scope =>
                         {
                             Console.WriteLine("new encoder");

                             var encoder = new GIFEncoder();
                             encoder.setSize(x, y);
                             encoder.setRepeat(0); //auto-loop
                             encoder.setDelay(1000 / 15);
                             encoder.start();

                             scope.onmessage +=
                                 ee =>
                                 {
                                     dynamic xdata = ee.data;
                                     string message = xdata.message;

                                     //Console.WriteLine(new { message });

                                     if (message == "addFrame")
                                     {
                                         Console.WriteLine("addFrame");

                                         // http://stackoverflow.com/questions/8776751/web-worker-dealing-with-imagedata-working-with-firefox-but-not-chrome
                                         //byte[] data = xdata.data;
                                         Uint8ClampedArray data = xdata.data;
                                         //data[
                                         // { data = [object Uint8ClampedArray] } 
                                         //Console.WriteLine(new { data });

                                         encoder.addFrame(data, true);


                                     }

                                     if (message == "finish")
                                     {
                                         Console.WriteLine("finish");

                                         encoder.finish();

                                         var bytes = Encoding.ASCII.GetBytes(encoder.stream().getData());
                                         var src = "data:image/gif;base64," + Convert.ToBase64String(bytes);

                                         ee.ports.WithEach(port => port.postMessage(src));

                                     }
                                 };
                         }
                     );





                     foreach (var data in frames)
                     {

                         dynamic xdata = new object();

                         xdata.message = "addFrame";
                         xdata.data = data;

                         // Error	4	Cannot convert lambda expression to type 'ScriptCoreLib.JavaScript.DOM.MessagePort[]' because it is not a delegate type	X:\jsc.svn\examples\javascript\synergy\jsgif\jsgif\Application.cs	171	38	jsgif


                         w.postMessage(
                             (object)xdata,
                             (MessageEvent ee) =>
                             {
                                 // ?
                                 Console.WriteLine("addFrame done");
                             }
                         );
                     }

                     w.postMessage(
                        new { message = "finish" },
                        (MessageEvent ee) =>
                        {
                            Console.WriteLine("finish done");

                            var src = (string)ee.data;

                            Console.WriteLine(e.Elapsed);
                            new IHTMLImage { src = src }.AttachToDocument();

                            w.terminate();
                        }
                     );



                     //var bytes = Encoding.ASCII.GetBytes(encoder.stream().getData());
                     //var src = "data:image/gif;base64," + Convert.ToBase64String(bytes);


                     //new IHTMLImage { src = src }.AttachToDocument();
                 }
             );

            new IHTMLButton { innerText = "encode via GIFEncoderAsync" }.AttachToDocument().WhenClicked(
                btn =>
                {
                    btn.disabled = true;
                    var e = new Stopwatch();
                    e.Start();

                    var w = new GIFEncoderAsync(
                        width: x,
                        height: y,
                        delay: 1000 / 10
                    );

                    //http://www.w3schools.com/tags/tryit.asp?filename=tryhtml5_meter

                    var innerText = btn.innerText;

                    btn.Clear();

                    dynamic meter = new IHTMLElement("meter").AttachTo(btn);

                    meter.max = frames.Count;
                    var value = 0;

                    foreach (var data in frames)
                    {
                        w.addFrame((Uint8ClampedArray)(object)data,
                            delegate
                            {
                                Console.WriteLine("addFrame done");

                                value++;

                                meter.value = value;
                            }
                        );
                    }

                    w.finish(
                        src =>
                        {
                            Console.WriteLine("finish done");


                            Console.WriteLine(e.Elapsed);
                            new IHTMLImage { src = src }.AttachToDocument();

                            btn.innerText = innerText;

                            btn.disabled = false;
                            btn.title = new { e.Elapsed }.ToString();

                        }
                    );


                }
            );


            new IHTMLButton { innerText = "encode via GIFEncoderWorker" }.AttachToDocument().WhenClicked(
                async btn =>
                {
                    Console.WriteLine("encoding!");

                    btn.disabled = true;
                    var e = new Stopwatch();
                    e.Start();

                    var src = await new GIFEncoderWorker(
                          x,
                          y,
                           delay: 1000 / 10,
                          frames: frames,

                          AtFrame:
                            index =>
                            {
                                btn.innerText = new { index, frames.Count }.ToString();

                            }
                      );



                    Console.WriteLine("done!");
                    Console.WriteLine(e.Elapsed);

                    new IHTMLImage { src = src }.AttachToDocument();


                    btn.disabled = false;
                    btn.title = new { e.Elapsed }.ToString();

                }
            );

        }

    }


}
