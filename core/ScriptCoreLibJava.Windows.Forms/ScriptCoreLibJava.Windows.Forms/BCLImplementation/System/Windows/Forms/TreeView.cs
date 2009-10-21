using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using javax.swing.@event;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.TreeView))]
	internal class __TreeView : __Control
	{
		public readonly javax.swing.JScrollPane InternalContainer;
		public readonly javax.swing.JTree InternalContent;
		public readonly javax.swing.tree.DefaultTreeModel InternalModel;
		public readonly javax.swing.tree.DefaultMutableTreeNode InternalRoot;

		public event global::System.Windows.Forms.TreeViewEventHandler AfterSelect;

		public void RaiseAfterSelect(object sender, global::System.Windows.Forms.TreeViewEventArgs e)
		{
			if (AfterSelect != null)
				AfterSelect(sender, e);
		}

		public __TreeView()
		{
			// look and feel:
			// http://www.developer.com/java/other/article.php/768051/User-Code-Using-the-Windows-JTree-Icons-in-Any-Look-and-Feel.htm

			// http://www.chka.de/swing/tree/DefaultTreeModel.html


			this.InternalRoot = new javax.swing.tree.DefaultMutableTreeNode("root");
			this.InternalModel = new javax.swing.tree.DefaultTreeModel(InternalRoot);

			
			//  a JTree with a sample model
			this.InternalContent = new javax.swing.JTree(InternalModel);
			//this.InternalElement.setAutoscrolls(true);

			this.InternalContainer = new javax.swing.JScrollPane(this.InternalContent);

			// http://www.apl.jhu.edu/~hall/java/Swing-Tutorial/Swing-Tutorial-JTree.html
			var renderer2 = new javax.swing.tree.DefaultTreeCellRenderer();
			renderer2.setOpenIcon(null);
			renderer2.setClosedIcon(null);
			renderer2.setLeafIcon(null);
			this.InternalContent.setCellRenderer(renderer2);

			//UIManager.put("Tree.expandedIcon", new WindowsTreeUI.ExpandedIcon());
			//UIManager.put("Tree.collapsedIcon", new WindowsTreeUI.CollapsedIcon());


			this.Nodes =
				(global::System.Windows.Forms.TreeNodeCollection)(object)new __TreeNodeCollection
				{
					InternalTreeView = this,
					InternalRoot = this.InternalRoot
				};

			this.InternalContent.setRootVisible(false);
			this.InternalContent.setShowsRootHandles(true);

			//this.Nodes.Clear();

			// http://forums.sun.com/thread.jspa?threadID=336159&tstart=23672
			// http://www.daniweb.com/forums/thread65014.html#
			// http://www.apl.jhu.edu/~hall/java/Swing-Tutorial/Swing-Tutorial-JTree.html


			this.InternalContent.addTreeSelectionListener(
				new __TreeSelectionListener
				{
					InternalTreeView = this
				}
			);

		}

		[Script]
		public class __TreeSelectionListener : TreeSelectionListener
		{
			public __TreeView InternalTreeView;

			#region TreeSelectionListener Members

			public void valueChanged(TreeSelectionEvent e)
			{
				this.InternalTreeView.RaiseAfterSelect(InternalTreeView,
					new global::System.Windows.Forms.TreeViewEventArgs(null)
				);
			}

			#endregion
		}

		public global::System.Windows.Forms.TreeNode SelectedNode
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
				
			}
		}

		public override java.awt.Component InternalGetElement()
		{
			return InternalContainer;
		}

		public global::System.Windows.Forms.TreeNodeCollection Nodes { get; set; }
	}
}
