// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.@event;
using java.util;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/WindowFocusListener.html
	[Script(IsNative = true)]
	public interface WindowFocusListener : EventListener
	{
		/// <summary>
		/// Invoked when the Window is set to be the focused Window, which means
		/// that the Window, or one of its subcomponents, will receive keyboard
		/// events.
		/// </summary>
		void windowGainedFocus(WindowEvent @e);

		/// <summary>
		/// Invoked when the Window is no longer the focused Window, which means
		/// that keyboard events will no longer be delivered to the Window or any of
		/// its subcomponents.
		/// </summary>
		void windowLostFocus(WindowEvent @e);

	}
}

