using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.sensors
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/sensors/Accelerometer.html

    [Script(IsNative = true)]
    public class Accelerometer : EventDispatcher
    {
        // http://blog.yoz.sk/2010/12/geolocation-accelerometer-orientation/

        #region Methods
        /// <summary>
        /// The setRequestedUpdateInterval method is used to set the desired time interval for updates.
        /// </summary>
        public void setRequestedUpdateInterval(double interval)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new Accelerometer instance.
        /// </summary>
        public Accelerometer()
        {
        }

        #endregion

        #region Properties
        /// <summary>
        /// [read-only] Specifies whether the user has denied access to the accelerometer (true) or allowed access (false).
        /// </summary>
        public bool muted { get; private set; }

        #endregion


        public static bool isSupported { get; private set; }

        #region Events
        /// <summary>
        /// Dispatched when an accelerometer changes its status.	Accelerometer
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<StatusEvent> status;

        /// <summary>
        /// The update event is dispatched in response to updates from the accelerometer sensor.	Accelerometer
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<AccelerometerEvent> update;

        #endregion


    }
}
