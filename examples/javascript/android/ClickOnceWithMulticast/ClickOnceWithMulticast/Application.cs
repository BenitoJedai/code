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
using ClickOnceWithMulticast.Design;
using ClickOnceWithMulticast.HTML.Pages;
using ScriptCoreLib.Ultra.WebService;

namespace ClickOnceWithMulticast
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();


        // - Unable to install this application because an application with the same identity is already installed. To install this application, either modify the manifest version for this application or uninstall the preexisting application.
        ClickOnceWithMulticast.Assets.Publish ref0;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            @"Hello world".ToDocumentTitle();


            new ClickOnceWithMulticast.Library.Recent().Show();
        }

    }

    public static class DownloadSDKFunction
    {
        public static void DownloadSDK(WebServiceHandler h)
        {
            const string _download = "/download/";
            const string a = @"assets/PromotionWebApplicationAssets";

            var path = h.Context.Request.Path;

            if (path == "/crx")
            {
                // https://code.google.com/p/chromium/issues/detail?id=128748

                h.Context.Response.Redirect("/download/foo.crx");
                h.CompleteRequest();
                return;
            }

            if (path == "/download")
            {
                h.Context.Response.Redirect("/download/ClickOnceWithMulticastClient.application");
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
            var p = new ClickOnceWithMulticast.Assets.Publish();

            foreach (string key in p.Keys)
            {
                Console.WriteLine(new { key });
            }

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

        private static void DownloadSDKFile(WebServiceHandler h, string f)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201306/20130605-lan-clickonce

            Console.WriteLine("download: " + f);

            if (f.EndsWith(".application"))
            {
                //                #14 GET /download/LANClickOnceClient.application HTTP/1.1 error:
                //#14 java.lang.RuntimeException: /assets/LANClickOnce/LANClickOnceClient.application: open failed: ENOENT (No such file or directory)

                //if (f.StartsWith("/"))
                //    f = f.Substring(1);

                // System.TypeLoadException: Method 'addView' in type 'InternalPopupWebView.XWindow' from assembly 'jsc.meta, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.
                // Data at the root level is invalid. Line 1, position 1.


                var bytes_application = System.IO.File.ReadAllText(f);

                var HostUri = new
                {
                    Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                    Port = h.Context.Request.Headers["Host"].SkipUntilIfAny(":")
                };

                var x = bytes_application.Replace(
                    "127.0.0.1:8181",
                    HostUri.Host + ":" + HostUri.Port
                );
                Console.WriteLine(x);
                h.Context.Response.Write(x);
                h.CompleteRequest();
                return;
            }


            var bytes = System.IO.File.ReadAllBytes(f);

            //var r = new MemoryStream(bytes);
            //var s = new SmartStreamReader(r);

            //var ascii = "127.0.0.1:8080";
            //var boundary = new MemoryStream();

            //foreach (byte item in Encoding.ASCII.GetBytes(ascii))
            //{
            //    boundary.WriteByte(item);
            //    boundary.WriteByte(0);
            //}

            //var x = s.ReadToBoundary(boundary.ToArray());


            h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            h.CompleteRequest();
        }
    }

}
