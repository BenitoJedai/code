using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.com.google.maps.controls
{
    [Script(IsNative = true)]
    public sealed class ControlPosition
    {
        #region Methods
        /// <summary>
        /// Retrieves the anchor identifier.
        /// </summary>
        public double getAnchor()
        {
            return default(double);
        }

        /// <summary>
        /// Retrieves the horizontal offset.
        /// </summary>
        public double getOffsetX()
        {
            return default(double);
        }

        /// <summary>
        /// Retrieves the vertical offset.
        /// </summary>
        public double getOffsetY()
        {
            return default(double);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a ControlPosition from offsets relative to a specified map corner.
        /// </summary>
        public ControlPosition(double anchor, double opt_paddingX, double opt_paddingY)
        {
        }

        #endregion

        #region Constants
        /// <summary>
        /// [static] The control will be anchored in the bottom left corner of the map.
        /// </summary>
        public static readonly double ANCHOR_BOTTOM_LEFT = 0x20;

        /// <summary>
        /// [static] The control will be anchored in the bottom right corner of the map.
        /// </summary>
        public static readonly double ANCHOR_BOTTOM_RIGHT = 0x21;

        /// <summary>
        /// [static] The control will be anchored in the top left corner of the map.
        /// </summary>
        public static readonly double ANCHOR_TOP_LEFT = 0;

        /// <summary>
        /// [static] The control will be anchored in the top right corner of the map.
        /// </summary>
        public static readonly double ANCHOR_TOP_RIGHT = 1;

        #endregion

    }
}
