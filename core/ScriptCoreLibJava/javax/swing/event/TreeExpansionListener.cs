// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.TreeExpansionListener

using ScriptCoreLib;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/TreeExpansionListener.html
	[Script(IsNative = true)]
	public interface TreeExpansionListener : EventListener
	{
		/// <summary>
		/// Called whenever an item in the tree has been collapsed.
		/// </summary>
		void treeCollapsed(TreeExpansionEvent @event);

		/// <summary>
		/// Called whenever an item in the tree has been expanded.
		/// </summary>
		void treeExpanded(TreeExpansionEvent @event);

	}
}
