// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.accessibility;
using javax.swing;
using java.applet;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JApplet.html
	[Script(IsNative = true)]
	public class JApplet : Applet
	{
		/// <summary>
		/// Creates a swing applet instance.
		/// </summary>
		public JApplet()
		{
		}

		/// <summary>
		/// By default, children may not be added directly to a this component,
		/// they must be added to its contentPane instead.
		/// </summary>
		protected void addImpl(Component @comp, object @constraints, int @index)
		{
		}

		/// <summary>
		/// Called by the constructor methods to create the default rootPane.
		/// </summary>
		public JRootPane createRootPane()
		{
			return default(JRootPane);
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JApplet.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the contentPane object for this applet.
		/// </summary>
		public Container getContentPane()
		{
			return default(Container);
		}

		/// <summary>
		/// Returns the glassPane object for this applet.
		/// </summary>
		public Component getGlassPane()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the menubar set on this applet.
		/// </summary>
		public JMenuBar getJMenuBar()
		{
			return default(JMenuBar);
		}

		/// <summary>
		/// Returns the layeredPane object for this applet.
		/// </summary>
		public JLayeredPane getLayeredPane()
		{
			return default(JLayeredPane);
		}

		/// <summary>
		/// Returns the rootPane object for this applet.
		/// </summary>
		public JRootPane getRootPane()
		{
			return default(JRootPane);
		}

		/// <summary>
		/// 
		/// </summary>
		protected bool isRootPaneCheckingEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representation of this JApplet.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Removes the specified component from this container.
		/// </summary>
		public void remove(Component @comp)
		{
		}

		/// <summary>
		/// Sets the contentPane property.
		/// </summary>
		public void setContentPane(Container @contentPane)
		{
		}

		/// <summary>
		/// Sets the glassPane property.
		/// </summary>
		public void setGlassPane(Component @glassPane)
		{
		}

		/// <summary>
		/// Sets the menubar for this applet.
		/// </summary>
		public void setJMenuBar(JMenuBar @menuBar)
		{
		}

		/// <summary>
		/// Sets the layeredPane property.
		/// </summary>
		public void setLayeredPane(JLayeredPane @layeredPane)
		{
		}

		/// <summary>
		/// By default the layout of this component may not be set,
		/// the layout of its contentPane should be set instead.
		/// </summary>
		public void setLayout(LayoutManager @manager)
		{
		}

		/// <summary>
		/// Sets the rootPane property.
		/// </summary>
		protected void setRootPane(JRootPane @root)
		{
		}

		/// <summary>
		/// If true then calls to add() and setLayout() will cause an exception
		/// to be thrown.
		/// </summary>
		protected void setRootPaneCheckingEnabled(bool @enabled)
		{
		}

		/// <summary>
		/// Just calls <code>paint(g)</code>.
		/// </summary>
		public void update(Graphics @g)
		{
		}

	}
}
