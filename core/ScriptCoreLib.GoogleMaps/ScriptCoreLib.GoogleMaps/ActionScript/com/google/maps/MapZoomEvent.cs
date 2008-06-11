using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.com.google.maps
{
    [Script(IsNative = true)]
    public class MapZoomEvent : MapEvent
    {
        #region Properties
        /// <summary>
        /// [read-only] Current zoom level for the map.
        /// </summary>
        public double zoomLevel { get; private set; }

        #endregion

        #region Methods
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a MapZoomEvent object to pass as a parameter to event listeners.
        /// </summary>
        public MapZoomEvent(string type, double zoomLevel, bool bubbles, bool cancellable)
        {
        }

        /// <summary>
        /// Creates a MapZoomEvent object to pass as a parameter to event listeners.
        /// </summary>
        public MapZoomEvent(string type, double zoomLevel, bool bubbles)
        {
        }

        /// <summary>
        /// Creates a MapZoomEvent object to pass as a parameter to event listeners.
        /// </summary>
        public MapZoomEvent(string type, double zoomLevel)
        {
        }

        #endregion


        #region Constants
        /// <summary>
        /// [static] This event is fired when zooming of the map ends.
        /// </summary>
        public static readonly string ZOOM_END = "mapevent_zoomend";

        /// <summary>
        /// [static] This event is fired when the available zoom range for the map changes.
        /// </summary>
        public static readonly string ZOOM_RANGE_CHANGED = "mapevent_zoomrangechanged";

        /// <summary>
        /// [static] This event is fired when zooming of the map starts.
        /// </summary>
        public static readonly string ZOOM_START = "mapevent_zoomstart";

        /// <summary>
        /// [static] This event is fired repeatedly while the map is zooming.
        /// </summary>
        public static readonly string ZOOM_STEP = "mapevent_zoomstep";

        #endregion

    }
}
