using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.geom
{
    /// <summary>
    /// http://livedocs.adobe.com/flex/3/langref/flash/geom/Point.html
    /// </summary>
    [Script(IsNative=true)]
    public class Point
    {
        #region Methods
        /// <summary>
        /// Adds the coordinates of another point to the coordinates of this point to create a new point.
        /// </summary>
        public Point add(Point v)
        {
            return default(Point);
        }

        /// <summary>
        /// Creates a copy of this Point object.
        /// </summary>
        public Point clone()
        {
            return default(Point);
        }

        /// <summary>
        /// [static] Returns the distance between pt1 and pt2.
        /// </summary>
        public static double distance(Point pt1, Point pt2)
        {
            return default(double);
        }

        /// <summary>
        /// Determines whether two points are equal.
        /// </summary>
        public bool equals(Point toCompare)
        {
            return default(bool);
        }

        /// <summary>
        /// [static] Determines a point between two specified points.
        /// </summary>
        public static Point interpolate(Point pt1, Point pt2, double f)
        {
            return default(Point);
        }

        /// <summary>
        /// Scales the line segment between (0,0) and the current point to a set length.
        /// </summary>
        public void normalize(double thickness)
        {
        }

        /// <summary>
        /// Offsets the Point object by the specified amount.
        /// </summary>
        public void offset(double dx, double dy)
        {
        }

        /// <summary>
        /// [static] Converts a pair of polar coordinates to a Cartesian point coordinate.
        /// </summary>
        public static Point polar(double len, double angle)
        {
            return default(Point);
        }

        /// <summary>
        /// Subtracts the coordinates of another point from the coordinates of this point to create a new point.
        /// </summary>
        public Point subtract(Point v)
        {
            return default(Point);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new point.
        /// </summary>
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Creates a new point.
        /// </summary>
        public Point(double x)
        {
            this.x = x;
        }

        /// <summary>
        /// Creates a new point.
        /// </summary>
        public Point()
        {
        }

        #endregion


        /// <summary>
        /// The length of the line segment from (0,0) to this point.
        /// </summary>
        public double length { get; private set; }

        /// <summary>
        /// The horizontal coordinate of the point.
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// The vertical coordinate of the point.
        /// </summary>
        public double y { get; set; }


        [Script(NotImplementedHere = true)]
        public static Point operator -(Point a, Point b)
        {
            return default(Point);
        }

        [Script(NotImplementedHere = true)]
        public static Point operator +(Point a, Point b)
        {
            return default(Point);
        }

				[Script(NotImplementedHere = true)]
				public static Point operator *(Point a, Point b)
				{
					return default(Point);
				}

				[Script(NotImplementedHere = true)]
				public static Point operator *(Point a, double b)
				{
					return default(Point);
				}

				[Script(NotImplementedHere = true)]
				public static Point operator /(Point a, Point b)
				{
					return default(Point);
				}
    }
}
