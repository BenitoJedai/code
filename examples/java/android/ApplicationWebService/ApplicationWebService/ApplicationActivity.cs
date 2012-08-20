using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using android.webkit;

namespace ApplicationWebService.Activities
{
    public class ApplicationActivity : Activity
    {
        protected override void onCreate(Bundle savedInstanceState)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20120-1/201208
            // this is a prototype implementation for ApplicationWebService on android
            // we need to serve assets at random port
            // we need a webbrowser

            base.onCreate(savedInstanceState);

            var webview = new WebView(this);
            webview.setWebViewClient(new MyWebViewClient { });
            this.setContentView(webview);

            webview.loadUrl("http://example.com");
        }


        #region MyWebViewClient
        class MyWebViewClient : WebViewClient
        {
            public override bool shouldOverrideUrlLoading(WebView view, string url)
            {
                view.loadUrl(url);
                return true;
            }

            public override void onPageFinished(WebView view, string url)
            {

            }

            public override void onReceivedError(WebView view, int errorCode, string description, string failingUrl)
            {

            }
        }
        #endregion

    }


}
