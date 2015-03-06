using chrome;
using ChromeAppWindowFrameNoneExperiment.Design;
using ChromeAppWindowFrameNoneExperiment.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ChromeAppWindowFrameNoneExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //Console.WriteLine("Application loading...");

            #region do InternalHTMLTargetAttachToDocument
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                // X:\jsc.svn\examples\javascript\chrome\ChromeAppWindowFrameNoneExperiment\ChromeAppWindowFrameNoneExperiment\Application.cs

                //The JavaScript context calling chrome.app.window.current() has no associated AppWindow. 
                //Console.WriteLine("appwindow loading... " + new { current = chrome.app.window.current() });
                // no HTML layout yet

                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("i am about:blank");
                    return;
                }

                //Console.WriteLine("Application wait for onLaunched");


                chrome.runtime.Suspend +=
                    delegate
                {
                    Console.WriteLine("suspend!");
                };


                Action later = delegate { };

                var windows = new List<AppWindow>();

                #region InternalHTMLTargetAttachToDocument
                Action<__Form, Action<bool>> InternalHTMLTargetAttachToDocument =
                   async (that, yield) =>
                   {

                       //Error in event handler for app.runtime.onLaunched: Error: Invalid value for argument 2. Property 'transparentBackground': Expected 'boolean' but got 'integer'.
                       var transparentBackground = true;

                       // https://code.google.com/p/chromium/issues/detail?id=260810
                       // http://src.chromium.org/viewvc/chrome/trunk/src/chrome/common/extensions/api/app_window.idl

                       var xappwindow = await chrome.app.window.create(
                             Native.document.location.pathname,
                             new
                       {
                           frame = "none"
                           //,transparentBackground
                       }
                        );


                       // Uncaught TypeError: Cannot read property 'contentWindow' of undefined 

                       Console.WriteLine("appwindow loading... " + new { xappwindow });
                       Console.WriteLine("appwindow loading... " + new { xappwindow.contentWindow });


                       xappwindow.With(
                           appwindow =>
                           {

                               #region onload
                               Action<IEvent> onload =

                                    delegate
                               {
                                   var c = that;
                                   var f = (Form)that;
                                   var ff = c;

                                   windows.Add(appwindow);

                                   // http://sandipchitale.blogspot.com/2013/03/tip-webkit-app-region-css-property.html

                                   (ff.CaptionForeground.style as dynamic).webkitAppRegion = "drag";

                                   //(ff.ResizeGripElement.style as dynamic).webkitAppRegion = "drag";
                                   // cant have it yet
                                   ff.ResizeGripElement.Orphanize();

                                   f.StartPosition = FormStartPosition.Manual;
                                   f.MoveTo(0, 0);

                                   f.FormClosing +=
                                       delegate
                                   {
                                       Console.WriteLine("FormClosing");
                                       appwindow.close();
                                   };

                                   appwindow.onRestored.addListener(
                                       new Action(
                                           delegate
                                   {
                                       that.CaptionShadow.Hide();

                                   }
                                       )
                                   );

                                   appwindow.onMaximized.addListener(
                                   new Action(
                                           delegate
                                   {
                                       that.CaptionShadow.Show();

                                   }
                                   )
                                   );

                                   appwindow.onClosed.addListener(
                                       new Action(
                                           delegate
                                   {
                                       Console.WriteLine("onClosed");
                                       windows.Remove(appwindow);

                                       f.Close();
                                   }
                                   )
                                   );

                                   // wont fire yet
                                   appwindow.contentWindow.onbeforeunload +=
                                       delegate
                                   {
                                       Console.WriteLine("onbeforeunload");
                                   };

                                   appwindow.contentWindow.onresize +=
                                                //appwindow.onBoundsChanged.addListener(
                                                //    new Action(
                                                delegate
                                   {
                                       f.SizeTo(
                                           appwindow.contentWindow.Width,
                                           appwindow.contentWindow.Height
                                       );
                                   }
                                        //)
                                        //)
                                        ;

                                   f.SizeTo(
                                       appwindow.contentWindow.Width,
                                       appwindow.contentWindow.Height
                                   );


                                   //Console.WriteLine("appwindow contentWindow onload");


                                   that.HTMLTarget.AttachTo(
                                       appwindow.contentWindow.document.body
                                   );


                                   // ????
                                   yield(default(bool));
                                   //Console.WriteLine("appwindow contentWindow onload done");
                               };
                               #endregion

                               //Uncaught TypeError: Cannot read property 'contentWindow' of undefined 



                               appwindow.contentWindow.onload +=
                                   onload;
                           }
                       );





                   };
                #endregion


                __Form.InternalHTMLTargetAttachToDocument =
                    (that, yield) =>
                    {
                        Console.WriteLine("Application wait for onLaunched for InternalHTMLTargetAttachToDocument");

                        later +=
                            delegate
                        {

                            InternalHTMLTargetAttachToDocument(that, yield);
                        };
                    };


                // why wait?
                chrome.app.runtime.Launched +=
                    delegate
                {
                    if (later == null)
                    {
                        if (windows.Count == 0)
                        {
                            Console.WriteLine("chrome.runtime.reload");
                            chrome.runtime.reload();
                            return;
                        }

                        Console.WriteLine("drawAttention");
                        windows.First().drawAttention();


                        return;
                    }

                    Console.WriteLine("Application onLaunched!");
                    // signal any pending Show commands?

                    // what do you mean it needs to return a bool?
                    // what happens if i dont fix it?
                    __Form.InternalHTMLTargetAttachToDocument = InternalHTMLTargetAttachToDocument;

                    later();
                    later = null;

                };


            }
            #endregion


            //FormStyler.AtFormCreated = FormStylerLikeAero.LikeAero;
            //FormStyler.AtFormCreated = FormStylerLikeFloat.LikeFloat;
            //FormStyler.AtFormCreated = FormStyler.LikeWindows3;
            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;

            #region new Form
            var xf = new Form();
            content.BackColor = System.Drawing.Color.Transparent;
            xf.Controls.Add(content);
            xf.Show();

            content.button2.Click +=
                delegate
                {

                    #region Worker
                    // we now should have simple scope sharing for Task.Run
                    var www = new Worker(
                      wworker =>
                          {
                              // running in worker context. cannot talk to outer scope yet.

                              //wworker.RedirectConsoleOutput();


                              // hello from the background worker { self = [object WorkerGlobalScope] }

                              var x = 0.0;


                              Console.WriteLine("Start");
                              var s = new Stopwatch();
                              s.Start();
                              for (int j = 0; j < 32; j++)
                              {
                                  for (int i = 0; i < 32000000; i++)
                                  {
                                      x = Math.Sin(i);

                                  }
                                  Console.WriteLine(new { j, s.Elapsed }.ToString());
                              }
                              Console.WriteLine("Stop");
                          }
                  );

                www.onmessage +=
                    e =>
                        {
                            Console.Write("www: " + e.data);
                        };
                    #endregion


                };

            //new Abstractatech.ConsoleFormPackage.Library.ConsoleForm { }.InitializeConsoleFormWriter().Show();
            #endregion

            Console.WriteLine("hello console!");


            //            The webpage at http://192.168.1.100:6669/ might be temporarily down or it may have moved permanently to a new web address.
            //Error code: ERR_UNSAFE_PORT
        }

    }
}
