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
using WebGLToAnimatedGIFExperiment.HTML.Pages;
using jsgif;
using jsgif.Library;

namespace WebGLToAnimatedGIFExperiment
{
    using ScriptCoreLib.JavaScript.Runtime;
    using System.Diagnostics;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using WebGLToAnimatedGIFExperiment.HTML.Images.FromAssets;



    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application :
        // should jsc promote inheritance? would enable html component designer scenario
        App.FromDocument, ISurface
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

            ani2.gl.canvas.AttachTo(this.e1);





            var gl = new WebGLRenderingContext(alpha: false, preserveDrawingBuffer: true);

            // replace placeholder
            gl.canvas.id = canvas.id;
            canvas = gl.canvas;

            canvas.width = 96;
            canvas.height = 96;

            var s = new SpiralSurface(this);

            this.onsurface(gl);



            this.onresize(canvas.width, canvas.height);

            var st = new Stopwatch();
            st.Start();

            Native.window.onframe += delegate
            {
                s.ucolor_1 = (float)Math.Sin(st.ElapsedMilliseconds * 0.001) * 0.5f + 0.5f;

                this.onframe();
            };






            // jsc should link that js file once we reference it. for now its manual
            new jsgif.Design.b64().Content.WhenAvailable(
                   delegate
                   {

                       new jsgif.Design.LZWEncoder().Content.WhenAvailable(
                            delegate
                            {

                                new jsgif.Design.NeuQuant().Content.WhenAvailable(
                                   delegate
                                   {

                                       new jsgif.Design.GIFEncoder().Content.WhenAvailable(
                                          delegate
                                          {

                                              Action<WebGLRenderingContext> activate =
                                                  context =>
                                                  {



                                                      context.canvas.style.border = "2px solid yellow";
                                                      context.canvas.style.cursor = IStyle.CursorEnum.pointer;




                                                      context.canvas.onclick +=
                                                           delegate
                                                           {
                                                               var c0 = new CanvasRenderingContext2D();

                                                               // no scale!
                                                               c0.canvas.width = 96;
                                                               c0.canvas.height = 96;
                                                               c0.canvas.style.SetSize(96, 96);

                                                               c0.canvas.AttachToDocument();

                                                               page.output.Clear();


                                                               var encoder = new GIFEncoder();

                                                               encoder.setRepeat(0); //auto-loop

                                                               encoder.setDelay(1000 / 60);
                                                               encoder.start();

                                                               var t = new Timer();

                                                               t.Tick +=
                                                                   delegate
                                                                   {
                                                                       if (t.Counter == 60 * 4)
                                                                       {
                                                                           t.Stop();


                                                                           encoder.finish();

                                                                           var image = new IHTMLImage();
                                                                           image.src = "data:image/gif;base64," + b64.Window.encode64(encoder.stream().getData());
                                                                           image.AttachToDocument();

                                                                           return;
                                                                       }

                                                                       #region force redraw all
                                                                       s.ucolor_2 = t.Counter / 32.0f;

                                                                       // force redraw
                                                                       this.onframe();
                                                                       #endregion


                                                                       var icon = new IHTMLImage(context.canvas.toDataURL("image/png"));

                                                                       icon.InvokeOnComplete(
                                                                           delegate
                                                                           {
                                                                               if (!t.IsAlive)
                                                                                   return;

                                                                               icon.AttachTo(page.output);
                                                                               page.output.ScrollToBottom();

                                                                               c0.drawImage(icon, 0, 0, 96, 96);

                                                                               encoder.addFrame(c0);
                                                                           }
                                                                       );



                                                                   };

                                                               t.StartInterval(1000 / 60);

                                                           };
                                                  };

                                              activate(gl);
                                              activate(ani2.gl);
                                          }
                                        );
                                   }
                                );
                            }
                         );
                   }
                );




        }

    }
}
