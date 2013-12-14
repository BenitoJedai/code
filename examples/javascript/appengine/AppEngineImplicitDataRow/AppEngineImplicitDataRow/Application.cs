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
        sealed class IntBox
        {
            public int index;
        }

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

            var yx = 0;
            var y = 0;
            var i = -1;


            // http://www.w3schools.com/cssref/tryit.asp?filename=trycss3_nth-child_formula

            var xxx = default(CSSStyleRuleMonkier);
            //var xxxi = -1;

            page.content.onmouseout +=
                e =>
                {
                    xxx.OrphanizeRule();
                };

            var w = new Stopwatch();

            #region onmousemove

         

            bool skip = false;

            page.content.style.backgroundColor = "rgba(0,0,0,0.1)";
            page.content.onmousemove +=
                e =>
                {
                    if (skip)
                        return;

                    w.Restart();

                    //// Uncaught TypeError: Cannot read property 'layerY' of null
                    y = e.OffsetY;

                    // the padding
                    yx = e.OffsetX - 128;

                    if (page.fs.@checked)
                    {
                        // ??? magic!
                        //y += page.topic.clientHeight;
                    }

                    y = (int)Math.Floor((double)y / ((IHTMLElement)page.contentinfo.firstChild).clientHeight);

                    page.content.style.cursor = IStyle.CursorEnum.text;
                    page.content.title = "";


                    page.contentinfo.childNodes.ElementAtOrDefault(y).With(
                        li =>
                        {
                            // how many chars until the outer right is more than y?

                            // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.Cast(System.Collections.IEnumerable)]
                            i = li.childNodes.AsEnumerable().Select(z => (IHTMLSpan)z).TakeWhile(span => span.offsetLeft < yx).Count();

                            // cant be less than 0 yet can be more than visible chars
                            li.childNodes.AsEnumerable().Select(z => (IHTMLSpan)z).ElementAtOrDefault(i).With(
                                 async span =>
                                 {

                                     if (span.title == "c")
                                         if ((string)span.getAttribute("data-prev2") == "js")
                                         {

                                             // reveal
                                             //page.content.style.visibility = IStyle.VisibilityEnum.hidden;


                                             // upgrade
                                             span.style.cursor = IStyle.CursorEnum.pointer;
                                             //span.title = "jsc";

                                             // once
                                             span.onclick +=
                                                 ee =>
                                                 {
                                                     e.preventDefault();
                                                     e.stopPropagation();

                                                     Native.window.alert("jsc");
                                                 };

                                             span.onmouseout +=
                                                 delegate
                                                 {

                                                     page.content.style.zIndex = 10;
                                                     skip = false;
                                                 };

                                             skip = true;
                                             page.content.style.zIndex = -10;
                                             await Native.window.requestAnimationFrameAsync;


                                             span.ondragstart +=
                                                 async ee =>
                                                 {
                                                     //ee.preventDefault();
                                                     //ee.stopPropagation();

                                                     Console.WriteLine("dragstart!");

                                                     ee.dataTransfer.setData("text/uri-list", "http://my.jsc-solutions.net");

                                                     await Native.window.requestAnimationFrameAsync;
                                                     page.content.style.zIndex = 10;
                                                     skip = false;

                                                     // revert, stop events
                                                     //page.content.style.visibility = IStyle.VisibilityEnum.visible;
                                                 };


                                             return;
                                         }


                                 }
                            );
                        }
                    );



                    w.Stop();
                };
            #endregion





            //page.content.onsel

            // Uncaught SyntaxError: An invalid or illegal string was specified. 



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

                var charAsString = new string(ch, 1);

                //var byAttr = "[title='" + new string(ch, 1)
                //    .Replace("\\", "\\\\")
                //    .Replace("'", "\\'")
                //    + "']";

                //Console.WriteLine(new { ch, charAsString });

                page.contentinfo.css
                    [IHTMLElement.HTMLElementEnum.li]
                    [IHTMLElement.HTMLElementEnum.span]
                    [span => span.title == charAsString].before.style.color = color;

            }
            #endregion

            // .before.style.color = color;
            //page.contentinfo.css[IHTMLElement.HTMLElementEnum.li]
            var charAsString_n = "\\n";

            page.contentinfo.css
                  [IHTMLElement.HTMLElementEnum.li]
                  [IHTMLElement.HTMLElementEnum.span]
                  [span => span.title == charAsString_n].With(
                    n =>
                    {
                        n.before.content = "¶";
                        n.before.style.color = "gray";
                    }
            );


            //page.contentinfo.css[IHTMLElement.HTMLElementEnum.li].after.style.color = "gray";


            page.contentinfo.css
                 [IHTMLElement.HTMLElementEnum.li]
                 [IHTMLElement.HTMLElementEnum.span]
                 ["[data-prev1='j']"]
                 ["[data-next1='c']"]
                 [span => span.title == "s"].before.style.backgroundColor = "yellow";

            page.contentinfo.css
               [IHTMLElement.HTMLElementEnum.li]
               [IHTMLElement.HTMLElementEnum.span]
               ["[data-prev2='js']"]
               [span => span.title == "c"].With(__c =>
                   {
                       __c.before.style.backgroundColor = "black";
                       __c.before.style.color = "yellow";
                       __c.before.style.textDecoration = "underline";

                       // make clickable
                       __c.style.zIndex = 200;
                   }
            );


            page.contentinfo.css
               [IHTMLElement.HTMLElementEnum.li]
               [IHTMLElement.HTMLElementEnum.span]
               ["[data-next1='s']"]
               [span => span.title == "j"].before.style.backgroundColor = "yellow";


            // { selectorText = [style-id="0"] > li > :nth-child(3) > span::before } 

            var SelectedRowIndex = new IntBox { index = 1 };
            var SelectedColumnIndex = new IntBox { index = 1 };

            var xs_focus = page.contentinfo.css[() => SelectedRowIndex.index];
            xs_focus.style.backgroundColor = "rgba(0,0,0,0.2)";

            var xs_hover = page.contentinfo.css[() => y];

            xs_hover.style.backgroundColor = "rgba(0,0,0,0.1)";


            xs_focus[() => SelectedColumnIndex.index].before.With(
                async blinker =>
                {
                    while (true)
                    {
                        blinker.style.backgroundColor = "red";
                        await Task.Delay(300);

                        blinker.style.backgroundColor = "";
                        await Task.Delay(100);
                    }
                }
            );
            xs_hover[() => i].before.style.backgroundColor = "cyan";

            //[IHTMLElement.HTMLElementEnum.span]
            //.before;

            //Console.WriteLine(new { xs.selectorText });

            //xs.style.backgroundColor = "cyan";

            #region onvaluechanged
            Action onvaluechanged = delegate
            {
                w.Restart();

                page.content.style.color = "transparent";


                page.contentinfo.Clear();

                page.content.Lines.ToArray().WithEachIndex(
                    (text, index) =>
                    {
                        var li = new IHTMLListItem();



                        var spans = text.ToCharArray().AsEnumerable().Select(item =>
                            {
                                //Console.WriteLine(new { item });

                                IHTMLSpan span = item;



                                //span.title = " " + item;
                                span.title = span.innerText;
                                span.Clear();

                                //if (item == '\n')
                                //{
                                //    span.title = "\\n";
                                //    span.style.backgroundColor = "red";
                                //}

                                span.AttachTo(li);

                                return span;
                            }
                        ).ToArray();

                        new IHTMLSpan { title = "\\n" }.AttachTo(li);


                        // make css happy by pre indexing 
                        // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.Aggregate(
                        // { x = , yy =  } 
                        spans.Aggregate(
                            seed: new List<IHTMLSpan>(),
                            func: (prev, current) =>
                            {
                                if (prev.Count > 0)
                                {
                                    var prev1 = prev[0];

                                    //Console.WriteLine(
                                    //    new { prev1 = prev1.title, current = current.title }
                                    //    );



                                    //dynamic current_data = current;



                                    current.setAttribute("data-prev1", prev1.title);

                                    prev1.setAttribute("data-next1", current.title);


                                    if (prev.Count > 1)
                                    {
                                        var prev2 = prev[1];

                                        current.setAttribute("data-prev2", prev2.title + prev1.title);
                                    }
                                }

                                var list = new List<IHTMLSpan>();

                                list.Add(current);


                                list.AddRange(
                                    prev
                                );



                                return list;
                            }
                        );


                        //foreach (var item in text)
                        //{
                        //    IHTMLSpan span = item;

                        //    span.title = span.innerText;
                        //    span.Clear();


                        //    span.AttachTo(li);
                        //}


                        li.AttachTo(page.contentinfo);
                    }
                );

                w.Stop();
                page.content.style.zIndex = 10;
                skip = false;
            };
            #endregion


            page.content.onvaluechanged += onvaluechanged;



            onvaluechanged();

            page.content.value += "\n\n hover, click or drag jsc <--";


            Native.window.onframe +=
                delegate
                {
                    SelectedRowIndex.index = page.content.value.Substring(0, page.content.SelectionStart).ToCharArray().Where(x => x == '\n').Count();

                    // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.TakeWhile(System.Collections.Generic.IEnumerable`1[[System.Char, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], System.Func`2[[System.Char, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]
                    var reverse = page.content.value.Substring(0, page.content.SelectionStart).ToCharArray().Reverse();


                    SelectedColumnIndex.index = reverse.TakeWhile(x => x != '\n').Count();



                    var lines = page.content.Lines.ToArray();

                    Native.document.title = new
                    {
                        y,
                        yx,
                        i,

                        row = SelectedRowIndex.index,
                        col = SelectedColumnIndex.index,

                        page.content.SelectionStart,
                        lines.Length,

                        w.ElapsedMilliseconds
                    }.ToString();
                };
        }

    }
}
