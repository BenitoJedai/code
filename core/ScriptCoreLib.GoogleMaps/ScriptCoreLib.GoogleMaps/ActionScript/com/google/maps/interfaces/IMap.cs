using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.com.google.maps.interfaces
{
    [Script(IsNative = true)]
    public interface IMap
    {
        #region Properties
        /// <summary>
        /// [read-only] Retrieves Mercator projection.
        /// </summary>
        IProjection MERCATOR_PROJECTION { get; }

        #endregion

        #region Methods
        /// <summary>
        /// Registers a new control.
        /// </summary>
        void addControl(IControl control);

        /// <summary>
        /// Registers a new map type.
        /// </summary>
        void addMapType(IMapType newMapType);

        /// <summary>
        /// Adds an overlay to the map.
        /// </summary>
        void addOverlay(IOverlay overlay);

        /// <summary>
        /// Removes all overlays from the map.
        /// </summary>
        void clearOverlays();

        /// <summary>
        /// Closes the information window.
        /// </summary>
        bool closeInfoWindow();

        /// <summary>
        /// Checks whether continuous zoom is enabled.
        /// </summary>
        bool continuousZoomEnabled();

        /// <summary>
        /// Disables continuous smooth zooming.
        /// </summary>
        void disableContinuousZoom();

        /// <summary>
        /// Disables dragging of the map.
        /// </summary>
        void disableDragging();

        /// <summary>
        /// Disables zooming using a mouse's scroll wheel.
        /// </summary>
        void disableScrollWheelZoom();

        /// <summary>
        /// Checks whether dragging of the map is enabled.
        /// </summary>
        bool draggingEnabled();

        /// <summary>
        /// Enables continuous smooth zooming.
        /// </summary>
        void enableContinuousZoom();

        /// <summary>
        /// Enables dragging of the map.
        /// </summary>
        void enableDragging();

        /// <summary>
        /// Enables zooming using a mouse's scroll wheel.
        /// </summary>
        void enableScrollWheelZoom();

        /// <summary>
        /// Returns x,y coordinates of specified lat, lng and zoom.
        /// </summary>
        Point fromLatLngToPoint(LatLng latLng, double opt_zoom);

        /// <summary>
        /// Returns lat,lng coordinates of specified x, y and zoom.
        /// </summary>
        LatLng fromPointToLatLng(Point pos, double opt_zoom, bool opt_nowrap);

        /// <summary>
        /// Returns lat,lng coordinates of specified x, y and zoom.
        /// </summary>
        LatLng fromPointToLatLng(Point pos, double opt_zoom);

        /// <summary>
        /// Returns the highest resolution zoom level at which the given rectangular region fits in the map view.
        /// </summary>
        double getBoundsZoomLevel(LatLngBounds bounds);

        /// <summary>
        /// Retrieves coordinates of the center in the map view control.
        /// </summary>
        LatLng getCenter();

        /// <summary>
        /// Retrieves the current map type.
        /// </summary>
        IMapType getCurrentMapType();

        /// <summary>
        /// Retrieves the display object that represents the map.
        /// </summary>
        DisplayObject getDisplayObject();

        /// <summary>
        /// Get the version of the implementation SWF.
        /// </summary>
        string getImplementationVersion();

        /// <summary>
        /// Returns the the visible rectangular region of the map view in geographical coordinates.
        /// </summary>
        LatLngBounds getLatLngBounds();

        /// <summary>
        /// Retrieves the list of the map types available for the location.
        /// </summary>
        Array getMapTypes();

        /// <summary>
        /// Retrieves the maximum zoom level.
        /// </summary>
        double getMaxZoomLevel(IMapType opt_mapType, LatLng opt_point);

        /// <summary>
        /// Retrieves the maximum zoom level.
        /// </summary>
        double getMaxZoomLevel(IMapType opt_mapType);

        /// <summary>
        /// Retrieves the maximum zoom level.
        /// </summary>
        double getMaxZoomLevel();

        /// <summary>
        /// Retrieves the minimum zoom level.
        /// </summary>
        double getMinZoomLevel(IMapType opt_mapType, LatLng opt_point);

        /// <summary>
        /// Retrieves the minimum zoom level.
        /// </summary>
        double getMinZoomLevel(IMapType opt_mapType);

        /// <summary>
        /// Retrieves the minimum zoom level.
        /// </summary>
        double getMinZoomLevel();

        /// <summary>
        /// Returns the projection being applied to the map.
        /// </summary>
        IProjection getProjection();

        /// <summary>
        /// Retrieves the map view size.
        /// </summary>
        Point getSize();

        /// <summary>
        /// Retrieves the map zoom level.
        /// </summary>
        double getZoom();

        /// <summary>
        /// Checks whether the map has been initialized.
        /// </summary>
        bool isLoaded();

        /// <summary>
        /// Opens a simple information window at the given point.
        /// </summary>
        IInfoWindow openInfoWindow(LatLng latlng, InfoWindowOptions options);

        /// <summary>
        /// Opens a simple information window at the given point.
        /// </summary>
        IInfoWindow openInfoWindow(LatLng latlng);

        /// <summary>
        /// Starts a pan animation by the given distance in pixels.
        /// </summary>
        void panBy(Point distance);

        /// <summary>
        /// Pans the map to the specified centre location.
        /// </summary>
        void panTo(LatLng latLng);

        /// <summary>
        /// Removes a control from the map.
        /// </summary>
        void removeControl(IControl control);

        /// <summary>
        /// Removes a registered map type.
        /// </summary>
        void removeMapType(IMapType oldMapType);

        /// <summary>
        /// Removes an overlay from the map.
        /// </summary>
        void removeOverlay(IOverlay overlay);

        /// <summary>
        /// Returns map to the saved position.
        /// </summary>
        void returnToSavedPosition();

        /// <summary>
        /// Stores the current map position and zoom level for later recall by returnToSavedPosition.
        /// </summary>
        void savePosition();

        /// <summary>
        /// Returns true if scroll wheel zooming is enabled.
        /// </summary>
        bool scrollWheelZoomEnabled();

        /// <summary>
        /// Changes the centre point of the map.
        /// </summary>
        void setCenter(LatLng latLng, double opt_zoom, IMapType opt_mapType);

        /// <summary>
        /// Changes the centre point of the map.
        /// </summary>
        void setCenter(LatLng latLng, double opt_zoom);

        /// <summary>
        /// Changes the map type for the map.
        /// </summary>
        void setMapType(IMapType mapType);

        /// <summary>
        /// Sets the size of the map view.
        /// </summary>
        void setSize(Point newSize);

        /// <summary>
        /// Changes the zoom level for the map view control.
        /// </summary>
        void setZoom(double level);

        /// <summary>
        /// Zooms in the map by one zoom level if possible.
        /// </summary>
        void zoomIn(LatLng opt_latlng, bool opt_doCenter, bool opt_doContinuousZoom);

        /// <summary>
        /// Zooms in the map by one zoom level if possible.
        /// </summary>
        void zoomIn(LatLng opt_latlng, bool opt_doCenter);

        /// <summary>
        /// Zooms in the map by one zoom level if possible.
        /// </summary>
        void zoomIn(LatLng opt_latlng);

        /// <summary>
        /// Zooms in the map by one zoom level if possible.
        /// </summary>
        void zoomIn();

        /// <summary>
        /// Zooms out the map by one zoom level if possible.
        /// </summary>
        void zoomOut(LatLng opt_latlng, bool opt_doContinuousZoom);

        /// <summary>
        /// Zooms out the map by one zoom level if possible.
        /// </summary>
        void zoomOut(LatLng opt_latlng);

        /// <summary>
        /// Zooms out the map by one zoom level if possible.
        /// </summary>
        void zoomOut();

        #endregion

    }
}
