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

namespace ChromeNotificationExperiment
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
            Console.WriteLine("Application loading... ");

            //Application loading... { Document = [object HTMLDocument], Window = [object Window], app = [object Object] }

            Console.WriteLine("Application loading... " + new
            {
                Native.Document,
                Native.Document.location.href,
                Native.Document.location.pathname,
                Native.Window,
                Native.Window.opener,
                Native.Window.navigator.userAgent,
                chrome.app,
                chrome.app.runtime,
                chrome.app.isInstalled,
                chrome.app.window,
            });

            // what are we running as?e
            // in browser by default?
            // in a web socket?
            // as a chrome application script? as _generated_background_page.html


            #region switch to chrome AppWindow
            if (chrome.app.runtime != null)
            {
                //The JavaScript context calling chrome.app.window.current() has no associated AppWindow. 
                //Console.WriteLine("appwindow loading... " + new { current = chrome.app.window.current() });

                // no HTML layout yet

                if (Native.Window.opener == null)
                    if (Native.Window.parent == Native.Window.self)
                    {
                    chrome.app.runtime.onLaunched.addListener(
                        new Action(
                            delegate
                            {
                                // runtime will launch only once?

                                // http://developer.chrome.com/apps/app.window.html
                                // do we even need index?

                                // https://code.google.com/p/chromium/issues/detail?id=148857
                                // https://developer.mozilla.org/en-US/docs/data_URIs

                                // chrome-extension://mdcjoomcbillipdchndockmfpelpehfc/data:text/html,%3Ch1%3EHello%2C%20World!%3C%2Fh1%3E
                                chrome.app.window.create(
                                    Native.Document.location.pathname,
                                    null,
                                    new Action<AppWindow>(
                                        appwindow =>
                                        {
                                            // Uncaught TypeError: Cannot read property 'contentWindow' of undefined 

                                            Console.WriteLine("appwindow loading... " + new { appwindow });
                                            Console.WriteLine("appwindow loading... " + new { appwindow.contentWindow });


                                            appwindow.contentWindow.onload +=
                                                delegate
                                                {
                                                    Console.WriteLine("appwindow contentWindow onload");


                                                    //new IHTMLButton("dynamic").AttachTo(
                                                    //    appwindow.contentWindow.document.body
                                                    //);


                                                };

                                            //Uncaught TypeError: Cannot read property 'contentWindow' of undefined 

                                        }
                                    )
                                );
                            }
                        )
                    );
                    return;
                }

                // if we are in a window lets add layout
                new App().Container.AttachToDocument();
            }
            #endregion


            var c = 0;

            #region notify
            new IHTMLButton { innerText = "notify" }.AttachToDocument().WhenClicked(
                delegate
                {
                    Action<string> callback =
                        notificationId =>
                        {

                            Console.WriteLine("create " + new { notificationId });

                            chrome.notifications.onClosed.addListener(
                                new Action<string, bool>(
                                    (__notificationId, __byUser) =>
                                    {
                                        Console.WriteLine("onClosed " + new { __notificationId, __byUser });
                                    }
                                )
                            );

                            chrome.notifications.onClicked.addListener(
                                 new Action<string>(
                                     (__notificationId) =>
                                     {
                                         Console.WriteLine("onClicked " + new { __notificationId });



                                         // 'tabs' is only allowed for extensions and legacy packaged apps, and this is a packaged app.

                                         //dynamic createProperties = new object();

                                         //createProperties.url = "http://example.com";

                                         //chrome.tabs.create(createProperties,

                                         //   new Action<Tab>(
                                         //       tab =>
                                         //       {
                                         //           Console.WriteLine("tab " + new { tab.id, tab.windowId });
                                         //       }
                                         //   )
                                         //);


                                         Native.Window.open("http://example.com", "_blank");
                                     }
                                 )
                             );


                            chrome.notifications.onButtonClicked.addListener(
                                 new Action<string, int>(
                                     (__notificationId, __buttonIndex) =>
                                     {
                                         Console.WriteLine("onButtonClicked " + new { __notificationId });
                                     }
                                 )
                             );

                        };


                    // http://developer.chrome.com/extensions/notifications.html#type-NotificationOptions
                    c++;
                    chrome.notifications.create(
                        "foo" + c,
                        new NotificationOptions
                        {
                            type = "basic",
                            title = "Primary Title",
                            message = "Primary message to display",


                            iconUrl = "assets/ScriptCoreLib/jsc.png"
                            //Invalid value for argument 2. Property 'iconUrl': Property is required. 
                            // Unable to download all specified images. 
                        },
                        callback
                    );


                }
            );
            #endregion






        }

    }
}
