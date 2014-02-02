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
using TestManyTableRowsFromDataTable;
using TestManyTableRowsFromDataTable.Design;
using TestManyTableRowsFromDataTable.HTML.Pages;

namespace TestManyTableRowsFromDataTable
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
            // X:\jsc.svn\examples\javascript\Test\TestManyTableRows\TestManyTableRows\Application.cs
            (page.title.css | page.head.css).style.display = IStyle.DisplayEnum.block;


            // shall our .htm elements start recording their .ctor stopwatch yet?
            var s = Stopwatch.StartNew();


            var table = new DataTable();

            //var table = new IHTMLTable { border = 1 };

            var c0 = table.Columns.Add("Column1");
            var c1 = table.Columns.Add("Column2");


            //table.AttachToDocument();
            //var tbody = table.AddBody();

            //tbody.css.odd.style.backgroundColor = "gray";
            var count = 40000;

            for (int i = 0; i < count; i++)
            {
                var tr = table.NewRow();

                tr[0] = new { i }.ToString();
                tr[1] = new { count, s.ElapsedMilliseconds }.ToString();

                table.Rows.Add(tr);
            }

            // attach to DOM only now?
            //table.AttachToDocument();

            // 10000: 299ms
            // 10000: 319ms

            s.Stop();

            (count + ": " + s.ElapsedMilliseconds + "ms").ToDocumentTitle();


            var ss = Stopwatch.StartNew();

            // 10000: 308ms data to html table: 6684ms

            // haha, what does it do for 6 secs?
            table.AttachToDocument();

            //Native.css[IHTMLElement.HTMLElementEnum.tbody].children.odd.style.background = "cyan";

            // html the :root should default from descendant to children?
            Native.css.descendant
                [IHTMLElement.HTMLElementEnum.tbody]
                [IHTMLElement.HTMLElementEnum.tr].odd.style.background = "cyan";

            //page.title.css.after.contentXAttribute = new XAttribute("ss", " data to html table: " + ss.ElapsedMilliseconds + "ms");

            // 10000: 21ms data to html table: 126ms
            // 10000: 24ms data to html table: 119ms
            // 40000: 138ms data to html table: 418ms
            // 40000: 285ms data to html table: 733ms
            (count + ": " + s.ElapsedMilliseconds + "ms data to html table: " + ss.ElapsedMilliseconds + "ms").ToDocumentTitle();


        }

    }
}
