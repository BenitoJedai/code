using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.com.google.maps
{
    [Script(IsNative = true)]
    public class MapMoveEvent : MapEvent
    {
        #region Properties
        /// <summary>
        /// [read-only] LatLng over which the MapMoveEvent occurred.
        /// </summary>
        public LatLng latLng { get; private set; }

        #endregion

        #region Methods
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a MapMoveEvent object to pass as a parameter to event listeners.
        /// </summary>
        public MapMoveEvent(string type, LatLng latLng, bool bubbles, bool cancellable)
        {
        }

        /// <summary>
        /// Creates a MapMoveEvent object to pass as a parameter to event listeners.
        /// </summary>
        public MapMoveEvent(string type, LatLng latLng, bool bubbles)
        {
        }

        /// <summary>
        /// Creates a MapMoveEvent object to pass as a parameter to event listeners.
        /// </summary>
        public MapMoveEvent(string type, LatLng latLng)
        {
        }

        #endregion


        #region Constants
        /// <summary>
        /// [static] This event is fired when the change of the map view ends.
        /// </summary>
        public static readonly string MOVE_END = "mapevent_moveend";

        /// <summary>
        /// [static] This event is fired when the map view starts changing.
        /// </summary>
        public static readonly string MOVE_START = "mapevent_movestart";

        /// <summary>
        /// [static] This event is fired repeatedly while the map view is changing.
        /// </summary>
        public static readonly string MOVE_STEP = "mapevent_movestep";

        #endregion

    }
}
