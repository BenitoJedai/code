using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.hardware
{
    // http://developer.android.com/reference/android/hardware/SensorEvent.html
    [Script(IsNative = true)]
    public class SensorEvent
    {
        public Sensor sensor;

        public float[] values;
    }
}
