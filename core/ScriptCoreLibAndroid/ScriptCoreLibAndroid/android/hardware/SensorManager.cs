using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.hardware
{

    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/hardware/SensorManager.java

    // http://developer.android.com/reference/android/hardware/SensorManager.html
    [Script(IsNative = true)]
    public class SensorManager
    {
        public static readonly int SENSOR_DELAY_NORMAL;

        public Sensor getDefaultSensor(int type)
        {
            return default(Sensor);
        }

        public bool registerListener(SensorEventListener listener, Sensor sensor, int rate)
        {
            return default(bool);
        }
    }
}
