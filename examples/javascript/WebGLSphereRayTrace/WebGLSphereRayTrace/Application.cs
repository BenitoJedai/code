using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
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

        //sealed class __preserveDrawingBuffer
        //{
        //    public bool alpha = false;
        //    public bool preserveDrawingBuffer = true;
        //}

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
                //gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl", new __preserveDrawingBuffer());
                gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl", new { preserveDrawingBuffer = true, alpha = false });

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
            @"Sphere Ray Trace".ToDocumentTitle();

            Native.Window.requestAnimationFrame +=
                delegate
                {
                    var icon = newicon();
                    var img = new IHTMLImage { src = icon };

                    //img.width = Native.Window.Width / 2;
                    //img.height = Native.Window.Height / 2;

#if PackageAsApplication
                    Native.Document.getElementsByTagName("script")
                        .Select(k => (IHTMLScript)k)
                        .FirstOrDefault(k => k.src.EndsWith("/view-source"))
                        .With(
                            source =>
                            {
                    #region PackageAsApplication
                                Action<IHTMLScript, XElement, Action<string>> PackageAsApplication =
                                    (source0, xml, yield) =>
                                    {
                                        new IXMLHttpRequest(
                                            ScriptCoreLib.Shared.HTTPMethodEnum.GET, source0.src,
                                            (IXMLHttpRequest r) =>
                                            {
                                                // store hash
                                                xml.Add(new XElement("link", new XAttribute("rel", "location"), new XAttribute("href", Native.Document.location.hash)));

                    #region script
                                                xml.Add(
                                                    new XElement("script",
                                                        "/* source */"
                                                   )
                                                );

                                                var data = "";


                                                Action later = delegate
                                                {

                                                    data = data.Replace("/* source */", r.responseText);

                                                };
                                                #endregion


                                                //Native.Document.getElementsByTagName("link").AsEnumerable().ToList().ForEach(

                                                xml.Elements("link").ToList().ForEach(
                                                    (XElement link, Action next) =>
                                                    {
                    #region style
                                                        var rel = link.Attribute("rel");
                                                        if (rel.Value != "stylesheet")
                                                        {
                                                            next();
                                                            return;
                                                        }

                                                        var href = link.Attribute("href");

                                                        var placeholder = "/* " + href.Value + " */";

                                                        //page.DragHTM.innerText += " " + placeholder;


                                                        xml.Add(new XElement("style", placeholder));

                                                        new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET, href.Value,
                                                            rr =>
                                                            {

                                                                later += delegate
                                                                {


                                                                    data = data.Replace(placeholder, rr.responseText);

                                                                };

                                                                Console.WriteLine("link Remove");
                                                                link.Remove();

                                                                next();
                                                            }
                                                        );

                                                        #endregion
                                                    }
                                                )(
                                                    delegate
                                                    {


                                                        data = xml.ToString();
                                                        Console.WriteLine("data: " + data);
                                                        later();

                                                        yield(data);
                                                    }
                                                );
                                            }
                                        );

                                    };
                                #endregion


                                PackageAsApplication(
                                     source,
                                     XElement.Parse(new DefaultPage.XMLSourceSource().Text),
                                     data =>
                                     {
                                         var bytes = Encoding.ASCII.GetBytes(data);
                                         var data64 = System.Convert.ToBase64String(bytes);


                                         Native.Document.body.title = "Drag me!";

                                         Native.Document.body.ondragstart +=
                                                 e =>
                                                 {
                                                     //e.dataTransfer.setData("text/plain", "Sphere");

                                                     // http://codebits.glennjones.net/downloadurl/virtualdownloadurl.htm
                                                     //e.dataTransfer.setData("DownloadURL", "image/png:Sphere.png:" + icon);

                                                     e.dataTransfer.setData("DownloadURL", "application/octet-stream:Sphere.htm:data:application/octet-stream;base64," + data64);
                                                     e.dataTransfer.setData("text/html", data);
                                                     e.dataTransfer.setData("text/uri-list", Native.Document.location + "");
                                                     e.dataTransfer.setDragImage(img, img.width / 2, img.height / 2);
                                                 };


                                     }
                                 );
                            }
                    );


#endif


                };


            #endregion

        }
        public Action Dispose;



    }
}
