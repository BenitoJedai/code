
using android.app;
using android.content;
//using android.database;
//using android.database.sqlite;
//using android.provider;
using android.util;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using System;

namespace AndroidWebViewActivity.Activities
{
	[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
	//[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
	public class AndroidWebViewActivity : Activity
    {
        public WebView webview;
        //public ProgressDialog progressBar;
        //public AlertDialog alertDialog;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            //this.ToFullscreen();

            // http://stackoverflow.com/questions/8955228/webview-with-an-iframe-android
            // http://www.chrisdanielson.com/tag/webviewclient/

            //this.alertDialog = new AlertDialog.Builder(this).create();

            //this.progressBar = ProgressDialog.show(this, "look here!", "Loading...");
            this.webview = new WebView(this);

			//LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(300, 300);
			//this.getWindow().setLayoutParams(layoutParams);

			getWindow().setFlags(
                WindowManager_LayoutParams.FLAG_HARDWARE_ACCELERATED,
                WindowManager_LayoutParams.FLAG_HARDWARE_ACCELERATED);

            setContentView(webview);

            //webview.getSettings().setSupportZoom(true); 
            //webview.getSettings().setLoadsImagesAutomatically(true);
            webview.getSettings().setJavaScriptEnabled(true);
            //webview.getSettings().setBuiltInZoomControls(true);
            //webview.setInitialScale(1);

            webview.setWebViewClient(new MyWebViewClient { __this = this });

            //webview.getSettings().setSupportZoom(true);
            //webview.setScrollBarStyle(WebView.SCROLLBARS_INSIDE_OVERLAY);

            //webview.getSettings().setJavaScriptEnabled(true);

            // no flash in emulator?
            // works on my phone!
            // no Flash since android 4.1.0!!!
            //webview.getSettings().setPluginsEnabled(true);
            //webview.getSettings().setPluginState(android.webkit.WebSettings.PluginState.ON);



            // OR, you can also load from an HTML string:
            //var summary = "<html><body>You scored <b>192</b> points.</body></html>";
            //webview.loadData(summary, "text/html", null);
            //Log.i(TAG, "loadUrl");

            //var uri = "http://cubiq.org/dropbox/3dcity/";
            var uri = "http://webglreport.com/";
            webview.loadUrl(uri);
            //this.ShowToast(uri);

            #region AtPrepareOptions
            AtPrepareOptions +=
                value =>
                {
                    value.clear();

                    var item2 = value.add(
                      (java.lang.CharSequence)(object)uri
                  );


                    item2.setIcon(android.R.drawable.ic_menu_view);

                    var i = new Intent(Intent.ACTION_VIEW,
                        android.net.Uri.parse(uri)
                    );

                    // http://vaibhavsarode.wordpress.com/2012/05/14/creating-our-own-activity-launcher-chooser-dialog-android-launcher-selection-dialog/
                    var ic = Intent.createChooser(i, uri);


                    item2.setIntent(
                        ic
                    );
                };
            #endregion


        }

        #region AtPrepareOptions
        public event Action<Menu> AtPrepareOptions;

        public override bool onPrepareOptionsMenu(Menu value)
        {
            if (AtPrepareOptions != null)
                AtPrepareOptions(value);



            return base.onPrepareOptionsMenu(value);
        }
        #endregion

        class MyWebViewClient : WebViewClient
        {
            public AndroidWebViewActivity __this;

            public override bool shouldOverrideUrlLoading(WebView view, string url)
            {
                //Log.i(TAG, "Processing webview url click...");
                view.loadUrl(url);
                return true;
            }

            //public override void onPageFinished(WebView view, string url)
            //{
            //    //Log.i(TAG, "Finished loading URL: " + url);
            //    if (__this.progressBar.isShowing())
            //    {
            //        __this.progressBar.dismiss();
            //    }
            //}

            //public override void onReceivedError(WebView view, int errorCode, string description, string failingUrl)
            //{
            //    //Log.e(TAG, "Error: " + description);

            //    __this.ShowToast("Oh no! " + description);

            //    //Toast.makeText(__this, "Oh no! " + description, Toast.LENGTH_SHORT).show();
            //    //__this.alertDialog.setTitle((CharSequence)(object)"Error");
            //    //__this.alertDialog.setMessage(description);
            //    //__this.alertDialog.setButton((CharSequence)(object)"OK", new DialogInterface.OnClickListener() {
            //    //    public void onClick(DialogInterface dialog, int which) {
            //    //        return;
            //    //    }
            //    //});
            //    //__this.alertDialog.show();
            //}
        }
    }
}
