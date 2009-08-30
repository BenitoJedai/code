// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.geom;
using java.lang;

namespace java.awt.geom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/geom/RectangularShape.html
	[Script(IsNative = true)]
	public abstract class RectangularShape
	{
		/// <summary>
		/// This is an abstract class that cannot be instantiated directly.
		/// </summary>
		public RectangularShape()
		{
		}

		/// <summary>
		/// Creates a new object of the same class and with the same
		/// contents as this object.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Tests if a specified <code>Point2D</code> is inside the boundary
		/// of the <code>Shape</code>.
		/// </summary>
		public bool contains(Point2D @p)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if the interior of the <code>Shape</code> entirely contains the
		/// specified <code>Rectangle2D</code>.
		/// </summary>
		public bool contains(Rectangle2D @r)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the bounding box of the <code>Shape</code>.
		/// </summary>
		public Rectangle getBounds()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the X coordinate of the center of the framing
		/// rectangle of the <code>Shape</code> in <code>double</code>
		/// precision.
		/// </summary>
		public double getCenterX()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the Y coordinate of the center of the framing
		/// rectangle of the <code>Shape</code> in <code>double</code>
		/// precision.
		/// </summary>
		public double getCenterY()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the framing <A HREF="../../../java/awt/geom/Rectangle2D.html" title="class in java.awt.geom"><CODE>Rectangle2D</CODE></A>
		/// that defines the overall shape of this object.
		/// </summary>
		public Rectangle2D getFrame()
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Returns the height of the framing rectangle
		/// in <code>double</code> precision.
		/// </summary>
		abstract public double getHeight();

		/// <summary>
		/// Returns the largest X coordinate of the framing
		/// rectangle of the <code>Shape</code> in <code>double</code>
		/// precision.
		/// </summary>
		public double getMaxX()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the largest Y coordinate of the framing
		/// rectangle of the <code>Shape</code> in <code>double</code>
		/// precision.
		/// </summary>
		public double getMaxY()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the smallest X coordinate of the framing
		/// rectangle of the <code>Shape</code> in <code>double</code>
		/// precision.
		/// </summary>
		public double getMinX()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the smallest Y coordinate of the framing
		/// rectangle of the <code>Shape</code> in <code>double</code>
		/// precision.
		/// </summary>
		public double getMinY()
		{
			return default(double);
		}

		/// <summary>
		/// Returns an iterator object that iterates along the
		/// <code>Shape</code> object's boundary and provides access to a
		/// flattened view of the outline of the <code>Shape</code>
		/// object's geometry.
		/// </summary>
		public PathIterator getPathIterator(AffineTransform @at, double @flatness)
		{
			return default(PathIterator);
		}

		/// <summary>
		/// Returns the width of the framing rectangle in
		/// <code>double</code> precision.
		/// </summary>
		abstract public double getWidth();

		/// <summary>
		/// Returns the X coordinate of the upper left corner of
		/// the framing rectangle in <code>double</code> precision.
		/// </summary>
		abstract public double getX();

		/// <summary>
		/// Returns the Y coordinate of the upper left corner of
		/// the framing rectangle in <code>double</code> precision.
		/// </summary>
		abstract public double getY();

		/// <summary>
		/// Tests if the interior of the<code>Shape</code> intersects the
		/// interior of a specified <code>Rectangle2D</code>.
		/// </summary>
		public bool intersects(Rectangle2D @r)
		{
			return default(bool);
		}

		/// <summary>
		/// Determines whether the <code>RectangularShape</code> is empty.
		/// </summary>
		abstract public bool isEmpty();

		/// <summary>
		/// Sets the location and size of the framing rectangle of this
		/// <code>Shape</code> to the specified rectangular values.
		/// </summary>
		abstract public void setFrame(double @x, double @y, double @w, double @h);

		/// <summary>
		/// Sets the location and size of the framing rectangle of this
		/// <code>Shape</code> to the specified <A HREF="../../../java/awt/geom/Point2D.html" title="class in java.awt.geom"><CODE>Point2D</CODE></A> and
		/// <A HREF="../../../java/awt/geom/Dimension2D.html" title="class in java.awt.geom"><CODE>Dimension2D</CODE></A>, respectively.
		/// </summary>
		public void setFrame(Point2D @loc, Dimension2D @size)
		{
		}

		/// <summary>
		/// Sets the framing rectangle of this <code>Shape</code> to
		/// be the specified <code>Rectangle2D</code>.
		/// </summary>
		public void setFrame(Rectangle2D @r)
		{
		}

		/// <summary>
		/// Sets the framing rectangle of this <code>Shape</code>
		/// based on the specified center point coordinates and corner point
		/// coordinates.
		/// </summary>
		public void setFrameFromCenter(double @centerX, double @centerY, double @cornerX, double @cornerY)
		{
		}

		/// <summary>
		/// Sets the framing rectangle of this <code>Shape</code> based on a
		/// specified center <code>Point2D</code> and corner
		/// <code>Point2D</code>.
		/// </summary>
		public void setFrameFromCenter(Point2D @center, Point2D @corner)
		{
		}

		/// <summary>
		/// Sets the diagonal of the framing rectangle of this <code>Shape</code>
		/// based on the two specified coordinates.
		/// </summary>
		public void setFrameFromDiagonal(double @x1, double @y1, double @x2, double @y2)
		{
		}

		/// <summary>
		/// Sets the diagonal of the framing rectangle of this <code>Shape</code>
		/// based on two specified <code>Point2D</code> objects.
		/// </summary>
		public void setFrameFromDiagonal(Point2D @p1, Point2D @p2)
		{
		}

	}
}

