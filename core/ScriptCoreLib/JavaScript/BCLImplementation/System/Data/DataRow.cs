using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataRow))]
    public class __DataRow
    {
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView.cs

        public List<Tuple<DataColumn, object>> InternalData = new List<Tuple<DataColumn, object>>();

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

                x = new Tuple<DataColumn, object>(column, value);

                InternalData.Add(x);
            }
        }

        public static implicit operator DataRow(__DataRow x)
        {
            return (DataRow)(object)x;
        }
    }
}
