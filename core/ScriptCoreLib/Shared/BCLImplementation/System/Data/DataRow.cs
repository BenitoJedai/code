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

        public List<Tuple<DataColumn, object>> InternalData = new List<Tuple<DataColumn, object>>();

        public object this[string column]
        {
            // tested by
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Schema\FooTableDesigner.cs

            set
            {

                var c = this.InternalData.FirstOrDefault(k => k.Item1.ColumnName == column);

                if (c == null)
                {
                    var cc = new DataColumn { ColumnName = column };

                    this[cc] = value;

                    return;
                }

                this[c.Item1] = value;
            }

            get
            {
                //InternalHandler { path = /xml }
                //java.lang.RuntimeException: Sequence contains no elements
                //        at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__DefinedError.NoElements(__DefinedError.java:27)
                //        at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable.First(__Enumerable.java:462)
                //        at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable.First(__Enumerable.java:438)
                //        at ScriptCoreLib.Shared.BCLImplementation.System.Data.__DataRow.get_Item(__DataRow.java:105)
                //        at ScriptCoreLib.Library.StringConversionsForDataTable.ConvertToString(StringConversionsForDataTable.java:168)

                var x = InternalData.FirstOrDefault(k => k.Item1.ColumnName == column);

                if (x == null)
                    return null;

                return x.Item2;
            }
        }
        //script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataRow.get_Item(System.Int32)]
        public object this[int column]
        {
            get
            {
                return this[this.Table.Columns[column]];

            }

            set
            {
                this[this.Table.Columns[column]] = value;
            }
        }
        public object this[DataColumn column]
        {
            get
            {
                var x = InternalData.FirstOrDefault(k => k.Item1.ColumnName == column.ColumnName);

                if (x == null)
                    return null;

                return x.Item2;
            }

            set
            {
                var x = InternalData.FirstOrDefault(k => k.Item1.ColumnName == column.ColumnName);

                if (x != null)
                {
                    InternalData.Remove(x);
                }

                var args = new DataColumnChangeEventArgs(
                    this, column,
                    value
                );

                //Console.WriteLine("before raise_ColumnChanged");

                ((__DataTable)(object)this.Table).raise_ColumnChanged(args);


                x = new Tuple<DataColumn, object>(column, args.ProposedValue);

                InternalData.Add(x);
            }
        }

        public DataTable Table { get; set; }

        public static implicit operator DataRow(__DataRow x)
        {
            return (DataRow)(object)x;
        }
    }
}
