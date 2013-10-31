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
        public object InternalDataSourceSync;

        public object DataSource
        {
            get
            {
                return InternalDataSource;
            }
            set
            {
                var CurrentDataSourceSync = new object();
                InternalDataSourceSync = CurrentDataSourceSync;

                this.InternalDataSource = value;

                this.Rows.Clear();

                if (value == null)
                {
                    while (this.Columns.Count > 0)
                        this.Columns.RemoveAt(this.Columns.Count - 1);

                    return;
                }

                #region DataTable
                var SourceDataTable = value as DataTable;
                if (SourceDataTable != null)
                {
                    // now what?

                    // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableToJavascript\TestDataTableToJavascript\ApplicationControl.cs
                    // http://stackoverflow.com/questions/6902269/moving-data-from-datatable-to-datagridview-in-c-sharp


                    while (this.Columns.Count > SourceDataTable.Columns.Count)
                        this.Columns.RemoveAt(this.Columns.Count - 1);


                    var cIndex = 0;
                    foreach (DataColumn item in SourceDataTable.Columns)
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

                    foreach (DataRow item in SourceDataTable.Rows)
                    {
                        var r = new DataGridViewRow();

                        foreach (DataColumn c in SourceDataTable.Columns)
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

                //Console.WriteLine("add CellValueChanged ");

                var NewRow = default(DataRow);

         

                this.CellValueChanged +=
                    (_s, _e) =>
                    {
                        // faulty
                        // X:\jsc.svn\examples\javascript\CSVAssetAsGridExperiment\CSVAssetAsGridExperiment\Application.cs
                        //return;

                        if (this.InternalDataSourceSync != CurrentDataSourceSync)
                            return;

                        Console.WriteLine("DataSource at CellValueChanged " + new { _e.RowIndex, NewRow, SourceDataTable.Rows.Count });


                        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\ApplicationWebService.cs

                        //InternalRaiseCellBeginEdit { ColumnIndex = 1, Index = 32 }
                        // view-source:28036
                        //TableNewRow { RowIndexOf = -1 }
                        // view-source:28036
                        //at CellValueChanged

                        var CurrentRow = NewRow;

                        if (_e.RowIndex >= 0)
                            if (_e.RowIndex < SourceDataTable.Rows.Count)
                            {
                                NewRow = SourceDataTable.Rows[_e.RowIndex];
                            }

                        //                        script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataRow.set_Item(System.Int32, System.Object)]
                        //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
                        //script: error JSC1000: error at ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__DataGridView+<>c__DisplayClass3.<set_DataSource>b__1,
                        // assembly: X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\bin\Release\ScriptCoreLib.Windows.Forms.dll

                        Console.WriteLine("DataSource at CellValueChanged DataTable");
                        NewRow[_e.ColumnIndex] = this[_e.ColumnIndex, _e.RowIndex].Value;
                    };

                this.UserAddedRow +=
                    (_s, _e) =>
                    {
                        // faulty
                        // X:\jsc.svn\examples\javascript\CSVAssetAsGridExperiment\CSVAssetAsGridExperiment\Application.cs
                        //return;

                        if (this.InternalDataSourceSync != CurrentDataSourceSync)
                            return;

                        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\ApplicationWebService.cs
                        Console.WriteLine("DataSource UserAddedRow" + new { SourceDataTable.Rows.Count });
                        NewRow = SourceDataTable.NewRow();

                        foreach (DataColumn item in SourceDataTable.Columns)
                        {
                            // user cannot enter null can he
                            NewRow[item] = "";
                        }


                        // argh we need to add it!
                        SourceDataTable.Rows.Add(NewRow);


                        Console.WriteLine("DataSource UserAddedRow" + new { RowIndex = SourceDataTable.Rows.IndexOf(NewRow), SourceDataTable.Rows.Count });

                    };



                if (DataSourceChanged != null)
                    DataSourceChanged(this, new EventArgs());

            }
        }
        #endregion



    }
}
