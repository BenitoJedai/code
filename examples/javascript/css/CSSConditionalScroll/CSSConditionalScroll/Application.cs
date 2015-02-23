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
using CSSConditionalScroll;
using CSSConditionalScroll.Design;
using CSSConditionalScroll.HTML.Pages;

namespace CSSConditionalScroll
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://developer.mozilla.org/en-US/docs/Web/Guide/CSS/Media_queries
            // http://stackoverflow.com/questions/7657193/extending-attributes-selector-with-less-than-greater-than-in-jquery

            //Native.document.body.async.on
            //Native.document.documentElement.async.on


            //Native.document.body.ScrollToBottom();

            var status = new XAttribute("status", "?").AttachTo(Native.document.body);

            // can we send attributes to the server? can we send a list of attributes?
            var scrollBottom = new XAttribute("scrollBottom", "0").AttachTo(Native.document.body);
            var scrollTop = new XAttribute("scrollTop", "0").AttachTo(Native.document.body);

            //Native.document.body.css.before.contentXAttribute = scrollTop;
            Native.document.body.css.before.contentXAttribute = status;
            Native.document.body.css.before.style.position = IStyle.PositionEnum.@fixed;


            Native.document.body.css.orientation.landscape.before.style.background = "cyan";
            Native.document.body.css.orientation.portrait.before.style.background = "blue";

            //Native.document.body.css.before.style.background = "cyan";

            Native.document.body.css.before.style.bottom = "0";



            //Native.document.body.style.overflow = IStyle.OverflowEnum.auto;

            //Native.document.documentElement.onscroll +=

            // by using a local variable within css conditional, we could promte it into an xattribute?
            //var loc0 = 0;
            //Native.document.body.css[e => loc0 == 0].style

            Native.document.body.css.style.borderLeft = "1em solid yellow";
            Native.document.body.css[scrollBottom].style.borderLeft = "1em solid red";
            Native.document.body.css[scrollTop].style.borderLeft = "1em solid green";

            //21ms this.rule = this.parent.rule[__selectorText]; 
            // { parent = { selectorElement = , rule = { 
            // selectorText = @media all and (orientation: landscape) , type = 4 } },
            // __selectorText = body } 

            // http://stackoverflow.com/questions/15172520/drawback-of-css-displayinline-block-vs-floatleft
            // 41ms IStyleSheetRule.AddRule error { text = >div{/**/} } 
            //Native.document.body.css.orientation.portrait[" div"].style.display = IStyle.DisplayEnum.inline_block;
            //Native.document.body.css.orientation.portrait.style.whiteSpace = IStyle.WhiteSpaceEnum.nowrap;

            //Native.document.body.css[page.scrollheader].style.position = IStyle.PositionEnum.@fixed;

            // error?
            // http://my.jsc-solutions.net/
            // when running as an inline example why the error?
            // not able to find our scroll header?
            // not part of the html?
            Native.document.body.css[page.scrollheader].style.top = "-5em";
            Native.document.body.css[page.scrollheader].style.transition = "top 300ms linear";

            Native.document.body.css[scrollTop][page.scrollheader].style.backgroundColor = "green";
            Native.document.body.css[scrollTop][page.scrollheader].style.top = "0em";


            //Native.document.body.css[page.scrollfooter].style.position = IStyle.PositionEnum.@fixed;
            Native.document.body.css[page.scrollfooter].style.left = "-100%";
            Native.document.body.css[page.scrollfooter].style.transition = "left 300ms linear";

            Native.document.body.css[scrollBottom][page.scrollfooter].style.backgroundColor = "red";
            Native.document.body.css[scrollBottom][page.scrollfooter].style.left = "0em";


            Native.window.onscroll +=
                delegate
                {
                    // scrollTop: 324

                    //scrollTop.Value = "" + Native.document.documentElement.scrollTop;
                    scrollTop.Value = "" + Native.document.body.scrollTop;
                    scrollBottom.Value = "" + Native.document.body.scrollBottom;

                    status.Value = new
                    {
                        scrollTop,
                        scrollBottom,
                        Native.document.documentElement.scrollHeight,
                        Native.document.documentElement.offsetHeight,

                        Native.document.documentElement.clientHeight,
                    }.ToString();

                    //scrollTop.Value = "" + Native.document.documentElement.scrollTop;

                    //new { Native.document.body.scrollTop }.ToString().ToDocumentTitle();


                };

            //new IHTMLAnchor { "drag me" }.AttachTo(Native.document.documentElement).With(
            //    dragme =>
            //    {
            //        dragme.style.position = IStyle.PositionEnum.@fixed;
            //        dragme.style.left = "1em";
            //        dragme.style.bottom = "1em";

            //        dragme.style.zIndex = 1000;

            //        dragme.AllowToDragAsApplicationPackage();
            //    }
            //);
        }

    }
}
