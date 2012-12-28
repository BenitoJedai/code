using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestNestedIFrameForMoreWidth.Design;
using TestNestedIFrameForMoreWidth.HTML.Pages;

namespace TestNestedIFrameForMoreWidth
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
        public Application(IApp page)
        {
            var w = Native.Window.Width;
            var h = Native.Window.Height;

            new IHTMLPre { }.AttachToDocument().With(
                i =>
                {
                    Action u = delegate
                    {
                        i.innerText = new { w, Native.Window.Width, h, Native.Window.Height }.ToString();
                    };

                    u();

                    Native.Window.onresize +=
                        delegate
                        {
                            u();
                        };
                }
            );

            var location = "" + Native.Document.location;
            var parent = "";

            new IHTMLPre
            {
                innerText = new
                {
                    location,
                    Native.Document.location.protocol,
                    Native.Document.location.host,
                    Native.Document.location.pathname,
                    Native.Document.location.search,
                    Native.Document.location.hash,
                }.ToString()
            }.AttachToDocument();

            Native.Window.parent.With(
                xparent =>
                {
                    // http://stackoverflow.com/questions/5934538/is-there-a-limitation-on-an-iframe-containing-another-iframe-with-the-same-url
                    parent = "" + xparent.document.location;

                    new IHTMLPre { innerText = new { parent }.ToString() }.AttachToDocument();
                }
            );

            if (w < 400)
            {
                Native.Document.body.style.backgroundColor = JSColor.Yellow;

                new IHTMLButton("reopen as a bigger document").AttachToDocument().onclick +=
                    delegate
                    {
                        var src = location;

                        if (location == parent)
                        {
                            var withouthash = src.TakeUntilIfAny("#");
                            var onlyhash = src.SkipUntilOrEmpty("#");

                            withouthash += "?";

                            if (onlyhash != "")
                            {
                                withouthash += "#" + onlyhash;
                            }

                            src = withouthash;
                        }

                        var iframe = new IHTMLIFrame { src = src, frameBorder = "0" };


                        iframe.style.minWidth = "800px";
                        iframe.style.minHeight = "600px";

                        iframe.style.position = IStyle.PositionEnum.absolute;
                        iframe.style.left = "0px";
                        iframe.style.top = "0px";
                        iframe.style.width = "100%";
                        iframe.style.height = "100%";

                        Native.Document.body.Clear();
                        Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

                        iframe.onload +=
                            delegate
                            {
                                Console.WriteLine(
                                    new { location, iframe.src }
                                );

                                iframe.style.minWidth = "";
                                iframe.style.minHeight = "";

                            };


                        Console.WriteLine(
                             "will reload as: " + new { src }
                         );

                        iframe.AttachToDocument();
                    };

                return;
            }

            Native.Document.body.style.backgroundColor = JSColor.White;


            new IHTMLButton("this is dynamic content").AttachToDocument();

            new IHTMLButton("reload").AttachToDocument().onclick +=
                delegate
                {
                    Native.Document.location = Native.Document.location;
                };


            new IHTMLButton("add frame").AttachToDocument().onclick +=
                delegate
                {
                    var src = location;

                    if (location == parent)
                    {
                        var withouthash = src.TakeUntilIfAny("#");
                        var onlyhash = src.SkipUntilOrEmpty("#");

                        withouthash += "?";

                        if (onlyhash != "")
                        {
                            withouthash += "#" + onlyhash;
                        }

                        src = withouthash;
                    }




                    var iframe = new IHTMLIFrame { src = "about:blank" };


                    iframe.style.position = IStyle.PositionEnum.absolute;
                    iframe.style.left = "10%";
                    iframe.style.top = "10%";
                    iframe.style.width = "80%";
                    iframe.style.height = "80%";

                    iframe.AttachToDocument();


                    Console.WriteLine(
                         "will frame as: " + new { src }
                     );

                    Native.Window.requestAnimationFrame +=
                        delegate
                        {
                            iframe.src = src;

                            Console.WriteLine(
                                 "did it work? " + new { iframe.src }
                             );

                        };


                };


            new IHTMLButton("add frame via source").AttachToDocument().onclick +=
               delegate
               {
                   var iframe = new IHTMLIFrame { };


                   iframe.style.position = IStyle.PositionEnum.absolute;
                   iframe.style.left = "10%";
                   iframe.style.top = "10%";
                   iframe.style.width = "80%";
                   iframe.style.height = "80%";

                   iframe.AttachToDocument();


                   var x = iframe.contentWindow.open("about:blank", "_self");

                   x.document.write("<script src='/view-source'></script>");
                   x.document.close();



               };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
