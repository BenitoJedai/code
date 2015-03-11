using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkerMD5Experiment;
using WorkerMD5Experiment.Design;
using WorkerMD5Experiment.HTML.Pages;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Diagnostics;
using System.Threading;

namespace WorkerMD5Experiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeWebGLExtensions\ChromeWebGLExtensions\Application.cs

			// http://link.springer.com/chapter/10.1007%2F978-3-319-02726-5_20
			// roslyn broke worker support?
			// Uncaught TypeError: c._3BYABlqcAz6k53tGgDEanQ is not a function

			var gl = new WebGLRenderingContext();

            // http://webglreport.com/
            //       unMaskedRenderer: getUnmaskedInfo(gl).renderer,
            //<th>Unmasked Renderer:</th>
            //			<td><%= report.unMaskedRenderer %></td>

            var UNMASKED_RENDERER_WEBGL = "";
            var WEBGL_debug_renderer_info = new
            {
                UNMASKED_RENDERER_WEBGL = 0x9246u
            };


//            02000509 ScriptCoreLib.Shared.BCLImplementation.System.Linq.__OrderedEnumerable`1 +<> c__DisplayClass0
//{ SourceMethod = Int32 < GetEnumerator > b__1(TSource, TSource) }
//        script: error JSC1000: unknown opcode brtrue.s at < GetEnumerator > b__1 + 0x002f

            var dbgRenderInfo = gl.getExtension("WEBGL_debug_renderer_info");
            if (dbgRenderInfo != null)
            {
                // https://www.khronos.org/registry/webgl/extensions/WEBGL_debug_renderer_info/
                UNMASKED_RENDERER_WEBGL = (string)gl.getParameter(WEBGL_debug_renderer_info.UNMASKED_RENDERER_WEBGL);
            }


            new IHTMLButton { "do MD5" }.AttachToDocument().onclick +=
                async a =>
                {
                    var data = "whats the hash for this?";

                    var z = await Task.Run(
                        delegate
                        {
                            // 20140629 level1 scope sharing!

                            var bytes = Encoding.UTF8.GetBytes(data);

                            var s = Stopwatch.StartNew();

                            // { data = "{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 41 }" }

                            // { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 1268 }
                            // { i = 255, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 170 }

                            // {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 245, ManagedThreadId = 10 }}
                            // laptop {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 439, ManagedThreadId = 10 }}

                            // on red server. how fast is the laptop?
                            // laptop wont trust the server ssl?
                            // certs are configured via certmgr.msc 
                            // after export and import the laptop should now be able to trust the ssl?

                            var scope = new { data };

                            for (int i = 0; i < 0x1000; i++)
                            {

                                var hash = bytes.ToMD5Bytes();
                                var hex = hash.ToHexString();

                                //scope = new { data = new { i, hex, s.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId, Environment.ProcessorCount }.ToString() };
                                scope = new { data = new { i, hex, s.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId }.ToString() };

                            }


                            return scope;
                        }
                    );

                    // show proof of work
                    //a.Element.innerText = z.data;


                    //Environment.OSVersion.
                    var winver = Native.window.navigator.userAgent.SkipUntilOrEmpty("(Windows ").TakeUntilOrEmpty(")");




                    new IHTMLPre {
                        // ProcessorCount allows to know if we are on our lite laptop or the server
                        new {
                            Environment.ProcessorCount,
                            winver,
                            UNMASKED_RENDERER_WEBGL,
                            Native.window.navigator.userAgent,
                            //Native.window.navigator.mem
                            z.data }
                    }.AttachToDocument();

                    // {{ ProcessorCount = 8, userAgent = Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.99 Safari/537.36, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 252, ManagedThreadId = 10 }} }}

                    // red server 2008r2:
                    // {{ ProcessorCount = 8, winver = NT 6.1; WOW64, UNMASKED_RENDERER_WEBGL = ANGLE (Intel(R) HD Graphics 4000 Direct3D11 vs_5_0 ps_5_0), userAgent = Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.99 Safari/537.36, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 255, ManagedThreadId = 10 }} }}
                    // lenovo 8.1 battery
                    // {{ ProcessorCount = 4, winver = NT 6.3; Win64; x64, UNMASKED_RENDERER_WEBGL = ANGLE (Intel(R) HD Graphics Family Direct3D11 vs_5_0 ps_5_0), userAgent = Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2275.2 Safari/537.36, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 383, ManagedThreadId = 10 }} }}
                    // windows7
                    // {{ ProcessorCount = 4, winver = NT 6.1; Win64; x64, UNMASKED_RENDERER_WEBGL = ANGLE (Intel(R) HD Graphics Family Direct3D9Ex vs_3_0 ps_3_0), userAgent = Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2280.2 Safari/537.36, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 453, ManagedThreadId = 10 }} }}

                    // {{ ProcessorCount = 8, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 246, ManagedThreadId = 10 }} }}
                    //{ { ProcessorCount = 8, data = { { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 240, ManagedThreadId = 11 } } } }
                    //{ { ProcessorCount = 8, data = { { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 240, ManagedThreadId = 12 } } } }

                    // high profile/ac mode
                    //{ { ProcessorCount = 4, data = { { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 445, ManagedThreadId = 10 } } } }
                    //{ { ProcessorCount = 4, data = { { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 426, ManagedThreadId = 11 } } } }
                    //{ { ProcessorCount = 4, data = { { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 422, ManagedThreadId = 12 } } } }

                    // battery/no power
                    // {{ ProcessorCount = 4, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 1253, ManagedThreadId = 13 }} }}
                    // {{ ProcessorCount = 4, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 1350, ManagedThreadId = 14 }} }}
                    // {{ ProcessorCount = 4, data = {{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 1550, ManagedThreadId = 15 }} }}


                };

        }

    }
}
