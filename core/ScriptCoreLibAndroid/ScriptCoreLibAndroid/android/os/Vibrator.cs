using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/Vibrator.java

    // http://developer.android.com/reference/android/os/Vibrator.html
    [Script(IsNative = true)]
    public abstract class Vibrator
    {
        // tested by
        // X:\jsc.svn\examples\java\android\AndroidVibrationActivity\AndroidVibrationActivity\ApplicationActivity.cs

        public abstract void vibrate(long milliseconds);
    }
}
