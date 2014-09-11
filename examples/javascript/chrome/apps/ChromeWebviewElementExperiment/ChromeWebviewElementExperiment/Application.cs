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
using ChromeWebviewElementExperiment.Design;
using ChromeWebviewElementExperiment.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace ChromeWebviewElementExperiment
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
            // ! https://code.google.com/p/chromium/issues/detail?id=413165

            // X:\jsc.svn\examples\javascript\chrome\apps\ChomeAlphaAppWindow\ChomeAlphaAppWindow\Application.cs

            #region switch to chrome AppWindow
            if (Expando.InternalIsMember(Native.window, "chrome"))
                if (chrome.app.runtime != null)
                {





                    Console.WriteLine("Application switch to chrome AppWindow");

                    //The JavaScript context calling chrome.app.window.current() has no associated AppWindow. 
                    //Console.WriteLine("appwindow loading... " + new { current = chrome.app.window.current() });

                    // no HTML layout yet

                    if (Native.window.opener == null)
                        if (Native.window.parent == Native.window.self)
                        {



                            //chrome.app.runtime.
                            //chrome.app.runtime.Launched +=
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


                            var options = new
                            {
                                // works with and without a frame.

                                frame = "none",
                                //alwaysOnTop,
                                ////transparentBackground,
                                //alphaEnabled,
                                //resizable
                            };

                            // whats the async version of it?
                            chrome.app.window.create(
                                Native.document.location.pathname,
                                //null,
                                options,
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

                    Console.WriteLine("Application renew body:");
                    Console.WriteLine(Native.document.documentElement.AsXElement());

                    // if we are in a window lets add layout
                    //page = new IApp();

                    //page.AsNode().With(
                    //    n =>
                    //    {
                    //        Console.WriteLine("Application renew body " + new { childNodes = n.childNodes.Length });
                    //        Console.WriteLine("Application renew body " + new { childNodes = n.attributes.Length });

                    //        n.childNodes.WithEach(k => k.AttachToDocument());
                    //        n.attributes.WithEach(k => Native.Document.body.setAttribute(k.name, k.value));
                    //    }
                    //);

                    //var uri = "http://discover.xavalon.net";
                    var uri = "http://example.com";


                    uri.ToDocumentTitle();


                    // http://developer.chrome.com/apps/webview_tag.html
                    // http://stackoverflow.com/questions/16635739/google-chrome-app-webview-behavior

                    // 20140911 yes it does still work with a AppWindow frame.
                    // what about without frame?

                    var webview = Native.document.createElement("webview");
                    // You do not have permission to use <webview> tag. Be sure to declare 'webview' permission in your manifest. 
                    webview.setAttribute("partition", "p1");
                    webview.setAttribute("src", uri);
                    webview.style.SetLocation(0, 0);
                    webview.style.width = "100%";
                    webview.style.height = "100%";

                    webview.AttachToDocument();

                    //<webview id="wv1" partition="p1" style="width: 450px; height: 300px; border: 2px solid red" src="http://www.google.com"></webview>




                }
            #endregion


        }

    }
}
