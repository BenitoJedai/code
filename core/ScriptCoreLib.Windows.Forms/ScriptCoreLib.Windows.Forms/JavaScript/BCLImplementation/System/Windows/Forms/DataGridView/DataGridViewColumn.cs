using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewColumn))]
    public class __DataGridViewColumn : __DataGridViewBand
    {
        public IHTMLTableColumn InternalTableColumn;
        public DragHelper InternalHorizontalDrag;

        public IHTMLSpan InternalContent;

        #region DefaultCellStyle
        public DataGridViewCellStyle InternalDefaultCellStyle;
        public event Action InternalDefaultCellStyleChanged;
        public override DataGridViewCellStyle DefaultCellStyle
        {
            get { return InternalDefaultCellStyle; }
            set
            {
                InternalDefaultCellStyle = value;
                if (InternalDefaultCellStyleChanged != null)
                    InternalDefaultCellStyleChanged();
            }
        }
        #endregion

        public override bool ReadOnly { get; set; }

        public string DataPropertyName { get; set; }

        #region Visible
        public bool InternalVisible = true;
        public event Action InternalVisibleChanged;
        public override bool Visible
        {
            get
            {
                // https://developer.mozilla.org/en/docs/Web/API/window.getComputedStyle

                return InternalVisible;
            }
            set
            {
                InternalVisible = value;


                if (InternalVisibleChanged != null)
                    InternalVisibleChanged();
            }
        }
        #endregion

        #region HeaderText
        public string InternalHeaderText;
        public event Action InternalHeaderTextChanged;

        public string HeaderText
        {
            get
            {
                return InternalHeaderText;
            }
            set
            {
                InternalHeaderText = value;

                if (InternalHeaderTextChanged != null)
                    InternalHeaderTextChanged();
            }
        }
        #endregion


        public string Name { get; set; }


        public override int InternalGetIndex()
        {
            if (InternalContext == null)
                return -1;



            return InternalContext.InternalColumns.InternalItems.IndexOf(this);
        }



        public DataGridViewAutoSizeColumnMode AutoSizeMode { get; set; }


        public virtual int GetPreferredWidth(DataGridViewAutoSizeColumnMode autoSizeColumnMode, bool fixedHeight)
        {
            return 200;
        }


        #region Width
        public int InternalWidth;
        public event Action InternalWidthChanged;
        public IHTMLDiv ColumnHorizontalResizer;
        public int Width
        {
            get
            {
                return InternalWidth;
            }
            set
            {
                InternalWidth = value;
                if (InternalWidthChanged != null)
                    InternalWidthChanged();
            }
        }
        #endregion


        public __DataGridViewColumn()
        {
            this.HeaderText = "Column";
            this.Width = 100;
            this.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
        }

        #region operators
        public static implicit operator __DataGridViewColumn(DataGridViewColumn c)
        {
            return (__DataGridViewColumn)(object)c;
        }
        public static implicit operator DataGridViewColumn(__DataGridViewColumn c)
        {
            return (DataGridViewColumn)(object)c;
        }
        #endregion

    }
}
