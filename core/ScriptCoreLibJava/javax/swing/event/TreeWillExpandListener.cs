// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.TreeWillExpandListener

using ScriptCoreLib;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/TreeWillExpandListener.html
	[Script(IsNative = true)]
	public interface TreeWillExpandListener : EventListener
	{
		/// <summary>
		/// Invoked whenever a node in the tree is about to be collapsed.
		/// </summary>
		void treeWillCollapse(TreeExpansionEvent @event);

		/// <summary>
		/// Invoked whenever a node in the tree is about to be expanded.
		/// </summary>
		void treeWillExpand(TreeExpansionEvent @event);

	}
}
