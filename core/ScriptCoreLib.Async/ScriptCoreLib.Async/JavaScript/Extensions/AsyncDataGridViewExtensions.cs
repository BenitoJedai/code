using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.Extensions
{
    public static class AsyncDataGridViewExtensions
    {
        public static async Task<DataGridView> AttachDataGridViewToDocument(this Task<DataTable> data)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\appengine\DataGridWithHeaders\DataGridWithHeaders\Application.cs

            var x = await data;

            var grid = new DataGridView();


            grid.DataSource = x;

            grid.AttachControlToDocument();

            Native.document.title = x.TableName;

            return grid;
        }
    }
}
