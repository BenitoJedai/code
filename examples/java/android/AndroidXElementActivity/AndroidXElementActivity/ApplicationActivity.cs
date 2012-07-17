using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.provider;
using android.webkit;
using android.widget;
using AndroidXElementActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidXElementActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        public const string Version = "http://www.jsc-solutions.net/assets/PromotionWebApplication1/jsc.configuration.application";



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



            this.setContentView(sv);


            this.ShowLongToast("http://jsc-solutions.net");
        }


    }
}
