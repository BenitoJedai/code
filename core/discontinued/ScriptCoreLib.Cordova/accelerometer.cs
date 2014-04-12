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
    /// Captures device motion in the x, y, and z direction
    /// http://docs.phonegap.com/en/1.7.0/cordova_accelerometer_accelerometer.md.html#Accelerometer
    /// </summary>
    //[Script(IsNative = true)]
    //[Script(IsNative = true)]
    [Script(HasNoPrototype = true, ExternalTarget = "Accelerometer")]
	public class Accelerometer
	{
		#region Constructor

		public Accelerometer()
		{
		}

        /// <summary>
        /// The accelerometer is a motion sensor that detects the change (delta) in movement relative to the current device orientation. 
        /// The accelerometer can detect 3D movement along the x, y, and z axis. 
        /// The acceleration is returned using the accelerometerSuccess callback function.
        /// </summary>
        /// <param name="onSuccess"></param>
        /// <param name="onError"></param>
        public void getCurrentAcceleration(Action<Acceleration> onSuccess, Action onError)
        {

        }


        /// <summary>
        /// The accelerometer is a motion sensor that detects the change (delta) in movement relative to the current position. The accelerometer can detect 3D movement along the x, y, and z axis.
        /// The accelerometer.watchAcceleration gets the device's current acceleration at a regular interval. Each time the Acceleration is retrieved, the accelerometerSuccess callback function is executed. 
        /// Specify the interval in milliseconds via the frequency parameter in the acceleratorOptions object.
        /// The returned watch ID references references the accelerometer watch interval. The watch ID can be used with accelerometer.clearWatch to stop watching the accelerometer.
        /// </summary>
        /// <param name="onSuccess"></param>
        /// <param name="onError"></param>
        /// <param name="?"></param>
        public void watchAcceleration(Action<Acceleration> onSuccess, Action onError,AccelerometerOptions options=null)
        {

        }
        

        /// <summary>
        /// Stop watching the Acceleration referenced by the watch ID parameter.
        /// </summary>
        /// <param name="watchID"></param>
        public void clearWatch(int watchID)
        {
        }

		#endregion

	}
}
