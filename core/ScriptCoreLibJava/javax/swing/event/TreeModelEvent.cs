// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.TreeModelEvent

using ScriptCoreLib;
using java.lang;
using java.util;
using javax.swing.tree;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/TreeModelEvent.html
	[Script(IsNative = true)]
	public class TreeModelEvent : EventObject
	{
		/// <summary>
		/// Used to create an event when the node structure has changed in some way,
		/// identifying the path to the root of a modified subtree as an array of
		/// Objects.
		/// </summary>
		public TreeModelEvent(object @source, object[] @path)
		{
		}

		/// <summary>
		/// Used to create an event when nodes have been changed, inserted, or
		/// removed, identifying the path to the parent of the modified items as
		/// an array of Objects.
		/// </summary>
		public TreeModelEvent(object @source, object[] @path, int[] @childIndices, object[] @children)
		{
		}

		/// <summary>
		/// Used to create an event when the node structure has changed in some way,
		/// identifying the path to the root of the modified subtree as a TreePath
		/// object.
		/// </summary>
		public TreeModelEvent(object @source, TreePath @path)
		{
		}

		/// <summary>
		/// Used to create an event when nodes have been changed, inserted, or
		/// removed, identifying the path to the parent of the modified items as
		/// a TreePath object.
		/// </summary>
		public TreeModelEvent(object @source, TreePath @path, int[] @childIndices, object[] @children)
		{
		}

		/// <summary>
		/// Returns the values of the child indexes.
		/// </summary>
		public int[] getChildIndices()
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the objects that are children of the node identified by
		/// <code>getPath</code> at the locations specified by
		/// <code>getChildIndices</code>.
		/// </summary>
		public object[] getChildren()
		{
			return default(object[]);
		}

		/// <summary>
		/// Convenience method to get the array of objects from the TreePath
		/// instance that this event wraps.
		/// </summary>
		public object[] getPath()
		{
			return default(object[]);
		}

		/// <summary>
		/// For all events, except treeStructureChanged,
		/// returns the parent of the changed nodes.
		/// </summary>
		public TreePath getTreePath()
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns a string that displays and identifies this object's
		/// properties.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}
