using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewBand))]
    public class __DataGridViewBand : __DataGridViewElement
    {
        public virtual DataGridViewCellStyle DefaultCellStyle { get; set; }
        public virtual bool ReadOnly { get; set; }
        public virtual bool Visible { get; set; }

        public virtual int InternalGetIndex()
        {
            return -1;
        }

        public int Index
        {
            get
            {
                return InternalGetIndex();
            }
        }

        public __DataGridView InternalContext;


        //t [System.Windows.Forms.DataGridViewBand.set_Visible(System.Boolean)]
    }
}
