using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.widget;

namespace android.webkit
{
    // http://developer.android.com/reference/android/webkit/WebView.html
    [Script(IsNative = true)]
    public class WebView : AbsoluteLayout
    {
        // members and types are to be extended by jsc at release build

        public static int SCROLLBARS_OUTSIDE_OVERLAY;

        public override void setScrollBarStyle(int style)
        {
        }

        public WebView(Context c)
            : base(c)
        {

        }

        public WebSettings getSettings()
        {
            return default(WebSettings);
        }

        public virtual bool canGoBack()
        {
            return default(bool);
        }

        public virtual void goBack()
        {
        }


        public virtual void loadUrl(string value)
        {

        }

        public virtual void setWebViewClient(WebViewClient value)
        {

        }

        public virtual void setWebChromeClient(WebChromeClient value)
        {

        }
    }
}
