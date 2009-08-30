// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using javax.swing;
using java.awt;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/MenuKeyEvent.html
	[Script(IsNative = true)]
	public class MenuKeyEvent
	{
		/// <summary>
		/// Constructs a MenuKeyEvent object.
		/// </summary>
		public MenuKeyEvent(Component @source, int @id, long @when, int @modifiers, int @keyCode, char @keyChar, MenuElement[] @p, MenuSelectionManager @m)
		{
		}

		/// <summary>
		/// Returns the current menu selection manager.
		/// </summary>
		public MenuSelectionManager getMenuSelectionManager()
		{
			return default(MenuSelectionManager);
		}

		/// <summary>
		/// Returns the path to the menu item referenced by this event.
		/// </summary>
		public MenuElement[] getPath()
		{
			return default(MenuElement[]);
		}

	}
}

