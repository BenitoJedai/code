// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JViewport

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.accessibility;
using javax.swing.border;
using javax.swing.@event;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JViewport.html
	[Script(IsNative = true)]
	public class JViewport : JComponent
	{
		/// <summary>
		/// Creates a <code>JViewport</code>.
		/// </summary>
		public JViewport()
		{
		}

		/// <summary>
		/// Adds a <code>ChangeListener</code> to the list that is
		/// notified each time the view's
		/// size, position, or the viewport's extent size has changed.
		/// </summary>
		public void addChangeListener(ChangeListener @l)
		{
		}

		/// <summary>
		/// Sets the <code>JViewport</code>'s one lightweight child,
		/// which can be <code>null</code>.
		/// </summary>
		protected void addImpl(Component @child, object @constraints, int @index)
		{
		}

		/// <summary>
		/// Computes the parameters for a blit where the backing store image
		/// currently contains <code>oldLoc</code> in the upper left hand corner
		/// and we're scrolling to <code>newLoc</code>.
		/// </summary>
		protected bool computeBlit(int @dx, int @dy, Point @blitFrom, Point @blitTo, Dimension @blitSize, Rectangle @blitPaint)
		{
			return default(bool);
		}

		/// <summary>
		/// Subclassers can override this to install a different
		/// layout manager (or <code>null</code>) in the constructor.
		/// </summary>
		protected LayoutManager createLayoutManager()
		{
			return default(LayoutManager);
		}



		/// <summary>
		/// Notifies listeners of a property change.
		/// </summary>
		protected void firePropertyChange(string @propertyName, object @oldValue, object @newValue)
		{
		}

		/// <summary>
		/// Notifies all <code>ChangeListeners</code> when the views
		/// size, position, or the viewports extent size has changed.
		/// </summary>
		protected void fireStateChanged()
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JViewport.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns an array of all the <code>ChangeListener</code>s added
		/// to this JViewport with addChangeListener().
		/// </summary>
		public ChangeListener[] getChangeListeners()
		{
			return default(ChangeListener[]);
		}

		/// <summary>
		/// Returns the size of the visible part of the view in view coordinates.
		/// </summary>
		public Dimension getExtentSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the insets (border) dimensions as (0,0,0,0), since borders
		/// are not supported on a <code>JViewport</code>.
		/// </summary>
		public Insets getInsets()
		{
			return default(Insets);
		}

		/// <summary>
		/// Returns an <code>Insets</code> object containing this
		/// <code>JViewport</code>s inset values.
		/// </summary>
		public Insets getInsets(Insets @insets)
		{
			return default(Insets);
		}

		/// <summary>
		/// Returns the current scrolling mode.
		/// </summary>
		public int getScrollMode()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the L&F object that renders this component.
		/// </summary>
		public ViewportUI getUI()
		{
			return default(ViewportUI);
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
		/// Returns the <code>JViewport</code>'s one child or <code>null</code>.
		/// </summary>
		public Component getView()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the view coordinates that appear in the upper left
		/// hand corner of the viewport, or 0,0 if there's no view.
		/// </summary>
		public Point getViewPosition()
		{
			return default(Point);
		}

		/// <summary>
		/// Returns a rectangle whose origin is <code>getViewPosition</code>
		/// and size is <code>getExtentSize</code>.
		/// </summary>
		public Rectangle getViewRect()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// If the view's size hasn't been explicitly set, return the
		/// preferred size, otherwise return the view's current size.
		/// </summary>
		public Dimension getViewSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Java 2 platform v1.3, replaced by
		/// <code>getScrollMode()</code>.</I>
		/// </summary>
		public bool isBackingStoreEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// The <code>JViewport</code> overrides the default implementation of
		/// this method (in <code>JComponent</code>) to return false.
		/// </summary>
		public bool isOptimizedDrawingEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Depending on whether the <code>backingStore</code> is enabled,
		/// either paint the image through the backing store or paint
		/// just the recently exposed part, using the backing store
		/// to "blit" the remainder.
		/// </summary>
		public void paint(Graphics @g)
		{
		}

		/// <summary>
		/// Returns a string representation of this <code>JViewport</code>.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Removes the <code>Viewport</code>s one lightweight child.
		/// </summary>
		public void remove(Component @child)
		{
		}

		/// <summary>
		/// Removes a <code>ChangeListener</code> from the list that's notified each
		/// time the views size, position, or the viewports extent size
		/// has changed.
		/// </summary>
		public void removeChangeListener(ChangeListener @l)
		{
		}

		/// <summary>
		/// Always repaint in the parents coordinate system to make sure
		/// only one paint is performed by the <code>RepaintManager</code>.
		/// </summary>
		public void repaint(long @tm, int @x, int @y, int @w, int @h)
		{
		}

		/// <summary>
		/// Sets the bounds of this viewport.
		/// </summary>
		public void reshape(int @x, int @y, int @w, int @h)
		{
		}

		/// <summary>
		/// Scrolls the view so that <code>Rectangle</code>
		/// within the view becomes visible.
		/// </summary>
		public void scrollRectToVisible(Rectangle @contentRect)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Java 2 platform v1.3, replaced by
		/// <code>setScrollMode()</code>.</I>
		/// </summary>
		public void setBackingStoreEnabled(bool @enabled)
		{
		}

		/// <summary>
		/// The viewport "scrolls" its child (called the "view") by the
		/// normal parent/child clipping (typically the view is moved in
		/// the opposite direction of the scroll).
		/// </summary>
		public void setBorder(Border @border)
		{
		}

		/// <summary>
		/// Sets the size of the visible part of the view using view coordinates.
		/// </summary>
		public void setExtentSize(Dimension @newExtent)
		{
		}

		/// <summary>
		/// Used to control the method of scrolling the viewport contents.
		/// </summary>
		public void setScrollMode(int @mode)
		{
		}

		/// <summary>
		/// Sets the L&F object that renders this component.
		/// </summary>
		public void setUI(ViewportUI @ui)
		{
		}

		/// <summary>
		/// Sets the <code>JViewport</code>'s one lightweight child
		/// (<code>view</code>), which can be <code>null</code>.
		/// </summary>
		public void setView(Component @view)
		{
		}

		/// <summary>
		/// Sets the view coordinates that appear in the upper left
		/// hand corner of the viewport, does nothing if there's no view.
		/// </summary>
		public void setViewPosition(Point @p)
		{
		}

		/// <summary>
		/// Sets the size of the view.
		/// </summary>
		public void setViewSize(Dimension @newSize)
		{
		}

		/// <summary>
		/// Converts a size in pixel coordinates to view coordinates.
		/// </summary>
		public Dimension toViewCoordinates(Dimension @size)
		{
			return default(Dimension);
		}

		/// <summary>
		/// Converts a point in pixel coordinates to view coordinates.
		/// </summary>
		public Point toViewCoordinates(Point @p)
		{
			return default(Point);
		}

		/// <summary>
		/// Resets the UI property to a value from the current look and feel.
		/// </summary>
		public void updateUI()
		{
		}

	}
}
