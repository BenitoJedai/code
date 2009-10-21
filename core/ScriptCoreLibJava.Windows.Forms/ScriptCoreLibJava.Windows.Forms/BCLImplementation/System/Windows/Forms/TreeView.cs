using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Forms;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.TreeView))]
	internal class __TreeView : __Control
	{
		readonly javax.swing.JTree InternalElement = new javax.swing.JTree();

		public override java.awt.Component InternalGetElement()
		{
			return InternalElement;
		}

		public TreeNodeCollection Nodes { get; set; }
	}
}
