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
    /// onError callback function for compass functions
    /// http://docs.phonegap.com/en/1.7.0/cordova_compass_compass.md.html#compassError
    /// </summary>
    [Script(IsNative = true)]
    public class CompassError
    {
        #region Constructor

        public CompassError()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// One of the pre-defined capture error codes listed below
        /// </summary>
        public int code;

        #endregion

        #region CONSTS

        public static int COMPASS_INTERNAL_ERR = 0;   
        public static int COMPASS_NOT_SUPPORTED = 20; 

        #endregion


    }
}
