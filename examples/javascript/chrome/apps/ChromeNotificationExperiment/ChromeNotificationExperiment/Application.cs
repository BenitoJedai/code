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
using ChromeNotificationExperiment.Design;
using ChromeNotificationExperiment.HTML.Pages;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.Runtime;
using chrome;
using ScriptCoreLib.JavaScript.WebGL;
using WebGLSpiral.Shaders;

namespace ChromeNotificationExperiment
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using System.Diagnostics;
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
        : ISurface
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
            // Additional information: Could not load file or assembly 'TestPackageAsApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies.

            // { Message = Method 'InternalAsNode' in type 'WebGLTetrahedron.HTML.Images.FromAssets.Preview' from assembly 'WebGLTetrahedron, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation. }
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140604/chrome

            Console.WriteLine("Application loading... ");

            //Application loading... { Document = [object HTMLDocument], Window = [object Window], app = [object Object] }

            Console.WriteLine("Application loading... " + new
            {
                Native.Document,
                Native.Document.location.href,
                Native.Document.location.pathname,
                Native.window,
                Native.window.opener,
                Native.window.navigator.userAgent,
                //chrome.app,
                //chrome.app.runtime,
                //chrome.app.isInstalled,
                //chrome.app.window,
            });

            // what are we running as?e
            // in browser by default?
            // in a web socket?
            // as a chrome application script? as _generated_background_page.html


            #region switch to chrome AppWindow

            //var ischrome = typeof(chrome.app.runtime) != null;
            if (Expando.InternalIsMember(Native.self, "chrome"))
            {
                //The JavaScript context calling chrome.app.window.current() has no associated AppWindow. 
                //Console.WriteLine("appwindow loading... " + new { current = chrome.app.window.current() });

                // no HTML layout yet

                if (Native.window.opener == null)
                    if (Native.window.parent == Native.window.self)
                    {
                        chrome.app.runtime.Launched +=
                            async delegate
                        {
                            // runtime will launch only once?

                            // http://developer.chrome.com/apps/app.window.html
                            // do we even need index?

                            // https://code.google.com/p/chromium/issues/detail?id=148857
                            // https://developer.mozilla.org/en-US/docs/data_URIs

                            // chrome-extension://mdcjoomcbillipdchndockmfpelpehfc/data:text/html,%3Ch1%3EHello%2C%20World!%3C%2Fh1%3E
                            var appwindow = await chrome.app.window.create(

                                url: Native.Document.location.pathname,
                                options: null
                            );


                            Console.WriteLine("appwindow loading... " + new { appwindow });
                            Console.WriteLine("appwindow loading... " + new { appwindow.contentWindow });


                            appwindow.contentWindow.onload +=
                                delegate
                            {
                                Console.WriteLine("appwindow contentWindow onload");
                            };
                        };

                        return;
                    }

                // if we are in a window lets add layout
                new App().Container.AttachToDocument();
            }
            #endregion


            var c = 0;

            //#region gl
            //var gl = new WebGLRenderingContext(alpha: false, preserveDrawingBuffer: true);

            //gl.canvas.width = 96;
            //gl.canvas.height = 96;

            //var s = new SpiralSurface(this);

            //this.onsurface(gl);
            //this.onresize(gl.canvas.width, gl.canvas.height);
            //#endregion

            var st = new Stopwatch();
            st.Start();

            // are we running out of memory?
            //Native.window.onframe += delegate
            //{
            //    s.ucolor_1 = (float)Math.Sin(st.ElapsedMilliseconds * 0.001) * 0.5f + 0.5f;

            //    this.onframe();
            //};

            new chrome.Notification
            {
                Title = "ChromeNotificationExperiment",
                Message = "activated!",
                //IconCanvas = new WebGLTetrahedron.Application().gl.canvas
            };

            //#region notify  with spiral
            //new IHTMLButton { innerText = "notify with WebGLTetrahedron" }.AttachToDocument().WhenClicked(
            // async delegate
            //{
            //    Console.WriteLine("enter WhenClicked");

            //    var n = new Notification
            //    {
            //        Title = "WebGLTetrahedron",
            //        Message = "energy!",

            //        // this locks up chrome nowadays. why? are we doing something wrong?
            //        //IconCanvas = new WebGLTetrahedron.Application().gl.canvas
            //    };

            //    Console.WriteLine("at WhenClicked 175");

            //    n.Clicked +=
            //        delegate
            //    {
            //        Console.WriteLine("Clicked");
            //    };

            //    Console.WriteLine("at WhenClicked 183");

            //    n.Closed +=
            //        byUser =>
            //         {
            //             Console.WriteLine("Closed " + new { byUser });
            //         };


            //    Console.WriteLine("at WhenClicked 192");

            //    // and now it blows up. why?
            //}
            //);
            //#endregion

            #region notify 
            new IHTMLButton { innerText = "notify" }.AttachToDocument().WhenClicked(
             async delegate
            {
                c++;

                var n = new chrome.Notification
                {
                    Message = "Primary message to display",
                    //IconCanvas = gl.canvas
                };

                n.Clicked +=
                    delegate
                {
                    Console.WriteLine("Clicked");
                };

                n.Closed +=
                    byUser =>
                     {
                         Console.WriteLine("Closed " + new { byUser });
                     };



            }
            );
            #endregion


            #region notify 2
            new IHTMLButton { innerText = "notify 2" }.AttachToDocument().WhenClicked(
             async delegate
            {
                c++;

                var n = new chrome.Notification("foo" + c,
                   message: "Primary message to display"
                );

                n.Clicked +=
                    delegate
                {
                    Console.WriteLine("Clicked");
                };

                n.Closed +=
                    byUser =>
                     {
                         Console.WriteLine("Closed " + new { byUser });
                     };


                n.Message = "Primary message to display [3]";
                await Task.Delay(500);
                n.Message = "Primary message to display [2]";
                await Task.Delay(500);
                n.Message = "Primary message to display [1]";
            }
            );
            #endregion


            //#region notify
            //new IHTMLButton { innerText = "notify" }.AttachToDocument().WhenClicked(
            //    async delegate
            //    {
            //        // http://developer.chrome.com/extensions/notifications.html#type-NotificationOptions
            //        c++;

            //        //default(TaskCompletionSource<string>).SetResult
            //        var notificationId = await chrome.notifications.create(
            //            "foo" + c,
            //            new NotificationOptions
            //            {
            //                type = "basic",
            //                title = "Primary Title",
            //                message = "Primary message to display",


            //                iconUrl = "assets/ScriptCoreLib/jsc.png"
            //                //Invalid value for argument 2. Property 'iconUrl': Property is required. 
            //                // Unable to download all specified images. 
            //            }
            //        );



            //        Console.WriteLine("create " + new { notificationId });

            //        chrome.notifications.onClosed.addListener(
            //            new Action<string, bool>(
            //                (__notificationId, __byUser) =>
            //                {
            //                    Console.WriteLine("onClosed " + new { __notificationId, __byUser });
            //                }
            //            )
            //        );

            //        chrome.notifications.onClicked.addListener(
            //                new Action<string>(
            //                    (__notificationId) =>
            //                    {
            //                        Console.WriteLine("onClicked " + new { __notificationId });



            //                        // 'tabs' is only allowed for extensions and legacy packaged apps, and this is a packaged app.

            //                        //dynamic createProperties = new object();

            //                        //createProperties.url = "http://example.com";

            //                        //chrome.tabs.create(createProperties,

            //                        //   new Action<Tab>(
            //                        //       tab =>
            //                        //       {
            //                        //           Console.WriteLine("tab " + new { tab.id, tab.windowId });
            //                        //       }
            //                        //   )
            //                        //);


            //                        Native.window.open("http://example.com", "_blank");
            //                    }
            //                )
            //            );


            //        chrome.notifications.onButtonClicked.addListener(
            //                new Action<string, int>(
            //                    (__notificationId, __buttonIndex) =>
            //                    {
            //                        Console.WriteLine("onButtonClicked " + new { __notificationId });
            //                    }
            //                )
            //            );



            //    }
            //);
            //#endregion






        }

    }
}
