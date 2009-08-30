// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.awt.im;
using java.awt.image;
using java.beans;
using java.lang;
using java.util;
using javax.accessibility;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Window.html
	[Script(IsNative = true)]
	public class Window : Container
	{
		/// <summary>
		/// Constructs a new invisible window with the specified
		/// <code>Frame</code> as its owner.
		/// </summary>
		public Window(Frame @owner)
		{
		}

		/// <summary>
		/// Constructs a new invisible window with the specified
		/// <code>Window</code> as its owner.
		/// </summary>
		public Window(Window @owner)
		{
		}

		/// <summary>
		/// Constructs a new invisible window with the specified
		/// window as its owner and a
		/// <code>GraphicsConfiguration</code> of a screen device.
		/// </summary>
		public Window(Window @owner, GraphicsConfiguration @gc)
		{
		}

		/// <summary>
		/// Makes this Window displayable by creating the connection to its
		/// native screen resource.
		/// </summary>
		public void addNotify()
		{
		}

		/// <summary>
		/// Adds a PropertyChangeListener to the listener list.
		/// </summary>
		public void addPropertyChangeListener(PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Adds a PropertyChangeListener to the listener list for a specific
		/// property.
		/// </summary>
		public void addPropertyChangeListener(string @propertyName, PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Adds the specified window focus listener to receive window events
		/// from this window.
		/// </summary>
		public void addWindowFocusListener(WindowFocusListener @l)
		{
		}

		/// <summary>
		/// Adds the specified window listener to receive window events from
		/// this window.
		/// </summary>
		public void addWindowListener(WindowListener @l)
		{
		}

		/// <summary>
		/// Adds the specified window state listener to receive window
		/// events from this window.
		/// </summary>
		public void addWindowStateListener(WindowStateListener @l)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of J2SE 1.4, replaced by
		/// <A HREF="../../java/awt/Component.html#applyComponentOrientation(java.awt.ComponentOrientation)"><CODE>Component.applyComponentOrientation</CODE></A>.</I>
		/// </summary>
		public void applyResourceBundle(ResourceBundle @rb)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of J2SE 1.4, replaced by
		/// <A HREF="../../java/awt/Component.html#applyComponentOrientation(java.awt.ComponentOrientation)"><CODE>Component.applyComponentOrientation</CODE></A>.</I>
		/// </summary>
		public void applyResourceBundle(string @rbName)
		{
		}

		/// <summary>
		/// Creates a new strategy for multi-buffering on this component.
		/// </summary>
		public void createBufferStrategy(int @numBuffers)
		{
		}

		/// <summary>
		/// Creates a new strategy for multi-buffering on this component with the
		/// required buffer capabilities.
		/// </summary>
		public void createBufferStrategy(int @numBuffers, BufferCapabilities @caps)
		{
		}

		/// <summary>
		/// Releases all of the native screen resources used by this
		/// <code>Window</code>, its subcomponents, and all of its owned
		/// children.
		/// </summary>
		public void dispose()
		{
		}

		/// <summary>
		/// Disposes of the input methods and context, and removes the WeakReference
		/// which formerly pointed to this Window from the parent's owned Window
		/// list.
		/// </summary>
		protected void finalize()
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this Window.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// 
		/// </summary>
		public BufferStrategy getBufferStrategy()
		{
			return default(BufferStrategy);
		}

		/// <summary>
		/// Returns whether this Window can become the focused Window if it meets
		/// the other requirements outlined in <code>isFocusableWindow</code>.
		/// </summary>
		public bool getFocusableWindowState()
		{
			return default(bool);
		}

		/// <summary>
		/// Always returns <code>null</code> because Windows have no ancestors; they
		/// represent the top of the Component hierarchy.
		/// </summary>
		public Container getFocusCycleRootAncestor()
		{
			return default(Container);
		}

		/// <summary>
		/// Returns the child Component of this Window that has focus if this Window
		/// is focused; returns null otherwise.
		/// </summary>
		public Component getFocusOwner()
		{
			return default(Component);
		}

		/// <summary>
		/// Gets a focus traversal key for this Window.
		/// </summary>
		public Set getFocusTraversalKeys(int @id)
		{
			return default(Set);
		}

		/// <summary>
		/// This method returns the GraphicsConfiguration used by this Window.
		/// </summary>
		public GraphicsConfiguration getGraphicsConfiguration()
		{
			return default(GraphicsConfiguration);
		}

		/// <summary>
		/// Gets the input context for this window.
		/// </summary>
		public InputContext getInputContext()
		{
			return default(InputContext);
		}

		/// <summary>
		/// Returns an array of all the objects currently registered
		/// as <code><em>Foo</em>Listener</code>s
		/// upon this <code>Window</code>.
		/// </summary>
		public EventListener[] getListeners(Class @listenerType)
		{
			return default(EventListener[]);
		}

		/// <summary>
		/// Gets the <code>Locale</code> object that is associated
		/// with this window, if the locale has been set.
		/// </summary>
		public Locale getLocale()
		{
			return default(Locale);
		}

		/// <summary>
		/// Returns the child Component of this Window that will receive the focus
		/// when this Window is focused.
		/// </summary>
		public Component getMostRecentFocusOwner()
		{
			return default(Component);
		}

		/// <summary>
		/// Return an array containing all the windows this
		/// window currently owns.
		/// </summary>
		public Window[] getOwnedWindows()
		{
			return default(Window[]);
		}

		/// <summary>
		/// Returns the owner of this window.
		/// </summary>
		public Window getOwner()
		{
			return default(Window);
		}

		/// <summary>
		/// Returns the toolkit of this frame.
		/// </summary>
		public Toolkit getToolkit()
		{
			return default(Toolkit);
		}

		/// <summary>
		/// Gets the warning string that is displayed with this window.
		/// </summary>
		public string getWarningString()
		{
			return default(string);
		}

		/// <summary>
		/// Returns an array of all the window focus listeners
		/// registered on this window.
		/// </summary>
		public WindowFocusListener[] getWindowFocusListeners()
		{
			return default(WindowFocusListener[]);
		}

		/// <summary>
		/// Returns an array of all the window listeners
		/// registered on this window.
		/// </summary>
		public WindowListener[] getWindowListeners()
		{
			return default(WindowListener[]);
		}

		/// <summary>
		/// Returns an array of all the window state listeners
		/// registered on this window.
		/// </summary>
		public WindowStateListener[] getWindowStateListeners()
		{
			return default(WindowStateListener[]);
		}

		/// <summary>
		/// Hide this Window, its subcomponents, and all of its owned children.
		/// </summary>
		public void hide()
		{
		}

		/// <summary>
		/// Returns whether this Window is active.
		/// </summary>
		public bool isActive()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether this Window can become the focused Window, that is,
		/// whether this Window or any of its subcomponents can become the focus
		/// owner.
		/// </summary>
		public bool isFocusableWindow()
		{
			return default(bool);
		}

		/// <summary>
		/// Always returns <code>true</code> because all Windows must be roots of a
		/// focus traversal cycle.
		/// </summary>
		public bool isFocusCycleRoot()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether this Window is focused.
		/// </summary>
		public bool isFocused()
		{
			return default(bool);
		}

		/// <summary>
		/// Checks if this Window is showing on screen.
		/// </summary>
		public bool isShowing()
		{
			return default(bool);
		}

		/// <summary>
		/// Causes this Window to be sized to fit the preferred size
		/// and layouts of its subcomponents.
		/// </summary>
		public void pack()
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1
		/// replaced by <code>dispatchEvent(AWTEvent)</code>.</I>
		/// </summary>
		public bool postEvent(Event @e)
		{
			return default(bool);
		}

		/// <summary>
		/// Processes events on this window.
		/// </summary>
		protected void processEvent(AWTEvent @e)
		{
		}

		/// <summary>
		/// Processes window events occurring on this window by
		/// dispatching them to any registered WindowListener objects.
		/// </summary>
		protected void processWindowEvent(WindowEvent @e)
		{
		}

		/// <summary>
		/// Processes window focus event occuring on this window by
		/// dispatching them to any registered WindowFocusListener objects.
		/// </summary>
		protected void processWindowFocusEvent(WindowEvent @e)
		{
		}

		/// <summary>
		/// Processes window state event occuring on this window by
		/// dispatching them to any registered <code>WindowStateListener</code>
		/// objects.
		/// </summary>
		protected void processWindowStateEvent(WindowEvent @e)
		{
		}

		/// <summary>
		/// Removes the specified window focus listener so that it no longer
		/// receives window events from this window.
		/// </summary>
		public void removeWindowFocusListener(WindowFocusListener @l)
		{
		}

		/// <summary>
		/// Removes the specified window listener so that it no longer
		/// receives window events from this window.
		/// </summary>
		public void removeWindowListener(WindowListener @l)
		{
		}

		/// <summary>
		/// Removes the specified window state listener so that it no
		/// longer receives window events from this window.
		/// </summary>
		public void removeWindowStateListener(WindowStateListener @l)
		{
		}

		/// <summary>
		/// Set the cursor image to a specified cursor.
		/// </summary>
		public void setCursor(Cursor @cursor)
		{
		}

		/// <summary>
		/// Sets whether this Window can become the focused Window if it meets
		/// the other requirements outlined in <code>isFocusableWindow</code>.
		/// </summary>
		public void setFocusableWindowState(bool @focusableWindowState)
		{
		}

		/// <summary>
		/// Does nothing because Windows must always be roots of a focus traversal
		/// cycle.
		/// </summary>
		public void setFocusCycleRoot(bool @focusCycleRoot)
		{
		}

		/// <summary>
		/// Sets the location of the window relative to the specified
		/// component.
		/// </summary>
		public void setLocationRelativeTo(Component @c)
		{
		}

		/// <summary>
		/// Makes the Window visible.
		/// </summary>
		public void show()
		{
		}

		/// <summary>
		/// If this Window is visible, sends this Window to the back and may cause
		/// it to lose focus or activation if it is the focused or active Window.
		/// </summary>
		public void toBack()
		{
		}

		/// <summary>
		/// If this Window is visible, brings this Window to the front and may make
		/// it the focused Window.
		/// </summary>
		public void toFront()
		{
		}

	}
}

