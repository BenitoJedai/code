using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.filters;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/2/langref/flash/display/DisplayObject.html
    [Script(IsNative = true)]
    public class DisplayObject
    {
        /// <summary>
        /// An indexed array that contains each filter object currently associated with the display object.
        /// </summary>
        public BitmapFilter[] filters { get; set; }

        /// <summary>
        /// Indicates the alpha transparency value of the object specified.
        /// </summary>
        public double alpha { get; set; }

        /// <summary>
        /// Indicates the rotation of the DisplayObject instance, in degrees, from its original orientation.
        /// </summary>
        public double rotation { get; set; }

        /// <summary>
        /// Indicates the x coordinate of the DisplayObject instance relative to the local coordinates of the parent DisplayObjectContainer.
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// Indicates the y coordinate of the DisplayObject instance relative to the local coordinates of the parent DisplayObjectContainer.
        /// </summary>
        public double y { get; set; }

        /// <summary>
        /// For a display object in a loaded SWF file, the root property is the top-most display object in the portion of the display list's tree structure represented by that SWF file.
        /// </summary>
        public DisplayObject root { get; private set; }

        /// <summary>
        /// Indicates the width of the display object, in pixels.
        /// </summary>
        public double width { get; set; }

        /// <summary>
        /// Indicates the height of the display object, in pixels.
        /// </summary>
        public double height { get; set; }
    }
}
