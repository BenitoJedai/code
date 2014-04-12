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
    /// Obtains the direction that the device is pointing
    /// http://docs.phonegap.com/en/1.7.0/cordova_compass_compass.md.html#Compass
    /// </summary>
    [Script(IsNative = true)]
    public class Compass
    {
        #region Constructor

        public Compass()
        {

        }

        #endregion
      
        #region METHODS

        /// <summary>
        /// Encapsulates audio capture configuration options
        /// </summary>
        /// <param name="captureSuccess"></param>
        /// <param name="captureError"></param>
        /// <param name="options"></param>
        public void getCurrentHeading(Action<CompassHeading> compassSuccess, Action<CompassError> compassError,  CompassOptions options=null)
        {

        }

        /// <summary>
        /// At a regular interval, get the compass heading in degrees.
        /// </summary>
        /// <param name="compassSuccess"></param>
        /// <param name="compassError"></param>
        /// <param name="options"></param>
        public void watchHeading(Action<CompassHeading> compassSuccess, Action<CompassError> compassError, CompassOptions options=null)
        {

        }

        /// <summary>
        /// Stop watching the compass referenced by the watch ID parameter
        /// </summary>
        /// <param name="watchID"></param>
       public void clearWatch(int watchID)
       {

       }

        #endregion

    }
}
