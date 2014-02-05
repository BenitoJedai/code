#define xFBINDING
#define FPRERENDER

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
using ScriptCoreLib.JavaScript.DOM.HTML;


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
                // if there is an animation in flight,
                // will we freeze it ?


                InternalSetDataSource(value);


            }
        }

        public Queue<IHTMLTableRow> InternalPrerenderZeroRows = new Queue<IHTMLTableRow>();
        public Queue<IHTMLTableRow> InternalPrerenderRows = new Queue<IHTMLTableRow>();

        private void InternalSetDataSource(object value)
        {
            // 16241ms event: dataGridView1 set DataSource { ColumnIndex = 30, SourceRowIndex = 8, ElapsedMilliseconds = 1575, a = 175 } 

            //Console.WriteLine(
            //    "event: "
            //    + this.Name
            //    + " set DataSource enter "
            //    + new
            //    {

            //    }
            // );

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


            this.InternalRows.Clear();

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


            //Console.WriteLine(
            //    "event: "
            //    + this.Name
            //    + " set DataSource clear done "
            //    + new
            //    {

            //    }
            // );

            // now what?

            // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableToJavascript\TestDataTableToJavascript\ApplicationControl.cs
            // http://stackoverflow.com/questions/6902269/moving-data-from-datatable-to-datagridview-in-c-sharp

            //Console.WriteLine(
            //    new { Name, stopwatch.ElapsedMilliseconds }
            //    + " before Columns"
            // );

            var SourceDataTableColumnCount = SourceDataTable.Columns.Count;
            var cstopwatch = Stopwatch.StartNew();


            #region Columns
            while (this.Columns.Count > SourceDataTableColumnCount)
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



            cstopwatch.Stop();
            // 4141ms event: dataGridView1 set DataSource columns { SourceDataTableColumnCount = 8, ElapsedMilliseconds = 999 } 
            Console.WriteLine(
                    "event: "
                    + this.Name
                    + " set DataSource columns "
                    + new
                    {
                        SourceDataTableColumnCount,
                        cstopwatch.ElapsedMilliseconds,
                    }
            );

            // show the columns and continue in a moment
            Native.window.requestAnimationFrame += delegate
            {
                //return;

                #region PrerenderStopwatch
                var PrerenderStopwatch = Stopwatch.StartNew();

                // X:\jsc.svn\examples\javascript\Test\TestManyTableRows\TestManyTableRows\Application.cs

                var SourceDataTableRowCount = SourceDataTable.Rows.Count;

#if FPRERENDER
                for (int i = 0; i < SourceDataTableRowCount; i++)
                {
                    var DataBoundItem = SourceDataTable.Rows[i];

                    // what the hell. safari and chrome show ok.
                    // ie and ff show empty cells. why?

                    #region prerender
                    // 2096ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 1793, a = 1.7947947947947949 }
                    // add a placeholder

                    // http://stackoverflow.com/questions/3076708/can-we-have-multiple-tbody-in-same-table
                    // http://www.w3.org/TR/html401/struct/tables.html#h-11.3.1

                    //if (NewTBody == null)
                    //{
                    //    NewTBody = this.__ContentTable.AddBody();
                    //    //NewTBody.css[IHTMLElement.HTMLElementEnum.tr].children.style.backgroundColor = "cyan";
                    //}

                    //var tr = NewTBody.AddRow();
                    //var tr = __ContentTableBody.AddRow();

                    var __tr = new IHTMLTableRow { };
                    __RowsTableBody.insertBefore(__tr, InternalNewRow.InternalZeroColumnTableRow);

                    var InternalTableColumn = __tr.AddColumn();

                    var tr = new IHTMLTableRow { };
                    __ContentTableBody.insertBefore(tr, InternalNewRow.InternalTableRow);

                    InternalPrerenderZeroRows.Enqueue(__tr);
                    InternalPrerenderRows.Enqueue(tr);

                    // http://www.w3.org/TR/html5/tabular-data.html#the-table-element

                    var PrerenderData = (PrerenderStopwatch.ElapsedMilliseconds < 50);

                    for (int ic = 0; ic < SourceDataTableColumnCount; ic++)
                    {
                        var data = DataBoundItem[ic];

                        var td = tr.AddColumn();
                        // http://www.w3schools.com/cssref/css3_pr_column-span.asp

                        // 4760ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 9999, ElapsedMilliseconds = 4092, a = 0.4092 } 



                        if (PrerenderData)
                        {
                            // X:\jsc.svn\examples\javascript\Test\TestManyTableRows\TestManyTableRows\Application.cs
                            // we need a special div to play relative 
                            var td_div = new IHTMLDiv { }.AttachTo(td);

                            td_div.setAttribute("data", data);
                        }
                        else
                        {
                            td.colSpan = SourceDataTableColumnCount;
                            break;
                        }
                    }

                    #endregion



                    // 6881ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 6590, a = 6.596596596596597 } 

                }
#endif
                PrerenderStopwatch.Stop();
                if (PrerenderStopwatch.ElapsedMilliseconds > 30)
                    Console.WriteLine(
                        "event: "
                        + this.Name
                        + " set DataSource prerender "
                        + new
                        {
                            SourceDataTableColumnCount,
                            SourceDataTableRowCount,
                            PrerenderStopwatch.ElapsedMilliseconds,
                        }
                );

                #endregion


                var AddRowsStopwatch = Stopwatch.StartNew();

                #region Rows


                var AddRowAction = Enumerable.Range(0, SourceDataTableRowCount).Select(
                    i =>
                        new Action(
                            delegate
                            {
                                var RowStopwatch = Stopwatch.StartNew();

                                var DataBoundItem = SourceDataTable.Rows[i];

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


                                this.Rows.Add(r);


                                if (RowStopwatch.ElapsedMilliseconds > 30)
                                {
                                    // report slowdowns only.

                                    Console.WriteLine(
                                        new { Name }
                                        + " InternalSetDataSource add row "
                                        + new { i, RowStopwatch.ElapsedMilliseconds }
                                     );
                                }

                            }
                        )
                    ).GetEnumerator();



                while (AddRowAction.MoveNext())
                {
                    AddRowAction.Current();

                    // 3145ms { Name = dataGridView1 } InternalSetDataSource add row { i = 7, ElapsedMilliseconds = 13 } view-source:35829
                    if (AddRowsStopwatch.ElapsedMilliseconds > 100)
                    {
                        // continue later?
                        break;
                    }
                }
                #endregion

                AddRowsStopwatch.Stop();

                if (AddRowsStopwatch.ElapsedMilliseconds > 30)
                    Console.WriteLine(
                          "event: "
                          + this.Name
                          + " set DataSource add rows "
                          + new
                          {
                              SourceDataTableColumnCount,
                              SourceDataTableRowCount,
                              AddRowsStopwatch.ElapsedMilliseconds,
                          }
                  );


                //stopwatch.Restart();

#if FBINDING
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

                        // who changed it?

                        Console.WriteLine(
                            "DataSource at CellValueChanged " + new
                            {
                                _e.RowIndex,
                                NewRow,
                                SourceDataTable.Rows.Count
                            }
                        );


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

#endif

                // do we still have time for this?
                // 23230ms event: { Name = dataGridView1 } autoresize done { ElapsedMilliseconds = 2761, Count = 6 } 
                // 23229ms got offsetWidth in { ElapsedMilliseconds = 397 } 


                // 3644ms event: { Name = dataGridView1 } autoresize done { ElapsedMilliseconds = 2275, Count = 6 } 
                // do we want to autoresize if it takes up to 2500 ms?
                // InternalAutoResizeAll();

                //stopwatch.Stop();

                // 111485ms { Form = ExampleForm, Name = dataGridView1 } exit InternalSetDataSource{ ElapsedMilliseconds = 2775 } 

                // 2281ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 1908, a = 1.90990990990991 } 
                //2136ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 1788, a = 1.7897897897897899 } 
                // 750ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 435, a = 0.43543543543543545 }
                // 9710ms event: dataGridView1 set DataSource { SourceDataTableColumnCount = 6, SourceDataTableRowCount = 1000, ElapsedMilliseconds = 1333 } 

                // 079ms event: dataGridView1 set DataSource { SourceDataTableColumnCount = 6, SourceDataTableRowCount = 100, ElapsedMilliseconds = 564 } 
                Console.WriteLine(
                    "event: "
                    // what if there is no form?
                    //+ this.FindForm().Name + "." 
                    // what if there is no name?
                    + this.Name
                    + " set DataSource almost done "
                    + new
                    {
                        SourceDataTableColumnCount,
                        SourceDataTableRowCount,
                        stopwatch.ElapsedMilliseconds
                    }
                 );

                // 4069ms { Form = Form1, Name = dataGridView1 } exit InternalSetDataSource{ ElapsedMilliseconds = 2027 } 


                // 371ms event: dataGridView1 set DataSource { SourceDataTableColumnCount = 6, SourceDataTableRowCount = 32, ElapsedMilliseconds = 188 } 
                // 1182ms event: dataGridView1 set DataSource { SourceDataTableColumnCount = 6, SourceDataTableRowCount = 1000, ElapsedMilliseconds = 901 }


                Action yield = null;

                yield = delegate
                {
                    var CStopwatch = Stopwatch.StartNew();

                    while (AddRowAction.MoveNext())
                    {
                        AddRowAction.Current();

                        if (CStopwatch.ElapsedMilliseconds > 300)
                        {

                            break;
                        }
                    }

                    if (CStopwatch.ElapsedMilliseconds > 300)
                    {
                        Console.WriteLine(
                            "event: "
                            // what if there is no form?
                            //+ this.FindForm().Name + "." 
                            // what if there is no name?
                            + this.Name
                            + " set DataSource yield "
                            + new
                            {
                                //SourceDataTableColumnCount,

                                InternalPrerenderRows.Count

                                ,

                                stopwatch.ElapsedMilliseconds
                            }
                         );
                        Native.window.requestAnimationFrame += yield;
                        return;
                    }


                    //584ms event: dataGridView1 set DataSource { SourceDataTableColumnCount = 6, SourceDataTableRowCount = 1000, ElapsedMilliseconds = 313 } 


                    InternalDataSourceBusy = false;

                    var sReposition0 = Stopwatch.StartNew();

                    // bulk insert done. rorder?
                    // Reposition
                    // do we even allow column resize?
                    if (this.Columns.Count > 0)
                        this.Columns[0].Width = this.Columns[0].Width;

                    Console.WriteLine(
                          this.Name
                          + " set DataSource sReposition0 "
                          + new
                          {
                              SourceDataTableColumnCount,
                              sReposition0.ElapsedMilliseconds
                          }
                       );


                    InternalAutoSizeWhenFill();



                    new XAttribute(
                        "Stopwatch",

                        new
                        {
                            columns = cstopwatch.ElapsedMilliseconds,
                            prerender = PrerenderStopwatch.ElapsedMilliseconds,
                            rows = AddRowsStopwatch.ElapsedMilliseconds,

                            total = stopwatch.ElapsedMilliseconds
                        }.ToString()
                    ).AttachTo(this.HTMLTargetRef);



                    Console.WriteLine(
                          "event: "
                        // what if there is no form?
                        //+ this.FindForm().Name + "." 
                        // what if there is no name?
                          + this.Name
                          + " DataSourceChanged "
                          + new
                          {
                              SourceDataTableColumnCount,
                              SourceDataTableRowCount,
                              stopwatch.ElapsedMilliseconds
                          }
                       );


                    if (DataSourceChanged != null)
                        DataSourceChanged(this, new EventArgs());
                };

                Native.window.requestAnimationFrame += yield;


            };

        }



    }
}
