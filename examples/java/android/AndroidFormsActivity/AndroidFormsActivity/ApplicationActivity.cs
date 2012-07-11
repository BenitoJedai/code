using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.provider;
using android.webkit;
using android.widget;
using AndroidFormsActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidFormsActivity.Activities
{
    public class ApplicationActivity : Activity
    {


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);


            InitializeContent();
        }

        private void InitializeContent()
        {
            var u = new ApplicationControl();

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


            this.ShowLongToast("http://jsc-solutions.net");
        }


    }
}
