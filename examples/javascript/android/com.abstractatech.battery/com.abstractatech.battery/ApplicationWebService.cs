using android.content;
using android.os;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace com.abstractatech.battery
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        //   I/System.Console( 8730): NewGlobalInvokeMethod load fields from client{ Name = batteryStatus }
        //I/System.Console( 8730): NewGlobalInvokeMethod load fields from client{ Name = isCharging }
        //I/System.Console( 8730): enter batteryStatus
        //I/System.Console( 8730): { batteryStatus = Intent { act=android.intent.action.BATTERY_CHANGED flg=0x60000010 (has extras) }, isCharging = false }
        //I/System.Console( 8730): InternalWebMethodInfo.AddField { FieldName = field_batteryStatus, FieldValue = 0.19 }
        //I/System.Console( 8730): InternalWebMethodInfo.AddField { FieldName = field_isCharging, FieldValue = false }
        //I/System.Console( 8730): { InternalFieldName = field_batteryStatus }
        //I/System.Console( 8730): { InternalFieldName = field_isCharging }
        //I/chromium( 8730): [INFO:CONSOLE(36565)] "%c0:599ms GetInternalFields save to localstorage! { InternalFieldsCookieValue = field_batteryStatus=0.19&field_isCharging=false& }", source: http://192.168.1.103:22180/view-source (36565)
        //I/chromium( 8730): [INFO:CONSOLE(51662)] "Uncaught TypeError: Cannot read property 'length' of null", source: http://192.168.1.103:22180/view-source (51662)

        //0:2750ms GetInternalFieldValue { FieldName = field_batteryStatus } view-source:36565
        //0:2750ms GetInternalFieldValue { FieldName = field_batteryStatus, FieldValue = 0.19 } view-source:36565
        //0:2750ms did we get xml back? are we supposed to merge it back? view-source:36565
        //0:2751ms GetInternalFieldValue { FieldName = field_isCharging } view-source:36565
        //0:2751ms GetInternalFieldValue { FieldName = field_isCharging, FieldValue = false } view-source:36565
        //0:2751ms did we get xml back? are we supposed to merge it back? view-source:36565
        //0:2752ms { a = 0.19, transform = rotate(-64.8deg) } 

        //// com.abstractatech.battery.Library.Templates.JavaScript.InternalWebMethodRequest.Complete
        //  type$z74XYrkTXzCbW_a2j9Mjk3w.CQAABrkTXzCbW_a2j9Mjk3w = function (b)
        //  {
        //    var a = [this], c, d, e, f, g, h, i, j, k;

        //    a[0].InternalFields = BQAABrkTXzCbW_a2j9Mjk3w(b);
        //    f = !!(~~b.length);

        // http://developer.android.com/training/monitoring-device-state/battery-monitoring.html
        // http://davidwalsh.name/battery-api
        // https://developer.mozilla.org/en-US/docs/DOM/window.navigator.battery

        // Because it's a sticky intent, you don't need to 
        // register a BroadcastReceiver—by simply 
        // calling registerReceiver passing in null as the receiver as 
        // shown in the next snippet, the current battery status 
        // intent is returned.

        public float batteryStatus = 0;
        public bool isCharging = false;

        public async Task batteryStatusCheck()
        {
            // script: error JSC1000: No implementation found for this native method, please implement [System.Text.StringBuilder.Append(System.Int16)]

            // http://developer.android.com/training/monitoring-device-state/battery-monitoring.html
            // http://stackoverflow.com/questions/20663403/android-batterymanager-not-reporting-correct-charging-status
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\WebClient.cs

            Console.WriteLine("enter batteryStatus");

#if DEBUG
            batteryStatus = new Random().NextFloat();
            isCharging = new Random().NextDouble() > 0.5;


            //return Task.FromResult(new object());
#else


            // partial build?
            var ifilter = new IntentFilter(Intent.ACTION_BATTERY_CHANGED);

            var intent = global::ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.registerReceiver(null, ifilter);


            // I/System.Console(11387): { batteryStatus = Intent { act=android.intent.action.BATTERY_CHANGED flg=0x60000010 (has extras) }, isCharging = false }
            int status = intent.getIntExtra(BatteryManager.EXTRA_STATUS, -1);
            int chargePlug = intent.getIntExtra(BatteryManager.EXTRA_PLUGGED, -1);


            // X:\jsc.svn\examples\java\Test\TestJavaFinalIntegerField\TestJavaFinalIntegerField\Foo\Bar.java
            //isCharging = (chargePlug > 0);
            isCharging = status == BatteryManager.BATTERY_STATUS_CHARGING;

            Console.WriteLine("batteryStatusCheck " + new { status, chargePlug, isCharging });



            // http://developer.android.com/reference/android/os/BatteryManager.html
            // ???
            // is our java natives for android messing up const int?
            //public const int BATTERY_PLUGGED_AC = 0;
            //        public const int BATTERY_PLUGGED_USB = 0;
            //        public const int BATTERY_PLUGGED_WIRELESS = 0;

            //batteryStatusCheck { status = 3, chargePlug = 0 }
            //{ batteryStatus = Intent { act=android.intent.action.BATTERY_CHANGED flg=0x60000010 (has extras) }, isCharging = true }


            int level = intent.getIntExtra(BatteryManager.EXTRA_LEVEL, -1);
            int scale = intent.getIntExtra(BatteryManager.EXTRA_SCALE, -1);

            // are floats correctly serialize from android?
            this.batteryStatus = level / (float)scale;

            // android webview does not get our floats?
            Console.WriteLine(new { this.batteryStatus, intent, isCharging });
            //I/System.Console(11110): { batteryStatus = 0.8, intent = Intent { act=android.intent.action.BATTERY_CHANGED flg=0x60000000 (has extras) }, isCharging = true }

            //return Task.FromResult(batteryPct);
#endif

        }

        // jsc please implement other datatypes :)
    }
}
