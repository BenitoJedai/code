using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.widget;

namespace android.webkit
{

    //  https://github.com/android/platform_frameworks_base/blob/master/core/java/android/webkit/WebChromeClient.java
    // http://developer.android.com/reference/android/webkit/WebChromeClient.html
    [Script(IsNative = true)]
    public class WebChromeClient
    {
        public virtual void onReceivedTitle(WebView view, String title)
        {

        }
        public virtual void openFileChooser(ValueCallback<Uri> uploadMsg)
        {

        }
    }
}
