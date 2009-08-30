// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;
using javax.accessibility;
using javax.swing;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/SwingUtilities.html
	[Script(IsNative = true)]
	public class SwingUtilities
	{
		/// <summary>
		/// Stores the position and size of
		/// the inner painting area of the specified component
		/// in <code>r</code> and returns <code>r</code>.
		/// </summary>
		public Rectangle calculateInnerArea(JComponent @c, Rectangle @r)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Convenience returning an array of rect representing the regions within
		/// <code>rectA</code> that do not overlap with <code>rectB</code>.
		/// </summary>
		public Rectangle[] computeDifference(Rectangle @rectA, Rectangle @rectB)
		{
			return default(Rectangle[]);
		}

		/// <summary>
		/// Convenience to calculate the intersection of two rectangles
		/// without allocating a new rectangle.
		/// </summary>
		public Rectangle computeIntersection(int @x, int @y, int @width, int @height, Rectangle @dest)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Compute the width of the string using a font with the specified
		/// "metrics" (sizes).
		/// </summary>
		static public int computeStringWidth(FontMetrics @fm, string @str)
		{
			return default(int);
		}

		/// <summary>
		/// Convenience method that calculates the union of two rectangles
		/// without allocating a new rectangle.
		/// </summary>
		public Rectangle computeUnion(int @x, int @y, int @width, int @height, Rectangle @dest)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns a MouseEvent similar to <code>sourceEvent</code> except that its x
		/// and y members have been converted to <code>destination</code>'s coordinate
		/// system.
		/// </summary>
		public MouseEvent convertMouseEvent(Component @source, MouseEvent @sourceEvent, Component @destination)
		{
			return default(MouseEvent);
		}

		/// <summary>
		/// Convert the point <code>(x,y)</code> in <code>source</code> coordinate system to
		/// <code>destination</code> coordinate system.
		/// </summary>
		public Point convertPoint(Component @source, int @x, int @y, Component @destination)
		{
			return default(Point);
		}

		/// <summary>
		/// Convert a <code>aPoint</code> in <code>source</code> coordinate system to
		/// <code>destination</code> coordinate system.
		/// </summary>
		public Point convertPoint(Component @source, Point @aPoint, Component @destination)
		{
			return default(Point);
		}

		/// <summary>
		/// Convert a point from a screen coordinates to a component's
		/// coordinate system
		/// </summary>
		static public void convertPointFromScreen(Point @p, Component @c)
		{
		}

		/// <summary>
		/// Convert a point from a component's coordinate system to
		/// screen coordinates.
		/// </summary>
		static public void convertPointToScreen(Point @p, Component @c)
		{
		}

		/// <summary>
		/// Convert the rectangle <code>aRectangle</code> in <code>source</code> coordinate system to
		/// <code>destination</code> coordinate system.
		/// </summary>
		public Rectangle convertRectangle(Component @source, Rectangle @aRectangle, Component @destination)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of 1.4, replaced by
		/// <code>KeyboardFocusManager.getFocusOwner()</code>.</I>
		/// </summary>
		public Component findFocusOwner(Component @c)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the <code>Accessible</code> child contained at the
		/// local coordinate <code>Point</code>, if one exists.
		/// </summary>
		public Accessible getAccessibleAt(Component @c, Point @p)
		{
			return default(Accessible);
		}

		/// <summary>
		/// Return the nth Accessible child of the object.
		/// </summary>
		public Accessible getAccessibleChild(Component @c, int @i)
		{
			return default(Accessible);
		}

		/// <summary>
		/// Returns the number of accessible children in the object.
		/// </summary>
		static public int getAccessibleChildrenCount(Component @c)
		{
			return default(int);
		}

		/// <summary>
		/// Get the index of this object in its accessible parent.
		/// </summary>
		static public int getAccessibleIndexInParent(Component @c)
		{
			return default(int);
		}

		/// <summary>
		/// Get the state of this object.
		/// </summary>
		public AccessibleStateSet getAccessibleStateSet(Component @c)
		{
			return default(AccessibleStateSet);
		}

		/// <summary>
		/// Convenience method for searching above <code>comp</code> in the
		/// component hierarchy and returns the first object of <code>name</code> it
		/// finds.
		/// </summary>
		public Container getAncestorNamed(string @name, Component @comp)
		{
			return default(Container);
		}

		/// <summary>
		/// Convenience method for searching above <code>comp</code> in the
		/// component hierarchy and returns the first object of class <code>c</code> it
		/// finds.
		/// </summary>
		public Container getAncestorOfClass(Class @c, Component @comp)
		{
			return default(Container);
		}

		/// <summary>
		/// Returns the deepest visible descendent Component of <code>parent</code>
		/// that contains the location <code>x</code>, <code>y</code>.
		/// </summary>
		public Component getDeepestComponentAt(Component @parent, int @x, int @y)
		{
			return default(Component);
		}

		/// <summary>
		/// Return the rectangle (0,0,bounds.width,bounds.height) for the component <code>aComponent</code>
		/// </summary>
		public Rectangle getLocalBounds(Component @aComponent)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the root component for the current component tree.
		/// </summary>
		public Component getRoot(Component @c)
		{
			return default(Component);
		}

		/// <summary>
		/// If c is a JRootPane descendant return its JRootPane ancestor.
		/// </summary>
		public JRootPane getRootPane(Component @c)
		{
			return default(JRootPane);
		}

		/// <summary>
		/// Returns the ActionMap provided by the UI
		/// in component <code>component</code>.
		/// </summary>
		public ActionMap getUIActionMap(JComponent @component)
		{
			return default(ActionMap);
		}

		/// <summary>
		/// Returns the InputMap provided by the UI for condition
		/// <code>condition</code> in component <code>component</code>.
		/// </summary>
		public InputMap getUIInputMap(JComponent @component, int @condition)
		{
			return default(InputMap);
		}

		/// <summary>
		/// 
		/// </summary>
		public Window getWindowAncestor(Component @c)
		{
			return default(Window);
		}

		/// <summary>
		/// Causes <i>doRun.run()</i> to be executed synchronously on the
		/// AWT event dispatching thread.
		/// </summary>
		static public void invokeAndWait(Runnable @doRun)
		{
		}

		/// <summary>
		/// Causes <i>doRun.run()</i> to be executed asynchronously on the
		/// AWT event dispatching thread.
		/// </summary>
		static public void invokeLater(Runnable @doRun)
		{
		}

		/// <summary>
		/// Return <code>true</code> if a component <code>a</code> descends from a component <code>b</code>
		/// </summary>
		static public bool isDescendingFrom(Component @a, Component @b)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the current thread is an AWT event dispatching thread.
		/// </summary>
		static public bool isEventDispatchThread()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the mouse event specifies the left mouse button.
		/// </summary>
		static public bool isLeftMouseButton(MouseEvent @anEvent)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the mouse event specifies the middle mouse button.
		/// </summary>
		static public bool isMiddleMouseButton(MouseEvent @anEvent)
		{
			return default(bool);
		}

		/// <summary>
		/// Return true if <code>a</code> contains <code>b</code>
		/// </summary>
		static public bool isRectangleContainingRectangle(Rectangle @a, Rectangle @b)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the mouse event specifies the right mouse button.
		/// </summary>
		static public bool isRightMouseButton(MouseEvent @anEvent)
		{
			return default(bool);
		}

		/// <summary>
		/// Compute and return the location of the icons origin, the
		/// location of origin of the text baseline, and a possibly clipped
		/// version of the compound labels string.
		/// </summary>
		public string layoutCompoundLabel(FontMetrics @fm, string @text, Icon @icon, int @verticalAlignment, int @horizontalAlignment, int @verticalTextPosition, int @horizontalTextPosition, Rectangle @viewR, Rectangle @iconR, Rectangle @textR, int @textIconGap)
		{
			return default(string);
		}

		/// <summary>
		/// Compute and return the location of the icons origin, the
		/// location of origin of the text baseline, and a possibly clipped
		/// version of the compound labels string.
		/// </summary>
		public string layoutCompoundLabel(JComponent @c, FontMetrics @fm, string @text, Icon @icon, int @verticalAlignment, int @horizontalAlignment, int @verticalTextPosition, int @horizontalTextPosition, Rectangle @viewR, Rectangle @iconR, Rectangle @textR, int @textIconGap)
		{
			return default(string);
		}

		/// <summary>
		/// Invokes <code>actionPerformed</code> on <code>action</code> if
		/// <code>action</code> is enabled (and non null).
		/// </summary>
		static public bool notifyAction(Action @action, KeyStroke @ks, KeyEvent @event, object @sender, int @modifiers)
		{
			return default(bool);
		}

		/// <summary>
		/// Paints a component <code>c</code> on an arbitrary graphics
		/// <code>g</code> in the
		/// specified rectangle, specifying the rectangle's upper left corner
		/// and size.
		/// </summary>
		static public void paintComponent(Graphics @g, Component @c, Container @p, int @x, int @y, int @w, int @h)
		{
		}

		/// <summary>
		/// Paints a component <code>c</code> on an arbitrary graphics
		/// <code>g</code> in the specified rectangle, specifying a Rectangle object.
		/// </summary>
		static public void paintComponent(Graphics @g, Component @c, Container @p, Rectangle @r)
		{
		}

		/// <summary>
		/// Process the key bindings for the <code>Component</code> associated with
		/// <code>event</code>.
		/// </summary>
		static public bool processKeyBindings(KeyEvent @event)
		{
			return default(bool);
		}

		/// <summary>
		/// Convenience method to change the UI ActionMap for <code>component</code>
		/// to <code>uiActionMap</code>.
		/// </summary>
		static public void replaceUIActionMap(JComponent @component, ActionMap @uiActionMap)
		{
		}

		/// <summary>
		/// Convenience method to change the UI InputMap for <code>component</code>
		/// to <code>uiInputMap</code>.
		/// </summary>
		static public void replaceUIInputMap(JComponent @component, int @type, InputMap @uiInputMap)
		{
		}

		/// <summary>
		/// A simple minded look and feel change: ask each node in the tree
		/// to <code>updateUI()</code> -- that is, to initialize its UI property
		/// with the current look and feel.
		/// </summary>
		static public void updateComponentTreeUI(Component @c)
		{
		}

		/// <summary>
		/// Return <code>aComponent</code>'s window
		/// </summary>
		public Window windowForComponent(Component @aComponent)
		{
			return default(Window);
		}

	}
}

