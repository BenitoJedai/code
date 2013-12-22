using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewRow))]
    public class __DataGridViewRow : __DataGridViewBand
    {
        #region Height
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
        #endregion

        public override DataGridViewCellStyle DefaultCellStyle { get; set; }

        public DOM.HTML.IHTMLTableRow InternalZeroColumnTableRow;
        public DOM.HTML.IHTMLTableRow InternalTableRow;

        public __DataGridViewCellCollection InternalCells;
        public DataGridViewCellCollection Cells { get; set; }



        public bool IsNewRow
        {
            get
            {
                return (InternalContext.Rows.Count - 1) == this.Index;
            }
        }

        public override int InternalGetIndex()
        {
            if (InternalContext == null)
                return -1;

            var r = this as __DataGridViewRow;

            // what else could it be?
            if (r == null)
                return -1;

            return InternalContext.InternalRows.InternalItems.Source.IndexOf(r);
        }

        public __DataGridViewRow()
        {
            this.Height = 22;

            this.InternalCells = new __DataGridViewCellCollection {
            
                InternalRow = this
            };

            this.Cells = (DataGridViewCellCollection)(object)this.InternalCells;
            this.DefaultCellStyle = new DataGridViewCellStyle();

            this.InternalCells.InternalItems.ListChanged +=
                (s, e) =>
                {
                    if (e.ListChangedType == global::System.ComponentModel.ListChangedType.ItemAdded)
                    {
                        this.InternalCells.InternalItems[e.NewIndex].InternalOwningRow = this;
                    }
                };


        }


        #region operators
        public static implicit operator __DataGridViewRow(DataGridViewRow c)
        {
            return (__DataGridViewRow)(object)c;
        }
        public static implicit operator DataGridViewRow(__DataGridViewRow c)
        {
            return (DataGridViewRow)(object)c;
        }
        #endregion
    }
}
