using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataColumnCollection))]
    public class __DataColumnCollection : __InternalDataCollectionBase
    {
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView.cs
        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableToJavascript\TestDataTableToJavascript\ApplicationWebService.cs

        public List<DataColumn> InternalList = new List<DataColumn>();

        public void Add(DataColumn column)
        {
            this.InternalList.Add(column);
        }

        public void AddRange(DataColumn[] columns)
        {
            this.InternalList.AddRange(columns);
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


        public static implicit operator DataColumnCollection(__DataColumnCollection x)
        {
            return (DataColumnCollection)(object)x;
        }
    }
}
