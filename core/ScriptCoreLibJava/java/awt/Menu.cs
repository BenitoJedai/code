// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.accessibility;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Menu.html
	[Script(IsNative = true)]
	public class Menu : MenuItem
	{
		/// <summary>
		/// Constructs a new menu with an empty label.
		/// </summary>
		public Menu()
		{
		}

		/// <summary>
		/// Constructs a new menu with the specified label.
		/// </summary>
		public Menu(string @label)
		{
		}

		/// <summary>
		/// Constructs a new menu with the specified label,
		/// indicating whether the menu can be torn off.
		/// </summary>
		public Menu(string @label, bool @tearOff)
		{
		}

		/// <summary>
		/// Adds the specified menu item to this menu.
		/// </summary>
		public MenuItem add(MenuItem @mi)
		{
			return default(MenuItem);
		}

		/// <summary>
		/// Adds an item with the specified label to this menu.
		/// </summary>
		public void add(string @label)
		{
		}

		/// <summary>
		/// Creates the menu's peer.
		/// </summary>
		public void addNotify()
		{
		}

		/// <summary>
		/// Adds a separator line, or a hypen, to the menu at the current position.
		/// </summary>
		public void addSeparator()
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>getItemCount()</code>.</I>
		/// </summary>
		public int countItems()
		{
			return default(int);
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this Menu.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Gets the item located at the specified index of this menu.
		/// </summary>
		public MenuItem getItem(int @index)
		{
			return default(MenuItem);
		}

		/// <summary>
		/// Get the number of items in this menu.
		/// </summary>
		public int getItemCount()
		{
			return default(int);
		}

		/// <summary>
		/// Inserts a menu item into this menu
		/// at the specified position.
		/// </summary>
		public void insert(MenuItem @menuitem, int @index)
		{
		}

		/// <summary>
		/// Inserts a menu item with the specified label into this menu
		/// at the specified position.
		/// </summary>
		public void insert(string @label, int @index)
		{
		}

		/// <summary>
		/// Inserts a separator at the specified position.
		/// </summary>
		public void insertSeparator(int @index)
		{
		}

		/// <summary>
		/// Indicates whether this menu is a tear-off menu.
		/// </summary>
		public bool isTearOff()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representing the state of this <code>Menu</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Removes the menu item at the specified index from this menu.
		/// </summary>
		public void remove(int @index)
		{
		}

		/// <summary>
		/// Removes the specified menu item from this menu.
		/// </summary>
		public void remove(MenuComponent @item)
		{
		}

		/// <summary>
		/// Removes all items from this menu.
		/// </summary>
		public void removeAll()
		{
		}

		/// <summary>
		/// Removes the menu's peer.
		/// </summary>
		public void removeNotify()
		{
		}

	}
}

