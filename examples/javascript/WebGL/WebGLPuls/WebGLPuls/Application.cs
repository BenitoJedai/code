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
        // http://wakaba.c3.cx/w/puls.html
        // see also: http://meatfighter.com/puls/
        // it only took 2 years :)
        // Revision: 2744
        //Date: Friday, July 23, 2010 4:34:01 PM
        //Added : /templates/TwentyTen/WebGLPuls/WebGLPuls.sln
        // 2014!
        //2015!


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            // broken!
            InitializeContent(page);
        }

        #region ISurface
        public event Action onframe;

        public event Action<int, int> onresize;

        public event Action<gl> onsurface;
        #endregion


        public Action Dispose;

        private void InitializeContent(IDefault page = null)
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

                canvas.width = Native.window.Width;
                canvas.height = Native.window.Height;

                this.onresize(Native.window.Width, Native.window.Height);

                Console.WriteLine("onresize");
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




            #region loop
            Action loop = null;

            loop = delegate
            {
                if (IsDisposed)
                    return;

                this.onframe();

                Native.window.requestAnimationFrame += loop;

            };

            Native.window.requestAnimationFrame += loop;
            #endregion


        }


    }



}
