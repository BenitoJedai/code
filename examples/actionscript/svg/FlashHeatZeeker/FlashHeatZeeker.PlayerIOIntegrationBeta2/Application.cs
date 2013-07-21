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

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Console.WriteLine("Application "
                + new
                {
                    Native.Document.location.href,
                    Native.Window.parent,
                    Native.Window.opener
                }
            );

            #region switch to chrome AppWindow
            if (Expando.InternalIsMember(Native.Window, "chrome"))
                if (chrome.app.runtime != null)
                {





                    Console.WriteLine("Application switch to chrome AppWindow");

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

                    Console.WriteLine("Application renew body?");

                    //// http://developer.chrome.com/apps/webview_tag.html
                    //// http://stackoverflow.com/questions/16635739/google-chrome-app-webview-behavior
                    var webview = Native.Document.createElement("webview");
                    // You do not have permission to use <webview> tag. Be sure to declare 'webview' permission in your manifest. 
                    webview.setAttribute("partition", "p1");
                    webview.setAttribute("src", Native.Document.location.pathname + "#webview");
                    webview.style.SetLocation(0, 0);
                    webview.style.width = "100%";
                    webview.style.height = "100%";

                    webview.AttachToDocument();

                    //<webview id="wv1" partition="p1" style="width: 450px; height: 300px; border: 2px solid red" src="http://www.google.com"></webview>


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


                    return;
                }
            #endregion







            sprite.wmode();

            sprite.AttachSpriteToDocument().With(
                   embed =>
                   {
                       embed.style.SetLocation(0, 0);
                       embed.style.SetSize(Native.Window.Width, Native.Window.Height);

                       Native.Window.onresize +=
                           delegate
                           {
                               embed.style.SetSize(Native.Window.Width, Native.Window.Height);
                           };
                   }
               );


            "Operation «Heat Zeeker»".ToDocumentTitle();

            try
            {
                Console.WriteLine(new { chrome.app.isInstalled });

            }
            catch
            {
                Console.WriteLine("error, not in chrome?");
            }

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
