#define FBINDING
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
using System.ComponentModel;


namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    partial class __DataGridView
    {
        // tested by
        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\ApplicationWebService.cs

        public event EventHandler DataSourceChanged;

        // X:\jsc.svn\examples\javascript\Forms\FormsDataSet\FormsDataSet\ApplicationControl.cs

        #region DataMember
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
        #endregion


        #region DataSource
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
        #endregion


        public Queue<IHTMLTableRow> InternalPrerenderZeroRows = new Queue<IHTMLTableRow>();
        public Queue<IHTMLTableRow> InternalPrerenderRows = new Queue<IHTMLTableRow>();


        public int InternalPreviousPosition = -1;
        public int InternalPosition
        {
            get
            {

                if (this.SelectedRows.Count == 0)
                {
                    // we lost active focus?
                    return InternalPreviousPosition;
                }


                InternalPreviousPosition = this.SelectedRows[0].Index;
                return InternalPreviousPosition;
            }
        }

        private void InternalSetDataSource(object value, object CurrentDataSourceSync = null)
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

            if (CurrentDataSourceSync == null)
                CurrentDataSourceSync = new object();
            InternalDataSourceSync = CurrentDataSourceSync;

            this.InternalDataSource = value;


            this.InternalRows.Clear();

            if (value == null)
            {
                // x:\jsc.svn\examples\javascript\forms\test\testsqljoin\testsqljoin\applicationcontrol.cs
                if (AutoGenerateColumns)
                {
                    while (this.Columns.Count > 0)
                        this.Columns.RemoveAt(this.Columns.Count - 1);

                }
                else
                {
                    // !! do not clear. we are likely to be rebound to same data
                }

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


            // 26:154ms InternalSetDataSource not implemented for <Namespace>.BindingSource 
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140409
            #region BindingSource
            var SourceBindingSource = value as __BindingSource;
            if (SourceBindingSource != null)
            {
                // x:\jsc.svn\examples\javascript\forms\test\testsqljoin\testsqljoin\applicationcontrol.cs

                //26:199ms InternalSetDataSource BindingSource { DataSource =  } view-source:37729
                //26:200ms InternalSetDataSource not implemented for <Namespace>.BindingSource view-source:37770
                //26:202ms __BindingSource EndInit 

                // we are being called first and then the designer sets the type.
                //this.myOtherDataSourceBindingSource.DataSource = typeof(FormsAutoSumGridSelection.Data.MyOtherDataSource);
                //this.myOtherDataSourceBindingSource.Position = 0;

                #region AtSourceBindingSourceDataSource
                Action AtSourceBindingSourceDataSource = delegate
                {
                    // X:\jsc.svn\examples\javascript\forms\FormsDualDataSource\FormsDualDataSource\ApplicationControl.cs

                    // once only?

                    //26:156ms  designer is still setting things up? view-source:37729
                    //26:158ms InternalSetDataSource BindingSource { Type = <Namespace>.BindingSource, DataSource = <Namespace>.MyOtherDataSource } view-source:37770
                    //26:161ms __BindingSource EndInit 

                    // X:\jsc.svn\examples\javascript\forms\FormsAutoSumGridSelection\FormsAutoSumGridSelection\Data\MyOtherDataSource.cs

                    //var isBindingSource = SourceBindingSource.DataSource;

                    //27:131ms  designer is still setting things up?
                    //27:132ms  designer is still setting things up? DataSourceChanged
                    //27:142ms InternalSetDataSource BindingSource { DataSource = <Namespace>.NavigationOrdersNavigateBindingSource }

                    //Console.WriteLine("InternalSetDataSource BindingSource " + new
                    //{
                    //    //Type = SourceBindingSource.DataSource.GetType(),
                    //    SourceBindingSource.DataSource
                    //});

                    object SourceBindingSource_DataSource_asDataTable = SourceBindingSource.ActivatedDataSource as DataTable;


                    //Console.WriteLine(
                    //    new { SourceBindingSource_DataSource_asDataTable }
                    //    );

                    // X:\jsc.svn\examples\javascript\forms\FormsDualDataSource\FormsDualDataSource\ApplicationControl.cs


                    if (SourceBindingSource_DataSource_asDataTable == null)
                    {
                        // not set by the designer?

                        //    #region asType
                        //    // tested by?
                        //    var asType = SourceBindingSource.DataSource as Type;
                        //    if (asType != null)
                        //    {
                        //        // 26:152ms InternalSetDataSource BindingSource { Type = <Namespace>.Type, DataSource = <Namespace>.MyDataSource } 
                        //        // GenericObjectDataSource!
                        //        // are we calling the ctor?
                        //        var newT = Activator.CreateInstance(asType);

                        //        Console.WriteLine(new { newT });
                        //        // 26:149ms { newT = <Namespace>.MyDataSource } 

                        var asBindingSource = SourceBindingSource.ActivatedDataSource as __BindingSource;
                        if (asBindingSource != null)
                        {
                            SourceBindingSource_DataSource_asDataTable = asBindingSource.ActivatedDataSource as DataTable;

                        }
                        //}
                        //#endregion
                    }

                    //Console.WriteLine(new { MyDataSource_DataSource = SourceBindingSource_DataSource_asDataTable });

                    if (SourceBindingSource_DataSource_asDataTable == null)
                        return;


                    #region DoSyncPosition
                    Action DoSyncPosition = delegate
                    {
                        //Console.WriteLine(" we can sync the selection!");

                        // should the grid be destroying the selection on blur or keep it actually?
                        this.SelectionChanged +=
                            delegate
                            {
                                // this methods is defined too early?

                                var __SourceBindingSource_DataSource_asDataTable = SourceBindingSource_DataSource_asDataTable as DataTable;

                                var isCurrentDataSourceSync = CurrentDataSourceSync == InternalDataSourceSync;

                                // 30:49422ms SelectionChanged { isCurrentDataSourceSync = true, InternalPosition = 3, Count = 4 }

                                // message: "Cannot read property 'hRIABq5zDzqOgooWgQkAYQ' of null"


                                Console.WriteLine("SelectionChanged " + new
                                {
                                    isCurrentDataSourceSync,
                                    this.InternalPosition,
                                    VisibleRowsCount = this.Rows.Count,
                                    DataRowsCount = __SourceBindingSource_DataSource_asDataTable.Rows.Count
                                });

                                // some other datasource?
                                if (!isCurrentDataSourceSync)
                                    return;

                                // grid is letting bindingsource know what was selected!

                                // is the new row ready yet?

                                if (this.InternalPosition >= __SourceBindingSource_DataSource_asDataTable.Rows.Count)
                                    // selection should wait for data sync?
                                    return;

                                SourceBindingSource.Position = this.InternalPosition;
                            };
                    };
                    #endregion


                    DoSyncPosition();

                    // 26:180ms { MyDataSource_DataSource = <Namespace>.MyOtherDataSource } 

                    #region MyDataSource_DataSource_as_DataTable
                    var MyDataSource_DataSource_as_DataTable = SourceBindingSource_DataSource_asDataTable as DataTable;
                    if (MyDataSource_DataSource_as_DataTable != null)
                    {
                        // X:\jsc.svn\examples\javascript\forms\Test\TestDynamicBindingSourceForDataTable\TestDynamicBindingSourceForDataTable\ApplicationControl.Designer.cs
                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140409

                        // yay. we found the source.
                        // we should learn to talk to IListSource

                        // keep sync object!
                        InternalSetDataSource(MyDataSource_DataSource_as_DataTable, CurrentDataSourceSync);
                        return;
                    }
                    #endregion

                    if (SourceBindingSource_DataSource_asDataTable is IListSource)
                    {
                        // X:\jsc.svn\examples\javascript\forms\FormsAutoSumGridSelection\FormsAutoSumGridSelection\Data\MyDataSource.cs

                        //26:3237ms InternalSetDataSource does not yet support IListSource 
                        Console.WriteLine("InternalSetDataSource does not yet support IListSource");
                        return;
                    }


                    //                                    26:140ms { MyDataSource_DataSource = [object Object] } view-source:37388
                    //26:140ms InternalSetDataSource does not yet support ? 

                    //Console.WriteLine("InternalSetDataSource activated " + new
                    //{
                    //    Type = asBindingSource.DataSource.GetType(),
                    //    asBindingSource.DataSource
                    //});
                    Console.WriteLine("InternalSetDataSource does not yet support ?");

                    //26:182ms InternalSetDataSource BindingSource { Type = <Namespace>.Type, DataSource = <Namespace>.MyDataSource } view-source:37770
                    //26:185ms __BindingSource EndInit 

                    // continue data binding?
                };
                #endregion



                // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\Library\TheView.cs
                SourceBindingSource.DataSourceChanged +=
                    delegate
                    {
                        //Console.WriteLine(" designer is still setting things up? DataSourceChanged");

                        if (SourceBindingSource.ActivatedDataSource == null)
                            return;

                        AtSourceBindingSourceDataSource();
                    };


                if (SourceBindingSource.ActivatedDataSource == null)
                {
                    //Console.WriteLine(" designer is still setting things up?");
                    return;
                }

                AtSourceBindingSourceDataSource();
                return;
            }
            #endregion


            if (SourceDataTable == null)
            {
                Console.WriteLine("InternalSetDataSource not implemented for " + value.GetType());
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


            // X:\jsc.svn\examples\javascript\forms\FormsHistoricBindingSourcePosition\FormsHistoricBindingSourcePosition\ApplicationControl.cs
            if (this.AutoGenerateColumns)
                InternalAutoGenerateColumns(SourceDataTable);

            // show the columns and continue in a moment
            Native.window.requestAnimationFrame += delegate
            {
                if (this.InternalDataSourceSync != CurrentDataSourceSync)
                    return;
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

                    //for (int ic = 0; ic < SourceDataTableColumnCount; ic++)
                    // visible columns?
                    for (int ic = 0; ic < this.Columns.Count; ic++)
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
                            //td.colSpan = SourceDataTableColumnCount;

                            // visible columns?
                            td.colSpan = this.Columns.Count;
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
                            //SourceDataTableColumnCount,
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

                                // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.BaseCollection.GetEnumerator()]
                                // columns reordered?

                                for (int ci = 0; ci < this.Columns.Count; ci++)
                                {
                                    DataGridViewColumn c = this.Columns[ci];

                                    var cc = new DataGridViewTextBoxCell
                                    {
                                        // two way binding?
                                        //ReadOnly = true,

                                        Value = DataBoundItem[c.DataPropertyName]

                                        // Timestamp / datetime thingis need special attention?
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
                              //SourceDataTableColumnCount,
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

                        var xColumnIndex = SourceDataTable.Columns.IndexOf(x.Column);

                        if (xColumnIndex >= this.Columns.Count)
                        {
                            // we are not showing that data column! bail!
                            return;
                        }

                        //29:16660ms DataSource UserAddedRow{ Count = 3 } view-source:37895
                        //29:16661ms SourceDataTable.ColumnChanged { RowIndex = 3, xColumnIndex = 0 } view-source:37895
                        //29:16662ms SourceDataTable.ColumnChanged { RowIndex = 3, xColumnIndex = 1 } 

                        var RowIndex = SourceDataTable.Rows.IndexOf(x.Row);

                        Console.WriteLine("SourceDataTable.ColumnChanged " + new { RowIndex, xColumnIndex });

                        var c = this[xColumnIndex, RowIndex];

                        if (c == null)
                            Debugger.Break();


                        if (c.Value == x.ProposedValue)
                            return;

                        c.Value = x.ProposedValue;
                    };





                #region CellValueChanged
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

                #endregion



                #region TableNewRow
                SourceDataTable.TableNewRow +=
                    (s, e) =>
                    {
                        if (this.InternalDataSourceSync != CurrentDataSourceSync)
                            return;

                        this.InternalDataSourceSync = null;

                        //                        60:417ms { FooColumn = foo from server1, GooColumn = 400 }
                        //60:443ms a new row was added, auto resize?

                        //Console.WriteLine("SourceDataTable.TableNewRow");


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

                        // is this allowed?
                        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableNewRow\TestDataTableNewRow\ApplicationWebService.cs
                        Console.WriteLine("DataSource UserAddedRow" + new { SourceDataTable.Rows.Count });


                        this.InternalDataSourceSync = null;

                        NewRow = SourceDataTable.NewRow();
                        SourceDataTable.Rows.Add(NewRow);
                        this.InternalDataSourceSync = CurrentDataSourceSync;

                        foreach (DataColumn item in SourceDataTable.Columns)
                        {
                            // user cannot enter null can he
                            // raise_ColumnChanged
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

                if (stopwatch.ElapsedMilliseconds > 30)
                    Console.WriteLine(
                    "event: "
                        // what if there is no form?
                        //+ this.FindForm().Name + "." 
                        // what if there is no name?
                    + this.Name
                    + " set DataSource almost done "
                    + new
                    {
                        //SourceDataTableColumnCount,
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
                    if (this.InternalDataSourceSync != CurrentDataSourceSync)
                        return;

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


                    if (sReposition0.ElapsedMilliseconds > 30)
                        Console.WriteLine(
                              this.Name
                              + " set DataSource sReposition0 "
                              + new
                              {
                                  //SourceDataTableColumnCount,
                                  sReposition0.ElapsedMilliseconds
                              }
                           );


                    InternalAutoSizeWhenFill();



                    new XAttribute(
                        "Stopwatch",

                        new
                        {
                            //columns = cstopwatch.ElapsedMilliseconds,
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
                              //SourceDataTableColumnCount,
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
