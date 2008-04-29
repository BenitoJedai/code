using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.geom
{
    // http://livedocs.adobe.com/flex/3/langref/flash/geom/Matrix.html
    [Script(IsNative=true)]
    public class Matrix
    {
        #region Properties
        /// <summary>
        /// The value that affects the positioning of pixels along the x axis when scaling or rotating an image.
        /// </summary>
        public double a { get; set; }

        /// <summary>
        /// The value that affects the positioning of pixels along the y axis when rotating or skewing an image.
        /// </summary>
        public double b { get; set; }

        /// <summary>
        /// The value that affects the positioning of pixels along the x axis when rotating or skewing an image.
        /// </summary>
        public double c { get; set; }

        /// <summary>
        /// The value that affects the positioning of pixels along the y axis when scaling or rotating an image.
        /// </summary>
        public double d { get; set; }

        /// <summary>
        /// The distance by which to translate each point along the x axis.
        /// </summary>
        public double tx { get; set; }

        /// <summary>
        /// The distance by which to translate each point along the y axis.
        /// </summary>
        public double ty { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Returns a new Matrix object that is a clone of this matrix, with an exact copy of the contained object.
        /// </summary>
        public Matrix clone()
        {
            return default(Matrix);
        }

        /// <summary>
        /// Concatenates a matrix with the current matrix, effectively combining the geometric effects of the two.
        /// </summary>
        public void concat(Matrix m)
        {
        }

        /// <summary>
        /// Includes parameters for scaling, rotation, and translation.
        /// </summary>
        public void createBox(double scaleX, double scaleY, double rotation, double tx, double ty)
        {
        }

        /// <summary>
        /// Includes parameters for scaling, rotation, and translation.
        /// </summary>
        public void createBox(double scaleX, double scaleY, double rotation, double tx)
        {
        }

        /// <summary>
        /// Includes parameters for scaling, rotation, and translation.
        /// </summary>
        public void createBox(double scaleX, double scaleY, double rotation)
        {
        }

        /// <summary>
        /// Includes parameters for scaling, rotation, and translation.
        /// </summary>
        public void createBox(double scaleX, double scaleY)
        {
        }

        /// <summary>
        /// Creates the specific style of matrix expected by the beginGradientFill() and lineGradientStyle() methods of the Graphics class.
        /// </summary>
        public void createGradientBox(double width, double height, double rotation, double tx, double ty)
        {
        }

        /// <summary>
        /// Creates the specific style of matrix expected by the beginGradientFill() and lineGradientStyle() methods of the Graphics class.
        /// </summary>
        public void createGradientBox(double width, double height, double rotation, double tx)
        {
        }

        /// <summary>
        /// Creates the specific style of matrix expected by the beginGradientFill() and lineGradientStyle() methods of the Graphics class.
        /// </summary>
        public void createGradientBox(double width, double height, double rotation)
        {
        }

        /// <summary>
        /// Creates the specific style of matrix expected by the beginGradientFill() and lineGradientStyle() methods of the Graphics class.
        /// </summary>
        public void createGradientBox(double width, double height)
        {
        }

        /// <summary>
        /// Given a point in the pretransform coordinate space, returns the coordinates of that point after the transformation occurs.
        /// </summary>
        public Point deltaTransformPoint(Point point)
        {
            return default(Point);
        }

        /// <summary>
        /// Sets each matrix property to a value that causes a null transformation.
        /// </summary>
        public void identity()
        {
        }

        /// <summary>
        /// Performs the opposite transformation of the original matrix.
        /// </summary>
        public void invert()
        {
        }

        /// <summary>
        /// Applies a rotation transformation to the Matrix object.
        /// </summary>
        public void rotate(double angle)
        {
        }

        /// <summary>
        /// Applies a scaling transformation to the matrix.
        /// </summary>
        public void scale(double sx, double sy)
        {
        }

        /// <summary>
        /// Returns the result of applying the geometric transformation represented by the Matrix object to the specified point.
        /// </summary>
        public Point transformPoint(Point point)
        {
            return default(Point);
        }

        /// <summary>
        /// Translates the matrix along the x and y axes, as specified by the dx and dy parameters.
        /// </summary>
        public void translate(double dx, double dy)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new Matrix object with the specified parameters.
        /// </summary>
        public Matrix(double a, double b, double c, double d, double tx, double ty)
        {
        }

        /// <summary>
        /// Creates a new Matrix object with the specified parameters.
        /// </summary>
        public Matrix(double a, double b, double c, double d, double tx)
        {
        }

        /// <summary>
        /// Creates a new Matrix object with the specified parameters.
        /// </summary>
        public Matrix(double a, double b, double c, double d)
        {
        }

        /// <summary>
        /// Creates a new Matrix object with the specified parameters.
        /// </summary>
        public Matrix(double a, double b, double c)
        {
        }

        /// <summary>
        /// Creates a new Matrix object with the specified parameters.
        /// </summary>
        public Matrix(double a, double b)
        {
        }

        /// <summary>
        /// Creates a new Matrix object with the specified parameters.
        /// </summary>
        public Matrix(double a)
        {
        }

        /// <summary>
        /// Creates a new Matrix object with the specified parameters.
        /// </summary>
        public Matrix()
        {
        }

        #endregion

    }
}
