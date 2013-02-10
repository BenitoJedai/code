using android.content;
using android.os;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace com.abstractatech.battery
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void batteryStatus(Action<string> y)
        {
            // http://developer.android.com/training/monitoring-device-state/battery-monitoring.html
            // http://davidwalsh.name/battery-api
            // https://developer.mozilla.org/en-US/docs/DOM/window.navigator.battery

            // Because it's a sticky intent, you don't need to 
            // register a BroadcastReceiver—by simply 
            // calling registerReceiver passing in null as the receiver as 
            // shown in the next snippet, the current battery status 
            // intent is returned.

            var context = global::ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;

            IntentFilter ifilter = new IntentFilter(Intent.ACTION_BATTERY_CHANGED);
            Intent batteryStatus = context.registerReceiver(null, ifilter);

            int level = batteryStatus.getIntExtra(BatteryManager.EXTRA_LEVEL, -1);
            int scale = batteryStatus.getIntExtra(BatteryManager.EXTRA_SCALE, -1);

            float batteryPct = level / (float)scale;

            // jsc please implement other datatypes :)
            y("" + batteryPct);
        }

    }
}
