using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.com.google.maps.interfaces;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.com.google.maps
{
    [Script(IsNative = true)]
    public class MapEvent : Event
    {
        #region Properties
        /// <summary>
        /// [read-only] The object that the event refers to (such as an instance of IMapType for MapEvent.MAPTYPE_ADDED event or an instance of IControl for MapEvent.CONTROL_REMOVED).
        /// </summary>
        public object feature { get; private set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] This event is fired on the map when a control is added to the map.
        /// </summary>
        public static readonly string CONTROL_ADDED = "mapevent_controladded";

        /// <summary>
        /// [static] This event is fired on the map when a control is removed from the map.
        /// </summary>
        public static readonly string CONTROL_REMOVED = "mapevent_controlremoved";

        /// <summary>
        /// [static] This event is fired when the copyright that should be displayed on the map is updated.
        /// </summary>
        public static readonly string COPYRIGHTS_UPDATED = "mapevent_copyrightsupdated";

        /// <summary>
        /// [static] This event is fired when the info window closes.
        /// </summary>
        public static readonly string INFOWINDOW_CLOSED = "mapevent_infowindowclosed";

        /// <summary>
        /// [static] This event is fired before the info window closes.
        /// </summary>
        public static readonly string INFOWINDOW_CLOSING = "mapevent_infowindowclosing";

        /// <summary>
        /// [static] This event is fired when the info window opens.
        /// </summary>
        public static readonly string INFOWINDOW_OPENED = "mapevent_infowindowopened";

        /// <summary>
        /// [static] This event is fired when map setup is complete and isLoaded() would return true.
        /// </summary>
        public static readonly string MAP_READY = "mapevent_mapready";

        /// <summary>
        /// [static] This event is fired when a new MapType has been added to the map.
        /// </summary>
        public static readonly string MAPTYPE_ADDED = "mapevent_maptypeadded";

        /// <summary>
        /// [static] This event is fired when another map type is selected.
        /// </summary>
        public static readonly string MAPTYPE_CHANGED = "maptypechanged";

        /// <summary>
        /// [static] This event is fired when a MapType has been removed from the map.
        /// </summary>
        public static readonly string MAPTYPE_REMOVED = "mapevent_maptyperemoved";

        /// <summary>
        /// [static] This event is fired when the size of the map has changed.
        /// </summary>
        public static readonly string SIZE_CHANGED = "mapevent_sizechanged";

        /// <summary>
        /// [static] This event is fired when an overlay's visibility has changed (from visible to hidden or vice-versa).
        /// </summary>
        public static readonly string VISIBILITY_CHANGED = "mapevent_visibilitychanged";

        #endregion


    }
}
