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
using AndroidToChromeNotificationExperiment.Design;
using AndroidToChromeNotificationExperiment.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.WebGL;

namespace AndroidToChromeNotificationExperiment
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
            //service.InitializeComponent(page);

            Console.WriteLine("Application");

            var c = 0;

            #region notify
            Action<string, string, Action> notify =
                (title, message, yield) =>
                {
                    Action<string> callback =
                           notificationId =>
                           {

                               Console.WriteLine("create " + new { notificationId });

                               chrome.notifications.onClosed.addListener(
                                   new Action<string, bool>(
                                       (__notificationId, __byUser) =>
                                       {
                                           if (__notificationId != notificationId)
                                               return;

                                           Console.WriteLine("onClosed " + new { __notificationId, __byUser });
                                       }
                                   )
                               );

                               chrome.notifications.onClicked.addListener(
                                    new Action<string>(
                                        (__notificationId) =>
                                        {
                                            if (__notificationId != notificationId)
                                                return;


                                            Console.WriteLine("onClicked " + new { __notificationId });


                                            if (yield != null)
                                                yield();

                                            //Native.Window.open("http://example.com", "_blank");
                                        }
                                    )
                                );


                               chrome.notifications.onButtonClicked.addListener(
                                    new Action<string, int>(
                                        (__notificationId, __buttonIndex) =>
                                        {
                                            if (__notificationId != notificationId)
                                                return;

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
                            title = title,
                            message = message,


                            iconUrl = "assets/ScriptCoreLib/jsc.png"
                            //Invalid value for argument 2. Property 'iconUrl': Property is required. 
                            // Unable to download all specified images. 
                        },
                        callback
                    );
                };
            #endregion

            //            I/Web Console(17596): Application
            //I/Web Console(17596):  at http://192.168.1.103:13734/view-source:23649
            //E/Web Console(17596): Uncaught ReferenceError: chrome is not defined at http://192.168.1.103:13734/view-source:32745

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
                                        Console.WriteLine("appwindow udp");

                                        #region recvFrom
                                        Action<CreateInfo> atcreate =
                                         socket =>
                                         {
                                             Console.WriteLine("appwindow udp " + new { socket.socketId });

                                             //var x = Expando.Of(socket);

                                             //new IHTMLDiv { innerText = new { x.constructor }.ToString() }.AttachToDocument();
                                             //new IHTMLDiv { innerText = new { x.prototype }.ToString() }.AttachToDocument();

                                             //x.GetMemberNames().WithEach(
                                             //    member =>
                                             //    {
                                             //        new IHTMLDiv { innerText = new { member }.ToString() }.AttachToDocument();

                                             //    }
                                             //);

                                             var socketId = socket.socketId;

                                             new IHTMLDiv { innerText = new { socketId }.ToString() }.AttachToDocument();

                                             #region send data
                                             new IHTMLButton { innerText = "send data" }.AttachToDocument().WhenClicked(
                                                 delegate
                                                 {
                                                     var data = new ScriptCoreLib.JavaScript.WebGL.Uint8Array(
                                                         40, 41, 42
                                                     );

                                                     // Uncaught Error: Invocation of form socket.sendTo(object, string, integer, function) 
                                                     // doesn't match definition socket.sendTo(integer socketId, binary data, string address, integer port, function callback) 

                                                     chrome.socket.sendTo(
                                                         socketId,
                                                         data.buffer,
                                                         "239.1.2.3",
                                                         40404,

                                                         callback:
                                                         new Action<WriteInfo>(
                                                             result =>
                                                             {
                                                                 new IHTMLDiv { innerText = new { result.bytesWritten }.ToString() }.AttachToDocument();
                                                             }
                                                         )
                                                     );


                                                 }
                                             );
                                             #endregion

                                             Action<int> at_setMulticastTimeToLive =
                                                 value_setMulticastTimeToLive =>
                                                 {
                                                     new IHTMLDiv { innerText = new { value_setMulticastTimeToLive }.ToString() }.AttachToDocument();

                                                     Action<int> at_bind =
                                                         value_bind =>
                                                         {
                                                             new IHTMLDiv { innerText = new { value_bind }.ToString() }.AttachToDocument();

                                                             chrome.socket.joinGroup(socketId, "239.1.2.3",

                                                                 callback: new Action<int>(
                                                                      value_joinGroup =>
                                                                      {
                                                                          new IHTMLDiv { innerText = new { value_joinGroup }.ToString() }.AttachToDocument();


                                                                          Action poll = null;

                                                                          poll = delegate
                                                                          {
                                                                              chrome.socket.recvFrom(socketId,
                                                                                  1048576,

                                                                                  callback: new Action<RecvFromInfo>(
                                                                                      result =>
                                                                                      {
                                                                                          try
                                                                                          {
                                                                                              var bytes = new byte[result.data.byteLength];

                                                                                              var source = new ScriptCoreLib.JavaScript.WebGL.Uint8Array(
                                                                                                     result.data,
                                                                                                     0,
                                                                                                     result.data.byteLength
                                                                                                     );

                                                                                              for (uint i = 0; i < source.length; i++)
                                                                                              {
                                                                                                  bytes[i] = source[i];
                                                                                              }

                                                                                              var text = Encoding.UTF8.GetString(bytes);

                                                                                              notify("recvFrom", new
                                                                                              {
                                                                                                  result.resultCode,
                                                                                                  result.address,
                                                                                                  result.port,
                                                                                                  result.data.byteLength,
                                                                                              }.ToString()
                                                                                                 + "\n" + text,

                                                                                                 delegate
                                                                                                 {
                                                                                                     Console.WriteLine("Click!");

                                                                                                     var xml = XElement.Parse(text);

                                                                                                     if (xml.Value.StartsWith("Visit me at "))
                                                                                                     {
                                                                                                         //StringExtensions
                                                                                                         //StringExtensions

                                                                                                         var uri = "http://" + xml.Value.SkipUntilOrEmpty("Visit me at ");


                                                                                                         Console.WriteLine(new { uri });

                                                                                                         #region AppWindow
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

                                                                                                                                  // http://developer.chrome.com/apps/webview_tag.html
                                                                                                                                  // http://stackoverflow.com/questions/16635739/google-chrome-app-webview-behavior
                                                                                                                                  var webview = Native.Document.createElement("webview");
                                                                                                                                  // You do not have permission to use <webview> tag. Be sure to declare 'webview' permission in your manifest. 
                                                                                                                                  webview.setAttribute("partition", "p1");
                                                                                                                                  webview.setAttribute("src", uri);
                                                                                                                                  webview.style.SetLocation(0, 0);
                                                                                                                                  webview.style.width = "100%";
                                                                                                                                  webview.style.height = "100%";

                                                                                                                                  webview.AttachTo(appwindow.contentWindow.document.body);
                                                                                                                                  // https://code.google.com/p/chromium/issues/detail?id=248421
                                                                                                                                  // https://docs.google.com/document/d/1T4MOIw0CN_3RU5Dr9f6DDZUiXye0x0CGxgfYStP2Sms/edit?pli=1

                                                                                                                                  webview.addEventListener(
                                                                                                                                      "newwindow",
                                                                                                                                      new Action<IEvent>(
                                                                                                                                          ee =>
                                                                                                                                          {
                                                                                                                                              // Uncaught Error: <webview>: An action has already been taken for this "newwindow" event. 

                                                                                                                                              ee.preventDefault();

                                                                                                                                              dynamic e = ee;


                                                                                                                                              // https://plus.google.com/100132233764003563318/posts/2dNmkacjiat

                                                                                                                                              string targetUrl = e.targetUrl;
                                                                                                                                              Console.WriteLine(new { targetUrl });

                                                                                                                                              // attach or discard
                                                                                                                                              object newwindow = e.window;

                                                                                                                                              Console.WriteLine(new { newwindow });

                                                                                                                                              chrome.app.window.create(
                                                                                                                                                   Native.Document.location.pathname,
                                                                                                                                                   null,
                                                                                                                                                   new Action<AppWindow>(
                                                                                                                                                       xappwindow =>
                                                                                                                                                       {
                                                                                                                                                           // Uncaught TypeError: Cannot read property 'contentWindow' of undefined 

                                                                                                                                                           Console.WriteLine("appwindow loading... " + new { xappwindow });
                                                                                                                                                           Console.WriteLine("appwindow loading... " + new { xappwindow.contentWindow });



                                                                                                                                                           xappwindow.contentWindow.onload +=
                                                                                                                                                                delegate
                                                                                                                                                                {
                                                                                                                                                                    var xwebview = xappwindow.contentWindow.document.createElement("webview");
                                                                                                                                                                    //var xwebview = xappwindow.contentWindow.document.createElement("webview");
                                                                                                                                                                    // You do not have permission to use <webview> tag. Be sure to declare 'webview' permission in your manifest. 
                                                                                                                                                                    //xwebview.setAttribute("partition", "p1");
                                                                                                                                                                    //xwebview.setAttribute("src", uri);
                                                                                                                                                                    xwebview.style.SetLocation(0, 0);
                                                                                                                                                                    xwebview.style.width = "100%";
                                                                                                                                                                    xwebview.style.height = "100%";

                                                                                                                                                                    xwebview.AttachTo(xappwindow.contentWindow.document.body);
                                                                                                                                                                    //https://groups.google.com/a/chromium.org/forum/#!topic/chromium-apps/zzGLSiyWCnM
                                                                                                                                                                    // <webview>: Unable to attach the new window to the provided webview. 


                                                                                                                                                                    new IFunction("w", "v", "w.attach(v);").apply(null, newwindow, xwebview);

                                                                                                                                                                };
                                                                                                                                                       }
                                                                                                                                                                               ));



                                                                                                                                          }
                                                                                                                                    ),
                                                                                                                                    false
                                                                                                                                  );
                                                                                                                              };

                                                                                                                          //Uncaught TypeError: Cannot read property 'contentWindow' of undefined 




                                                                                                                      }
                                                                                                                  )
                                                                                                              );
                                                                                                         #endregion


                                                                                                         // <webview>: A new window was blocked. 



                                                                                                         //Native.Window.open(uri);




                                                                                                     }
                                                                                                 }
                                                                                              );
                                                                                          }
                                                                                          catch
                                                                                          {
                                                                                              notify("recvFrom", "error", null);
                                                                                          }



                                                                                          if (result.resultCode < 0)
                                                                                              return;

                                                                                          //new IHTMLDiv { innerText = new { result.data.byteLength }.ToString() }.AttachToDocument();

                                                                                          poll();
                                                                                      }
                                                                                  )
                                                                              );
                                                                          };

                                                                          poll();
                                                                      }
                                                                 )
                                                             );
                                                         };

                                                     chrome.socket.bind(socketId, "0.0.0.0", 40404, at_bind);

                                                 };

                                             chrome.socket.setMulticastTimeToLive(socket.socketId, 30, at_setMulticastTimeToLive);
                                         };

                                        // https://code.google.com/p/chromium/issues/detail?id=246872
                                        // chrome.socket is not available: 'socket' requires a different Feature that is not present. 
                                        // chrome.socket is not available: 'socket' is only allowed for packaged apps, and this is a legacy packaged app. 
                                        chrome.socket.create("udp", new object(), atcreate);
                                        #endregion

                                        Action spawn = null;

                                        spawn = delegate
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


                                                            appwindow.onClosed.addListener(
                                                                new Action(
                                                                    delegate
                                                                    {
                                                                        notify("Ok!", "You can still click on the button on your android. Click here to see diagnostics.",
                                                                            spawn

                                                                        );

                                                                    }
                                                                )
                                                            );


                                                        }
                                                    )
                                                );
                                            };


                                        notify("Ready!", "You can already click on the button on your android. Click here to see diagnostics.",
                                            spawn

                                        );

                                    }
                                )
                            );
                            return;
                        }

                    Console.WriteLine("Application renew body:");
                    Console.WriteLine(Native.Document.documentElement.AsXElement());

                    // if we are in a window lets add layout
                    page = new IApp();

                    page.AsNode().With(
                        n =>
                        {
                            Console.WriteLine("Application renew body " + new { childNodes = n.childNodes.Length });
                            Console.WriteLine("Application renew body " + new { childNodes = n.attributes.Length });

                            n.childNodes.WithEach(k => k.AttachToDocument());
                            n.attributes.WithEach(k => Native.Document.body.setAttribute(k.name, k.value));
                        }
                    );

                    // we are running in chrome! 
                    page.NotifyChromeViaLANBroadcast.disabled = true;

                    Console.WriteLine("Application renew body done:");
                    Console.WriteLine(Native.Document.documentElement.AsXElement());

                    page.ChromeNotification.disabled = false;
                    page.ChromeNotification.onclick +=
                        delegate
                        {
                            notify("Primary Title", "Primary message to display", delegate
                            {
                                Native.Window.open("http://example.com", "_blank");


                            });

                        };



                }
            #endregion



            page.NotifyChromeViaLANBroadcast.onclick +=
                delegate
                {
                    // Send data from JavaScript to the server tier
                    service.NotifyChromeViaLANBroadcast(
                        @"A string from JavaScript.",
                        value => value.ToDocumentTitle()
                    );
                };
        }

    }
}
