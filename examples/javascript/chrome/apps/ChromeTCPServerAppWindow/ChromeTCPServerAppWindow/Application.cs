using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChromeTCPServerAppWindow;
using ChromeTCPServerAppWindow.Design;
using ChromeTCPServerAppWindow.HTML.Pages;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using System.Windows.Forms;

namespace ChromeTCPServer
{
    // http://www.snip2code.com/Snippet/19734/Visual-studio-intellisense-file-for-chro
    [Script(HasNoPrototype = true)]
    class xPointerLockPermissionRequest
    {
        // https://developer.chrome.com/apps/tags/webview#type-PointerLockPermissionRequest

        // tested by ?
        public void allow()
        {
        }
    }

    public static class TheServerWithAppWindow
    {
        public static void Invoke(
            string AppSource
        )
        {
            if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
            {
                Console.WriteLine("chrome.app.window.create, is that you?");

                #region __WebBrowser.InitializeInternalElement
                __WebBrowser.InitializeInternalElement = that =>
                {
                    var webview = Native.document.createElement("webview");
                    // You do not have permission to use <webview> tag. Be sure to declare 'webview' permission in your manifest. 
                    webview.setAttribute("partition", "p1");
                    webview.setAttribute("allowtransparency", "true");
                    webview.setAttribute("allowfullscreen", "true");

                    webview.style.Opacity = 0.0;


                    webview.addEventListener("loadstop", async e =>
                             {
                                 Console.WriteLine("loadstop");
                                 // prevent showing white while loading...

                                 await Task.Delay(100);

                                 //webview.style.display = IStyle.DisplayEnum.block;
                                 webview.style.Opacity = 1.0;
                             }
                     );



                    #region permissionrequest
                    // https://github.com/GoogleChrome/chromium-webview-samples
                    // permissionrequest
                    // https://developer.chrome.com/apps/tags/webview#type-WebRequestEventInteface
                    webview.addEventListener("permissionrequest",
                        (e) =>
                                {
                                    // https://code.google.com/p/chromium/issues/detail?id=141198

                                    //% c9:176376ms permissionrequest { { permission = pointerLock } }
                                    //Uncaught TypeError: Cannot read property 'allow' of undefined
                                    //< webview >: The permission request for "pointerLock" has been denied.

                                    // X:\jsc.internal.git\market\chrome\ChromeMyJscSolutionsNet\ChromeMyJscSolutionsNet\Application.cs

                                    // https://chromium.googlesource.com/chromium/src/+/git-svn/chrome/common/extensions/api/webview_tag.json
                                    // https://bugzilla.mozilla.org/show_bug.cgi?id=896143
                                    // https://developer.chrome.com/apps/tags/webview#event-permissionrequest
                                    // https://code.google.com/p/chromium/issues/detail?id=153540

                                    //  The permission request for "pointerLock" has been denied.
                                    // http://stackoverflow.com/questions/16302627/geolocation-in-a-webview-inside-a-chrome-packaged-app
                                    // http://git.chromium.org/gitweb/?p=chromium.git;a=commitdiff;h=e1d226c0ea739adaed36cc4b617f7a387d44eca0

                                    string permission = (e as dynamic).permission;
                                    xPointerLockPermissionRequest e_request = (e as dynamic).request;

                                    Console.WriteLine("permissionrequest " + new
                                    {
                                        permission,
                                        e,
                                        e_request
                                    });
                                    //% c9:167409ms permissionrequest { { permission = pointerLock } }
                                    //Uncaught TypeError: Cannot read property 'allow' of undefined

                                    e.preventDefault();


                                    //9:122010ms permissionrequest { { permission = pointerLock, e = [object Event], e_request = [object Object] } }
                                    //9:122028ms delay permissionrequest { { permission = pointerLock, e = [object Event], delay_e_request = [object Object] } }
                                    //Uncaught Error: < webview >: Permission has already been decided for this "permissionrequest" event. 

                                    //Expando.

                                    if (e_request != null)
                                        e_request.allow();

                                    //Task.Delay(1).ContinueWith(
                                    //    delegate
                                    //{
                                    //    xPointerLockPermissionRequest delay_e_request = (e as dynamic).request;

                                    //    Console.WriteLine("delay permissionrequest " + new { permission, e, delay_e_request });


                                    //    if (delay_e_request != null)
                                    //        delay_e_request.allow();
                                    //}
                                    //);
                                }
                    );
                    #endregion


                    that.InternalElement = (IHTMLIFrame)(object)webview;

                    // src was not copied for some reason. force it.
                    that.Size = that.Size;
                    that.Refresh();

                };
                #endregion

                var css = Native.css[typeof(Form)][" .caption"];
                (css.style as dynamic).webkitAppRegion = "drag";


                FormStyler.AtFormCreated = FormStylerLikeFloat.LikeFloat;

                var ShadowRightBottom = 8;

                var f = new Form
                {

                    ShowIcon = false,

                    Text = Native.document.title,

                    //Text = Native.document.location.hash,
                    StartPosition = FormStartPosition.Manual
                };


                f.MoveTo(0, 0).SizeTo(
                        Native.window.Width - ShadowRightBottom,
                        Native.window.Height - ShadowRightBottom
                    );

                //f.Opacity = 0.5;

                f.Show();




                var w = new WebBrowser
                {

                    // this wont work?
                    //Dock = DockStyle.Fill

                }.AttachTo(f);

                w.Navigate(
                    Native.document.title
                );



                f.FormClosed +=
                    delegate
                {
                    // close the appwindow

                    // DWM animates the close.
                    Native.window.close();
                };

                f.SizeChanged +=
                    delegate
                {
                    var cs = f.ClientSize;

                    w.SizeTo(
                        cs.Width,
                        cs.Height
                        );

                    Native.window.resizeTo(
                        f.Width + ShadowRightBottom,
                        f.Height + ShadowRightBottom
                    );

                };

                Native.window.onresize +=
                    delegate
                {
                    // outer frame is resized
                    f.SizeTo(
                        Native.window.Width - ShadowRightBottom,
                        Native.window.Height - ShadowRightBottom
                    );

                };

                return;
            }


            ChromeTCPServer.TheServer.InvokeAsync(AppSource, async uri =>
            {
                var o = new object();
                var hidden = o == o;
                var alphaEnabled = o == o;
                var alwaysOnTop = o == o;


                var options = new
                {
                    //allow webkitAppRegion
                    frame = "none",
                    hidden,
                    alphaEnabled,
                    alwaysOnTop
                };

                // The URL used for window creation must be local for security reasons.

                var xappwindow = await chrome.app.window.create(
                   Native.document.location.pathname,
                           options
            );

                //xappwindow.set

                // can we prevent the white page from appearing?
                await xappwindow.contentWindow.async.onload;

                //xappwindow.contentWindow.document.title = "http://example.com";
                xappwindow.contentWindow.document.title = uri;

                await Task.Delay(100);
                //await Task.Delay(200);

                xappwindow.show();
            }
            );


        }
    }
}

namespace ChromeTCPServerAppWindow
{

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // we do not like our old server anymore
            // we like the new one.

            // based on 
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowForm\ChromeAppWindowForm\Application.cs
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs

            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                ChromeTCPServer.TheServerWithAppWindow.Invoke(AppSource.Text);

                return;
            }
            #endregion

            Native.body.style.backgroundColor = "transparent";


            new IHTMLPre { "app is now running" }.AttachToDocument();

        }

    }
}
