// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using javax.swing;
using javax.swing.@event;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/MenuSelectionManager.html
	[Script(IsNative = true)]
	public class MenuSelectionManager
	{
		/// <summary>
		/// 
		/// </summary>
		public MenuSelectionManager()
		{
		}

		/// <summary>
		/// Adds a ChangeListener to the button.
		/// </summary>
		public void addChangeListener(ChangeListener @l)
		{
		}

		/// <summary>
		/// Tell the menu selection to close and unselect all the menu components.
		/// </summary>
		public void clearSelectedPath()
		{
		}

		/// <summary>
		/// Returns the component in the currently selected path
		/// which contains sourcePoint.
		/// </summary>
		public Component componentForPoint(Component @source, Point @sourcePoint)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the default menu selection manager.
		/// </summary>
		public MenuSelectionManager defaultManager()
		{
			return default(MenuSelectionManager);
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireStateChanged()
		{
		}

		/// <summary>
		/// Returns an array of all the <code>ChangeListener</code>s added
		/// to this MenuSelectionManager with addChangeListener().
		/// </summary>
		public ChangeListener[] getChangeListeners()
		{
			return default(ChangeListener[]);
		}

		/// <summary>
		/// Returns the path to the currently selected menu item
		/// </summary>
		public MenuElement[] getSelectedPath()
		{
			return default(MenuElement[]);
		}

		/// <summary>
		/// Return true if c is part of the currently used menu
		/// </summary>
		public bool isComponentPartOfCurrentMenu(Component @c)
		{
			return default(bool);
		}

		/// <summary>
		/// When a MenuElement receives an event from a KeyListener, it should never process the event
		/// directly.
		/// </summary>
		public void processKeyEvent(KeyEvent @e)
		{
		}

		/// <summary>
		/// When a MenuElement receives an event from a MouseListener, it should never process the event
		/// directly.
		/// </summary>
		public void processMouseEvent(MouseEvent @event)
		{
		}

		/// <summary>
		/// Removes a ChangeListener from the button.
		/// </summary>
		public void removeChangeListener(ChangeListener @l)
		{
		}

		/// <summary>
		/// Change the selection in the menu hierarchy.
		/// </summary>
		public void setSelectedPath(MenuElement[] @path)
		{
		}

	}
}

