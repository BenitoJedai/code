// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using javax.swing.@event;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/MenuListener.html
	[Script(IsNative = true)]
	public interface MenuListener : EventListener
	{
		/// <summary>
		/// Invoked when the menu is canceled.
		/// </summary>
		void menuCanceled(MenuEvent @e);

		/// <summary>
		/// Invoked when the menu is deselected.
		/// </summary>
		void menuDeselected(MenuEvent @e);

		/// <summary>
		/// Invoked when a menu is selected.
		/// </summary>
		void menuSelected(MenuEvent @e);

	}
}

