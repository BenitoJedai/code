using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.widget;

namespace android.webkit
{
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
