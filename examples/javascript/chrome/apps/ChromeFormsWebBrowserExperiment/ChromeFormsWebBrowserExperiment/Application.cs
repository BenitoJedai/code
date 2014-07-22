using chrome;
using ChromeFormsWebBrowserExperiment.Design;
using ChromeFormsWebBrowserExperiment.HTML.Pages;
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ChromeFormsWebBrowserExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140722

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


                       // http://src.chromium.org/viewvc/chrome/trunk/src/chrome/common/extensions/api/app_window.idl
                       var appwindow = await chrome.app.window.create(
                             Native.Document.location.pathname,
                             new
                             {
                                 frame = "none"
                                 //,transparentBackground
                             });

                       // Uncaught TypeError: Cannot read property 'contentWindow' of undefined 

                       Console.WriteLine("appwindow loading... " + new { appwindow });
                       Console.WriteLine("appwindow loading... " + new { appwindow.contentWindow });

                       #region onload
                       appwindow.contentWindow.onload +=
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

                               yield(false);
                               //Console.WriteLine("appwindow contentWindow onload done");
                           };
                       #endregion


                       //Uncaught TypeError: Cannot read property 'contentWindow' of undefined 






                   };
                #endregion

                #region __WebBrowser.InitializeInternalElement
                __WebBrowser.InitializeInternalElement = that =>
                    {
                        var webview = Native.Document.createElement("webview");
                        // You do not have permission to use <webview> tag. Be sure to declare 'webview' permission in your manifest. 
                        webview.setAttribute("partition", "p1");



                        webview.addEventListener(
                          "newwindow",
                          new Action<IEvent>(
                              ee =>
                              {
                                  Console.WriteLine("newwindow");

                                  // Uncaught Error: <webview>: An action has already been taken for this "newwindow" event. 

                                  ee.preventDefault();

                                  dynamic e = ee;


                                  // https://plus.google.com/100132233764003563318/posts/2dNmkacjiat

                                  string targetUrl = e.targetUrl;
                                  Console.WriteLine(new { targetUrl });

                                  // attach or discard
                                  object newwindow = e.window;

                                  Console.WriteLine(new { newwindow });


                                  var nf = new Form();
                                  var nfw = new WebBrowser();
                                  var __nfw = (__WebBrowser)nfw;
                                  nfw.Dock = DockStyle.Fill;
                                  nf.Controls.Add(nfw);

                                  nf.FormClosing +=
                                      delegate
                                      {
                                          // { InternalUrl = chrome-extension://aemlnmcokphbneegoefdckonejmknohh:80/_generated_background_page.htmlnull## } 
                                          Console.WriteLine("newwindow FormClosing");


                                          nfw.Navigate("about:blank#");

                                      };
                                  nf.Shown +=
                                      delegate
                                      {

                                          Console.WriteLine("newwindow Shown");

                                          new IFunction("w", "v", "w.attach(v);").apply(null, newwindow, __nfw.InternalElement);

                                      };

                                  nf.Show();
                              }
                       )
                       );










                        that.InternalElement = (IHTMLIFrame)(object)webview;
                    };
                #endregion

                #region __Form.InternalHTMLTargetAttachToDocument
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
                #endregion


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

                            __Form.InternalHTMLTargetAttachToDocument = InternalHTMLTargetAttachToDocument;

                            later();
                            later = null;

                        };


            }
            #endregion

            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;

            var xf = new Form();
            var content = new ApplicationControl();
            content.BackColor = System.Drawing.Color.Transparent;
            xf.Controls.Add(content);
            xf.Show();
        }

    }
}
