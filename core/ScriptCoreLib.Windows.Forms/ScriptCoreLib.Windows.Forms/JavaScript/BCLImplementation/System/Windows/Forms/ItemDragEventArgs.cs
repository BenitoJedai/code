using ScriptCoreLib.Shared.BCLImplementation.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ItemDragEventArgs))]
    internal class __ItemDragEventArgs : __EventArgs
    {
        public __ItemDragEventArgs(MouseButtons button, object item)
        {
            this.Button = button;
            this.Item = item;

        }

        public MouseButtons Button { get; }
        public object Item { get; }




        public static implicit operator ItemDragEventArgs(__ItemDragEventArgs e)
        {
            return (ItemDragEventArgs)(object)e;
        }
    }
}
