using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using TestLongConnection.Design;
using TestLongConnection.HTML.Pages;

namespace TestLongConnection
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            page.Start.onclick +=
                delegate
                {
                    InitializeContnt(page);
                };
        }

        static void InitializeContnt(IDefaultPage page)
        {
            Action<dynamic> stream =
                x =>
                {
                    object time = x.time;
                    object i = x.i;

                    Native.Document.title = new { time, i }.ToString();
                    Console.WriteLine(new { time, i }.ToString());
                    page.Content.innerText = new { time, i }.ToString();
                };

            dynamic window = Native.Window;

            window.stream = stream;

            var stop = new IHTMLButton("Stop").AttachToDocument();

            var iframe = new IHTMLIFrame { src = "/stream" }.AttachToDocument();
            iframe.Hide();

            page.Start.disabled = true;

            stop.onclick +=
                delegate
                {
                    stop.Orphanize();
                    page.Start.disabled = false;
                    iframe.Orphanize();
                };

            Native.Document.body.style.cursor = IStyle.CursorEnum.@default;
        }
    }

    partial class ApplicationWebService
    {

        public void Handler(WebServiceHandler h)
        {
            if (h.Context.Request.Path == "/stream")
            {
                //Connection:close
                //Content-Type:text/html
                //Transfer-Encoding:chunked

                //Cache-Control:private
                //Connection:Close
                //Content-Type:text/html; charset=utf-8
                //Date:Tue, 09 Apr 2013 20:27:16 GMT
                //Server:ASP.NET Development Server/11.0.0.0
                //Transfer-Encoding:chunked
                //X-AspNet-Version:4.0.30319


                h.Context.Response.ContentType = "text/html";
                //h.Context.Response.Cache.SetCacheability(System.Web.HttpCacheability.Private);


                var i = 0;
                while (h.Context.Response.IsClientConnected)
                {
                    i++;



                    #region flush
                    Action<object> flush =
                        (time) =>
                        {
                            var w = new StringBuilder();

                            // Cannot implicitly convert type '<>f__AnonymousType0<System.DateTime,int>' to 'System.Collections.IEnumerable'

                            Console.WriteLine(new { time, i });


                            w.Append("parent.stream(");
                            w.Append("{");



                            w.Append("time:'" + time + "',");
                            w.Append("i:" + i + "");
                            w.Append("}");
                            w.Append(");");

                            // http://stackoverflow.com/questions/13557900/chunked-transfer-encoding-browser-behavior

                            w.Append(new string('/', 1024));


                            h.Context.Response.Write(new XElement("script", w.ToString()) + "\r\n");
                            //h.Context.Response.Flush();
                        };
                    #endregion

                    flush(DateTime.Now);




                    Thread.Sleep(1000);
                }

                h.CompleteRequest();
                return;
            }
        }
    }
}
