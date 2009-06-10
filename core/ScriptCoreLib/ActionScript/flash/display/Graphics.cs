﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/3/langref/flash/display/Graphics.html
    [Script(IsNative = true)]
    public sealed class Graphics
    {
        #region Methods
        /// <summary>
        /// Fills a drawing area with a bitmap image.
        /// </summary>
        public void beginBitmapFill(BitmapData bitmap, Matrix matrix, bool repeat, bool smooth)
        {
        }

        /// <summary>
        /// Fills a drawing area with a bitmap image.
        /// </summary>
        public void beginBitmapFill(BitmapData bitmap, Matrix matrix, bool repeat)
        {
        }

        /// <summary>
        /// Fills a drawing area with a bitmap image.
        /// </summary>
        public void beginBitmapFill(BitmapData bitmap, Matrix matrix)
        {
        }

        /// <summary>
        /// Fills a drawing area with a bitmap image.
        /// </summary>
        public void beginBitmapFill(BitmapData bitmap)
        {
        }

        #endregion

        #region Constructors
        #endregion


       

		#region Methods
		/// <summary>
		/// Specifies a gradient fill used by subsequent calls to other Graphics methods (such as lineTo() or drawCircle()) for the object.
		/// </summary>
		public void beginGradientFill(string type, uint[] colors, double[] alphas, int[] ratios, Matrix matrix, string spreadMethod, string interpolationMethod, double focalPointRatio)
		{
		}

		/// <summary>
		/// Specifies a gradient fill used by subsequent calls to other Graphics methods (such as lineTo() or drawCircle()) for the object.
		/// </summary>
		public void beginGradientFill(string type, uint[] colors, double[] alphas, int[] ratios, Matrix matrix, string spreadMethod, string interpolationMethod)
		{
		}

		/// <summary>
		/// Specifies a gradient fill used by subsequent calls to other Graphics methods (such as lineTo() or drawCircle()) for the object.
		/// </summary>
		public void beginGradientFill(string type, uint[] colors, double[] alphas, int[] ratios, Matrix matrix, string spreadMethod)
		{
		}

		/// <summary>
		/// Specifies a gradient fill used by subsequent calls to other Graphics methods (such as lineTo() or drawCircle()) for the object.
		/// </summary>
		public void beginGradientFill(string type, uint[] colors, double[] alphas, int[] ratios, Matrix matrix)
		{
		}

		/// <summary>
		/// Specifies a gradient fill used by subsequent calls to other Graphics methods (such as lineTo() or drawCircle()) for the object.
		/// </summary>
		public void beginGradientFill(string type, uint[] colors, double[] alphas, int[] ratios)
		{
		}

		#endregion

		#region Constructors
		#endregion


        /// <summary>
        /// Clears the graphics that were drawn to this Graphics object, and resets fill and line style settings.
        /// </summary>
        public void clear()
        {
        }


        /// <summary>
        /// Specifies a simple one-color fill that Flash Player uses for subsequent calls to other Graphics methods (such as lineTo() or drawCircle()) for the object.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="alpha"></param>
        public void beginFill([Hex] uint color, double alpha)
        {
        }

        public void beginFill([Hex] uint color)
        {
        }

        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        public void drawCircle(double x, double y, double radius)
        {
        }


        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void drawRect(double x, double y, double width, double height)
        {
        }


        /// <summary>
        /// Applies a fill to the lines and curves that were added since the last call to the beginFill(), beginGradientFill(), or beginBitmapFill() method.
        /// </summary>
        public void endFill()
        {
        }




        /// <summary>
        /// Specifies a line style that Flash uses for subsequent calls to other Graphics methods (such as lineTo() or drawCircle()) for the object.
        /// </summary>
        public void lineStyle(double thickness, uint color, double alpha)
        {

        }

        /// <summary>
        /// Draws a line using the current line style from the current drawing position to (x, y); the current drawing position is then set to (x, y).
        /// </summary>
        public void lineTo(double x, double y)
        {
        }

        /// <summary>
        /// Moves the current drawing position to (x, y).
        /// </summary>
        public void moveTo(double x, double y)
        {
        }


    }
}
