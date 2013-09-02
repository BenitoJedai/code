using jsgif;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLSpiral.Shaders;
using WebGLToAnimatedGIFExperiment.Design;
using WebGLToAnimatedGIFExperiment.HTML.Images.FromAssets;
using WebGLToAnimatedGIFExperiment.HTML.Pages;
using System.Diagnostics;

namespace WebGLToAnimatedGIFExperiment
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using System.Collections.Generic;
    using System.Threading.Tasks;



    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application :
        // should jsc promote inheritance? would enable html component designer scenario
        //App.FromDocument,

        ISurface
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        #region ISurface
        public event Action onframe;

        public event Action<int, int> onresize;

        public event Action<gl> onsurface;
        #endregion


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // : ScriptComponent
            var ani2 = new WebGLEscherDrosteEffect.Application();

            ani2.gl.canvas.AttachTo(page.e1);





            var gl = new WebGLRenderingContext(alpha: false, preserveDrawingBuffer: true);

            // replace placeholder
            gl.canvas.id = page.canvas.id;
            page.canvas = gl.canvas;

            page.canvas.width = 96;
            page.canvas.height = 96;

            var s = new SpiralSurface(this);

            this.onsurface(gl);



            this.onresize(page.canvas.width, page.canvas.height);

            var st = new Stopwatch();
            st.Start();

            Native.window.onframe += delegate
            {
                s.ucolor_1 = (float)Math.Sin(st.ElapsedMilliseconds * 0.001) * 0.5f + 0.5f;

                this.onframe();
            };






            // jsc should link that js file once we reference it. for now its manual


            Action<WebGLRenderingContext> activate =
                context =>
                {
                    context.canvas.style.border = "2px solid yellow";
                    context.canvas.style.cursor = IStyle.CursorEnum.pointer;

                    context.canvas.onclick +=
                         delegate
                         {
                             var c0 = new CanvasRenderingContext2D(96, 96);
                             c0.canvas.AttachToDocument();

                             var frames = new List<byte[]>();

                             new ScriptCoreLib.JavaScript.Runtime.Timer(
                                 async t =>
                                 {
                                     if (t.Counter == 60)
                                     {
                                         Console.WriteLine("GIFEncoderWorker!");


                                         var src = await new GIFEncoderWorker(
                                                 96,
                                                 96,
                                                  delay: 1000 / 60,
                                                 frames: frames
                                         );

                                         Console.WriteLine("done!");

                                         new IHTMLImage { src = src }.AttachToDocument();
                                    
                                         return;
                                     }

                                     if (t.Counter >= 60)
                                     {
                                         c0.bytes = frames[t.Counter % frames.Count];

                                         return;
                                     }

                                     #region force redraw all
                                     s.ucolor_2 = t.Counter / 32.0f;

                                     // force redraw
                                     this.onframe();
                                     #endregion


                                     if (!t.IsAlive)
                                         return;

                                     c0.drawImage(context.canvas, 0, 0, 96, 96);

                                     frames.Add(c0.getImageData().data);



                                 }
                              ).StartInterval(1000 / 60);

                         };
                };

            activate(gl);
            activate(ani2.gl);





        }

    }
}
