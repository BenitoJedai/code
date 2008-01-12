using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/2/langref/flash/display/DisplayObject.html
    [Script(IsNative = true)]
    public class DisplayObject
    {
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

        public DisplayObject root { get; private set; }
    }
}
