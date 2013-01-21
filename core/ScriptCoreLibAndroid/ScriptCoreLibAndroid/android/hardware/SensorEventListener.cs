using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.hardware
{
    // http://developer.android.com/reference/android/hardware/SensorEventListener.html
    [Script(IsNative = true)]
    public interface SensorEventListener
    {
        void onAccuracyChanged(Sensor sensor, int accuracy);
        void onSensorChanged(SensorEvent e);
    }
}
