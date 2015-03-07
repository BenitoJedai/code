// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using WebGLChocolux.HTML.Pages;
using WebGLChocolux;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.GLSL;
using WebGLChocolux.Shaders;

namespace WebGLChocolux
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            Initialize(page);
        }




        private void Initialize(IDefault page = null)
        {
			#region += Launched chrome.app.window
			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_socket = self_chrome.socket;

			if (self_chrome_socket != null)
			{
				if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
				{
					Console.WriteLine("chrome.app.window.create, is that you?");

					// pass thru
				}
				else
				{
					// should jsc send a copresence udp message?
					chrome.runtime.UpdateAvailable += delegate
					{
						new chrome.Notification(title: "UpdateAvailable");

					};

					chrome.app.runtime.Launched += async delegate
					{
						// 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
						Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

						new chrome.Notification(title: "ChromeUDPSendAsync");

						var xappwindow = await chrome.app.window.create(
							   Native.document.location.pathname, options: null
						);

						//xappwindow.setAlwaysOnTop

						xappwindow.show();

						await xappwindow.contentWindow.async.onload;

						Console.WriteLine("chrome.app.window loaded!");
					};


					return;
				}
			}
			#endregion


			int w = Native.window.Width;
            int h = Native.window.Height;



            var gl = new WebGLRenderingContext(preserveDrawingBuffer: true);

            var canvas = gl.canvas.AttachToDocument();
            canvas.style.backgroundColor = JSColor.Black;
            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0);


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


            // http://cs.helsinki.fi/u/ilmarihe/metatunnel.html


            var p = gl.createProgram(
                new ChocoluxVertexShader(),
                new ChocoluxFragmentShader()
            );


            gl.bindAttribLocation(p, 0, "position");
            gl.linkProgram(p);

            gl.useProgram(p);

            var uniforms = p.Uniforms(gl);

            gl.viewport(0, 0, w, h);

            gl.enableVertexAttribArray(0);


            var verts = gl.createBuffer();

            gl.bindBuffer(gl.ARRAY_BUFFER, verts);
            gl.bufferData(gl.ARRAY_BUFFER,
              new[] { -1f, -1f, -1f, 1f, 1f, -1f, 1f, 1f }
            , gl.STATIC_DRAW);
            gl.vertexAttribPointer((uint)0, 2, gl.FLOAT, false, 0, 0);

            var indicies = gl.createBuffer();

            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indicies);

            var q = new Uint16Array(0, 1, 2, 3);



            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, q, gl.STATIC_DRAW);

            var start = new IDate().getTime();

            Native.window.onframe += delegate
            {
                var timestamp = new IDate().getTime();
                var t = (timestamp - start) / 1000.0f * 30f;


                uniforms.t = t * 100;
                //gl.uniform1f(gl.getUniformLocation(p, "t"), t * 100);
                gl.drawElements(gl.TRIANGLE_STRIP, 4, gl.UNSIGNED_SHORT, 0);
                gl.flush();



            };




            #region AtResize
            Action AtResize = delegate
            {
                if (IsDisposed)
                {
                    return;
                }

                canvas.width = Native.window.Width;
                canvas.height = Native.window.Height;


                gl.viewport(0, 0, canvas.width, canvas.height);
            };

            AtResize();

            Native.window.onresize += delegate
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
                      if (IsDisposed)
                          return;

                      newicon();
                  };

#if PackageAsApplication
            @"Spiral".ToDocumentTitle();

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
                                   XElement.Parse(new XDefaultPage.XMLSourceSource().Text),
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

                                                   e.dataTransfer.setData("DownloadURL", "application/octet-stream:Chocolux.htm:data:application/octet-stream;base64," + data64);
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

        public Action Dispose;
    }



}
