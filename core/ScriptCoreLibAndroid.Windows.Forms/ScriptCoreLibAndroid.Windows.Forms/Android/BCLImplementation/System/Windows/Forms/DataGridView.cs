using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridView))]
    internal class __DataGridView : __Control
    {
        //        Implementation not found for type import :
        //type: System.Windows.Forms.DataGridView
        //method: Void set_DataSource(System.Object)

        // tested by
        // X:\jsc.svn\examples\javascript\forms\FormsConfiguredAtWebService\FormsConfiguredAtWebService\ApplicationWebService.cs

        public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode { get; set; }
        public object DataSource { get; set; }
    }
}
