using ScriptCoreLib.Shared.BCLImplementation.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [ScriptCoreLib.Script(Implements = typeof(global::System.Windows.Forms.TreeViewEventArgs))]
    internal class __TreeViewEventArgs : __EventArgs
    {
        public __TreeViewEventArgs(TreeNode Node)
        {
            this.Node = Node;
        }

        public TreeNode Node { get; set; }
    }

}
