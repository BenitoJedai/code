using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.TextBox))]
    internal class __TextBox : __TextBoxBase
    {
        // should we listen for enter key?
        public bool AcceptsReturn { get; set; }

        public __TextBox()
        {
            this.ScrollBars = global::System.Windows.Forms.ScrollBars.None;
        }

        private HorizontalAlignment _TextAlign;

        public HorizontalAlignment TextAlign
        {
            get { return _TextAlign; }
            set { _TextAlign = value; }
        }

        public override bool Enabled
        {
            get
            {
                return !InternalTextField.disabled;
            }
            set
            {
                if (InternalTextField_MultiLine != null)
                    InternalTextField_MultiLine.disabled = !value;

                InternalTextField.disabled = !value;
            }
        }


        #region ScrollBars
        ScrollBars InternalScrollBars;

        public ScrollBars ScrollBars
        {
            get
            {
                return InternalScrollBars;
            }
            set
            {
                InternalScrollBars = value;

                InternalUpdateScrollBars = delegate
                {
                    if (value == ScrollBars.Both)
                    {
                        this.InternalGetTextField().style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.scroll;

                    }

                    if (value == ScrollBars.None)
                    {
                        this.InternalGetTextField().style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;
                    }
                    else
                    {
                        this.InternalGetTextField().style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.auto;
                    }
                };


                InternalUpdateScrollBars();


            }
        }
        #endregion
    }
}
