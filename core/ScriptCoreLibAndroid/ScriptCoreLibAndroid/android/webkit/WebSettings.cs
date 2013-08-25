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
    public abstract class WebSettings
    {
        public static readonly int LOAD_NO_CACHE = 0x00000002;

        public void setCacheMode(int value)
        {
        }

        public void setJavaScriptEnabled(bool value)
        {
        }
    }
}
