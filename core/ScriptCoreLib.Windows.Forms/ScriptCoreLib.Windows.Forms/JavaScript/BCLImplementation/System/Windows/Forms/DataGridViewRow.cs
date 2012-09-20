using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewRow))]
    internal class __DataGridViewRow : __DataGridViewBand
    {
        public int InternalHeight;
        public event Action InternalHeightChanged;
        public int Height
        {
            get
            {
                return InternalHeight;
            }
            set
            {
                InternalHeight = value;
                if (InternalHeightChanged != null)
                    InternalHeightChanged();
            }
        }


        public __DataGridViewCellCollection InternalCells;
        public DataGridViewCellCollection Cells { get; set; }

        public __DataGridViewRow()
        {
            this.Height = 22; 

            this.InternalCells = new __DataGridViewCellCollection();
            this.Cells = (DataGridViewCellCollection)(object)this.InternalCells;

        }
    }
}
