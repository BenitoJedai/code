using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.com.google.maps.interfaces;

namespace ScriptCoreLib.ActionScript.com.google.maps
{
    [Script(IsNative = true)]
    public class LatLng
    {
        #region Methods
        /// <summary>
        /// Returns the angle (radians) between this point and the given point.
        /// </summary>
        public double angleFrom(LatLng other)
        {
            return default(double);
        }

        /// <summary>
        /// Returns a new LatLng object that is a copy of this.
        /// </summary>
        public LatLng clone()
        {
            return default(LatLng);
        }

        /// <summary>
        /// Returns the distance, in meters, from this point to the given point.
        /// </summary>
        public double distanceFrom(LatLng other, double opt_radius)
        {
            return default(double);
        }

        /// <summary>
        /// Tests whether this LatLng is coincident with another specified LatLng, allowing for numerical rounding errors.
        /// </summary>
        public bool equals(LatLng other)
        {
            return default(bool);
        }

        /// <summary>
        /// [static] Creates a latlng from radian values.
        /// </summary>
        public static LatLng fromRadians(double lat, double lng, bool opt_noCorrect)
        {
            return default(LatLng);
        }

        /// <summary>
        /// [static] Creates a latlng from radian values.
        /// </summary>
        public static LatLng fromRadians(double lat, double lng)
        {
            return default(LatLng);
        }

        /// <summary>
        /// [static] Parses a string of the form "lat,lng" and returns a point with those values.
        /// </summary>
        public static LatLng fromUrlValue(string value)
        {
            return default(LatLng);
        }

        /// <summary>
        /// Returns the latitude in degrees.
        /// </summary>
        public double lat()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the latitude in radians.
        /// </summary>
        public double latRadians()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the longitude in degrees.
        /// </summary>
        public double lng()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the longitude in radians.
        /// </summary>
        public double lngRadians()
        {
            return default(double);
        }

        /// <summary>
        /// Returns a string of the form "lat,lng" for this LatLng.
        /// </summary>
        public string toUrlValue(double opt_precision)
        {
            return default(string);
        }

        /// <summary>
        /// Returns a string of the form "lat,lng" for this LatLng.
        /// </summary>
        public string toUrlValue()
        {
            return default(string);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a LatLng.
        /// </summary>
        public LatLng(double lat, double lng, bool opt_noCorrect)
        {
        }

        /// <summary>
        /// Constructs a LatLng.
        /// </summary>
        public LatLng(double lat, double lng)
        {
        }

        #endregion

        #region Constants
        /// <summary>
        /// [static] The equatorial radius of the earth in meters.
        /// </summary>
        public static readonly double EARTH_RADIUS = 6378137;

        #endregion

    }
}
