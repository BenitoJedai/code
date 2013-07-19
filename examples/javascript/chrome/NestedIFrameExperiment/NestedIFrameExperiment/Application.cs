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
using NestedIFrameExperiment.Design;
using NestedIFrameExperiment.HTML.Pages;

namespace NestedIFrameExperiment
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
            //  opener = , parent_is_self = true, parent = [object Window], self = [object Window], top = [object Window], window = [object Window] } 
            // { opener = , parent_is_self = false, parent = [object Window], self = [object Window], top = [object Window], window = [object Window] }

            Console.WriteLine(

                new
                {
                    Native.Window.opener,
                    parent_is_self = Native.Window.parent == Native.Window.self,
                    Native.Window.parent,
                    Native.Window.self,
                    Native.Window.top,
                    Native.Window.window,
                }

            );



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



            new IHTMLButton { innerText = "new iframe from " + Native.Document.location.href }.WhenClicked(
                delegate
                {
                    new IHTMLIFrame { src = Native.Document.location.href + "#nested/" }.AttachToDocument();

                }
             ).AttachToDocument();
        }

    }
}

