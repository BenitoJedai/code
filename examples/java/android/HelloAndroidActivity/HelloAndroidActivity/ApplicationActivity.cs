using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;

namespace HelloAndroidActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);


            var b = new Button(this);
            b.setText((java.lang.CharSequence)(object)"I don't do anything, but I was added dynamically. :)");
            ll.addView(b);


            Action onclick = delegate
            {
                b.setText((java.lang.CharSequence)(object)"onclick");
            };

            b.setText((java.lang.CharSequence)(object)"before AtClick");
            b.AtClick(
                v =>
                {
                    b.setText((java.lang.CharSequence)(object)"AtClick");
                }
            );
            
            this.setContentView(sv);
        }

        void InitializeContent()
        {
            Action handler =
                delegate
                {
                };
        }
    }

    public static class X
    {
        class OnClickListener : View.OnClickListener
        {
            public Action<View> h;

            public void onClick(View v)
            {
                h(v);
            }
        }

        public static void AtClick(this View v,  Action<View> h)
        {
            v.setOnClickListener(
                new OnClickListener { h = h }
            );
        }

    }
}
