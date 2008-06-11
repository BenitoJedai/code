using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.com.google.maps.interfaces;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.com.google.maps
{
    [Script(IsNative = true)]
    public class MapType : IMapType
    {
        #region Properties
        /// <summary>
        /// [static][read-only] Provides access to the list of default map types.
        /// </summary>
        public static Array DEFAULT_MAP_TYPES { get; private set; }

        /// <summary>
        /// [static][read-only] Provides access to Hybrid Map Type
        /// </summary>
        public static IMapType HYBRID_MAP_TYPE { get; private set; }

        /// <summary>
        /// [static][read-only] Provides access to Normal Map Type
        /// </summary>
        public static IMapType NORMAL_MAP_TYPE { get; private set; }

        /// <summary>
        /// [static][read-only] Provides access to Physical Map Type
        /// </summary>
        public static IMapType PHYSICAL_MAP_TYPE { get; private set; }

        /// <summary>
        /// [static][read-only] Provides access to Satellite Map Type
        /// </summary>
        public static IMapType SATELLITE_MAP_TYPE { get; private set; }

        #endregion

        #region Methods
        /// <summary>
        /// Registers an event listener object so that the listener could receive notification of an event.
        /// </summary>
        public void addEventListener(string eventType, Function listener, bool useCapture, int priority, bool useWeakReference)
        {
        }

        /// <summary>
        /// Registers an event listener object so that the listener could receive notification of an event.
        /// </summary>
        public void addEventListener(string eventType, Function listener, bool useCapture, int priority)
        {
        }

        /// <summary>
        /// Registers an event listener object so that the listener could receive notification of an event.
        /// </summary>
        public void addEventListener(string eventType, Function listener, bool useCapture)
        {
        }

        /// <summary>
        /// Registers an event listener object so that the listener could receive notification of an event.
        /// </summary>
        public void addEventListener(string eventType, Function listener)
        {
        }

        /// <summary>
        /// Dispatches an event into the event flow.
        /// </summary>
        public bool dispatchEvent(Event @event)
        {
            return default(bool);
        }

        /// <summary>
        /// Returns the text of the hint that is displayed when the user hovers over a control that allows selection of this map type.
        /// </summary>
        public string getAlt()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the highest resolution zoom level required to show the given lat/lng bounds in a map of the given pixel size.
        /// </summary>
        public double getBoundsZoomLevel(LatLngBounds bounds, Point viewSize)
        {
            return default(double);
        }

        /// <summary>
        /// Returns an array of copyright notices for the given bounds and zoom level.
        /// </summary>
        public Array getCopyrights(LatLngBounds bounds, double zoom)
        {
            return default(Array);
        }

        /// <summary>
        /// Returns the text to be displayed if a tile fails to download.
        /// </summary>
        public string getErrorMessage()
        {
            return default(string);
        }

        /// <summary>
        /// If a control displays a link above the map, returns the color we should use.
        /// </summary>
        public double getLinkColor()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the zoom level of the maximum resolution supported by this map type.
        /// </summary>
        public double getMaximumResolution(LatLng opt_point)
        {
            return default(double);
        }

        /// <summary>
        /// Returns the zoom level of the maximum resolution supported by this map type.
        /// </summary>
        public double getMaximumResolution()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the max resolution override.
        /// </summary>
        public double getMaxResolutionOverride()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the zoom level of the minimum resolution supported by this map type.
        /// </summary>
        public double getMinimumResolution(LatLng opt_point)
        {
            return default(double);
        }

        /// <summary>
        /// Returns the zoom level of the minimum resolution supported by this map type.
        /// </summary>
        public double getMinimumResolution()
        {
            return default(double);
        }

        /// <summary>
        /// Retrieves the map type name.
        /// </summary>
        public string getName(bool opt_short)
        {
            return default(string);
        }

        /// <summary>
        /// Retrieves the map type name.
        /// </summary>
        public string getName()
        {
            return default(string);
        }

        /// <summary>
        /// Retrieves the map type projection.
        /// </summary>
        public IProjection getProjection()
        {
            return default(IProjection);
        }

        /// <summary>
        /// Returns the radius of the planet for which this map type is defined.
        /// </summary>
        public double getRadius()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the highest resolution zoom level required to show the given lat/lng span with the given center point.
        /// </summary>
        public double getSpanZoomLevel(LatLng center, LatLng span, Point viewSize)
        {
            return default(double);
        }

        /// <summary>
        /// If controls are textual, returns the appropriate color to display the text.
        /// </summary>
        public double getTextColor()
        {
            return default(double);
        }

        /// <summary>
        /// Gets the list of tile layers for this map type.
        /// </summary>
        public Array getTileLayers()
        {
            return default(Array);
        }

        /// <summary>
        /// Gets the tile size for this map type.
        /// </summary>
        public double getTileSize()
        {
            return default(double);
        }

        /// <summary>
        /// Returns a string that may be used as a URL parameter to identify this map type in permalinks to the current map view.
        /// </summary>
        public string getUrlArg()
        {
            return default(string);
        }

        /// <summary>
        /// Checks whether the EventDispatcher object has any listeners registered for a specific type of event.
        /// </summary>
        public bool hasEventListener(string eventType)
        {
            return default(bool);
        }

        /// <summary>
        /// Removes a listener from the EventDispatcher object.
        /// </summary>
        public void removeEventListener(string eventType, Function listener, bool useCapture)
        {
        }

        /// <summary>
        /// Removes a listener from the EventDispatcher object.
        /// </summary>
        public void removeEventListener(string eventType, Function listener)
        {
        }

        /// <summary>
        /// Sets the max resolution override, such that, if this number is greater than the max resolution that our map type reports to us, we will use this number instead.
        /// </summary>
        public void setMaxResolutionOverride(double maxResolution)
        {
        }

        /// <summary>
        /// Checks whether an event listener is registered with this object or any of its ancestors for the specified event type.
        /// </summary>
        public bool willTrigger(string eventType)
        {
            return default(bool);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of MapType object (for custom map types).
        /// </summary>
        public MapType(Array tileLayers, IProjection projection, string name, MapTypeOptions options)
        {
        }

        /// <summary>
        /// Creates an instance of MapType object (for custom map types).
        /// </summary>
        public MapType(Array tileLayers, IProjection projection, string name)
        {
        }

        #endregion

    }
}
