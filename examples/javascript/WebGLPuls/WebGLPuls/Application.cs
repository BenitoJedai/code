// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using WebGLPuls.HTML.Pages;
using WebGLPuls;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.GLSL;
using WebGLPuls.Shaders;

namespace WebGLPuls
{
    using gl = WebGLRenderingContext;


    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application : ISurface
    {
        // see also: http://meatfighter.com/puls/
        // it only took 2 years :)
        // Revision: 2744
        //Date: Friday, July 23, 2010 4:34:01 PM
        //Added : /templates/TwentyTen/WebGLPuls/WebGLPuls.sln




        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IXDefaultPage page = null)
        {

            InitializeContent(page);
        }

        #region ISurface
        public event Action onframe;

        public event Action<int, int> onresize;

        public event Action<gl> onsurface;
        #endregion


        public Action Dispose;

        private void InitializeContent(IXDefaultPage page = null)
        {
            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            canvas.style.SetLocation(0, 0);


            //http://www.khronos.org/webgl/public-mailing-list/archives/1002/msg00125.html
        

            var gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl");

            #region Dispose
            var IsDisposed = false;

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                canvas.Orphanize();
            };
            #endregion


            var s = new PulsSurface(this);

            this.onsurface(gl);

            #region AtResize
            Action AtResize = delegate
            {
                if (IsDisposed)
                {
                    return;
                }

                canvas.width = Native.Window.Width;
                canvas.height = Native.Window.Height;

                this.onresize(Native.Window.Width, Native.Window.Height);
            };

            AtResize();

            Native.Window.onresize += delegate
            {
                AtResize();
            };
            #endregion



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




            #region loop
            Action loop = null;

            loop = delegate
            {
                if (IsDisposed)
                    return;

                this.onframe();

                Native.Window.requestAnimationFrame += loop;

            };

            Native.Window.requestAnimationFrame += loop;
            #endregion


        }


    }



}
