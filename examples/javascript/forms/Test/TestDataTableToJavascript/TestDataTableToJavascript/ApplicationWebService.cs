using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestDataTableToJavascript
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public async Task<DataTable> GetQueryResultAsDataTable()
        {
            var table = new DataTable();

            var column = new DataColumn();
            column.ColumnName = "Column 1";

            var column2 = new DataColumn();
            column2.ColumnName = "Column 2";
            table.Columns.Add(column);
            table.Columns.Add(column2);

            var row = table.NewRow();
           
            row[column] = "test1";
            row[column2] = "test2";
            table.Rows.Add(row);

            return table;
        }

    }
}
