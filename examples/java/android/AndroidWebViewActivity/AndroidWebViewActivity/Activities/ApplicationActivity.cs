
using android.app;
using android.content;
using android.database;
using android.database.sqlite;
using android.provider;
using android.webkit;
using android.widget;
using AndroidWebViewActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidWebViewActivity.Activities
{
    public class AndroidWebViewActivity : Activity
    {
        // inspired by http://android-er.blogspot.com/2011/06/simple-example-using-androids-sqlite.html

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidWebViewActivity.Activities --activity AndroidWebViewActivity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidWebViewActivity\AndroidWebViewActivity\staging\


        // running it in emulator:
        // start C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r  "y:\jsc.svn\examples\java\android\AndroidWebViewActivity\AndroidWebViewActivity\staging\bin\AndroidWebViewActivity-debug.apk"

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        //Z:\jsc.svn\examples\java\android\HelloAndroid>C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            WebView webview = new WebView(this);

            webview.getSettings().setSupportZoom(true); 
            webview.getSettings().setLoadsImagesAutomatically(true);
            webview.getSettings().setJavaScriptEnabled(true);
            webview.getSettings().setBuiltInZoomControls(true);
            webview.setInitialScale(100);  

            //webview.getSettings().setJavaScriptEnabled(true);
            webview.getSettings().setPluginsEnabled(true);
            setContentView(webview);

            // OR, you can also load from an HTML string:
            //var summary = "<html><body>You scored <b>192</b> points.</body></html>";
            //webview.loadData(summary, "text/html", null);
            webview.loadUrl("http://studio.jsc-solutions.net");

            this.ShowToast("http://jsc-solutions.net");
        }


    }
}
