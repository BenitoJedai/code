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
    /// A CompassHeading object is returned to the compassSuccess callback function when an error occurs
    /// http://docs.phonegap.com/en/1.7.0/cordova_compass_compass.md.html#compassHeading    
    /// </summary>
    [Script(IsNative = true)]
    public class CompassHeading
    {
        #region Constructor

        public CompassHeading()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// magneticHeading: The heading in degrees from 0 - 359.99 at a single moment in time. (Number)
        /// </summary>
        public int magneticHeading;
        
        /// <summary>
        ///trueHeading: The heading relative to the geographic North Pole in degrees 0 - 359.99 at a single moment in time. 
        ///A negative value indicates that the true heading could not be determined. (Number)
        /// </summary>
        public int trueHeading;

        /// <summary>
        /// headingAccuracy: The deviation in degrees between the reported heading and the true heading. (Number)
        /// </summary>
        public int headingAccuracy;

        /// <summary>
        /// timestamp: The time at which this heading was determined. (milliseconds)
        /// </summary>
        public int timestamp;

        #endregion


    }
}
