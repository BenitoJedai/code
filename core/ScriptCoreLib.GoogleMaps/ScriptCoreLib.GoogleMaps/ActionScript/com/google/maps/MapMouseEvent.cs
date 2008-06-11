using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.com.google.maps
{
    [Script(IsNative = true)]
    public class MapMouseEvent : MapEvent
    {
        #region Methods
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a MapMouseEvent object to pass as a parameter to event listeners.
        /// </summary>
        public MapMouseEvent(string type, object feature, LatLng latLng, bool bubbles, bool cancellable)
        {
        }

        /// <summary>
        /// Creates a MapMouseEvent object to pass as a parameter to event listeners.
        /// </summary>
        public MapMouseEvent(string type, object feature, LatLng latLng, bool bubbles)
        {
        }

        /// <summary>
        /// Creates a MapMouseEvent object to pass as a parameter to event listeners.
        /// </summary>
        public MapMouseEvent(string type, object feature, LatLng latLng)
        {
        }

        #endregion

        #region Properties
        /// <summary>
        /// [read-only] LatLng over which the MapMouseEvent occurred.
        /// </summary>
        public LatLng latLng { get; private set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] This event is fired when the map is clicked with the mouse.
        /// </summary>
        public static readonly string CLICK = "mapevent_click";

        /// <summary>
        /// [static] This event is fired when a double click is done on the map.
        /// </summary>
        public static readonly string DOUBLE_CLICK = "mapevent_doubleclick";

        /// <summary>
        /// [static] This event is fired when the user stops dragging the map.
        /// </summary>
        public static readonly string DRAG_END = "mapevent_dragend";

        /// <summary>
        /// [static] This event is fired when the user starts dragging the map.
        /// </summary>
        public static readonly string DRAG_START = "mapevent_dragstart";

        /// <summary>
        /// [static] This event is fired repeatedly while the user drags the map.
        /// </summary>
        public static readonly string DRAG_STEP = "mapevent_dragstep";

        /// <summary>
        /// [static] This event is fired when the user presses the mouse button over the map.
        /// </summary>
        public static readonly string MOUSE_DOWN = "mapevent_mousedown";

        /// <summary>
        /// [static] This event is fired when the mouse moves over the map.
        /// </summary>
        public static readonly string MOUSE_MOVE = "mapevent_mousemove";

        /// <summary>
        /// [static] This event is fired when the user releases the mouse button over the map.
        /// </summary>
        public static readonly string MOUSE_UP = "mapevent_mouseup";

        /// <summary>
        /// [static] This event is fired when the user rolls the mouse off the map.
        /// </summary>
        public static readonly string ROLL_OUT = "mapevent_rollout";

        /// <summary>
        /// [static] This event is fired when the user rolls the mouse over the map.
        /// </summary>
        public static readonly string ROLL_OVER = "mapevent_rollover";

        #endregion

    }
}
