// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.TreeModelListener

using ScriptCoreLib;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/TreeModelListener.html
	[Script(IsNative = true)]
	public interface TreeModelListener : EventListener
	{
		/// <summary>
		/// Invoked after a node (or a set of siblings) has changed in some
		/// way.
		/// </summary>
		void treeNodesChanged(TreeModelEvent @e);

		/// <summary>
		/// Invoked after nodes have been inserted into the tree.
		/// </summary>
		void treeNodesInserted(TreeModelEvent @e);

		/// <summary>
		/// Invoked after nodes have been removed from the tree.
		/// </summary>
		void treeNodesRemoved(TreeModelEvent @e);

		/// <summary>
		/// Invoked after the tree has drastically changed structure from a
		/// given node down.
		/// </summary>
		void treeStructureChanged(TreeModelEvent @e);

	}
}
