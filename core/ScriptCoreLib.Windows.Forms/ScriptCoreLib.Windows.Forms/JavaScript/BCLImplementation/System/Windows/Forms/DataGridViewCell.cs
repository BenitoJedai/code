using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewCell))]
    internal abstract class __DataGridViewCell : __DataGridViewElement
    {
        public IHTMLDiv InternalContentContainer;
        public IHTMLTableColumn InternalTableColumn;

        #region Value
        public object InternalValue;
        public event Action InternalValueChanged;
        public object Value
        {
            get
            {
                return InternalValue;
            }
            set
            {
                InternalValue = value;
                if (InternalValueChanged != null)
                    InternalValueChanged();
            }
        }
        #endregion

        public __DataGridViewCellStyle InternalStyle { get; set; }
        public DataGridViewCellStyle Style { get; set; }

        public __DataGridViewRow InternalContext;

        public int ColumnIndex
        {
            get
            {
                if (InternalContext == null)
                    return -1;

                return InternalContext.InternalCells.InternalItems.IndexOf(this);
            }
        }

        public  bool InternalSelected;
        public virtual bool Selected
        {
            get
            {
                return InternalSelected;
            }
            set
            {
                InternalSelected = value;

                if (value)
                    this.InternalContentContainer.focus();
            }
        }
        public __DataGridViewCell()
        {
            this.InternalValue = "";
            this.InternalStyle = new __DataGridViewCellStyle();
            this.Style = (DataGridViewCellStyle)(object)this.InternalStyle;
        }

        #region operators
        public static implicit operator __DataGridViewCell(DataGridViewCell c)
        {
            return (__DataGridViewCell)(object)c;
        }
        public static implicit operator DataGridViewCell(__DataGridViewCell c)
        {
            return (DataGridViewCell)(object)c;
        }
        #endregion
    }
}
