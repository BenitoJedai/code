using chrome;
using ChromeTCPServerWithFrameNone;
using ChromeTCPServerWithFrameNone.Design;
using ChromeTCPServerWithFrameNone.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ChromeTCPServerWithFrameNone
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
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultTitle = "ChromeTCPServerWithFrameNone";



                #region __Form
                {
                    var windows = new List<AppWindow>();


                    __Form.InternalHTMLTargetAttachToDocument =
                       async (that, yield) =>
                       {

                           //Error in event handler for app.runtime.onLaunched: Error: Invalid value for argument 2. Property 'transparentBackground': Expected 'boolean' but got 'integer'.
                           var transparentBackground = true;


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
                                            //appwindow.contentWindow.onbeforeunload +=
                                            //    delegate
                                            //    {
                                            //        Console.WriteLine("onbeforeunload");
                                            //    };

                                            //appwindow.onBoundsChanged.addListener(
                                            //        new Action(
                                            //        delegate
                                            //        {
                                            //            Console.WriteLine("appwindow.onBoundsChanged");

                                            //            f.SizeTo(
                                            //                appwindow.contentWindow.Width,
                                            //                appwindow.contentWindow.Height
                                            //            );
                                            //        }
                                            //    )
                                            //);


                                            appwindow.contentWindow.onresize +=
                                                //appwindow.onBoundsChanged.addListener(
                                                //    new Action(
                                                    delegate
                                                    {

                                                        Console.WriteLine("appwindow.contentWindow.onresize SizeTo " +
                                                            new
                                                            {
                                                                appwindow.contentWindow.Width,
                                                                appwindow.contentWindow.Height
                                                            }
                                                            );

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
                                                appwindow.contentWindow.document.body.parentNode
                                            );

                                            // no fade in thanks
                                            yield(false);


                                            //Console.WriteLine("appwindow contentWindow onload done");
                                        };
                                   #endregion

                                   //Uncaught TypeError: Cannot read property 'contentWindow' of undefined 



                                   appwindow.contentWindow.onload +=
                                       onload;
                               }
                           );





                       };


                }
                #endregion

                #region __WebBrowser
                {
                    // X:\jsc.svn\examples\javascript\chrome\ChromeFormsWebBrowserExperiment\ChromeFormsWebBrowserExperiment\Application.cs
                    __WebBrowser.InitializeInternalElement = that =>
                    {
                        var webview = Native.document.createElement("webview");
                        // You do not have permission to use <webview> tag. Be sure to declare 'webview' permission in your manifest. 
                        webview.setAttribute("partition", "p1");

                        that.InternalElement = (IHTMLIFrame)(object)webview;
                    };

                }
                #endregion


                #region open
                Action<string> open =
                    uri =>
                    {

                        var f = new Form();



                        //Refused to frame 'http://192.168.43.252:8877/' because it violates the following Content Security Policy directive: "frame-src 'self' data: chrome-extension-resource:"

                        var w = new WebBrowser { }.AttachTo(f);

                        f.SizeChanged +=
                            delegate
                            {
                                Console.WriteLine("SizeChanged");

                                var ClientSize = f.ClientSize;


                                w.SizeTo(
                                    ClientSize.Width,
                                    ClientSize.Height
                                );

                            };
                        w.Navigate(uri);

                        f.Show();
                    };
                #endregion


                ChromeTCPServer.TheServer.Invoke(AppSource.Text,
                    async uri =>
                    {
                        // Error	25	Cannot await 'chrome.Notification'	X:\jsc.svn\examples\javascript\chrome\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs	202	25	ChromeTCPServerWithFrameNone
                        //Error	26	Only assignment, call, increment, decrement, await, and new object expressions can be used as a statement	X:\jsc.svn\examples\javascript\chrome\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs	204	25	ChromeTCPServerWithFrameNone

                        await (Task)"Make me a window!".ToNotification();

                        open(uri);
                    }
               );



                return;
            }

        }

    }
}
