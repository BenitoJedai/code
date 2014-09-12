using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.widget;

namespace android.webkit
{

    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/webkit/WebViewClient.java
    // http://developer.android.com/reference/android/webkit/WebViewClient.html

    [Script(IsNative = true)]
    public class WebViewClient
    {
        public virtual bool shouldOverrideUrlLoading(WebView view, string url)
        {
            return default(bool);
        }
    }
}
