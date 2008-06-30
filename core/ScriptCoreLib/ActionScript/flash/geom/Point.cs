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
    }
}
