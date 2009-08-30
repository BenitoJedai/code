// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.@event;
using java.util;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/MouseMotionListener.html
	[Script(IsNative = true)]
	public interface MouseMotionListener : EventListener
	{
		/// <summary>
		/// Invoked when a mouse button is pressed on a component and then
		/// dragged.
		/// </summary>
		void mouseDragged(MouseEvent @e);

		/// <summary>
		/// Invoked when the mouse cursor has been moved onto a component
		/// but no buttons have been pushed.
		/// </summary>
		void mouseMoved(MouseEvent @e);

	}
}

