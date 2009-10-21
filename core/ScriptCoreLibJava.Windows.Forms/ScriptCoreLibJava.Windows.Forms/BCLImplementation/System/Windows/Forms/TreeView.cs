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
		public readonly javax.swing.JTree InternalElement;

		public __TreeView()
		{
			this.InternalElement = new javax.swing.JTree();
			this.Nodes = 
				(TreeNodeCollection)(object)new __TreeNodeCollection
				{
					InternalTreeView = this
				};

			this.Nodes.Clear();
		}

		public override java.awt.Component InternalGetElement()
		{
			return InternalElement;
		}

		public TreeNodeCollection Nodes { get; set; }
	}
}
