using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewBand))]
    internal class __DataGridViewBand : __DataGridViewElement
    {
        public virtual DataGridViewCellStyle DefaultCellStyle { get; set; }
        public virtual bool ReadOnly { get; set; }

        public int Index
        {
            get
            {
                if (InternalContext == null)
                    return -1;

                var r = this as __DataGridViewRow;
                
                // what else could it be?
                if (r == null)
                    return -1;

                return InternalContext.InternalRows.InternalItems.IndexOf(r);
            }
        }

        public __DataGridView InternalContext;

    }
}
