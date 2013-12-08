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
using AppEngineImplicitDataRow;
using AppEngineImplicitDataRow.Design;
using AppEngineImplicitDataRow.HTML.Pages;
using System.Diagnostics;

namespace AppEngineImplicitDataRow
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
            // a linked list builder

            #region title
            Native.document.ontitlechanged +=
                delegate
                {
                    page.topic.value = Native.document.title;
                };

            page.topic.onchange +=
                delegate
                {
                    Native.document.title = page.topic.value;
                };
            #endregion


            #region onscroll
            page.content.onscroll +=
                delegate
                {
                    //                c = nyMABtNdQz66ZYUODttTfw(new Number((-a[0].page.RAAABuvXAzazmyjb19NBig().scrollTop)), 'px');
                    //ugcABL0v_bj2Oe_a3KkOpn0g.title = (new ctor$UQAABt6szjCsnB_aL9_aJaIQ(c, a[0].page.RgAABuvXAzazmyjb19NBig().scrollTop)+'');

                    var scrollTop = page.content.scrollTop;

                    var marginTop = (-scrollTop) + "px";

                    //Native.document.title = new { marginTop, scrollTop }.ToString();

                    page.contentinfo.style.marginTop = marginTop;

                };
            #endregion

            var y = 0;
            var i = -1;


            // http://www.w3schools.com/cssref/tryit.asp?filename=trycss3_nth-child_formula

            var xxx = default(CSSStyleRule);
            //var xxxi = -1;

            page.content.onmouseout +=
                e =>
                {
                    xxx.OrphanizeRule();
                };

            page.content.onmousemove +=
                e =>
                {
                    // Uncaught TypeError: Cannot read property 'layerY' of null
                    y = e.OffsetY + page.content.scrollTop;

                    if (page.fs.@checked)
                    {
                        y += page.content.parentNode.offsetTop;
                    }

                    var hit =
                        from x in page.contentinfo.childNodes
                        where x.nodeType == INode.NodeTypeEnum.ElementNode
                        let u = (IHTMLElement)x
                        select u;


                    hit.WithEachIndex(
                        (u, index) =>
                        {

                            if (y > u.offsetTop)
                                if (y < (u.offsetTop + u.offsetHeight))
                                {
                                    i = index;
                                }
                        }
                    );

                    xxx.OrphanizeRule();
                    xxx = page.contentinfo.css.nthChild[i + 1];
                    xxx.style.backgroundColor = "lightgray";
                };

            var w = new Stopwatch();

            Native.window.onframe +=
                delegate
                {
                    var lines = page.content.Lines.ToArray();

                    Native.document.title = new { y = y, i, lines.Length, page.content.SelectionStart, w.ElapsedMilliseconds }.ToString();
                };

            //page.content.onsel

            // Uncaught SyntaxError: An invalid or illegal string was specified. 

            page.contentinfo.css[IHTMLElement.HTMLElementEnum.li].after.content = "¶";
            page.contentinfo.css[IHTMLElement.HTMLElementEnum.li].after.style.color = "gray";

            //Uncaught SyntaxError: An invalid or illegal string was specified. 
            //page.contentinfo.css[IHTMLElement.HTMLElementEnum.li][">span:contains('<')"].style.color = "red";

            // http://www.w3schools.com/cssref/pr_gen_content.asp
            page.contentinfo.css[IHTMLElement.HTMLElementEnum.li][IHTMLElement.HTMLElementEnum.span].before.style.content = "attr(title)";


            #region  charmap
            for (char ch = '!'; ch < 0xff; ch++)
            {
                var color = "purple";

                if (char.IsNumber(ch))
                {
                    color = "red";
                }
                else if (char.IsLetter(ch))
                {
                    color = "blue";
                }

                // Uncaught SyntaxError: An invalid or illegal string was specified. 
                // "[title='" + new string(ch, 1) + "']"

                // IStyleSheetRule.AddRule error { text = [style-id="0"] > li > span[title='']{/**/} }

                // { ch = 92, byAttr = [title='\'] } 
                var byAttr = "[title='" + new string(ch, 1)
                    .Replace("\\", "\\\\")
                    .Replace("'", "\\'")
                    + "']";

                Console.WriteLine(new { ch, byAttr });

                page.contentinfo.css[IHTMLElement.HTMLElementEnum.li][IHTMLElement.HTMLElementEnum.span][
                    byAttr
                    ].before.style.color = color;
            }
            #endregion


            Action onvaluechanged = delegate
            {
                w.Restart();

                page.content.style.color = "transparent";


                page.contentinfo.Clear();

                page.content.Lines.ToArray().WithEachIndex(
                    (text, index) =>
                    {
                        var li = new IHTMLListItem();

                        foreach (var item in text)
                        {
                            IHTMLSpan span = item;

                            span.title = span.innerText;
                            span.Clear();


                            span.AttachTo(li);
                        }


                        li.AttachTo(page.contentinfo);
                    }
                );

                w.Stop();

            };

            page.content.onvaluechanged += onvaluechanged;



            onvaluechanged();


        }

    }
}
