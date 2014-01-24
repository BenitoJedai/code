using TestDataGridPadding;
using TestDataGridPadding.Design;
using TestDataGridPadding.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;

namespace TestDataGridPadding
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            content.AttachControlToDocument();


            #region css
            __DataGridView gg = content.dataGridView1;

            //gg.__ColumnsTable_css_td.style.textTransform = IStyle.TextTransformEnum.uppercase;
            //gg.__ColumnsTable_css_td.style.color = "gray";

            //gg.__ContentTable_css_td.hover.style.cursor = IStyle.CursorEnum.pointer;

            //gg.__ContentTable_css_td.hover.style.color = "blue";

            ////gg.__ContentTable.css[IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr].hover[IHTMLElement.HTMLElementEnum.td].style.backgroundColor = "yellow";

            //gg.__ContentTable_css_td.hover.style.textDecoration = "underline";

            //gg.__ContentTable_css_td.style.fontWeight = "bold";
            //gg.__ContentTable_css_td.style.paddingTop = "1em";
            //gg.__ContentTable_css_td.style.paddingBottom = "1em";
            #endregion

            //var tdf = gg.__ContentTable.css[IHTMLElement.HTMLElementEnum.tr].nthChild[1];

            //// { selectorText = >:nth-child(2) } 
            //// { selectorText = >:nth-child(2), parent = >tr } 
            //Console.WriteLine(new { tdf.selectorText, parent = tdf.parent.selectorText });

            //tdf.style.paddingLeft = "3em";

            //.FirstOfType<>();

            // 
            //var radio = IStyleSheet.all["input[type='radio']"][x => x.name == "group1"];
            //var radio = IStyleSheet.all["input[type='radio']"]["[name='group1']"];

            gg.__ContentTable.css[IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr]
                .first[IHTMLElement.HTMLElementEnum.td].style.backgroundColor = "red";


            gg.__ContentTable.css
                [IHTMLElement.HTMLElementEnum.tbody]
                [IHTMLElement.HTMLElementEnum.tr]
                .first.child
                [IHTMLElement.HTMLElementEnum.div]
                [IHTMLElement.HTMLElementEnum.span].style.paddingLeft = "3em";


            //// hide Tag?
            //gg.__ColumnsTable.css
            // [IHTMLElement.HTMLElementEnum.tbody]
            // [IHTMLElement.HTMLElementEnum.tr]
            // [IHTMLElement.HTMLElementEnum.td]
            // [3].style.display = IStyle.DisplayEnum.none;

            //gg.__ContentTable.css
            // [IHTMLElement.HTMLElementEnum.tbody]
            // [IHTMLElement.HTMLElementEnum.tr]
            // [IHTMLElement.HTMLElementEnum.td]
            // [3].style.display = IStyle.DisplayEnum.none;

        }

    }
}
