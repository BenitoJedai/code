// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.TreeSelectionEvent

using ScriptCoreLib;
using java.lang;
using java.util;
using javax.swing.tree;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/TreeSelectionEvent.html
	[Script(IsNative = true)]
	public class TreeSelectionEvent : EventObject
	{
		/// <summary>
		/// Represents a change in the selection of a TreeSelectionModel.
		/// </summary>
		public TreeSelectionEvent(object @source, TreePath[] @paths, bool[] @areNew, TreePath @oldLeadSelectionPath, TreePath @newLeadSelectionPath)
		{
		}

		/// <summary>
		/// Represents a change in the selection of a TreeSelectionModel.
		/// </summary>
		public TreeSelectionEvent(object @source, TreePath @path, bool @isNew, TreePath @oldLeadSelectionPath, TreePath @newLeadSelectionPath)
		{
		}

		/// <summary>
		/// Returns a copy of the receiver, but with the source being newSource.
		/// </summary>
		public object cloneWithSource(object @newSource)
		{
			return default(object);
		}

		/// <summary>
		/// Returns the current lead path.
		/// </summary>
		public TreePath getNewLeadSelectionPath()
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns the path that was previously the lead path.
		/// </summary>
		public TreePath getOldLeadSelectionPath()
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns the first path element.
		/// </summary>
		public TreePath getPath()
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns the paths that have been added or removed from the
		/// selection.
		/// </summary>
		public TreePath[] getPaths()
		{
			return default(TreePath[]);
		}

		/// <summary>
		/// Returns true if the first path element has been added to the
		/// selection, a return value of false means the first path has been
		/// removed from the selection.
		/// </summary>
		public bool isAddedPath()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the path identified by <code>index</code> was added to
		/// the selection.
		/// </summary>
		public bool isAddedPath(int @index)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the path identified by path was added to the
		/// selection.
		/// </summary>
		public bool isAddedPath(TreePath @path)
		{
			return default(bool);
		}

	}
}
