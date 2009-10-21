// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.tree.TreeModel

using ScriptCoreLib;
using java.lang;
using javax.swing.@event;

namespace javax.swing.tree
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/tree/TreeModel.html
	[Script(IsNative = true)]
	public interface TreeModel
	{
		/// <summary>
		/// Adds a listener for the <code>TreeModelEvent</code>
		/// posted after the tree changes.
		/// </summary>
		void addTreeModelListener(TreeModelListener @l);

		/// <summary>
		/// Returns the child of <code>parent</code> at index <code>index</code>
		/// in the parent's
		/// child array.
		/// </summary>
		object getChild(object @parent, int @index);

		/// <summary>
		/// Returns the number of children of <code>parent</code>.
		/// </summary>
		int getChildCount(object @parent);

		/// <summary>
		/// Returns the index of child in parent.
		/// </summary>
		int getIndexOfChild(object @parent, object @child);

		/// <summary>
		/// Returns the root of the tree.
		/// </summary>
		object getRoot();

		/// <summary>
		/// Returns <code>true</code> if <code>node</code> is a leaf.
		/// </summary>
		bool isLeaf(object @node);

		/// <summary>
		/// Removes a listener previously added with
		/// <code>addTreeModelListener</code>.
		/// </summary>
		void removeTreeModelListener(TreeModelListener @l);

		/// <summary>
		/// Messaged when the user has altered the value for the item identified
		/// by <code>path</code> to <code>newValue</code>.
		/// </summary>
		void valueForPathChanged(TreePath @path, object @newValue);

	}
}
