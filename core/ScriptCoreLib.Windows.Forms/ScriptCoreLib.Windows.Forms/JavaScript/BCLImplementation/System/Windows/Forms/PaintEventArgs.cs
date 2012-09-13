using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    [Script(Implements = typeof(global::System.Windows.Forms.PaintEventArgs))]
    internal class __PaintEventArgs : __EventArgs, IDisposable
    {
        public Graphics Graphics { get; set; }

        public void Dispose()
        {
        }
    }
}
