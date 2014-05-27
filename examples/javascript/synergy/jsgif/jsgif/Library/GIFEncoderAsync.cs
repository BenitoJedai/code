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
using System.Runtime.CompilerServices;
using System.Threading;


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

    public TaskAwaiter<string> GetAwaiter()
    {
        return this.Task.GetAwaiter();
    }

    public GIFEncoderWorker(
        int width,
        int height,
        int delay = 1000 / 15,
        int repeat = 0,

        //object transparentColor = null,

        IEnumerable<byte[]> frames = null,

        Action<int> AtFrame = null
        )
    {
        var progress = (new Progress<int>(
             x =>
             {
                 Console.WriteLine("DOM Progress: " + new { x, Thread.CurrentThread.ManagedThreadId });

                 if (AtFrame != null)
                     AtFrame(x);
             }
          ) as IProgress<int>);


        this.Task = System.Threading.Tasks.Task.Factory.StartNew(
             Tuple.Create(progress,
                    new
                    {
                        width,
                        height,
                        delay,
                        repeat,
                        //transparentColor, 
                        frames = frames.ToArray()
                    }
                )
            ,
            xx =>
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140526/stack

                var src = default(string);

                // is this killing the rewrite?
                Action<int> yield = xx.Item1.Report;

                // wtf?

                //0200003f GIFEncoderWorker
                //script: error JSC1000: Method: <.ctor>b__1, Type: GIFEncoderWorker; emmiting failed : System.NullReferenceException: Object reference not set to an instance of an object.
                //   at jsc.IL2ScriptGenerator.OpCode_newobj(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem[] s) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.OpCodes.Newobj.cs:line 151

                yield(0);

                var x = xx.Item2;
                var state = new
                {
                    x.width,
                    x.height,
                    x.delay,
                    //x.transparentColor, 
                    x.repeat,
                    x.frames.Length
                };

                // { state = { width = 640, height = 480, delay = 100, repeat = 0, Length = 16 } } 
                Console.WriteLine(new { state });

                var encoder = new GIFEncoder();
                encoder.setSize(x.width, x.height);
                encoder.setRepeat(x.repeat); //auto-loop
                encoder.setDelay(x.delay);
                //encoder.setTransparent(x.transparentColor);
                encoder.start();
                //#if OK

                x.frames.WithEachIndex(
                    (frame, index) =>
                    {
                        Console.WriteLine("addFrame " + new { index });


                        encoder.addFrame((Uint8ClampedArray)(object)frame, true);

                        yield(index);
                    }
                );
                //#endif

                Console.WriteLine("finish");

                encoder.finish();

                var bytes = Encoding.ASCII.GetBytes(encoder.stream().getData());

                src = "data:image/gif;base64," + Convert.ToBase64String(bytes);

                return src;
            }
        );
    }
}