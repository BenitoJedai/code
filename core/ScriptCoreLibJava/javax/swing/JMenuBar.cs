// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;
using javax.accessibility;
using javax.swing;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JMenuBar.html
	[Script(IsNative = true)]
	public class JMenuBar : JComponent
	{
		/// <summary>
		/// Creates a new menu bar.
		/// </summary>
		public JMenuBar()
		{
		}

		/// <summary>
		/// Appends the specified menu to the end of the menu bar.
		/// </summary>
		public JMenu add(JMenu @c)
		{
			return default(JMenu);
		}

		/// <summary>
		/// Overrides <code>JComponent.addNotify</code> to register this
		/// menu bar with the current keyboard manager.
		/// </summary>
		public void addNotify()
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JMenuBar.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Implemented to be a <code>MenuElement</code>.
		/// </summary>
		public Component getComponent()
		{
			return default(Component);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>replaced by <code>getComponent(int i)</code></I>
		/// </summary>
		public Component getComponentAtIndex(int @i)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the index of the specified component.
		/// </summary>
		public int getComponentIndex(Component @c)
		{
			return default(int);
		}

		/// <summary>
		/// Gets the help menu for the menu bar.
		/// </summary>
		public JMenu getHelpMenu()
		{
			return default(JMenu);
		}

		/// <summary>
		/// Returns the margin between the menubar's border and
		/// its menus.
		/// </summary>
		public Insets getMargin()
		{
			return default(Insets);
		}

		/// <summary>
		/// Returns the menu at the specified position in the menu bar.
		/// </summary>
		public JMenu getMenu(int @index)
		{
			return default(JMenu);
		}

		/// <summary>
		/// Returns the number of items in the menu bar.
		/// </summary>
		public int getMenuCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the model object that handles single selections.
		/// </summary>
		public SingleSelectionModel getSelectionModel()
		{
			return default(SingleSelectionModel);
		}

		/// <summary>
		/// Implemented to be a <code>MenuElement</code> -- returns the
		/// menus in this menu bar.
		/// </summary>
		public MenuElement[] getSubElements()
		{
			return default(MenuElement[]);
		}

		/// <summary>
		/// Returns the menubar's current UI.
		/// </summary>
		public MenuBarUI getUI()
		{
			return default(MenuBarUI);
		}

		/// <summary>
		/// Returns the name of the L&F class that renders this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Returns true if the menu bars border should be painted.
		/// </summary>
		public bool isBorderPainted()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the menu bar currently has a component selected.
		/// </summary>
		public bool isSelected()
		{
			return default(bool);
		}

		/// <summary>
		/// Implemented to be a <code>MenuElemen<code>t -- does nothing.
		/// </summary>
		public void menuSelectionChanged(bool @isIncluded)
		{
		}

		/// <summary>
		/// Paints the menubar's border if <code>BorderPainted</code>
		/// property is true.
		/// </summary>
		protected void paintBorder(Graphics @g)
		{
		}

		/// <summary>
		/// Returns a string representation of this <code>JMenuBar</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Subclassed to check all the child menus.
		/// </summary>
		protected bool processKeyBinding(KeyStroke @ks, KeyEvent @e, int @condition, bool @pressed)
		{
			return default(bool);
		}

		/// <summary>
		/// Implemented to be a <code>MenuElement</code> -- does nothing.
		/// </summary>
		public void processKeyEvent(KeyEvent @e, MenuElement[] @path, MenuSelectionManager @manager)
		{
		}

		/// <summary>
		/// Implemented to be a <code>MenuElement</code> -- does nothing.
		/// </summary>
		public void processMouseEvent(MouseEvent @event, MenuElement[] @path, MenuSelectionManager @manager)
		{
		}

		/// <summary>
		/// Overrides <code>JComponent.removeNotify</code> to unregister this
		/// menu bar with the current keyboard manager.
		/// </summary>
		public void removeNotify()
		{
		}

		/// <summary>
		/// Sets whether the border should be painted.
		/// </summary>
		public void setBorderPainted(bool @b)
		{
		}

		/// <summary>
		/// Sets the help menu that appears when the user selects the
		/// "help" option in the menu bar.
		/// </summary>
		public void setHelpMenu(JMenu @menu)
		{
		}

		/// <summary>
		/// Sets the margin between the menubar's border and
		/// its menus.
		/// </summary>
		public void setMargin(Insets @m)
		{
		}

		/// <summary>
		/// Sets the currently selected component, producing a
		/// a change to the selection model.
		/// </summary>
		public void setSelected(Component @sel)
		{
		}

		/// <summary>
		/// Sets the model object to handle single selections.
		/// </summary>
		public void setSelectionModel(SingleSelectionModel @model)
		{
		}

		/// <summary>
		/// Sets the L&F object that renders this component.
		/// </summary>
		public void setUI(MenuBarUI @ui)
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

