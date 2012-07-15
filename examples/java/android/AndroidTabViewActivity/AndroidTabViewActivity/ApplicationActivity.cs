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
        // http://www.devdaily.com/java/jwarehouse/android/core/java/android/widget/TabHost.java.shtml

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
            tw.setId(android.R.id.tabs);
            tw.AttachTo(ll);

            var fl = new FrameLayout(c);
            fl.setId(android.R.id.tabcontent);
            fl.AttachTo(ll);


            //th.str

            this.setContentView(th);


            //  Your TabHost must have a TabWidget whose id attribute is 'android.R.id.tabs'
            // what?
            // android.widget.TabHost cannot be cast to android.widget.TabWidget
            th.setup();
            // for some reason R.layout.tab_indicator cannot be loaded and causes a fault!
            // http://www.devdaily.com/java/jwarehouse/android/core/java/android/widget/TabHost.java.shtml
            // http://ericharlow.blogspot.com/2010/10/experience-customizing-androids-tab.html

            {
                var a = new TextView(c);

                a.setText("Hello1");

                var ts =
                    th
                    .newTabSpec("")
                    .setIndicator(
                    a
                    //(CharSequence)(object)"Hello"
                    //,  res.getDrawable(R.drawable.ic_tab_main)
                    )
                    .setContent(

                        new XTabContentFactory { c = this }

                    );

                //E/AndroidRuntime( 1610): Caused by: android.content.res.Resources$NotFoundException: Resource ID #0x0
                //E/AndroidRuntime( 1610):        at android.content.res.Resources.getValue(Resources.java:1018)
                //E/AndroidRuntime( 1610):        at android.content.res.Resources.loadXmlResourceParser(Resources.java:2105)
                //E/AndroidRuntime( 1610):        at android.content.res.Resources.getLayout(Resources.java:857)
                //E/AndroidRuntime( 1610):        at android.view.LayoutInflater.inflate(LayoutInflater.java:394)
                //E/AndroidRuntime( 1610):        at android.widget.TabHost$LabelIndicatorStrategy.createIndicatorView(TabHost.java:531)
                //E/AndroidRuntime( 1610):        at android.widget.TabHost.addTab(TabHost.java:223)
                //E/AndroidRuntime( 1610):        at AndroidTabViewActivity.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:54)

                th.addTab(ts
                );
            }

            {
                var a = new TextView(c);

                a.setText("Hello2");

                var ts =
                    th
                    .newTabSpec("")
                    .setIndicator(
                    a
                    //(CharSequence)(object)"Hello"
                    //,  res.getDrawable(R.drawable.ic_tab_main)
                    )
                    .setContent(

                        new YTabContentFactory { c = this }

                    );

                //E/AndroidRuntime( 1610): Caused by: android.content.res.Resources$NotFoundException: Resource ID #0x0
                //E/AndroidRuntime( 1610):        at android.content.res.Resources.getValue(Resources.java:1018)
                //E/AndroidRuntime( 1610):        at android.content.res.Resources.loadXmlResourceParser(Resources.java:2105)
                //E/AndroidRuntime( 1610):        at android.content.res.Resources.getLayout(Resources.java:857)
                //E/AndroidRuntime( 1610):        at android.view.LayoutInflater.inflate(LayoutInflater.java:394)
                //E/AndroidRuntime( 1610):        at android.widget.TabHost$LabelIndicatorStrategy.createIndicatorView(TabHost.java:531)
                //E/AndroidRuntime( 1610):        at android.widget.TabHost.addTab(TabHost.java:223)
                //E/AndroidRuntime( 1610):        at AndroidTabViewActivity.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:54)

                th.addTab(ts
                );
            }

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


        class YTabContentFactory : android.widget.TabHost.TabContentFactory
        {
            public Context c;

            public android.view.View createTabContent(string value)
            {
                ScrollView sv = new ScrollView(c);

                LinearLayout ll = new LinearLayout(c);

                ll.setOrientation(LinearLayout.VERTICAL);

                sv.addView(ll);


                Button b = new Button(c);

                b.setText("Yay. This almost works!");

                ll.addView(b);





                return sv;
            }
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


                Button b = new Button(c);

                b.setText("I don't do anything, but I was added dynamically. :)");

                ll.addView(b);




          
                return sv;
            }
        }
    }

  }
