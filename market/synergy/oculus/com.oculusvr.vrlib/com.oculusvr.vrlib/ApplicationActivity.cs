using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.provider;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLibJava.Extensions;

namespace com.oculusvr.vrlib.Activities
{
    public class ApplicationActivity : Activity
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20141127



        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // cmd /K c:\util\android-sdk-windows\platform-tools\adb.exe logcat
            // Camera PTP

            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html

            base.onCreate(savedInstanceState);

            ScrollView sv = new ScrollView(this);

            LinearLayout ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);


            Button b = new Button(this);

            b.setText("Notify!");
            int counter = 0;

           


            ll.addView(b);

            this.setContentView(sv);

            this.ShowToast("http://jsc-solutions.net");


        }




    }
}
