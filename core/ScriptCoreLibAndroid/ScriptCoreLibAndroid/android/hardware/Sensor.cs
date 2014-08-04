using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.hardware
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/hardware/Sensor.java

    [Script(IsNative = true)]
    public class Sensor
    {
        // tested by ?
        // X:\jsc.svn\examples\java\android\AndroidAccelerometerActivity\AndroidAccelerometerActivity\ApplicationActivity.cs

        public static readonly int TYPE_ORIENTATION;

        public int getType()
        {
            return default(int);

        }
    }
}
