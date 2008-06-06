using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.geom
{
    // http://livedocs.adobe.com/flex/3/langref/flash/geom/Transform.html
    [Script(IsNative=true)]
    public class Transform
    {
        #region Properties
        /// <summary>
        /// A ColorTransform object containing values that universally adjust the colors in the display object.
        /// </summary>
        public ColorTransform colorTransform { get; set; }

        /// <summary>
        /// [read-only] A ColorTransform object representing the combined color transformations applied to the display object and all of its parent objects, back to the root level.
        /// </summary>
        public ColorTransform concatenatedColorTransform { get; private set; }

        /// <summary>
        /// [read-only] A Matrix object representing the combined transformation matrixes of the display object and all of its parent objects, back to the root level.
        /// </summary>
        public Matrix concatenatedMatrix { get; private set; }

        /// <summary>
        /// A Matrix object containing values that affect the scaling, rotation, and translation of the display object.
        /// </summary>
        public Matrix matrix { get; set; }

        /// <summary>
        /// [read-only] A Rectangle object that defines the bounding rectangle of the display object on the Stage.
        /// </summary>
        public Rectangle pixelBounds { get; private set; }

        #endregion

    }
}
