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
using WebGLSphereRayTrace.Design;
using WebGLSphereRayTrace.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using WebGLSphereRayTrace.Shaders;
using ScriptCoreLib.GLSL;
using System.Collections.Generic;

namespace WebGLSphereRayTrace
{
    using gl = WebGLRenderingContext;



    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ISurface
    {
        // inspired by http://people.mozilla.com/~sicking/webgl/ray.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page = null)
        {

            InitializeContent();
        }

        #region ISurface
        public event Action onframe;

        public event Action<int, int> onresize;

        public event Action<gl> onsurface;
        #endregion

        sealed class __preserveDrawingBuffer
        {
            public bool alpha = false;
            public bool preserveDrawingBuffer = true;
        }

        public void InitializeContent()
        {
            #region canvas 3D
            var canvas = new IHTMLCanvas();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            canvas.AttachToDocument();
            canvas.style.SetLocation(0, 0);
            canvas.style.backgroundColor = "black";

            // Initialise WebGL

            var gl = default(WebGLRenderingContext);

            try
            {
                // if canvas object makes use of toDataUrl then this arg is required!
                gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl", new __preserveDrawingBuffer());

            }
            catch { }

            if (gl == null)
            {
                Native.Window.alert("WebGL not supported");
                throw new InvalidOperationException("cannot create webgl context");
            }
            #endregion

            var s = new RaySurface(this);

            this.onsurface(gl);



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

            Func<string> newicon = delegate
            {
                var icon = canvas.toDataURL("image/png");

                Native.Document.getElementsByTagName("link").AsEnumerable().ToList().WithEach(
                    e =>
                    {
                        var link = (IHTMLLink)e;

                        if (link.rel == "icon")
                        {
                            if (link.type == "image/png")
                            {

                                link.href = icon;
                            }
                            else
                            {
                                link.Orphanize();
                            }
                        }
                    }
                );

                return icon;
            };

            Action loop = null;

            loop = delegate
            {
                if (IsDisposed)
                    return;

                this.onframe();

                Native.Window.requestAnimationFrame += loop;


            };

            Native.Window.requestAnimationFrame += loop;

            Native.Document.body.onclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    newicon();
                };


            Native.Document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;


                    Native.Document.body.requestFullscreen();
                };

            #region draggable

            // http://www.thecssninja.com/javascript/gmail-dragout
            // can we drag ourself into crx?
            Native.Document.body.title = "Drag me!";
            Native.Document.body.draggable = true;
            @"Sphere Ray Trace".ToDocumentTitle();

            Native.Window.requestAnimationFrame +=
                delegate
                {
                    var icon = newicon();
                    var img = new IHTMLImage { src = icon };

                    //img.width = Native.Window.Width / 2;
                    //img.height = Native.Window.Height / 2;

                    Native.Document.body.ondragstart +=
                        e =>
                        {
                            //e.dataTransfer.setData("text/uri-list", Native.Document.location.ToString());
                            e.dataTransfer.setData("text/plain", "Sphere");
                            e.dataTransfer.setData("text/uri-list", Native.Document.location + "");

                            // http://codebits.glennjones.net/downloadurl/virtualdownloadurl.htm

                            e.dataTransfer.setData("DownloadURL", "image/png:Sphere.png:" + icon);
                            //e.dataTransfer.setData("DownloadURL", img);

                            e.dataTransfer.setDragImage(img, img.width / 2, img.height / 2);

                        };

                };


            #endregion

        }
        public Action Dispose;



    }
}
