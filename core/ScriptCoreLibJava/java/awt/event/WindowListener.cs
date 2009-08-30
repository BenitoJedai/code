// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.@event;
using java.util;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/WindowListener.html
	[Script(IsNative = true)]
	public interface WindowListener : EventListener
	{
		/// <summary>
		/// Invoked when the Window is set to be the active Window.
		/// </summary>
		void windowActivated(WindowEvent @e);

		/// <summary>
		/// Invoked when a window has been closed as the result
		/// of calling dispose on the window.
		/// </summary>
		void windowClosed(WindowEvent @e);

		/// <summary>
		/// Invoked when the user attempts to close the window
		/// from the window's system menu.
		/// </summary>
		void windowClosing(WindowEvent @e);

		/// <summary>
		/// Invoked when a Window is no longer the active Window.
		/// </summary>
		void windowDeactivated(WindowEvent @e);

		/// <summary>
		/// Invoked when a window is changed from a minimized
		/// to a normal state.
		/// </summary>
		void windowDeiconified(WindowEvent @e);

		/// <summary>
		/// Invoked when a window is changed from a normal to a
		/// minimized state.
		/// </summary>
		void windowIconified(WindowEvent @e);

		/// <summary>
		/// Invoked the first time a window is made visible.
		/// </summary>
		void windowOpened(WindowEvent @e);

	}
}

