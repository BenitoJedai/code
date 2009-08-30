// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.geom;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Polygon.html
	[Script(IsNative = true)]
	public class Polygon
	{
		/// <summary>
		/// Creates an empty polygon.
		/// </summary>
		public Polygon()
		{
		}

		/// <summary>
		/// Constructs and initializes a <code>Polygon</code> from the specified
		/// parameters.
		/// </summary>
		public Polygon(int[] @xpoints, int[] @ypoints, int @npoints)
		{
		}

		/// <summary>
		/// Appends the specified coordinates to this <code>Polygon</code>.
		/// </summary>
		public void addPoint(int @x, int @y)
		{
		}

		/// <summary>
		/// Determines if the specified coordinates are inside this
		/// <code>Polygon</code>.
		/// </summary>
		public bool contains(double @x, double @y)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the interior of this <code>Polygon</code> entirely
		/// contains the specified set of rectangular coordinates.
		/// </summary>
		public bool contains(double @x, double @y, double @w, double @h)
		{
			return default(bool);
		}

		/// <summary>
		/// Determines whether the specified coordinates are inside this
		/// <code>Polygon</code>.
		/// </summary>
		public bool contains(int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// Determines whether the specified <A HREF="../../java/awt/Point.html" title="class in java.awt"><CODE>Point</CODE></A> is inside this
		/// <code>Polygon</code>.
		/// </summary>
		public bool contains(Point @p)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if a specified <A HREF="../../java/awt/geom/Point2D.html" title="class in java.awt.geom"><CODE>Point2D</CODE></A> is inside the boundary of this
		/// <code>Polygon</code>.
		/// </summary>
		public bool contains(Point2D @p)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the interior of this <code>Polygon</code> entirely
		/// contains the specified <code>Rectangle2D</code>.
		/// </summary>
		public bool contains(Rectangle2D @r)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>getBounds()</code>.</I>
		/// </summary>
		public Rectangle getBoundingBox()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Gets the bounding box of this <code>Polygon</code>.
		/// </summary>
		public Rectangle getBounds()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the high precision bounding box of the <A HREF="../../java/awt/Shape.html" title="interface in java.awt"><CODE>Shape</CODE></A>.
		/// </summary>
		public Rectangle2D getBounds2D()
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Returns an iterator object that iterates along the boundary of this
		/// <code>Polygon</code> and provides access to the geometry
		/// of the outline of this <code>Polygon</code>.
		/// </summary>
		public PathIterator getPathIterator(AffineTransform @at)
		{
			return default(PathIterator);
		}

		/// <summary>
		/// Returns an iterator object that iterates along the boundary of
		/// the <code>Shape</code> and provides access to the geometry of the
		/// outline of the <code>Shape</code>.
		/// </summary>
		public PathIterator getPathIterator(AffineTransform @at, double @flatness)
		{
			return default(PathIterator);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>contains(int, int)</code>.</I>
		/// </summary>
		public bool inside(int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the interior of this <code>Polygon</code> intersects the
		/// interior of a specified set of rectangular coordinates.
		/// </summary>
		public bool intersects(double @x, double @y, double @w, double @h)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the interior of this <code>Polygon</code> intersects the
		/// interior of a specified <code>Rectangle2D</code>.
		/// </summary>
		public bool intersects(Rectangle2D @r)
		{
			return default(bool);
		}

		/// <summary>
		/// Invalidates or flushes any internally-cached data that depends
		/// on the vertex coordinates of this <code>Polygon</code>.
		/// </summary>
		public void invalidate()
		{
		}

		/// <summary>
		/// Resets this <code>Polygon</code> object to an empty polygon.
		/// </summary>
		public void reset()
		{
		}

		/// <summary>
		/// Translates the vertices of the <code>Polygon</code> by
		/// <code>deltaX</code> along the x axis and by
		/// <code>deltaY</code> along the y axis.
		/// </summary>
		public void translate(int @deltaX, int @deltaY)
		{
		}

	}
}

