// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;
using java.awt.geom;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Point.html
	[Script(IsNative = true)]
	public class Point : Point2D
	{
		/// <summary>
		/// Constructs and initializes a point at the origin
		/// (0, 0) of the coordinate space.
		/// </summary>
		public Point()
		{
		}

		/// <summary>
		/// Constructs and initializes a point at the specified
		/// (<i>x</i>, <i>y</i>) location in the coordinate space.
		/// </summary>
		public Point(int @x, int @y)
		{
		}

		/// <summary>
		/// Constructs and initializes a point with the same location as
		/// the specified <code>Point</code> object.
		/// </summary>
		public Point(Point @p)
		{
		}

		/// <summary>
		/// Determines whether or not two points are equal.
		/// </summary>
		public bool equals(object @obj)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the location of this point.
		/// </summary>
		public Point getLocation()
		{
			return default(Point);
		}

		/// <summary>
		/// Returns the X coordinate of the point in double precision.
		/// </summary>
		public override double getX()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the Y coordinate of the point in double precision.
		/// </summary>
		public override double getY()
		{
			return default(double);
		}

		/// <summary>
		/// Moves this point to the specified location in the
		/// (<i>x</i>, <i>y</i>) coordinate plane.
		/// </summary>
		public void move(int @x, int @y)
		{
		}

		/// <summary>
		/// Sets the location of this point to the specified double coordinates.
		/// </summary>
		public override void setLocation(double @x, double @y)
		{
		}

		/// <summary>
		/// Changes the point to have the specified location.
		/// </summary>
		public void setLocation(int @x, int @y)
		{
		}

		/// <summary>
		/// Sets the location of the point to the specified location.
		/// </summary>
		public void setLocation(Point @p)
		{
		}

		/// <summary>
		/// Returns a string representation of this point and its location
		/// in the (<i>x</i>, <i>y</i>) coordinate space.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

		/// <summary>
		/// Translates this point, at location (<i>x</i>, <i>y</i>),
		/// by <code>dx</code> along the <i>x</i> axis and <code>dy</code>
		/// along the <i>y</i> axis so that it now represents the point
		/// (<code>x</code> <code>+</code> <code>dx</code>,
		/// <code>y</code> <code>+</code> <code>dy</code>).
		/// </summary>
		public void translate(int @dx, int @dy)
		{
		}

	}
}

