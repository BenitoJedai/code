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
    /// Contains Accelerometer data captured at a specific point in time.
    /// http://docs.phonegap.com/en/1.7.0/cordova_accelerometer_accelerometer.md.html#Acceleration
    /// </summary>
    [Script(IsNative = true)]
    public class Acceleration
    {
        #region Constructor

        public Acceleration()
        {
            
        }

       
        
        /// <summary>
        /// x: Amount of acceleration on the x-axis. (in m/s^2) (Number)
        /// </summary>
        public int x { get { return default(int); } }


        /// <summary>
        /// y: Amount of acceleration on the y-axis. (in m/s^2) (Number)
        /// </summary>
        public int y { get { return default(int); } }


        /// <summary>
        /// z: Amount of acceleration on the z-axis. (in m/s^2) (Number)
        /// </summary>
        public int z { get { return default(int); } }


        /// <summary>
        /// timestamp: Creation timestamp in milliseconds. (DOMTimeStamp)
        /// </summary>
        public int timestamp { get { return default(int); }  }



        #endregion

    }
}
