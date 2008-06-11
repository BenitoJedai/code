using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.com.google.maps.interfaces
{
    [Script(IsNative = true)]
    public interface IMapType : IEventDispatcher
    {
        #region Methods
        /// <summary>
        /// Returns the text of the hint that is displayed when the user hovers over a control that allows selection of this map type.
        /// </summary>
        string getAlt();

        /// <summary>
        /// Returns the highest resolution zoom level required to show the given lat/lng bounds in a map of the given pixel size.
        /// </summary>
        double getBoundsZoomLevel(LatLngBounds bounds, Point viewSize);

        /// <summary>
        /// Returns an array of copyright notices for the given bounds and zoom level.
        /// </summary>
        Array getCopyrights(LatLngBounds bounds, double zoom);

        /// <summary>
        /// Returns the text to be displayed if a tile fails to download.
        /// </summary>
        string getErrorMessage();

        /// <summary>
        /// If a control displays a link above the map, returns the color we should use.
        /// </summary>
        double getLinkColor();

        /// <summary>
        /// Returns the zoom level of the maximum resolution supported by this map type.
        /// </summary>
        double getMaximumResolution(LatLng opt_point);

        /// <summary>
        /// Returns the zoom level of the maximum resolution supported by this map type.
        /// </summary>
        double getMaximumResolution();

        /// <summary>
        /// Returns the max resolution override.
        /// </summary>
        double getMaxResolutionOverride();

        /// <summary>
        /// Returns the zoom level of the minimum resolution supported by this map type.
        /// </summary>
        double getMinimumResolution(LatLng opt_point);

        /// <summary>
        /// Returns the zoom level of the minimum resolution supported by this map type.
        /// </summary>
        double getMinimumResolution();

        /// <summary>
        /// Retrieves the map type name.
        /// </summary>
        string getName(bool opt_short);

        /// <summary>
        /// Retrieves the map type name.
        /// </summary>
        string getName();

        /// <summary>
        /// Retrieves the map type projection.
        /// </summary>
        IProjection getProjection();

        /// <summary>
        /// Returns the radius of the planet for which this map type is defined.
        /// </summary>
        double getRadius();

        /// <summary>
        /// Returns the highest resolution zoom level required to show the given lat/lng span with the given center point.
        /// </summary>
        double getSpanZoomLevel(LatLng center, LatLng span, Point viewSize);

        /// <summary>
        /// If controls are textual, returns the appropriate color to display the text.
        /// </summary>
        double getTextColor();

        /// <summary>
        /// Gets the list of tile layers for this map type.
        /// </summary>
        Array getTileLayers();

        /// <summary>
        /// Gets the tile size for this map type.
        /// </summary>
        double getTileSize();

        /// <summary>
        /// Returns a string that may be used as a URL parameter to identify this map type in permalinks to the current map view.
        /// </summary>
        string getUrlArg();

        /// <summary>
        /// Sets the max resolution override, such that, if this number is greater than the max resolution that our map type reports to us, we will use this number instead.
        /// </summary>
        void setMaxResolutionOverride(double maxResolution);

        #endregion

    }
}
