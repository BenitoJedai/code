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
                // jsc, add chrome nuget
                chrome.runtime.Installed += delegate
                {
                    // our API does not have a Show
                    new chrome.Notification
                    {
                        Message = "Extension Installed!"
                    };
                };


                var IgnoreSecondaryUpdatesFor = new List<TabIdInteger>();

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


                        Console.WriteLine("before insertCSS");

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


                        p.onMessage.addListener(
                            new Action<object>(
                                data =>
                                {
                                    //Console.WriteLine("extension: onMessage " + new { data });
                                    new Notification
                                    {
                                        Message = "extension onMessage: " + new { tab.id, data }
                                    };
                                }
                            )
                        );


                        //p.postMessage("hello executeScript");


                        // lets enable workers within tab
                        p.postMessage(new { code });
                    };

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
                () => (

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
                button.style.zIndex = 1000;


                button.onclick +=
                    async e =>
                    {
                        e.Element.disabled = true;
                        Native.body.style.borderTop = "1em solid blue";


                        var scopedata1 = "enter";
                        var scopedata2 = "exit";

                        var x = await Task.Run(
                            async delegate
                            {
                                var s = Stopwatch.StartNew();

                                // does it show up?
                                await new xxAvatar().InsertAsync(
                                    new xxAvatarRow
                                    {
                                        Tag = "tab worker! " + scopedata1 + new { s.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId },
                                    }
                                );


                                Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });
                                await Task.Delay(999);




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

                        Notify(x);

                        Native.body.style.borderTop = "1em solid pink";
                        e.Element.disabled = false;
                    };

            }
            );
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

                            Console.WriteLine("chrome.runtime.Connect " + new { Native.document.domain });

                            //0:123ms chrome.runtime.Connect
                            //0:126ms webview: onMessage { data = hello executeScript }

                            e.onMessage.addListener(
                                new Action<dynamic>(
                                    data =>
                                    {
                                        string code = data.code;


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
                                            e.postMessage(x);
                                        };
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
