using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ListControl))]
    internal class __ListControl : __Control
    {

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ListControl.get_SelectedIndex()]

        public virtual int InternalGetSelectedIndex()
        {
            return -1;
        }

        public virtual void InternalSetSelectedIndex(int value)
        {
        }

        public int SelectedIndex
        {
            get
            {
                return InternalGetSelectedIndex();
            }
            set
            {
                InternalSetSelectedIndex(value);
            }
        }

        public bool FormattingEnabled { get; set; }
    }
}
