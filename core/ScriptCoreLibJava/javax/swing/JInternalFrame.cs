// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JInternalFrame

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.accessibility;
using javax.swing.@event;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JInternalFrame.html
	[Script(IsNative = true)]
	public class JInternalFrame : JComponent
	{
		/// <summary>
		/// Creates a non-resizable, non-closable, non-maximizable,
		/// non-iconifiable <code>JInternalFrame</code> with no title.
		/// </summary>
		public JInternalFrame()
		{
		}

		/// <summary>
		/// Creates a non-resizable, non-closable, non-maximizable,
		/// non-iconifiable <code>JInternalFrame</code> with the specified title.
		/// </summary>
		public JInternalFrame(string @title)
		{
		}

		/// <summary>
		/// Creates a non-closable, non-maximizable, non-iconifiable
		/// <code>JInternalFrame</code> with the specified title
		/// and resizability.
		/// </summary>
		public JInternalFrame(string @title, bool @resizable)
		{
		}

		/// <summary>
		/// Creates a non-maximizable, non-iconifiable <code>JInternalFrame</code>
		/// with the specified title, resizability, and
		/// closability.
		/// </summary>
		public JInternalFrame(string @title, bool @resizable, bool @closable)
		{
		}

		/// <summary>
		/// Creates a non-iconifiable <code>JInternalFrame</code>
		/// with the specified title,
		/// resizability, closability, and maximizability.
		/// </summary>
		public JInternalFrame(string @title, bool @resizable, bool @closable, bool @maximizable)
		{
		}

		/// <summary>
		/// Creates a <code>JInternalFrame</code> with the specified title,
		/// resizability, closability, maximizability, and iconifiability.
		/// </summary>
		public JInternalFrame(string @title, bool @resizable, bool @closable, bool @maximizable, bool @iconifiable)
		{
		}

		/// <summary>
		/// Ensures that, by default, children cannot be added
		/// directly to this component.
		/// </summary>
		protected void addImpl(Component @comp, object @constraints, int @index)
		{
		}

		/// <summary>
		/// Adds the specified listener to receive internal
		/// frame events from this internal frame.
		/// </summary>
		public void addInternalFrameListener(InternalFrameListener @l)
		{
		}

		/// <summary>
		/// Called by the constructor to set up the <code>JRootPane</code>.
		/// </summary>
		protected JRootPane createRootPane()
		{
			return default(JRootPane);
		}

		/// <summary>
		/// Makes this internal frame
		/// invisible, unselected, and closed.
		/// </summary>
		public void dispose()
		{
		}

		/// <summary>
		/// Fires an
		/// <code>INTERNAL_FRAME_CLOSING</code> event
		/// and then performs the action specified by
		/// the internal frame's default close operation.
		/// </summary>
		public void doDefaultCloseAction()
		{
		}

		/// <summary>
		/// Fires an internal frame event.
		/// </summary>
		protected void fireInternalFrameEvent(int @id)
		{
		}

		/// <summary>
		/// Gets the <code>AccessibleContext</code> associated with this
		/// <code>JInternalFrame</code>.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the content pane for this internal frame.
		/// </summary>
		public Container getContentPane()
		{
			return default(Container);
		}

		/// <summary>
		/// Returns the default operation that occurs when the user
		/// initiates a "close" on this internal frame.
		/// </summary>
		public int getDefaultCloseOperation()
		{
			return default(int);
		}

	

		/// <summary>
		/// Convenience method that searches the ancestor hierarchy for a
		/// <code>JDesktop</code> instance.
		/// </summary>
		public JDesktopPane getDesktopPane()
		{
			return default(JDesktopPane);
		}

		/// <summary>
		/// Always returns <code>null</code> because <code>JInternalFrame</code>s
		/// must always be roots of a focus
		/// traversal cycle.
		/// </summary>
		public Container getFocusCycleRootAncestor()
		{
			return default(Container);
		}

		/// <summary>
		/// If this <code>JInternalFrame</code> is active,
		/// returns the child that has focus.
		/// </summary>
		public Component getFocusOwner()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the image displayed in the title bar of this internal frame (usually
		/// in the top-left corner).
		/// </summary>
		public Icon getFrameIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the glass pane for this internal frame.
		/// </summary>
		public Component getGlassPane()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns an array of all the <code>InternalFrameListener</code>s added
		/// to this <code>JInternalFrame</code> with
		/// <code>addInternalFrameListener</code>.
		/// </summary>
		public InternalFrameListener[] getInternalFrameListeners()
		{
			return default(InternalFrameListener[]);
		}

		/// <summary>
		/// Returns the current <code>JMenuBar</code> for this
		/// <code>JInternalFrame</code>, or <code>null</code>
		/// if no menu bar has been set.
		/// </summary>
		public JMenuBar getJMenuBar()
		{
			return default(JMenuBar);
		}

		/// <summary>
		/// Convenience method for getting the layer attribute of this component.
		/// </summary>
		public int getLayer()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the layered pane for this internal frame.
		/// </summary>
		public JLayeredPane getLayeredPane()
		{
			return default(JLayeredPane);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Swing version 1.0.3,
		/// replaced by <code>getJMenuBar()</code>.</I>
		/// </summary>
		public JMenuBar getMenuBar()
		{
			return default(JMenuBar);
		}

		/// <summary>
		/// Returns the child component of this <code>JInternalFrame</code>
		/// that will receive the
		/// focus when this <code>JInternalFrame</code> is selected.
		/// </summary>
		public Component getMostRecentFocusOwner()
		{
			return default(Component);
		}

		/// <summary>
		/// If the <code>JInternalFrame</code> is not in maximized state, returns
		/// <code>getBounds()</code>; otherwise, returns the bounds that the
		/// <code>JInternalFrame</code> would be restored to.
		/// </summary>
		public Rectangle getNormalBounds()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the <code>rootPane</code> object for this internal frame.
		/// </summary>
		public JRootPane getRootPane()
		{
			return default(JRootPane);
		}

		/// <summary>
		/// Returns the title of the <code>JInternalFrame</code>.
		/// </summary>
		public string getTitle()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the look-and-feel object that renders this component.
		/// </summary>
		public InternalFrameUI getUI()
		{
			return default(InternalFrameUI);
		}

		/// <summary>
		/// Returns the name of the look-and-feel
		/// class that renders this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Gets the warning string that is displayed with this internal frame.
		/// </summary>
		public string getWarningString()
		{
			return default(string);
		}

		/// <summary>
		/// 
		/// </summary>
		public void hide()
		{
		}

		/// <summary>
		/// Returns whether this <code>JInternalFrame</code> can be closed by
		/// some user action.
		/// </summary>
		public bool isClosable()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether this <code>JInternalFrame</code> is currently closed.
		/// </summary>
		public bool isClosed()
		{
			return default(bool);
		}

		/// <summary>
		/// Always returns <code>true</code> because all <code>JInternalFrame</code>s must be
		/// roots of a focus traversal cycle.
		/// </summary>
		public bool isFocusCycleRoot()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the <code>JInternalFrame</code> is currently iconified.
		/// </summary>
		public bool isIcon()
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the <code>iconable</code> property,
		/// which by default is <code>false</code>.
		/// </summary>
		public bool isIconifiable()
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the value of the <code>maximizable</code> property.
		/// </summary>
		public bool isMaximizable()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the <code>JInternalFrame</code> is currently maximized.
		/// </summary>
		public bool isMaximum()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the <code>JInternalFrame</code> can be resized
		/// by some user action.
		/// </summary>
		public bool isResizable()
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
		/// Returns whether the <code>JInternalFrame</code> is the
		/// currently "selected" or active frame.
		/// </summary>
		public bool isSelected()
		{
			return default(bool);
		}

		/// <summary>
		/// Convenience method that moves this component to position -1 if its
		/// parent is a <code>JLayeredPane</code>.
		/// </summary>
		public void moveToBack()
		{
		}

		/// <summary>
		/// Convenience method that moves this component to position 0 if its
		/// parent is a <code>JLayeredPane</code>.
		/// </summary>
		public void moveToFront()
		{
		}

		/// <summary>
		/// Causes subcomponents of this <code>JInternalFrame</code>
		/// to be laid out at their preferred size.
		/// </summary>
		public void pack()
		{
		}

		/// <summary>
		/// Overridden to allow optimized painting when the
		/// internal frame is being dragged.
		/// </summary>
		protected void paintComponent(Graphics @g)
		{
		}

		/// <summary>
		/// Returns a string representation of this <code>JInternalFrame</code>.
		/// </summary>
		protected string paramString()
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
		/// Removes the specified internal frame listener so that it no longer
		/// receives internal frame events from this internal frame.
		/// </summary>
		public void removeInternalFrameListener(InternalFrameListener @l)
		{
		}

		/// <summary>
		/// Moves and resizes this component.
		/// </summary>
		public void reshape(int @x, int @y, int @width, int @height)
		{
		}

		/// <summary>
		/// Requests the internal frame to restore focus to the
		/// last subcomponent that had focus.
		/// </summary>
		public void restoreSubcomponentFocus()
		{
		}

		/// <summary>
		/// Sets whether this <code>JInternalFrame</code> can be closed by
		/// some user action.
		/// </summary>
		public void setClosable(bool @b)
		{
		}

		/// <summary>
		/// Closes this internal frame if the argument is <code>true</code>.
		/// </summary>
		public void setClosed(bool @b)
		{
		}

		/// <summary>
		/// Sets this <code>JInternalFrame</code>'s <code>contentPane</code>
		/// property.
		/// </summary>
		public void setContentPane(Container @c)
		{
		}

		/// <summary>
		/// Sets the operation that will happen by default when
		/// the user initiates a "close" on this internal frame.
		/// </summary>
		public void setDefaultCloseOperation(int @operation)
		{
		}

	

		/// <summary>
		/// Does nothing because <code>JInternalFrame</code>s must always be roots of a focus
		/// traversal cycle.
		/// </summary>
		public void setFocusCycleRoot(bool @focusCycleRoot)
		{
		}

		/// <summary>
		/// Sets an image to be displayed in the titlebar of this internal frame (usually
		/// in the top-left corner).
		/// </summary>
		public void setFrameIcon(Icon @icon)
		{
		}

		/// <summary>
		/// Sets this <code>JInternalFrame</code>'s
		/// <code>glassPane</code> property.
		/// </summary>
		public void setGlassPane(Component @glass)
		{
		}

		/// <summary>
		/// Iconifies or de-iconifies this internal frame,
		/// if the look and feel supports iconification.
		/// </summary>
		public void setIcon(bool @b)
		{
		}

		/// <summary>
		/// Sets the <code>iconable</code> property,
		/// which must be <code>true</code>
		/// for the user to be able to
		/// make the <code>JInternalFrame</code> an icon.
		/// </summary>
		public void setIconifiable(bool @b)
		{
		}

		/// <summary>
		/// Sets the <code>menuBar</code> property for this <code>JInternalFrame</code>.
		/// </summary>
		public void setJMenuBar(JMenuBar @m)
		{
		}

		/// <summary>
		/// Convenience method for setting the layer attribute of this component.
		/// </summary>
		public void setLayer(int @layer)
		{
		}

		/// <summary>
		/// Convenience method for setting the layer attribute of this component.
		/// </summary>
		public void setLayer(Integer @layer)
		{
		}

		/// <summary>
		/// Sets this <code>JInternalFrame</code>'s
		/// <code>layeredPane</code> property.
		/// </summary>
		public void setLayeredPane(JLayeredPane @layered)
		{
		}

		/// <summary>
		/// Ensures that, by default, the layout of this component cannot be set.
		/// </summary>
		public void setLayout(LayoutManager @manager)
		{
		}

		/// <summary>
		/// Sets the <code>maximizable</code> property,
		/// which determines whether the <code>JInternalFrame</code>
		/// can be maximized by
		/// some user action.
		/// </summary>
		public void setMaximizable(bool @b)
		{
		}

		/// <summary>
		/// Maximizes and restores this internal frame.
		/// </summary>
		public void setMaximum(bool @b)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Swing version 1.0.3
		/// replaced by <code>setJMenuBar(JMenuBar m)</code>.</I>
		/// </summary>
		public void setMenuBar(JMenuBar @m)
		{
		}

		/// <summary>
		/// Sets the normal bounds for this internal frame, the bounds that
		/// this internal frame would be restored to from its maximized state.
		/// </summary>
		public void setNormalBounds(Rectangle @r)
		{
		}

		/// <summary>
		/// Sets whether the <code>JInternalFrame</code> can be resized by some
		/// user action.
		/// </summary>
		public void setResizable(bool @b)
		{
		}

		/// <summary>
		/// Sets the <code>rootPane</code> property
		/// for this <code>JInternalFrame</code>.
		/// </summary>
		protected void setRootPane(JRootPane @root)
		{
		}

		/// <summary>
		/// Determines whether calls to <code>add</code> and
		/// <code>setLayout</code> cause an exception to be thrown.
		/// </summary>
		protected void setRootPaneCheckingEnabled(bool @enabled)
		{
		}

		/// <summary>
		/// Selects or deselects the internal frame
		/// if it's showing.
		/// </summary>
		public void setSelected(bool @selected)
		{
		}

		/// <summary>
		/// Sets the <code>JInternalFrame</code> title.
		/// </summary>
		public void setTitle(string @title)
		{
		}

		/// <summary>
		/// Sets the UI delegate for this <code>JInternalFrame</code>.
		/// </summary>
		public void setUI(InternalFrameUI @ui)
		{
		}

		/// <summary>
		/// If the internal frame is not visible,
		/// brings the internal frame to the front,
		/// makes it visible,
		/// and attempts to select it.
		/// </summary>
		public void show()
		{
		}

		/// <summary>
		/// Sends this internal frame to the back.
		/// </summary>
		public void toBack()
		{
		}

		/// <summary>
		/// Brings this internal frame to the front.
		/// </summary>
		public void toFront()
		{
		}

		/// <summary>
		/// Notification from the <code>UIManager</code> that the look and feel
		/// has changed.
		/// </summary>
		public void updateUI()
		{
		}

	}
}
