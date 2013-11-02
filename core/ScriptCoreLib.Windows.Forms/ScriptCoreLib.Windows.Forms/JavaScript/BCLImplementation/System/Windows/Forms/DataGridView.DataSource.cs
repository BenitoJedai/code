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
                InternalSetDataSource(value);

                if (DataSourceChanged != null)
                    DataSourceChanged(this, new EventArgs());

            }
        }

        private void InternalSetDataSource(object value)
        {
            // this cost 6h of work to fix the sync timing issue
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


            #region Value
            SourceDataTable.ColumnChanged +=
                (sender, x) =>
                {
                    if (this.InternalDataSourceSync != CurrentDataSourceSync)
                        return;

                    // data source is changing under us!
                    // keep up!

                    // script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataColumnCollection.IndexOf(System.Data.DataColumn)]
                    var ColumnIndex = SourceDataTable.Columns.IndexOf(x.Column);
                    var RowIndex = SourceDataTable.Rows.IndexOf(x.Row);

                    if (this[ColumnIndex, RowIndex].Value == x.ProposedValue)
                        return;

                    this[ColumnIndex, RowIndex].Value = x.ProposedValue;
                };





            this.CellValueChanged +=
                (_s, _e) =>
                {
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
                            CurrentRow = SourceDataTable.Rows[_e.RowIndex];
                        }

                    //                        script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataRow.set_Item(System.Int32, System.Object)]
                    //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
                    //script: error JSC1000: error at ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__DataGridView+<>c__DisplayClass3.<set_DataSource>b__1,
                    // assembly: X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\bin\Release\ScriptCoreLib.Windows.Forms.dll

                    if (CurrentRow[_e.ColumnIndex] == this[_e.ColumnIndex, _e.RowIndex].Value)
                        return;

                    Console.WriteLine("DataSource at CellValueChanged DataTable");
                    CurrentRow[_e.ColumnIndex] = this[_e.ColumnIndex, _e.RowIndex].Value;
                };
            #endregion




            SourceDataTable.TableNewRow +=
                (s, e) =>
                {
                    if (this.InternalDataSourceSync != CurrentDataSourceSync)
                        return;

                    this.InternalDataSourceSync = null;

                    // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridViewRowCollection.Add()]
                    this.Rows.Add();
                    this.InternalDataSourceSync = CurrentDataSourceSync;
                };

            this.UserAddedRow +=
                (_s, _e) =>
                {
                    if (this.InternalDataSourceSync != CurrentDataSourceSync)
                        return;

                    // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\ApplicationWebService.cs
                    Console.WriteLine("DataSource UserAddedRow" + new { SourceDataTable.Rows.Count });


                    this.InternalDataSourceSync = null;

                    NewRow = SourceDataTable.NewRow();
                    SourceDataTable.Rows.Add(NewRow);
                    this.InternalDataSourceSync = CurrentDataSourceSync;

                    foreach (DataColumn item in SourceDataTable.Columns)
                    {
                        // user cannot enter null can he
                        NewRow[item] = "";
                    }


                    // argh we need to add it!


                    Console.WriteLine("DataSource UserAddedRow" + new { RowIndex = SourceDataTable.Rows.IndexOf(NewRow), SourceDataTable.Rows.Count });


                };

            #region RemoveAt
            this.InternalBeforeUserDeletedRow +=
                (sender, e) =>
                {
                    if (this.InternalDataSourceSync != CurrentDataSourceSync)
                        return;

                    //RowDeleted { RowIndex = 2 }

                    this.InternalDataSourceSync = null;

                    SourceDataTable.Rows.RemoveAt(e.Row.Index);

                    this.InternalDataSourceSync = CurrentDataSourceSync;
                };


            // script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataTable.add_RowDeleted(System.Data.DataRowChangeEventHandler)]
            SourceDataTable.RowDeleting +=
                (sender, e) =>
                {
                    if (this.InternalDataSourceSync != CurrentDataSourceSync)
                        return;

                    var RowIndex = SourceDataTable.Rows.IndexOf(e.Row);

                    Console.WriteLine(
                        "RowDeleted " +
                        new { RowIndex }
                        );

                    this.InternalDataSourceSync = null;

                    this.Rows.RemoveAt(RowIndex);

                    this.InternalDataSourceSync = CurrentDataSourceSync;
                };
            #endregion

        }



    }
}
