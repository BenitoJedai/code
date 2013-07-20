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
using MulticastListenExperiment.Design;
using MulticastListenExperiment.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace MulticastListenExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // will the service run on cloud or lan?
        public readonly ApplicationWebService service = new ApplicationWebService();



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://developer.chrome.com/extensions/contentSecurityPolicy.html
            //var __chrome = new IFunction("return window['chrome'];").apply(null);


            //new { chrome = __chrome }.ToString().ToDocumentTitle();


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


            // not an app
            //{ member = loadTimes }
            //{ member = csi }
            //{ member = app }
            //{ member = webstore }

            // running as a legacy app
            //{ member = loadTimes }
            //{ member = csi }
            //{ member = Event }
            //{ member = activityLogPrivate }
            //{ member = adview }
            //{ member = alarms }
            //{ member = app }
            //{ member = audio }
            //{ member = autotestPrivate }
            //{ member = bluetooth }
            //{ member = bookmarkManagerPrivate }
            //{ member = bookmarks }
            //{ member = browserAction }
            //{ member = browsingData }
            //{ member = chromeosInfoPrivate }
            //{ member = cloudPrintPrivate }
            //{ member = commandLinePrivate }
            //{ member = commands }
            //{ member = contentSettings }
            //{ member = contextMenus }
            //{ member = cookies }
            //{ member = debugger }
            //{ member = declarativeContent }
            //{ member = declarativeWebRequest }
            //{ member = developerPrivate }
            //{ member = diagnostics }
            //{ member = dial }
            //{ member = downloads }
            //{ member = echoPrivate }
            //{ member = enterprise }
            //{ member = experimental }
            //{ member = extension }
            //{ member = feedbackPrivate }
            //{ member = fileBrowserHandler }
            //{ member = fileBrowserPrivate }
            //{ member = fileSystem }
            //{ member = fontSettings }
            //{ member = history }
            //{ member = i18n }
            //{ member = identity }
            //{ member = identityPrivate }
            //{ member = idle }
            //{ member = input }
            //{ member = inputMethodPrivate }
            //{ member = location }
            //{ member = management }
            //{ member = mediaGalleries }
            //{ member = mediaGalleriesPrivate }
            //{ member = mediaPlayerPrivate }
            //{ member = metricsPrivate }
            //{ member = musicManagerPrivate }
            //{ member = networkingPrivate }
            //{ member = notifications }
            //{ member = omnibox }
            //{ member = pageAction }
            //{ member = pageActions }
            //{ member = pageCapture }
            //{ member = pageLauncher }
            //{ member = permissions }
            //{ member = power }
            //{ member = preferencesPrivate }
            //{ member = privacy }
            //{ member = proxy }
            //{ member = pushMessaging }
            //{ member = rtcPrivate }
            //{ member = runtime }
            //{ member = scriptBadge }
            //{ member = serial }
            //{ member = sessionRestore }
            //{ member = socket }
            //{ member = storage }
            //{ member = streamsPrivate }
            //{ member = syncFileSystem }
            //{ member = systemIndicator }
            //{ member = systemInfo }
            //{ member = systemPrivate }
            //{ member = tabCapture }
            //{ member = tabs }
            //{ member = terminalPrivate }
            //{ member = test }
            //{ member = topSites }
            //{ member = tts }
            //{ member = ttsEngine }
            //{ member = types }
            //{ member = usb }
            //{ member = wallpaperPrivate }
            //{ member = webNavigation }
            //{ member = webRequest }
            //{ member = webSocketProxyPrivate }
            //{ member = webstorePrivate }
            //{ member = webview }
            //{ member = windows }

            // as packaged app
            //            { member = loadTimes }
            //{ member = csi }
            //{ member = Event }
            //{ member = activityLogPrivate }
            //{ member = adview }
            //{ member = alarms }
            //{ member = app }
            //{ member = audio }
            //{ member = autotestPrivate }
            //{ member = bluetooth }
            //{ member = bookmarkManagerPrivate }
            //{ member = bookmarks }
            //{ member = browserAction }
            //{ member = browsingData }
            //{ member = chromeosInfoPrivate }
            //{ member = cloudPrintPrivate }
            //{ member = commandLinePrivate }
            //{ member = commands }
            //{ member = contentSettings }
            //{ member = contextMenus }
            //{ member = cookies }
            //{ member = debugger }
            //{ member = declarativeContent }
            //{ member = declarativeWebRequest }
            //{ member = developerPrivate }
            //{ member = diagnostics }
            //{ member = dial }
            //{ member = downloads }
            //{ member = echoPrivate }
            //{ member = enterprise }
            //{ member = experimental }
            //{ member = extension }
            //{ member = feedbackPrivate }
            //{ member = fileBrowserHandler }
            //{ member = fileBrowserPrivate }
            //{ member = fileSystem }
            //{ member = fontSettings }
            //{ member = history }
            //{ member = i18n }
            //{ member = identity }
            //{ member = identityPrivate }
            //{ member = idle }
            //{ member = input }
            //{ member = inputMethodPrivate }
            //{ member = location }
            //{ member = management }
            //{ member = mediaGalleries }
            //{ member = mediaGalleriesPrivate }
            //{ member = mediaPlayerPrivate }
            //{ member = metricsPrivate }
            //{ member = musicManagerPrivate }
            //{ member = networkingPrivate }
            //{ member = notifications }
            //{ member = omnibox }
            //{ member = pageAction }
            //{ member = pageActions }
            //{ member = pageCapture }
            //{ member = pageLauncher }
            //{ member = permissions }
            //{ member = power }
            //{ member = preferencesPrivate }
            //{ member = privacy }
            //{ member = proxy }
            //{ member = pushMessaging }
            //{ member = rtcPrivate }
            //{ member = runtime }
            //{ member = scriptBadge }
            //{ member = serial }
            //{ member = sessionRestore }
            //{ member = socket }
            //{ member = storage }
            //{ member = streamsPrivate }
            //{ member = syncFileSystem }
            //{ member = systemIndicator }
            //{ member = systemInfo }
            //{ member = systemPrivate }
            //{ member = tabCapture }
            //{ member = tabs }
            //{ member = terminalPrivate }
            //{ member = test }
            //{ member = topSites }
            //{ member = tts }
            //{ member = ttsEngine }
            //{ member = types }
            //{ member = usb }
            //{ member = wallpaperPrivate }
            //{ member = webNavigation }
            //{ member = webRequest }
            //{ member = webSocketProxyPrivate }
            //{ member = webstorePrivate }
            //{ member = webview }
            //{ member = windows }

            //Expando.Of(__chrome).GetMemberNames().WithEach(
            //    member =>
            //    {
            //        new IHTMLDiv { innerText = new { member }.ToString() }.AttachToDocument();

            //    }
            //);

            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);

            // suggest: HTMLElements
            IHTMLElement.HTMLElementEnum.hr.AttachToDocument();

            Action<CreateInfo> atcreate =
                socket =>
                {
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
                                    
                                        callback: new  Action<int> (
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
                                                                new IHTMLDiv { innerText = new { result.resultCode }.ToString() }.AttachToDocument();


                                                                if (result.resultCode < 0)
                                                                    return;

                                                                new IHTMLDiv { innerText = new { result.data.byteLength }.ToString() }.AttachToDocument();

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

        }

    }

    public static class X
    {
        public static IHTMLElement AttachToDocument(this IHTMLElement.HTMLElementEnum node)
        {
            var e = new IHTMLElement(node);

            e.AttachToDocument();
            return e;
        }
    }
}
