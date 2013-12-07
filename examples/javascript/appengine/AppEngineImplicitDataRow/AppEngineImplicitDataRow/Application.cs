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

            Native.window.onframe +=
                delegate
                {
                    var lines = page.content.Lines.ToArray();

                    Native.document.title = new { lines.Length, page.content.SelectionStart }.ToString();
                };

            page.contentinfo.AsXElement().Elements().First().With(
                line =>
                {

                    page.content.onvaluechanged +=
                        delegate
                        {
                            page.content.style.color = "transparent";


                            page.contentinfo.AsXElement().RemoveNodes();

                            page.content.Lines.ToArray().WithEachIndex(
                                (text, index) =>
                                {

                                    var x = new XElement(line);

                                    // Uncaught TypeError: Cannot call method 'IgUABvGk4TmHjG5RCF5eIg' of undefined 

                                    //x.Element("SPAN").Value = "" + index;

                                    if (text == "")
                                        text = "...?";

                                    x.Element("DIV").Value = text;

                                    x.AsHTMLElement().AttachTo(page.contentinfo);
                                }
                            );
                        };



                }
            );

        }

    }
}
