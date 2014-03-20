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
using com.abstractatech.scholar.Design;
using com.abstractatech.scholar.HTML.Pages;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System.Diagnostics;
using ScriptCoreLib.Ultra.WebService;

namespace com.abstractatech.scholar
{
    public static class XXX
    {
        public static void MoveNodeToFirst(this INode e)
        {
            var p = e.parentNode;
            ((IHTMLElement)e).Orphanize();

            p.insertBefore(
                e, p.firstChild);

        }


        public static object scrollTo_sync = new object();

        public static void scrollTo(this IWindow w, int x, int y, TimeSpan time)
        {
            var sync = new object();
            scrollTo_sync = sync;

            var s = new Stopwatch();

            s.Start();

            var start = new { Native.Document.body.scrollLeft, Native.Document.body.scrollTop };
            var check = start;


            Action loop = null;

            loop = delegate
            {
                if (sync != scrollTo_sync)
                {
                    return;
                }

                // scrollTo { check = { scrollLeft = 106, scrollTop = 0 }, current = { scrollLeft = 0, scrollTop = 0 } }


                var a = 1.0;

                if (s.ElapsedMilliseconds < time.TotalMilliseconds)
                {
                    a = s.ElapsedMilliseconds / time.TotalMilliseconds;

                    Native.window.requestAnimationFrame += loop;

                }

                var scrollLeft = (int)Math.Ceiling(start.scrollLeft + (double)(x - start.scrollLeft) * a);

                // this was expen
                var scrollTop = (int)Math.Ceiling(start.scrollTop + (double)(y - start.scrollTop) * a);

                //Console.WriteLine(new { a, x, xx = scrollLeft, s.Elapsed });

                w.scrollTo(scrollLeft, scrollTop);

                // scrollTo { check = { scrollLeft = 0, scrollTop = 0 }, current = { scrollLeft = 77, scrollTop = 0 } }



            };

            Native.window.requestAnimationFrame += loop;
        }
    }

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        com.abstractatech.scholar.Assets.Publish ref0;

        public readonly Abstractatech.JavaScript.FileStorage.IApplicationWebServiceX service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new ApplicationContent(page, service);
        }
    }

    public class ApplicationContent
    {

        public ApplicationContent(
            IApp page,
            Abstractatech.JavaScript.FileStorage.IApplicationWebServiceX service,
            bool DisableBackground = false

            )
        {


            FormStyler.AtFormCreated = FormStylerLikeFloat.LikeFloat;

            if (!DisableBackground)
            {
                #region  I want animated background!

                WebGLClouds.Application.Loaded +=
                    a =>
                    {
                        Native.Document.body.parentNode.insertBefore(
                             a.container.Orphanize(),
                              Native.Document.body
                        );
                        a.container.style.position = IStyle.PositionEnum.@fixed;

                    };


                new WebGLClouds.Application();
                #endregion
            }

            //var minsize = new IHTMLDiv().AttachToDocument();

            //minsize.style.SetSize(4000, 2000);




            var f = new Form
            {
                Text = "My Files",
                StartPosition = FormStartPosition.Manual,
                SizeGripStyle = SizeGripStyle.Hide
            };

            #region w
            var ff = new Form
            {
                StartPosition = FormStartPosition.Manual,
                SizeGripStyle = SizeGripStyle.Hide

            };

            var w = new WebBrowser
            {
                Dock = DockStyle.Fill
            }.AttachTo(ff);
            w.GetHTMLTarget().name = "view";

            w.Navigating +=
                delegate
                {
                    ff.Text = "Navigating";


                    if (Native.window.Width < 1024)
                        // docked?
                        if (ff.GetHTMLTarget().parentNode != null)
                            Native.window.scrollTo(ff.Left - 8, ff.Top - 8, TimeSpan.FromMilliseconds(300));

                };



            w.Navigated +=
                delegate
                {
                    if (w.Url.ToString() == "about:blank")
                    {

                        Native.window.scrollTo(0, 0, TimeSpan.FromMilliseconds(200));

                        ff.Text = "...";

                        "Web Files".ToDocumentTitle();

                        return;
                    }

                    //ff.Text = w.DocumentTitle;
                    ff.Text = Native.window.unescape(
                        w.Url.ToString().SkipUntilLastIfAny("/").TakeUntilLastIfAny(".")
                        );

                    ff.Text.ToDocumentTitle();


                };

            ff.FormClosing +=
                (sender, e) =>
                {
                    Console.WriteLine(new { e.CloseReason });

                    if (e.CloseReason == CloseReason.UserClosing)
                    {
                        e.Cancel = true;

                        w.Navigate("about:blank");
                    }

                };
            #endregion



            var content = f.GetHTMLTargetContainer();




            var hh = new HorizontalSplit
            {
                Minimum = 0.05,
                Maximum = 0.95,
                Value = 0.4,
            };

            hh.Container.AttachToDocument();
            hh.Container.style.position = IStyle.PositionEnum.absolute;
            hh.Container.style.left = "0px";
            hh.Container.style.top = "0px";
            hh.Container.style.right = "0px";
            hh.Container.style.bottom = "0px";

            hh.Split.Splitter.style.backgroundColor = "rgba(0,0,0,0.0)";


            #region AtResize
            Action AtResize = delegate
           {
               Native.Document.getElementById("feedlyMiniIcon").Orphanize();

               Native.Document.body.style.minWidth = "";

               if (ff.GetHTMLTarget().parentNode == null)
               {
                   Native.window.scrollTo(0, 0);
                   f.MoveTo(8, 8).SizeTo(Native.window.Width - 16, Native.window.Height - 16);

                   return;
               }

               if (f.GetHTMLTarget().parentNode == null)
               {
                   Native.window.scrollTo(0, 0);
                   ff.MoveTo(8, 8).SizeTo(Native.window.Width - 16, Native.window.Height - 16);

                   return;
               }

               if (Native.window.Width < 1024)
               {
                   Native.Document.body.style.minWidth = (Native.window.Width * 2) + "px";


                   f.MoveTo(8, 8).SizeTo(Native.window.Width - 16, Native.window.Height - 16);

                   ff.MoveTo(Native.window.Width + 8, 8).SizeTo(Native.window.Width - 16, Native.window.Height - 16);

                   // already scrolled...
                   if (w.Url.ToString() != "about:blank")
                       // docked?
                       if (ff.GetHTMLTarget().parentNode != null)
                           Native.window.scrollTo(ff.Left - 8, ff.Top - 8);

                   return;
               }




               f.MoveTo(16, 64).SizeTo(hh.LeftContainer.clientWidth - 32, Native.window.Height - 128);


               ff.MoveTo(
                   Native.window.Width - hh.RightContainer.clientWidth + 16

                   , 64).SizeTo(hh.RightContainer.clientWidth - 32, Native.window.Height - 128);

               //Console.WriteLine("LeftContainer " + new { hh.LeftContainer.clientWidth });
               //Console.WriteLine("RightContainer " + new { hh.RightContainer.clientWidth });
           };

            hh.ValueChanged +=
          delegate
          {
              AtResize();
          };

            Native.window.onresize +=
             delegate
             {
                 AtResize();
             };

            Native.window.requestAnimationFrame +=
        delegate
        {
            AtResize();
        };
            #endregion


            //hh.Split.LeftScrollable = new IHTMLDiv { className = "SidebarForButtons" };


            ff.Show();
            f.Show();

            f.PopupInsteadOfClosing(SpecialNoMovement: true, NotifyDocked: AtResize);
            ff.PopupInsteadOfClosing(SpecialNoMovement: true, HandleFormClosing: false, NotifyDocked: AtResize);


            var layout = new Abstractatech.JavaScript.FileStorage.HTML.Pages.App();

            layout.Container.AttachTo(content);

            Abstractatech.JavaScript.FileStorage.ApplicationContent.Target = "view";


            new Abstractatech.JavaScript.FileStorage.ApplicationContent(
                layout,
                service
            );







            "Web Files".ToDocumentTitle();
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

            var p = new com.abstractatech.scholar.Assets.Publish();
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
