using ScriptCoreLib.Shared.BCLImplementation.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [ScriptCoreLib.Script(Implements = typeof(global::System.Windows.Forms.TreeNodeMouseClickEventArgs))]
    internal class __TreeNodeMouseClickEventArgs : __EventArgs
    {
        public __TreeNodeMouseClickEventArgs(TreeNode node, MouseButtons button, int clicks, int x, int y)
        {
            this.Node = node;

        }

        public TreeNode Node { get; set; }
    }

}
