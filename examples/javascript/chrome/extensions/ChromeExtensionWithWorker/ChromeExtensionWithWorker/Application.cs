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
using ChromeExtensionWithWorker;
using ChromeExtensionWithWorker.Design;
using ChromeExtensionWithWorker.HTML.Pages;
using chrome;
using System.Net;
using System.Threading;
using System.Diagnostics;
using ScriptCoreLib.Query.Experimental;
using System.Data.SQLite;
using ChromeExtensionWithWorker.Data;
using System.Windows.Forms;

namespace ChromeExtensionWithWorker
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static Application()
        {
            Console.WriteLine("Application.cctor");

            // this will run as extension, tab ui, tab worker

            #region QueryExpressionBuilder.WithConnection
            QueryExpressionBuilder.WithConnection =
                y =>
                {
                    var cc = new SQLiteConnection();
                    cc.Open();
                    y(cc);
                    cc.Dispose();
                };
            #endregion
        }


        public Application(IApp page)
        {
            // jsc does not yet pre package chrome apps nor extensions
            // thus we do it manually.
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140712
            // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeTabsExperiment\ChromeTabsExperiment\Application.cs

            // A single extension can override only one page. For example, an extension can't override both the Bookmark Manager and History pages.


            // we could provide special API for scriptcorelib runtime on the tab being loaded
            // http://blog.chromium.org/

            // what else can we override besides options?
            // "options_page": "Application.htm",
            // https://code.google.com/p/chromium/issues/detail?id=171752
            // https://developer.chrome.com/extensions/options

            // can we provide an API about available android devices?

            #region self_chrome_tabs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_tabs = self_chrome.tabs;

            if (self_chrome_tabs != null)
            {
                #region Suspend
                chrome.runtime.Suspend += delegate
                  {
                      // dispose all injection done so far?
                      Console.WriteLine("chrome.runtime.Suspend");
                  };
                #endregion


                // jsc, add chrome nuget
                #region Installed
                chrome.runtime.Installed += delegate
                {
                    // our API does not have a Show
                    new chrome.Notification
                    {
                        Message = "Extension Installed!"
                    };
                };
                #endregion

                Action<string> AtMessageFromTabToExtensionForApplication = delegate { };

                Action<string> AtUDPString = delegate { };

                var IgnoreSecondaryUpdatesFor = new List<TabIdInteger>();

                #region Updated
                chrome.tabs.Updated +=
                    async (i, x, tab) =>
                    {
                        // chrome://newtab/

                        if (tab.url.StartsWith("chrome-devtools://"))
                            return;

                        if (tab.url.StartsWith("chrome://"))
                            return;

                        if (tab.status != "complete")
                            return;


                        if (IgnoreSecondaryUpdatesFor.Contains(tab.id))
                            return;

                        // inject?

                        // what if we sent the uri to our android tab?
                        var n = new Notification
                        {
                            Message = "Updated! " + new { tab.id, tab.url }
                        };

                        IgnoreSecondaryUpdatesFor.Add(tab.id);



                        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\WebClient.cs

                        //var aFileParts = new[] { code };
                        //var oMyBlob = new Blob(aFileParts, new { type = "text/javascript" }); // the blob
                        //var url = oMyBlob.ToObjectURL();

                        //Console.WriteLine(new { url });


                        // when will roslyn learn to expose events as async?
                        await tab.pageAction.async.onclick;

                        var nn = new Notification
                        {
                            Message = "Clicked " + new { tab.id, tab.url }
                        };



                        var code = await new WebClient().DownloadStringTaskAsync(
                             new Uri(Worker.ScriptApplicationSource, UriKind.Relative)
                        );


                        //Console.WriteLine("before insertCSS");

                        // https://developer.chrome.com/extensions/tabs#type-Tab
                        // http://stackoverflow.com/questions/9795058/how-to-run-chrome-tabs-insertcss-from-the-background-page-on-each-page
                        // chrome::
                        tab.id.insertCSS(
                            new
                            {
                                // .css do we have a CSS parser/builder available yet?

                                //code = "body { border: 1em solid red; }"
                                code = "body { border-left: 1em solid yellow; }"
                            },
                            null
                        );

                        // await tab.id.Run(async delegate {});
                        //};


                        // Unchecked runtime.lastError while running tabs.executeScript: No source code or file specified.
                        var result = await tab.id.executeScript(
                            //new { file = url }
                            new { code }
                        );




                        // how to use connect?
                        var p = tab.id.connect();
                        var p_disconnected = false;

                        p.onDisconnect.addListener(
                            new Action(
                                delegate
                                {
                                    p_disconnected = true;
                                }
                            )
                        );


                        p.onMessage.addListener(
                            new Action<string>(
                                data =>
                                {
                                    AtMessageFromTabToExtensionForApplication(data);

                                    //Console.WriteLine("extension: onMessage " + new { data });
                                    //new Notification
                                    //{
                                    //    Message = "extension onMessage: " + new { tab.id, data }
                                    //};
                                }
                            )
                        );


                        //p.postMessage("hello executeScript");


                        // lets enable workers within tab
                        p.postMessage(new { code });

                        AtUDPString +=
                            xml =>
                            {
                                //Console.WriteLine("extension: " + new { xml });

                                if (p_disconnected)
                                    return;

                                if (tab.active)
                                {
                                    // only if active tab?
                                    p.postMessage(new { xml });
                                }
                            };
                    };
                #endregion





                #region __MulticastListenExperiment

                // can this chrome.extension connect to a chrome app?
                var __MulticastListenExperiment = "aemlnmcokphbneegoefdckonejmknohh";

                Console.WriteLine("chrome.runtime.connect " + new { __MulticastListenExperiment });

                // what if the app is not loaded, or is inactive?

                chrome.runtime.connect(__MulticastListenExperiment).With(
                      port =>
                      {
                          // Uncaught TypeError: Cannot read property 'id' of undefined
                          Console.WriteLine("chrome.runtime.connect OK " + new { __MulticastListenExperiment, chrome.runtime.lastError });
                          // 0:60ms chrome.runtime.connect OK { __MulticastListenExperiment = aemlnmcokphbneegoefdckonejmknohh, lastError =  } 
                          //Console.WriteLine("chrome.runtime.connect OK " + new { __MulticastListenExperiment, chrome.runtime.lastError, port.sender.id });


                          port.onDisconnect.addListener(
                              new Action(
                                  delegate
                                  {
                                      Console.WriteLine("chrome.runtime.connect onDisconnect " + new { __MulticastListenExperiment });
                                  }
                              )
                          );

                          // we wont know if we got the connection...
                          port.onMessage.addListener(
                                new Action<object>(
                                    message =>
                                    {
                                        // %c0:182ms app to extension { message = hello from app }

                                        //Console.WriteLine("app to extension " + new { message, port.sender.id });
                                        Console.WriteLine("app to extension " + new { message });

                                        //var nn = new chrome.Notification
                                        //{
                                        //    Title = "app to extension",
                                        //    Message = new { message }.ToString(),
                                        //};

                                        AtUDPString((string)message);
                                    }
                                )
                            );

                          AtMessageFromTabToExtensionForApplication +=
                              message =>
                              {
                                  port.postMessage(message);
                              };
                      }
                  );
                #endregion


                //Console.WriteLine("chrome.runtime.connect exit " + new { __MulticastListenExperiment });

                return;
            }
            #endregion

            Native.body.style.borderTop = "1em solid yellow";

            // if we were injected by executeScript, how would we launch a worker now?

            // VM608:41423


            // https://code.google.com/p/v8/wiki/JavaScriptStackTraceApi
            //getFileName: if this function was defined in a script returns the name of the script

            //var xx = new Exception();

            //Console.WriteLine(new { xx.StackTrace });
            //0:46ms { StackTrace = Error
            //    at AgsABmfn8j_aa_bqCu59PrPg (http://192.168.43.252:12879/view-source:27498:56)


            //0:25ms { StackTrace = Error
            //    at AgsABmfn8j_aa_bqCu59PrPg (<anonymous>:27498:56)
            //    at mV1GtUeaNze1ZFoeDPNoHQ.type$mV1GtUeaNze1ZFoeDPNoHQ.AwAABkeaNze1ZFoeDPNoHQ (<anonymous>:59652:9)


            // can we atleast try to ask for the source?


            Action<string> Notify = delegate { };

            new xxAvatar().Create();



            #region go worker
            new IHTMLButton { 
                "go worker: ",
               (

                    // we can read our own data, and any other browser extension can too, encrypt it?
                    from x in new xxAvatar()
                    orderby x.Key descending
                    //select x.Tag
                    //select new  {x.Tag}
                    select new xxAvatarRow { Tag = x.Tag}
                ).FirstOrDefaultAsync()
            }.AttachToDocument().With(
            button =>
            {
                button.style.position = IStyle.PositionEnum.@fixed;
                button.style.left = "1em";
                button.style.bottom = "1em";
                //button.style.zIndex = 1000;
                button.style.zIndex = 10000;




                button.onclick +=
                    async e =>
                    {
                        e.Element.disabled = true;


                        button.innerText = "working... ";


                        // live updates from worker via DB.
                        button.Add(

                             () => (

                                // we can read our own data, and any other browser extension can too, encrypt it?
                                from xx in new xxAvatar()
                                orderby xx.Key descending
                                //select x.Tag
                                //select new  {x.Tag}
                                select new xxAvatarRow { Tag = xx.Tag }
                            ).FirstOrDefaultAsync()

                        );


                        Native.body.style.borderTop = "1em solid blue";


                        var scopedata1 = "enter";
                        var scopedata2 = "exit";

                        var x = await Task.Run(
                            async delegate
                            {
                                var s = Stopwatch.StartNew();



                                for (int index = 0; index < 10; index++)
                                {

                                    // does it show up?
                                    await new xxAvatar().InsertAsync(
                                        new xxAvatarRow
                                        {
                                            Tag = "tab worker! " + scopedata1 + new { index, s.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId },
                                        }
                                    );

                                    Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });
                                    await Task.Delay(99);
                                }


                                // does it show up?
                                await new xxAvatar().InsertAsync(
                                    new xxAvatarRow
                                    {
                                        Tag = "tab worker! " + scopedata2 + new { s.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId },
                                    }
                                );

                                return "webview worker calling extension " + new { s.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId };
                            }
                        );


                        button.innerText = "go worker: ";


                        // live updates from worker via DB.
                        button.Add(

                            // show a static field, so we wont spam console
                             (

                                // we can read our own data, and any other browser extension can too, encrypt it?
                                from xx in new xxAvatar()
                                orderby xx.Key descending
                                //select x.Tag
                                //select new  {x.Tag}
                                select new xxAvatarRow { Tag = xx.Tag }
                            ).FirstOrDefaultAsync()

                        );

                        Notify(x);

                        Native.body.style.borderTop = "1em solid pink";
                        e.Element.disabled = false;
                    };




            }
            );
            #endregion



            var forms = new List<Form>();



            #region onxmlmessage
            Action<XElement> onxmlmessage = null;

            onxmlmessage =
                xml =>
                {
                    // we are working within another webapp
                    // the tab was told by the extension
                    // the extension was told by the app
                    // the app was told by udp broadcast
                    // that a jsc app is now running.

                    // cool.

                    // can we load it into here?

                    if (xml.Value.StartsWith("Visit me at "))
                    {
                        // what about android apps runnning on SSL?
                        // what about preview images?
                        // do we get localhost events too?

                        var uri = "http://" + xml.Value.SkipUntilOrEmpty("Visit me at ");

                        if (forms.Any(x => x.Text == uri))
                        {
                            // look already opened!
                            return;
                        }

                        // X:\jsc.internal.git\market\chrome\ChromeMyJscSolutionsNet\ChromeMyJscSolutionsNet\Application.cs

                        // "X:\jsc.svn\examples\javascript\android\com.abstractatech.appmanager\com.abstractatech.appmanager.sln"
                        // X:\jsc.svn\examples\javascript\android\com.abstractatech.appmanager\com.abstractatech.appmanager\Application.cs




                        // can we pop ourselves out of here too?
                        // can chrome>extensions do AppWindows?


                        // on some pages they style our div. shall we use a non div to get nonstyled element?
                        // or do we need shadow DOM? is it even available yet for us?
                        var f = new Form { Text = uri, ShowIcon = false };
                        forms.Add(f);

                        var w = new WebBrowser();
                        w.Dock = DockStyle.Fill;

                        f.Controls.Add(w);

                        w.Navigate(uri);
                        f.Show();

                        f.FormClosed +=
                            delegate
                            {
                                Console.WriteLine("FormClosed " + new { uri });

                                forms.Remove(f);

                            };

                        // if we close it we want it to be gone for good.
                        // the extension cannot detatch our frame. it may need to ask the app to reopen this virtual tab...
                        f.PopupInsteadOfClosing(HandleFormClosing: false,

                            SpecialCloseOnLeft:
                                delegate
                                {
                                    // shall we ask app:: to reopen uri in AppWindow?
                                    Notify(xml.ToString());

                                    f.Close();
                                }
                        );


                    }

                };
            #endregion


            #region Connect
            object self_chrome_runtime = self_chrome.runtime;
            Console.WriteLine(new { self_chrome_runtime });
            // 0:39ms { self_chrome_runtime = [object Object] }
            if (self_chrome_runtime != null)
            {
                chrome.runtime.Connect +=
                        e =>
                        {
                            // port
                            // extension connects to injected tab?
                            Console.WriteLine("chrome.runtime.Connect " + new { Native.document.domain });
                            //Console.WriteLine("chrome.runtime.Connect " + new { Native.document.domain, e.sender.id });

                            //0:123ms chrome.runtime.Connect
                            //0:126ms webview: onMessage { data = hello executeScript }


                            //http://stackoverflow.com/questions/15798516/is-there-an-event-for-when-a-chrome-extension-popup-is-closed


                            e.onDisconnect.addListener(
                                new Action(
                                    delegate
                                    {
                                        // extension unloaded
                                        Native.body.style.borderTop = "0em solid red";
                                        Native.body.style.borderLeft = "0em solid red";

                                        forms.WithEach(f => f.Close());

                                    }
                                )
                            );



                            e.onMessage.addListener(
                                new Action<dynamic>(
                                    data =>
                                    {
                                        string xml = data.xml;
                                        if (xml != null)
                                        {
                                            // tab injection was notified by extension, by app, by udp android?
                                            Native.body.style.borderLeft = "1em solid red";

                                            // 0:40394ms { xml = <string c="1">Visit me at 192.168.1.67:6169</string> }
                                            Console.WriteLine(new { xml });


                                            // X:\jsc.internal.git\market\chrome\ChromeMyJscSolutionsNet\ChromeMyJscSolutionsNet\Application.cs

                                            onxmlmessage(
                                                XElement.Parse(xml)
                                                );

                                        }

                                        string code = data.code;
                                        if (code != null)
                                        {

                                            //Console.WriteLine("webview: onMessage " + new { data });

                                            // %c0:41906ms extension: onMessage { data = connected! }
                                            //e.postMessage("got code! " + new { code.Length });

                                            Native.body.style.borderTop = "1em solid red";

                                            // InternalInlineWorker

                                            // http://stackoverflow.com/questions/21408510/chrome-cant-load-web-worker
                                            // this wont work for file:// tabs

                                            // message: "Failed to construct 'Worker': Script at 'blob:null/f544915f-b855-480b-8db8-bd6c686829b9#worker' cannot be accessed from origin 'null'."
                                            var aFileParts = new[] { code };
                                            var oMyBlob = new Blob(aFileParts, new { type = "text/javascript" }); // the blob
                                            var url = oMyBlob.ToObjectURL();

                                            InternalInlineWorker.ScriptApplicationSourceForInlineWorker = url;

                                            Notify = x =>
                                            {
                                                // Error in event handler for (unknown): Attempting to use a disconnected port object Stack trace: Error: Attempting to use a disconnected port object
                                                e.postMessage(x);
                                            };
                                        }
                                    }
                                )
                            );


                        };

                //chrome.runtime.Message +=
                //    delegate
                //    {
                //        Console.WriteLine("chrome.runtime.Message");
                //    };
            }
            #endregion





            // 0:168ms chrome.runtime.Connect
            // https://developer.chrome.com/extensions/tabs#method-sendMessage
            // chrome extension wont call here?
            //Native.window.onmessage +=
            //    e =>
            //    {
            //        Console.WriteLine(
            //            "onmessage: " +
            //            new { e.data }
            //        );




            //        e.postMessage("ok");

            //    };
        }

    }
}
