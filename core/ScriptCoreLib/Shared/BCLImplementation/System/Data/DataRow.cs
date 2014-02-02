using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataRow))]
    public class __DataRow
    {
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView.cs

        // 10000: 308ms data to html table: 6684ms
        // X:\jsc.svn\examples\javascript\Test\TestManyTableRowsFromDataTable\TestManyTableRowsFromDataTable\Application.cs

        // max columns 32? last element shall be the default value for non existant row?
        public object[] InternalDataArray = new object[32];

        //public List<Tuple<DataColumn, object>> InternalData = new List<Tuple<DataColumn, object>>();


        public object this[string column]
        {
            // tested by
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Schema\FooTableDesigner.cs

            set
            {

                //var c = this.InternalData.FirstOrDefault(k => k.Item1.ColumnName == column);

                //if (c == null)
                //{
                //    var cc = new DataColumn { ColumnName = column };

                //    this[cc] = value;

                //    return;
                //}

                this[this.Table.Columns.IndexOf(column)] = value;
            }

            get
            {

                return this[this.Table.Columns.IndexOf(column)];


                //var x = InternalData.FirstOrDefault(k => k.Item1.ColumnName == column);

                //if (x == null)
                //    return null;

                //return x.Item2;
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
                this.InternalDataArray[column] = value;
                // X:\jsc.svn\examples\javascript\Test\TestManyTableRowsFromDataTable\TestManyTableRowsFromDataTable\Application.cs
                // you better not reorder or reindex the columns!

                //this[this.Table.Columns[column]] = value;
            }
        }
        public object this[DataColumn column]
        {
            get
            {
                return this[this.Table.Columns.IndexOf(column)];


                //var x = InternalData.FirstOrDefault(k => k.Item1.ColumnName == column.ColumnName);

                //if (x == null)
                //    return null;

                //return x.Item2;
            }

            set
            {
                this[this.Table.Columns.IndexOf(column)] = value;
                //var x = InternalData.FirstOrDefault(k => k.Item1.ColumnName == column.ColumnName);

                //if (x != null)
                //{
                //    InternalData.Remove(x);
                //}

                //var args = new DataColumnChangeEventArgs(
                //    this, column,
                //    value
                //);

                ////Console.WriteLine("before raise_ColumnChanged");

                //((__DataTable)(object)this.Table).raise_ColumnChanged(args);


                //x = new Tuple<DataColumn, object>(column, args.ProposedValue);

                //InternalData.Add(x);
            }
        }

        public DataTable Table { get; set; }

        public static implicit operator DataRow(__DataRow x)
        {
            return (DataRow)(object)x;
        }
    }
}
