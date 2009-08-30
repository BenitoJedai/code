// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.geom;
using java.lang;

namespace java.awt.geom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/geom/Point2D.html
	[Script(IsNative = true)]
	public abstract class Point2D
	{
		/// <summary>
		/// This is an abstract class that cannot be instantiated directly.
		/// </summary>
		public Point2D()
		{
		}

		/// <summary>
		/// Creates a new object of the same class and with the
		/// same contents as this object.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the distance from this <code>Point2D</code> to
		/// a specified point.
		/// </summary>
		public double distance(double @PX, double @PY)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the distance between two points.
		/// </summary>
		static public double distance(double @X1, double @Y1, double @X2, double @Y2)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the distance from this <code>Point2D</code> to a
		/// specified <code>Point2D</code>.
		/// </summary>
		public double distance(Point2D @pt)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the square of the distance from this
		/// <code>Point2D</code> to a specified point.
		/// </summary>
		public double distanceSq(double @PX, double @PY)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the square of the distance between two points.
		/// </summary>
		static public double distanceSq(double @X1, double @Y1, double @X2, double @Y2)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the square of the distance from this
		/// <code>Point2D</code> to a specified <code>Point2D</code>.
		/// </summary>
		public double distanceSq(Point2D @pt)
		{
			return default(double);
		}

		/// <summary>
		/// Determines whether or not two points are equal.
		/// </summary>
		public bool equals(object @obj)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the X coordinate of this <code>Point2D</code> in
		/// <code>double</code> precision.
		/// </summary>
		abstract public double getX();

		/// <summary>
		/// Returns the Y coordinate of this <code>Point2D</code> in
		/// <code>double</code> precision.
		/// </summary>
		abstract public double getY();

		/// <summary>
		/// Returns the hashcode for this <code>Point2D</code>.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Sets the location of this <code>Point2D</code> to the
		/// specified <code>double</code> coordinates.
		/// </summary>
		abstract public void setLocation(double @x, double @y);

		/// <summary>
		/// Sets the location of this <code>Point2D</code> to the same
		/// coordinates as the specified <code>Point2D</code> object.
		/// </summary>
		public void setLocation(Point2D @p)
		{
		}

	}
}

