using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.content.res;
using android.provider;
using android.webkit;
using android.widget;
using AndroidTabViewActivity.Library;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidTabViewActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        // http://www.techwavedev.com/?p=14
        // http://www.androidhive.info/2011/08/android-tab-layout-tutorial/
        // http://stackoverflow.com/questions/6685257/android-tabhost-addtab-null-pointer-exception
        // http://stackoverflow.com/questions/6674044/android-application-is-not-runnning-errorresourcesnotfoundexception-resource

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            var c = this;

            var th = new TabHost(c);


            LinearLayout ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            th.addView(ll);


            var tw = new TabWidget(c);

            var fl = new FrameLayout(c);



            tw.AttachTo(ll);
            fl.AttachTo(ll);

            this.setContentView(th);
            th.setup();


            Resources res = this.getResources(); // Resource object to get Drawables


            var ts =
                th
                .newTabSpec("")
                .setIndicator(
                    (CharSequence)(object)"Hello"
                //,  res.getDrawable(R.drawable.ic_tab_main)
                )
                .setContent(

                    new XTabContentFactory { c = this }

                );

            th.addTab(ts
            );

            //th.addTab(th
            //    .newTabSpec("")
            //    .setIndicator(
            //        (CharSequence)(object)"World"
            //        //,  res.getDrawable(R.drawable.ic_tab_setup)
            //        )
            //    .setContent(new Intent(c, GetMainActivityClass()))
            //);

            this.ShowLongToast("http://jsc-solutions.net");


        }

        [Script(OptimizedCode = "return AndroidTabViewActivity.Activities.MainActivity.class;")]
        public static Class GetMainActivityClass()
        {
            return null;
        }

        class XTabContentFactory : android.widget.TabHost.TabContentFactory
        {
            public Context c;

            public android.view.View createTabContent(string value)
            {
                ScrollView sv = new ScrollView(c);

                LinearLayout ll = new LinearLayout(c);

                ll.setOrientation(LinearLayout.VERTICAL);

                sv.addView(ll);

                //// http://stackoverflow.com/questions/9784570/webview-inside-scrollview-disappears-after-zooming
                //// http://stackoverflow.com/questions/8123804/unable-to-add-web-view-dynamically
                //// http://developer.android.com/reference/android/webkit/WebView.html



                TextView tv = new TextView(c);

                tv.setText("What would you like to create today?");

                ll.addView(tv);



                EditText et = new EditText(c);

                et.setText("JSC");

                ll.addView(et);



                Button b = new Button(c);

                b.setText("I don't do anything, but I was added dynamically. :)");

                ll.addView(b);




          
                return sv;
            }
        }
    }

    class MainActivity : Activity
    {
        protected override void onCreate(android.os.Bundle savedInstanceState)
        {

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
        }
    }
}
