// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using javax.swing;
using java.awt;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/MenuDragMouseEvent.html
	[Script(IsNative = true)]
	public class MenuDragMouseEvent
	{
		/// <summary>
		/// Constructs a MenuDragMouseEvent object.
		/// </summary>
		public MenuDragMouseEvent(Component @source, int @id, long @when, int @modifiers, int @x, int @y, int @clickCount, bool @popupTrigger, MenuElement[] @p, MenuSelectionManager @m)
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
		/// Returns the path to the selected menu item.
		/// </summary>
		public MenuElement[] getPath()
		{
			return default(MenuElement[]);
		}

	}
}

