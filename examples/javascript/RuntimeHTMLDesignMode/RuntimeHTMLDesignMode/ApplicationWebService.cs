using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Xml.Linq;

namespace RuntimeHTMLDesignMode
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        public void Update(
            //string oldsource,
            XElement newbody,

            Action<XElement> y)
        {

            var modified = newbody.Attribute("data-modified").Value;

            // which is it. update the client. or update the source?


            var originalsource = XElement.Parse(RuntimeHTMLDesignMode.HTML.Pages.AppSource.Text);
            var path = originalsource.Attribute("data-source");

            var diskmodified = "" + File.GetLastWriteTime(path.Value).Ticks;

            if (diskmodified == modified)
            {
                var message = "Up to date.";

                #region Green WriteLine
                var old = new { Console.BackgroundColor, Console.ForegroundColor };
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(message);

                Console.BackgroundColor = old.BackgroundColor;
                Console.ForegroundColor = old.ForegroundColor;
                #endregion

                return;
            }

            var disksource = XElement.Parse(File.ReadAllText(path.Value));
            var diskbody = disksource.Element("body");




            var newbodynodes = newbody.Nodes().TakeWhile(
                k =>
                {
                    var script = k as XElement;
                    if (script != null)
                        if (script.Name.LocalName == "script")
                            return false;

                    return true;
                }
            );


            diskbody.ReplaceNodes(newbodynodes);

            var newdisksource = disksource.ToString();

            {
                var message = "Project assets have been updated.".PadRight(80);


                #region red WriteLine
                var old = new { Console.BackgroundColor, Console.ForegroundColor };
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(message);

                Console.BackgroundColor = old.BackgroundColor;
                Console.ForegroundColor = old.ForegroundColor;
                #endregion
            }


            // The process cannot access the file 'X:\jsc.svn\examples\javascript\RuntimeHTMLDesignMode\RuntimeHTMLDesignMode\Design\App.htm' because it is being used by another process.
            try
            {
                File.WriteAllText(path.Value, newdisksource);
            }
            catch
            {
                Console.WriteLine("retry");
                Thread.Sleep(1);
                File.WriteAllText(path.Value, newdisksource);

            }

            //Console.WriteLine(newsource);
        }

        public void InternalHandler(WebServiceHandler h)
        {
            var app = h.Applications.Single();

            var originalsource = XElement.Parse(app.PageSource);
            var path = originalsource.Attribute("data-source");

            #region /view-source
            if (h.Context.Request.Path == "/view-source")
            {
                h.Context.Response.ContentType = "text/javascript";
                foreach (var item in app.References)
                {
                    h.Context.Response.Write("/* " + new { item.AssemblyFile, bytes = 1 } + " */\r\n");
                }

                foreach (var item in app.References)
                {
                    // asp.net needs absolute paths
                    h.Context.Response.WriteFile("/" + item.AssemblyFile + ".js");
                }


                h.CompleteRequest();
                return;
            }
            #endregion


            #region IsDefaultPath always take the dev version. only available on dev machine and session tho.. sqlite?
            if (h.IsDefaultPath)
            {
                h.Context.Response.ContentType = "text/html";


                var disksource = XElement.Parse(File.ReadAllText(path.Value));
                var diskbody = disksource.Element("body");

                // dont show this to browser.
                diskbody.Attribute("data-source").Remove();
                diskbody.Add(
                    new XAttribute("data-modified", "" + File.GetLastWriteTime(path.Value).Ticks)
                );

                #region /view-source
                var src = "/view-source";
                diskbody.Add(
                    new XElement("script",
                        new XAttribute("src", src),

                        // android otherwise closes the tag?
                        " "
                    )
                );
                #endregion

                h.Context.Response.Write(diskbody.ToString());

                h.CompleteRequest();
                return;
            }
            #endregion

            #region /upload
            if (h.Context.Request.Path == "/upload")
            {


                //var TextContent = h.Context.Request.Form["TextContent"];

                //Console.WriteLine(TextContent);


                var ok = new XElement("ok");

                foreach (HttpPostedFile item in h.Context.Request.Files.AllKeys.Select(k => h.Context.Request.Files[k]))
                {
                    //var bytes = item.InputStream.ReadToEnd();
                    var bytes = item.InputStream.ToBytes();

                    Console.WriteLine(
                        new { item.ContentType, item.FileName, item.ContentLength, bytes.Length }
                    );

                }


                h.Context.Response.ContentType = "text/xml";

                h.Context.Response.Write(ok);

                // close
                h.CompleteRequest();
                return;
            }
            #endregion

            if (!h.Context.Request.Path.StartsWith("/assets"))
            {
                // jsc did rewrite the assets. lets guess here. we are bypassing the asset path fixups
                // as we do a live reload.

                h.Context.Response.Redirect("/assets/RuntimeHTMLDesignMode" + h.Context.Request.Path);
                h.CompleteRequest();
                return;
            }
        }
    }
}
