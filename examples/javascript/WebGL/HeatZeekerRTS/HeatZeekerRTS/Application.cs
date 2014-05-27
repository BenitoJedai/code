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
    // for intellisense? or will roslyn allow to import enums too?
    public static IHTMLElement.HTMLElementEnum img = IHTMLElement.HTMLElementEnum.img;
    public static IHTMLElement.HTMLElementEnum div = IHTMLElement.HTMLElementEnum.div;

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
            // position: fixed; top: 0; left: 0; right: 0; 

            //background: linear - gradient(to bottom, rgba(0, 0, 0, 0.9) 0 %, rgba(0, 0, 0, 0) 100 %); /* W3C */

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


            new IStyle(css[IHTMLElement.HTMLElementEnum.div])
            {

                transition = "border 100ms linear"
            };




            new IStyle(!css[IHTMLElement.HTMLElementEnum.div].hover)
            //new IStyle(css.hover & !css[IHTMLElement.HTMLElementEnum.div].hover)
            {
                border = "1px solid rgba(255,0,0,0.7)"
            };

            new IStyle(css[IHTMLElement.HTMLElementEnum.div].active)
            {
                // while mouse down be cyan
                border = "1px solid rgba(255,255,0,0.7)"
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

            // X:\jsc.svn\examples\javascript\android\com.abstractatech.gamification.craft\com.abstractatech.gamification.craft\Design\App.htm

            new IStyle(IHTMLElement.HTMLElementEnum.head)
            {
                top = "0px",
                left = "0px",
                right = "0px",
                height = "3em",

                background = "linear-gradient(to bottom, rgba(0,0,0,0.9) 0%,rgba(0,0,0,0) 100%)" /* W3C */
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




            #region playmusic
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
            #endregion


            //body[typeof(IHTMLDiv)].onclick += { };


            document.onmousedown +=
                                e =>
                {
                    e.preventDefault();

                };

            document[div].onmouseover +=
                e =>
                {
                    new flag { volume = 0.3 }.play();

                    page.hud_look.Hide();
                };

            document[div].onmouseout +=
                e =>
                {

                    // should jsc prebuffer all audio linked into app?
                    // and fonts?
                    new tick { volume = 0.3 }.play();

                    page.hud_look.Show();
                };


            document.oncontextmenu +=
                 e =>
                {
                    e.preventDefault();
                    e.stopPropagation();
                };

            document[div].oncontextmenu +=
                 e =>
                {
                    new buzzer { volume = 0.2 }.play();

                    ((IHTMLElement)e.Element).Orphanize();
                };


            //document[e => e.Element is IHTMLDiv]


            // query selector.
            //document[IHTMLElement.HTMLElementEnum.div].


            // X:\jsc.svn\examples\javascript\VirtualElementEvents\VirtualElementEvents\Application.cs


            document[div].onclick += delegate
            {
                new snd_dooropen().play();

            };

            document[img].onclick += e =>
            {
                new snd_dooropen().play();

                Console.WriteLine(
                                                    new
                {
                    div,
                    e.Element.nodeName,

                    e.OffsetX,
                    e.OffsetY
                }

                                                    );

                //var fixleft = e.CursorX - body.scrollLeft;



                new IHTMLDiv().AttachToDocument().style.SetLocation(
                    e.OffsetX,
                    e.OffsetY
                );

            };



            document.documentElement.onmousemove +=
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
                        //body.scrollTop,

                        ////fixtop,
                        //body.scrollHeight,
                        ////body.clientHeight,
                        //document.documentElement.clientHeight,
                        x = fixleft,
                        y = e.CursorY,

                        ////e.OffsetX,
                        ////e.CursorX,

                        //body.scrollLeft,

                        //body.scrollWidth,

                        //body.clientWidth
                    };

                };
        }

    }
}


