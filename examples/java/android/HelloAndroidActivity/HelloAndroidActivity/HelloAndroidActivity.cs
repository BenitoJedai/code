﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.widget;
using ScriptCoreLib;

using com.example.helloandroid.Library;
using android.provider;
using android.webkit;

namespace com.example.helloandroid
{
    [Script]
    public class HelloAndroidActivity : Activity
    {
        // C:\util\android-sdk-windows\tools\android.bat create project --package com.example.helloandroid --activity HelloAndroidActivity  --target 2  --path Z:\jsc.svn\examples\java\android\HelloAndroidActivity\HelloAndroidActivity\staging\HelloAndroidActivity
        // JSC should not explicity import all interfaces like Callback if not being defined 
        // see also: 
        // http://stackoverflow.com/questions/4055634/simple-java-question
        // http://developer.android.com/guide/developing/building/building-cmdline.html
        // http://developer.android.com/guide/developing/device.html#setting-up

        // running it in emulator:
        // C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r  "Z:\jsc.svn\examples\java\android\HelloAndroidActivity\HelloAndroidActivity\staging\HelloAndroidActivity\bin\HelloAndroidActivity-debug.apk"

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        //Z:\jsc.svn\examples\java\android\HelloAndroid>C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);
            //setContentView(R.layout.main);


            ScrollView sv = new ScrollView(this);

            LinearLayout ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);

            //// http://stackoverflow.com/questions/9784570/webview-inside-scrollview-disappears-after-zooming
            //// http://stackoverflow.com/questions/8123804/unable-to-add-web-view-dynamically
            //// http://developer.android.com/reference/android/webkit/WebView.html
            //WebView web = new WebView(this);
            ////web.setId(1);
            //web.getSettings().setJavaScriptEnabled(true);
            //web.setWebViewClient(new HelloWebViewClient());

            //web.loadUrl("http://www.jsc-solutions.net");

            //ll.addView(web);




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

                ll.addView(cb);

            }

            this.setContentView(sv);

        }

        [Script]
        private class HelloWebViewClient : WebViewClient {
            public bool shouldOverrideUrlLoading(WebView view, string url) {
                view.loadUrl(url);
                return true;
            }
        }
    }
}
