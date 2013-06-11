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
using RSSGhost.Design;
using RSSGhost.HTML.Pages;
using ScriptCoreLib.Ultra.WebService;

namespace RSSGhost
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        RSSGhost.Assets.Publish ref0;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //Native.Document.body.dro
            Native.Document.body.ondragover +=
                e =>
                {
                    e.stopPropagation();
                    e.preventDefault();

                    e.dataTransfer.dropEffect = "copy";

                    page.header.style.color = "blue";

                };




            Native.Document.body.ondragleave +=
                delegate
                {
                    page.header.style.color = "";
                };

            //ondrop
            //text/plain
            //text/uri-list
            //{ uri = http://www.fromthetrenchesworldreport.com/ }
            //{ summary = N.J. Bill Seeks Crackdown On Distracted Driving By Forcing Drivers To Hand Over Phones }
            //{ summary = Second Colorado Senator recall Election underway, Group rallies against Pro-gun control Lawmakers }
            //{ summary = Chilling Flashback: ‘Transformers’ actor tells Jay Leno in 2008 how an FBI consultant showed him proof of gov’t phone spying years ago }
            //{ summary = Max Velocity Tactical Training Services }
            //{ summary = A Call To The Invisible Army Of The Restoration Of Liberty }
            //{ summary = Whatâ€™s in the Rest of the Top-Secret NSA PowerPoint Deck? }
            //{ summary = Four Popular Tourniquets }
            //{ summary = The Word From the Trenches – June 10, 2013 }
            //{ summary = Whole Foods Under Fire for English-Only Policy }
            //{ summary = Man Charged With DUI With No Alcohol In His System }
            //{ summary = When the bond markets crash then gold and silver prices will go ballistic }
            //{ summary = Supreme Court Ends Torture Lawsuit Against Donald Rumsfeld }
            //{ summary = U.S. wants resignation of “anti-Israel” United Nations representative }
            //{ summary = Military told not to read Obama-Scandal news }
            //{ summary = State Department memo reveals possible cover-ups, halted investigations }
            //{ summary = Record 23,116,441 households on food stampsâ€¦ 60 percent of Virginia families are single parent }
            //{ summary = Boulder woman disturbed by police policy to enter unsecured residences }
            //{ summary = Mass Surveillance in America }
            //{ summary = Bilderberg PR diversions concealed massive police build-up in advance of event }
            //{ summary = Anti-Hezbollah Protester Killed In Lebanon }

            Native.Document.body.ondrop +=
                e =>
                {
                    page.header.style.color = "";

                    e.stopPropagation();
                    e.preventDefault();

                    page.Output.appendChild(
                              new IHTMLDiv { innerText = "ondrop" }
                          );


                    e.dataTransfer.types.WithEach(
                        type =>
                        {
                            page.Output.appendChild(
                                new IHTMLDiv { innerText = type }
                            );

                            if (type == "text/uri-list")
                            {
                                var uri = e.dataTransfer.getData(type);
                                new IHTMLDiv { innerText = new { uri }.ToString() }.AttachTo(page.Output);

                                service.Add(uri,
                                    done:
                                    delegate
                                    {
                                        service.GetSummary(
                                             summary =>
                                             {
                                                 new IHTMLDiv { innerText = new { summary }.ToString() }.AttachTo(page.Output);

                                             }
                                         );
                                    }
                                );

                            }
                        }
                    );
                };




            service.GetSummary(
                summary =>
                {
                    new IHTMLDiv { innerText = new { summary }.ToString() }.AttachTo(page.Output);

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

            var p = new RSSGhost.Assets.Publish();
            //var p2 = new WithClickOnceLANLauncher.Assets.Publish2();

            if (path == "/download")
            {
                //I/System.Console( 3016):        at ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__Dictionary_2___KeyCollection.System_Collections_ICollection_get_Count(__Dictionary_2___KeyCollection.java:83)
                //I/System.Console( 3016):        at ScriptCoreLib.Extensions.IEnumerableExtensions.AsEnumerable(IEnumerableExtensions.java:25)
                //I/System.Console( 3016):        at WithClickOnceLANLauncher.DownloadSDKFunction.DownloadSDK(DownloadSDKFunction.java:73)
                //I/System.Console( 3016):        at WithClickOnceLANLauncher.ApplicationWebService.DownloadSDK(ApplicationWebService.java:44)

                var key = p.Keys.AsEnumerable().Select(k => p[(string)k]).First(k => k.EndsWith(".application")).SkipUntilLastIfAny("/");

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
