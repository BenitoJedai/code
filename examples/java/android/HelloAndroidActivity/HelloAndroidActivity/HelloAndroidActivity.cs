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
        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            setContentView(R.layout.main);
        }
    }
}
