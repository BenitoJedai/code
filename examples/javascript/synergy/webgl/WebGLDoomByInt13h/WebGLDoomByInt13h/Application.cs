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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGLDoomByInt13h;
using WebGLDoomByInt13h.Design;
using WebGLDoomByInt13h.HTML.Pages;

namespace WebGLDoomByInt13h
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
            { WebGLDoomByInt13h.Assets.he3d.Scripts ref0; }
            { WebGLDoomByInt13h.Assets.he3d.Shaders ref0; }
            { WebGLDoomByInt13h.Assets.webgldoom.Shaders ref1; }
            { WebGLDoomByInt13h.Assets.webgldoom.Wads ref1; }

            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader ref3; }
            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader_audio ref3; }
            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader_heightmap ref3; }
            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader_level ref3; }
            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader_textures ref3; }
            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader_things ref3; }


            //            glOpts: {
            //    alpha:	true,
            //    antialias:true,
            //    depth:	true,
            //    stencil:true,
            //    premultipliedAlpha: true,
            //    preserveDrawingBuffer: false
            //}

            var he3d = page.he3d;



            page.he3d.style.border = "2px solid red";
            page.v.onmouseover +=
                delegate
                {

                    page.he3d.style.border = "2px solid yellow";
                };

            page.v.onmouseout +=
              delegate
              {

                  page.he3d.style.border = "2px solid blue";
              };

            //Action doPatch = delegate
            //{
            Console.WriteLine("patching...");
            IFunction getContext = (page.he3d as dynamic).getContext;

            Func<string, object, object> patched_getContext =
                (x, glOpts) =>
                {
                    (glOpts as dynamic).preserveDrawingBuffer = true;

                    Console.WriteLine("at patched_getContext");

                    return getContext.apply(page.he3d,
                        x,
                        glOpts
                    );
                };

            (page.he3d as dynamic).getContext = (IFunction)patched_getContext;
            (page.he3d as dynamic).getContextPatched = true;
            Console.WriteLine("patching... done");
            //};

    


            Native.window.navigator.getUserMedia(
                stream =>
                {
                    page.v.src = stream.ToObjectURL();
                    page.v.play();
                }
            );


            #region onclick
            page.v.onclick +=
                delegate
                {


                    Console.WriteLine("rec...");



                    var frames = new List<byte[]>();
                    var x = page.he3d.clientWidth;
                    var y = page.he3d.clientHeight;

                    new Timer(
                        tt =>
                        {
                            var bytes = page.he3d.bytes;

                            Console.WriteLine(new { tt.Counter, bytes.Length });

                            // script: error JSC1000: No implementation found for this native method, please implement
                            // [System.Threading.Tasks.TaskFactory`1.
                            // StartNew(, System.Object, System.Threading.CancellationToken, System.Threading.Tasks.TaskCreationOptions, System.Threading.Tasks.TaskScheduler)]

                            frames.Add(bytes);

                            if (tt.Counter == 4)
                            {
                                tt.Stop();

                                var e = new Stopwatch();
                                e.Start();

                                new GIFEncoderWorker(
                                     x,
                                     y,
                                      delay: 1000 / 10,
                                     frames: frames
                                 ).Task.ContinueWith(
                                     t =>
                                     {
                                         var src = t.Result;

                                         Console.WriteLine("done!");
                                         Console.WriteLine(e.Elapsed);

                                         Native.document.body.style.position = IStyle.PositionEnum.absolute;
                                         Native.document.body.style.zIndex = 1000;

                                         new IHTMLImage { src = src }.AttachToDocument();


                                         Console.WriteLine(new { e.Elapsed });
                                     }
                                 );
                            }
                        }
                    ).StartInterval(1000 / 15);

                };
            #endregion


            // wow. found it. that script is making a new copy of our dom. not nice

            // document.getElementsByTagName('body')[0].innerHTML += '<div id="console" class="console">'

            // Uncaught NotFoundError: An attempt was made to reference a Node in a context where it does not exist. 


            Native.document.body.parentNode.insertBefore(
                new IHTMLBody(),
                Native.document.body
            );



            // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.Cast(System.Collections.IEnumerable)]

            new AppCode().body.ScriptElements().With(
                async s =>
                {
                    Console.WriteLine("load em up!");
                    await s;
                    Console.WriteLine("load em up! done!");

                    new IFunction(@"


var DOMContentLoaded_event = document.createEvent('Event')
DOMContentLoaded_event.initEvent('DOMContentLoaded', true, true)
window.document.dispatchEvent(DOMContentLoaded_event)
").apply(null);
                }
            );


        }


    }
}


public static class X
{
    public static void DebuggerBreakIfMissing(this object i)
    {
        if (i == null)
            Debugger.Break();
    }

    public static Task<IHTMLScript> ToTask(this IHTMLScript i)
    {
        var y = new TaskCompletionSource<IHTMLScript>();

        i.onload +=
            delegate
            {
                y.SetResult(i);
            };

        i.AttachToHead();

        return y.Task;
    }

    public static IEnumerable<IHTMLScript> ScriptElements(this IElement i)
    {
        return i.querySelectorAll(IHTMLElement.HTMLElementEnum.script).Select(k => (IHTMLScript)k);
    }

    public static TaskAwaiter<IHTMLScript[]> GetAwaiter(this IEnumerable<IHTMLScript> i)
    {
        var script = i
            //.Where(x => x.nodeName.ToLower() == "script")
            .Select(x => ((IHTMLScript)x).ToTask());

        var y = Task.WhenAll(script);

        return y.GetAwaiter();
    }

    public static TaskAwaiter<TResult[]> GetAwaiter<TResult>(this IEnumerable<Task<TResult>> i)
    {
        return Task.WhenAll(i).GetAwaiter();
    }
}
