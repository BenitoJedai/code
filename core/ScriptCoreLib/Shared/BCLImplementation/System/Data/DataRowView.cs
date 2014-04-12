using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataRowView))]
    public class __DataRowView
    {
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\BindingSource.cs

        public DataRow Row { get; set; }

        public static implicit operator DataRowView(__DataRowView x)
        {
            return (DataRowView)(object)x;
        }

        public object this[string property]
        {
            get
            {
                return this.Row[property];
            }
            set
            {
                this.Row[property] = value;
            }
        }
    }
}
