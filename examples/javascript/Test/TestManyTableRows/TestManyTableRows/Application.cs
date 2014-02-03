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
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestManyTableRows;
using TestManyTableRows.Design;
using TestManyTableRows.HTML.Pages;

namespace TestManyTableRows
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
            // X:\jsc.svn\examples\javascript\Test\TestManyTableRowsFromDataTable\TestManyTableRowsFromDataTable\Application.cs
            (page.title.css | page.head.css).style.display = IStyle.DisplayEnum.block;

            // shall our .htm elements start recording their .ctor stopwatch yet?
            var s = Stopwatch.StartNew();

            var table = new IHTMLTable { border = 1 };


            //table.AttachToDocument();
            var tbody = table.AddBody();

            tbody.css.odd.style.backgroundColor = "gray";
            tbody.css[IHTMLElement.HTMLElementEnum.tr].hover.style.textDecoration = "underline";

            // http://www.joepettersson.com/demo/the-outline-property/

            var cssf = tbody.css[IHTMLElement.HTMLElementEnum.tr].children.focus;

            cssf.style.color = "red";

            //var cssf = tbody.css[IHTMLElement.HTMLElementEnum.tr][IHTMLElement.HTMLElementEnum.div].outli;

            //tbody.onclick +=
            //    delegate
            //    {
            //        ("onclick").ToDocumentTitle();

            //    };

            var count = 10000;

            for (int i = 0; i < count; i++)
            {
                var tr = tbody.AddRow();
                var td = tr.AddColumn();

                //var label = new IHTMLLabel { new { i } }.AttachTo(td);
                var label = new IHTMLAnchor { new { i } }.AttachTo(td);

                label.href = "#";

                //label.onfocus +=
                //    delegate
                //    {

                //    };

                //td.innerText = new { i }.ToString();

                // If you use label markup, as recommendable for usability and accessibility, e.g.
                // http://www.coderanch.com/t/597537/HTML-CSS-JavaScript/td-focus
                // As it is not a focusable element, no, it does not. And because focus events do not bubble, there's no way that a focus event can trigger for a non-focusable element.
                //label.tabIndex = i;

            }

            // attach to DOM only now?
            table.AttachToDocument();

            // 100: 4ms
            // 100: 6ms
            // 1000: 18ms
            // 10000: 109ms
            // 10000: 116ms
            new { count, s.ElapsedMilliseconds, cssf }.ToString().ToDocumentTitle();
        }

    }
}

