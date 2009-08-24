// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.@event;

namespace robocode.robotinterfaces
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/IInteractiveEvents.html
	[Script(IsNative = true)]
	public interface IInteractiveEvents
	{
		/// <summary>
		/// This method is called when a key has been pressed.
		/// </summary>
		void onKeyPressed(KeyEvent @event);

		/// <summary>
		/// This method is called when a key has been released.
		/// </summary>
		void onKeyReleased(KeyEvent @event);

		/// <summary>
		/// This method is called when a key has been typed (pressed and released).
		/// </summary>
		void onKeyTyped(KeyEvent @event);

		/// <summary>
		/// This method is called when a mouse button has been clicked (pressed and
		/// released).
		/// </summary>
		void onMouseClicked(MouseEvent @event);

		/// <summary>
		/// This method is called when a mouse button has been pressed and then
		/// dragged.
		/// </summary>
		void onMouseDragged(MouseEvent @event);

		/// <summary>
		/// This method is called when the mouse has entered the battle view.
		/// </summary>
		void onMouseEntered(MouseEvent @event);

		/// <summary>
		/// This method is called when the mouse has exited the battle view.
		/// </summary>
		void onMouseExited(MouseEvent @event);

		/// <summary>
		/// This method is called when the mouse has been moved.
		/// </summary>
		void onMouseMoved(MouseEvent @event);

		/// <summary>
		/// This method is called when a mouse button has been pressed.
		/// </summary>
		void onMousePressed(MouseEvent @event);

		/// <summary>
		/// This method is called when a mouse button has been released.
		/// </summary>
		void onMouseReleased(MouseEvent @event);

		/// <summary>
		/// This method is called when the mouse wheel has been rotated.
		/// </summary>
		void onMouseWheelMoved(MouseWheelEvent @event);

	}
}
