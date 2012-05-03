using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.provider;
using android.webkit;
using android.widget;
using AndroidVortexLesson1.Library;
using ScriptCoreLib;

namespace AndroidVortexLesson1.Activities
{
    // A package name must be constitued of two Java identifiers.

    [Script]
    public class VortexActivity : Activity
    {
        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidVortexLesson1.Activities --activity VortexActivity  --target 2  --path Z:\jsc.svn\examples\java\android\AndroidVortexLesson1\AndroidVortexLesson1\staging

        // http://developer.android.com/guide/developing/device.html#setting-up

        // running it in emulator:
        // C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r  "Z:\jsc.svn\examples\java\android\HelloAndroidActivity\HelloAndroidActivity\staging\HelloAndroidActivity\bin\HelloAndroidActivity-debug.apk"

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        //Z:\jsc.svn\examples\java\android\HelloAndroid>C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);


            ScrollView sv = new ScrollView(this);

            LinearLayout ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);



            TextView tv = new TextView(this);

            tv.setText("What would you like to create today?");

            ll.addView(tv);



            EditText et = new EditText(this);

            et.setText("JSC");

            ll.addView(et);



            Button b = new Button(this);

            b.setText("I don't do anything, but I was added dynamically. :)");

            ll.addView(b);





            this.setContentView(sv);

        }

     
    }
}
