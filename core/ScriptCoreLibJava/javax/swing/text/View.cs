// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.text.View

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.swing.@event;

namespace javax.swing.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/text/View.html
	[Script(IsNative = true)]
	public abstract class View
	{
		/// <summary>
		/// Creates a new <code>View</code> object.
		/// </summary>
		public View(Element @elem)
		{
		}

		/// <summary>
		/// Appends a single child view.
		/// </summary>
		public void append(View @v)
		{
		}

		/// <summary>
		/// Tries to break this view on the given axis.
		/// </summary>
		public View breakView(int @axis, int @offset, float @pos, float @len)
		{
			return default(View);
		}

		/// <summary>
		/// Gives notification from the document that attributes were changed
		/// in a location that this view is responsible for.
		/// </summary>
		public void changedUpdate(DocumentEvent @e, Shape @a, ViewFactory @f)
		{
		}

		/// <summary>
		/// Creates a view that represents a portion of the element.
		/// </summary>
		public View createFragment(int @p0, int @p1)
		{
			return default(View);
		}



		/// <summary>
		/// Forwards the <code>DocumentEvent</code> to the give child view.
		/// </summary>
		protected void forwardUpdateToView(View @v, DocumentEvent @e, Shape @a, ViewFactory @f)
		{
		}

		/// <summary>
		/// Determines the desired alignment for this view along an
		/// axis.
		/// </summary>
		public float getAlignment(int @axis)
		{
			return default(float);
		}

		/// <summary>
		/// Fetches the attributes to use when rendering.
		/// </summary>
		public AttributeSet getAttributes()
		{
			return default(AttributeSet);
		}

		/// <summary>
		/// Determines how attractive a break opportunity in
		/// this view is.
		/// </summary>
		public int getBreakWeight(int @axis, float @pos, float @len)
		{
			return default(int);
		}

		/// <summary>
		/// Fetches the allocation for the given child view.
		/// </summary>
		public Shape getChildAllocation(int @index, Shape @a)
		{
			return default(Shape);
		}

		/// <summary>
		/// Fetches the container hosting the view.
		/// </summary>
		public Container getContainer()
		{
			return default(Container);
		}

		/// <summary>
		/// Fetches the model associated with the view.
		/// </summary>
		public Document getDocument()
		{
			return default(Document);
		}

		/// <summary>
		/// Fetches the structural portion of the subject that this
		/// view is mapped to.
		/// </summary>
		public Element getElement()
		{
			return default(Element);
		}

		/// <summary>
		/// Fetches the portion of the model for which this view is
		/// responsible.
		/// </summary>
		public int getEndOffset()
		{
			return default(int);
		}

		/// <summary>
		/// Fetch a <code>Graphics</code> for rendering.
		/// </summary>
		public Graphics getGraphics()
		{
			return default(Graphics);
		}

		/// <summary>
		/// Determines the maximum span for this view along an
		/// axis.
		/// </summary>
		public float getMaximumSpan(int @axis)
		{
			return default(float);
		}

		/// <summary>
		/// Determines the minimum span for this view along an
		/// axis.
		/// </summary>
		public float getMinimumSpan(int @axis)
		{
			return default(float);
		}

	

		/// <summary>
		/// Returns the parent of the view.
		/// </summary>
		public View getParent()
		{
			return default(View);
		}

		/// <summary>
		/// Determines the preferred span for this view along an
		/// axis.
		/// </summary>
		abstract public float getPreferredSpan(int @axis);

		/// <summary>
		/// Determines the resizability of the view along the
		/// given axis.
		/// </summary>
		public int getResizeWeight(int @axis)
		{
			return default(int);
		}

		/// <summary>
		/// Fetches the portion of the model for which this view is
		/// responsible.
		/// </summary>
		public int getStartOffset()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the tooltip text at the specified location.
		/// </summary>
		public string getToolTipText(float @x, float @y, Shape @allocation)
		{
			return default(string);
		}

		/// <summary>
		/// Gets the <i>n</i>th child view.
		/// </summary>
		public View getView(int @n)
		{
			return default(View);
		}

		/// <summary>
		/// Returns the number of views in this view.
		/// </summary>
		public int getViewCount()
		{
			return default(int);
		}

		/// <summary>
		/// Fetches the <code>ViewFactory</code> implementation that is feeding
		/// the view hierarchy.
		/// </summary>
		public ViewFactory getViewFactory()
		{
			return default(ViewFactory);
		}

		/// <summary>
		/// Returns the child view index representing the given position in
		/// the view.
		/// </summary>
		public int getViewIndex(float @x, float @y, Shape @allocation)
		{
			return default(int);
		}

	

		/// <summary>
		/// Inserts a single child view.
		/// </summary>
		public void insert(int @offs, View @v)
		{
		}

		/// <summary>
		/// Gives notification that something was inserted into
		/// the document in a location that this view is responsible for.
		/// </summary>
		public void insertUpdate(DocumentEvent @e, Shape @a, ViewFactory @f)
		{
		}

		/// <summary>
		/// Returns a boolean that indicates whether
		/// the view is visible or not.
		/// </summary>
		public bool isVisible()
		{
			return default(bool);
		}


		/// <summary>
		/// <B>Deprecated.</B> <I></I>
		/// </summary>
		public Shape modelToView(int @pos, Shape @a)
		{
			return default(Shape);
		}

		/// <summary>
		/// Renders using the given rendering surface and area on that
		/// surface.
		/// </summary>
		abstract public void paint(Graphics @g, Shape @allocation);

		/// <summary>
		/// Child views can call this on the parent to indicate that
		/// the preference has changed and should be reconsidered
		/// for layout.
		/// </summary>
		public void preferenceChanged(View @child, bool @width, bool @height)
		{
		}

		/// <summary>
		/// Removes one of the children at the given position.
		/// </summary>
		public void remove(int @i)
		{
		}

		/// <summary>
		/// Removes all of the children.
		/// </summary>
		public void removeAll()
		{
		}

		/// <summary>
		/// Gives notification that something was removed from the document
		/// in a location that this view is responsible for.
		/// </summary>
		public void removeUpdate(DocumentEvent @e, Shape @a, ViewFactory @f)
		{
		}

		/// <summary>
		/// Replaces child views.
		/// </summary>
		public void replace(int @offset, int @length, View[] @views)
		{
		}

		/// <summary>
		/// Establishes the parent view for this view.
		/// </summary>
		public void setParent(View @parent)
		{
		}

		/// <summary>
		/// Sets the size of the view.
		/// </summary>
		public void setSize(float @width, float @height)
		{
		}



		/// <summary>
		/// <B>Deprecated.</B> <I></I>
		/// </summary>
		public int viewToModel(float @x, float @y, Shape @a)
		{
			return default(int);
		}

	
	}
}
