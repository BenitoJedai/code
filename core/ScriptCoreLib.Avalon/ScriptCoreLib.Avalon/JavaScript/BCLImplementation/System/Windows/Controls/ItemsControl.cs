using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
    [Script(Implements = typeof(global::System.Windows.Controls.ItemsControl))]
    public class __ItemsControl
    {
        public ItemCollection Items { get; set; }
    }
}
