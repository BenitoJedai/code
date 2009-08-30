// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;
using java.util;
using javax.accessibility;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/MenuItem.html
	[Script(IsNative = true)]
	public class MenuItem : MenuComponent
	{
		/// <summary>
		/// Constructs a new MenuItem with an empty label and no keyboard
		/// shortcut.
		/// </summary>
		public MenuItem()
		{
		}

		/// <summary>
		/// Constructs a new MenuItem with the specified label
		/// and no keyboard shortcut.
		/// </summary>
		public MenuItem(string @label)
		{
		}

		/// <summary>
		/// Create a menu item with an associated keyboard shortcut.
		/// </summary>
		public MenuItem(string @label, MenuShortcut @s)
		{
		}

		/// <summary>
		/// Adds the specified action listener to receive action events
		/// from this menu item.
		/// </summary>
		public void addActionListener(ActionListener @l)
		{
		}

		/// <summary>
		/// Creates the menu item's peer.
		/// </summary>
		public void addNotify()
		{
		}

		/// <summary>
		/// Delete any <code>MenuShortcut</code> object associated
		/// with this menu item.
		/// </summary>
		public void deleteShortcut()
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>setEnabled(boolean)</code>.</I>
		/// </summary>
		public void disable()
		{
		}

		/// <summary>
		/// Disables event delivery to this menu item for events
		/// defined by the specified event mask parameter.
		/// </summary>
		protected void disableEvents(long @eventsToDisable)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>setEnabled(boolean)</code>.</I>
		/// </summary>
		public void enable()
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>setEnabled(boolean)</code>.</I>
		/// </summary>
		public void enable(bool @b)
		{
		}

		/// <summary>
		/// Enables event delivery to this menu item for events
		/// to be defined by the specified event mask parameter
		/// </summary>
		protected void enableEvents(long @eventsToEnable)
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this MenuItem.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Gets the command name of the action event that is fired
		/// by this menu item.
		/// </summary>
		public string getActionCommand()
		{
			return default(string);
		}

		/// <summary>
		/// Returns an array of all the action listeners
		/// registered on this menu item.
		/// </summary>
		public ActionListener[] getActionListeners()
		{
			return default(ActionListener[]);
		}

		/// <summary>
		/// Gets the label for this menu item.
		/// </summary>
		public string getLabel()
		{
			return default(string);
		}

		/// <summary>
		/// Returns an array of all the objects currently registered
		/// as <code><em>Foo</em>Listener</code>s
		/// upon this <code>MenuItem</code>.
		/// </summary>
		public EventListener[] getListeners(Class @listenerType)
		{
			return default(EventListener[]);
		}

		/// <summary>
		/// Get the <code>MenuShortcut</code> object associated with this
		/// menu item,
		/// </summary>
		public MenuShortcut getShortcut()
		{
			return default(MenuShortcut);
		}

		/// <summary>
		/// Checks whether this menu item is enabled.
		/// </summary>
		public bool isEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representing the state of this <code>MenuItem</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Processes action events occurring on this menu item,
		/// by dispatching them to any registered
		/// <code>ActionListener</code> objects.
		/// </summary>
		protected void processActionEvent(ActionEvent @e)
		{
		}

		/// <summary>
		/// Processes events on this menu item.
		/// </summary>
		protected void processEvent(AWTEvent @e)
		{
		}

		/// <summary>
		/// Removes the specified action listener so it no longer receives
		/// action events from this menu item.
		/// </summary>
		public void removeActionListener(ActionListener @l)
		{
		}

		/// <summary>
		/// Sets the command name of the action event that is fired
		/// by this menu item.
		/// </summary>
		public void setActionCommand(string @command)
		{
		}

		/// <summary>
		/// Sets whether or not this menu item can be chosen.
		/// </summary>
		public void setEnabled(bool @b)
		{
		}

		/// <summary>
		/// Sets the label for this menu item to the specified label.
		/// </summary>
		public void setLabel(string @label)
		{
		}

		/// <summary>
		/// Set the <code>MenuShortcut</code> object associated with this
		/// menu item.
		/// </summary>
		public void setShortcut(MenuShortcut @s)
		{
		}

	}
}

