using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/VisualStyles/VisualStyleElement.cs

    [Script(Implements = typeof(global::System.Windows.Window))]
    internal class __Window : __FrameworkElement
    {
        // tested by?
        // can we render avalon window in webgl?

        public bool? ShowDialog()
        {
            throw new NotSupportedException();
        }
    }
}
