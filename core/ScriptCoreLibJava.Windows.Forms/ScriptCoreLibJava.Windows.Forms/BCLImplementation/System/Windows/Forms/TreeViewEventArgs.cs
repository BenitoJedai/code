using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Forms;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.TreeViewEventArgs))]
	internal class __TreeViewEventArgs
	{
		public __TreeViewEventArgs(TreeNode Node)
		{
			this.Node = Node;
		}

		public TreeNode Node { get; set; }
	}
}
