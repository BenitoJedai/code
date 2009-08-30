// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.@event;
using java.util;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/HierarchyBoundsListener.html
	[Script(IsNative = true)]
	public interface HierarchyBoundsListener : EventListener
	{
		/// <summary>
		/// Called when an ancestor of the source is moved.
		/// </summary>
		void ancestorMoved(HierarchyEvent @e);

		/// <summary>
		/// Called when an ancestor of the source is resized.
		/// </summary>
		void ancestorResized(HierarchyEvent @e);

	}
}

