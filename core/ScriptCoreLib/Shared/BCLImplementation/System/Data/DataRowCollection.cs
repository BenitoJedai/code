
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
        public __DataTable InternalDataTable;

        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView.cs
        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableToJavascript\TestDataTableToJavascript\ApplicationWebService.cs

        public List<DataRow> InternalList = new List<DataRow>();

        public void Add(DataRow row)
        {
            this.InternalList.Add(row);

            // ? TableNewRow
            // X:\jsc.svn\examples\javascript\forms\Test\TestDynamicBindingSourceForDataTable\TestDynamicBindingSourceForDataTable\Application.cs

        }

        //script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataRowCollection.Add(System.Object[])]
        public DataRow Add(object[] x)
        {
            // X:\jsc.svn\examples\javascript\WebGL\WebGLGoldDropletTransactions\WebGLGoldDropletTransactions\Application.cs

            var row = this.InternalDataTable.NewRow();
            this.InternalList.Add(row);

            var i = -1;
            foreach (var item in x)
            {
                i++;

                var item_ToString = Convert.ToString(item);
                row[i] = item_ToString;
            }


            return row;
        }

        public void AddRange(DataRow[] row)
        {
            this.InternalList.AddRange(row);
        }

        public void Remove(DataRow row)
        {
            var i = IndexOf(row);

            RemoveAt(i);

        }
        public void RemoveAt(int i)
        {
            // X:\jsc.svn\examples\javascript\forms\FormsNICWithDataSource\FormsNICWithDataSource\ApplicationControl.cs

            var row = this.InternalList[i];

            this.InternalDataTable.RaiseRowDeleting(
                this.InternalDataTable,
                new DataRowChangeEventArgs(row, DataRowAction.Delete)
            );

            this.InternalList.Remove(row);


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

        //script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataRowCollection.IndexOf(System.Data.DataRow)]
        public int IndexOf(DataRow row)
        {
            return this.InternalList.IndexOf(row);
        }

        public DataRow this[int index]
        {
            get
            {
                return this.InternalList[index];
            }
        }

        public static implicit operator DataRowCollection(__DataRowCollection x)
        {
            return (DataRowCollection)(object)x;
        }
    }
}
