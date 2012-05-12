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
    /// Optional parameters to customize the retrieval of the geolocation
    /// http://docs.phonegap.com/en/1.7.0/cordova_geolocation_geolocation.md.html#geolocationOptions
    /// </summary>
    [Script(IsNative = true)]
    public class GeolocationOptions
    {
        #region Constructor

        public GeolocationOptions()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  How often to retrieve the position in milliseconds. This option is not part of the W3C spec and will be removed in the future. maximumAge should be used instead. (Number) (Default: 10000)
        /// </summary>
        public int frequency=10000;

        /// <summary>
        ///  Provides a hint that the application would like to receive the best possible results. (Boolean)
        /// </summary>
        public bool enableHighAccuracy;

        /// <summary>
        ///  The maximum length of time (msec) that is allowed to pass from the call to geolocation.getCurrentPosition or geolocation.watchPosition until the corresponding geolocationSuccess callback is invoked. (Number)
        /// </summary>
        public int timeout;

        /// <summary>
        ///  Accept a cached position whose age is no greater than the specified time in milliseconds. (Number)
        /// </summary>
        public int maximumAge;

        #endregion
    }
}
