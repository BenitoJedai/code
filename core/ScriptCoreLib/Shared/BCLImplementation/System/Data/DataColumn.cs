using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    // http://referencesource.microsoft.com/#System.Data/data/System/Data/DataColumn.cs

    [Script(Implements = typeof(global::System.Data.DataColumn))]
    public class __DataColumn : __MarshalByValueComponent
    {
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView.cs

        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\ApplicationWebService.cs
        public bool ReadOnly { get; set; }

        public string ColumnName { get; set; }
    }
}
