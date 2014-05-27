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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HeatZeekerRTS;
using HeatZeekerRTS.Design;
using HeatZeekerRTS.HTML.Pages;
using ScriptCoreLib.JavaScript.Native;
using XNative;
using HeatZeekerRTS.HTML.Images.FromAssets;
using ScriptCoreLib.JavaScript.DOM.SVG;
using HeatZeekerRTS.HTML.Audio.FromAssets;

static class XNative
{
    //public dynamic title;
    public static object title
    {
        set
        {
            document.title = value.ToString();
        }
    }

    public static IHTMLBody body
    {
        get
        {
            return document.body;
        }
    }
}

namespace HeatZeekerRTS
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    [Obsolete("jsc jit")]
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\svg\SVGCSSContent\SVGCSSContent\Application.cs
            // jsc where id the svg cursor example?
            // X:\jsc.svn\examples\javascript\android\MultiMouse\MultiMouse.SVGCursor


            // webgl is to provide 3d point clouds with tilt shift
            // for physics and palm trees, with md2 actors

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140527/hz

            // can we create our svg object now and interact with it?
            // path2985



            //css.style.cursorImage = new MyCursor();

            //new MyCursor().AttachToHead();
            //document.head


            // jsc when can we talk to assets?
            //new MyCursor().path2985.fill = "blue";


            #region svg cursor
            new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET,
                 new MyCursor().src,
                 r =>
                {
                    // public static XElement AsXElement(this IElement e);
                    var svg = (ISVGSVGElement)(IElement)(r.responseXML.documentElement);


                    var cursor1 = svg.AsXElement();




                    cursor1
                        .Elements("g")
                        .Elements("path")
                        .Where(x => x.Attribute("id").Value == "path2985")
                        .WithEach(
                            path =>
                                path.Attribute("style").Value = path.Attribute("style").Value.Replace("fill:#ffff00;", "fill:#00ff00;")
                        );

                    cursor1
                        .Elements("g")
                        .Elements("path")
                        .Where(x => x.Attribute("id").Value == "path2985-1")
                        .WithEach(
                            path =>
                                path.Attribute("style").Value = path.Attribute("style").Value.Replace("fill:#d9d900;", "fill:#00df00;")
                        );

                    //.AttachToDocument();

                    css.style.cursorImage = svg;

                    new IStyle(css[IHTMLElement.HTMLElementEnum.div].hover)
                    {
                        // last change was abut adding pointer
                        // jsc jit could atleast let us know how it looks like
                        //cursor = IStyle.CursorEnum.pointer

                        cursorImage = new MyCursor()
                    };

                    //Native.document.documentElement.style.cursorImage = svg;
                    //Native.document.documentElement.style.cursorImage = cursor1;
                    //Native.document.documentElement.style.cursorElement = cursor1;
                    //Native.document.documentElement.style.cursorElement = cursor1.AsHTMLElement();

                    //public static IHTMLElement AttachToDocument(this XElement e);

                    //.AttachToHead();
            }
            );
            #endregion



            // can we show another color
            // if howering?




            new IStyle(!css[IHTMLElement.HTMLElementEnum.div].hover)
            {
                border = "1px solid rgba(0,0,0,0.0)"
            };

            new IStyle(css[IHTMLElement.HTMLElementEnum.head] | css[IHTMLElement.HTMLElementEnum.title])
            {
                display = IStyle.DisplayEnum.block,
                //position = IStyle.PositionEnum.absolute,
                position = IStyle.PositionEnum.@fixed,


                color = "yellow",
                //fontSize = ""
                whiteSpace = IStyle.WhiteSpaceEnum.nowrap,

                zIndex = 100
            };

            // https://bugs.webkit.org/show_bug.cgi?id=56543
            //(css[IHTMLElement.HTMLElementEnum.title].style as dynamic).webkitTextSizeAdjust = "none";

            // http://stackoverflow.com/questions/7907760/why-the-font-size-wont-change-with-browser-zoom-in


            // {{ OffsetX = 3721, CursorX = 3721, scrollLeft = 2831.818120440177, 
            // scrollWidth = 3999.9999133023366, clientWidth = 1142.1875 }}


            // wwould jsc be able to tell me whats the difference by adding this line?
            //body.style.overflowY = IStyle.OverflowEnum.hidden;
            body.style.overflow = IStyle.OverflowEnum.hidden;




            // music!
            // http://stackoverflow.com/questions/7747526/why-wont-my-html5-audio-loop


            Action playmusic = null;

            playmusic = delegate
            {
                new crickets
                {

                    //loop = true,
                    //controls = true
                }.AttachToHead().With(
                //async music =>
                music =>
                {
                    music.play();

                    //while (music.async.onen)

                    music.onended +=
                        delegate
                    {

                        // why wont it play?
                        title = "  music.onended ";

                        //music.currentTime = 0;
                        //music.play();

                        music.Orphanize();

                        playmusic();
                    };
                }
                );
            };

            playmusic();


            //body[typeof(IHTMLDiv)].onclick += { };

            //body.onmouseover[typeof(IHTMLDiv)] += 


            body.onmouseover +=
                e =>
                {
                    if (e.Element.nodeName == "DIV")
                    {
                        new flag { volume = 0.3 }.play();
                    }
                };

            body.onmouseout +=
                e =>
                {
                    if (e.Element.nodeName == "DIV")
                    {
                        // should jsc prebuffer all audio linked into app?
                        // and fonts?

                        new tick { volume = 0.3 }.play();
                    }
                };


            body.oncontextmenu +=
                 e =>
                {
                    if (e.Element.nodeName == "DIV")
                    {
                        //e.Element.Orphanize();
                        e.preventDefault();
                        e.stopPropagation();

                        ((IHTMLElement)e.Element).Orphanize();
                    }

                };

            body.onclick +=
                e =>
                {
                    //e.Element

                    // native isinst. do we support that yet?
                    var div = e.Element is IHTMLDiv;


                    //4:3426ms { { div = false, nodeName = DIV } }
                    //4:33440ms { { div = false, nodeName = IMG } }

                    if (e.Element.nodeName == "DIV")
                    {
                        new snd_dooropen().play();
                    }

                    if (e.Element.nodeName == "IMG")
                    {
                        Console.WriteLine(
                            new
                        {
                            div,
                            e.Element.nodeName
                        }

                            );

                        new IHTMLDiv().AttachToDocument().style.SetLocation(
                            e.OffsetX,
                            e.OffsetY
                        );

                    }

                };



            body.onmousemove +=
                e =>
                {
                    // edit and continue yet?
                    // either msbuild or roslyn?

                    var fixleft = e.CursorX - body.scrollLeft;
                    //var fixtop = e.CursorY - body.scrollTop;
                    // fixleft spans from 0 ... clientwidth
                    // scrollleft shal follow it from 0 to scrollWidth - ?view width * zoom?

                    // scroll width oes not know about zoom
                    // we did this already for pipe mania
                    // and webgl earth
                    body.scrollLeft = fixleft * (body.scrollWidth) / body.clientWidth;
                    //body.scrollTop = fixtop * (body.scrollHeight) / document.documentElement.clientHeight;
                    body.scrollTop = e.CursorY * (body.scrollHeight) / document.documentElement.clientHeight;

                    title = new
                    {
                        e.CursorY,
                        body.scrollTop,

                        //fixtop,
                        body.scrollHeight,
                        //body.clientHeight,
                        document.documentElement.clientHeight,
                        fixleft,

                        //e.OffsetX,
                        //e.CursorX,

                        body.scrollLeft,

                        body.scrollWidth,

                        body.clientWidth
                    };

                };
        }

    }
}


