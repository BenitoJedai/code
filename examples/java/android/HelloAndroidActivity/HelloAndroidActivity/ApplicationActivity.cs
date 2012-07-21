using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.widget;
using ScriptCoreLib;

namespace HelloAndroidActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        Action Handler;

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

            this.setContentView(sv);
        }


    }
}
