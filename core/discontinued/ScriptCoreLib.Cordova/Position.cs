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
    /// Contains Position coordinates that are created by the geolocation API
    /// http://docs.phonegap.com/en/1.7.0/cordova_geolocation_geolocation.md.html#Position
    /// </summary>
    [Script(IsNative = true)]
    public class Position
    {
        #region Constructor

        public Position(Coordinates coords, int timestamp)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        ///  A set of geographic coordinates. (Coordinates)
        /// </summary>
        public Coordinates coords;

        /// <summary>
        ///  Creation timestamp for coords in milliseconds. (DOMTimeStamp)
        /// </summary>
        public int timestamp;

        #endregion


    }
}
