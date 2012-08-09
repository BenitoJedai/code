using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.os;
using android.provider;
using android.webkit;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;

namespace AndroidVibrationActivity.Activities
{
    public class ApplicationActivity : Activity
    {


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            ScrollView sv = new ScrollView(this);

            LinearLayout ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);


            var b = new Button(this);

            b.setText("Vibrate!");

            b.AtClick(
                delegate
                {
                    var vibrator = (Vibrator)this.getSystemService(Context.VIBRATOR_SERVICE);

                    vibrator.vibrate(600);
                }
            );

            ll.addView(b);



            this.setContentView(sv);


            this.ShowLongToast("http://my.jsc-solutions.net x");
        }


    }
}
