using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/device_orientation/DeviceOrientationEvent.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/device_orientation/WindowDeviceOrientation.idl

    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/device_orientation/WindowDeviceMotion.idl
    // http://www.html5rocks.com/en/tutorials/device/orientation/
    // http://caniuse.com/#feat=deviceorientation
    // http://www.w3.org/TR/orientation-event/

    [Script(HasNoPrototype = true, ExternalTarget = "DeviceOrientationEvent")]
    public class DeviceOrientationEvent : IEvent
    {
        // X:\jsc.svn\examples\javascript\android\Test\TestDeviceOrientationEvent\TestDeviceOrientationEvent\Application.cs
        // X:\jsc.svn\examples\javascript\android\Test\TestCompassHeading\TestCompassHeading\Application.cs

        public readonly bool absolute;
        public readonly double alpha;
        public readonly double beta;
        public readonly double gamma;

        public DeviceOrientationEvent(string type) { }
    }

    partial class IWindow
    {
        // what about accelertion?
        // http://www.albertosarullo.com/blog/javascript-accelerometer-demo-source
        // tested by?

        #region event deviceorientation
        public event Action<DeviceOrientationEvent> ondeviceorientation
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "deviceorientation");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "deviceorientation");
            }
        }
        #endregion
    }
}
