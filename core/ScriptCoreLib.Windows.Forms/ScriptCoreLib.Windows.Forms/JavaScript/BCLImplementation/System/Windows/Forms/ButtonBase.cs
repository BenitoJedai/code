using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ButtonBase))]
    public class __ButtonBase : __Control
    {
        public FlatButtonAppearance FlatAppearance { get; set; }
        public bool UseVisualStyleBackColor { get; set; }
        public FlatStyle FlatStyle { get; set; }


    }
}
