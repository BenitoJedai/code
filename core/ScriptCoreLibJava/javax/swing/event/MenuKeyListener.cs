// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using javax.swing.@event;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/MenuKeyListener.html
	[Script(IsNative = true)]
	public interface MenuKeyListener : EventListener
	{
		/// <summary>
		/// Invoked when a key has been pressed.
		/// </summary>
		void menuKeyPressed(MenuKeyEvent @e);

		/// <summary>
		/// Invoked when a key has been released.
		/// </summary>
		void menuKeyReleased(MenuKeyEvent @e);

		/// <summary>
		/// Invoked when a key has been typed.
		/// </summary>
		void menuKeyTyped(MenuKeyEvent @e);

	}
}

