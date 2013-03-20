using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.widget;

namespace android.webkit
{
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
