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
using com.abstractatech.adminshell.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using System.Drawing;
using Abstractatech.JavaScript.FormAsPopup;
using ScriptCoreLib.Ultra.WebService;
using Abstractatech.ConsoleFormPackage.Library;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading;
using ScriptCoreLib.JavaScript.WebGL;
using System.IO;

namespace com.abstractatech.adminshell
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        HTML.Images.FromAssets.Preview ref0;
        com.abstractatech.adminshell.Assets.Publish publish0;

        // type is loaded before virtual console is loaded
        FormAsPopupExtensionsForConsoleFormPackageMediator ref_allow_webview_to_talk;




        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // ask for credentials for new ui

            //new ConsoleForm().InitializeConsoleFormWriter().Show();


            // or just change the base?

            //new IHTMLBase
            //{
            //    href = "http://"
            //        + page.username.value
            //        + ":"
            //        + page.password.value
            //        + "@"
            //        + Native.document.location.host

            //}.AttachToDocument();


            //var s = new IHTMLScript
            //{
            //    src = "/a"
            //};

            //// http://stackoverflow.com/questions/538745/how-to-tell-if-a-script-tag-failed-to-load
            //s.onload +=
            //    delegate
            //    {
            //    };

            //    at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.<.ctor>b__47(ILInstruction )

            //var s = new IHTMLScript { src = "/a" };

            page.go.With(
                async go =>
                //async delegate
                {
                    Console.WriteLine("click me!");

                    await go;

                    AsyncHistoryExperiment.XState.Create(
                        new { o = 0.5 },
                        async state =>
                        {
                            // jsc cannot share scope across time yet
                            var xpage = new App.FromDocument();


                            xpage.LoginButton.style.Opacity = 0.5;
                            //s.AttachToDocument();
                            Console.WriteLine("loading secondary app");



                            await typeof(a);
                            //await new IHTMLScript { src = "/a" };


                            Console.WriteLine("loading secondary app done");


                            //await new IHTMLScript { src = "/a" };


                            // jsc, you should remember the elements, once removed we may still want to talk
                            // to them
                            var login = xpage.LoginButton.Orphanize();


                            await state;

                            Console.WriteLine("lets clear the body and then reattach the buttons");
                            Native.window.alert("lets clear the body and then reattach the buttons");

                            Native.document.body.Clear();

                            //Native.document.body.style.backgroundColor = JSColor.Red;

                            //var old= Native.document.body.childNodes.Select(k => k.Orphanize()).ToArray();
                            login.style.Opacity = 1;
                            login.AttachToDocument();
                        }
                    );
                }
             );

            "Remote Web Shell".ToDocumentTitle();

        }


        [Obsolete("jsc should rewrite nested secondary apps, by referencing the primary, to reduce any duplicate code, or both")]
        public sealed class a
        {


            public readonly ApplicationWebService service = new ApplicationWebService();

            static a()
            {
                Console.WriteLine("secondary app loaded");
            }

            public a(IApp e)
            {
                //yield(new { hello = "secondary app loaded and running!" }.ToString());

                var c = new ShellWithPing.Library.ConsoleWindow
                {
                    Text = "Remote Web Shell",
                    //Text = "Remote Web Shell (Logged in as " + new Cookie("foo").Value + ")",
                    Color = Color.Red,
                    BackColor = Color.Black
                };

                c.AppendLine(
@" *** WARNING *** be careful!
example:
 am start -a android.intent.action.CALL tel:254007
");

                // if we will popup, wil we have a frame?
                c.AppendLine(new { FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame }.ToString());



                c.Show();


                global::CSSMinimizeFormToSidebar.ApplicationExtension.InitializeSidebarBehaviour(
                    c, HandleClosed: false
                );


                if (Native.window.parent != Native.window.self)
                {
                    Native.document.body.style.backgroundColor = JSColor.Transparent;
                }
                else
                {
                    Native.document.body.style.backgroundColor = "#185D7B";
                }

                c.PopupInsteadOfClosing();

                c.AtCommand += service.ShellAsync;
            }
        }
    }

    //interface IApplication
    //{ 

    //}

    static class X
    {
        public static TaskAwaiter<object> GetAwaiter(this Type __e)
        {
            Console.WriteLine(new { __e.Name });

            // http://stackoverflow.com/questions/9713058/sending-post-data-with-a-xmlhttprequest

            var y = new TaskCompletionSource<object>();

            //return 

            //InternalInitializeInlineWorker Report: { __IProgress_Report = { value = [object Object] } }
            // view-source:27346
            //{ Name = a, loaded = 4538818, total = 4538818 } view-source:27346

            // view-source:27346
            //loading secondary app in a moment... { responseType = arraybuffer, ManagedThreadId = 10 }
            // view-source:27346
            //loading secondary app in a moment... { Length = 4538818 } decrypting...
            // view-source:27346
            //loading secondary app in a moment... { Length = 2269409 } done!


            Task.Factory.StartNewWithProgress(
                new { __e.Name, loaded = default(long), total = default(long), source = default(string) },

                tuple =>
                {
                    var progress = tuple.Item1;
                    var scope = tuple.Item2;

                    // http://stackoverflow.com/questions/13870853/how-to-upload-files-in-web-workers-when-formdata-is-not-defined
                    // FormData is not defined
                    //var f = new FormData();

                    //f.append("Application", scope.Name);

                    var x = new IXMLHttpRequest();

                    x.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/view-source",
                        async: true,
                        name: "public",
                        pass: "key1555555"
                    );

                    // Uncaught InvalidStateError: An attempt was made to use an object that is not, or is no longer, usable.
                    x.setRequestHeader(
                        "X-Application", scope.Name
                    );

                    // what about progress?

                    x.responseType = "arraybuffer";

                    // http://stackoverflow.com/questions/10956574/why-might-xmlhttprequest-progressevent-lengthcomputable-be-false
                    x.onprogress +=
                        e =>
                        {
                            Console.WriteLine(new { e.loaded, e.total });

                            progress.Report(
                                new { scope.Name, e.loaded, e.total, scope.source }
                            );

                        };

                    // await x.bytes instead ?
                    x.InvokeOnComplete(
                        delegate
                        {


                            // can we use progress to do a lazy return

                            // these browsers no longer let you use the responseType attribute when performing synchronous requests. Attempting to do so throws an NS_ERROR_DOM_INVALID_ACCESS_ERR exception. This change has been proposed to the W3C for standardization.
                            // we could load encrypted binary blob here in background worker

                            // loading secondary app in a moment... { responseType = , ManagedThreadId = 10 }

                            // loading secondary app in a moment... { responseType = arraybuffer, ManagedThreadId = 10 }
                            Console.WriteLine("loading secondary app in a moment... " + new { x.responseType, Thread.CurrentThread.ManagedThreadId });

                            // https://developer.mozilla.org/en-US/docs/Web/API/XMLHttpRequest/Sending_and_Receiving_Binary_Data

                            //loading secondary app in a moment... { responseType = arraybuffer, ManagedThreadId = 10 }
                            // view-source:27267
                            //loading secondary app in a moment... { Length = 4515364 } done!


                            var response = (byte[])new Uint8ClampedArray((ArrayBuffer)x.response);


                            #region got it
                            Console.WriteLine("loading secondary app in a moment... " + new { response.Length } + " decrypting...");

                            //y.SetResult(new { x.responseText.Length }.ToString());
                            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.cs

                            var m = new MemoryStream();

                            var lo = default(byte);
                            var lo_set = false;

                            foreach (var item in response)
                            {
                                if (lo_set)
                                {
                                    lo_set = false;

                                    var hi = (byte)(item << 4);

                                    m.WriteByte(
                                        (byte)(lo | hi)
                                    );
                                }
                                else
                                {
                                    lo = item;
                                    lo_set = true;
                                }
                            }

                            // decrypted
                            var source = Encoding.UTF8.GetString(m.ToArray());


                            Console.WriteLine("loading secondary app in a moment... " + new { source.Length } + " done!");

                            //return new { response.Length, responseText = source };

                            #endregion


                        }
                    );

                    //x.overrideMimeType("application/octet-stream");
                    x.send();



                    // no changes yet
                    return scope;
                },

                x =>
                {
                    Console.WriteLine(
                        new { x.Name, x.loaded, x.total }
                    );
                }
            );


            return y.Task.GetAwaiter();


            //.ContinueWith(
            //    task =>
            //    {
            //        var x = task.Result;

            //        // should we analyze? IFunction
            //        Native.window.eval(
            //            //x.responseText
            //            x.responseText
            //        );


            //        //script: error JSC1000: Method: LockBits, Type: ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing.__Bitmap; emmiting failed : System.NullReferenceException: Object reference not set to an instance of an object.

            //        return new Action(delegate { Console.WriteLine("log out in progres..."); });
            //    }
            //    , scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //).GetAwaiter();

            //return y.Task.GetAwaiter();

            //Activator.CreateInstance();
            // can I have only the types I do not yet have?
            //return new IHTMLScript { src = "/" + e.Name }.ToTask().GetAwaiter();
        }
    }


    public static class DownloadSDKFunction
    {
        // - The digital signature of the object did not verify.

        public static void DownloadSDK(WebServiceHandler h)
        {
            const string _download = "/download/";

            var path = h.Context.Request.Path;

            if (path == "/crx")
            {
                // https://code.google.com/p/chromium/issues/detail?id=128748

                h.Context.Response.Redirect("/download/foo.crx");
                h.CompleteRequest();
                return;
            }

            var p = new com.abstractatech.adminshell.Assets.Publish();
            //var p2 = new WithClickOnceLANLauncher.Assets.Publish2();

            if (path == "/download")
            {
                //I/System.Console( 3016):        at ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__Dictionary_2___KeyCollection.System_Collections_ICollection_get_Count(__Dictionary_2___KeyCollection.java:83)
                //I/System.Console( 3016):        at ScriptCoreLib.Extensions.IEnumerableExtensions.AsEnumerable(IEnumerableExtensions.java:25)
                //I/System.Console( 3016):        at WithClickOnceLANLauncher.DownloadSDKFunction.DownloadSDK(DownloadSDKFunction.java:73)
                //I/System.Console( 3016):        at WithClickOnceLANLauncher.ApplicationWebService.DownloadSDK(ApplicationWebService.java:44)

                var key = p.Keys.ICollectionAsEnumerable().Select(k => p[(string)k]).First(k => k.EndsWith(".application")).SkipUntilLastIfAny("/");

                Console.WriteLine(
                    new
                    {
                        key
                    }
                );

                h.Context.Response.Redirect("/download/" + key);
                h.CompleteRequest();
                return;
            }



            //if (path == "/download/jsc-web-installer.exe")
            //{
            //    // http://msdn.microsoft.com/en-us/library/h4k032e1.aspx
            //    // is chrome happier if we rename it?
            //    path = "/download/setup.exe";
            //}

            if (path == "/download/")
            {
                var href = "http://www.jsc-solutions.net/download/jsc-web-installer.exe";

                var html = @"
                    <meta http-equiv='Refresh' target='_top' content='1;url=" + href + @"' />

                    
                    <center>
                    
                    <br />
                    <br />
                    <br />

<a href='" + href + @"'>Thank you for downloading JSC!</a>
                     
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

<div><i>Note that recent versions of <b>Google Chrome</b> may need additional time to verify.</i></div>                  
                                       
                                       </center>";

                h.Context.Response.ContentType = "text/html";

                var bytes = Encoding.UTF8.GetBytes(html);
                h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                h.CompleteRequest();
                return;
            }


            // we will compare the win32 relative paths here...
            var publish = path.SkipUntilOrEmpty("/download/").Replace("/", @"\");

            // 	- Exception reading manifest from http://192.168.1.100:24257/#/download/Application%20Files/WithClickOnceLANLauncherClient_1_0_0_2/WithClickOnceLANLauncherClient.exe.manifest: the manifest may not be valid or the file could not be opened.
            // did publish work and were it compiled into AssetsLibrary correctly?

            if (p.ContainsKey(publish))
            {
                var f = p[publish];


                var ext = "." + f.SkipUntilLastOrEmpty(".").ToLower();

                // http://en.wikipedia.org/wiki/Mime_type
                // http://msdn.microsoft.com/en-us/library/ms228998.aspx

                var ContentType = "application/octet-stream";

                if (ext == ".application")
                {
                    ContentType = "application/x-ms-application";
                }
                else if (ext == ".manifest")
                {
                    ContentType = "application/x-ms-manifest";
                }
                else if (ext == ".htm")
                {
                    ContentType = "text/html";
                }
                else if (ext == ".crx")
                {
                    // http://feedback.livereload.com/knowledgebase/articles/85889-chrome-extensions-apps-and-user-scripts-cannot
                    // http://stackoverflow.com/questions/12049366/re-enabling-extension-installs

                    ContentType = "application/x-chrome-extension";
                    // Resource interpreted as Document but transferred with MIME type application/x-chrome-extension: "http://192.168.1.106:16507/download/foo.crx".

                    //h.Context.Response.AddHeader(
                    //    "Content-Disposition", "attachment; filename=\"xfoo.crx\"");


                }


                h.Context.Response.ContentType = ContentType;




                DownloadSDKFile(h, f);


            }




            return;
        }

        private static void DownloadSDKFile(WebServiceHandler h, string fpath, string folder = "/download")
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201306/20130605-lan-clickonce

            Console.WriteLine("download: " + fpath);

            if (fpath.EndsWith(".application"))
            {
                var bytes_application = System.IO.File.ReadAllText(fpath);

                var HostUri = new
                {
                    Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                    Port = h.Context.Request.Headers["Host"].SkipUntilIfAny(":")
                };

                var x = bytes_application.Replace(
                    "127.0.0.1:8181",

                    // change path by adding a sub folder
                    HostUri.Host + ":" + HostUri.Port + folder
                );
                Console.WriteLine(x);
                h.Context.Response.Write(x);
                h.CompleteRequest();
                return;
            }


            var bytes = System.IO.File.ReadAllBytes(fpath);
            h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            h.CompleteRequest();
        }
    }
}
