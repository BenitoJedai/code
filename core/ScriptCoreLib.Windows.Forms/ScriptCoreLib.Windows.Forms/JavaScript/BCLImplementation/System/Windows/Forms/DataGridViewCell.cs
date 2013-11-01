using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
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
        public virtual bool ReadOnly { get; set; }


        public IHTMLSpan InternalContent;

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
                if (InternalValue == value)
                    return;

                InternalValue = value;
                if (InternalValueChanged != null)
                    InternalValueChanged();
            }
        }
        #endregion

        public __DataGridViewCellStyle InternalStyle;
        public DataGridViewCellStyle Style { get { return InternalStyle; } set { InternalStyle = value; } }

        public __DataGridViewRow InternalOwningRow;
        public DataGridViewRow OwningRow { get { return InternalOwningRow; } }

        public int ColumnIndex
        {
            get
            {
                if (InternalOwningRow == null)
                    return -1;

                return InternalOwningRow.InternalCells.InternalItems.IndexOf(this);
            }
        }

        #region Selected
        public bool InternalSelected;
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
        #endregion

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

//using ScriptCoreLib.JavaScript.;
namespace ScriptCoreLib.JavaScript.Extensions
{
    [Script]
    public static class DataGridViewCellExtensions
    {
        public static IHTMLDiv AsHTMLElementContainer(this DataGridViewCell c)
        {
            __DataGridViewCell x = c;

            return x.InternalContentContainer;
        }
    }
}
