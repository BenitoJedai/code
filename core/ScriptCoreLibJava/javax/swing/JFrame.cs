// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;
using javax.accessibility;
using javax.swing;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JFrame.html
	[Script(IsNative = true)]
	public class JFrame : Frame
	{
		public static readonly int EXIT_ON_CLOSE;
		/// <summary>
		/// Constructs a new frame that is initially invisible.
		/// </summary>
		public JFrame()
		{
		}

		/// <summary>
		/// Creates a <code>Frame</code> in the specified
		/// <code>GraphicsConfiguration</code> of
		/// a screen device and a blank title.
		/// </summary>
		public JFrame(GraphicsConfiguration @gc)
		{
		}

		/// <summary>
		/// Creates a new, initially invisible <code>Frame</code> with the
		/// specified title.
		/// </summary>
		public JFrame(string @title)
		{
		}

		/// <summary>
		/// Creates a <code>JFrame</code> with the specified title and the
		/// specified <code>GraphicsConfiguration</code> of a screen device.
		/// </summary>
		public JFrame(string @title, GraphicsConfiguration @gc)
		{
		}

		/// <summary>
		/// By default, children may not be added directly to this component,
		/// they must be added to its contentPane instead.
		/// </summary>
		protected void addImpl(Component @comp, object @constraints, int @index)
		{
		}

		/// <summary>
		/// Called by the constructor methods to create the default
		/// <code>rootPane</code>.
		/// </summary>
		public JRootPane createRootPane()
		{
			return default(JRootPane);
		}

		/// <summary>
		/// Called by the constructors to init the <code>JFrame</code> properly.
		/// </summary>
		protected void frameInit()
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JFrame.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the <code>contentPane</code> object for this frame.
		/// </summary>
		public Container getContentPane()
		{
			return default(Container);
		}

		/// <summary>
		/// Returns the operation that occurs when the user
		/// initiates a "close" on this frame.
		/// </summary>
		public int getDefaultCloseOperation()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the <code>glassPane</code> object for this frame.
		/// </summary>
		public Component getGlassPane()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the menubar set on this frame.
		/// </summary>
		public JMenuBar getJMenuBar()
		{
			return default(JMenuBar);
		}

		/// <summary>
		/// Returns the <code>layeredPane</code> object for this frame.
		/// </summary>
		public JLayeredPane getLayeredPane()
		{
			return default(JLayeredPane);
		}

		/// <summary>
		/// Returns the <code>rootPane</code> object for this frame.
		/// </summary>
		public JRootPane getRootPane()
		{
			return default(JRootPane);
		}

		/// <summary>
		/// Returns true if newly created <code>JFrame</code>s should have their
		/// Window decorations provided by the current look and feel.
		/// </summary>
		static public bool isDefaultLookAndFeelDecorated()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether calls to <code>add</code> and
		/// <code>setLayout</code> cause an exception to be thrown.
		/// </summary>
		protected bool isRootPaneCheckingEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representation of this <code>JFrame</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Processes window events occurring on this component.
		/// </summary>
		protected void processWindowEvent(WindowEvent @e)
		{
		}

		/// <summary>
		/// Removes the specified component from this container.
		/// </summary>
		public void remove(Component @comp)
		{
		}

		/// <summary>
		/// Sets the <code>contentPane</code> property.
		/// </summary>
		public void setContentPane(Container @contentPane)
		{
		}

		/// <summary>
		/// Sets the operation that will happen by default when
		/// the user initiates a "close" on this frame.
		/// </summary>
		public void setDefaultCloseOperation(int @operation)
		{
		}

		/// <summary>
		/// Provides a hint as to whether or not newly created <code>JFrame</code>s
		/// should have their Window decorations (such as borders, widgets to
		/// close the window, title...) provided by the current look
		/// and feel.
		/// </summary>
		static public void setDefaultLookAndFeelDecorated(bool @defaultLookAndFeelDecorated)
		{
		}

		/// <summary>
		/// Sets the <code>glassPane</code> property.
		/// </summary>
		public void setGlassPane(Component @glassPane)
		{
		}

		/// <summary>
		/// Sets the menubar for this frame.
		/// </summary>
		public void setJMenuBar(JMenuBar @menubar)
		{
		}

		/// <summary>
		/// Sets the <code>layeredPane</code> property.
		/// </summary>
		public void setLayeredPane(JLayeredPane @layeredPane)
		{
		}

		/// <summary>
		/// By default the layout of this component may not be set,
		/// the layout of its <code>contentPane</code> should be set instead.
		/// </summary>
		public void setLayout(LayoutManager @manager)
		{
		}

		/// <summary>
		/// Sets the <code>rootPane</code> property.
		/// </summary>
		protected void setRootPane(JRootPane @root)
		{
		}

		/// <summary>
		/// Determines whether calls to <code>add</code> and
		/// <code>setLayout</code> will cause an exception to be thrown.
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

