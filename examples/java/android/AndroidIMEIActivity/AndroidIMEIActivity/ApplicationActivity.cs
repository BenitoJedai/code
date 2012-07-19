using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.provider;
using android.telephony;
using android.view;
using android.webkit;
using android.widget;
using AndroidIMEIActivity.Library;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidIMEIActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        // http://www.happygeek.in/programmatically-get-device-imei-in-android


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html

            base.onCreate(savedInstanceState);

            ScrollView sv = new ScrollView(this);

            LinearLayout ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);


            Button b = new Button(this);

            b.setText("Whats my IMEI?");

            b.setOnClickListener(
                new _onclick { that = this }
            );

            ll.addView(b);

            this.setContentView(sv);



        }


        class _onclick : android.view.View.OnClickListener
        {
            public ApplicationActivity that;

            int counter;

            public void onClick(View v)
            {
                TelephonyManager telephonyManager = (TelephonyManager)that.getSystemService(Context.TELEPHONY_SERVICE);

                string imei = telephonyManager.getDeviceId();

                that.ShowLongToast("IMEI: " + imei);

              
            }
        }

    }
}
