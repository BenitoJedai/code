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
    /// An optional parameter to customize the retrieval of the accelerometer
    /// http://docs.phonegap.com/en/1.7.0/cordova_accelerometer_accelerometer.md.html#accelerometerOptions
    /// </summary>
    [Script(IsNative = true)]
    public class CaptureAudioOptions
    {
        #region Constructor

        public CaptureAudioOptions()
        {

        }
        
        #endregion

        #region VARS

        public int frequency = 10000;

        #endregion

        #region PROPERTIES


        /// <summary>
        /// limit: The maximum number of audio clips the device user can record in a single capture operation. 
        /// The value must be greater than or equal to 1 (defaults to 1).
        /// </summary>
        public int limit;



        /// <summary>
        /// duration: The maximum duration of an audio sound clip, in seconds.
        /// </summary>
        public int duration;




        /// <summary>
        ///mode: The selected audio mode. The value must match one of the elements in capture.supportedAudioModes.
        /// </summary>
        public object mode;

        #endregion

    }
}
