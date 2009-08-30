// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.beans;
using java.lang;
using java.util;
using javax.accessibility;
using javax.swing;
using javax.swing.border;
using javax.swing.@event;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JComponent.html
	[Script(IsNative = true)]
	public class JComponent : Container
	{
		/// <summary>
		/// Default <code>JComponent</code> constructor.
		/// </summary>
		public JComponent()
		{
		}

		/// <summary>
		/// Registers <code>listener</code> so that it will receive
		/// <code>AncestorEvents</code> when it or any of its ancestors
		/// move or are made visible or invisible.
		/// </summary>
		public void addAncestorListener(AncestorListener @listener)
		{
		}

		/// <summary>
		/// Notifies this component that it now has a parent component.
		/// </summary>
		public void addNotify()
		{
		}

		/// <summary>
		/// Adds a <code>PropertyChangeListener</code> to the listener list.
		/// </summary>
		public void addPropertyChangeListener(PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Adds a <code>PropertyChangeListener</code> for a specific property.
		/// </summary>
		public void addPropertyChangeListener(string @propertyName, PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Adds a <code>VetoableChangeListener</code> to the listener list.
		/// </summary>
		public void addVetoableChangeListener(VetoableChangeListener @listener)
		{
		}

		/// <summary>
		/// Returns the <code>Component</code>'s "visible rect rectangle" -  the
		/// intersection of the visible rectangles for this component
		/// and all of its ancestors.
		/// </summary>
		public void computeVisibleRect(Rectangle @visibleRect)
		{
		}

		/// <summary>
		/// Gives the UI delegate an opportunity to define the precise
		/// shape of this component for the sake of mouse processing.
		/// </summary>
		public bool contains(int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the instance of <code>JToolTip</code> that should be used
		/// to display the tooltip.
		/// </summary>
		public JToolTip createToolTip()
		{
			return default(JToolTip);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>java.awt.Component.setEnable(boolean)</code>.</I>
		/// </summary>
		public void disable()
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>java.awt.Component.setEnable(boolean)</code>.</I>
		/// </summary>
		public void enable()
		{
		}

		/// <summary>
		/// Reports a bound property change.
		/// </summary>
		public void firePropertyChange(string @propertyName, bool @oldValue, bool @newValue)
		{
		}

		/// <summary>
		/// Reports a bound property change.
		/// </summary>
		public void firePropertyChange(string @propertyName, byte @oldValue, byte @newValue)
		{
		}

		/// <summary>
		/// Reports a bound property change.
		/// </summary>
		public void firePropertyChange(string @propertyName, char @oldValue, char @newValue)
		{
		}

		/// <summary>
		/// Reports a bound property change.
		/// </summary>
		public void firePropertyChange(string @propertyName, double @oldValue, double @newValue)
		{
		}

		/// <summary>
		/// Reports a bound property change.
		/// </summary>
		public void firePropertyChange(string @propertyName, float @oldValue, float @newValue)
		{
		}

		/// <summary>
		/// Reports a bound property change.
		/// </summary>
		public void firePropertyChange(string @propertyName, int @oldValue, int @newValue)
		{
		}

		/// <summary>
		/// Reports a bound property change.
		/// </summary>
		public void firePropertyChange(string @propertyName, long @oldValue, long @newValue)
		{
		}

		/// <summary>
		/// Supports reporting bound property changes.
		/// </summary>
		protected void firePropertyChange(string @propertyName, object @oldValue, object @newValue)
		{
		}

		/// <summary>
		/// Reports a bound property change.
		/// </summary>
		public void firePropertyChange(string @propertyName, short @oldValue, short @newValue)
		{
		}

		/// <summary>
		/// Supports reporting constrained property changes.
		/// </summary>
		protected void fireVetoableChange(string @propertyName, object @oldValue, object @newValue)
		{
		}

		/// <summary>
		/// Returns the <code>AccessibleContext</code> associated with this
		/// <code>JComponent</code>.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the object that will perform the action registered for a
		/// given keystroke.
		/// </summary>
		public ActionListener getActionForKeyStroke(KeyStroke @aKeyStroke)
		{
			return default(ActionListener);
		}

		/// <summary>
		/// Returns the <code>ActionMap</code> used to determine what
		/// <code>Action</code> to fire for particular <code>KeyStroke</code>
		/// binding.
		/// </summary>
		public ActionMap getActionMap()
		{
			return default(ActionMap);
		}

		/// <summary>
		/// Overrides <code>Container.getAlignmentX</code> to return
		/// the vertical alignment.
		/// </summary>
		public float getAlignmentX()
		{
			return default(float);
		}

		/// <summary>
		/// Overrides <code>Container.getAlignmentY</code> to return
		/// the horizontal alignment.
		/// </summary>
		public float getAlignmentY()
		{
			return default(float);
		}

		/// <summary>
		/// Returns an array of all the ancestor listeners
		/// registered on this component.
		/// </summary>
		public AncestorListener getAncestorListeners()
		{
			return default(AncestorListener);
		}

		/// <summary>
		/// Gets the <code>autoscrolls</code> property.
		/// </summary>
		public bool getAutoscrolls()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the border of this component or <code>null</code> if no
		/// border is currently set.
		/// </summary>
		public Border getBorder()
		{
			return default(Border);
		}

		/// <summary>
		/// Stores the bounds of this component into "return value"
		/// <code>rv</code> and returns <code>rv</code>.
		/// </summary>
		public Rectangle getBounds(Rectangle @rv)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the value of the property with the specified key.
		/// </summary>
		public object getClientProperty(object @key)
		{
			return default(object);
		}

		/// <summary>
		/// Returns the graphics object used to paint this component.
		/// </summary>
		public Graphics getComponentGraphics(Graphics @g)
		{
			return default(Graphics);
		}

		/// <summary>
		/// Returns the condition that determines whether a registered action
		/// occurs in response to the specified keystroke.
		/// </summary>
		public int getConditionForKeyStroke(KeyStroke @aKeyStroke)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the state of graphics debugging.
		/// </summary>
		public int getDebugGraphicsOptions()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the default locale used to initialize each JComponent's
		/// locale property upon creation.
		/// </summary>
		public Locale getDefaultLocale()
		{
			return default(Locale);
		}

		/// <summary>
		/// Returns this component's graphics context, which lets you draw
		/// on a component.
		/// </summary>
		public Graphics getGraphics()
		{
			return default(Graphics);
		}

		/// <summary>
		/// Returns the current height of this component.
		/// </summary>
		public int getHeight()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the <code>InputMap</code> that is used when the
		/// component has focus.
		/// </summary>
		public InputMap getInputMap()
		{
			return default(InputMap);
		}

		/// <summary>
		/// Returns the <code>InputMap</code> that is used during
		/// <code>condition</code>.
		/// </summary>
		public InputMap getInputMap(int @condition)
		{
			return default(InputMap);
		}

		/// <summary>
		/// Returns the input verifier for this component.
		/// </summary>
		public InputVerifier getInputVerifier()
		{
			return default(InputVerifier);
		}

		/// <summary>
		/// If a border has been set on this component, returns the
		/// border's insets; otherwise calls <code>super.getInsets</code>.
		/// </summary>
		public Insets getInsets()
		{
			return default(Insets);
		}

		/// <summary>
		/// Returns an <code>Insets</code> object containing this component's inset
		/// values.
		/// </summary>
		public Insets getInsets(Insets @insets)
		{
			return default(Insets);
		}

		/// <summary>
		/// Returns an array of all the objects currently registered
		/// as <code><em>Foo</em>Listener</code>s
		/// upon this <code>JComponent</code>.
		/// </summary>
		public EventListener getListeners(Class @listenerType)
		{
			return default(EventListener);
		}

		/// <summary>
		/// Stores the x,y origin of this component into "return value"
		/// <code>rv</code> and returns <code>rv</code>.
		/// </summary>
		public Point getLocation(Point @rv)
		{
			return default(Point);
		}

		/// <summary>
		/// If the maximum size has been set to a non-<code>null</code> value
		/// just returns it.
		/// </summary>
		public Dimension getMaximumSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// If the minimum size has been set to a non-<code>null</code> value
		/// just returns it.
		/// </summary>
		public Dimension getMinimumSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of 1.4, replaced by <code>FocusTraversalPolicy</code>.</I>
		/// </summary>
		public Component getNextFocusableComponent()
		{
			return default(Component);
		}

		/// <summary>
		/// If the <code>preferredSize</code> has been set to a
		/// non-<code>null</code> value just returns it.
		/// </summary>
		public Dimension getPreferredSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns an array of all the <code>PropertyChangeListener</code>s
		/// added to this Component with addPropertyChangeListener().
		/// </summary>
		public PropertyChangeListener getPropertyChangeListeners()
		{
			return default(PropertyChangeListener);
		}

		/// <summary>
		/// Returns an array of all the listeners which have been associated
		/// with the named property.
		/// </summary>
		public PropertyChangeListener getPropertyChangeListeners(string @propertyName)
		{
			return default(PropertyChangeListener);
		}

		/// <summary>
		/// Returns the <code>KeyStrokes</code> that will initiate
		/// registered actions.
		/// </summary>
		public KeyStroke getRegisteredKeyStrokes()
		{
			return default(KeyStroke);
		}

		/// <summary>
		/// Returns the <code>JRootPane</code> ancestor for this component.
		/// </summary>
		public JRootPane getRootPane()
		{
			return default(JRootPane);
		}

		/// <summary>
		/// Stores the width/height of this component into "return value"
		/// <code>rv</code> and returns <code>rv</code>.
		/// </summary>
		public Dimension getSize(Dimension @rv)
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the tooltip location in this component's coordinate system.
		/// </summary>
		public Point getToolTipLocation(MouseEvent @event)
		{
			return default(Point);
		}

		/// <summary>
		/// Returns the tooltip string that has been set with
		/// <code>setToolTipText</code>.
		/// </summary>
		public string getToolTipText()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the string to be used as the tooltip for <i>event</i>.
		/// </summary>
		public string getToolTipText(MouseEvent @event)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the top-level ancestor of this component (either the
		/// containing <code>Window</code> or <code>Applet</code>),
		/// or <code>null</code> if this component has not
		/// been added to any container.
		/// </summary>
		public Container getTopLevelAncestor()
		{
			return default(Container);
		}

		/// <summary>
		/// Gets the <code>transferHandler</code> property.
		/// </summary>
		public TransferHandler getTransferHandler()
		{
			return default(TransferHandler);
		}

		/// <summary>
		/// Returns the <code>UIDefaults</code> key used to
		/// look up the name of the <code>swing.plaf.ComponentUI</code>
		/// class that defines the look and feel
		/// for this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the value that indicates whether the input verifier for the
		/// current focus owner will be called before this component requests
		/// focus.
		/// </summary>
		public bool getVerifyInputWhenFocusTarget()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns an array of all the vetoable change listeners
		/// registered on this component.
		/// </summary>
		public VetoableChangeListener getVetoableChangeListeners()
		{
			return default(VetoableChangeListener);
		}

		/// <summary>
		/// Returns the <code>Component</code>'s "visible rectangle" -  the
		/// intersection of this component's visible rectangle:
		/// </summary>
		public Rectangle getVisibleRect()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the current width of this component.
		/// </summary>
		public int getWidth()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the current x coordinate of the component's origin.
		/// </summary>
		public int getX()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the current y coordinate of the component's origin.
		/// </summary>
		public int getY()
		{
			return default(int);
		}

		/// <summary>
		/// Requests that this Component get the input focus, and that this
		/// Component's top-level ancestor become the focused Window.
		/// </summary>
		public void grabFocus()
		{
		}

		/// <summary>
		/// Returns whether this component should use a buffer to paint.
		/// </summary>
		public bool isDoubleBuffered()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if this component is lightweight, that is, if it doesn't
		/// have a native window system peer.
		/// </summary>
		static public bool isLightweightComponent(Component @c)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of 1.4, replaced by
		/// <code>Component.setFocusTraversalKeys(int, Set)</code> and
		/// <code>Container.setFocusCycleRoot(boolean)</code>.</I>
		/// </summary>
		public bool isManagingFocus()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the maximum size has been set to a non-<code>null</code>
		/// value otherwise returns false.
		/// </summary>
		public bool isMaximumSizeSet()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the minimum size has been set to a non-<code>null</code>
		/// value otherwise returns false.
		/// </summary>
		public bool isMinimumSizeSet()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if this component is completely opaque.
		/// </summary>
		public bool isOpaque()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if this component tiles its children -- that is, if
		/// it can guarantee that the children will not overlap.
		/// </summary>
		public bool isOptimizedDrawingEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the component is currently painting a tile.
		/// </summary>
		public bool isPaintingTile()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the preferred size has been set to a
		/// non-<code>null</code> value otherwise returns false.
		/// </summary>
		public bool isPreferredSizeSet()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if this <code>JComponent</code> should
		/// get focus; otherwise returns <code>false</code>.
		/// </summary>
		public bool isRequestFocusEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// If this method returns true, <code>revalidate</code> calls by
		/// descendants of this component will cause the entire tree
		/// beginning with this root to be validated.
		/// </summary>
		public bool isValidateRoot()
		{
			return default(bool);
		}

		/// <summary>
		/// Invoked by Swing to draw components.
		/// </summary>
		public void paint(Graphics @g)
		{
		}

		/// <summary>
		/// Paints the component's border.
		/// </summary>
		protected void paintBorder(Graphics @g)
		{
		}

		/// <summary>
		/// Paints this component's children.
		/// </summary>
		protected void paintChildren(Graphics @g)
		{
		}

		/// <summary>
		/// Calls the UI delegate's paint method, if the UI delegate
		/// is non-<code>null</code>.
		/// </summary>
		protected virtual void paintComponent(Graphics @g)
		{
		}

		/// <summary>
		/// Paints the specified region in this component and all of its
		/// descendants that overlap the region, immediately.
		/// </summary>
		public void paintImmediately(int @x, int @y, int @w, int @h)
		{
		}

		/// <summary>
		/// Paints the specified region now.
		/// </summary>
		public void paintImmediately(Rectangle @r)
		{
		}

		/// <summary>
		/// Returns a string representation of this <code>JComponent</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Invoke this method to print the component.
		/// </summary>
		public void print(Graphics @g)
		{
		}

		/// <summary>
		/// Invoke this method to print the component.
		/// </summary>
		public void printAll(Graphics @g)
		{
		}

		/// <summary>
		/// Prints the component's border.
		/// </summary>
		protected void printBorder(Graphics @g)
		{
		}

		/// <summary>
		/// Prints this component's children.
		/// </summary>
		protected void printChildren(Graphics @g)
		{
		}

		/// <summary>
		/// This is invoked during a printing operation.
		/// </summary>
		protected void printComponent(Graphics @g)
		{
		}

		/// <summary>
		/// Processes any key events that the component itself
		/// recognizes.
		/// </summary>
		protected void processComponentKeyEvent(KeyEvent @e)
		{
		}

		/// <summary>
		/// Invoked to process the key bindings for <code>ks</code> as the result
		/// of the <code>KeyEvent</code> <code>e</code>.
		/// </summary>
		protected bool processKeyBinding(KeyStroke @ks, KeyEvent @e, int @condition, bool @pressed)
		{
			return default(bool);
		}

		/// <summary>
		/// Overrides <code>processKeyEvent</code> to process events.
		/// </summary>
		protected void processKeyEvent(KeyEvent @e)
		{
		}

		/// <summary>
		/// Processes mouse motion events, such as MouseEvent.MOUSE_DRAGGED.
		/// </summary>
		protected void processMouseMotionEvent(MouseEvent @e)
		{
		}

		/// <summary>
		/// Adds an arbitrary key/value "client property" to this component.
		/// </summary>
		public void putClientProperty(object @key, object @value)
		{
		}

		/// <summary>
		/// This method is now obsolete, please use a combination of
		/// <code>getActionMap()</code> and <code>getInputMap()</code> for
		/// similiar behavior.
		/// </summary>
		public void registerKeyboardAction(ActionListener @anAction, KeyStroke @aKeyStroke, int @aCondition)
		{
		}

		/// <summary>
		/// This method is now obsolete, please use a combination of
		/// <code>getActionMap()</code> and <code>getInputMap()</code> for
		/// similiar behavior.
		/// </summary>
		public void registerKeyboardAction(ActionListener @anAction, string @aCommand, KeyStroke @aKeyStroke, int @aCondition)
		{
		}

		/// <summary>
		/// Unregisters <code>listener</code> so that it will no longer receive
		/// <code>AncestorEvents</code>.
		/// </summary>
		public void removeAncestorListener(AncestorListener @listener)
		{
		}

		/// <summary>
		/// Notifies this component that it no longer has a parent component.
		/// </summary>
		public void removeNotify()
		{
		}

		/// <summary>
		/// Removes a <code>PropertyChangeListener</code> from the listener list.
		/// </summary>
		public void removePropertyChangeListener(PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Removes a <code>PropertyChangeListener</code> for a specific property.
		/// </summary>
		public void removePropertyChangeListener(string @propertyName, PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Removes a <code>VetoableChangeListener</code> from the listener list.
		/// </summary>
		public void removeVetoableChangeListener(VetoableChangeListener @listener)
		{
		}

		/// <summary>
		/// Adds the specified region to the dirty region list if the component
		/// is showing.
		/// </summary>
		public void repaint(long @tm, int @x, int @y, int @width, int @height)
		{
		}

		/// <summary>
		/// Adds the specified region to the dirty region list if the component
		/// is showing.
		/// </summary>
		public void repaint(Rectangle @r)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of 1.4, replaced by
		/// <code>FocusTraversalPolicy.getDefaultComponent(Container).requestFocus()</code></I>
		/// </summary>
		public bool requestDefaultFocus()
		{
			return default(bool);
		}

		/// <summary>
		/// Requests that this Component get the input focus, and that this
		/// Component's top-level ancestor become the focused Window.
		/// </summary>
		public void requestFocus()
		{
		}

		/// <summary>
		/// <code>JComponent</code> overrides <code>requestFocus</code> solely
		/// to make the method public so that UI implementations can cause
		/// temporary focus changes.
		/// </summary>
		public bool requestFocus(bool @temporary)
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
		/// <code>JComponent</code> overrides <code>requestFocusInWindow</code>
		/// solely to make the method public so that UI implementations can cause
		/// temporary focus changes.
		/// </summary>
		protected bool requestFocusInWindow(bool @temporary)
		{
			return default(bool);
		}

		/// <summary>
		/// Unregisters all the bindings in the first tier <code>InputMaps</code>
		/// and <code>ActionMap</code>.
		/// </summary>
		public void resetKeyboardActions()
		{
		}

		/// <summary>
		/// Moves and resizes this component.
		/// </summary>
		public void reshape(int @x, int @y, int @w, int @h)
		{
		}

		/// <summary>
		/// Supports deferred automatic layout.
		/// </summary>
		public void revalidate()
		{
		}

		/// <summary>
		/// Forwards the <code>scrollRectToVisible()</code> message to the
		/// <code>JComponent</code>'s parent.
		/// </summary>
		public void scrollRectToVisible(Rectangle @aRect)
		{
		}

		/// <summary>
		/// Sets the <code>ActionMap</code> to <code>am</code>.
		/// </summary>
		public void setActionMap(ActionMap @am)
		{
		}

		/// <summary>
		/// Sets the the vertical alignment.
		/// </summary>
		public void setAlignmentX(float @alignmentX)
		{
		}

		/// <summary>
		/// Sets the the horizontal alignment.
		/// </summary>
		public void setAlignmentY(float @alignmentY)
		{
		}

		/// <summary>
		/// Sets the <code>autoscrolls</code> property.
		/// </summary>
		public void setAutoscrolls(bool @autoscrolls)
		{
		}

		/// <summary>
		/// Sets the background color of this component.
		/// </summary>
		public void setBackground(Color @bg)
		{
		}

		/// <summary>
		/// Sets the border of this component.
		/// </summary>
		public void setBorder(Border @border)
		{
		}

		/// <summary>
		/// Enables or disables diagnostic information about every graphics
		/// operation performed within the component or one of its children.
		/// </summary>
		public void setDebugGraphicsOptions(int @debugOptions)
		{
		}

		/// <summary>
		/// Sets the default locale used to initialize each JComponent's locale
		/// property upon creation.
		/// </summary>
		static public void setDefaultLocale(Locale @l)
		{
		}

		/// <summary>
		/// Sets whether the this component should use a buffer to paint.
		/// </summary>
		public void setDoubleBuffered(bool @aFlag)
		{
		}

		/// <summary>
		/// Sets whether or not this component is enabled.
		/// </summary>
		public void setEnabled(bool @enabled)
		{
		}

		/// <summary>
		/// Sets the font for this component.
		/// </summary>
		public void setFont(Font @font)
		{
		}

		/// <summary>
		/// Sets the foreground color of this component.
		/// </summary>
		public void setForeground(Color @fg)
		{
		}

		/// <summary>
		/// Sets the <code>InputMap</code> to use under the condition
		/// <code>condition</code> to
		/// <code>map</code>.
		/// </summary>
		public void setInputMap(int @condition, InputMap @map)
		{
		}

		/// <summary>
		/// Sets the input verifier for this component.
		/// </summary>
		public void setInputVerifier(InputVerifier @inputVerifier)
		{
		}

		/// <summary>
		/// Sets the maximum size of this component to a constant
		/// value.
		/// </summary>
		public void setMaximumSize(Dimension @maximumSize)
		{
		}

		/// <summary>
		/// Sets the minimum size of this component to a constant
		/// value.
		/// </summary>
		public void setMinimumSize(Dimension @minimumSize)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of 1.4, replaced by <code>FocusTraversalPolicy</code></I>
		/// </summary>
		public void setNextFocusableComponent(Component @aComponent)
		{
		}

		/// <summary>
		/// If true the component paints every pixel within its bounds.
		/// </summary>
		public void setOpaque(bool @isOpaque)
		{
		}

		/// <summary>
		/// Sets the preferred size of this component.
		/// </summary>
		public void setPreferredSize(Dimension @preferredSize)
		{
		}

		/// <summary>
		/// Provides a hint as to whether or not this <code>JComponent</code>
		/// should get focus.
		/// </summary>
		public void setRequestFocusEnabled(bool @requestFocusEnabled)
		{
		}

		/// <summary>
		/// Registers the text to display in a tool tip.
		/// </summary>
		public void setToolTipText(string @text)
		{
		}

		/// <summary>
		/// Sets the <code>transferHandler</code> property,
		/// which is <code>null</code> if the component does
		/// not support data transfer operations.
		/// </summary>
		public void setTransferHandler(TransferHandler @newHandler)
		{
		}

		/// <summary>
		/// Sets the look and feel delegate for this component.
		/// </summary>
		protected void setUI(ComponentUI @newUI)
		{
		}

		/// <summary>
		/// Sets the value to indicate whether input verifier for the
		/// current focus owner will be called before this component requests
		/// focus.
		/// </summary>
		public void setVerifyInputWhenFocusTarget(bool @verifyInputWhenFocusTarget)
		{
		}

		/// <summary>
		/// Makes the component visible or invisible.
		/// </summary>
		public void setVisible(bool @aFlag)
		{
		}

		/// <summary>
		/// This method is now obsolete.
		/// </summary>
		public void unregisterKeyboardAction(KeyStroke @aKeyStroke)
		{
		}

		/// <summary>
		/// Calls <code>paint</code>.
		/// </summary>
		public void update(Graphics @g)
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
