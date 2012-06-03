using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.provider;
using android.webkit;
using android.widget;
using HelloAndroidActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace HelloAndroidActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        // C:\util\android-sdk-windows\tools\android.bat create project --package HelloAndroidActivity.Activities --activity HelloAndroidActivity  --target 2  --path y:\jsc.svn\examples\java\android\HelloAndroidActivity\HelloAndroidActivity\staging\
        // JSC should not explicity import all interfaces like Callback if not being defined 
        // see also: 
        // http://stackoverflow.com/questions/4055634/simple-java-question
        // http://developer.android.com/guide/developing/building/building-cmdline.html
        // http://developer.android.com/guide/developing/device.html#setting-up

        // running it in emulator:
        // start C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r  "y:\jsc.svn\examples\java\android\HelloAndroidActivity\HelloAndroidActivity\staging\bin\HelloAndroidActivity-debug.apk"

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

            //// http://stackoverflow.com/questions/9784570/webview-inside-scrollview-disappears-after-zooming
            //// http://stackoverflow.com/questions/8123804/unable-to-add-web-view-dynamically
            //// http://developer.android.com/reference/android/webkit/WebView.html
    


            TextView tv = new TextView(this);

            tv.setText("What would you like to create today?");

            ll.addView(tv);



            EditText et = new EditText(this);

            et.setText("JSC");

            ll.addView(et);



            Button b = new Button(this);

            b.setText("I don't do anything, but I was added dynamically. :)");

            ll.addView(b);




            for (int i = 0; i < 20; i++)
            {

                CheckBox cb = new CheckBox(this);

                cb.setText("I'm dynamic!");


                cb.AttachTo(ll);

            }

            this.setContentView(sv);


            this.ShowToast("http://jsc-solutions.net");
        }


    }
}
