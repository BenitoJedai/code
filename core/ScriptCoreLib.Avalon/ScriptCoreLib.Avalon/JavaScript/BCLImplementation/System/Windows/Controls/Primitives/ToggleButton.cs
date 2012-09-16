using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls.Primitives
{
    [Script(Implements = typeof(global::System.Windows.Controls.Primitives.ToggleButton))]
    internal class __ToggleButton : __ButtonBase
    {
        protected virtual void InternalSetIsChecked(bool? e)
        {

        }

        protected virtual bool? InternalGetIsChecked()
        {
            return default(bool?);
        }

        public bool? IsChecked
        {
            get
            {
                return InternalGetIsChecked();
            }
            set
            {
                InternalSetIsChecked(value);
            }
        }
    }
}
