using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Collections;
using System.Windows.Forms;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.TreeNodeCollection))]
	internal class __TreeNodeCollection :   __IList, __ICollection, __IEnumerable
	{
		public __TreeView InternalTreeView;

		public virtual void Clear()
		{
			// http://forums.sun.com/thread.jspa?threadID=610715
			// http://www.javakb.com/Uwe/Forum.aspx/java-gui/2724/remove-all-nodes-from-a-JTree
			// http://java.sun.com/docs/books/tutorial/uiswing/components/tree.html#sel
			// http://www.coderanch.com/t/345711/Swing-AWT-SWT-JFace/java/JTree-Compare-Nodes

			var m = (javax.swing.tree.DefaultTreeModel)this.InternalTreeView.InternalElement.getModel();

			m.setRoot(null);
		}

		public virtual void AddRange(TreeNode[] nodes)
		{

		}

		#region __IEnumerable Members

		public global::System.Collections.IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region __IEnumerable Members

		global::System.Collections.IEnumerator __IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
