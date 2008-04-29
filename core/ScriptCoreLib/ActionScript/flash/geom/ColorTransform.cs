using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.geom
{
    // http://livedocs.adobe.com/flex/3/langref/flash/geom/ColorTransform.html
    [Script(IsNative=true)]
    public class ColorTransform
    {
        #region Properties
        /// <summary>
        /// A decimal value that is multiplied with the alpha transparency channel value.
        /// </summary>
        public double alphaMultiplier { get; set; }

        /// <summary>
        /// A number from -255 to 255 that is added to the alpha transparency channel value after it has been multiplied by the alphaMultiplier value.
        /// </summary>
        public double alphaOffset { get; set; }

        /// <summary>
        /// A decimal value that is multiplied with the blue channel value.
        /// </summary>
        public double blueMultiplier { get; set; }

        /// <summary>
        /// A number from -255 to 255 that is added to the blue channel value after it has been multiplied by the blueMultiplier value.
        /// </summary>
        public double blueOffset { get; set; }

        /// <summary>
        /// The RGB color value for a ColorTransform object.
        /// </summary>
        public uint color { get; set; }

        /// <summary>
        /// A decimal value that is multiplied with the green channel value.
        /// </summary>
        public double greenMultiplier { get; set; }

        /// <summary>
        /// A number from -255 to 255 that is added to the green channel value after it has been multiplied by the greenMultiplier value.
        /// </summary>
        public double greenOffset { get; set; }

        /// <summary>
        /// A decimal value that is multiplied with the red channel value.
        /// </summary>
        public double redMultiplier { get; set; }

        /// <summary>
        /// A number from -255 to 255 that is added to the red channel value after it has been multiplied by the redMultiplier value.
        /// </summary>
        public double redOffset { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Concatenates the ColorTranform object specified by the second parameter with the current ColorTransform object and sets the current object as the result, which is an additive combination of the two color transformations.
        /// </summary>
        public void concat(ColorTransform second)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a ColorTransform object for a display object with the specified color channel values and alpha values.
        /// </summary>
        public ColorTransform(double redMultiplier, double greenMultiplier, double blueMultiplier, double alphaMultiplier, double redOffset, double greenOffset, double blueOffset, double alphaOffset)
        {
        }

        /// <summary>
        /// Creates a ColorTransform object for a display object with the specified color channel values and alpha values.
        /// </summary>
        public ColorTransform(double redMultiplier, double greenMultiplier, double blueMultiplier, double alphaMultiplier, double redOffset, double greenOffset, double blueOffset)
        {
        }

        /// <summary>
        /// Creates a ColorTransform object for a display object with the specified color channel values and alpha values.
        /// </summary>
        public ColorTransform(double redMultiplier, double greenMultiplier, double blueMultiplier, double alphaMultiplier, double redOffset, double greenOffset)
        {
        }

        /// <summary>
        /// Creates a ColorTransform object for a display object with the specified color channel values and alpha values.
        /// </summary>
        public ColorTransform(double redMultiplier, double greenMultiplier, double blueMultiplier, double alphaMultiplier, double redOffset)
        {
        }

        /// <summary>
        /// Creates a ColorTransform object for a display object with the specified color channel values and alpha values.
        /// </summary>
        public ColorTransform(double redMultiplier, double greenMultiplier, double blueMultiplier, double alphaMultiplier)
        {
        }

        /// <summary>
        /// Creates a ColorTransform object for a display object with the specified color channel values and alpha values.
        /// </summary>
        public ColorTransform(double redMultiplier, double greenMultiplier, double blueMultiplier)
        {
        }

        /// <summary>
        /// Creates a ColorTransform object for a display object with the specified color channel values and alpha values.
        /// </summary>
        public ColorTransform(double redMultiplier, double greenMultiplier)
        {
        }

        /// <summary>
        /// Creates a ColorTransform object for a display object with the specified color channel values and alpha values.
        /// </summary>
        public ColorTransform(double redMultiplier)
        {
        }

        /// <summary>
        /// Creates a ColorTransform object for a display object with the specified color channel values and alpha values.
        /// </summary>
        public ColorTransform()
        {
        }

        #endregion

    }
}
