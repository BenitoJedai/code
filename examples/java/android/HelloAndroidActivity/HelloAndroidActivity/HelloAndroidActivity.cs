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

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            setContentView(R.layout.main);
        }
    }
}
