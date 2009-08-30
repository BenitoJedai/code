// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.util;
using javax.accessibility;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/MenuBar.html
	[Script(IsNative = true)]
	public class MenuBar : MenuComponent
	{
		/// <summary>
		/// Creates a new menu bar.
		/// </summary>
		public MenuBar()
		{
		}

		/// <summary>
		/// Adds the specified menu to the menu bar.
		/// </summary>
		public Menu add(Menu @m)
		{
			return default(Menu);
		}

		/// <summary>
		/// Creates the menu bar's peer.
		/// </summary>
		public void addNotify()
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>getMenuCount()</code>.</I>
		/// </summary>
		public int countMenus()
		{
			return default(int);
		}

		/// <summary>
		/// Deletes the specified menu shortcut.
		/// </summary>
		public void deleteShortcut(MenuShortcut @s)
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this MenuBar.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Gets the help menu on the menu bar.
		/// </summary>
		public Menu getHelpMenu()
		{
			return default(Menu);
		}

		/// <summary>
		/// Gets the specified menu.
		/// </summary>
		public Menu getMenu(int @i)
		{
			return default(Menu);
		}

		/// <summary>
		/// Gets the number of menus on the menu bar.
		/// </summary>
		public int getMenuCount()
		{
			return default(int);
		}

		/// <summary>
		/// Gets the instance of <code>MenuItem</code> associated
		/// with the specified <code>MenuShortcut</code> object,
		/// or <code>null</code> if none of the menu items being managed
		/// by this menu bar is associated with the specified menu
		/// shortcut.
		/// </summary>
		public MenuItem getShortcutMenuItem(MenuShortcut @s)
		{
			return default(MenuItem);
		}

		/// <summary>
		/// Removes the menu located at the specified
		/// index from this menu bar.
		/// </summary>
		public void remove(int @index)
		{
		}

		/// <summary>
		/// Removes the specified menu component from this menu bar.
		/// </summary>
		public void remove(MenuComponent @m)
		{
		}

		/// <summary>
		/// Removes the menu bar's peer.
		/// </summary>
		public void removeNotify()
		{
		}

		/// <summary>
		/// Sets the specified menu to be this menu bar's help menu.
		/// </summary>
		public void setHelpMenu(Menu @m)
		{
		}

		/// <summary>
		/// Gets an enumeration of all menu shortcuts this menu bar
		/// is managing.
		/// </summary>
		public Enumeration shortcuts()
		{
			return default(Enumeration);
		}

	}
}

