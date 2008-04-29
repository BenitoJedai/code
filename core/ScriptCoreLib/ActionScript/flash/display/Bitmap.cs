using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/3/langref/flash/display/Bitmap.html
    [Script(IsNative = true)]
    public class Bitmap : DisplayObject
    {
        #region Methods
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a Bitmap object to refer to the specified BitmapData object.
        /// </summary>
        public Bitmap(BitmapData bitmapData, string pixelSnapping, bool smoothing)
        {
        }

        /// <summary>
        /// Initializes a Bitmap object to refer to the specified BitmapData object.
        /// </summary>
        public Bitmap(BitmapData bitmapData, string pixelSnapping)
        {
        }

        /// <summary>
        /// Initializes a Bitmap object to refer to the specified BitmapData object.
        /// </summary>
        public Bitmap(BitmapData bitmapData)
        {
        }

        /// <summary>
        /// Initializes a Bitmap object to refer to the specified BitmapData object.
        /// </summary>
        public Bitmap()
        {
        }

        #endregion


        #region Properties
        /// <summary>
        /// The BitmapData object being referenced.
        /// </summary>
        public BitmapData bitmapData { get; set; }

        /// <summary>
        /// Controls whether or not the Bitmap object is snapped to the nearest pixel.
        /// </summary>
        public string pixelSnapping { get; set; }

        /// <summary>
        /// Controls whether or not the bitmap is smoothed when scaled.
        /// </summary>
        public bool smoothing { get; set; }

        #endregion

    }
}
