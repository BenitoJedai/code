using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CanvasPlasma.HTML.Pages;
//using CanvasPlasma.Styles;
using CanvasPlasma.Library;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;
using System.Collections.Generic;

namespace CanvasPlasma
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        // port from Z:\jsc.svn\examples\actionscript\FlashPlasma\FlashPlasmaDocument\js\OrcasScriptApplication.cs
        // code could also be shared via windows forms and java applet

        public readonly ApplicationWebService service = new ApplicationWebService();

        //public readonly DefaultStyle style = new DefaultStyle();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            InitializeContent();

            //style.Content.AttachToHead();
            @"Plasma".ToDocumentTitle();

        }




        class PlasmaAsync
        {
            public readonly Action Dispose;

            public byte[] frame { private set; get; }


            public event Action onframe;

            public PlasmaAsync(int _DefaultWidth, int _DefaultHeight)
            {
                var xscope = new { DefaultWidth = _DefaultWidth, DefaultHeight = _DefaultHeight };


                var w = new Worker(
                    scope =>
                    {
                        Console.WriteLine("waiting for scope data");

                        int shift = 0;
                        int zDefaultWidth = 0;
                        int zDefaultHeight = 0;

                        var once = false;
                        Action<object> init =
                            data =>
                            {
                                int zzDefaultWidth = (data as dynamic).DefaultWidth;
                                int zzDefaultHeight = (data as dynamic).DefaultHeight;

                                zDefaultWidth = zzDefaultWidth;
                                zDefaultHeight = zzDefaultHeight;
                            };


                        scope.onmessage +=
                            ze =>
                            {
                                #region waiting for scope data
                                if (!once)
                                {
                                    once = true;

                                    init(ze.data);


                                    Plasma.generatePlasma(zDefaultWidth, zDefaultHeight);

                                    return;
                                }
                                #endregion



                                var frame = (byte[])ze.data;

                                Console.WriteLine("got frame: " + new { frame.Length });

                                var e = new Stopwatch();
                                e.Start();


                                var buffer = Plasma.shiftPlasma(shift);

                                var k = 0;
                                for (int i = 0; i < zDefaultWidth; i++)
                                    for (int j = 0; j < zDefaultHeight; j++)
                                    {
                                        var i4 = i * 4;
                                        var j4 = j * 4;


                                        frame[(i4 + j4 * zDefaultWidth + 2)] = (byte)((buffer[k] >> (0 * 8)) & 0xff);
                                        frame[(i4 + j4 * zDefaultWidth + 1)] = (byte)((buffer[k] >> (1 * 8)) & 0xff);
                                        frame[(i4 + j4 * zDefaultWidth + 0)] = (byte)((buffer[k] >> (2 * 8)) & 0xff);
                                        frame[(i4 + j4 * zDefaultWidth + 3)] = 0xff;

                                        k++;
                                    }

                                ze.ports.WithEach(port => port.postMessage(frame));

                                //Console.WriteLine("worker: " + new { shift, e.ElapsedMilliseconds });

                                shift++;

                            };
                    }
                );

                w.postMessage(xscope);


                var memory = new Queue<byte[]>();

                for (int i = 0; i < 3; i++)
                    memory.Enqueue(new byte[_DefaultWidth * _DefaultHeight * 4]);

                Native.window.onframe +=
                    delegate
                    {
                        // need a few next frames

                        if (memory.Count > 0)
                        {
                            var x = memory.Dequeue();

                            Action<MessageEvent> yield =
                                e =>
                                {
                                    var xe = new Stopwatch();
                                    xe.Start();


                                    if (frame != null)
                                        memory.Enqueue(frame);

                                    frame = (byte[])e.data;

                                    Console.WriteLine("got new frame: " + new { frame.Length });

                                    if (onframe != null)
                                        onframe();

                                    //Console.WriteLine("yield: " + new { xe.ElapsedMilliseconds });
                                };

                            Console.WriteLine("send frame: " + new { x.Length, x });
                            w.postMessage(x, yield);
                        }
                    };

                Dispose = () => { w.terminate(); memory.Clear(); };

            }
        }

        public void InitializeContent()
        {
            // now can we supply this to css3d, webgl?

            // fullscreen 20fps
            // fullscreen 50fps, 4wfps
            // fullscreen 38fps, 8wfps


            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;

            var DefaultWidth = Native.window.Width;
            var DefaultHeight = Native.window.Height;

            var PlasmaAsync = new PlasmaAsync(DefaultWidth, DefaultHeight);

            //Plasma.generatePlasma(DefaultWidth, DefaultHeight);
            //var shift = 0;



            var context = new CanvasRenderingContext2D();


            var canvas = context.canvas;

            canvas.width = DefaultWidth;
            canvas.height = DefaultHeight;

            canvas.style.position = IStyle.PositionEnum.absolute;
            canvas.style.SetLocation(0, 0, DefaultWidth, DefaultHeight);



            var fpsca = new Stopwatch();
            fpsca.Start();
            var fpsa = 0;
            var fpsavalue = 0;

            var fpsc = new Stopwatch();
            fpsc.Start();

            var fps = 0;
            var xxx = 0;
            var yyy = 0;

            PlasmaAsync.onframe += delegate
            {
                if (fpsca.ElapsedMilliseconds < 1000)
                {
                    fpsa++;
                }
                else
                {
                    fpsavalue = fpsa;
                    fpsa = 0;
                    fpsca.Restart();
                }
            };

            Native.window.onframe += delegate
            {
                if (canvas == null)
                    return;

                if (DefaultWidth != Native.window.Width || DefaultHeight != Native.window.Height)
                {


                    PlasmaAsync.Dispose();

                    canvas.Orphanize();
                    canvas = null;


                    InitializeContent();

                    return;
                }


                if (PlasmaAsync.frame == null)
                    return;

                yyy++;

                //Console.WriteLine(new { xxx, yyy });


                if (fpsc.ElapsedMilliseconds < 1000)
                {
                    fps++;
                }
                else
                {
                    Native.document.title = new { fps, wfps = fpsavalue }.ToString();
                    fps = 0;
                    fpsc.Restart();
                }

                Console.WriteLine("set frame");

                context.bytes = PlasmaAsync.frame;

                //var x = context.getImageData();
                //x.data.set(PlasmaAsync.frame, 0);
                //context.putImageData(x, 0, 0, 0, 0, DefaultWidth, DefaultHeight);

            };


            #region requestFullscreen
            Native.document.body.ondblclick +=
                delegate
                {
                    //if (IsDisposed)
                    //    return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    if (canvas.parentNode == null)
                        return;

                    Native.document.body.requestFullscreen();


                };
            #endregion



            canvas.AttachToDocument();


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


            Native.Document.body.onclick +=
            delegate
            {
                //if (IsDisposed)
                //    return;

                //newicon();
            };

            //@"Spiral".ToDocumentTitle();


#if PackageAsApplication
            Native.Window.requestAnimationFrame +=
              delegate
              {
                  var icon = newicon();
                  var img = new IHTMLImage { src = icon };

                  //img.width = Native.Window.Width / 2;
                  //img.height = Native.Window.Height / 2;

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

                                                   e.dataTransfer.setData("DownloadURL", "application/octet-stream:Spiral.htm:data:application/octet-stream;base64," + data64);
                                                   e.dataTransfer.setData("text/html", data);
                                                   e.dataTransfer.setData("text/uri-list", Native.Document.location + "");
                                                   e.dataTransfer.setDragImage(img, img.width / 2, img.height / 2);
                                               };


                                   }
                               );
                          }
                  );




              };
#endif

        }


    }
}
