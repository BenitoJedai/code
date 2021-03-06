﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    // http://referencesource.microsoft.com/#System.Data/data/System/Data/DataRow.cs
    [Script(Implements = typeof(global::System.Data.DataRow))]
    public class __DataRow
    {
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView.cs

        // 10000: 308ms data to html table: 6684ms
        // X:\jsc.svn\examples\javascript\Test\TestManyTableRowsFromDataTable\TestManyTableRowsFromDataTable\Application.cs

        // max columns 32? last element shall be the default value for non existant row?
        public object[] InternalDataArray = new object[32];

        //public List<Tuple<DataColumn, object>> InternalData = new List<Tuple<DataColumn, object>>();

        //Caused by: java.lang.ArrayIndexOutOfBoundsException: -1
        //        at ScriptCoreLib.Shared.BCLImplementation.System.Data.__DataRow.get_Item(__DataRow.java:51)
        //        at ScriptCoreLib.Shared.BCLImplementation.System.Data.__DataRow.get_Item(__DataRow.java:46)
        //        at TestSQLiteGroupBy.Data.Book1MiddleAsGroupByGooWithCountRow.Of(Book1MiddleAsGroupByGooWithCountRow.java:95)


        public object this[string column]
        {
            // tested by
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Schema\FooTableDesigner.cs

            get
            {
                var i = this.Table.Columns.IndexOf(column);

                if (i < 0)
                    return null;

                //throw new InvalidOperationException(new { column }.ToString());

                // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs
                return this[i];
            }

            set
            {

                var i = this.Table.Columns.IndexOf(column);

                if (i < 0)
                    throw new InvalidOperationException(new { column }.ToString());

                this[i] = value;
            }


        }
        //script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataRow.get_Item(System.Int32)]
        public object this[int column]
        {
            get
            {
                //return this[this.Table.Columns[column]];
                return this.InternalDataArray[column];
            }

            set
            {
                // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.cs

                //61:19609ms about to add form server { Key = 3 }
                //61:19610ms after update { x_Key = 1, x_Key_isString = false, Key = 0 }
                //61:19610ms { Key = 0 } eq { Key = 3 }
                //61:19610ms after update { x_Key = 2, x_Key_isString = false, Key = 0 }
                //61:19610ms { Key = 0 } eq { Key = 3 }

                // unless we know of a better data type, it is a string..
                this.InternalDataArray[column] = Convert.ToString(value);
                // X:\jsc.svn\examples\javascript\Test\TestManyTableRowsFromDataTable\TestManyTableRowsFromDataTable\Application.cs
                // you better not reorder or reindex the columns!

                var args = new DataColumnChangeEventArgs(
                    this,
                    this.Table.Columns[column],
                    value
                );

                //Console.WriteLine("before raise_ColumnChanged");

                ((__DataTable)(object)this.Table).raise_ColumnChanged(args);

            }
        }
        public object this[DataColumn column]
        {
            get
            {
                var i = this.Table.Columns.IndexOf(column);

                if (i < 0)
                    return null;
                //throw new InvalidOperationException(new { column }.ToString());


                return this[i];
            }

            set
            {
                this[this.Table.Columns.IndexOf(column)] = value;

            }
        }

        public DataTable Table { get; set; }

        public static implicit operator DataRow(__DataRow x)
        {
            return (DataRow)(object)x;
        }
    }
}
