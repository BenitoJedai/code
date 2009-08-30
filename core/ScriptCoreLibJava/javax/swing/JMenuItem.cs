// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.beans;
using java.lang;
using javax.accessibility;
using javax.swing;
using javax.swing.@event;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JMenuItem.html
	[Script(IsNative = true)]
	public class JMenuItem : AbstractButton
	{
		/// <summary>
		/// Creates a <code>JMenuItem</code> with no set text or icon.
		/// </summary>
		public JMenuItem()
		{
		}

		/// <summary>
		/// Creates a menu item whose properties are taken from the
		/// specified <code>Action</code>.
		/// </summary>
		public JMenuItem(Action @a)
		{
		}

		/// <summary>
		/// Creates a <code>JMenuItem</code> with the specified icon.
		/// </summary>
		public JMenuItem(Icon @icon)
		{
		}

		/// <summary>
		/// Creates a <code>JMenuItem</code> with the specified text.
		/// </summary>
		public JMenuItem(string @text)
		{
		}

		/// <summary>
		/// Creates a <code>JMenuItem</code> with the specified text and icon.
		/// </summary>
		public JMenuItem(string @text, Icon @icon)
		{
		}

		/// <summary>
		/// Creates a <code>JMenuItem</code> with the specified text and
		/// keyboard mnemonic.
		/// </summary>
		public JMenuItem(string @text, int @mnemonic)
		{
		}

		/// <summary>
		/// Adds a <code>MenuDragMouseListener</code> to the menu item.
		/// </summary>
		public void addMenuDragMouseListener(MenuDragMouseListener @l)
		{
		}

		/// <summary>
		/// Adds a <code>MenuKeyListener</code> to the menu item.
		/// </summary>
		public void addMenuKeyListener(MenuKeyListener @l)
		{
		}

		/// <summary>
		/// Factory method which sets the <code>ActionEvent</code> source's
		/// properties according to values from the <code>Action</code> instance.
		/// </summary>
		protected void configurePropertiesFromAction(Action @a)
		{
		}

		/// <summary>
		/// Factory method which creates the <code>PropertyChangeListener</code>
		/// used to update the <code>ActionEvent</code> source as properties
		/// change on its <code>Action</code> instance.
		/// </summary>
		public PropertyChangeListener createActionPropertyChangeListener(Action @a)
		{
			return default(PropertyChangeListener);
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireMenuDragMouseDragged(MenuDragMouseEvent @event)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireMenuDragMouseEntered(MenuDragMouseEvent @event)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireMenuDragMouseExited(MenuDragMouseEvent @event)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireMenuDragMouseReleased(MenuDragMouseEvent @event)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireMenuKeyPressed(MenuKeyEvent @event)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireMenuKeyReleased(MenuKeyEvent @event)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireMenuKeyTyped(MenuKeyEvent @event)
		{
		}

		/// <summary>
		/// Returns the <code>KeyStroke</code> which serves as an accelerator
		/// for the menu item.
		/// </summary>
		public KeyStroke getAccelerator()
		{
			return default(KeyStroke);
		}

		/// <summary>
		/// Returns the <code>AccessibleContext</code> associated with this
		/// <code>JMenuItem</code>.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the <code>java.awt.Component</code> used to paint
		/// this object.
		/// </summary>
		public Component getComponent()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns an array of all the <code>MenuDragMouseListener</code>s added
		/// to this JMenuItem with addMenuDragMouseListener().
		/// </summary>
		public MenuDragMouseListener[] getMenuDragMouseListeners()
		{
			return default(MenuDragMouseListener[]);
		}

		/// <summary>
		/// Returns an array of all the <code>MenuKeyListener</code>s added
		/// to this JMenuItem with addMenuKeyListener().
		/// </summary>
		public MenuKeyListener[] getMenuKeyListeners()
		{
			return default(MenuKeyListener[]);
		}

		/// <summary>
		/// This method returns an array containing the sub-menu
		/// components for this menu component.
		/// </summary>
		public MenuElement[] getSubElements()
		{
			return default(MenuElement[]);
		}

		/// <summary>
		/// Returns the suffix used to construct the name of the L&F class used to
		/// render this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Initializes the menu item with the specified text and icon.
		/// </summary>
		protected void init(string @text, Icon @icon)
		{
		}

		/// <summary>
		/// Returns whether the menu item is "armed".
		/// </summary>
		public bool isArmed()
		{
			return default(bool);
		}

		/// <summary>
		/// Called by the <code>MenuSelectionManager</code> when the
		/// <code>MenuElement</code> is selected or unselected.
		/// </summary>
		public void menuSelectionChanged(bool @isIncluded)
		{
		}

		/// <summary>
		/// Returns a string representation of this <code>JMenuItem</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Processes a key event forwarded from the
		/// <code>MenuSelectionManager</code> and changes the menu selection,
		/// if necessary, by using <code>MenuSelectionManager</code>'s API.
		/// </summary>
		public void processKeyEvent(KeyEvent @e, MenuElement[] @path, MenuSelectionManager @manager)
		{
		}

		/// <summary>
		/// Handles mouse drag in a menu.
		/// </summary>
		public void processMenuDragMouseEvent(MenuDragMouseEvent @e)
		{
		}

		/// <summary>
		/// Handles a keystroke in a menu.
		/// </summary>
		public void processMenuKeyEvent(MenuKeyEvent @e)
		{
		}

		/// <summary>
		/// Processes a mouse event forwarded from the
		/// <code>MenuSelectionManager</code> and changes the menu
		/// selection, if necessary, by using the
		/// <code>MenuSelectionManager</code>'s API.
		/// </summary>
		public void processMouseEvent(MouseEvent @e, MenuElement[] @path, MenuSelectionManager @manager)
		{
		}

		/// <summary>
		/// Removes a <code>MenuDragMouseListener</code> from the menu item.
		/// </summary>
		public void removeMenuDragMouseListener(MenuDragMouseListener @l)
		{
		}

		/// <summary>
		/// Removes a <code>MenuKeyListener</code> from the menu item.
		/// </summary>
		public void removeMenuKeyListener(MenuKeyListener @l)
		{
		}

		/// <summary>
		/// Sets the key combination which invokes the menu item's
		/// action listeners without navigating the menu hierarchy.
		/// </summary>
		public void setAccelerator(KeyStroke @keyStroke)
		{
		}

		/// <summary>
		/// Identifies the menu item as "armed".
		/// </summary>
		public void setArmed(bool @b)
		{
		}

		/// <summary>
		/// Enables or disables the menu item.
		/// </summary>
		public void setEnabled(bool @b)
		{
		}

		/// <summary>
		/// Sets the look and feel object that renders this component.
		/// </summary>
		public void setUI(MenuItemUI @ui)
		{
		}

		/// <summary>
		/// Resets the UI property with a value from the current look and feel.
		/// </summary>
		public void updateUI()
		{
		}

	}
}

