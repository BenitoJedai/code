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
using com.abstractatech.dcimgalleryapp.Design;
using com.abstractatech.dcimgalleryapp.HTML.Pages;

namespace com.abstractatech.dcimgalleryapp
{
    using ystring = Action<string>;
    using ScriptCoreLib.JavaScript.Runtime;
    using DiagnosticsConsole;
    using ScriptCoreLib.Ultra.WebService;


    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        com.abstractatech.dcimgalleryapp.Assets.Publish ref0;

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            // does not work for android 2.2?
            //Action Toggle = ApplicationContent.BindKeyboardToDiagnosticsConsole();

            //page.Tilde.onclick +=
            //    delegate
            //    {
            //        Toggle();
            //    };

            Native.Document.title = "DCIM Gallery App";

            if (Native.window.parent != Native.window.self)
            {
                Native.document.body.style.backgroundColor = JSColor.Transparent;
            }

            // see also. http://en.wikipedia.org/wiki/Design_rule_for_Camera_File_system

            ystring ydirectory = path =>
            {

            };

            var container = new IHTMLCenter().AttachToDocument();

            #region AddThumbnailTo
            Action<string, IHTMLElement> AddThumbnailTo =
                (path, div) =>
                {
                    new IHTMLImage { }.AttachTo(div).With(
                             img =>
                             {
                                 // portrait mode only!

                                 div.style.color = JSColor.Red;
                                 img.src = "/thumb/" + path;

                                 #region onload +=
                                 img.InvokeOnComplete(
                                     delegate
                                     {
                                         div.style.color = JSColor.Green;

                                         IHTMLPre p = null;

                                         img.onclick += delegate
                                         {
                                             if (p == null)
                                             {
                                                 img.src = "/io/" + path;
                                                 img.style.width = "100%";
                                                 div.style.display = IStyle.DisplayEnum.block;

                                                 p = new IHTMLPre { }.AttachTo(div);
                                                 service.GetEXIF("/io/" + path,
                                                     x =>
                                                     {
                                                         p.innerText = x;
                                                     }
                                                 );


                                             }
                                             else
                                             {

                                                 p.Orphanize();
                                                 p = null;
                                                 img.src = "/thumb/" + path;
                                                 img.style.width = "";
                                             }

                                         };
                                     }
                                 );
                                 #endregion





                             }
                         );
                };
            #endregion

            ystring yfile = path =>
            {
                new IHTMLDiv { innerText = path }.With(
                    div =>
                    {
                        if (path.ToLower().EndsWith(".jpg"))
                        {
                            div.innerText = "";
                            div.AttachTo(container);
                            // hide path

                            //new IHTMLBreak().AttachTo(div);
                            div.style.display = IStyle.DisplayEnum.inline_block;


                            AddThumbnailTo(path, div);
                        }
                    }
                );
            };

            var skip = 0;
            var take = 30;

            page.icon.style.cursor = IStyle.CursorEnum.pointer;
            page.icon.onclick +=
                delegate
                {
                    service.TakePicture("",
                        path =>
                        {
                            Console.WriteLine(new { path });

                            new IHTMLDiv { innerText = path }.With(
                                div =>
                                {
                                    if (path.ToLower().EndsWith(".jpg"))
                                    {
                                        div.innerText = "";

                                        container.insertBefore(div, container.firstChild);

                                        //div.AttachTo(container);
                                        // hide path

                                        //new IHTMLBreak().AttachTo(div);
                                        div.style.display = IStyle.DisplayEnum.inline_block;


                                        AddThumbnailTo(path, div);
                                    }
                                }
                            );
                        }
                    );

                };


            new IHTMLButton { innerText = "more" }.AttachToDocument().With(
                more =>
                {
                    Action MoveNext = delegate
                    {
                        more.disabled = true;
                        more.innerText = "checking for more...";

                        ystring done = delegate
                        {
                            more.innerText = "more";
                            more.disabled = false;

                        };

                        service.File_list("",
                            ydirectory: ydirectory,
                            yfile: yfile,
                            sskip: skip.ToString(),
                            stake: take.ToString(),
                            done: done
                        );

                        skip += take;

                    };


                    MoveNext();

                    more.onclick += delegate
                    {
                        MoveNext();
                    };



                    Native.Window.onscroll +=
                          e =>
                          {

                              Native.Document.body.With(
                                  body =>
                                  {
                                      if (more.disabled)
                                          return;

                                      if (body.scrollHeight - 1 <= Native.Window.Height + body.scrollTop)
                                      {
                                          MoveNext();
                                      }

                                  }
                            );

                          };
                }
            );



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

            var p = new com.abstractatech.dcimgalleryapp.Assets.Publish();
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
