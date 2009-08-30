// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.geom;
using java.lang;

namespace java.awt.geom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/geom/Line2D.html
	[Script(IsNative = true)]
	public abstract class Line2D
	{
		/// <summary>
		/// This is an abstract class that cannot be instantiated directly.
		/// </summary>
		public Line2D()
		{
		}

		/// <summary>
		/// Creates a new object of the same class as this object.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Tests if a specified coordinate is inside the boundary of this
		/// <code>Line2D</code>.
		/// </summary>
		public bool contains(double @x, double @y)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the interior of this <code>Line2D</code> entirely contains
		/// the specified set of rectangular coordinates.
		/// </summary>
		public bool contains(double @x, double @y, double @w, double @h)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if a given <code>Point2D</code> is inside the boundary of
		/// this <code>Line2D</code>.
		/// </summary>
		public bool contains(Point2D @p)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the interior of this <code>Line2D</code> entirely contains
		/// the specified <code>Rectangle2D</code>.
		/// </summary>
		public bool contains(Rectangle2D @r)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the bounding box of this <code>Line2D</code>.
		/// </summary>
		public Rectangle getBounds()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the starting <code>Point2D</code> of this
		/// <code>Line2D</code>.
		/// </summary>
		public Point2D getP1()
		{
			return default(Point2D);
		}

		/// <summary>
		/// Returns the end <code>Point2D</code> of this <code>Line2D</code>.
		/// </summary>
		public Point2D getP2()
		{
			return default(Point2D);
		}

		/// <summary>
		/// Returns an iteration object that defines the boundary of this
		/// <code>Line2D</code>.
		/// </summary>
		public PathIterator getPathIterator(AffineTransform @at)
		{
			return default(PathIterator);
		}

		/// <summary>
		/// Returns an iteration object that defines the boundary of this
		/// flattened <code>Line2D</code>.
		/// </summary>
		public PathIterator getPathIterator(AffineTransform @at, double @flatness)
		{
			return default(PathIterator);
		}

		/// <summary>
		/// Returns the X coordinate of the start point in double precision.
		/// </summary>
		abstract public double getX1();

		/// <summary>
		/// Returns the X coordinate of the end point in double precision.
		/// </summary>
		abstract public double getX2();

		/// <summary>
		/// Returns the Y coordinate of the start point in double precision.
		/// </summary>
		abstract public double getY1();

		/// <summary>
		/// Returns the Y coordinate of the end point in double precision.
		/// </summary>
		abstract public double getY2();

		/// <summary>
		/// Tests if this <code>Line2D</code> intersects the interior of a
		/// specified set of rectangular coordinates.
		/// </summary>
		public bool intersects(double @x, double @y, double @w, double @h)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if this <code>Line2D</code> intersects the interior of a
		/// specified <code>Rectangle2D</code>.
		/// </summary>
		public bool intersects(Rectangle2D @r)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the line segment from (X1, Y1) to
		/// (X2, Y2) intersects this line segment.
		/// </summary>
		public bool intersectsLine(double @X1, double @Y1, double @X2, double @Y2)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the specified line segment intersects this line segment.
		/// </summary>
		public bool intersectsLine(Line2D @l)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the line segment from (X1, Y1) to
		/// (X2, Y2) intersects the line segment from (X3, Y3)
		/// to (X4, Y4).
		/// </summary>
		static public bool linesIntersect(double @X1, double @Y1, double @X2, double @Y2, double @X3, double @Y3, double @X4, double @Y4)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the distance from a point to this line.
		/// </summary>
		public double ptLineDist(double @PX, double @PY)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the distance from a point to a line.
		/// </summary>
		static public double ptLineDist(double @X1, double @Y1, double @X2, double @Y2, double @PX, double @PY)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the distance from a <code>Point2D</code> to this line.
		/// </summary>
		public double ptLineDist(Point2D @pt)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the square of the distance from a point to this line.
		/// </summary>
		public double ptLineDistSq(double @PX, double @PY)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the square of the distance from a point to a line.
		/// </summary>
		static public double ptLineDistSq(double @X1, double @Y1, double @X2, double @Y2, double @PX, double @PY)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the square of the distance from a specified
		/// <code>Point2D</code> to this line.
		/// </summary>
		public double ptLineDistSq(Point2D @pt)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the distance from a point to this line segment.
		/// </summary>
		public double ptSegDist(double @PX, double @PY)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the distance from a point to a line segment.
		/// </summary>
		static public double ptSegDist(double @X1, double @Y1, double @X2, double @Y2, double @PX, double @PY)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the distance from a <code>Point2D</code> to this line
		/// segment.
		/// </summary>
		public double ptSegDist(Point2D @pt)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the square of the distance from a point to this line segment.
		/// </summary>
		public double ptSegDistSq(double @PX, double @PY)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the square of the distance from a point to a line segment.
		/// </summary>
		static public double ptSegDistSq(double @X1, double @Y1, double @X2, double @Y2, double @PX, double @PY)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the square of the distance from a <code>Point2D</code> to
		/// this line segment.
		/// </summary>
		public double ptSegDistSq(Point2D @pt)
		{
			return default(double);
		}

		/// <summary>
		/// Returns an indicator of where the specified point
		/// (PX, PY) lies with respect to this line segment.
		/// </summary>
		public int relativeCCW(double @PX, double @PY)
		{
			return default(int);
		}

		/// <summary>
		/// Returns an indicator of where the specified point
		/// (PX, PY) lies with respect to the line segment from
		/// (X1, Y1) to (X2, Y2).
		/// </summary>
		static public int relativeCCW(double @X1, double @Y1, double @X2, double @Y2, double @PX, double @PY)
		{
			return default(int);
		}

		/// <summary>
		/// Returns an indicator of where the specified <code>Point2D</code>
		/// lies with respect to this line segment.
		/// </summary>
		public int relativeCCW(Point2D @p)
		{
			return default(int);
		}

		/// <summary>
		/// Sets the location of the endpoints of this <code>Line2D</code> to
		/// the specified double coordinates.
		/// </summary>
		abstract public void setLine(double @X1, double @Y1, double @X2, double @Y2);

		/// <summary>
		/// Sets the location of the endpoints of this <code>Line2D</code> to
		/// the same as those endpoints of the specified <code>Line2D</code>.
		/// </summary>
		public void setLine(Line2D @l)
		{
		}

		/// <summary>
		/// Sets the location of the endpoints of this <code>Line2D</code> to
		/// the specified <code>Point2D</code> coordinates.
		/// </summary>
		public void setLine(Point2D @p1, Point2D @p2)
		{
		}

	}
}

