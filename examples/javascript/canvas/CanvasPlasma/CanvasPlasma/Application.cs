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






        public void InitializeContent()
        {
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            var DefaultWidth = Native.Window.Width;
            var DefaultHeight = Native.Window.Height;


            Plasma.generatePlasma(DefaultWidth, DefaultHeight);

            var shift = 0;



            var context = new CanvasRenderingContext2D();


            var canvas = context.canvas;

            canvas.width = DefaultWidth;
            canvas.height = DefaultHeight;

            canvas.style.position = IStyle.PositionEnum.absolute;
            canvas.style.SetLocation(0, 0, DefaultWidth, DefaultHeight);

            var xx = context.getImageData(0, 0, DefaultWidth, DefaultHeight);
            //var x = (ImageData)(object)xx;
            var x = xx;


            Native.window.onframe += delegate
            {
                if (DefaultWidth != Native.window.Width)
                    if (DefaultHeight != Native.window.Height)
                    {
                        canvas.Orphanize();
                        InitializeContent();
                        return;
                    }

                var buffer = Plasma.shiftPlasma(shift);

                //var x = context.createImageData(DefaultWidth, DefaultHeight);


                var k = 0;
                for (int i = 0; i < DefaultWidth; i++)
                    for (int j = 0; j < DefaultHeight; j++)
                    {
                        var i4 = i * 4;
                        var j4 = j * 4;


                        x.data[(uint)(i4 + j4 * DefaultWidth + 2)] = (byte)((buffer[k] >> (0 * 8)) & 0xff);
                        x.data[(uint)(i4 + j4 * DefaultWidth + 1)] = (byte)((buffer[k] >> (1 * 8)) & 0xff);
                        x.data[(uint)(i4 + j4 * DefaultWidth + 0)] = (byte)((buffer[k] >> (2 * 8)) & 0xff);
                        x.data[(uint)(i4 + j4 * DefaultWidth + 3)] = 0xff;

                        k++;
                    }

                context.putImageData(xx, 0, 0, 0, 0, DefaultWidth, DefaultHeight);
                shift++;
            };


            #region requestFullscreen
            Native.Document.body.ondblclick +=
                delegate
                {
                    //if (IsDisposed)
                    //    return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    if (canvas.parentNode == null)
                        return;

                    Native.Document.body.requestFullscreen();


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

                newicon();
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
