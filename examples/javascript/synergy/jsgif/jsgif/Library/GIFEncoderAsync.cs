using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Extensions;
using System.Threading.Tasks;


[Description("This is the first synergy javascript component we wrapped into a worker")]
public class GIFEncoderAsync
{
    // this class will look like F#

    public static class Actions
    {
        public delegate void addFrame(Uint8ClampedArray data, Action yield = null);
        public delegate void finish(Action<string> yield_src);
    }

    public readonly Actions.addFrame addFrame;
    public readonly Actions.finish finish;

    public GIFEncoderAsync(
        int width,
        int height,
        int delay = 1000 / 15,
        int repeat = 0
        )
    {
        var xscope = new { width, height, repeat, delay };

        Console.WriteLine("will share scope data: " + xscope);

        var w = new Worker(
            scope =>
            {
                var encoder = default(GIFEncoder);
                Console.WriteLine("waiting for scope data");

                scope.onmessage +=
                    ze =>
                    {
                        #region once
                        if (encoder != null)
                            return;
                        #endregion


                        Console.WriteLine("new encoder");

                        dynamic zscope = ze.data;

                        int zwidth = zscope.width;
                        int zheight = zscope.height;
                        int zrepeat = zscope.repeat;
                        int zdelay = zscope.delay;

                        Console.WriteLine("got scope data: " + new { zwidth, zheight, zrepeat, zdelay });

                        encoder = new GIFEncoder();
                        encoder.setSize(zwidth, zheight);
                        encoder.setRepeat(zrepeat); //auto-loop
                        encoder.setDelay(zdelay);
                        encoder.start();

                        scope.onmessage +=
                            ee =>
                            {
                                dynamic xdata = ee.data;
                                string message = xdata.message;

                                //Console.WriteLine(new { message });


                                #region addFrame
                                if (message == "addFrame")
                                {
                                    //Console.WriteLinke("addFrame");

                                    // http://stackoverflow.com/questions/8776751/web-worker-dealing-with-imagedata-working-with-firefox-but-not-chrome
                                    //byte[] data = xdata.data;
                                    Uint8ClampedArray data = xdata.data;
                                    //data[
                                    // { data = [object Uint8ClampedArray] } 
                                    //Console.WriteLine(new { data });

                                    encoder.addFrame(data, true);

                                    ee.ports.WithEach(port => port.postMessage("ok"));
                                }
                                #endregion

                                #region finish
                                if (message == "finish")
                                {
                                    Console.WriteLine("finish");

                                    encoder.finish();

                                    var bytes = Encoding.ASCII.GetBytes(encoder.stream().getData());
                                    var src = "data:image/gif;base64," + Convert.ToBase64String(bytes);

                                    ee.ports.WithEach(port => port.postMessage(src));

                                }
                                #endregion

                            };
                    };

            }
        );

        w.postMessage(xscope);

        #region addFrame
        this.addFrame =
            (data, yield) =>
            {

                dynamic xdata = new object();

                xdata.message = "addFrame";
                xdata.data = data;

                // Error	4	Cannot convert lambda expression to type 'ScriptCoreLib.JavaScript.DOM.MessagePort[]' because it is not a delegate type	X:\jsc.svn\examples\javascript\synergy\jsgif\jsgif\Application.cs	171	38	jsgif


                w.postMessage(
                    (object)xdata,
                    (MessageEvent ee) =>
                    {
                        //Console.WriteLine("addFrame done");

                        if (yield != null)
                            yield();
                    }
                );

            };
        #endregion

        #region finish
        this.finish =
            yield_src =>
            {
                w.postMessage(
                      new { message = "finish" },
                      (MessageEvent ee) =>
                      {
                          //Console.WriteLine("finish done");

                          var src = (string)ee.data;

                          //Console.WriteLine(e.Elapsed);
                          //new IHTMLImage { src = src }.AttachToDocument();

                          w.terminate();

                          yield_src(src);
                      }
                   );
            };
        #endregion

    }
}


public class GIFEncoderWorker
{
    public readonly Task<string> Task;

    public GIFEncoderWorker(
        int width,
        int height,
        int delay = 1000 / 15,
        int repeat = 0,
        IEnumerable<byte[]> frames = null
        )
    {
        this.Task = System.Threading.Tasks.Task.Factory.StartNew(
            new { width, height, delay, repeat, frames = frames.ToArray() },
            x =>
            {
                var state = new { x.width, x.height, x.delay, x.repeat, x.frames.Length };

                // { state = { width = 640, height = 480, delay = 100, repeat = 0, Length = 16 } } 
                Console.WriteLine(new { state });

                var encoder = new GIFEncoder();
                encoder.setSize(x.width, x.height);
                encoder.setRepeat(x.repeat); //auto-loop
                encoder.setDelay(x.delay);
                encoder.start();

                x.frames.WithEachIndex(
                    (frame, index) =>
                    {
                        Console.WriteLine("addFrame " + new { index });

                        encoder.addFrame((Uint8ClampedArray)(object)frame, true);
                    }
                );

                Console.WriteLine("finish");

                encoder.finish();

                var bytes = Encoding.ASCII.GetBytes(encoder.stream().getData());
                var src = "data:image/gif;base64," + Convert.ToBase64String(bytes);

                return src;
            }
        );
    }
}