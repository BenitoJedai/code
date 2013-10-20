
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataRowCollection))]
    public class __DataRowCollection : __InternalDataCollectionBase
    {
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView.cs
        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableToJavascript\TestDataTableToJavascript\ApplicationWebService.cs

        public List<DataRow> InternalList = new List<DataRow>();

        public void Add(DataRow row)
        {
            this.InternalList.Add(row);
        }

        public void AddRange(DataRow[] row)
        {
            this.InternalList.AddRange(row);
        }


        public override int Count
        {
            get
            {
                return this.InternalList.Count();
            }
        }

        public override IEnumerable GetInternalList()
        {
            return InternalList;
        }


        public static implicit operator DataRowCollection(__DataRowCollection x)
        {
            return (DataRowCollection)(object)x;
        }
    }
}
