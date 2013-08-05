using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
    [Script(Implements = typeof(global::System.Windows.Window))]
    internal class __Window : __FrameworkElement
    {
        public bool? ShowDialog()
        {
            throw new NotSupportedException();
        }
    }
}
