using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Forms;
using javax.swing.tree;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.TreeView))]
	internal class __TreeView : __Control
	{
		public readonly javax.swing.JTree InternalElement;
		public readonly DefaultTreeModel InternalModel;
		public readonly DefaultMutableTreeNode InternalRoot;

		public __TreeView()
		{
			// look and feel:
			// http://www.developer.com/java/other/article.php/768051/User-Code-Using-the-Windows-JTree-Icons-in-Any-Look-and-Feel.htm

			// http://www.chka.de/swing/tree/DefaultTreeModel.html


			this.InternalRoot = new DefaultMutableTreeNode("root");
			this.InternalModel = new DefaultTreeModel(InternalRoot);

			//  a JTree with a sample model
			this.InternalElement = new javax.swing.JTree(InternalModel);

			//UIManager.put("Tree.expandedIcon", new WindowsTreeUI.ExpandedIcon());
			//UIManager.put("Tree.collapsedIcon", new WindowsTreeUI.CollapsedIcon());


			this.Nodes =
				(TreeNodeCollection)(object)new __TreeNodeCollection
				{
					InternalTreeView = this,
				};

			this.InternalElement.setRootVisible(false);
			//this.InternalElement.setShowsRootHandles(false);

			//this.Nodes.Clear();

			// http://forums.sun.com/thread.jspa?threadID=336159&tstart=23672
			// http://www.daniweb.com/forums/thread65014.html#
			// http://www.apl.jhu.edu/~hall/java/Swing-Tutorial/Swing-Tutorial-JTree.html


			//this.InternalElement.add
		}

		public override java.awt.Component InternalGetElement()
		{
			return InternalElement;
		}

		public TreeNodeCollection Nodes { get; set; }
	}
}
