// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JDialog

using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;
using javax.accessibility;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JDialog.html
	[Script(IsNative = true)]
	public class JDialog : Dialog
	{
		/// <summary>
		/// Creates a non-modal dialog without a title and without a specified
		/// <code>Frame</code> owner.
		/// </summary>
		public JDialog()
			: base((Dialog)null)
		{
		}

		/// <summary>
		/// Creates a non-modal dialog without a title with the
		/// specified <code>Dialog</code> as its owner.
		/// </summary>
		public JDialog(Dialog @owner)
			: base(@owner)
		{
		}

		/// <summary>
		/// Creates a modal or non-modal dialog without a title and
		/// with the specified owner dialog.
		/// </summary>
		public JDialog(Dialog @owner, bool @modal)
			: base(@owner)
		{
		}

		/// <summary>
		/// Creates a non-modal dialog with the specified title and
		/// with the specified owner dialog.
		/// </summary>
		public JDialog(Dialog @owner, string @title)
			: base(@owner)
		{
		}

		/// <summary>
		/// Creates a modal or non-modal dialog with the specified title
		/// and the specified owner frame.
		/// </summary>
		public JDialog(Dialog @owner, string @title, bool @modal)
			: base(@owner)
		{
		}

		/// <summary>
		/// Creates a modal or non-modal dialog with the specified title,
		/// owner <code>Dialog</code>, and <code>GraphicsConfiguration</code>.
		/// </summary>
		public JDialog(Dialog @owner, string @title, bool @modal, GraphicsConfiguration @gc)
			: base(@owner)
		{
		}

		/// <summary>
		/// Creates a non-modal dialog without a title with the
		/// specified <code>Frame</code> as its owner.
		/// </summary>
		public JDialog(Frame @owner)
			: base(@owner)
		{
		}

		/// <summary>
		/// Creates a modal or non-modal dialog without a title and
		/// with the specified owner <code>Frame</code>.
		/// </summary>
		public JDialog(Frame @owner, bool @modal)
			: base(@owner)
		{
		}

		/// <summary>
		/// Creates a non-modal dialog with the specified title and
		/// with the specified owner frame.
		/// </summary>
		public JDialog(Frame @owner, string @title)
			: base(@owner)
		{
		}

		/// <summary>
		/// Creates a modal or non-modal dialog with the specified title
		/// and the specified owner <code>Frame</code>.
		/// </summary>
		public JDialog(Frame @owner, string @title, bool @modal)
			: base(@owner)
		{
		}

		/// <summary>
		/// Creates a modal or non-modal dialog with the specified title,
		/// owner <code>Frame</code>, and <code>GraphicsConfiguration</code>.
		/// </summary>
		public JDialog(Frame @owner, string @title, bool @modal, GraphicsConfiguration @gc)
			: base(@owner)
		{
		}

		/// <summary>
		/// By default, children may not be added directly to this component,
		/// they must be added to its <code>contentPane</code> instead.
		/// </summary>
		protected void addImpl(Component @comp, object @constraints, int @index)
		{
		}

		/// <summary>
		/// Called by the constructor methods to create the default
		/// <code>rootPane</code>.
		/// </summary>
		protected JRootPane createRootPane()
		{
			return default(JRootPane);
		}

		/// <summary>
		/// Called by the constructors to init the <code>JDialog</code> properly.
		/// </summary>
		protected void dialogInit()
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JDialog.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the <code>contentPane</code> object for this dialog.
		/// </summary>
		public Container getContentPane()
		{
			return default(Container);
		}

		/// <summary>
		/// Returns the operation which occurs when the user
		/// initiates a "close" on this dialog.
		/// </summary>
		public int getDefaultCloseOperation()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the <code>glassPane</code> object for this dialog.
		/// </summary>
		public Component getGlassPane()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the menubar set on this dialog.
		/// </summary>
		public JMenuBar getJMenuBar()
		{
			return default(JMenuBar);
		}

		/// <summary>
		/// Returns the <code>layeredPane</code> object for this dialog.
		/// </summary>
		public JLayeredPane getLayeredPane()
		{
			return default(JLayeredPane);
		}

		/// <summary>
		/// Returns the <code>rootPane</code> object for this dialog.
		/// </summary>
		public JRootPane getRootPane()
		{
			return default(JRootPane);
		}

		/// <summary>
		/// Returns true if newly created <code>JDialog</code>s should have their
		/// Window decorations provided by the current look and feel.
		/// </summary>
		static public bool isDefaultLookAndFeelDecorated()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the methods <code>add</code> and <code>setLayout</code>
		/// should be checked.
		/// </summary>
		protected bool isRootPaneCheckingEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representation of this <code>JDialog</code>.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Handles window events depending on the state of the
		/// <code>defaultCloseOperation</code> property.
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
		/// Sets the operation which will happen by default when
		/// the user initiates a "close" on this dialog.
		/// </summary>
		public void setDefaultCloseOperation(int @operation)
		{
		}

		/// <summary>
		/// Provides a hint as to whether or not newly created <code>JDialog</code>s
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
		/// Sets the menubar for this dialog.
		/// </summary>
		public void setJMenuBar(JMenuBar @menu)
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
		/// If true then calls to <code>add</code> and <code>setLayout</code>
		/// will cause an exception to be thrown.
		/// </summary>
		protected void setRootPaneCheckingEnabled(bool @enabled)
		{
		}

		/// <summary>
		/// Calls <code>paint(g)</code>.
		/// </summary>
		public void update(Graphics @g)
		{
		}

	}
}
