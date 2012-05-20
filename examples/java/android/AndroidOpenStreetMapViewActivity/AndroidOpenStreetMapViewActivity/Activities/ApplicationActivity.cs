
using android.app;
using android.content;
using android.database;
using android.database.sqlite;
using android.provider;
using android.util;
using android.view;
using android.webkit;
using android.widget;
using AndroidOpenStreetMapViewActivity.Library;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidOpenStreetMapViewActivity.Activities
{
    public class AndroidOpenStreetMapViewActivity : Activity
    {
        // inspired by http://android-er.blogspot.com/2012/05/openstreetmapview-openstreetmap-tools.html

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidOpenStreetMapViewActivity.Activities --activity AndroidOpenStreetMapViewActivity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidOpenStreetMapViewActivity\AndroidOpenStreetMapViewActivity\staging\


        // running it in emulator:
        // start C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r  "y:\jsc.svn\examples\java\android\AndroidOpenStreetMapViewActivity\AndroidOpenStreetMapViewActivity\staging\bin\AndroidOpenStreetMapViewActivity-debug.apk"

        // note: rebuild could auto reinstall

        // "C:\util\android-sdk-windows\platform-tools\adb.exe" uninstall   AndroidOpenStreetMapViewActivity.Activities

        // running it on device:
        // attach device to usb
        //Z:\jsc.svn\examples\java\android\HelloAndroid>C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        public WebView webview;
        public ProgressDialog progressBar;
        public AlertDialog alertDialog;

        //WindowManager hack;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);


            requestWindowFeature(Window.FEATURE_NO_TITLE);

            getWindow().setFlags(WindowManager_LayoutParams.FLAG_FULLSCREEN, WindowManager_LayoutParams.FLAG_FULLSCREEN);

            this.ShowToast("studio.jsc-solutions.net");

            // http://stackoverflow.com/questions/8955228/webview-with-an-iframe-android
            // http://www.chrisdanielson.com/tag/webviewclient/

            this.alertDialog = new AlertDialog.Builder(this).create();

            this.progressBar = ProgressDialog.show(this, (CharSequence)(object)"look here!", (CharSequence)(object)"Loading...");


            this.webview = new WebView(this);


            setContentView(webview);



            //webview.getSettings().setSupportZoom(true); 
            webview.getSettings().setLoadsImagesAutomatically(true);
            webview.getSettings().setJavaScriptEnabled(true);
            //webview.getSettings().setBuiltInZoomControls(true);
            //webview.setInitialScale(1);

            webview.getSettings().setSupportZoom(false);

            webview.setScrollBarStyle(WebView.SCROLLBARS_INSIDE_OVERLAY);

            //webview.getSettings().setJavaScriptEnabled(true);

            // no flash in emulator?
            webview.getSettings().setPluginsEnabled(true);
            webview.getSettings().setPluginState(android.webkit.WebSettings.PluginState.ON);

            webview.setWebViewClient(new MyWebViewClient { __this = this });


            // OR, you can also load from an HTML string:
            //var summary = "<html><body>You scored <b>192</b> points.</body></html>";
            //webview.loadData(summary, "text/html", null);
            Log.i(TAG, "loadUrl");
            webview.loadUrl("http://studio.jsc-solutions.net");

        }

        class MyWebViewClient : WebViewClient
        {
            public AndroidOpenStreetMapViewActivity __this;

            public override bool shouldOverrideUrlLoading(WebView view, string url)
            {
                Log.i(TAG, "Processing webview url click...");
                view.loadUrl(url);
                return true;
            }

            public override void onPageFinished(WebView view, string url)
            {
                Log.i(TAG, "Finished loading URL: " + url);
                if (__this.progressBar.isShowing())
                {
                    __this.progressBar.dismiss();
                }
            }

            public override void onReceivedError(WebView view, int errorCode, string description, string failingUrl)
            {
                Log.e(TAG, "Error: " + description);

                __this.ShowToast("Oh no! " + description);

                //Toast.makeText(__this, "Oh no! " + description, Toast.LENGTH_SHORT).show();
                //__this.alertDialog.setTitle((CharSequence)(object)"Error");
                //__this.alertDialog.setMessage(description);
                //__this.alertDialog.setButton((CharSequence)(object)"OK", new DialogInterface.OnClickListener() {
                //    public void onClick(DialogInterface dialog, int which) {
                //        return;
                //    }
                //});
                //__this.alertDialog.show();
            }
        }
    }
}
