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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using com.abstractatech.appmanager.Design;
using com.abstractatech.appmanager.HTML.Pages;
using com.abstractatech.appmanager.windows;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
//using Abstractatech.JavaScript.FormAsPopup;

namespace com.abstractatech.appmanager
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        com.abstractatech.appmanager.Assets.Publish ref0;

        Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensionsForConsoleFormPackageMediator ref_allow_webview_to_talk;


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IBeforeLogin page)
        {
            "My Appz".ToDocumentTitle();



            new IHTMLBase
            {
                href = "http://"
                    + page.username.value
                    + ":"
                    + page.password.value
                    + "@"
                    + Native.document.location.host

            }.AttachToDocument();


            var s = new IHTMLScript
            {
                src = "/a"
            };


            // http://stackoverflow.com/questions/538745/how-to-tell-if-a-script-tag-failed-to-load
            s.onload +=
                delegate
                {
                    page.LoginButton.Orphanize();
                };

            page.LoginButton.onclick +=
                delegate
                {
                    page.LoginButton.style.Opacity = 0.5;


                    Console.WriteLine("will load secondary application...");
                    s.AttachToDocument();
                };



            #region LaunchMyAppz
            var about = new Cookie("about");

            if (!about.BooleanValue)
            {
                var a = new com.abstractatech.appmanager.about.HTML.Pages.App();

                a.Container.AttachToDocument();

                a.LaunchMyAppz.onclick +=
                    delegate
                    {
                        about.BooleanValue = true;

                        a.Container.Orphanize();
                    };


                return;
            }
            #endregion


        }

        public sealed class a
        {
            public readonly ApplicationWebService service = new ApplicationWebService();


            public a(IBeforeLogin ee)
            {
                //FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;
                FormStyler.AtFormCreated = FormStylerLikeFloat.LikeFloat;

                var ff = new Form { FormBorderStyle = FormBorderStyle.None };


                var ScrollArea = new App().ScrollArea.AttachTo(ff.GetHTMLTargetContainer());



                //ScrollArea.style.backgroundColor = "#185D7B";
                //ScrollArea.style.backgroundColor = "#185D7B";
                ScrollArea.style.backgroundColor = "#105070";

                var SidebarWidth = 172;

                ff.MoveTo(SidebarWidth, 0);

                Action AtResize = delegate
                {

                    ff.SizeTo(Native.window.Width - SidebarWidth, Native.window.Height);
                };

                Native.window.onresize +=
                    delegate
                    {
                        AtResize();

                    };

                AtResize();
                var iii = global::CSSMinimizeFormToSidebar.ApplicationExtension.InitializeSidebarBehaviour(
                  ff,

                  // should be handle close instead!
                  HandleClosed: true,
                  HandleDragToLeft: false
                );

                iii.SidebarText.className = "AppPreviewText";
                //iii.SidebarText.innerText = "My Appz";
                //iii.SidebarText.innerText = "Synchronizing...";
                var finish = iii.SidebarText.ToASCIIStyledLoadAnimation("My Appz");


                //Native.Document.body.style.backgroundColor = "#105070";
                Native.document.body.style.backgroundColor = "#185D7B";

                Native.window.onresize +=
                    delegate
                    {
                        // lets not centerize
                        //ff.Show();
                    };


                //var page = new App();

                //page.ScrollArea.AttachToDocument();

                var count = 0;

                var yield_BringToFront = false;

                var icon_throttle = 0;

                #region yield
                yield_ACTION_MAIN yield = (
                            packageName,
                            name,
                            __IsCoreAndroidWebServiceActivity,
                            label
                        ) =>
                {

                    if (string.IsNullOrEmpty(label))
                        label = packageName;

                    var IsCoreAndroidWebServiceActivity = System.Convert.ToBoolean(__IsCoreAndroidWebServiceActivity);

                    count++;

                    var a = new AppPreview();

                    #region icon
                    if (packageName != "foo")
                    {
                        // see also: X:\jsc.svn\examples\javascript\ImageCachedIntoLocalStorageExperiment\ImageCachedIntoLocalStorageExperiment\Application.cs

                        // extension to the system
                        // data.icon[packageName].With

                        Action loadicon = delegate
                        {
                            new ScriptCoreLib.JavaScript.Runtime.Timer(
                                delegate
                                {
                                    new IHTMLImage { src = "/icon/" + packageName }.InvokeOnComplete(
                                        i =>
                                        {
                                            var dataURL = i.toDataURL();

                                            // Uncaught QuotaExceededError: An attempt was made to add something to storage that exceeded the quota. 
                                            // http://stackoverflow.com/questions/6276282/how-can-i-request-an-increase-to-the-html5-localstorage-size-on-ipad-like-the-f

                                            //message: "An attempt was made to add something to storage that exceeded the quota."
                                            //name: "QuotaExceededError"



                                            a.Icon.src = dataURL;

                                            try
                                            {
                                                Native.window.localStorage[new { packageName }] = dataURL;
                                            }
                                            catch (Exception err)
                                            {
                                                Console.WriteLine(new { packageName, error = new { err.Message } });
                                            }
                                        }
                                    );
                                }
                            ).StartTimeout(icon_throttle);
                        };

                        // VirtualDictionary?
                        Native.window.localStorage[new { packageName }].With(
                            dataURL =>
                            {
                                Console.WriteLine("load from localstorage: " + new { packageName });

                                a.Icon.src = dataURL;

                                loadicon = delegate { };
                            }
                        );

                        loadicon();

                        icon_throttle += 900;

                    }
                    #endregion


                    //a.Icon.src = "/icon/" + packageName;

                    //a.Icon.src = "data:image/png;base64," + icon_base64;
                    a.Label.innerText = label;

                    if (yield_BringToFront)
                    {
                        Console.WriteLine("yield_BringToFront " + new { packageName });

                        ScrollArea.insertBefore(a.Container, ScrollArea.firstChild);
                    }
                    else
                    {
                        a.Container.AttachTo(ScrollArea);
                    }


                    //(page.gauge_layer1.style as dynamic).webkitTransition = "-webkit-transform 0.7s ease-in";

                    //-webkit-transition: filter 0.3s linear;
                    __Form __ff = ff;
                    (__ff.HTMLTarget.style as dynamic).webkitTransition = "-webkit-filter 0.7s ease-in";


                    #region onclick
                    Action<bool> onclick =
                        CanAutoLaunch =>
                        {


                            Console.WriteLine(new { label });
                            var content = new ApplicationControl();


                            var f = new Form { Text = label, ShowIcon = false };
                            f.ClientSize = content.Size;

                            f.FormClosed +=
                                delegate
                                {
                                    (__ff.HTMLTarget.style as dynamic).webkitFilter = "blur(0px)";
                                };

                            f.Shown += delegate
                            {
                                (__ff.HTMLTarget.style as dynamic).webkitFilter = "blur(4px)";
                            };

                            f.Show();


                            Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(
                                f,
                                HandleFormClosing: false,
                                SpecialCloseOnLeft: delegate
                                {
                                    Console.WriteLine("SpecialCloseOnLeft");

                                    service.Launch(
                                        packageName,
                                        name,
                                        DisableCallbackToken: "true"
                                    );

                                    //f.Close();
                                }
                            );

                            if (CanAutoLaunch && IsCoreAndroidWebServiceActivity)
                            {

                                f.Opacity = 0.5;

                                var w = new WebBrowser();
                                w.Dock = DockStyle.Fill;

                                f.Controls.Add(w);

                                service.Launch(
                                    packageName,
                                    name,

                                    yield_port:
                                    // we should remember the port
                                    // to launch it offline via AppCache
                                        port =>
                                        {
                                            Console.WriteLine(new { port });

                                            // close to left sidebar!
                                            // broken?
                                            //ff.Close();


                                            f.Opacity = 1.0;

                                            var uri = Native.document.location.protocol
                                                + "//"
                                                + Native.document.location.host.TakeUntilIfAny(":")
                                                + ":" + port;

                                            //w.Navigated +=
                                            //    delegate
                                            //    {
                                            //        Console.WriteLine("WebBrowser Navigated " + new { packageName, uri, Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame });

                                            //        // FormAsPopupExtensionsForConsoleFormPackageMediator

                                            //        if (Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame)
                                            //        {

                                            //        }
                                            //    };

                                            w.Navigate(uri);

                                            f.ClientSize = content.Size;
                                        }
                                );
                            }
                            else
                            {
                                f.Controls.Add(content);
                                content.Dock = DockStyle.Fill;




                                content.Label.Text = label;
                                content.Package.Text = packageName;

                                a.Icon.cloneNode(true).AttachTo(

                                    ScriptCoreLib.JavaScript.Windows.Forms.Extensions.GetHTMLTargetContainer(content.Icon)

                                );

                                content.Uninstall.Click +=
                                    delegate
                                    {
                                        service.Remove(
                                           packageName,
                                           name
                                        );

                                        //f.Hide();

                                        // http://www.w3schools.com/cssref/pr_text_text-decoration.asp
                                        a.Label.style.textDecoration = "line-through";

                                        f.Close();
                                    };

                                content.Launch.Click +=
                                    delegate
                                    {
                                        // level 1
                                        // run on android

                                        // level 2
                                        // run as float

                                        // level 3 
                                        // run here as iframe

                                        // level 4
                                        // run here as js import

                                        service.Launch(
                                            packageName,
                                            name,

                                            yield_port:
                                                port =>
                                                {
                                                    Console.WriteLine(new { port });
                                                    // close to left sidebar!
                                                    ff.Close();


                                                    var uri = Native.Document.location.protocol
                                                        + "//"
                                                        + Native.Document.location.host.TakeUntilIfAny(":")
                                                        + ":" + port;

                                                    var w = new WebBrowser();

                                                    f.Controls.Add(w);
                                                    w.Dock = DockStyle.Fill;
                                                    w.Navigate(uri);

                                                    f.ClientSize = content.Size;
                                                }
                                        );

                                    };
                            }
                        };
                    #endregion

                    a.Clickable.oncontextmenu +=
                       e =>
                       {
                           e.preventDefault();

                           onclick(false);
                       };

                    a.Clickable.onclick +=
                        e =>
                        {
                            e.preventDefault();

                            onclick(true);
                        };

                };
                #endregion


                #region more
                var skip = 0;
                var take = 32;



                {


                    Action done = delegate { };


                    Action MoveNext = delegate
                    {
                        icon_throttle = 0;

                        //more.disabled = true;
                        //more.innerText = "checking for more...";

                        Console.WriteLine("MoveNext: " + new { skip, take });

                        service.queryIntentActivities(
                            yield,
                            skip: "" + skip,
                            take: "" + take,
                            yield_done: done

                        );


                        //service.File_list("",
                        //    ydirectory: ydirectory,
                        //    yfile: yfile,
                        //    sskip: skip.ToString(),
                        //    stake: take.ToString(),
                        //    done: done
                        //);

                        skip += take;

                    };

                    done = delegate
                    {
                        //more.innerText = getmore;
                        //more.disabled = false;

                        if (count == skip)
                        {
                            //Native.Document.body.With(
                            //     body =>
                            //     {
                            //         if (more.disabled)
                            //             return;

                            //         if (body.scrollHeight - 1 <= Native.Window.Height + body.scrollTop)
                            //         {
                            MoveNext();
                            //          }

                            //      }
                            //);
                        }
                        else
                        {
                            finish();
                            yield_BringToFront = true;

                            service.oninstall(yield);

                        }
                    };



                    MoveNext();

                    //more.onclick += delegate
                    //{
                    //    MoveNext();
                    //};



                    //Native.Window.onscroll +=
                    //      e =>
                    //      {

                    //          Native.Document.body.With(
                    //              body =>
                    //              {
                    //                  if (more.disabled)
                    //                      return;

                    //                  if (body.scrollHeight - 1 <= Native.Window.Height + body.scrollTop)
                    //                  {
                    //                      MoveNext();
                    //                  }

                    //              }
                    //        );

                    //      };
                }
                //);
                #endregion


            }
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

            var p = new com.abstractatech.appmanager.Assets.Publish();
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
