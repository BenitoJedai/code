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
    /// The geolocation object provides access to the device's GPS sensor
    /// http://docs.phonegap.com/en/1.7.0/cordova_geolocation_geolocation.md.html#Geolocation
    /// </summary>
    [Script(IsNative = true)]
    public class Geolocation
    {
        #region Constructor

        public Geolocation()
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the device's current position as a Position object
        /// </summary>
        /// <param name="geolocationSuccess"></param>
        /// <param name="geolocationError"></param>
        /// <param name="geolocationOptions"></param>
       public void getCurrentPosition(Action<Position> geolocationSuccess, Action<string>geolocationError=null, GeolocationOptions geolocationOptions=null)
       {}

        /// <summary>
        /// Watches for changes to the device's current position
        /// </summary>
        /// <param name="geolocationSuccess"></param>
        /// <param name="geolocationError"></param>
        /// <param name="geolocationOptions"></param>
        public void watchPosition(Action<Position> geolocationSuccess, Action<string>geolocationError=null, GeolocationOptions geolocationOptions=null)
        {}

        /// <summary>
        /// Stop watching for changes to the device's location referenced by the watchID parameter
        /// </summary>
        /// <param name="watchID"></param>
        public void clearWatch(int watchID)
        { }

        #endregion

    }
}
