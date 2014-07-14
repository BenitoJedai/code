using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using System.Data;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLTableElement.idl

    /// <summary>
    /// http://developer.mozilla.org/en/docs/Traversing_an_HTML_table_with_JavaScript_and_DOM_Interfaces
    /// </summary>
    [Script(InternalConstructor = true)]
    public class IHTMLTable : IHTMLElement
    {
        public int cellPadding;
        public int cellSpacing;
        public int border;
        public string align;

        #region ctor
        public IHTMLTable()
        {
        }


        static IHTMLTable InternalConstructor()
        {
            return (IHTMLTable)(object)new IHTMLElement(HTMLElementEnum.table);
        }


        #endregion

        [Script(DefineAsStatic = true)]
        public IHTMLTableBody AddBody()
        {
            IHTMLTableBody r = new IHTMLTableBody();

            this.appendChild(r);

            return r;
        }


        // when will c# learn extension operators?
        public static implicit operator IHTMLTable(DataTable data)
        {
            // X:\jsc.svn\examples\javascript\Test\TestManyTableRowsFromDataTable\TestManyTableRowsFromDataTable\Application.cs

            var table = new IHTMLTable { };


            //table.AttachToDocument();
            var tbody = table.AddBody();

            //tbody.css.odd.style.backgroundColor = "gray";
            //var count = 10000;

            var RowCount = data.Rows.Count;
            var ColumnCount = data.Columns.Count;

            for (int i = 0; i < RowCount; i++)
            {
                var tr = tbody.AddRow();

                var SourceRow = data.Rows[i];

                for (int ci = 0; ci < ColumnCount; ci++)
                {
                    var td = tr.AddColumn();

                    td.innerText = (string)SourceRow[ci];

                }


            }


            return table;
        }
    }
}

namespace ScriptCoreLib.JavaScript.Extensions
{
    [Script]
    public static class DataTableToHTMLTableExtensions
    {
        public static IHTMLTable AttachToDocument(this DataTable table)
        {
            IHTMLTable table2 = table;

            table2.AttachToDocument();

            return table2;
        }
    }
}