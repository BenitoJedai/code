// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.geom;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Shape.html
	[Script(IsNative = true)]
	public interface Shape
	{
		/// <summary>
		/// Tests if the specified coordinates are inside the boundary of the
		/// <code>Shape</code>.
		/// </summary>
		bool contains(double @x, double @y);

		/// <summary>
		/// Tests if the interior of the <code>Shape</code> entirely contains
		/// the specified rectangular area.
		/// </summary>
		bool contains(double @x, double @y, double @w, double @h);

		/// <summary>
		/// Tests if a specified <A HREF="../../java/awt/geom/Point2D.html" title="class in java.awt.geom"><CODE>Point2D</CODE></A> is inside the boundary
		/// of the <code>Shape</code>.
		/// </summary>
		bool contains(Point2D @p);

		/// <summary>
		/// Tests if the interior of the <code>Shape</code> entirely contains the
		/// specified <code>Rectangle2D</code>.
		/// </summary>
		bool contains(Rectangle2D @r);

		/// <summary>
		/// Returns an integer <A HREF="../../java/awt/Rectangle.html" title="class in java.awt"><CODE>Rectangle</CODE></A> that completely encloses the
		/// <code>Shape</code>.
		/// </summary>
		Rectangle getBounds();

		/// <summary>
		/// Returns a high precision and more accurate bounding box of
		/// the <code>Shape</code> than the <code>getBounds</code> method.
		/// </summary>
		Rectangle2D getBounds2D();

		/// <summary>
		/// Returns an iterator object that iterates along the
		/// <code>Shape</code> boundary and provides access to the geometry of the
		/// <code>Shape</code> outline.
		/// </summary>
		PathIterator getPathIterator(AffineTransform @at);

		/// <summary>
		/// Returns an iterator object that iterates along the <code>Shape</code>
		/// boundary and provides access to a flattened view of the
		/// <code>Shape</code> outline geometry.
		/// </summary>
		PathIterator getPathIterator(AffineTransform @at, double @flatness);

		/// <summary>
		/// Tests if the interior of the <code>Shape</code> intersects the
		/// interior of a specified rectangular area.
		/// </summary>
		bool intersects(double @x, double @y, double @w, double @h);

		/// <summary>
		/// Tests if the interior of the <code>Shape</code> intersects the
		/// interior of a specified <code>Rectangle2D</code>.
		/// </summary>
		bool intersects(Rectangle2D @r);

	}
}

