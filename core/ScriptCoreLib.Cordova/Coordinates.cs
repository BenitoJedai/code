using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System;


namespace ScriptCoreLib.Cordova
{
    /// <summary>
    /// A set of properties that describe the geographic coordinates of a position
    /// http://docs.phonegap.com/en/1.7.0/cordova_geolocation_geolocation.md.html#Coordinates
    /// </summary>
    [Script(IsNative = true)]
    public class Coordinates
    {
        #region Constructor

        public Coordinates()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        ///  Latitude in decimal degrees. (Number)
        /// </summary>
        public readonly int latitude;

        /// <summary>
        ///  Longitude in decimal degrees. (Number)
        /// </summary>
        public readonly int longitude;

        /// <summary>
        ///  Height of the position in meters above the ellipsoid. (Number)
        /// </summary>
        public readonly int altitude;

        /// <summary>
        ///  Accuracy level of the latitude and longitude coordinates in meters. (Number)
        /// </summary>
        public readonly int accuracy;

        /// <summary>
        ///  Accuracy level of the altitude coordinate in meters. (Number)
        /// </summary>
        public readonly int altitudeAccuracy;

        /// <summary>
        ///  Direction of travel, specified in degrees counting clockwise relative to the true north. (Number)
        /// </summary>
        public readonly int heading;

        /// <summary>
        ///  Current ground speed of the device, specified in meters per second. (Number)
        /// </summary>
        public readonly int speed;

        #endregion

    }
}
