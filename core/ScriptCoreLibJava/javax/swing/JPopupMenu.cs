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
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JPopupMenu.html
	[Script(IsNative = true)]
	public class JPopupMenu : JComponent
	{
		/// <summary>
		/// Constructs a <code>JPopupMenu</code> without an "invoker".
		/// </summary>
		public JPopupMenu()
		{
		}

		/// <summary>
		/// Constructs a <code>JPopupMenu</code> with the specified title.
		/// </summary>
		public JPopupMenu(string @label)
		{
		}

		/// <summary>
		/// Appends a new menu item to the end of the menu which
		/// dispatches the specified <code>Action</code> object.
		/// </summary>
		public JMenuItem add(Action @a)
		{
			return default(JMenuItem);
		}

		/// <summary>
		/// Appends the specified menu item to the end of this menu.
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
		/// Adds a <code>PopupMenu</code> listener.
		/// </summary>
		public void addPopupMenuListener(PopupMenuListener @l)
		{
		}

		/// <summary>
		/// Appends a new separator at the end of the menu.
		/// </summary>
		public void addSeparator()
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
		/// <code>Actions</code> added to the <code>JPopupMenu</code>.
		/// </summary>
		public JMenuItem createActionComponent(Action @a)
		{
			return default(JMenuItem);
		}

		/// <summary>
		/// Notifies <code>PopupMenuListeners</code> that this popup menu is
		/// cancelled.
		/// </summary>
		protected void firePopupMenuCanceled()
		{
		}

		/// <summary>
		/// Notifies <code>PopupMenuListener</code>s that this popup menu will
		/// become invisible.
		/// </summary>
		protected void firePopupMenuWillBecomeInvisible()
		{
		}

		/// <summary>
		/// Notifies <code>PopupMenuListener</code>s that this popup menu will
		/// become visible.
		/// </summary>
		protected void firePopupMenuWillBecomeVisible()
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JPopupMenu.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns this <code>JPopupMenu</code> component.
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
		/// Gets the <code>defaultLightWeightPopupEnabled</code> property,
		/// which by default is <code>true</code>.
		/// </summary>
		static public bool getDefaultLightWeightPopupEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the component which is the 'invoker' of this
		/// popup menu.
		/// </summary>
		public Component getInvoker()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the popup menu's label
		/// </summary>
		public string getLabel()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the margin, in pixels, between the popup menu's border and
		/// its containees.
		/// </summary>
		public Insets getMargin()
		{
			return default(Insets);
		}

		/// <summary>
		/// Returns an array of all the <code>PopupMenuListener</code>s added
		/// to this JMenuItem with addPopupMenuListener().
		/// </summary>
		public PopupMenuListener[] getPopupMenuListeners()
		{
			return default(PopupMenuListener[]);
		}

		/// <summary>
		/// Returns the model object that handles single selections.
		/// </summary>
		public SingleSelectionModel getSelectionModel()
		{
			return default(SingleSelectionModel);
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
		/// Returns the look and feel (L&F) object that renders this component.
		/// </summary>
		public PopupMenuUI getUI()
		{
			return default(PopupMenuUI);
		}

		/// <summary>
		/// Returns the name of the L&F class that renders this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Inserts a menu item for the specified <code>Action</code> object at
		/// a given position.
		/// </summary>
		public void insert(Action @a, int @index)
		{
		}

		/// <summary>
		/// Inserts the specified component into the menu at a given
		/// position.
		/// </summary>
		public void insert(Component @component, int @index)
		{
		}

		/// <summary>
		/// Checks whether the border should be painted.
		/// </summary>
		public bool isBorderPainted()
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the <code>lightWeightPopupEnabled</code> property.
		/// </summary>
		public bool isLightWeightPopupEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the <code>MouseEvent</code> is considered a popup trigger
		/// by the <code>JPopupMenu</code>'s currently installed UI.
		/// </summary>
		public bool isPopupTrigger(MouseEvent @e)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the popup menu is visible (currently
		/// being displayed).
		/// </summary>
		public bool isVisible()
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
		/// Lays out the container so that it uses the minimum space
		/// needed to display its contents.
		/// </summary>
		public void pack()
		{
		}

		/// <summary>
		/// Paints the popup menu's border if the <code>borderPainted</code>
		/// property is <code>true</code>.
		/// </summary>
		protected void paintBorder(Graphics @g)
		{
		}

		/// <summary>
		/// Returns a string representation of this <code>JPopupMenu</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Processes focus events occurring on this component by
		/// dispatching them to any registered
		/// <code>FocusListener</code> objects.
		/// </summary>
		protected void processFocusEvent(FocusEvent @evt)
		{
		}

		/// <summary>
		/// Processes key stroke events such as mnemonics and accelerators.
		/// </summary>
		protected void processKeyEvent(KeyEvent @evt)
		{
		}

		/// <summary>
		/// This method is required to conform to the
		/// <code>MenuElement</code> interface, but it not implemented.
		/// </summary>
		public void processKeyEvent(KeyEvent @e, MenuElement[] @path, MenuSelectionManager @manager)
		{
		}

		/// <summary>
		/// This method is required to conform to the
		/// <code>MenuElement</code> interface, but it not implemented.
		/// </summary>
		public void processMouseEvent(MouseEvent @event, MenuElement[] @path, MenuSelectionManager @manager)
		{
		}

		/// <summary>
		/// Removes the component at the specified index from this popup menu.
		/// </summary>
		public void remove(int @pos)
		{
		}

		/// <summary>
		/// Removes a <code>PopupMenu</code> listener.
		/// </summary>
		public void removePopupMenuListener(PopupMenuListener @l)
		{
		}

		/// <summary>
		/// Sets whether the border should be painted.
		/// </summary>
		public void setBorderPainted(bool @b)
		{
		}

		/// <summary>
		/// Sets the default value of the <code>lightWeightPopupEnabled</code>
		/// property.
		/// </summary>
		static public void setDefaultLightWeightPopupEnabled(bool @aFlag)
		{
		}

		/// <summary>
		/// Sets the invoker of this popup menu -- the component in which
		/// the popup menu menu is to be displayed.
		/// </summary>
		public void setInvoker(Component @invoker)
		{
		}

		/// <summary>
		/// Sets the popup menu's label.
		/// </summary>
		public void setLabel(string @label)
		{
		}

		/// <summary>
		/// Sets the value of the <code>lightWeightPopupEnabled</code> property,
		/// which by default is <code>true</code>.
		/// </summary>
		public void setLightWeightPopupEnabled(bool @aFlag)
		{
		}

		/// <summary>
		/// Sets the location of the upper left corner of the
		/// popup menu using x, y coordinates.
		/// </summary>
		public void setLocation(int @x, int @y)
		{
		}

		/// <summary>
		/// Sets the size of the Popup window using a <code>Dimension</code> object.
		/// </summary>
		public void setPopupSize(Dimension @d)
		{
		}

		/// <summary>
		/// Sets the size of the Popup window to the specified width and
		/// height.
		/// </summary>
		public void setPopupSize(int @width, int @height)
		{
		}

		/// <summary>
		/// Sets the currently selected component,  This will result
		/// in a change to the selection model.
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
		public void setUI(PopupMenuUI @ui)
		{
		}

		/// <summary>
		/// Sets the visibility of the popup menu.
		/// </summary>
		public void setVisible(bool @b)
		{
		}

		/// <summary>
		/// Displays the popup menu at the position x,y in the coordinate
		/// space of the component invoker.
		/// </summary>
		public void show(Component @invoker, int @x, int @y)
		{
		}

		/// <summary>
		/// Resets the UI property to a value from the current look and feel.
		/// </summary>
		public void updateUI()
		{
		}

	}
}

