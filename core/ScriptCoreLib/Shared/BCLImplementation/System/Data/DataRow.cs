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

        public object this[int column]
        {

            set
            {
                this[this.Table.Columns[column]] = value;
            }
        }
        public object this[DataColumn column]
        {
            get
            {
                var x = InternalData.FirstOrDefault(k => k.Item1 == column);

                if (x == null)
                    return null;

                return x.Item2;
            }

            set
            {
                var x = InternalData.FirstOrDefault(k => k.Item1 == column);

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
