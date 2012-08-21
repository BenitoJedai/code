using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.widget;

namespace android.webkit
{
    // http://developer.android.com/reference/android/webkit/WebSettings.html
    [Script(IsNative = true)]
    public class WebSettings
    {
        public void setJavaScriptEnabled(bool value)
        {
        }
    }
}
