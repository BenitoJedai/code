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
    public class AccelerometerOptions
    {
        #region Constructor

        public AccelerometerOptions()
        {

        }

        public int frequency = 10000;


        #endregion

    }
}
