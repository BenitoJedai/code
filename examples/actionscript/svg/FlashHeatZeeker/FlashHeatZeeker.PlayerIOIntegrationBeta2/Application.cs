using Abstractatech.ConsoleFormPackage.Library;
using FlashHeatZeeker.PlayerIOIntegrationBeta2.Design;
using FlashHeatZeeker.PlayerIOIntegrationBeta2.HTML.Pages;
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
using Abstractatech.JavaScript.FormAsPopup;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.Runtime;
using chrome;
using System.Collections.Generic;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using System.Threading.Tasks;

namespace FlashHeatZeeker.PlayerIOIntegrationBeta2
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // crx
        // https://chrome.google.com/webstore/detail/operation-heat-zeeker/iiabebggdceojiejhopnopmbkgandhha?hl=et&utm_source=chrome-ntp-launcher
        // use non chrome to download without install
        // http://clients2.google.com/service/update2/crx?response=redirect&x=id%3Diiabebggdceojiejhopnopmbkgandhha%26uc%26lang%3Den-US&prod=chrome
        // Download interrupted
        // means servers are not yet in sync, wait some
        // pbaaphpbkehboammnlcihpemkkimdfgo
        // http://clients2.google.com/service/update2/crx?response=redirect&x=id%3Dpbaaphpbkehboammnlcihpemkkimdfgo%26uc%26lang%3Den-US&prod=chrome
        // error-unknownApplication
        // Error 404

        //public readonly ApplicationWebService service = new ApplicationWebService();

        // https://groups.google.com/a/chromium.org/forum/#!msg/chromium-extensions/3sXvdfb5qk8/iMteKuIawcUJ
        // An error occurred: Failed to process your item.
        // background subsection of app section is not supported.

        // * New packaged apps are currently able to be searched and browsed in the Chrome Web Store by Windows and Chrome OS users on Chrome's developer channel. Users on other OSs and Chrome channels can view and install the app via a direct link.


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            #region TheServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;
                chrome.Notification.DefaultTitle = "Operation «Heat Zeeker»";

                FormStyler.AtFormCreated = s =>
                {
                    // X:\jsc.svn\examples\javascript\IsometricTycoonViewWithToolbar\IsometricTycoonViewWithToolbar\Application.cs
                    // X:\jsc.internal.svn\core\com.abstractatech.web\com.abstractatech.web\Domains\discover.xavalon.net\discover_xavalon_net.cs

                    // browser popup will use this color
                    ((__Form)s.Context).HTMLTargetContainerRef.style.backgroundColor = JSColor.FromRGB(154, 108, 70);

                    s.Caption.style.backgroundColor = JSColor.FromRGB(154, 108, 70);
                    s.TargetOuterBorder.style.boxShadow = "rgba(154, 108, 70, 0.3) 0px 0px 6px 3px";
                    s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(154, 108, 70);

                    s.TargetInnerBorder.style.borderWidth = "0px";

                    s.CloseButton.style.color = JSColor.White;
                    s.CloseButton.style.backgroundColor = JSColor.None;
                    s.CloseButton.style.borderWidth = "0px";
                    s.CloseButtonContent.style.borderWidth = "0px";

                    s.TargetResizerPadding.style.left = "0px";
                    s.TargetResizerPadding.style.top = "0px";
                    s.TargetResizerPadding.style.right = "0px";
                    s.TargetResizerPadding.style.bottom = "0px";

                };


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
                                                appwindow.contentWindow.document.body
                                            );

                                            yield();
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

                        var f = new Form
                        {

                            Text = "Operation «Heat Zeeker»",
                            ShowIcon = false
                        };



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


                ChromeTCPServer.TheServer.Invoke(AppSource.Text, open
                    //async uri =>
                    //{
                    //    // Error	25	Cannot await 'chrome.Notification'	X:\jsc.svn\examples\javascript\chrome\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs	202	25	ChromeTCPServerWithFrameNone
                    //    //Error	26	Only assignment, call, increment, decrement, await, and new object expressions can be used as a statement	X:\jsc.svn\examples\javascript\chrome\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs	204	25	ChromeTCPServerWithFrameNone

                    //    await (Task)"Make me a window!".ToNotification();

                    //    open(uri);
                    //}
               );


                return;
            }
            #endregion

            // Your item is in the process of being published and may take up to 60 minutes to appear in the Chrome Web Store. 
            // https://chrome.google.com/webstore/detail/cpcgbahhjcobocclaolehaffhpfonofe/publish-delayed


            ApplicationSprite sprite = new ApplicationSprite();


            //sprite.wmode();

            sprite.AttachSpriteToDocument().With(
                   embed =>
                   {
                       embed.style.SetLocation(0, 0);
                       embed.style.SetSize(Native.window.Width, Native.window.Height);

                       Native.window.onresize +=
                           delegate
                           {
                               embed.style.SetSize(Native.window.Width, Native.window.Height);
                           };
                   }
               );


            "Operation «Heat Zeeker»".ToDocumentTitle();

            //try
            //{
            //    //Console.WriteLine(new { chrome.app.isInstalled });

            //}
            //catch
            //{
            //    Console.WriteLine("error, not in chrome?");
            //}

        }

    }

    public static class XX
    {
        public static void wmode(this Sprite s, string value = "direct")
        {
            var x = s.ToHTMLElement();

            var p = x.parentNode;
            if (p != null)
            {
                // if we continue, element will be reloaded!
                return;
            }

            x.setAttribute("wmode", value);


        }
    }
}
