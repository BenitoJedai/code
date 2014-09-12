using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.widget;

namespace android.webkit
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/webkit/WebView.java

    // http://src.chromium.org/viewvc/chrome/trunk/src/chrome/android/java/src/org/chromium/chrome/browser/PKCS11AuthenticationManager.java
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/webkit/WebViewFragment.java

    // http://developer.android.com/reference/android/webkit/WebView.html
    // chrome has <webview>
    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs

    // https://code.google.com/p/chromium/issues/detail?id=413165
    // Cr-Mobile-WebView is for android webview.

    [Script(IsNative = true)]
    public class WebView : AbsoluteLayout
    {
        // how does this relate to the floating layout we have for android?


        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\IHTMLIFrame.cs


        // members and types are to be extended by jsc at release build


        // public SslCertificate getCertificate() {

        public static int SCROLLBARS_OUTSIDE_OVERLAY;

        public void freeMemory()
        { 
        }

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
