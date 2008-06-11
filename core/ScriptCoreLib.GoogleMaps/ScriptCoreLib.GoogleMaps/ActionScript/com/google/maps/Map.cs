using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.com.google.maps.interfaces;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.com.google.maps
{
    // http://code.google.com/apis/maps/documentation/flash/reference.html#Map
    [Script(IsNative = true)]
    public class Map : Sprite, IMap
    {


        #region Properties
        /// <summary>
        /// The map key.
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// The desired map language.
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// [read-only] Retrieves Mercator projection.
        /// </summary>
        public IProjection MERCATOR_PROJECTION { get; private set; }

        /// <summary>
        /// The desired map library version.
        /// </summary>
        public string version { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Registers a new control.
        /// </summary>
        public void addControl(IControl control)
        {
        }

        /// <summary>
        /// Registers a new map type.
        /// </summary>
        public void addMapType(IMapType newMapType)
        {
        }

        /// <summary>
        /// Adds an overlay to the map.
        /// </summary>
        public void addOverlay(IOverlay overlay)
        {
        }

        /// <summary>
        /// Removes all overlays from the map.
        /// </summary>
        public void clearOverlays()
        {
        }

        /// <summary>
        /// Closes the information window.
        /// </summary>
        public bool closeInfoWindow()
        {
            return default(bool);
        }

        /// <summary>
        /// Checks whether continuous zoom is enabled.
        /// </summary>
        public bool continuousZoomEnabled()
        {
            return default(bool);
        }

        /// <summary>
        /// Disables continuous smooth zooming.
        /// </summary>
        public void disableContinuousZoom()
        {
        }

        /// <summary>
        /// Disables dragging of the map.
        /// </summary>
        public void disableDragging()
        {
        }

        /// <summary>
        /// Disables zooming using a mouse's scroll wheel.
        /// </summary>
        public void disableScrollWheelZoom()
        {
        }

        /// <summary>
        /// Checks whether dragging of the map is enabled.
        /// </summary>
        public bool draggingEnabled()
        {
            return default(bool);
        }

        /// <summary>
        /// Enables continuous smooth zooming.
        /// </summary>
        public void enableContinuousZoom()
        {
        }

        /// <summary>
        /// Enables dragging of the map.
        /// </summary>
        public void enableDragging()
        {
        }

        /// <summary>
        /// Enables zooming using a mouse's scroll wheel.
        /// </summary>
        public void enableScrollWheelZoom()
        {
        }

        /// <summary>
        /// Returns x,y coordinates of specified lat, lng and zoom.
        /// </summary>
        public Point fromLatLngToPoint(LatLng latLng, double opt_zoom)
        {
            return default(Point);
        }

        /// <summary>
        /// Returns lat,lng coordinates of specified x, y and zoom.
        /// </summary>
        public LatLng fromPointToLatLng(Point pos, double opt_zoom, bool opt_nowrap)
        {
            return default(LatLng);
        }

        /// <summary>
        /// Returns lat,lng coordinates of specified x, y and zoom.
        /// </summary>
        public LatLng fromPointToLatLng(Point pos, double opt_zoom)
        {
            return default(LatLng);
        }

        /// <summary>
        /// Returns the highest resolution zoom level at which the given rectangular region fits in the map view.
        /// </summary>
        public double getBoundsZoomLevel(LatLngBounds bounds)
        {
            return default(double);
        }

        /// <summary>
        /// Retrieves coordinates of the center in the map view control.
        /// </summary>
        public LatLng getCenter()
        {
            return default(LatLng);
        }

        /// <summary>
        /// Retrieves the current map type.
        /// </summary>
        public IMapType getCurrentMapType()
        {
            return default(IMapType);
        }

        /// <summary>
        /// Retrieves the display object that represents the map.
        /// </summary>
        public DisplayObject getDisplayObject()
        {
            return default(DisplayObject);
        }

        /// <summary>
        /// Get the version of the implementation SWF.
        /// </summary>
        public string getImplementationVersion()
        {
            return default(string);
        }

        /// <summary>
        /// Retrieves the version of the client interface.
        /// </summary>
        public string getInterfaceVersion()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the the visible rectangular region of the map view in geographical coordinates.
        /// </summary>
        public LatLngBounds getLatLngBounds()
        {
            return default(LatLngBounds);
        }

        /// <summary>
        /// Retrieves the list of the map types available for the location.
        /// </summary>
        public Array getMapTypes()
        {
            return default(Array);
        }

        /// <summary>
        /// Retrieves the maximum zoom level.
        /// </summary>
        public double getMaxZoomLevel(IMapType opt_mapType, LatLng opt_point)
        {
            return default(double);
        }

        /// <summary>
        /// Retrieves the maximum zoom level.
        /// </summary>
        public double getMaxZoomLevel(IMapType opt_mapType)
        {
            return default(double);
        }

        /// <summary>
        /// Retrieves the maximum zoom level.
        /// </summary>
        public double getMaxZoomLevel()
        {
            return default(double);
        }

        /// <summary>
        /// Retrieves the minimum zoom level.
        /// </summary>
        public double getMinZoomLevel(IMapType opt_mapType, LatLng opt_point)
        {
            return default(double);
        }

        /// <summary>
        /// Retrieves the minimum zoom level.
        /// </summary>
        public double getMinZoomLevel(IMapType opt_mapType)
        {
            return default(double);
        }

        /// <summary>
        /// Retrieves the minimum zoom level.
        /// </summary>
        public double getMinZoomLevel()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the projection being applied to the map.
        /// </summary>
        public IProjection getProjection()
        {
            return default(IProjection);
        }

        /// <summary>
        /// Retrieves the map view size.
        /// </summary>
        public Point getSize()
        {
            return default(Point);
        }

        /// <summary>
        /// Retrieves the map zoom level.
        /// </summary>
        public double getZoom()
        {
            return default(double);
        }

        /// <summary>
        /// Checks whether the map has been initialized.
        /// </summary>
        public bool isLoaded()
        {
            return default(bool);
        }

        /// <summary>
        /// Opens a simple information window at the given point.
        /// </summary>
        public IInfoWindow openInfoWindow(LatLng latlng, InfoWindowOptions options)
        {
            return default(IInfoWindow);
        }

        /// <summary>
        /// Opens a simple information window at the given point.
        /// </summary>
        public IInfoWindow openInfoWindow(LatLng latlng)
        {
            return default(IInfoWindow);
        }

        /// <summary>
        /// Starts a pan animation by the given distance in pixels.
        /// </summary>
        public void panBy(Point distance)
        {
        }

        /// <summary>
        /// Pans the map to the specified centre location.
        /// </summary>
        public void panTo(LatLng latLng)
        {
        }

        /// <summary>
        /// Removes a control from the map.
        /// </summary>
        public void removeControl(IControl control)
        {
        }

        /// <summary>
        /// Removes a registered map type.
        /// </summary>
        public void removeMapType(IMapType oldMapType)
        {
        }

        /// <summary>
        /// Removes an overlay from the map.
        /// </summary>
        public void removeOverlay(IOverlay overlay)
        {
        }

        /// <summary>
        /// Returns map to the saved position.
        /// </summary>
        public void returnToSavedPosition()
        {
        }

        /// <summary>
        /// Stores the current map position and zoom level for later recall by returnToSavedPosition.
        /// </summary>
        public void savePosition()
        {
        }

        /// <summary>
        /// Returns true if scroll wheel zooming is enabled.
        /// </summary>
        public bool scrollWheelZoomEnabled()
        {
            return default(bool);
        }

        /// <summary>
        /// Changes the centre point of the map.
        /// </summary>
        public void setCenter(LatLng latLng, double opt_zoom, IMapType opt_mapType)
        {
        }

        /// <summary>
        /// Changes the centre point of the map.
        /// </summary>
        public void setCenter(LatLng latLng, double opt_zoom)
        {
        }

        /// <summary>
        /// Changes the map type for the map.
        /// </summary>
        public void setMapType(IMapType mapType)
        {
        }

        /// <summary>
        /// Sets the size of the map view.
        /// </summary>
        public void setSize(Point newSize)
        {
        }

        /// <summary>
        /// Changes the zoom level for the map view control.
        /// </summary>
        public void setZoom(double level)
        {
        }

        /// <summary>
        /// Zooms in the map by one zoom level if possible.
        /// </summary>
        public void zoomIn(LatLng opt_latlng, bool opt_doCenter, bool opt_doContinuousZoom)
        {
        }

        /// <summary>
        /// Zooms in the map by one zoom level if possible.
        /// </summary>
        public void zoomIn(LatLng opt_latlng, bool opt_doCenter)
        {
        }

        /// <summary>
        /// Zooms in the map by one zoom level if possible.
        /// </summary>
        public void zoomIn(LatLng opt_latlng)
        {
        }

        /// <summary>
        /// Zooms in the map by one zoom level if possible.
        /// </summary>
        public void zoomIn()
        {
        }

        /// <summary>
        /// Zooms out the map by one zoom level if possible.
        /// </summary>
        public void zoomOut(LatLng opt_latlng, bool opt_doContinuousZoom)
        {
        }

        /// <summary>
        /// Zooms out the map by one zoom level if possible.
        /// </summary>
        public void zoomOut(LatLng opt_latlng)
        {
        }

        /// <summary>
        /// Zooms out the map by one zoom level if possible.
        /// </summary>
        public void zoomOut()
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public Map()
        {
        }

        #endregion

        #region Events
        /// <summary>
        /// [static] This event is fired on the map when a control is added to the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> ControlAdded;

        /// <summary>
        /// [static] This event is fired on the map when a control is removed from the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> ControlRemoved;

        /// <summary>
        /// [static] This event is fired when the copyright that should be displayed on the map is updated.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> CopyrightsUpdated;

        /// <summary>
        /// [static] This event is fired when the info window closes.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> InfowindowClosed;

        /// <summary>
        /// [static] This event is fired before the info window closes.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> InfowindowClosing;

        /// <summary>
        /// [static] This event is fired when the info window opens.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> InfowindowOpened;

        /// <summary>
        /// [static] This event is fired when map setup is complete and isLoaded() would return true.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> MapReady;

        /// <summary>
        /// [static] This event is fired when a new MapType has been added to the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> MaptypeAdded;

        /// <summary>
        /// [static] This event is fired when another map type is selected.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> MaptypeChanged;

        /// <summary>
        /// [static] This event is fired when a MapType has been removed from the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> MaptypeRemoved;

        /// <summary>
        /// [static] This event is fired when the size of the map has changed.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> SizeChanged;

        /// <summary>
        /// [static] This event is fired when an overlay's visibility has changed (from visible to hidden or vice-versa).
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapEvent> VisibilityChanged;

        #endregion

        #region Events
        /// <summary>
        /// [static] This event is fired when the change of the map view ends.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMoveEvent> MapMoveEnd;

        /// <summary>
        /// [static] This event is fired when the map view starts changing.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMoveEvent> MapMoveStart;

        /// <summary>
        /// [static] This event is fired repeatedly while the map view is changing.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMoveEvent> MapMoveStep;

        #endregion




        #region Events
        /// <summary>
        /// [static] This event is fired when zooming of the map ends.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapZoomEvent> MapZoomEnd;

        /// <summary>
        /// [static] This event is fired when the available zoom range for the map changes.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapZoomEvent> MapZoomRangeChanged;

        /// <summary>
        /// [static] This event is fired when zooming of the map starts.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapZoomEvent> MapZoomStart;

        /// <summary>
        /// [static] This event is fired repeatedly while the map is zooming.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapZoomEvent> MapZoomStep;

        #endregion

        



        #region Events
        /// <summary>
        /// [static] This event is fired when the map is clicked with the mouse.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMouseEvent> MapClick;

        /// <summary>
        /// [static] This event is fired when a double click is done on the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMouseEvent> MapDoubleClick;

        /// <summary>
        /// [static] This event is fired when the user stops dragging the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMouseEvent> MapDragEnd;

        /// <summary>
        /// [static] This event is fired when the user starts dragging the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMouseEvent> MapDragStart;

        /// <summary>
        /// [static] This event is fired repeatedly while the user drags the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMouseEvent> MapDragStep;

        /// <summary>
        /// [static] This event is fired when the user presses the mouse button over the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMouseEvent> MapMouseDown;

        /// <summary>
        /// [static] This event is fired when the mouse moves over the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMouseEvent> MapMouseMove;

        /// <summary>
        /// [static] This event is fired when the user releases the mouse button over the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMouseEvent> MapMouseUp;

        /// <summary>
        /// [static] This event is fired when the user rolls the mouse off the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMouseEvent> MapRollOut;

        /// <summary>
        /// [static] This event is fired when the user rolls the mouse over the map.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<MapMouseEvent> MapRollOver;

        #endregion

       

    }
}
