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
    public abstract class __DataGridViewCell : __DataGridViewElement
    {

        // X:\jsc.svn\examples\javascript\forms\Test\TestDataGridViewCellFormattingEven\TestDataGridViewCellFormattingEven\ApplicationControl.cs
        public virtual bool ReadOnly { get; set; }


        public IHTMLTableColumn InternalTableColumn;
        public IHTMLDiv InternalTableColumn_div;
        public IHTMLSpan InternalTableColumn_div_span;

        public object FormattedValue { get; set; }

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

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridViewCell.get_OwningColumn()]

        public string InternalToolTipText;
        public event Action InternalToolTipTextChanged;
        public string ToolTipText
        {
            get { return InternalToolTipText; }
            set
            {
                InternalToolTipText = value;
                if (InternalToolTipTextChanged != null)
                    InternalToolTipTextChanged();
            }
        }


        public int ColumnIndex
        {
            get
            {
                if (InternalOwningRow == null)
                    return -1;

                return InternalOwningRow.InternalCells.InternalItems.IndexOf(this);
            }
        }

        public DataGridViewColumn OwningColumn
        {
            get
            {
                // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridViewElement.get_DataGridView()]

                return InternalOwningRow.InternalContext.Columns[ColumnIndex];
            }
        }

        #region IsInEditMode
        bool InternalIsInEditMode;
        public bool IsInEditMode
        {
            get
            {
                return InternalIsInEditMode;
            }
            set
            {
                InternalIsInEditMode = value;
                this.InternalOwningRow.InternalZeroColumnTableRow.setAttribute("DataGridViewCellIsInEditMode", Convert.ToString(value));
            }
        }
        #endregion

        #region Selected
        bool InternalSelected;
        public virtual bool Selected
        {
            get
            {
                return InternalSelected;
            }
            set
            {
                InternalSetSelected(value);

                if (value)
                    this.InternalTableColumn_div.focus();
            }
        }

        public void InternalSetSelected(bool value)
        {
            InternalSelected = value;
            this.InternalOwningRow.InternalZeroColumnTableRow.setAttribute("DataGridViewCellSelected", Convert.ToString(value));
        }

        #endregion

        public __DataGridViewCell()
        {
            this.InternalValue = "";
            this.InternalStyle = new __DataGridViewCellStyle();
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

            return x.InternalTableColumn_div;
        }
    }
}
