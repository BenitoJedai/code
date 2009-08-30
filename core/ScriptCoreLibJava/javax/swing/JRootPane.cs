// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.accessibility;
using javax.swing;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JRootPane.html
	[Script(IsNative = true)]
	public class JRootPane : JComponent
	{
		/// <summary>
		/// Creates a <code>JRootPane</code>, setting up its
		/// <code>glassPane</code>, <code>layeredPane</code>,
		/// and <code>contentPane</code>.
		/// </summary>
		public JRootPane()
		{
		}

		/// <summary>
		/// Overridden to enforce the position of the glass component as
		/// the zero child.
		/// </summary>
		protected void addImpl(Component @comp, object @constraints, int @index)
		{
		}

		/// <summary>
		/// Register ourselves with the <code>SystemEventQueueUtils</code> as a new
		/// root pane.
		/// </summary>
		public void addNotify()
		{
		}

		/// <summary>
		/// Called by the constructor methods to create the default
		/// <code>contentPane</code>.
		/// </summary>
		public Container createContentPane()
		{
			return default(Container);
		}

		/// <summary>
		/// Called by the constructor methods to create the default
		/// <code>glassPane</code>.
		/// </summary>
		public Component createGlassPane()
		{
			return default(Component);
		}

		/// <summary>
		/// Called by the constructor methods to create the default
		/// <code>layeredPane</code>.
		/// </summary>
		public JLayeredPane createLayeredPane()
		{
			return default(JLayeredPane);
		}

		/// <summary>
		/// Called by the constructor methods to create the default
		/// <code>layoutManager</code>.
		/// </summary>
		public LayoutManager createRootLayout()
		{
			return default(LayoutManager);
		}

		/// <summary>
		/// Gets the <code>AccessibleContext</code> associated with this
		/// <code>JRootPane</code>.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the content pane -- the container that holds the components
		/// parented by the root pane.
		/// </summary>
		public Container getContentPane()
		{
			return default(Container);
		}

		/// <summary>
		/// Returns the value of the <code>defaultButton</code> property.
		/// </summary>
		public JButton getDefaultButton()
		{
			return default(JButton);
		}

		/// <summary>
		/// Returns the current glass pane for this <code>JRootPane</code>.
		/// </summary>
		public Component getGlassPane()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the menu bar from the layered pane.
		/// </summary>
		public JMenuBar getJMenuBar()
		{
			return default(JMenuBar);
		}

		/// <summary>
		/// Gets the layered pane used by the root pane.
		/// </summary>
		public JLayeredPane getLayeredPane()
		{
			return default(JLayeredPane);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Swing version 1.0.3
		/// replaced by <code>getJMenubar()</code>.</I>
		/// </summary>
		public JMenuBar getMenuBar()
		{
			return default(JMenuBar);
		}

		/// <summary>
		/// Returns the L&F object that renders this component.
		/// </summary>
		public RootPaneUI getUI()
		{
			return default(RootPaneUI);
		}

		/// <summary>
		/// Returns a string that specifies the name of the L&F class
		/// that renders this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Returns a constant identifying the type of Window decorations the
		/// <code>JRootPane</code> is providing.
		/// </summary>
		public int getWindowDecorationStyle()
		{
			return default(int);
		}

		/// <summary>
		/// The <code>glassPane</code> and <code>contentPane</code>
		/// have the same bounds, which means <code>JRootPane</code>
		/// does not tiles its children and this should return false.
		/// </summary>
		public bool isOptimizedDrawingEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// If a descendant of this <code>JRootPane</code> calls
		/// <code>revalidate</code>, validate from here on down.
		/// </summary>
		public bool isValidateRoot()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representation of this <code>JRootPane</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Unregister ourselves from <code>SystemEventQueueUtils</code>.
		/// </summary>
		public void removeNotify()
		{
		}

		/// <summary>
		/// Sets the content pane -- the container that holds the components
		/// parented by the root pane.
		/// </summary>
		public void setContentPane(Container @content)
		{
		}

		/// <summary>
		/// Sets the <code>defaultButton</code> property,
		/// which determines the current default button for this <code>JRootPane</code>.
		/// </summary>
		public void setDefaultButton(JButton @defaultButton)
		{
		}

		/// <summary>
		/// Sets a specified <code>Component</code> to be the glass pane for this
		/// root pane.
		/// </summary>
		public void setGlassPane(Component @glass)
		{
		}

		/// <summary>
		/// Adds or changes the menu bar used in the layered pane.
		/// </summary>
		public void setJMenuBar(JMenuBar @menu)
		{
		}

		/// <summary>
		/// Sets the layered pane for the root pane.
		/// </summary>
		public void setLayeredPane(JLayeredPane @layered)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Swing version 1.0.3
		/// replaced by <code>setJMenuBar(JMenuBar menu)</code>.</I>
		/// </summary>
		public void setMenuBar(JMenuBar @menu)
		{
		}

		/// <summary>
		/// Sets the L&F object that renders this component.
		/// </summary>
		public void setUI(RootPaneUI @ui)
		{
		}

		/// <summary>
		/// Sets the type of Window decorations (such as borders, widgets for
		/// closing a Window, title ...) the <code>JRootPane</code> should
		/// provide.
		/// </summary>
		public void setWindowDecorationStyle(int @windowDecorationStyle)
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
