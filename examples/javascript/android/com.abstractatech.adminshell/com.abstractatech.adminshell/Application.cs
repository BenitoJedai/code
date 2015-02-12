using Abstractatech.ConsoleFormPackage.Library;
using Abstractatech.JavaScript.FormAsPopup;
using com.abstractatech.adminshell.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.Experimental;


namespace com.abstractatech.adminshell
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
//    {



    //{ SourceMethod = Void<InitializeSidebarBehaviour> b__19(ScriptCoreLib.JavaScript.Runtime.Timer) }
    //    0x039616fd. [->0]
    //    [0] -0 +0 IL_0173: br.s[_] IL_0175
    //  |
    //script: error JSC1000: logic failure, TargetFlow.Branch: [0x0173]
    //    br.s       +0 -0
    //script: error JSC1000: error at CSSMinimizeFormToSidebar.ApplicationExtension+<>c__DisplayClass7.<InitializeSidebarBehaviour>b__19,


    HTML.Images.FromAssets.Preview ref0;

#if FEATURE_CLRINSTALL
        com.abstractatech.adminshell.Assets.Publish publish0;
#endif

    // type is loaded before virtual console is loaded
    FormAsPopupExtensionsForConsoleFormPackageMediator ref_allow_webview_to_talk;


    static void init(string InternalScriptApplicationSource)
    {
        (Native.window as dynamic).InternalScriptApplicationSource = InternalScriptApplicationSource;

    }


    public readonly ApplicationControl control = new ApplicationControl();


    /// <summary>
    /// This is a javascript application.
    /// </summary>
    /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
    public Application(IApp page)
    {
        // ask for credentials for new ui

        //new ConsoleForm().InitializeConsoleFormWriter().Show();

        control.nfc.onnfc +=
            nfcid =>
            {
                page.username.value = nfcid;
            };

        #region go
        Action<HistoryScope<InternalScriptApplicationSource>> go =
              //async
              scope =>
              {
                  var source = scope.state;


                  new App.FromDocument().LoginButton.Orphanize();

                  Console.WriteLine("eval a...");
                  var restore = source.eval();
                  Console.WriteLine("eval a... done");

                      //await scope;

                      //Native.window.alert("go back?");

                  };
        #endregion


        page.go.WhenClicked(
            async button =>
            //async delegate
            {
                Console.WriteLine("click!");

                    // stop polling
                    control.nfc.service = null;

                button.disabled = true;
                button.style.Opacity = 0.5;

                Console.WriteLine("loading a...");
                var source = await typeof(a);
                Console.WriteLine("loading a... done");

                Native.window.history.pushState(source, go);

                button.style.Opacity = 1;
                button.disabled = false;


            }
         );

        "Remote Web Shell".ToDocumentTitle();

    }


    //[Obsolete("jsc should rewrite nested secondary apps, by referencing the primary, to reduce any duplicate code, or both")]
    public sealed class a
    {


        public readonly ApplicationWebService service = new ApplicationWebService();

        static a()
        {
            // this should not be visible for the default app
            Console.WriteLine("typeof(a) is now available");
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


            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;

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

#if FEATURE_CLRINSTALL
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
#endif




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
