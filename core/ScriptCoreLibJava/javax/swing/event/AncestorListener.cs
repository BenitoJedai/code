// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using javax.swing.@event;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/AncestorListener.html
	[Script(IsNative = true)]
	public interface AncestorListener
	{
		/// <summary>
		/// Called when the source or one of its ancestors is made visible
		/// either by setVisible(true) being called or by its being
		/// added to the component hierarchy.
		/// </summary>
		void ancestorAdded(AncestorEvent @event);

		/// <summary>
		/// Called when either the source or one of its ancestors is moved.
		/// </summary>
		void ancestorMoved(AncestorEvent @event);

		/// <summary>
		/// Called when the source or one of its ancestors is made invisible
		/// either by setVisible(false) being called or by its being
		/// remove from the component hierarchy.
		/// </summary>
		void ancestorRemoved(AncestorEvent @event);

	}
}
