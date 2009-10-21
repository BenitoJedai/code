// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.tree.DefaultMutableTreeNode

using ScriptCoreLib;
using java.lang;
using java.util;

namespace javax.swing.tree
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/tree/DefaultMutableTreeNode.html
	[Script(IsNative = true)]
	public class DefaultMutableTreeNode : MutableTreeNode
	{
		/// <summary>
		/// Creates a tree node that has no parent and no children, but which
		/// allows children.
		/// </summary>
		public DefaultMutableTreeNode()
		{
		}

		/// <summary>
		/// Creates a tree node with no parent, no children, but which allows
		/// children, and initializes it with the specified user object.
		/// </summary>
		public DefaultMutableTreeNode(object @userObject)
		{
		}

		/// <summary>
		/// Creates a tree node with no parent, no children, initialized with
		/// the specified user object, and that allows children only if
		/// specified.
		/// </summary>
		public DefaultMutableTreeNode(object @userObject, bool @allowsChildren)
		{
		}

		/// <summary>
		/// Removes <code>newChild</code> from its parent and makes it a child of
		/// this node by adding it to the end of this node's child array.
		/// </summary>
		public void add(MutableTreeNode @newChild)
		{
		}

		/// <summary>
		/// Creates and returns an enumeration that traverses the subtree rooted at
		/// this node in breadth-first order.
		/// </summary>
		public Enumeration breadthFirstEnumeration()
		{
			return default(Enumeration);
		}

		/// <summary>
		/// Creates and returns a forward-order enumeration of this node's
		/// children.
		/// </summary>
		public Enumeration children()
		{
			return default(Enumeration);
		}

		/// <summary>
		/// Overridden to make clone public.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Creates and returns an enumeration that traverses the subtree rooted at
		/// this node in depth-first order.
		/// </summary>
		public Enumeration depthFirstEnumeration()
		{
			return default(Enumeration);
		}

		/// <summary>
		/// Returns true if this node is allowed to have children.
		/// </summary>
		public bool getAllowsChildren()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the child in this node's child array that immediately
		/// follows <code>aChild</code>, which must be a child of this node.
		/// </summary>
		public TreeNode getChildAfter(TreeNode @aChild)
		{
			return default(TreeNode);
		}

		/// <summary>
		/// Returns the child at the specified index in this node's child array.
		/// </summary>
		public TreeNode getChildAt(int @index)
		{
			return default(TreeNode);
		}

		/// <summary>
		/// Returns the child in this node's child array that immediately
		/// precedes <code>aChild</code>, which must be a child of this node.
		/// </summary>
		public TreeNode getChildBefore(TreeNode @aChild)
		{
			return default(TreeNode);
		}

		/// <summary>
		/// Returns the number of children of this node.
		/// </summary>
		public int getChildCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the depth of the tree rooted at this node -- the longest
		/// distance from this node to a leaf.
		/// </summary>
		public int getDepth()
		{
			return default(int);
		}

		/// <summary>
		/// Returns this node's first child.
		/// </summary>
		public TreeNode getFirstChild()
		{
			return default(TreeNode);
		}

		/// <summary>
		/// Finds and returns the first leaf that is a descendant of this node --
		/// either this node or its first child's first leaf.
		/// </summary>
		public DefaultMutableTreeNode getFirstLeaf()
		{
			return default(DefaultMutableTreeNode);
		}

		/// <summary>
		/// Returns the index of the specified child in this node's child array.
		/// </summary>
		public int getIndex(TreeNode @aChild)
		{
			return default(int);
		}

		/// <summary>
		/// Returns this node's last child.
		/// </summary>
		public TreeNode getLastChild()
		{
			return default(TreeNode);
		}

		/// <summary>
		/// Finds and returns the last leaf that is a descendant of this node --
		/// either this node or its last child's last leaf.
		/// </summary>
		public DefaultMutableTreeNode getLastLeaf()
		{
			return default(DefaultMutableTreeNode);
		}

		/// <summary>
		/// Returns the total number of leaves that are descendants of this node.
		/// </summary>
		public int getLeafCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of levels above this node -- the distance from
		/// the root to this node.
		/// </summary>
		public int getLevel()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the leaf after this node or null if this node is the
		/// last leaf in the tree.
		/// </summary>
		public DefaultMutableTreeNode getNextLeaf()
		{
			return default(DefaultMutableTreeNode);
		}

		/// <summary>
		/// Returns the node that follows this node in a preorder traversal of this
		/// node's tree.
		/// </summary>
		public DefaultMutableTreeNode getNextNode()
		{
			return default(DefaultMutableTreeNode);
		}

		/// <summary>
		/// Returns the next sibling of this node in the parent's children array.
		/// </summary>
		public DefaultMutableTreeNode getNextSibling()
		{
			return default(DefaultMutableTreeNode);
		}

		/// <summary>
		/// Returns this node's parent or null if this node has no parent.
		/// </summary>
		public TreeNode getParent()
		{
			return default(TreeNode);
		}

		/// <summary>
		/// Returns the path from the root, to get to this node.
		/// </summary>
		public TreeNode[] getPath()
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
		/// Returns the leaf before this node or null if this node is the
		/// first leaf in the tree.
		/// </summary>
		public DefaultMutableTreeNode getPreviousLeaf()
		{
			return default(DefaultMutableTreeNode);
		}

		/// <summary>
		/// Returns the node that precedes this node in a preorder traversal of
		/// this node's tree.
		/// </summary>
		public DefaultMutableTreeNode getPreviousNode()
		{
			return default(DefaultMutableTreeNode);
		}

		/// <summary>
		/// Returns the previous sibling of this node in the parent's children
		/// array.
		/// </summary>
		public DefaultMutableTreeNode getPreviousSibling()
		{
			return default(DefaultMutableTreeNode);
		}

		/// <summary>
		/// Returns the root of the tree that contains this node.
		/// </summary>
		public TreeNode getRoot()
		{
			return default(TreeNode);
		}

		/// <summary>
		/// Returns the nearest common ancestor to this node and <code>aNode</code>.
		/// </summary>
		public TreeNode getSharedAncestor(DefaultMutableTreeNode @aNode)
		{
			return default(TreeNode);
		}

		/// <summary>
		/// Returns the number of siblings of this node.
		/// </summary>
		public int getSiblingCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns this node's user object.
		/// </summary>
		public object getUserObject()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the user object path, from the root, to get to this node.
		/// </summary>
		public object[] getUserObjectPath()
		{
			return default(object[]);
		}

		/// <summary>
		/// Removes <code>newChild</code> from its present parent (if it has a
		/// parent), sets the child's parent to this node, and then adds the child
		/// to this node's child array at index <code>childIndex</code>.
		/// </summary>
		public void insert(MutableTreeNode @newChild, int @childIndex)
		{
		}

		/// <summary>
		/// Returns true if this node has no children.
		/// </summary>
		public bool isLeaf()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if <code>anotherNode</code> is an ancestor of this node
		/// -- if it is this node, this node's parent, or an ancestor of this
		/// node's parent.
		/// </summary>
		public bool isNodeAncestor(TreeNode @anotherNode)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if <code>aNode</code> is a child of this node.
		/// </summary>
		public bool isNodeChild(TreeNode @aNode)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if <code>anotherNode</code> is a descendant of this node
		/// -- if it is this node, one of this node's children, or a descendant of
		/// one of this node's children.
		/// </summary>
		public bool isNodeDescendant(DefaultMutableTreeNode @anotherNode)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if and only if <code>aNode</code> is in the same tree
		/// as this node.
		/// </summary>
		public bool isNodeRelated(DefaultMutableTreeNode @aNode)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if <code>anotherNode</code> is a sibling of (has the
		/// same parent as) this node.
		/// </summary>
		public bool isNodeSibling(TreeNode @anotherNode)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if this node is the root of the tree.
		/// </summary>
		public bool isRoot()
		{
			return default(bool);
		}

		/// <summary>
		/// Creates and returns an enumeration that follows the path from
		/// <code>ancestor</code> to this node.
		/// </summary>
		public Enumeration pathFromAncestorEnumeration(TreeNode @ancestor)
		{
			return default(Enumeration);
		}

		/// <summary>
		/// Creates and returns an enumeration that traverses the subtree rooted at
		/// this node in postorder.
		/// </summary>
		public Enumeration postorderEnumeration()
		{
			return default(Enumeration);
		}

		/// <summary>
		/// Creates and returns an enumeration that traverses the subtree rooted at
		/// this node in preorder.
		/// </summary>
		public Enumeration preorderEnumeration()
		{
			return default(Enumeration);
		}

		/// <summary>
		/// Removes the child at the specified index from this node's children
		/// and sets that node's parent to null.
		/// </summary>
		public void remove(int @childIndex)
		{
		}

		/// <summary>
		/// Removes <code>aChild</code> from this node's child array, giving it a
		/// null parent.
		/// </summary>
		public void remove(MutableTreeNode @aChild)
		{
		}

		/// <summary>
		/// Removes all of this node's children, setting their parents to null.
		/// </summary>
		public void removeAllChildren()
		{
		}

		/// <summary>
		/// Removes the subtree rooted at this node from the tree, giving this
		/// node a null parent.
		/// </summary>
		public void removeFromParent()
		{
		}

		/// <summary>
		/// Determines whether or not this node is allowed to have children.
		/// </summary>
		public void setAllowsChildren(bool @allows)
		{
		}

		/// <summary>
		/// Sets this node's parent to <code>newParent</code> but does not
		/// change the parent's child array.
		/// </summary>
		public void setParent(MutableTreeNode @newParent)
		{
		}

		/// <summary>
		/// Sets the user object for this node to <code>userObject</code>.
		/// </summary>
		public void setUserObject(object @userObject)
		{
		}

		/// <summary>
		/// Returns the result of sending <code>toString()</code> to this node's
		/// user object, or null if this node has no user object.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}
