using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.geom
{
    /// <summary>
    /// http://livedocs.adobe.com/flex/3/langref/flash/geom/Rectangle.html
    /// </summary>
    [Script(IsNative=true)]
    public class Rectangle
    {
        public Rectangle()
        {

        }
        	
        public Rectangle(double x, double y, double width, double height)
        {

        }
        /// <summary>
        /// The sum of the y and height properties.
        /// </summary>
        public double bottom { get; set; }
        /// <summary>
        /// The location of the Rectangle object's bottom-right corner, determined by the values of the right and bottom properties.
        /// </summary>
        public Point bottomRight { get; set; }
        /// <summary>
        /// The height of the rectangle, in pixels.
        /// </summary>
        public double height { get; set; }
        /// <summary>
        /// The x coordinate of the top-left corner of the rectangle.
        /// </summary>
        public double left { get; set; }
        /// <summary>
        /// The sum of the x and width properties.
        /// </summary>
        public double right { get; set; }
        /// <summary>
        /// The size of the Rectangle object, expressed as a Point object with the values of the width and height properties.
        /// </summary>
        public Point size { get; set; }
        /// <summary>
        /// The y coordinate of the top-left corner of the rectangle.
        /// </summary>
        public double top { get; set; }
        /// <summary>
        /// The location of the Rectangle object's top-left corner, determined by the x and y coordinates of the point.
        /// </summary>
        public Point topLeft { get; set; }
        /// <summary>
        /// The width of the rectangle, in pixels.
        /// </summary>
        public double width { get; set; }
        /// <summary>
        /// The x coordinate of the top-left corner of the rectangle.
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// The y coordinate of the top-left corner of the rectangle.
        /// </summary>
        public double y { get; set; }




    }
}
