// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.beans;
using java.lang;
using javax.accessibility;
using javax.swing;
using javax.swing.@event;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JMenu.html
	[Script(IsNative = true)]
	public class JMenu : JMenuItem
	{
		[Script(IsNative = true)]
		public class WinListener
		{
		}
		

		/// <summary>
		/// Constructs a new <code>JMenu</code> with no text.
		/// </summary>
		public JMenu()
		{
		}

		/// <summary>
		/// Constructs a menu whose properties are taken from the
		/// <code>Action</code> supplied.
		/// </summary>
		public JMenu(Action @a)
		{
		}

		/// <summary>
		/// Constructs a new <code>JMenu</code> with the supplied string
		/// as its text.
		/// </summary>
		public JMenu(string @s)
		{
		}

		/// <summary>
		/// Constructs a new <code>JMenu</code> with the supplied string as
		/// its text and specified as a tear-off menu or not.
		/// </summary>
		public JMenu(string @s, bool @b)
		{
		}

		/// <summary>
		/// Creates a new menu item attached to the specified
		/// <code>Action</code> object and appends it to the end of this menu.
		/// </summary>
		public JMenuItem add(Action @a)
		{
			return default(JMenuItem);
		}

		/// <summary>
		/// Appends a component to the end of this menu.
		/// </summary>
		public Component add(Component @c)
		{
			return default(Component);
		}

		/// <summary>
		/// Adds the specified component to this container at the given
		/// position.
		/// </summary>
		public Component add(Component @c, int @index)
		{
			return default(Component);
		}

		/// <summary>
		/// Appends a menu item to the end of this menu.
		/// </summary>
		public JMenuItem add(JMenuItem @menuItem)
		{
			return default(JMenuItem);
		}

		/// <summary>
		/// Creates a new menu item with the specified text and appends
		/// it to the end of this menu.
		/// </summary>
		public JMenuItem add(string @s)
		{
			return default(JMenuItem);
		}

		/// <summary>
		/// Adds a listener for menu events.
		/// </summary>
		public void addMenuListener(MenuListener @l)
		{
		}

		/// <summary>
		/// Appends a new separator to the end of the menu.
		/// </summary>
		public void addSeparator()
		{
		}

		/// <summary>
		/// Sets the <code>ComponentOrientation</code> property of this menu
		/// and all components contained within it.
		/// </summary>
		public void applyComponentOrientation(ComponentOrientation @o)
		{
		}

		/// <summary>
		/// Factory method which sets the <code>ActionEvent</code>
		/// source's properties according to values from the
		/// <code>Action</code> instance.
		/// </summary>
		protected void configurePropertiesFromAction(Action @a)
		{
		}

		/// <summary>
		/// Returns a properly configured <code>PropertyChangeListener</code>
		/// which updates the control as changes to the <code>Action</code> occur.
		/// </summary>
		public PropertyChangeListener createActionChangeListener(JMenuItem @b)
		{
			return default(PropertyChangeListener);
		}

		/// <summary>
		/// Factory method which creates the <code>JMenuItem</code> for
		/// <code>Action</code>s added to the <code>JMenu</code>.
		/// </summary>
		public JMenuItem createActionComponent(Action @a)
		{
			return default(JMenuItem);
		}

		/// <summary>
		/// Creates a window-closing listener for the popup.
		/// </summary>
		public JMenu.WinListener createWinListener(JPopupMenu @p)
		{
			return default(JMenu.WinListener);
		}

		/// <summary>
		/// Programmatically performs a "click".
		/// </summary>
		public void doClick(int @pressTime)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireMenuCanceled()
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireMenuDeselected()
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireMenuSelected()
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JMenu.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the <code>java.awt.Component</code> used to
		/// paint this <code>MenuElement</code>.
		/// </summary>
		public Component getComponent()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the suggested delay, in milliseconds, before submenus
		/// are popped up or down.
		/// </summary>
		public int getDelay()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the <code>JMenuItem</code> at the specified position.
		/// </summary>
		public JMenuItem getItem(int @pos)
		{
			return default(JMenuItem);
		}

		/// <summary>
		/// Returns the number of items on the menu, including separators.
		/// </summary>
		public int getItemCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the component at position <code>n</code>.
		/// </summary>
		public Component getMenuComponent(int @n)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the number of components on the menu.
		/// </summary>
		public int getMenuComponentCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array of <code>Component</code>s of the menu's
		/// subcomponents.
		/// </summary>
		public Component[] getMenuComponents()
		{
			return default(Component[]);
		}

		/// <summary>
		/// Returns an array of all the <code>MenuListener</code>s added
		/// to this JMenu with addMenuListener().
		/// </summary>
		public MenuListener[] getMenuListeners()
		{
			return default(MenuListener[]);
		}

		/// <summary>
		/// Returns the popupmenu associated with this menu.
		/// </summary>
		public JPopupMenu getPopupMenu()
		{
			return default(JPopupMenu);
		}

		/// <summary>
		/// Computes the origin for the <code>JMenu</code>'s popup menu.
		/// </summary>
		public Point getPopupMenuOrigin()
		{
			return default(Point);
		}

		/// <summary>
		/// Returns an array of <code>MenuElement</code>s containing the submenu
		/// for this menu component.
		/// </summary>
		public MenuElement[] getSubElements()
		{
			return default(MenuElement[]);
		}

		/// <summary>
		/// Returns the name of the L&F class that renders this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Inserts a new menu item attached to the specified <code>Action</code>
		/// object at a given position.
		/// </summary>
		public JMenuItem insert(Action @a, int @pos)
		{
			return default(JMenuItem);
		}

		/// <summary>
		/// Inserts the specified <code>JMenuitem</code> at a given position.
		/// </summary>
		public JMenuItem insert(JMenuItem @mi, int @pos)
		{
			return default(JMenuItem);
		}

		/// <summary>
		/// Inserts a new menu item with the specified text at a
		/// given position.
		/// </summary>
		public void insert(string @s, int @pos)
		{
		}

		/// <summary>
		/// Inserts a separator at the specified position.
		/// </summary>
		public void insertSeparator(int @index)
		{
		}

		/// <summary>
		/// Returns true if the specified component exists in the
		/// submenu hierarchy.
		/// </summary>
		public bool isMenuComponent(Component @c)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the menu's popup window is visible.
		/// </summary>
		public bool isPopupMenuVisible()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the menu is currently selected (highlighted).
		/// </summary>
		public bool isSelected()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the menu can be torn off.
		/// </summary>
		public bool isTearOff()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the menu is a 'top-level menu', that is, if it is
		/// the direct child of a menubar.
		/// </summary>
		public bool isTopLevelMenu()
		{
			return default(bool);
		}

		/// <summary>
		/// Messaged when the menubar selection changes to activate or
		/// deactivate this menu.
		/// </summary>
		public void menuSelectionChanged(bool @isIncluded)
		{
		}

		/// <summary>
		/// Returns a string representation of this <code>JMenu</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Processes key stroke events such as mnemonics and accelerators.
		/// </summary>
		protected void processKeyEvent(KeyEvent @evt)
		{
		}

		/// <summary>
		/// Removes the component <code>c</code> from this menu.
		/// </summary>
		public void remove(Component @c)
		{
		}

		/// <summary>
		/// Removes the menu item at the specified index from this menu.
		/// </summary>
		public void remove(int @pos)
		{
		}

		/// <summary>
		/// Removes the specified menu item from this menu.
		/// </summary>
		public void remove(JMenuItem @item)
		{
		}

		/// <summary>
		/// Removes all menu items from this menu.
		/// </summary>
		public void removeAll()
		{
		}

		/// <summary>
		/// Removes a listener for menu events.
		/// </summary>
		public void removeMenuListener(MenuListener @l)
		{
		}

		/// <summary>
		/// <code>setAccelerator</code> is not defined for <code>JMenu</code>.
		/// </summary>
		public void setAccelerator(KeyStroke @keyStroke)
		{
		}

		/// <summary>
		/// Sets the language-sensitive orientation that is to be used to order
		/// the elements or text within this component.
		/// </summary>
		public void setComponentOrientation(ComponentOrientation @o)
		{
		}

		/// <summary>
		/// Sets the suggested delay before the menu's <code>PopupMenu</code>
		/// is popped up or down.
		/// </summary>
		public void setDelay(int @d)
		{
		}

		/// <summary>
		/// Sets the location of the popup component.
		/// </summary>
		public void setMenuLocation(int @x, int @y)
		{
		}

		/// <summary>
		/// Sets the data model for the "menu button" -- the label
		/// that the user clicks to open or close the menu.
		/// </summary>
		public void setModel(ButtonModel @newModel)
		{
		}

		/// <summary>
		/// Sets the visibility of the menu's popup.
		/// </summary>
		public void setPopupMenuVisible(bool @b)
		{
		}

		/// <summary>
		/// Sets the selection status of the menu.
		/// </summary>
		public void setSelected(bool @b)
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

