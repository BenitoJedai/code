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
using com.abstractatech.spiral.Design;
using com.abstractatech.spiral.HTML.Pages;

namespace com.abstractatech.spiral
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using WebGLSpiral.Shaders;
    using ScriptCoreLib.JavaScript.WebGL;


    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ISurface
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
            "Spiral".ToDocumentTitle();

            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0);

            #region gl

            var gl = default(WebGLRenderingContext);

            try
            {

                gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl");

            }
            catch { }

            if (gl == null)
            {
                //Native.Window.alert("WebGL not supported");
                //throw new InvalidOperationException("cannot create webgl context");
                return;
            }
            #endregion

            #region Dispose
            var IsDisposed = false;

            //Dispose = delegate
            //{
            //    if (IsDisposed)
            //        return;

            //    IsDisposed = true;

            //    canvas.Orphanize();
            //};
            #endregion


            var s = new SpiralSurface(this);

            this.onsurface(gl);

            #region AtResize
            Action AtResize = delegate
            {
                if (IsDisposed)
                {
                    return;
                }

                canvas.width = Native.window.Width;
                canvas.height = Native.window.Height;

                this.onresize(Native.window.Width, Native.window.Height);
            };

            AtResize();

            Native.window.onresize += delegate
            {
                AtResize();
            };
            #endregion


          

            Native.window.onframe += delegate { this.onframe(); };


            #region requestFullscreen
            Native.Document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    Native.Document.body.requestFullscreen();
                };
            #endregion




            Native.Document.onmousemove +=
                e =>
                {
                    s.ucolor_1 = e.CursorX / Native.window.Width;
                    s.ucolor_2 = e.CursorY / Native.window.Height;
                };

        }

    }
}
