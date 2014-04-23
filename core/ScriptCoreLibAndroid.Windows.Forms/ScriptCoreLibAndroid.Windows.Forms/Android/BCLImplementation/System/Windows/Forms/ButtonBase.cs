using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ButtonBase))]
    public class __ButtonBase : __Control
    {
        public bool UseVisualStyleBackColor { get; set; }
    }
}
