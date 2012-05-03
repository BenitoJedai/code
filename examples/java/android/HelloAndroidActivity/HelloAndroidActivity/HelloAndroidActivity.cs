using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using ScriptCoreLib;

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


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            setContentView(R.layout.main);
        }
    }
}
