using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.TreeNodeCollection))]
	internal class __TreeNodeCollection : __IList, __ICollection, __IEnumerable
	{
		public __TreeView InternalTreeView;
		public javax.swing.tree.DefaultMutableTreeNode InternalRoot;

		public virtual void Clear()
		{
			// http://forums.sun.com/thread.jspa?threadID=610715
			// http://www.javakb.com/Uwe/Forum.aspx/java-gui/2724/remove-all-nodes-from-a-JTree
			// http://java.sun.com/docs/books/tutorial/uiswing/components/tree.html#sel
			// http://www.coderanch.com/t/345711/Swing-AWT-SWT-JFace/java/JTree-Compare-Nodes

			this.InternalRoot.removeAllChildren();



			if (this.InternalTreeView != null)
				this.InternalTreeView.InternalModel.reload(this.InternalRoot);
		}

		public virtual global::System.Windows.Forms.TreeNode Add(string text)
		{
			var n = new global::System.Windows.Forms.TreeNode(text);

			AddRange(new[] { n });

			return n;
		}

		public virtual void AddRange(global::System.Windows.Forms.TreeNode[] nodes)
		{
			foreach (var c in nodes)
			{
				var cc = (__TreeNode)(object)c;

				cc.InternalNodes.InternalTreeView = this.InternalTreeView;

				this.InternalRoot.add(cc.InternalElement);
			}

			// http://www.jguru.com/faq/view.jsp?EID=33924
			if (this.InternalTreeView != null)
				this.InternalTreeView.InternalModel.reload(this.InternalRoot);
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
