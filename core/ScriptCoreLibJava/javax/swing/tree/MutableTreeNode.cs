// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.tree.MutableTreeNode

using ScriptCoreLib;
using java.lang;

namespace javax.swing.tree
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/tree/MutableTreeNode.html
	[Script(IsNative = true)]
	public interface MutableTreeNode : TreeNode
	{
		/// <summary>
		/// Adds <code>child</code> to the receiver at <code>index</code>.
		/// </summary>
		void insert(MutableTreeNode @child, int @index);

		/// <summary>
		/// Removes the child at <code>index</code> from the receiver.
		/// </summary>
		void remove(int @index);

		/// <summary>
		/// Removes <code>node</code> from the receiver.
		/// </summary>
		void remove(MutableTreeNode @node);

		/// <summary>
		/// Removes the receiver from its parent.
		/// </summary>
		void removeFromParent();

		/// <summary>
		/// Sets the parent of the receiver to <code>newParent</code>.
		/// </summary>
		void setParent(MutableTreeNode @newParent);

		/// <summary>
		/// Resets the user object of the receiver to <code>object</code>.
		/// </summary>
		void setUserObject(object @object);

	}
}
