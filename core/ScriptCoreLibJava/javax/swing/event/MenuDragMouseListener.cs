// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using javax.swing.@event;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/MenuDragMouseListener.html
	[Script(IsNative = true)]
	public interface MenuDragMouseListener
	{
		/// <summary>
		/// Invoked when the mouse is being dragged in a menu component's
		/// display area.
		/// </summary>
		void menuDragMouseDragged(MenuDragMouseEvent @e);

		/// <summary>
		/// Invoked when the dragged mouse has entered a menu component's
		/// display area.
		/// </summary>
		void menuDragMouseEntered(MenuDragMouseEvent @e);

		/// <summary>
		/// Invoked when the dragged mouse has left a menu component's
		/// display area.
		/// </summary>
		void menuDragMouseExited(MenuDragMouseEvent @e);

		/// <summary>
		/// Invoked when a dragged mouse is release in a menu component's
		/// display area.
		/// </summary>
		void menuDragMouseReleased(MenuDragMouseEvent @e);

	}
}

