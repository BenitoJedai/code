// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.geom;
using java.lang;

namespace java.awt.geom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/geom/Rectangle2D.html
	[Script(IsNative = true)]
	public abstract class Rectangle2D : RectangularShape
	{
		/// <summary>
		/// This is an abstract class that cannot be instantiated directly.
		/// </summary>
		public Rectangle2D()
		{
		}

		/// <summary>
		/// Adds a point, specified by the double precision arguments
		/// <code>newx</code> and <code>newy</code>, to this
		/// <code>Rectangle2D</code>.
		/// </summary>
		public void add(double @newx, double @newy)
		{
		}

		/// <summary>
		/// Adds the <code>Point2D</code> object <code>pt</code> to this
		/// <code>Rectangle2D</code>.
		/// </summary>
		public void add(Point2D @pt)
		{
		}

		/// <summary>
		/// Adds a <code>Rectangle2D</code> object to this
		/// <code>Rectangle2D</code>.
		/// </summary>
		public void add(Rectangle2D @r)
		{
		}

		/// <summary>
		/// Tests if a specified coordinate is inside the boundary of this
		/// <code>Rectangle2D</code>.
		/// </summary>
		public bool contains(double @x, double @y)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the interior of this <code>Rectangle2D</code> entirely
		/// contains the specified set of rectangular coordinates.
		/// </summary>
		public bool contains(double @x, double @y, double @w, double @h)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a new <code>Rectangle2D</code> object representing the
		/// intersection of this <code>Rectangle2D</code> with the specified
		/// <code>Rectangle2D</code>.
		/// </summary>
		public Rectangle2D createIntersection(Rectangle2D @r)
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Returns a new <code>Rectangle2D</code> object representing the
		/// union of this <code>Rectangle2D</code> with the specified
		/// <code>Rectangle2D</code>.
		/// </summary>
		public Rectangle2D createUnion(Rectangle2D @r)
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Determines whether or not the specified <code>Object</code> is
		/// equal to this <code>Rectangle2D</code>.
		/// </summary>
		public override bool Equals(object @obj)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the high precision bounding box of this
		/// <code>Rectangle2D</code>.
		/// </summary>
		public Rectangle2D getBounds2D()
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Returns an iteration object that defines the boundary of this
		/// <code>Rectangle2D</code>.
		/// </summary>
		public PathIterator getPathIterator(AffineTransform @at)
		{
			return default(PathIterator);
		}

		/// <summary>
		/// Returns an iteration object that defines the boundary of the
		/// flattened <code>Rectangle2D</code>.
		/// </summary>
		public PathIterator getPathIterator(AffineTransform @at, double @flatness)
		{
			return default(PathIterator);
		}

		/// <summary>
		/// Returns the hashcode for this <code>Rectangle2D</code>.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Intersects the pair of specified source <code>Rectangle2D</code>
		/// objects and puts the result into the specified destination
		/// <code>Rectangle2D</code> object.
		/// </summary>
		static public void intersect(Rectangle2D @src1, Rectangle2D @src2, Rectangle2D @dest)
		{
		}

		/// <summary>
		/// Tests if the interior of this <code>Rectangle2D</code>
		/// intersects the interior of a specified set of rectangular
		/// coordinates.
		/// </summary>
		public bool intersects(double @x, double @y, double @w, double @h)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the specified line segment intersects the interior of this
		/// <code>Rectangle2D</code>.
		/// </summary>
		public bool intersectsLine(double @x1, double @y1, double @x2, double @y2)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the specified line segment intersects the interior of this
		/// <code>Rectangle2D</code>.
		/// </summary>
		public bool intersectsLine(Line2D @l)
		{
			return default(bool);
		}

		/// <summary>
		/// Determines where the specified coordinates lie with respect
		/// to this <code>Rectangle2D</code>.
		/// </summary>
		abstract public int outcode(double @x, double @y);

		/// <summary>
		/// Determines where the specified <A HREF="../../../java/awt/geom/Point2D.html" title="class in java.awt.geom"><CODE>Point2D</CODE></A> lies with
		/// respect to this <code>Rectangle2D</code>.
		/// </summary>
		public int outcode(Point2D @p)
		{
			return default(int);
		}

		/// <summary>
		/// Sets the location and size of the outer bounds of this
		/// <code>Rectangle2D</code> to the specified rectangular values.
		/// </summary>
		public override void setFrame(double @x, double @y, double @w, double @h)
		{
		}

		/// <summary>
		/// Sets the location and size of this <code>Rectangle2D</code>
		/// to the specified double values.
		/// </summary>
		abstract public void setRect(double @x, double @y, double @w, double @h);

		/// <summary>
		/// Sets this <code>Rectangle2D</code> to be the same as the specified
		/// <code>Rectangle2D</code>.
		/// </summary>
		public void setRect(Rectangle2D @r)
		{
		}

		/// <summary>
		/// Unions the pair of source <code>Rectangle2D</code> objects
		/// and puts the result into the specified destination
		/// <code>Rectangle2D</code> object.
		/// </summary>
		static public void union(Rectangle2D @src1, Rectangle2D @src2, Rectangle2D @dest)
		{
		}

	}
}

