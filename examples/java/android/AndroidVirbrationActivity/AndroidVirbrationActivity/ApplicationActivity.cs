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
using AndroidVirbrationActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidVirbrationActivity.Activities
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

            b.setOnClickListener(
                new b_onclick
                {
                    __this = this
                }
            );

            ll.addView(b);



            this.setContentView(sv);


            this.ShowLongToast("http://jsc-solutions.net");
        }

        class b_onclick : ViewAnimator.OnClickListener
        {
            public ApplicationActivity __this;

            public void onClick(android.view.View value)
            {
                  Vibrator vibrator = (Vibrator)__this.getSystemService(Context.VIBRATOR_SERVICE);

                  vibrator.vibrate(600);
            }
        }
    }
}
