// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.tree.TreeNode

using ScriptCoreLib;
using java.util;

namespace javax.swing.tree
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/tree/TreeNode.html
	[Script(IsNative = true)]
	public interface TreeNode
	{
		/// <summary>
		/// Returns the children of the receiver as an <code>Enumeration</code>.
		/// </summary>
		Enumeration children();

		/// <summary>
		/// Returns true if the receiver allows children.
		/// </summary>
		bool getAllowsChildren();

		/// <summary>
		/// Returns the child <code>TreeNode</code> at index
		/// <code>childIndex</code>.
		/// </summary>
		TreeNode getChildAt(int @childIndex);

		/// <summary>
		/// Returns the number of children <code>TreeNode</code>s the receiver
		/// contains.
		/// </summary>
		int getChildCount();

		/// <summary>
		/// Returns the index of <code>node</code> in the receivers children.
		/// </summary>
		int getIndex(TreeNode @node);

		/// <summary>
		/// Returns the parent <code>TreeNode</code> of the receiver.
		/// </summary>
		TreeNode getParent();

		/// <summary>
		/// Returns true if the receiver is a leaf.
		/// </summary>
		bool isLeaf();

	}
}
