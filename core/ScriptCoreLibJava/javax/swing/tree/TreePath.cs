// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.tree.TreePath

using ScriptCoreLib;
using java.lang;

namespace javax.swing.tree
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/tree/TreePath.html
	[Script(IsNative = true)]
	public class TreePath
	{
		/// <summary>
		/// Primarily provided for subclasses
		/// that represent paths in a different manner.
		/// </summary>
		public TreePath()
		{
		}

		/// <summary>
		/// Constructs a TreePath containing only a single element.
		/// </summary>
		public TreePath(object @singlePath)
		{
		}

		/// <summary>
		/// Constructs a path from an array of Objects, uniquely identifying
		/// the path from the root of the tree to a specific node, as returned
		/// by the tree's data model.
		/// </summary>
		public TreePath(object[] @path)
		{
		}

		/// <summary>
		/// Constructs a new TreePath with the identified path components of
		/// length <code>length</code>.
		/// </summary>
		public TreePath(object[] @path, int @length)
		{
		}

		/// <summary>
		/// Constructs a new TreePath, which is the path identified by
		/// <code>parent</code> ending in <code>lastElement</code>.
		/// </summary>
		public TreePath(TreePath @parent, object @lastElement)
		{
		}

		/// <summary>
		/// Tests two TreePaths for equality by checking each element of the
		/// paths for equality.
		/// </summary>
		public override bool Equals(object @o)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the last component of this path.
		/// </summary>
		public object getLastPathComponent()
		{
			return default(object);
		}

		/// <summary>
		/// Returns a path containing all the elements of this object, except
		/// the last path component.
		/// </summary>
		public TreePath getParentPath()
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns an ordered array of Objects containing the components of this
		/// TreePath.
		/// </summary>
		public object[] getPath()
		{
			return default(object[]);
		}

		/// <summary>
		/// Returns the path component at the specified index.
		/// </summary>
		public object getPathComponent(int @element)
		{
			return default(object);
		}

		/// <summary>
		/// Returns the number of elements in the path.
		/// </summary>
		public int getPathCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the hashCode for the object.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Returns true if <code>aTreePath</code> is a
		/// descendant of this
		/// TreePath.
		/// </summary>
		public bool isDescendant(TreePath @aTreePath)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a new path containing all the elements of this object
		/// plus <code>child</code>.
		/// </summary>
		public TreePath pathByAddingChild(object @child)
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns a string that displays and identifies this
		/// object's properties.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}
