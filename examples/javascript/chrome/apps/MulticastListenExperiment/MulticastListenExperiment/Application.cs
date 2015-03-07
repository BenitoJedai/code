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
using chrome;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using System.Windows.Forms;

namespace MulticastListenExperiment
{
    // http://www.snip2code.com/Snippet/19734/Visual-studio-intellisense-file-for-chro
    [Script(HasNoPrototype = true)]
    class xPointerLockPermissionRequest
    {
        // https://developer.chrome.com/apps/tags/webview#type-PointerLockPermissionRequest

        public void allow()
        {
        }
    }

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // will the service run on cloud or lan?
        public readonly ApplicationWebService service = new ApplicationWebService();



        static Form OpenUri(
            string uri,
            int DefaultWidth = 640,
            int DefaultHeight = 480,
            Action<FormStyler> AtFormCreated = null
            )
        {
            Console.WriteLine("OpenUri " + new { uri });




            #region  AtFormCreated
            if (AtFormCreated == null)
                AtFormCreated = AtFormCreated = s =>
                {
                    // X:\jsc.svn\examples\javascript\IsometricTycoonViewWithToolbar\IsometricTycoonViewWithToolbar\Application.cs
                    // X:\jsc.internal.svn\core\com.abstractatech.web\com.abstractatech.web\Domains\discover.xavalon.net\discover_xavalon_net.cs

                    // browser popup will use this color
                    ((__Form)s.Context).HTMLTargetContainerRef.style.backgroundColor = JSColor.FromRGB(0, 0, 0);

                    s.Caption.style.backgroundColor = JSColor.FromRGB(0, 0, 0);
                    s.TargetOuterBorder.style.boxShadow = "rgba(0, 0, 0, 0.3) 0px 0px 6px 3px";
                    s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(0, 0, 0);

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


            FormStyler.AtFormCreated = AtFormCreated;
            #endregion




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

                       // our window frame non client area plus inner body margin

                       if (that.FormBorderStyle == FormBorderStyle.None)
                       {
                           xappwindow.resizeTo(
                              DefaultWidth,
                              DefaultHeight
                             );
                       }
                       else
                       {
                           xappwindow.resizeTo(
                            DefaultWidth + 32,
                            DefaultHeight + 64
                           );
                       }

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


                                        f.Left = 0;
                                        f.Top = 0;


                                        f.FormClosing +=
                                            delegate
                                            {
                                                Console.WriteLine("FormClosing");
                                                appwindow.close();
                                            };


                                        #region  onRestored
                                        appwindow.onRestored.addListener(
                                            new Action(
                                                delegate
                                                {
                                                    that.CaptionShadow.Hide();

                                                }
                                            )
                                        );
                                        #endregion


                                        #region onMaximized
                                        appwindow.onMaximized.addListener(
                                        new Action(
                                                delegate
                                                {
                                                    that.CaptionShadow.Show();

                                                }
                                        )
                                        );
                                        #endregion


                                        #region onClosed
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
                                        #endregion

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

                                                         //Console.WriteLine("appwindow.contentWindow.onresize SizeTo " +
                                                         //    new
                                                         //    {
                                                         //        appwindow.contentWindow.Width,
                                                         //        appwindow.contentWindow.Height
                                                         //    }
                                                         //    );

                                                         f.Width = appwindow.contentWindow.Width;
                                                         f.Height = appwindow.contentWindow.Height;

                                                     }
                                            //)
                                            //)
                                             ;

                                        f.Width = appwindow.contentWindow.Width;
                                        f.Height = appwindow.contentWindow.Height;


                                        //Console.WriteLine("appwindow contentWindow onload");


                                        that.HTMLTarget.AttachTo(
                                            appwindow.contentWindow.document.body
                                        );



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



                    #region permissionrequest
                    // https://github.com/GoogleChrome/chromium-webview-samples
                    // permissionrequest
                    // https://developer.chrome.com/apps/tags/webview#type-WebRequestEventInteface
                    webview.addEventListener("permissionrequest",
                        (e) =>
                        {
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



                    // X:\jsc.svn\examples\javascript\WebGL\WebGLYomotsuTPS\WebGLYomotsuTPS\Application.cs
                    // http://src.chromium.org/viewvc/chrome/trunk/src/chrome/test/data/extensions/platform_apps/web_view/pointer_lock/main.js

                    that.InternalElement = (IHTMLIFrame)(object)webview;
                };

            }
            #endregion
            //var nn = new chrome.Notification
            //{
            //    Title = "extension to app",
            //    Message = new { message }.ToString(),
            //};




            var fff = new Form
            {

                Text = uri,
                ShowIcon = false
            };


            var w = new WebBrowser
            {

                //Dock = DockStyle.Fill 
            }.AttachTo(fff);



            #region SizeChanged
            fff.SizeChanged +=
                delegate
                {
                    //Console.WriteLine("SizeChanged");

                    var ClientSize = fff.ClientSize;


                    w.Width = ClientSize.Width;
                    w.Height = ClientSize.Height;

                };
            #endregion


            w.Navigate(uri);

            fff.Show();

            return fff;
        }


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
            //if (chrome.app.runtime != null)
            {
                //The JavaScript context calling chrome.app.window.current() has no associated AppWindow. 
                //Console.WriteLine("appwindow loading... " + new { current = chrome.app.window.current() });

                // no HTML layout yet

                if (Native.window.opener == null)
                    if (Native.window.parent == Native.window.self)
                    {
                        chrome.app.runtime.Launched +=
                                async delegate
                                {
                                    // runtime will launch only once?

                                    // http://developer.chrome.com/apps/app.window.html
                                    // do we even need index?

                                    // https://code.google.com/p/chromium/issues/detail?id=148857
                                    // https://developer.mozilla.org/en-US/docs/data_URIs

                                    // chrome-extension://mdcjoomcbillipdchndockmfpelpehfc/data:text/html,%3Ch1%3EHello%2C%20World!%3C%2Fh1%3E
                                    var appwindow = await chrome.app.window.create(Native.document.location.pathname, null);


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

                                };

                        return;
                    }

                // if we are in a window lets add layout
                new App().Container.AttachToDocument();
            }
            #endregion

            #region __chrome
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
            #endregion




            Action<string> AtUDPString = delegate { };

            // suggest: HTMLElements
            IHTMLElement.HTMLElementEnum.hr.AttachToDocument();


            // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionWithWorker\ChromeExtensionWithWorker\Application.cs
            // Fired when a connection is made from another extension.

            // X:\jsc.svn\examples\merge\TestDetectOpenFiles\TestDetectOpenFiles\Program.cs

            #region ConnectExternal
            chrome.runtime.ConnectExternal += e =>
            {
                // 0:10428ms extension connects to app: { id = fkgibadjpabiongmgoeomdbcefhabmah }
                // 
                Console.WriteLine("extension connects to app: " + new { e.sender.id });

                //e.postMessage("hello from app");


                var e_disconnected = false;

                e.onDisconnect.addListener(
                    new Action(
                        delegate
                        {
                            e_disconnected = true;

                            Console.WriteLine("extension onDisconnect from app");
                        }
                    )
                );


                e.onMessage.addListener(
                    new Action<string>(
                        xmlstring =>
                        {
                            var xml = XElement.Parse(xmlstring);

                            if (xml.Value.StartsWith("Visit me at "))
                            {
                                // what about android apps runnning on SSL?
                                // what about preview images?
                                // do we get localhost events too?

                                var uri = "http://" + xml.Value.SkipUntilOrEmpty("Visit me at ");

                                var fff = OpenUri(uri);


                                fff.FormClosed +=
                                    delegate
                                    {
                                        // dock into tab...

                                        AtUDPString(xmlstring);
                                    };

                            }

                        }
                    )
                );




                AtUDPString +=
                    xml =>
                    {
                        if (e_disconnected)
                            return;

                        e.postMessage(xml);

                    };
            };

            #endregion



            #region the new api. is it any better?
            new IHTMLButton { "chrome.sockets.udp.create" }.AttachToDocument().onclick +=
                async e =>
                {
                    // this aint defined for chrme38???
					// not available for c43 either.

                    // http://wefixbugs.com/blog/How-to-do-UDP-broadcast-using-chromesocketsudp-API-55233.html#.U8Krqm0wqCg
                    // https://developer.chrome.com/apps/app_network

                    e.Element.disabled = true;


                    var socket = await chrome.sockets.udp.create(new object());

                    var value_bind = await chrome.sockets.udp.bind(socket.socketId, "0.0.0.0", 40404);
                    var value_joinGroup = await chrome.sockets.udp.joinGroup(socket.socketId, "239.1.2.3");

                    e.Element.innerText = new { socket.socketId }.ToString();

                    chrome.sockets.udp.Receive +=
                        info =>
                        {
                            if (info.socketId != socket.socketId)
                                return;

                            var xml = Encoding.UTF8.GetString(info.data);

                            new IHTMLPre { new { info.remoteAddress, info.remotePort, xml } }.AttachToDocument();
                        };
                };
            #endregion



            #region  working API chrome.socket.create
            new IHTMLButton { "chrome.socket.create" }.AttachToDocument().onclick +=
                async e =>
                {
                    // X:\jsc.internal.git\market\chrome\ChromeMyJscSolutionsNet\ChromeMyJscSolutionsNet\Application.cs

                    e.Element.disabled = true;


                    var socket = await chrome.socket.create("udp", new object());

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
                        async delegate
                        {
							// x:\jsc.svn\examples\javascript\chrome\apps\chromeudpsendasync\chromeudpsendasync\application.cs

							var data = new ScriptCoreLib.JavaScript.WebGL.Uint8Array(
                                40, 41, 42
                            );

                            // Uncaught Error: Invocation of form socket.sendTo(object, string, integer, function) 
                            // doesn't match definition socket.sendTo(integer socketId, binary data, string address, integer port, function callback) 

                            var result = await socketId.sendTo(
                                data.buffer,
                                "239.1.2.3",
                                40404
                            );

                            new IHTMLDiv { innerText = new { result.bytesWritten }.ToString() }.AttachToDocument();

                        }
                    );
                    #endregion


                    var value_setMulticastTimeToLive = await socketId.setMulticastTimeToLive(30);

                    new IHTMLDiv { innerText = new { value_setMulticastTimeToLive }.ToString() }.AttachToDocument();


                    var value_bind = await socketId.bind("0.0.0.0", 40404);

                    new IHTMLDiv { innerText = new { value_bind }.ToString() }.AttachToDocument();

                    var value_joinGroup = await socketId.joinGroup("239.1.2.3");


                    new IHTMLDiv { innerText = new { value_joinGroup }.ToString() }.AttachToDocument();

                    var forever = true;

                    while (forever)
                    {
                        var result = await socketId.recvFrom(1048576);

                        new IHTMLDiv { innerText = new { result.resultCode }.ToString() }.AttachToDocument();


                        if (result.resultCode < 0)
                            return;

                        new IHTMLDiv { innerText = new { result.data.byteLength }.ToString() }.AttachToDocument();





                        byte[] source = new ScriptCoreLib.JavaScript.WebGL.Uint8ClampedArray(result.data);

                        var xml = Encoding.UTF8.GetString(source);

                        AtUDPString(xml);
                        new IHTMLPre { new { xml } }.AttachToDocument();
                        // 52 bytes
                    }

                };
            #endregion


            // https://code.google.com/p/chromium/issues/detail?id=246872
            // chrome.socket is not available: 'socket' requires a different Feature that is not present. 
            // chrome.socket is not available: 'socket' is only allowed for packaged apps, and this is a legacy packaged app. 



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
