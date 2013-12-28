using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    public static class DataGridViewExtensions
    {
        // moving code from "Y:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs"

        public static IEnumerable<DataGridViewRow> AsEnumerable(this DataGridViewSelectedRowCollection c)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView\DataGridViewSelectedRowCollection.cs
            return Enumerable.Range(0, c.Count).Select(k => c[k]);
        }

        public static IEnumerable<DataGridViewRow> AsEnumerable(this DataGridViewRowCollection r)
        {
            return Enumerable.Range(0, r.Count).Select(k => r[k]);
        }
    }
}
