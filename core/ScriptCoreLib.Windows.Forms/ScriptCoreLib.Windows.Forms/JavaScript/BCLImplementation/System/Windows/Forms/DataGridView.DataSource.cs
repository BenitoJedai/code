using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    partial class __DataGridView
    {
        // tested by
        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\ApplicationWebService.cs

        #region DataSource
        public event EventHandler DataSourceChanged;

        public object InternalDataSource;
        public object DataSource
        {
            get
            {
                return InternalDataSource;
            }
            set
            {
                this.InternalDataSource = value;

                #region DataTable
                var DataTable = value as DataTable;
                if (DataTable != null)
                {
                    // now what?

                    // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableToJavascript\TestDataTableToJavascript\ApplicationControl.cs
                    // http://stackoverflow.com/questions/6902269/moving-data-from-datatable-to-datagridview-in-c-sharp

                    this.Rows.Clear();

                    while (this.Columns.Count > DataTable.Columns.Count)
                        this.Columns.RemoveAt(this.Columns.Count - 1);


                    var cIndex = 0;
                    foreach (DataColumn item in DataTable.Columns)
                    {
                        if (cIndex < this.Columns.Count)
                        {
                        }
                        else
                        {
                            this.Columns.Add(
                                new DataGridViewColumn
                                {
                                }
                            );
                        }

                        this.Columns[cIndex].HeaderText = item.ColumnName;
                        this.Columns[cIndex].ReadOnly = item.ReadOnly;

                        cIndex++;
                    }

                    foreach (DataRow item in DataTable.Rows)
                    {
                        var r = new DataGridViewRow();

                        foreach (DataColumn c in DataTable.Columns)
                        {
                            var cc = new DataGridViewTextBoxCell
                            {
                                // two way binding?
                                //ReadOnly = true,

                                Value = item[c]
                            };

                            r.Cells.Add(cc);
                        }

                        this.Rows.Add(r);
                    }

                }
                #endregion

                Console.WriteLine("add CellValueChanged ");

                this.CellValueChanged +=
                    (_s, _e) =>
                    {
                        Console.WriteLine("at CellValueChanged ");


                        if (this.DataSource != DataTable)
                            return;

                        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\ApplicationWebService.cs

                        var r = DataTable.Rows[_e.RowIndex];

//                        script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataRow.set_Item(System.Int32, System.Object)]
//script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
//script: error JSC1000: error at ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__DataGridView+<>c__DisplayClass3.<set_DataSource>b__1,
// assembly: X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\bin\Release\ScriptCoreLib.Windows.Forms.dll

                        Console.WriteLine("at CellValueChanged DataTable");
                        r[_e.ColumnIndex] = this[_e.ColumnIndex, _e.RowIndex].Value;
                    };
                this.UserAddedRow +=
                    (_s, _e) =>
                    {
                        if (this.DataSource != DataTable)
                            return;

                        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\ApplicationWebService.cs
                        var r = DataTable.NewRow();
                    };



                if (DataSourceChanged != null)
                    DataSourceChanged(this, new EventArgs());

            }
        }
        #endregion



    }
}
