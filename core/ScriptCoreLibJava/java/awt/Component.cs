// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.dnd;
using java.awt.@event;
using java.awt.im;
using java.awt.image;
using java.beans;
using java.io;
using java.lang;
using java.util;
using javax.accessibility;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Component.html
	[Script(IsNative = true)]
	public class Component
	{
		/// <summary>
		/// Constructs a new component.
		/// </summary>
		public Component()
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// should register this component as ActionListener on component
		/// which fires action events.</I>
		/// </summary>
		public bool action(Event @evt, object @what)
		{
			return default(bool);
		}

		/// <summary>
		/// Adds the specified popup menu to the component.
		/// </summary>
		public void add(PopupMenu @popup)
		{
		}

		/// <summary>
		/// Adds the specified component listener to receive component events from
		/// this component.
		/// </summary>
		public void addComponentListener(ComponentListener @l)
		{
		}

		/// <summary>
		/// Adds the specified focus listener to receive focus events from
		/// this component when this component gains input focus.
		/// </summary>
		public void addFocusListener(FocusListener @l)
		{
		}

		/// <summary>
		/// Adds the specified hierarchy bounds listener to receive hierarchy
		/// bounds events from this component when the hierarchy to which this
		/// container belongs changes.
		/// </summary>
		public void addHierarchyBoundsListener(HierarchyBoundsListener @l)
		{
		}

		/// <summary>
		/// Adds the specified hierarchy listener to receive hierarchy changed
		/// events from this component when the hierarchy to which this container
		/// belongs changes.
		/// </summary>
		public void addHierarchyListener(HierarchyListener @l)
		{
		}

		/// <summary>
		/// Adds the specified input method listener to receive
		/// input method events from this component.
		/// </summary>
		public void addInputMethodListener(InputMethodListener @l)
		{
		}

		/// <summary>
		/// Adds the specified key listener to receive key events from
		/// this component.
		/// </summary>
		public void addKeyListener(KeyListener @l)
		{
		}

		/// <summary>
		/// Adds the specified mouse listener to receive mouse events from
		/// this component.
		/// </summary>
		public void addMouseListener(MouseListener @l)
		{
		}

		/// <summary>
		/// Adds the specified mouse motion listener to receive mouse motion
		/// events from this component.
		/// </summary>
		public void addMouseMotionListener(MouseMotionListener @l)
		{
		}

		/// <summary>
		/// Adds the specified mouse wheel listener to receive mouse wheel events
		/// from this component.
		/// </summary>
		public void addMouseWheelListener(MouseWheelListener @l)
		{
		}

		/// <summary>
		/// Makes this <code>Component</code> displayable by connecting it to a
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
		/// Sets the <code>ComponentOrientation</code> property of this component
		/// and all components contained within it.
		/// </summary>
		public void applyComponentOrientation(ComponentOrientation @orientation)
		{
		}

		/// <summary>
		/// Returns whether the Set of focus traversal keys for the given focus
		/// traversal operation has been explicitly defined for this Component.
		/// </summary>
		public bool areFocusTraversalKeysSet(int @id)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>getBounds()</code>.</I>
		/// </summary>
		public Rectangle bounds()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the status of the construction of a screen representation
		/// of the specified image.
		/// </summary>
		public int checkImage(Image @image, ImageObserver @observer)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the status of the construction of a screen representation
		/// of the specified image.
		/// </summary>
		public int checkImage(Image @image, int @width, int @height, ImageObserver @observer)
		{
			return default(int);
		}

		/// <summary>
		/// Potentially coalesce an event being posted with an existing
		/// event.
		/// </summary>
		public AWTEvent coalesceEvents(AWTEvent @existingEvent, AWTEvent @newEvent)
		{
			return default(AWTEvent);
		}

		/// <summary>
		/// Checks whether this component "contains" the specified point,
		/// where <code>x</code> and <code>y</code> are defined to be
		/// relative to the coordinate system of this component.
		/// </summary>
		public bool contains(int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// Checks whether this component "contains" the specified point,
		/// where the point's <i>x</i> and <i>y</i> coordinates are defined
		/// to be relative to the coordinate system of this component.
		/// </summary>
		public bool contains(Point @p)
		{
			return default(bool);
		}

		/// <summary>
		/// Creates an image from the specified image producer.
		/// </summary>
		public Image createImage(ImageProducer @producer)
		{
			return default(Image);
		}

		/// <summary>
		/// Creates an off-screen drawable image
		/// to be used for double buffering.
		/// </summary>
		public Image createImage(int @width, int @height)
		{
			return default(Image);
		}

		/// <summary>
		/// Creates a volatile off-screen drawable image
		/// to be used for double buffering.
		/// </summary>
		public VolatileImage createVolatileImage(int @width, int @height)
		{
			return default(VolatileImage);
		}

		/// <summary>
		/// Creates a volatile off-screen drawable image, with the given capabilities.
		/// </summary>
		public VolatileImage createVolatileImage(int @width, int @height, ImageCapabilities @caps)
		{
			return default(VolatileImage);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>dispatchEvent(AWTEvent e)</code>.</I>
		/// </summary>
		public void deliverEvent(Event @e)
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
		/// Disables the events defined by the specified event mask parameter
		/// from being delivered to this component.
		/// </summary>
		protected void disableEvents(long @eventsToDisable)
		{
		}

		/// <summary>
		/// Dispatches an event to this component or one of its sub components.
		/// </summary>
		public void dispatchEvent(AWTEvent @e)
		{
		}

		/// <summary>
		/// Prompts the layout manager to lay out this component.
		/// </summary>
		public void doLayout()
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
		/// Enables the events defined by the specified event mask parameter
		/// to be delivered to this component.
		/// </summary>
		protected void enableEvents(long @eventsToEnable)
		{
		}

		/// <summary>
		/// Enables or disables input method support for this component.
		/// </summary>
		public void enableInputMethods(bool @enable)
		{
		}

		/// <summary>
		/// Support for reporting bound property changes for boolean properties.
		/// </summary>
		protected void firePropertyChange(string @propertyName, bool @oldValue, bool @newValue)
		{
		}

		/// <summary>
		/// Support for reporting bound property changes for integer properties.
		/// </summary>
		protected void firePropertyChange(string @propertyName, int @oldValue, int @newValue)
		{
		}

		/// <summary>
		/// Support for reporting bound property changes for Object properties.
		/// </summary>
		protected void firePropertyChange(string @propertyName, object @oldValue, object @newValue)
		{
		}

		/// <summary>
		/// Gets the <code>AccessibleContext</code> associated
		/// with this <code>Component</code>.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the alignment along the x axis.
		/// </summary>
		public float getAlignmentX()
		{
			return default(float);
		}

		/// <summary>
		/// Returns the alignment along the y axis.
		/// </summary>
		public float getAlignmentY()
		{
			return default(float);
		}

		/// <summary>
		/// Gets the background color of this component.
		/// </summary>
		public Color getBackground()
		{
			return default(Color);
		}

		/// <summary>
		/// Gets the bounds of this component in the form of a
		/// <code>Rectangle</code> object.
		/// </summary>
		public Rectangle getBounds()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Stores the bounds of this component into "return value" <b>rv</b> and
		/// return <b>rv</b>.
		/// </summary>
		public Rectangle getBounds(Rectangle @rv)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Gets the instance of <code>ColorModel</code> used to display
		/// the component on the output device.
		/// </summary>
		public ColorModel getColorModel()
		{
			return default(ColorModel);
		}

		/// <summary>
		/// Determines if this component or one of its immediate
		/// subcomponents contains the (<i>x</i>, <i>y</i>) location,
		/// and if so, returns the containing component.
		/// </summary>
		public Component getComponentAt(int @x, int @y)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the component or subcomponent that contains the
		/// specified point.
		/// </summary>
		public Component getComponentAt(Point @p)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns an array of all the component listeners
		/// registered on this component.
		/// </summary>
		public ComponentListener[] getComponentListeners()
		{
			return default(ComponentListener[]);
		}

		/// <summary>
		/// Retrieves the language-sensitive orientation that is to be used to order
		/// the elements or text within this component.
		/// </summary>
		public ComponentOrientation getComponentOrientation()
		{
			return default(ComponentOrientation);
		}

		/// <summary>
		/// Gets the cursor set in the component.
		/// </summary>
		public Cursor getCursor()
		{
			return default(Cursor);
		}

		/// <summary>
		/// Gets the <code>DropTarget</code> associated with this
		/// <code>Component</code>.
		/// </summary>
		public DropTarget getDropTarget()
		{
			return default(DropTarget);
		}

		/// <summary>
		/// Returns the Container which is the focus cycle root of this Component's
		/// focus traversal cycle.
		/// </summary>
		public Container getFocusCycleRootAncestor()
		{
			return default(Container);
		}

		/// <summary>
		/// Returns an array of all the focus listeners
		/// registered on this component.
		/// </summary>
		public FocusListener[] getFocusListeners()
		{
			return default(FocusListener[]);
		}

		/// <summary>
		/// Returns the Set of focus traversal keys for a given traversal operation
		/// for this Component.
		/// </summary>
		public Set getFocusTraversalKeys(int @id)
		{
			return default(Set);
		}

		/// <summary>
		/// Returns whether focus traversal keys are enabled for this Component.
		/// </summary>
		public bool getFocusTraversalKeysEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the font of this component.
		/// </summary>
		public Font getFont()
		{
			return default(Font);
		}

		/// <summary>
		/// Gets the font metrics for the specified font.
		/// </summary>
		public FontMetrics getFontMetrics(Font @font)
		{
			return default(FontMetrics);
		}

		/// <summary>
		/// Gets the foreground color of this component.
		/// </summary>
		public Color getForeground()
		{
			return default(Color);
		}

		/// <summary>
		/// Creates a graphics context for this component.
		/// </summary>
		public Graphics getGraphics()
		{
			return default(Graphics);
		}

		/// <summary>
		/// Gets the <code>GraphicsConfiguration</code> associated with this
		/// <code>Component</code>.
		/// </summary>
		public GraphicsConfiguration getGraphicsConfiguration()
		{
			return default(GraphicsConfiguration);
		}

		/// <summary>
		/// Returns the current height of this component.
		/// </summary>
		public int getHeight()
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array of all the hierarchy bounds listeners
		/// registered on this component.
		/// </summary>
		public HierarchyBoundsListener[] getHierarchyBoundsListeners()
		{
			return default(HierarchyBoundsListener[]);
		}

		/// <summary>
		/// Returns an array of all the hierarchy listeners
		/// registered on this component.
		/// </summary>
		public HierarchyListener[] getHierarchyListeners()
		{
			return default(HierarchyListener[]);
		}

		/// <summary>
		/// 
		/// </summary>
		public bool getIgnoreRepaint()
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the input context used by this component for handling
		/// the communication with input methods when text is entered
		/// in this component.
		/// </summary>
		public InputContext getInputContext()
		{
			return default(InputContext);
		}

		/// <summary>
		/// Returns an array of all the input method listeners
		/// registered on this component.
		/// </summary>
		public InputMethodListener[] getInputMethodListeners()
		{
			return default(InputMethodListener[]);
		}

		/// <summary>
		/// Gets the input method request handler which supports
		/// requests from input methods for this component.
		/// </summary>
		public InputMethodRequests getInputMethodRequests()
		{
			return default(InputMethodRequests);
		}

		/// <summary>
		/// Returns an array of all the key listeners
		/// registered on this component.
		/// </summary>
		public KeyListener[] getKeyListeners()
		{
			return default(KeyListener[]);
		}

		/// <summary>
		/// Returns an array of all the objects currently registered
		/// as <code><em>Foo</em>Listener</code>s
		/// upon this <code>Component</code>.
		/// </summary>
		public EventListener[] getListeners(Class @listenerType)
		{
			return default(EventListener[]);
		}

		/// <summary>
		/// Gets the locale of this component.
		/// </summary>
		public Locale getLocale()
		{
			return default(Locale);
		}

		/// <summary>
		/// Gets the location of this component in the form of a
		/// point specifying the component's top-left corner.
		/// </summary>
		public Point getLocation()
		{
			return default(Point);
		}

		/// <summary>
		/// Stores the x,y origin of this component into "return value" <b>rv</b>
		/// and return <b>rv</b>.
		/// </summary>
		public Point getLocation(Point @rv)
		{
			return default(Point);
		}

		/// <summary>
		/// Gets the location of this component in the form of a point
		/// specifying the component's top-left corner in the screen's
		/// coordinate space.
		/// </summary>
		public Point getLocationOnScreen()
		{
			return default(Point);
		}

		/// <summary>
		/// Gets the maximum size of this component.
		/// </summary>
		public Dimension getMaximumSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Gets the mininimum size of this component.
		/// </summary>
		public Dimension getMinimumSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns an array of all the mouse listeners
		/// registered on this component.
		/// </summary>
		public MouseListener[] getMouseListeners()
		{
			return default(MouseListener[]);
		}

		/// <summary>
		/// Returns an array of all the mouse motion listeners
		/// registered on this component.
		/// </summary>
		public MouseMotionListener[] getMouseMotionListeners()
		{
			return default(MouseMotionListener[]);
		}

		/// <summary>
		/// Returns an array of all the mouse wheel listeners
		/// registered on this component.
		/// </summary>
		public MouseWheelListener[] getMouseWheelListeners()
		{
			return default(MouseWheelListener[]);
		}

		/// <summary>
		/// Gets the name of the component.
		/// </summary>
		public string getName()
		{
			return default(string);
		}

		/// <summary>
		/// Gets the parent of this component.
		/// </summary>
		public Container getParent()
		{
			return default(Container);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// programs should not directly manipulate peers;
		/// replaced by <code>boolean isDisplayable()</code>.</I>
		/// </summary>
		//public java.awt.peer.ComponentPeer getPeer()
		//{
		//    return default(java.awt.peer.ComponentPeer);
		//}

		/// <summary>
		/// Gets the preferred size of this component.
		/// </summary>
		public Dimension getPreferredSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns an array of all the property change listeners
		/// registered on this component.
		/// </summary>
		public PropertyChangeListener[] getPropertyChangeListeners()
		{
			return default(PropertyChangeListener[]);
		}

		/// <summary>
		/// Returns an array of all the listeners which have been associated
		/// with the named property.
		/// </summary>
		public PropertyChangeListener[] getPropertyChangeListeners(string @propertyName)
		{
			return default(PropertyChangeListener[]);
		}

		/// <summary>
		/// Returns the size of this component in the form of a
		/// <code>Dimension</code> object.
		/// </summary>
		public Dimension getSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Stores the width/height of this component into "return value" <b>rv</b>
		/// and return <b>rv</b>.
		/// </summary>
		public Dimension getSize(Dimension @rv)
		{
			return default(Dimension);
		}

		/// <summary>
		/// Gets the toolkit of this component.
		/// </summary>
		public Toolkit getToolkit()
		{
			return default(Toolkit);
		}

		/// <summary>
		/// Gets this component's locking object (the object that owns the thread
		/// sychronization monitor) for AWT component-tree and layout
		/// operations.
		/// </summary>
		public object getTreeLock()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the current width of this component.
		/// </summary>
		public int getWidth()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the current x coordinate of the components origin.
		/// </summary>
		public int getX()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the current y coordinate of the components origin.
		/// </summary>
		public int getY()
		{
			return default(int);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by processFocusEvent(FocusEvent).</I>
		/// </summary>
		public bool gotFocus(Event @evt, object @what)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1
		/// replaced by processEvent(AWTEvent).</I>
		/// </summary>
		public bool handleEvent(Event @evt)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if this <code>Component</code> is the
		/// focus owner.
		/// </summary>
		public bool hasFocus()
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>setVisible(boolean)</code>.</I>
		/// </summary>
		public void hide()
		{
		}

		/// <summary>
		/// Repaints the component when the image has changed.
		/// </summary>
		public bool imageUpdate(Image @img, int @infoflags, int @x, int @y, int @w, int @h)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by contains(int, int).</I>
		/// </summary>
		public bool inside(int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// Invalidates this component.
		/// </summary>
		public void invalidate()
		{
		}

		/// <summary>
		/// Returns whether the background color has been explicitly set for this
		/// Component.
		/// </summary>
		public bool isBackgroundSet()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the cursor has been explicitly set for this Component.
		/// </summary>
		public bool isCursorSet()
		{
			return default(bool);
		}

		/// <summary>
		/// Determines whether this component is displayable.
		/// </summary>
		public bool isDisplayable()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if this component is painted to an offscreen image
		/// ("buffer") that's copied to the screen later.
		/// </summary>
		public bool isDoubleBuffered()
		{
			return default(bool);
		}

		/// <summary>
		/// Determines whether this component is enabled.
		/// </summary>
		public bool isEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether this Component can be focused.
		/// </summary>
		public bool isFocusable()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the specified Container is the focus cycle root of this
		/// Component's focus traversal cycle.
		/// </summary>
		public bool isFocusCycleRoot(Container @container)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if this <code>Component</code> is the
		/// focus owner.
		/// </summary>
		public bool isFocusOwner()
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of 1.4, replaced by <code>isFocusable()</code>.</I>
		/// </summary>
		public bool isFocusTraversable()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the font has been explicitly set for this Component.
		/// </summary>
		public bool isFontSet()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the foreground color has been explicitly set for this
		/// Component.
		/// </summary>
		public bool isForegroundSet()
		{
			return default(bool);
		}

		/// <summary>
		/// A lightweight component doesn't have a native toolkit peer.
		/// </summary>
		public bool isLightweight()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if this component is completely opaque, returns
		/// false by default.
		/// </summary>
		public bool isOpaque()
		{
			return default(bool);
		}

		/// <summary>
		/// Determines whether this component is showing on screen.
		/// </summary>
		public bool isShowing()
		{
			return default(bool);
		}

		/// <summary>
		/// Determines whether this component is valid.
		/// </summary>
		public bool isValid()
		{
			return default(bool);
		}

		/// <summary>
		/// Determines whether this component should be visible when its
		/// parent is visible.
		/// </summary>
		public bool isVisible()
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by processKeyEvent(KeyEvent).</I>
		/// </summary>
		public bool keyDown(Event @evt, int @key)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by processKeyEvent(KeyEvent).</I>
		/// </summary>
		public bool keyUp(Event @evt, int @key)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>doLayout()</code>.</I>
		/// </summary>
		public void layout()
		{
		}

		/// <summary>
		/// Prints a listing of this component to the standard system output
		/// stream <code>System.out</code>.
		/// </summary>
		public void list()
		{
		}

		/// <summary>
		/// Prints a listing of this component to the specified output
		/// stream.
		/// </summary>
		public void list(PrintStream @out)
		{
		}

		/// <summary>
		/// Prints out a list, starting at the specified indentation, to the
		/// specified print stream.
		/// </summary>
		public void list(PrintStream @out, int @indent)
		{
		}

		/// <summary>
		/// Prints a listing to the specified print writer.
		/// </summary>
		public void list(PrintWriter @out)
		{
		}

		/// <summary>
		/// Prints out a list, starting at the specified indentation, to
		/// the specified print writer.
		/// </summary>
		public void list(PrintWriter @out, int @indent)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by getComponentAt(int, int).</I>
		/// </summary>
		public Component locate(int @x, int @y)
		{
			return default(Component);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>getLocation()</code>.</I>
		/// </summary>
		public Point location()
		{
			return default(Point);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by processFocusEvent(FocusEvent).</I>
		/// </summary>
		public bool lostFocus(Event @evt, object @what)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>getMinimumSize()</code>.</I>
		/// </summary>
		public Dimension minimumSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by processMouseEvent(MouseEvent).</I>
		/// </summary>
		public bool mouseDown(Event @evt, int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by processMouseMotionEvent(MouseEvent).</I>
		/// </summary>
		public bool mouseDrag(Event @evt, int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by processMouseEvent(MouseEvent).</I>
		/// </summary>
		public bool mouseEnter(Event @evt, int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by processMouseEvent(MouseEvent).</I>
		/// </summary>
		public bool mouseExit(Event @evt, int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by processMouseMotionEvent(MouseEvent).</I>
		/// </summary>
		public bool mouseMove(Event @evt, int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by processMouseEvent(MouseEvent).</I>
		/// </summary>
		public bool mouseUp(Event @evt, int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>setLocation(int, int)</code>.</I>
		/// </summary>
		public void move(int @x, int @y)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by transferFocus().</I>
		/// </summary>
		public void nextFocus()
		{
		}

		/// <summary>
		/// Paints this component.
		/// </summary>
		public void paint(Graphics @g)
		{
		}

		/// <summary>
		/// Paints this component and all of its subcomponents.
		/// </summary>
		public void paintAll(Graphics @g)
		{
		}

		/// <summary>
		/// Returns a string representing the state of this component.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by dispatchEvent(AWTEvent).</I>
		/// </summary>
		public bool postEvent(Event @e)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>getPreferredSize()</code>.</I>
		/// </summary>
		public Dimension preferredSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Prepares an image for rendering on this component.
		/// </summary>
		public bool prepareImage(Image @image, ImageObserver @observer)
		{
			return default(bool);
		}

		/// <summary>
		/// Prepares an image for rendering on this component at the
		/// specified width and height.
		/// </summary>
		public bool prepareImage(Image @image, int @width, int @height, ImageObserver @observer)
		{
			return default(bool);
		}

		/// <summary>
		/// Prints this component.
		/// </summary>
		public void print(Graphics @g)
		{
		}

		/// <summary>
		/// Prints this component and all of its subcomponents.
		/// </summary>
		public void printAll(Graphics @g)
		{
		}

		/// <summary>
		/// Processes component events occurring on this component by
		/// dispatching them to any registered
		/// <code>ComponentListener</code> objects.
		/// </summary>
		protected void processComponentEvent(ComponentEvent @e)
		{
		}

		/// <summary>
		/// Processes events occurring on this component.
		/// </summary>
		protected void processEvent(AWTEvent @e)
		{
		}

		/// <summary>
		/// Processes focus events occurring on this component by
		/// dispatching them to any registered
		/// <code>FocusListener</code> objects.
		/// </summary>
		protected void processFocusEvent(FocusEvent @e)
		{
		}

		/// <summary>
		/// Processes hierarchy bounds events occurring on this component by
		/// dispatching them to any registered
		/// <code>HierarchyBoundsListener</code> objects.
		/// </summary>
		protected void processHierarchyBoundsEvent(HierarchyEvent @e)
		{
		}

		/// <summary>
		/// Processes hierarchy events occurring on this component by
		/// dispatching them to any registered
		/// <code>HierarchyListener</code> objects.
		/// </summary>
		protected void processHierarchyEvent(HierarchyEvent @e)
		{
		}

		/// <summary>
		/// Processes input method events occurring on this component by
		/// dispatching them to any registered
		/// <code>InputMethodListener</code> objects.
		/// </summary>
		protected void processInputMethodEvent(InputMethodEvent @e)
		{
		}

		/// <summary>
		/// Processes key events occurring on this component by
		/// dispatching them to any registered
		/// <code>KeyListener</code> objects.
		/// </summary>
		protected void processKeyEvent(KeyEvent @e)
		{
		}

		/// <summary>
		/// Processes mouse events occurring on this component by
		/// dispatching them to any registered
		/// <code>MouseListener</code> objects.
		/// </summary>
		protected void processMouseEvent(MouseEvent @e)
		{
		}

		/// <summary>
		/// Processes mouse motion events occurring on this component by
		/// dispatching them to any registered
		/// <code>MouseMotionListener</code> objects.
		/// </summary>
		protected void processMouseMotionEvent(MouseEvent @e)
		{
		}

		/// <summary>
		/// Processes mouse wheel events occurring on this component by
		/// dispatching them to any registered
		/// <code>MouseWheelListener</code> objects.
		/// </summary>
		protected void processMouseWheelEvent(MouseWheelEvent @e)
		{
		}

		/// <summary>
		/// Removes the specified popup menu from the component.
		/// </summary>
		public void remove(MenuComponent @popup)
		{
		}

		/// <summary>
		/// Removes the specified component listener so that it no longer
		/// receives component events from this component.
		/// </summary>
		public void removeComponentListener(ComponentListener @l)
		{
		}

		/// <summary>
		/// Removes the specified focus listener so that it no longer
		/// receives focus events from this component.
		/// </summary>
		public void removeFocusListener(FocusListener @l)
		{
		}

		/// <summary>
		/// Removes the specified hierarchy bounds listener so that it no longer
		/// receives hierarchy bounds events from this component.
		/// </summary>
		public void removeHierarchyBoundsListener(HierarchyBoundsListener @l)
		{
		}

		/// <summary>
		/// Removes the specified hierarchy listener so that it no longer
		/// receives hierarchy changed events from this component.
		/// </summary>
		public void removeHierarchyListener(HierarchyListener @l)
		{
		}

		/// <summary>
		/// Removes the specified input method listener so that it no longer
		/// receives input method events from this component.
		/// </summary>
		public void removeInputMethodListener(InputMethodListener @l)
		{
		}

		/// <summary>
		/// Removes the specified key listener so that it no longer
		/// receives key events from this component.
		/// </summary>
		public void removeKeyListener(KeyListener @l)
		{
		}

		/// <summary>
		/// Removes the specified mouse listener so that it no longer
		/// receives mouse events from this component.
		/// </summary>
		public void removeMouseListener(MouseListener @l)
		{
		}

		/// <summary>
		/// Removes the specified mouse motion listener so that it no longer
		/// receives mouse motion events from this component.
		/// </summary>
		public void removeMouseMotionListener(MouseMotionListener @l)
		{
		}

		/// <summary>
		/// Removes the specified mouse wheel listener so that it no longer
		/// receives mouse wheel events from this component.
		/// </summary>
		public void removeMouseWheelListener(MouseWheelListener @l)
		{
		}

		/// <summary>
		/// Makes this <code>Component</code> undisplayable by destroying it native
		/// screen resource.
		/// </summary>
		public void removeNotify()
		{
		}

		/// <summary>
		/// Removes a PropertyChangeListener from the listener list.
		/// </summary>
		public void removePropertyChangeListener(PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Removes a PropertyChangeListener from the listener list for a specific
		/// property.
		/// </summary>
		public void removePropertyChangeListener(string @propertyName, PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Repaints this component.
		/// </summary>
		public void repaint()
		{
		}

		/// <summary>
		/// Repaints the specified rectangle of this component.
		/// </summary>
		public void repaint(int @x, int @y, int @width, int @height)
		{
		}

		/// <summary>
		/// Repaints the component.
		/// </summary>
		public void repaint(long @tm)
		{
		}

		/// <summary>
		/// Repaints the specified rectangle of this component within
		/// <code>tm</code> milliseconds.
		/// </summary>
		public void repaint(long @tm, int @x, int @y, int @width, int @height)
		{
		}

		/// <summary>
		/// Requests that this Component get the input focus, and that this
		/// Component's top-level ancestor become the focused Window.
		/// </summary>
		public void requestFocus()
		{
		}

		/// <summary>
		/// Requests that this Component get the input focus, and that this
		/// Component's top-level ancestor become the focused Window.
		/// </summary>
		protected bool requestFocus(bool @temporary)
		{
			return default(bool);
		}

		/// <summary>
		/// Requests that this Component get the input focus, if this Component's
		/// top-level ancestor is already the focused Window.
		/// </summary>
		public bool requestFocusInWindow()
		{
			return default(bool);
		}

		/// <summary>
		/// Requests that this Component get the input focus, if this Component's
		/// top-level ancestor is already the focused Window.
		/// </summary>
		protected bool requestFocusInWindow(bool @temporary)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>setBounds(int, int, int, int)</code>.</I>
		/// </summary>
		public void reshape(int @x, int @y, int @width, int @height)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>setSize(Dimension)</code>.</I>
		/// </summary>
		public void resize(Dimension @d)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>setSize(int, int)</code>.</I>
		/// </summary>
		public void resize(int @width, int @height)
		{
		}

		/// <summary>
		/// Sets the background color of this component.
		/// </summary>
		public void setBackground(Color @c)
		{
		}

		/// <summary>
		/// Moves and resizes this component.
		/// </summary>
		public void setBounds(int @x, int @y, int @width, int @height)
		{
		}

		/// <summary>
		/// Moves and resizes this component to conform to the new
		/// bounding rectangle <code>r</code>.
		/// </summary>
		public void setBounds(Rectangle @r)
		{
		}

		/// <summary>
		/// Sets the language-sensitive orientation that is to be used to order
		/// the elements or text within this component.
		/// </summary>
		public void setComponentOrientation(ComponentOrientation @o)
		{
		}

		/// <summary>
		/// Sets the cursor image to the specified cursor.
		/// </summary>
		public void setCursor(Cursor @cursor)
		{
		}

		/// <summary>
		/// Associate a <code>DropTarget</code> with this component.
		/// </summary>
		//public void setDropTarget(DropTarget @dt)
		//{
		//}

		/// <summary>
		/// Enables or disables this component, depending on the value of the
		/// parameter <code>b</code>.
		/// </summary>
		public void setEnabled(bool @b)
		{
		}

		/// <summary>
		/// Sets the focusable state of this Component to the specified value.
		/// </summary>
		public void setFocusable(bool @focusable)
		{
		}

		/// <summary>
		/// Sets the focus traversal keys for a given traversal operation for this
		/// Component.
		/// </summary>
		public void setFocusTraversalKeys(int @id, Set @keystrokes)
		{
		}

		/// <summary>
		/// Sets whether focus traversal keys are enabled for this Component.
		/// </summary>
		public void setFocusTraversalKeysEnabled(bool @focusTraversalKeysEnabled)
		{
		}

		/// <summary>
		/// Sets the font of this component.
		/// </summary>
		public void setFont(Font @f)
		{
		}

		/// <summary>
		/// Sets the foreground color of this component.
		/// </summary>
		public void setForeground(Color @c)
		{
		}

		/// <summary>
		/// Sets whether or not paint messages received from the operating system
		/// should be ignored.
		/// </summary>
		public void setIgnoreRepaint(bool @ignoreRepaint)
		{
		}

		/// <summary>
		/// Sets the locale of this component.
		/// </summary>
		public void setLocale(Locale @l)
		{
		}

		/// <summary>
		/// Moves this component to a new location.
		/// </summary>
		public void setLocation(int @x, int @y)
		{
		}

		/// <summary>
		/// Moves this component to a new location.
		/// </summary>
		public void setLocation(Point @p)
		{
		}

		/// <summary>
		/// Sets the name of the component to the specified string.
		/// </summary>
		public void setName(string @name)
		{
		}

		/// <summary>
		/// Resizes this component so that it has width <code>d.width</code>
		/// and height <code>d.height</code>.
		/// </summary>
		public void setSize(Dimension @d)
		{
		}

		/// <summary>
		/// Resizes this component so that it has width <code>width</code>
		/// and height <code>height</code>.
		/// </summary>
		public void setSize(int @width, int @height)
		{
		}

		/// <summary>
		/// Shows or hides this component depending on the value of parameter
		/// <code>b</code>.
		/// </summary>
		public void setVisible(bool @b)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>setVisible(boolean)</code>.</I>
		/// </summary>
		public void show()
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>setVisible(boolean)</code>.</I>
		/// </summary>
		public void show(bool @b)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>getSize()</code>.</I>
		/// </summary>
		public Dimension size()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns a string representation of this component and its values.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

		/// <summary>
		/// Transfers the focus to the next component, as though this Component were
		/// the focus owner.
		/// </summary>
		public void transferFocus()
		{
		}

		/// <summary>
		/// Transfers the focus to the previous component, as though this Component
		/// were the focus owner.
		/// </summary>
		public void transferFocusBackward()
		{
		}

		/// <summary>
		/// Transfers the focus up one focus traversal cycle.
		/// </summary>
		public void transferFocusUpCycle()
		{
		}

		/// <summary>
		/// Updates this component.
		/// </summary>
		public void update(Graphics @g)
		{
		}

		/// <summary>
		/// Ensures that this component has a valid layout.
		/// </summary>
		public void validate()
		{
		}

	}
}

