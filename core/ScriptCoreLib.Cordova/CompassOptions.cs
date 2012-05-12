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
    /// An optional parameter to customize the retrieval of the compass
    /// http://docs.phonegap.com/en/1.7.0/cordova_compass_compass.md.html#compassOptions
    /// </summary>
    [Script(IsNative = true)]
    public class CompassOptions
    {
        #region Constructor

        public CompassOptions()
        {

        }
        
        #endregion

        #region VARS
        
        /// <summary>
        /// frequency: How often to retrieve the compass heading in milliseconds. (Number) (Default: 100)
        /// </summary>
        public int frequency = 100;

        /// <summary>
        /// filter: The change in degrees required to initiate a watchHeading success callback. (Number)
        /// </summary>
        public int filter;

        #endregion

    }
}
