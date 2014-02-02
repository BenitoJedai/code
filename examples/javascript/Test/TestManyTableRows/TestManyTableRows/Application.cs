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
            var count = 10000;

            for (int i = 0; i < count; i++)
            {
                var tr = tbody.AddRow();
                var td = tr.AddColumn();

                td.innerText = new { i }.ToString();
            }

            // attach to DOM only now?
            table.AttachToDocument();

            // 100: 4ms
            // 100: 6ms
            // 1000: 18ms
            // 10000: 109ms
            // 10000: 116ms
            (count + ": " + s.ElapsedMilliseconds + "ms").ToDocumentTitle();
        }

    }
}
