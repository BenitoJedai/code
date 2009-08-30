// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.beans;
using java.io;
using java.lang;
using java.util;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Container.html
	[Script(IsNative = true)]
	public class Container : Component
	{
		/// <summary>
		/// Constructs a new Container.
		/// </summary>
		public Container()
		{
		}

		/// <summary>
		/// Appends the specified component to the end of this container.
		/// </summary>
		public Component add(Component @comp)
		{
			return default(Component);
		}

		/// <summary>
		/// Adds the specified component to this container at the given
		/// position.
		/// </summary>
		public Component add(Component @comp, int @index)
		{
			return default(Component);
		}

		/// <summary>
		/// Adds the specified component to the end of this container.
		/// </summary>
		public void add(Component @comp, object @constraints)
		{
		}

		/// <summary>
		/// Adds the specified component to this container with the specified
		/// constraints at the specified index.
		/// </summary>
		public void add(Component @comp, object @constraints, int @index)
		{
		}

		/// <summary>
		/// Adds the specified component to this container.
		/// </summary>
		public Component add(string @name, Component @comp)
		{
			return default(Component);
		}

		/// <summary>
		/// Adds the specified container listener to receive container events
		/// from this container.
		/// </summary>
		public void addContainerListener(ContainerListener @l)
		{
		}

		/// <summary>
		/// Adds the specified component to this container at the specified
		/// index.
		/// </summary>
		protected void addImpl(Component @comp, object @constraints, int @index)
		{
		}

		/// <summary>
		/// Makes this Container displayable by connecting it to
		/// a native screen resource.
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
		/// Sets the <code>ComponentOrientation</code> property of this container
		/// and all components contained within it.
		/// </summary>
		public void applyComponentOrientation(ComponentOrientation @o)
		{
		}

		/// <summary>
		/// Returns whether the Set of focus traversal keys for the given focus
		/// traversal operation has been explicitly defined for this Container.
		/// </summary>
		public bool areFocusTraversalKeysSet(int @id)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by getComponentCount().</I>
		/// </summary>
		public int countComponents()
		{
			return default(int);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>dispatchEvent(AWTEvent e)</code></I>
		/// </summary>
		public void deliverEvent(Event @e)
		{
		}

		/// <summary>
		/// Causes this container to lay out its components.
		/// </summary>
		public void doLayout()
		{
		}

		/// <summary>
		/// Locates the visible child component that contains the specified
		/// position.
		/// </summary>
		public Component findComponentAt(int @x, int @y)
		{
			return default(Component);
		}

		/// <summary>
		/// Locates the visible child component that contains the specified
		/// point.
		/// </summary>
		public Component findComponentAt(Point @p)
		{
			return default(Component);
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
		/// Gets the nth component in this container.
		/// </summary>
		public Component getComponent(int @n)
		{
			return default(Component);
		}

		/// <summary>
		/// Locates the component that contains the x,y position.
		/// </summary>
		public Component getComponentAt(int @x, int @y)
		{
			return default(Component);
		}

		/// <summary>
		/// Gets the component that contains the specified point.
		/// </summary>
		public Component getComponentAt(Point @p)
		{
			return default(Component);
		}

		/// <summary>
		/// Gets the number of components in this panel.
		/// </summary>
		public int getComponentCount()
		{
			return default(int);
		}

		/// <summary>
		/// Gets all the components in this container.
		/// </summary>
		public Component[] getComponents()
		{
			return default(Component[]);
		}

		/// <summary>
		/// Returns an array of all the container listeners
		/// registered on this container.
		/// </summary>
		public ContainerListener[] getContainerListeners()
		{
			return default(ContainerListener[]);
		}

		/// <summary>
		/// Returns the Set of focus traversal keys for a given traversal operation
		/// for this Container.
		/// </summary>
		public Set getFocusTraversalKeys(int @id)
		{
			return default(Set);
		}

		/// <summary>
		/// Returns the focus traversal policy that will manage keyboard traversal
		/// of this Container's children, or null if this Container is not a focus
		/// cycle root.
		/// </summary>
		public FocusTraversalPolicy getFocusTraversalPolicy()
		{
			return default(FocusTraversalPolicy);
		}

		/// <summary>
		/// Determines the insets of this container, which indicate the size
		/// of the container's border.
		/// </summary>
		public Insets getInsets()
		{
			return default(Insets);
		}

		/// <summary>
		/// Gets the layout manager for this container.
		/// </summary>
		public LayoutManager getLayout()
		{
			return default(LayoutManager);
		}

		/// <summary>
		/// Returns an array of all the objects currently registered
		/// as <code><em>Foo</em>Listener</code>s
		/// upon this <code>Container</code>.
		/// </summary>
		public EventListener[] getListeners(Class @listenerType)
		{
			return default(EventListener[]);
		}

		/// <summary>
		/// Returns the maximum size of this container.
		/// </summary>
		public Dimension getMaximumSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the minimum size of this container.
		/// </summary>
		public Dimension getMinimumSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the preferred size of this container.
		/// </summary>
		public Dimension getPreferredSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>getInsets()</code>.</I>
		/// </summary>
		public Insets insets()
		{
			return default(Insets);
		}

		/// <summary>
		/// Invalidates the container.
		/// </summary>
		public void invalidate()
		{
		}

		/// <summary>
		/// Checks if the component is contained in the component hierarchy of
		/// this container.
		/// </summary>
		public bool isAncestorOf(Component @c)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether this Container is the root of a focus traversal cycle.
		/// </summary>
		public bool isFocusCycleRoot()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the specified Container is the focus cycle root of this
		/// Container's focus traversal cycle.
		/// </summary>
		public bool isFocusCycleRoot(Container @container)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the focus traversal policy has been explicitly set for
		/// this Container.
		/// </summary>
		public bool isFocusTraversalPolicySet()
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
		/// Prints a listing of this container to the specified output
		/// stream.
		/// </summary>
		public void list(PrintStream @out, int @indent)
		{
		}

		/// <summary>
		/// Prints out a list, starting at the specified indention, to the specified
		/// print writer.
		/// </summary>
		public void list(PrintWriter @out, int @indent)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>getComponentAt(int, int)</code>.</I>
		/// </summary>
		public Component locate(int @x, int @y)
		{
			return default(Component);
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
		/// Paints the container.
		/// </summary>
		public void paint(Graphics @g)
		{
		}

		/// <summary>
		/// Paints each of the components in this container.
		/// </summary>
		public void paintComponents(Graphics @g)
		{
		}

		/// <summary>
		/// Returns a string representing the state of this <code>Container</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
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
		/// Prints the container.
		/// </summary>
		public void print(Graphics @g)
		{
		}

		/// <summary>
		/// Prints each of the components in this container.
		/// </summary>
		public void printComponents(Graphics @g)
		{
		}

		/// <summary>
		/// Processes container events occurring on this container by
		/// dispatching them to any registered ContainerListener objects.
		/// </summary>
		protected void processContainerEvent(ContainerEvent @e)
		{
		}

		/// <summary>
		/// Processes events on this container.
		/// </summary>
		protected void processEvent(AWTEvent @e)
		{
		}

		/// <summary>
		/// Removes the specified component from this container.
		/// </summary>
		public void remove(Component @comp)
		{
		}

		/// <summary>
		/// Removes the component, specified by <code>index</code>,
		/// from this container.
		/// </summary>
		public void remove(int @index)
		{
		}

		/// <summary>
		/// Removes all the components from this container.
		/// </summary>
		public void removeAll()
		{
		}

		/// <summary>
		/// Removes the specified container listener so it no longer receives
		/// container events from this container.
		/// </summary>
		public void removeContainerListener(ContainerListener @l)
		{
		}

		/// <summary>
		/// Makes this Container undisplayable by removing its connection
		/// to its native screen resource.
		/// </summary>
		public void removeNotify()
		{
		}

		/// <summary>
		/// Sets whether this Container is the root of a focus traversal cycle.
		/// </summary>
		public void setFocusCycleRoot(bool @focusCycleRoot)
		{
		}

		/// <summary>
		/// Sets the focus traversal keys for a given traversal operation for this
		/// Container.
		/// </summary>
		public void setFocusTraversalKeys(int @id, Set @keystrokes)
		{
		}

		/// <summary>
		/// Sets the focus traversal policy that will manage keyboard traversal of
		/// this Container's children, if this Container is a focus cycle root.
		/// </summary>
		public void setFocusTraversalPolicy(FocusTraversalPolicy @policy)
		{
		}

		/// <summary>
		/// Sets the font of this container.
		/// </summary>
		public void setFont(Font @f)
		{
		}

		/// <summary>
		/// Sets the layout manager for this container.
		/// </summary>
		public void setLayout(LayoutManager @mgr)
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
		/// Transfers the focus down one focus traversal cycle.
		/// </summary>
		public void transferFocusDownCycle()
		{
		}

		/// <summary>
		/// Updates the container.
		/// </summary>
		public void update(Graphics @g)
		{
		}

		/// <summary>
		/// Validates this container and all of its subcomponents.
		/// </summary>
		public void validate()
		{
		}

		/// <summary>
		/// Recursively descends the container tree and recomputes the
		/// layout for any subtrees marked as needing it (those marked as
		/// invalid).
		/// </summary>
		protected void validateTree()
		{
		}

	}
}

