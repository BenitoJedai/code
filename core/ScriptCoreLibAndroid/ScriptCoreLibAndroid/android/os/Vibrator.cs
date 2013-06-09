using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // http://developer.android.com/reference/android/os/Vibrator.html
    [Script(IsNative = true)]
    public abstract class Vibrator
    {
        public abstract void vibrate(long milliseconds);
    }
}
