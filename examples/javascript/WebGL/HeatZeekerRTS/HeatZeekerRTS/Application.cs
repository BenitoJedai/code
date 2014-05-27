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

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140527/hz

            new IStyle(css[IHTMLElement.HTMLElementEnum.div])
            {
                // last change was abut adding pointer
                // jsc jit could atleast let us know how it looks like
                cursor = IStyle.CursorEnum.pointer
            };

            new IStyle(!css[IHTMLElement.HTMLElementEnum.div].hover)
            {
                border = "1px solid rgba(0,0,0,0.0)"
            };

            new IStyle(css[IHTMLElement.HTMLElementEnum.head] | css[IHTMLElement.HTMLElementEnum.title])
            {
                display = IStyle.DisplayEnum.block,
                //position = IStyle.PositionEnum.absolute,
                position = IStyle.PositionEnum.@fixed,

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


