using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/BatteryManager.java
    [Script(IsNative = true)]
    public class BatteryManager
    {
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.battery\com.abstractatech.battery\ApplicationWebService.cs
        public static readonly string EXTRA_STATUS = "status";
        public static readonly string EXTRA_PLUGGED = "plugged";


        public static readonly int BATTERY_STATUS_CHARGING = 2;

        public static readonly string EXTRA_LEVEL = "level";
        public static readonly string EXTRA_SCALE = "scale";
    }

}
