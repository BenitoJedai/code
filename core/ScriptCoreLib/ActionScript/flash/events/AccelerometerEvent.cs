using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/events/AccelerometerEvent.html
    [Script(IsNative = true)]
    public class AccelerometerEvent : Event
    {
        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a AccelerometerEvent event object.
        /// </summary>
        public static readonly string UPDATE = "update";

        #endregion


        #region Properties
        /// <summary>
        /// Acceleration along the x-axis, measured in Gs.
        /// </summary>
        public double accelerationX { get; set; }

        /// <summary>
        /// Acceleration along the y-axis, measured in Gs.
        /// </summary>
        public double accelerationY { get; set; }

        /// <summary>
        /// Acceleration along the z-axis, measured in Gs.
        /// </summary>
        public double accelerationZ { get; set; }

        /// <summary>
        /// The number of milliseconds at the time of the event since the runtime was initialized.
        /// </summary>
        public double timestamp { get; set; }

        #endregion

    }
}
