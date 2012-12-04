using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    public static class DataGridViewExtensions
    {
        // moving code from "Y:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs"

        public static IEnumerable<DataGridViewRow> AsEnumerable(this DataGridViewRowCollection r)
        {
            return Enumerable.Range(0, r.Count).Select(k => r[k]);
        }
    }
}
