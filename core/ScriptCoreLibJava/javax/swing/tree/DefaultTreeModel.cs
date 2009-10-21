// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.tree.DefaultTreeModel

using ScriptCoreLib;
using java.lang;
using java.util;
using javax.swing.@event;

namespace javax.swing.tree
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/tree/DefaultTreeModel.html
	[Script(IsNative = true)]
	public class DefaultTreeModel
	{
		/// <summary>
		/// Creates a tree in which any node can have children.
		/// </summary>
		public DefaultTreeModel(TreeNode @root)
		{
		}

		/// <summary>
		/// Creates a tree specifying whether any node can have children,
		/// or whether only certain nodes can have children.
		/// </summary>
		public DefaultTreeModel(TreeNode @root, bool @asksAllowsChildren)
		{
		}

		/// <summary>
		/// Adds a listener for the TreeModelEvent posted after the tree changes.
		/// </summary>
		public void addTreeModelListener(TreeModelListener @l)
		{
		}

		/// <summary>
		/// Tells how leaf nodes are determined.
		/// </summary>
		public bool asksAllowsChildren()
		{
			return default(bool);
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireTreeNodesChanged(object @source, object[] @path, int[] @childIndices, object[] @children)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireTreeNodesInserted(object @source, object[] @path, int[] @childIndices, object[] @children)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireTreeNodesRemoved(object @source, object[] @path, int[] @childIndices, object[] @children)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireTreeStructureChanged(object @source, object[] @path, int[] @childIndices, object[] @children)
		{
		}

		/// <summary>
		/// Returns the child of <I>parent</I> at index <I>index</I> in the parent's
		/// child array.
		/// </summary>
		public object getChild(object @parent, int @index)
		{
			return default(object);
		}

		/// <summary>
		/// Returns the number of children of <I>parent</I>.
		/// </summary>
		public int getChildCount(object @parent)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index of child in parent.
		/// </summary>
		public int getIndexOfChild(object @parent, object @child)
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array of all the objects currently registered
		/// as <code><em>Foo</em>Listener</code>s
		/// upon this model.
		/// </summary>
		public EventListener[] getListeners(Class @listenerType)
		{
			return default(EventListener[]);
		}

		/// <summary>
		/// Builds the parents of node up to and including the root node,
		/// where the original node is the last element in the returned array.
		/// </summary>
		public TreeNode[] getPathToRoot(TreeNode @aNode)
		{
			return default(TreeNode[]);
		}

		/// <summary>
		/// Builds the parents of node up to and including the root node,
		/// where the original node is the last element in the returned array.
		/// </summary>
		protected TreeNode[] getPathToRoot(TreeNode @aNode, int @depth)
		{
			return default(TreeNode[]);
		}

		/// <summary>
		/// Returns the root of the tree.
		/// </summary>
		public object getRoot()
		{
			return default(object);
		}

		/// <summary>
		/// Returns an array of all the tree model listeners
		/// registered on this model.
		/// </summary>
		public TreeModelListener[] getTreeModelListeners()
		{
			return default(TreeModelListener[]);
		}

		/// <summary>
		/// Invoked this to insert newChild at location index in parents children.
		/// </summary>
		public void insertNodeInto(MutableTreeNode @newChild, MutableTreeNode @parent, int @index)
		{
		}

		/// <summary>
		/// Returns whether the specified node is a leaf node.
		/// </summary>
		public bool isLeaf(object @node)
		{
			return default(bool);
		}

		/// <summary>
		/// Invoke this method after you've changed how node is to be
		/// represented in the tree.
		/// </summary>
		public void nodeChanged(TreeNode @node)
		{
		}

		/// <summary>
		/// Invoke this method after you've changed how the children identified by
		/// childIndicies are to be represented in the tree.
		/// </summary>
		public void nodesChanged(TreeNode @node, int[] @childIndices)
		{
		}

		/// <summary>
		/// Invoke this method if you've totally changed the children of
		/// node and its childrens children...
		/// </summary>
		public void nodeStructureChanged(TreeNode @node)
		{
		}

		/// <summary>
		/// Invoke this method after you've inserted some TreeNodes into
		/// node.
		/// </summary>
		public void nodesWereInserted(TreeNode @node, int[] @childIndices)
		{
		}

		/// <summary>
		/// Invoke this method after you've removed some TreeNodes from
		/// node.
		/// </summary>
		public void nodesWereRemoved(TreeNode @node, int[] @childIndices, object[] @removedChildren)
		{
		}

		/// <summary>
		/// Invoke this method if you've modified the TreeNodes upon which this
		/// model depends.
		/// </summary>
		public void reload()
		{
		}

		/// <summary>
		/// Invoke this method if you've modified the TreeNodes upon which this
		/// model depends.
		/// </summary>
		public void reload(TreeNode @node)
		{
		}

		/// <summary>
		/// Message this to remove node from its parent.
		/// </summary>
		public void removeNodeFromParent(MutableTreeNode @node)
		{
		}

		/// <summary>
		/// Removes a listener previously added with <B>addTreeModelListener()</B>.
		/// </summary>
		public void removeTreeModelListener(TreeModelListener @l)
		{
		}

		/// <summary>
		/// Sets whether or not to test leafness by asking getAllowsChildren()
		/// or isLeaf() to the TreeNodes.
		/// </summary>
		public void setAsksAllowsChildren(bool @newValue)
		{
		}

		/// <summary>
		/// Sets the root to <code>root</code>.
		/// </summary>
		public void setRoot(TreeNode @root)
		{
		}

		/// <summary>
		/// This sets the user object of the TreeNode identified by path
		/// and posts a node changed.
		/// </summary>
		public void valueForPathChanged(TreePath @path, object @newValue)
		{
		}

	}
}
