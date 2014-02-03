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


                Native.window.requestAnimationFrame += delegate
                {


                    if (DataSourceChanged != null)
                        DataSourceChanged(this, new EventArgs());

                };
            }
        }

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


            // 547ms event: dataGridView1 set DataSource { ColumnIndex = 30, ElapsedMilliseconds = 140 } 
            // 279ms event: dataGridView1 set DataSource { ColumnIndex = 6, ElapsedMilliseconds = 41 } 
            Console.WriteLine(
                    "event: "
                    + this.Name
                    + " set DataSource "
                    + new
                    {
                        ColumnIndex,
                        stopwatch.ElapsedMilliseconds,
                    }
            );

            // show the columns and continue in a moment
            Native.window.requestAnimationFrame += delegate
            {
                stopwatch.Restart();


                #region Rows
                var SourceRowIndex = -1;
                var RowStopwatch = Stopwatch.StartNew();

                // X:\jsc.svn\examples\javascript\Test\TestManyTableRows\TestManyTableRows\Application.cs

                var SourceDataTableRowCount = SourceDataTable.Rows.Count;
                var SourceDataTableColumnCount = SourceDataTable.Columns.Count;


                //var NewTBody = default(IHTMLTableBody);
                //var NewTBody_innerHTML = new StringBuilder();
                //var NewTBody_tr_innerHTML = "";

                //NewTBody_tr_innerHTML += ("<tr>");

                //// 2309ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 1917, a = 1.9189189189189189 } 

                ////for (int ic = 0; ic < SourceDataTableColumnCount; ic++)
                ////{
                ////    //var data = DataBoundItem[ic];
                ////    //tr.AddColumn().setAttribute("data", data);
                ////    //break;
                ////    //NewTBody_innerHTML.Append("<td data='?'></td>");
                ////    NewTBody_tr_innerHTML += ("<td data='?'></td>");
                ////}

                //NewTBody_tr_innerHTML += ("<td colspan='" + SourceDataTableColumnCount + "' data='?'></td>");


                ////NewTBody_innerHTML.Append("</tr>");
                //NewTBody_tr_innerHTML += ("</tr>");

                for (int i = 0; i < SourceDataTableRowCount; i++)
                {
                    var DataBoundItem = SourceDataTable.Rows[i];

                    SourceRowIndex++;

                    if (i < 0)
                    {
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
                        //if (RowStopwatch.ElapsedMilliseconds > 30)
                        {
                            // report slowdowns only.
                            //35224ms { Name =  } InternalSetDataSource add Row { SourceRowIndex = 64, ElapsedMilliseconds = 396 } view-source:35785
                            //35634ms { Name =  } InternalSetDataSource add Row { SourceRowIndex = 65, ElapsedMilliseconds = 409 } 

                            //4415ms { Name =  } InternalSetDataSource add Row { SourceRowIndex = 65, ElapsedMilliseconds = 14 } view-source:35785
                            //4434ms event:  set DataSource{ ElapsedMilliseconds = 1153 }


                        }

                        this.Rows.Add(r);

                        Console.WriteLine(
                            new { Name }
                            + " InternalSetDataSource add Row "
                            + new { SourceRowIndex, RowStopwatch.ElapsedMilliseconds }
                         );
                    }
                    else
                    {
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

                        // http://www.w3.org/TR/html5/tabular-data.html#the-table-element


                        for (int ic = 0; ic < SourceDataTableColumnCount; ic++)
                        {
                            var data = DataBoundItem[ic];

                            var td = tr.AddColumn();
                            // http://www.w3schools.com/cssref/css3_pr_column-span.asp

                            // 4760ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 9999, ElapsedMilliseconds = 4092, a = 0.4092 } 



                            if (i < 24)
                            {
                                td.setAttribute("data", data);
                            }
                            else
                            {
                                td.colSpan = SourceDataTableColumnCount;
                                break;
                            }
                        }

                        // 1264ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 999, ElapsedMilliseconds = 882, a = 0.882 } 

                        // 843ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 522, a = 0.5225225225225225 } 
                        // 807ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 468, a = 0.46846846846846846 } 
                        // 789ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 461, a = 0.46146146146146144 }
                        // 783ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 442, a = 0.44244244244244246 } 
                        // 849ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 518, a = 0.5185185185185185 }
                        // 469ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 31, ElapsedMilliseconds = 166, a = 5.1875 } 
                        // 4406ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 9999, ElapsedMilliseconds = 3733, a = 0.3733 } 
                        // 2330ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 24, ElapsedMilliseconds = 1670, a = 66.8 } 
                        // 763ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 999, ElapsedMilliseconds = 387, a = 0.387 } 

                        // without DOM:
                        // 460ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 131, a = 0.13113113113113112 } 
                        // without dom we could use string builder
                        // this could in turn be done by a backgroun worker?
                        // 851ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 497, a = 0.4974974974974975 }
                        // with string with data prep
                        // 2595ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 2245, a = 2.2472472472472473 } 
                        // 2275ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 1919, a = 1.9209209209209208 } 
                        // 2298ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 1942, a = 1.9439439439439439 } 
                        // 2309ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 1917, a = 1.9189189189189189 } 

                        // what about DOM clone then?
                        //NewTBody_innerHTML.Append(NewTBody_tr_innerHTML);

                        // 6152ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 2822, a = 2.824824824824825 } 
                        // 870ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 535, a = 0.5355355355355356 }
                        // 826ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 521, a = 0.5215215215215215 } 
                        // 3235ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 2865, a = 2.8678678678678677 } 
                        // 3235ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 2865, a = 2.8678678678678677 } 


                    }


                    // 6881ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 6590, a = 6.596596596596597 } 


                    RowStopwatch.Restart();
                }
                #endregion

                //if (NewTBody != null)
                //    NewTBody.innerHTML = NewTBody_innerHTML.ToString();

                // 5908ms { Name = dataGridView1 } InternalSetDataSource Rows done at { ElapsedMilliseconds = 669 } 



                //Console.WriteLine("add CellValueChanged ");

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


                InternalAutoResizeAll();

                InternalDataSourceBusy = false;
                stopwatch.Stop();

                // 111485ms { Form = ExampleForm, Name = dataGridView1 } exit InternalSetDataSource{ ElapsedMilliseconds = 2775 } 

                // 2281ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 1908, a = 1.90990990990991 } 
                //2136ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 1788, a = 1.7897897897897899 } 
                // 750ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 435, a = 0.43543543543543545 }
                Console.WriteLine(
                    "event: "
                    // what if there is no form?
                    //+ this.FindForm().Name + "." 
                    // what if there is no name?
                    + this.Name
                    + " set DataSource "
                    + new
                    {
                        ColumnIndex,
                        SourceRowIndex,
                        stopwatch.ElapsedMilliseconds,
                        a = stopwatch.ElapsedMilliseconds / (SourceRowIndex + 1)
                    }
                 );

                // 4069ms { Form = Form1, Name = dataGridView1 } exit InternalSetDataSource{ ElapsedMilliseconds = 2027 } 



                new XAttribute(
                   "Stopwatch",
                   new { stopwatch.ElapsedMilliseconds }.ToString()
               ).AttachTo(this.HTMLTargetRef);
            };

        }



    }
}
