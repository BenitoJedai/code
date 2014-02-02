using ScriptCoreLib.Shared.BCLImplementation.System.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.Extensions;


namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    partial class __DataGridView
    {
        // tested by
        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\ApplicationWebService.cs

        public event EventHandler DataSourceChanged;

        // X:\jsc.svn\examples\javascript\Forms\FormsDataSet\FormsDataSet\ApplicationControl.cs
        public string InternalDataMember;
        public string DataMember
        {
            get
            {

                return InternalDataMember;
            }
            set
            {
                this.InternalDataMember = value;

                this.InternalSetDataSource(
                    this.InternalDataSource
                );
            }
        }


        public object InternalDataSource;
        public object InternalDataSourceSync;

        public bool InternalDataSourceBusy;

        public object DataSource
        {
            get
            {
                return InternalDataSource;
            }
            set
            {
                InternalSetDataSource(value);

                Native.window.requestAnimationFrame += delegate
                {


                    if (DataSourceChanged != null)
                        DataSourceChanged(this, new EventArgs());

                };
            }
        }

        private void InternalSetDataSource(object value)
        {
            InternalDataSourceBusy = true;
            var stopwatch = Stopwatch.StartNew();

            //Console.WriteLine(
            //    new { Name, stopwatch.ElapsedMilliseconds }
            //    + " enter InternalSetDataSource"
            // );

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

            var SourceDataTable = value as DataTable;

            #region InternalDataMember
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131107/assetslibrary
            var SourceDataSet = value as DataSet;
            if (SourceDataSet != null)
            {
                foreach (DataTable item in SourceDataSet.Tables)
                {
                    if (item.TableName == this.InternalDataMember)
                    {
                        SourceDataTable = item;
                    }
                }
            }
            #endregion


            if (SourceDataTable == null)
            {
                return;
            }

            // now what?

            // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableToJavascript\TestDataTableToJavascript\ApplicationControl.cs
            // http://stackoverflow.com/questions/6902269/moving-data-from-datatable-to-datagridview-in-c-sharp

            //Console.WriteLine(
            //    new { Name, stopwatch.ElapsedMilliseconds }
            //    + " before Columns"
            // );


            #region Columns
            while (this.Columns.Count > SourceDataTable.Columns.Count)
                this.Columns.RemoveAt(this.Columns.Count - 1);


            var ColumnIndex = 0;
            foreach (DataColumn item in SourceDataTable.Columns)
            {

                if (ColumnIndex < this.Columns.Count)
                {
                }
                else
                {
                    var ColumnStopwatch = Stopwatch.StartNew();

                    this.Columns.Add(
                        new DataGridViewColumn
                        {
                        }
                    );


                    // 793192ms { Name = dataGridView2, cIndex = 1 } InternalSetDataSource a Column done at { ElapsedMilliseconds = 935 } 

                    // Console.WriteLine(
                    //   new { Name, cIndex = ColumnIndex }
                    //   + " InternalSetDataSource a Column done at "
                    //   + new { ColumnStopwatch.ElapsedMilliseconds }
                    //);
                }

                // X:\jsc.internal.svn\core\com.abstractatech.my.business\com.abstractatech.my.business\Application.cs
                this.Columns[ColumnIndex].Name = item.ColumnName;
                this.Columns[ColumnIndex].HeaderText = item.ColumnName;

                this.Columns[ColumnIndex].ReadOnly = item.ReadOnly;

                ColumnIndex++;




            }

            #endregion


            #region Rows
            foreach (DataRow DataBoundItem in SourceDataTable.Rows)
            {
                var RowStopwatch = Stopwatch.StartNew();

                var r = new __DataGridViewRow
                {

                    DataBoundItem = new __DataRowView { Row = DataBoundItem }
                };


                foreach (DataColumn c in SourceDataTable.Columns)
                {
                    var cc = new DataGridViewTextBoxCell
                    {
                        // two way binding?
                        //ReadOnly = true,

                        Value = DataBoundItem[c]
                    };


                    r.Cells.Add(cc);
                }

                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140130-build-server/trace
                if (RowStopwatch.ElapsedMilliseconds > 10)
                {
                    // report slowdowns only.

                    Console.WriteLine(
                        new { Name }
                        + " InternalSetDataSource a Row done at "
                        + new { RowStopwatch.ElapsedMilliseconds }
                     );
                }

                this.Rows.Add(r);
            }
            #endregion

            // 5908ms { Name = dataGridView1 } InternalSetDataSource Rows done at { ElapsedMilliseconds = 669 } 



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
                    var xColumnIndex = SourceDataTable.Columns.IndexOf(x.Column);
                    var RowIndex = SourceDataTable.Rows.IndexOf(x.Row);

                    if (this[xColumnIndex, RowIndex].Value == x.ProposedValue)
                        return;

                    this[xColumnIndex, RowIndex].Value = x.ProposedValue;
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

                    if (CurrentRow == null)
                    {
                        // not data bound??

                        return;
                    }

                    var c = this[_e.ColumnIndex, _e.RowIndex];

                    if (c == null)
                    {
                        // X:\jsc.internal.svn\core\com.abstractatech.my.business\com.abstractatech.my.business\Application.cs
                        // ??
                        return;
                    }

                    if (CurrentRow[_e.ColumnIndex] == c.Value)
                        return;

                    Console.WriteLine("DataSource at CellValueChanged DataTable");
                    CurrentRow[_e.ColumnIndex] = this[_e.ColumnIndex, _e.RowIndex].Value;
                };
            #endregion



            #region TableNewRow
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
            #endregion


            #region UserAddedRow
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
            #endregion


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


            //886ms { Name = dataGridView1 } exit InternalSetDataSource{ ElapsedMilliseconds = 369 } 

            InternalAutoResizeAll();

            InternalDataSourceBusy = false;
            stopwatch.Stop();

            // 111485ms { Form = ExampleForm, Name = dataGridView1 } exit InternalSetDataSource{ ElapsedMilliseconds = 2775 } 

            var old = new { Console.BackgroundColor };
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                "event: " +
                this.FindForm().Name + "." + this.Name
                + " set DataSource"
                + new { stopwatch.ElapsedMilliseconds }
             );

            // 4069ms { Form = Form1, Name = dataGridView1 } exit InternalSetDataSource{ ElapsedMilliseconds = 2027 } 

            Console.BackgroundColor = old.BackgroundColor;


            new XAttribute(
               "Stopwatch",
               new { stopwatch.ElapsedMilliseconds }.ToString()
           ).AttachTo(this.HTMLTargetRef);

        }



    }
}
